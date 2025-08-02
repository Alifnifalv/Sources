import pyodbc
import pandas as pd

def create_db_connection():
    return pyodbc.connect(
        'DRIVER={SQL Server};'
        'SERVER=192.168.29.100;'
        'DATABASE=Pearl_2022_Staging;'
        'UID=eduegateuser;'
        'PWD=eduegate@123'
    )

def get_subject_id(subject_name, conn):
    """Get SubjectID for a given SubjectName"""
    cursor = conn.cursor()
    cursor.execute("SELECT SubjectID FROM schools.Subjects WHERE SubjectName = ?", subject_name)
    row = cursor.fetchone()
    if row:
        return row[0]
    return None

def fetch_train_data():
    """Fetch training data using shared connection method"""
    query = """SELECT StudentMarks 
               FROM schools.VWS_JSON_STUDENT_MARKS 
               WHERE AcademicYearCode = 2025"""
    
    with create_db_connection() as conn:
        return pd.read_sql(query, conn)

