import pandas as pd
import numpy as np
import pyodbc
from sklearn.ensemble import RandomForestRegressor
from sklearn.metrics import mean_squared_error, r2_score
import json
from io import StringIO
import pickle
from query import fetch_data_from_sql



def prepare_student_data(students_data):
    """Convert student data into features and targets for ML model"""
    features = []
    targets = []
    
    for _, row in students_data.iterrows():
        # Parse JSON string to Python object
        student_data = json.loads(row['StudentMarks'])
        
        # Handle case where student_data is a list
        if isinstance(student_data, list):
            student_records = student_data
        else:
            student_records = [student_data]
        
        # Process each student record
        for student_dict in student_records:
            subjects = student_dict.get('SubjectNames', [])
            term1_data = student_dict.get('Term1', {})
            term2_data = student_dict.get('Term2', {})
            
            for subject in subjects:
                if isinstance(subject, list):
                    subject = " ".join(subject)
                    
                # Features: Term1 score, Term2 score, subject name
                term1 = float(term1_data.get(subject, 0))
                term2 = float(term2_data.get(subject, 0))
                
                # Calculate additional features
                score_diff = term2 - term1
                avg_score = (term1 + term2) / 2
                subject_encoding = hash(subject) % 100
                
                features.append([term1, term2, score_diff, avg_score, subject_encoding])
                
                # Target: Term3 score (weighted average of Term1 and Term2 for training)
                target = term2 * 0.7 + term1 * 0.3 + (score_diff * 0.2)
                targets.append(min(100, target))
    
    return np.array(features), np.array(targets)

def train_prediction_model(features, targets):
    raw_data = fetch_data_from_sql()

    """Train a Random Forest model"""
    model = RandomForestRegressor(
        n_estimators=100,
        max_depth=10,
        random_state=42
    )
    model.fit(features, targets)
    with open('test_model.pkl', 'wb') as f:
        pickle.dump(model, f)
    
    # Save the raw data for future predictions
    raw_data.to_pickle('test_stud.pkl')
    return model

def predict_term3(model, student_dict):
    """Predict Term3 scores for a student"""
    predictions = {}
    
    subjects = student_dict.get('SubjectNames', [])
    term1_data = student_dict.get('Term1', {})
    term2_data = student_dict.get('Term2', {})
    
    for subject in subjects:
        if isinstance(subject, list):
            subject = " ".join(subject)
            
        term1 = float(term1_data.get(subject, 0))
        term2 = float(term2_data.get(subject, 0))
        score_diff = term2 - term1
        avg_score = (term1 + term2) / 2
        subject_encoding = hash(subject) % 100
        
        features = np.array([[term1, term2, score_diff, avg_score, subject_encoding]])
        pred = model.predict(features)[0]
        predictions[subject] = round(min(100, pred), 2)
    
    return predictions

def evaluate_model(model, features, targets):
    """Evaluate the model using R-squared and Mean Squared Error"""
    predictions = model.predict(features)
    mse = mean_squared_error(targets, predictions)
    r2 = r2_score(targets, predictions)
    return mse, r2

def main():
    # Fetch raw data from SQL
    raw_data = fetch_data_from_sql()
    print(raw_data.head())
    
    # Prepare features and train model
    features, targets = prepare_student_data(raw_data)
    model = train_prediction_model(features, targets)

    def get_student_predictions(student_id):
        """Get predictions for the student based on their ID"""
        # Loop through all rows to check for the student ID in the JSON
        for index, row in raw_data.iterrows():
            student_list = json.loads(row['StudentMarks'])
            
            for student_data in student_list:
                if student_data.get('StudentIID') == student_id:
                    predictions = predict_term3(model, student_data)
                    
                    print(f"\nPredictions for Student {student_id}:")
                    print("\nSubject-wise comparison:")
                    print("-" * 60)
                    print(f"{'Subject':<20} {'Term 1':<10} {'Term 2':<10} {'Predicted Term 3':<15}")
                    print("-" * 60)
                    
                    subjects = student_data.get('SubjectNames', [])
                    term1_data = student_data.get('Term1', {})
                    term2_data = student_data.get('Term2', {})
                    
                    for subject in subjects:
                        if isinstance(subject, list):
                            subject = " ".join(subject)
                        print(f"{subject:<20} {term1_data.get(subject, 0):<10} "
                            f"{term2_data.get(subject, 0):<10} "
                            f"{predictions[subject]:<15}")
                    return
        
        print(f"Student with ID {student_id} not found!")


    # Get predictions for a specific student ID
    get_student_predictions(9)

    # Evaluate the trained model
    mse, r2 = evaluate_model(model, features, targets)
    print(f"\nModel Evaluation:")
    print(f"Mean Squared Error (MSE): {mse:.2f}")
    print(f"R-squared (R²) Score: {r2:.2f}")

if __name__ == "__main__":
    main()