import os
from flask import Flask, json, request, jsonify
from flask_cors import CORS
from pymongo import MongoClient
from werkzeug.utils import secure_filename
from core import get_document_text, allowed_file, generate_response_openrouter, transcribe_audio
from PyPDF2 import PdfReader
from rasa.core.agent import Agent
import asyncio
import logging
from logging.handlers import RotatingFileHandler
from rasa.utils.endpoints import EndpointConfig
import nest_asyncio
import time

# Apply nest_asyncio to enable nested event loops
nest_asyncio.apply()

# Set up logging
logger = logging.getLogger(__name__)
logger.setLevel(logging.INFO)

# Create handlers
console_handler = logging.StreamHandler()
console_handler.setLevel(logging.DEBUG)

# Create rotating file handler to prevent log files from growing too large
file_handler = RotatingFileHandler('app.log', maxBytes=10485760, backupCount=5)  # 10MB per file, keep 5 backups
file_handler.setLevel(logging.INFO)  # Only log INFO and above to the file

# Create formatters and add them to handlers
log_format = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
console_handler.setFormatter(log_format)
file_handler.setFormatter(log_format)

# Add handlers to the logger
logger.addHandler(console_handler)
logger.addHandler(file_handler)

# Global variable to store the agent
agent = None

def run_async(coro):
    """Run an async function in a thread-safe way."""
    new_loop = asyncio.new_event_loop()
    try:
        asyncio.set_event_loop(new_loop)
        return new_loop.run_until_complete(coro)
    finally:
        new_loop.close()
        
import atexit

def on_shutdown():
    logger.info(f"Application is shutting down at: {time.ctime()}")
atexit.register(on_shutdown)
logger.info(f"Flask App PID: {os.getpid()}")
# Load the model
try:
    action_endpoint = EndpointConfig(url="http://localhost:5055/webhook")
    model_path = "models/20250322-121019-regional-broadcast.tar.gz"
    logger.info(f"Loading model from: {model_path}")
    
    agent = Agent.load(model_path, action_endpoint=action_endpoint)
    logger.info("Model loaded successfully")
except Exception as e:
    logger.error(f"Failed to load model: {str(e)}")
    raise

app = Flask(__name__)
CORS(app)

# MongoDB connection setup  
client = MongoClient('mongodb://localhost:27017/')
db = client['softop_schools']
collection = db['students']

@app.route('/GetMarks', methods=['GET'])
def get_student_results():
    """Fetch student results from MongoDB."""
    try:
        student_id = request.args.get('studentID')
        if not student_id:
            logger.info("Request failed: Missing studentID parameter")
            return jsonify({"error": "studentID parameter is required"}), 400
        try:
            student_id = int(student_id)
        except ValueError:
            logger.info(f"Request failed: Invalid studentID format - {student_id}")
            return jsonify({"error": "Invalid studentID. It must be an integer."}), 400

        student_result = collection.find_one({"student_id": student_id}, {"_id": 0})
        if not student_result:
            logger.info(f"No records found for studentID: {student_id}")
            return jsonify({"message": "No records found for the given studentID"}), 404

        logger.info(f"Successfully retrieved results for studentID: {student_id}")
        return jsonify(student_result), 200
    except Exception as e:
        logger.error(f"Error in get_student_results: {str(e)}")
        return jsonify({"error": f"Server error: {str(e)}"}), 500

@app.route('/Extract', methods=['POST'])
def extract_details():
    """Extracts details from uploaded files."""
    logger.info("Extract endpoint called")
    
    if 'files' not in request.files:
        logger.info("Extract failed: No files part in request")
        return jsonify({"error": "No files part"}), 400
    
    files = request.files.getlist('files')
    if not files:
        logger.info("Extract failed: No files selected")
        return jsonify({"error": "No files selected"}), 400

    for file in files:
        if file and not allowed_file(file.filename):
            logger.info(f"Extract failed: Invalid file type - {file.filename}")
            return jsonify({"error": f"Invalid file type: {file.filename}"}), 400

    logger.info(f"Processing {len(files)} files for extraction")
    combined_text, error = get_document_text(request.files)
    if error:
        logger.info(f"Extract failed: {error}")
        return jsonify({"error": error}), 400
    if not combined_text:
        logger.info("Extract failed: Could not extract text from any files")
        return jsonify({"error": "Could not extract text from any files."}), 400

    logger.info("Generating response via OpenRouter")
    llm_response, status_code = generate_response_openrouter(combined_text)
    if status_code != 200:
        logger.info(f"OpenRouter response failed with status code: {status_code}")
        return jsonify(llm_response), status_code

    try:
        if isinstance(llm_response, str):
            llm_response = json.loads(llm_response)
        if isinstance(llm_response, dict):
            logger.info("Successfully processed extraction request")
            return jsonify(llm_response), 200
        else:
            logger.info("Invalid response format from LLM")
            return jsonify({"error": "Invalid response format from LLM"}), 500
    except json.JSONDecodeError as e:
        logger.error(f"Failed to parse LLM response: {str(e)}")
        return jsonify({"error": f"Failed to parse LLM response: {str(e)}"}), 500

@app.route('/chat', methods=['POST'])
def chat():
    """Unified endpoint for text and voice chat."""
    logger.info("Chat endpoint called")
    
    extracted_text = None  # Variable to store extracted text

    if 'audio' in request.files:
        audio_file = request.files['audio']
        temp_path = "temp_audio.webm"  # Temporary file path
        logger.info("Processing audio file for transcription")

        try:
            # Save the uploaded file
            audio_file.save(temp_path)

            # Transcribe audio
            extracted_text = transcribe_audio(temp_path)
            logger.info("Audio transcription completed")

            # Remove temp file after processing
            os.remove(temp_path)

            user_message = extracted_text  # Use extracted text as input message
        except Exception as e:
            logger.error(f"Audio processing error: {str(e)}")
            return jsonify({"error": f"Audio processing error: {e}"}), 500
    else:
        user_message = request.json.get("message")
        logger.info("Processing text message")

    if not user_message:
        logger.info("Chat failed: No message provided")
        return jsonify({"error": "No message provided"}), 400

    # Process message (dummy response for now)
    response = run_async(agent.handle_text(user_message))

    # Add extracted_text only if an audio file was processed
    if extracted_text:
        response[0]["extracted_text"] = extracted_text

    logger.info("Successfully processed chat request")
    return jsonify(response)

@app.route('/health', methods=['GET'])
def health_check():
    """Health check endpoint to keep the application alive and verify its status."""
    logger.info("Health check endpoint called")
    
    try:
        # Perform a lightweight check to ensure the RASA agent is loaded
        global agent
        if agent is None:
            logger.error("Health check failed: RASA agent not initialized")
            return jsonify({
                "status": "error",
                "message": "RASA agent not initialized"
            }), 500
        
        # You could add additional checks here if needed
        # For example, verify MongoDB connection
        db_status = "connected" if client.admin.command('ping')['ok'] == 1.0 else "disconnected"
        logger.info(f"Health check passed. Database status: {db_status}")
        
        return jsonify({
            "status": "ok",
            "timestamp": time.time(),
            "uptime": "running",
            "database": db_status
        }), 200
    except Exception as e:
        logger.error(f"Health check failed: {str(e)}")
        return jsonify({
            "status": "error",
            "message": str(e)
        }), 500



if __name__ == '__main__':
    logger.info(f"Application started/restarted at: {time.ctime()}")
    app.run(host='0.0.0.0', port=5000, debug=False, use_reloader=False,threaded=True)
