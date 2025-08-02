import torch
import librosa
import numpy as np
import torch.nn.functional as F
import re
from transformers import (
    AutoFeatureExtractor, AutoModelForAudioClassification,
    pipeline
)
from typing import Dict, List, Optional
import warnings
import gc

def clean_text(text):
    text = text.lower().capitalize()
    text = re.sub(r'\s+', ' ', text.strip())
    if not text.endswith('.'):
        text += '.'
    return text

def chunk_audio(audio, sr, chunk_duration=30, overlap=2):
    """
    Split audio into overlapping chunks
    
    Args:
        audio: Audio signal
        sr: Sample rate
        chunk_duration: Duration of each chunk in seconds
        overlap: Overlap between chunks in seconds
    
    Returns:
        List of audio chunks
    """
    chunk_samples = int(chunk_duration * sr)
    overlap_samples = int(overlap * sr)
    stride = chunk_samples - overlap_samples
    
    chunks = []
    start = 0
    
    while start < len(audio):
        end = min(start + chunk_samples, len(audio))
        chunk = audio[start:end]
        
        # Pad if chunk is too short (for the last chunk)
        if len(chunk) < chunk_samples:
            chunk = np.pad(chunk, (0, chunk_samples - len(chunk)), mode='constant')
        
        chunks.append(chunk)
        
        if end >= len(audio):
            break
            
        start += stride
    
    return chunks

def analyze_audio_chunk(chunk, sr, models_dict):
    """Analyze a single audio chunk"""
    # Audio emotion analysis
    extractor = models_dict['extractor']
    audio_model = models_dict['audio_model']
    
    inputs = extractor(chunk, sampling_rate=sr, return_tensors="pt")
    
    with torch.no_grad():
        logits = audio_model(**inputs).logits
        audio_probs = F.softmax(logits, dim=-1).squeeze().tolist()
        audio_emotion = audio_model.config.id2label[int(np.argmax(audio_probs))]
    
    # Voice features for this chunk
    zcr = np.mean(librosa.feature.zero_crossing_rate(chunk))
    rmse = np.mean(librosa.feature.rms(y=chunk))
    mfcc = np.mean(librosa.feature.mfcc(y=chunk, sr=sr), axis=1)[:3]
    
    # Handle pitch extraction more carefully for chunks
    try:
        pitch, _ = librosa.piptrack(y=chunk, sr=sr)
        pitch_min = float(pitch[pitch > 0].min()) if np.any(pitch > 0) else 0.0
        pitch_max = float(pitch.max())
    except:
        pitch_min, pitch_max = 0.0, 0.0
    
    return {
        "audio_emotion": audio_emotion,
        "audio_emotion_probs": {audio_model.config.id2label[i]: float(prob) for i, prob in enumerate(audio_probs)},
        "voice_features": {
            "zcr": float(zcr),
            "rmse": float(rmse),
            "mfcc": [float(x) for x in mfcc],
            "pitch_min": pitch_min,
            "pitch_max": pitch_max,
        }
    }

def aggregate_chunk_results(chunk_results, weights=None):
    """Aggregate results from multiple chunks"""
    if not chunk_results:
        return {}
    
    if weights is None:
        weights = [1.0] * len(chunk_results)
    
    # Aggregate emotion probabilities (weighted average)
    emotion_labels = list(chunk_results[0]["audio_emotion_probs"].keys())
    aggregated_probs = {}
    
    for label in emotion_labels:
        weighted_sum = sum(result["audio_emotion_probs"][label] * w 
                          for result, w in zip(chunk_results, weights))
        aggregated_probs[label] = weighted_sum / sum(weights)
    
    # Get the most likely emotion
    best_emotion = max(aggregated_probs.items(), key=lambda x: x[1])[0]
    
    # Aggregate voice features (weighted average)
    voice_features = {}
    for feature in ["zcr", "rmse", "pitch_min", "pitch_max"]:
        if feature in ["pitch_min"]:
            # For pitch_min, take minimum across chunks (excluding zeros)
            values = [r["voice_features"][feature] for r in chunk_results if r["voice_features"][feature] > 0]
            voice_features[feature] = float(min(values)) if values else 0.0
        elif feature in ["pitch_max"]:
            # For pitch_max, take maximum
            values = [r["voice_features"][feature] for r in chunk_results]
            voice_features[feature] = float(max(values))
        else:
            # For other features, weighted average
            weighted_sum = sum(result["voice_features"][feature] * w 
                             for result, w in zip(chunk_results, weights))
            voice_features[feature] = float(weighted_sum / sum(weights))
    
    # MFCC - average across chunks
    mfcc_aggregated = []
    for i in range(3):  # First 3 MFCC coefficients
        weighted_sum = sum(result["voice_features"]["mfcc"][i] * w 
                          for result, w in zip(chunk_results, weights))
        mfcc_aggregated.append(float(weighted_sum / sum(weights)))
    
    voice_features["mfcc"] = mfcc_aggregated
    
    return {
        "audio_emotion": best_emotion,
        "audio_emotion_probs": {k: round(v, 4) for k, v in aggregated_probs.items()},
        "voice_features": {k: round(v, 4) if k != "mfcc" else [round(x, 2) for x in v] 
                          for k, v in voice_features.items()}
    }

def analyze_audio(filepath, max_duration=300, chunk_duration=30, overlap=2):
    """
    Analyze audio with support for large files
    
    Args:
        filepath: Path to audio file
        max_duration: Maximum duration to process (in seconds). None for no limit.
        chunk_duration: Duration of each processing chunk
        overlap: Overlap between chunks
    """
    
    print(f"Loading audio file: {filepath}")
    
    # Load audio with duration limit if specified
    sr = 16000
    if max_duration:
        speech, sr = librosa.load(filepath, sr=sr, duration=max_duration)
        print(f"Loaded first {max_duration} seconds of audio")
    else:
        speech, sr = librosa.load(filepath, sr=sr)
    
    duration = librosa.get_duration(y=speech, sr=sr)
    print(f"Audio duration: {duration:.2f} seconds")
    
    # Load models once
    print("Loading models...")
    audio_model_name = "superb/wav2vec2-base-superb-er"
    extractor = AutoFeatureExtractor.from_pretrained(audio_model_name)
    audio_model = AutoModelForAudioClassification.from_pretrained(audio_model_name)
    
    models_dict = {
        'extractor': extractor,
        'audio_model': audio_model
    }
    
    # Process audio in chunks if it's long
    if duration > chunk_duration:
        print(f"Processing audio in chunks of {chunk_duration}s with {overlap}s overlap...")
        chunks = chunk_audio(speech, sr, chunk_duration, overlap)
        print(f"Created {len(chunks)} chunks")
        
        chunk_results = []
        for i, chunk in enumerate(chunks):
            print(f"Processing chunk {i+1}/{len(chunks)}")
            try:
                result = analyze_audio_chunk(chunk, sr, models_dict)
                chunk_results.append(result)
            except Exception as e:
                print(f"Error processing chunk {i+1}: {e}")
                continue
            
            # Clear memory
            if i % 5 == 0:  # Every 5 chunks
                gc.collect()
        
        # Aggregate results
        audio_results = aggregate_chunk_results(chunk_results)
        
    else:
        # Process entire audio if it's short enough
        print("Processing entire audio file...")
        audio_results = analyze_audio_chunk(speech, sr, models_dict)
    
    # Add duration to results
    audio_results["voice_features"]["duration"] = round(duration, 2)
    
    # Text processing with chunking for Whisper
    print("Transcribing audio...")
    asr_pipeline = pipeline(
        "automatic-speech-recognition", 
        model="openai/whisper-small",
        chunk_length_s=30,  # Whisper chunking
        stride_length_s=5   # Overlap for Whisper
    )
    
    try:
        # For very long audio, Whisper will automatically chunk
        transcription_result = asr_pipeline(filepath, return_timestamps=True)
        text = transcription_result["text"] if isinstance(transcription_result, dict) else transcription_result
        text = clean_text(text)
        print(f"Transcribed text length: {len(text)} characters")
        
    except Exception as e:
        print(f"Transcription error: {e}")
        text = "Transcription failed"
    
    # Text emotion analysis (handle long text by truncating if needed)
    print("Analyzing text emotion...")
    try:
        # Many text models have token limits, truncate if necessary
        max_text_length = 512  # Adjust based on model limits
        text_for_emotion = text[:max_text_length] if len(text) > max_text_length else text
        
        emo_pipeline = pipeline("text-classification", model="j-hartmann/emotion-english-distilroberta-base", top_k=None)
        emotion_scores = emo_pipeline(text_for_emotion)[0]
        emotion_dict = {e['label']: round(e['score'], 4) for e in emotion_scores}
        
        # Sentiment analysis
        sentiment_pipeline = pipeline("sentiment-analysis", model="cardiffnlp/twitter-roberta-base-sentiment-latest")
        sentiment = sentiment_pipeline(text_for_emotion)[0]
        
    except Exception as e:
        print(f"Text analysis error: {e}")
        emotion_dict = {"error": "Text analysis failed"}
        sentiment = {"label": "UNKNOWN", "confidence": 0.0}
    
    # Clean up
    del models_dict, extractor, audio_model
    gc.collect()
    
    return {
        **audio_results,
        "transcribed_text": text,
        "text_emotion": emotion_dict,
        "sentiment": {
            "label": sentiment["label"],
            "confidence": round(sentiment["score"], 4)
        },
        "processing_info": {
            "original_duration": round(duration, 2),
            "processed_duration": round(min(duration, max_duration) if max_duration else duration, 2),
            "was_chunked": duration > chunk_duration
        }
    }

# Example usage with different configurations
def analyze_large_audio_fast(filepath):
    """Quick analysis - process only first 60 seconds"""
    return analyze_audio(filepath, max_duration=60, chunk_duration=20)

def analyze_large_audio_thorough(filepath):
    """Thorough analysis - process entire file with chunking"""
    return analyze_audio(filepath, max_duration=None, chunk_duration=30)