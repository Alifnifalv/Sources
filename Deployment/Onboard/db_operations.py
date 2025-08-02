import subprocess
from pathlib import Path
from .database import (
    backup_database, 
    restore_database
)

def handle_database_operations(args, dry_run, sql_updates):
    if dry_run:
        print("\nDry run: Database operations preview:")
        print(f" - Would restore database '{args.dbname}' on server '{args.server}'")
        print(f" - Would execute {len(sql_updates)} SQL update queries via sqlcmd:")
        for setting_code, value in sql_updates.items():
            print(f"   UPDATE setting.Settings SET SettingValue = '{value}' WHERE SettingCode = '{setting_code}'")
    else:
        print("\nApplying database changes...")
        try:
            

            # Execute SQL updates using SQLCMD
            if sql_updates:
                print("\nExecuting SQL Update Queries via sqlcmd...")
                update_commands = []
                
                for setting_code, value in sql_updates.items():
                    # Basic sanitization for SQLCMD execution
                    safe_value = str(value).replace("'", "''")
                    safe_code = str(setting_code).replace("'", "''")
                    update_commands.append(
                        f"UPDATE setting.Settings SET SettingValue = '{safe_value}' "
                        f"WHERE SettingCode = '{safe_code}';"
                    )

                # Wrap in transaction for atomic execution
                transaction_sql = (
                    "BEGIN TRY\n"
                    "  BEGIN TRANSACTION\n"
                    f"{chr(10).join(update_commands)}\n"
                    "  COMMIT TRANSACTION\n"
                    "END TRY\n"
                    "BEGIN CATCH\n"
                    "  ROLLBACK TRANSACTION\n"
                    "  THROW;\n"
                    "END CATCH"
                )

                cmd = [
                    "sqlcmd",
                    "-S", args.sql_server,
                    "-d", args.dbname,
                    "-U", args.sql_username,
                    "-P", args.sql_password,
                    "-Q", transaction_sql
                ]

                result = subprocess.run(
                    cmd,
                    capture_output=True,
                    text=True,
                    check=True
                )
                
                print("SQL updates executed successfully")
                print("Output:", result.stdout)
                
        except subprocess.CalledProcessError as e:
            print(f"Database operation failed: {str(e)}")
            print("Error output:", e.stderr)
            raise
        except Exception as e:
            print(f"Operation failed: {str(e)}")
            raise
