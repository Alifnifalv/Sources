# db_query.py
import pyodbc
import os
import re # Import regex module if needed for more complex normalization
from dotenv import load_dotenv

load_dotenv()

# --- Normalization Helper ---
def _normalize_string(input_string):
    """
    Normalizes a string for comparison:
    - Converts to lowercase
    - Replaces ' to ' with '-' to standardize ranges
    - Removes spaces (' ')
    - Removes hyphens ('-') (including those just added)
    - Removes '=>'
    - Trims leading/trailing whitespace (less critical after space removal)
    """
    if not input_string:
        return ""
    normalized = input_string.lower()
    # --- ORDER MATTERS ---
    # 1. Standardize ' to ' before removing spaces/hyphens
    normalized = normalized.replace(' to ', '-') # Note the spaces around 'to'
    # 2. Remove spaces
    normalized = normalized.replace(' ', '')
    # 3. Remove hyphens (original and those from ' to ')
    normalized = normalized.replace('-', '')
    # 4. Remove '=>'
    normalized = normalized.replace('=>', '')
    # Add other symbol removals here if needed
    # normalized = normalized.replace('/', '')

    return normalized.strip() # Strip is good practice but less vital now

# **Important Security Note:**
# NEVER hardcode database credentials! Use environment variables.
# This example uses environment variables for better security.
def get_db_connection():
    """Establishes a connection to the database using environment variables."""
    try:
        server = os.environ.get("DB_SERVER")
        database = os.environ.get("DB_DATABASE")
        username = os.environ.get("DB_USERNAME")
        password = os.environ.get("DB_PASSWORD")
        driver = os.environ.get("DB_DRIVER", "{ODBC Driver 17 for SQL Server}")  # Default driver

        # Check if all required environment variables are set
        if not all([server, database, username, password]):
            raise ValueError("Missing database connection environment variables.")

        conn_str = (
            f"DRIVER={driver};SERVER={server};DATABASE={database};UID={username};PWD={password}"
        )
        conn = pyodbc.connect(conn_str)
        return conn
    except Exception as e:
        print(f"Error connecting to the database: {e}")  # Log the error
        return None


def fetch_menu_names_from_db(search_query, conn):
    """
    Fetches distinct menu names and report types based on a normalized search query,
    using partial matching (LIKE). Handles 'X-Y' and 'X to Y' variations.
    """
    if not conn:
        print("Database connection is not available.")
        return []
    if not search_query or not search_query.strip():
        return [] # Return empty list if search query is empty

    try:
        cursor = conn.cursor()

        # Normalize the search query in Python using the updated helper function
        normalized_search = _normalize_string(search_query)
        if not normalized_search: # Check again after normalization
             return []

        # SQL normalization mirrors the Python helper function, including ORDER.
        # 1. lowercase -> 2. ' to ' to '-' -> 3. remove ' ' -> 4. remove '-' -> 5. remove '=>'
        # IMPORTANT: Ensure the SQL REPLACE functions match _normalize_string logic and order!
        sql_normalize_expr = "REPLACE(REPLACE(REPLACE(REPLACE(LOWER(LTRIM(RTRIM(MenuName))), ' to ', '-'), ' ', ''), '-', ''), '=>', '')"
        # Add more REPLACE calls *inside* the outermost ones if _normalize_string removes more characters.

        query = f"""
            SELECT
                DISTINCT MenuName,
                LEFT(ActionLink, CHARINDEX(',', ActionLink + ',') - 1) AS report_type
            FROM setting.MenuLinks
            WHERE
                -- Normalize MenuName in SQL and use LIKE for partial matching
                {sql_normalize_expr} LIKE ?
                AND ParentMenuID IS NOT NULL
                AND ActionLink IS NOT NULL

            UNION ALL

            SELECT
                DISTINCT MenuName,
                LEFT(ActionLink1, CHARINDEX(',', ActionLink1 + ',') - 1) AS report_type
            FROM setting.MenuLinks
            WHERE
                -- Normalize MenuName in SQL and use LIKE for partial matching
                {sql_normalize_expr} LIKE ?
                AND ParentMenuID IS NOT NULL
                AND ActionLink1 IS NOT NULL

            ORDER BY report_type, MenuName;
        """
        # Use the normalized search term with wildcards for the LIKE comparison
        like_pattern = f"%{normalized_search}%"
        cursor.execute(query, (like_pattern, like_pattern))
        results = cursor.fetchall()
        return results
    except pyodbc.Error as ex:
        sqlstate = ex.args[0]
        print(f"Database error in fetch_menu_names_from_db: {sqlstate} - {ex}")
        return []
    except Exception as e:
        print(f"An unexpected error occurred in fetch_menu_names_from_db: {e}")
        raise


def fetch_report_types_from_db(selected_menu_name, conn):
    """
    Fetches report types and action links for a SPECIFIC menu name,
    using exact matching after normalization. Handles 'X-Y' and 'X to Y' variations.
    Assumes `selected_menu_name` is one of the names returned by `fetch_menu_names_from_db`.
    """
    if not conn:
        print("Database connection is not available.")
        return []
    if not selected_menu_name or not selected_menu_name.strip():
        return [] # Return empty list if menu name is empty

    try:
        cursor = conn.cursor()

        # Normalize the *specific* input menu name using the updated helper function
        normalized_menu_name = _normalize_string(selected_menu_name)
        if not normalized_menu_name: # Check again after normalization
             return []

        # SQL normalization mirrors the Python helper function, including ORDER.
        # 1. lowercase -> 2. ' to ' to '-' -> 3. remove ' ' -> 4. remove '-' -> 5. remove '=>'
        # IMPORTANT: Ensure the SQL REPLACE functions match _normalize_string logic and order!
        sql_normalize_expr = "REPLACE(REPLACE(REPLACE(REPLACE(LOWER(LTRIM(RTRIM(MenuName))), ' to ', '-'), ' ', ''), '-', ''), '=>', '')"
        # Add more REPLACE calls *inside* the outermost ones if _normalize_string removes more characters.

        # This query normalizes the MenuName column in the same way
        # and compares using = for an EXACT match with the normalized input.
        query = f"""
            SELECT
                LEFT(ActionLink, CHARINDEX(',', ActionLink + ',') - 1) AS report_type,
                ActionLink,
                MenuName
            FROM setting.MenuLinks
            WHERE
                -- Normalize MenuName in SQL and use = for exact matching
                {sql_normalize_expr} = ?
                AND ParentMenuID IS NOT NULL
                AND ActionLink IS NOT NULL

            UNION ALL

            SELECT
                LEFT(ActionLink1, CHARINDEX(',', ActionLink1 + ',') - 1) AS report_type,
                ActionLink1 AS ActionLink,
                MenuName
            FROM setting.MenuLinks
            WHERE
                -- Normalize MenuName in SQL and use = for exact matching
                {sql_normalize_expr} = ?
                AND ParentMenuID IS NOT NULL
                AND ActionLink1 IS NOT NULL;
        """

        # Pass the fully normalized menu name *without* wildcards for the exact (=) comparison
        cursor.execute(query, (normalized_menu_name, normalized_menu_name))

        results = cursor.fetchall()
        return results

    except pyodbc.Error as ex:
        sqlstate = ex.args[0]
        print(f"Database error in fetch_report_types_from_db: {sqlstate} - {ex}")
        return []
    except Exception as e:
        print(f"An unexpected error occurred in fetch_report_types_from_db: {e}")
        raise


# --- Example Usage (Illustrating the new change) ---
if __name__ == "__main__":
    conn = get_db_connection()
    if conn:
        try:
            # --- Test Case 1: Search using 'to' ---
            search_to = "progress report 3 to 5"
            print(f"--- Searching for menu names like: '{search_to}' ---")
            # Python _normalize_string("progress report 3 to 5") -> "progressreport35"
            # SQL looks for DB MenuNames where normalized LIKE '%progressreport35%'
            menus_to = fetch_menu_names_from_db(search_to, conn)
            if menus_to:
                print("Found potential matches:")
                for menu, r_type in menus_to:
                    # This should find "Progress Report 3-5" if it exists and normalizes to "progressreport35"
                    print(f"  Menu: '{menu}', Report Type: {r_type}")

                # --- Test Case 2: Fetch details using the exact name found (which might have '-') ---
                specific_menu_to_find = menus_to[0][0] # e.g., "Progress Report 3-5"
                print(f"\n--- Fetching report types for specific menu: '{specific_menu_to_find}' ---")
                # Python _normalize_string("Progress Report 3-5") -> "progressreport35"
                # SQL looks for DB MenuNames where normalized = 'progressreport35'
                reports = fetch_report_types_from_db(specific_menu_to_find, conn)
                if reports:
                    print("Found specific report types:")
                    for r_type, link, m_name in reports:
                        print(f"  Report Type: {r_type}, Link: {link}, Original MenuName: {m_name}")
                else:
                    print(f"  No specific report types found for the exact normalized name of '{specific_menu_to_find}'.")

            else:
                 print(f"  No menu names found matching search '{search_to}'.")


            # --- Test Case 3: Search using '-' ---
            search_hyphen = "progress report 3-5"
            print(f"\n--- Searching for menu names like: '{search_hyphen}' ---")
            # Python _normalize_string("progress report 3-5") -> "progressreport35"
            # SQL looks for DB MenuNames where normalized LIKE '%progressreport35%'
            menus_hyphen = fetch_menu_names_from_db(search_hyphen, conn)
            if menus_hyphen:
                 print("Found potential matches:")
                 for menu, r_type in menus_hyphen:
                    print(f"  Menu: '{menu}', Report Type: {r_type}")
            else:
                 print(f"  No menu names found matching search '{search_hyphen}'.")

        except Exception as e:
            print(f"An error occurred during execution: {e}")
        finally:
            if conn:
                conn.close()
                print("\nDatabase connection closed.")
    else:
        print("Failed to establish database connection.")