# app.py

from flask import Flask, request, jsonify
from core import analyze_audio
import os

app = Flask(__name__)

@app.route('/analyze', methods=['POST'])
def analyze():
    if 'audio' not in request.files:
        return jsonify({"error": "No audio file provided"}), 400
    
    file = request.files['audio']
    filepath = os.path.join("uploads", file.filename)
    os.makedirs("uploads", exist_ok=True)
    file.save(filepath)

    result = analyze_audio(filepath)
    return jsonify(result)

if __name__ == '__main__':
    app.run(debug=True)
