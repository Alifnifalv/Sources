using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Utilities.SqlScriptGenerator
{
    public class ScriptGenerator
    {
        public static class GenerateSQL
        {
            // Returns a string containing all the fields in the table
            public static string BuildAllFieldsSQL(DataTable table)
            {
                string sql = "";
                foreach (DataColumn column in table.Columns)
                {
                    if (sql.Length > 0)
                        sql += ", ";
                    sql += column.ColumnName;
                }
                return sql;
            }

            // Returns a SQL INSERT command. Assumes autoincrement is identity (optional)
            public static string BuildInsertSQL(DataTable table)
            {
                StringBuilder sql = new StringBuilder("INSERT INTO " + table.TableName + " (");
                StringBuilder values = new StringBuilder("VALUES (");
                bool bFirst = true;
                bool bIdentity = false;
                string identityType = null;

                foreach (DataColumn column in table.Columns)
                {
                    if (column.AutoIncrement)
                    {
                        bIdentity = true;

                        switch (column.DataType.Name)
                        {
                            case "Int16":
                                identityType = "smallint";
                                break;
                            case "SByte":
                                identityType = "tinyint";
                                break;
                            case "Int64":
                                identityType = "bigint";
                                break;
                            case "Decimal":
                                identityType = "decimal";
                                break;
                            default:
                                identityType = "int";
                                break;
                        }
                    }
                    else
                    {
                        if (bFirst)
                            bFirst = false;
                        else
                        {
                            sql.Append(", ");
                            values.Append(", ");
                        }

                        sql.Append(column.ColumnName);
                        values.Append("@");
                        values.Append(column.ColumnName);
                    }
                }
                sql.Append(") ");
                sql.Append(values.ToString());
                sql.Append(")");

                if (bIdentity)
                {
                    sql.Append("; SELECT CAST(scope_identity() AS ");
                    sql.Append(identityType);
                    sql.Append(")");
                }

                return sql.ToString(); ;
            }

            public static string BuildUpdateSQL(DataTable table)
            {
                StringBuilder sql = new StringBuilder("UPDATE " + table.TableName);
                StringBuilder values = new StringBuilder("SET ");
                bool bFirst = true;
                bool bIdentity = false;
                string identityType = null;

                foreach (DataColumn column in table.Columns)
                {
                    if (column.AutoIncrement)
                    {
                        bIdentity = true;

                        switch (column.DataType.Name)
                        {
                            case "Int16":
                                identityType = "smallint";
                                break;
                            case "SByte":
                                identityType = "tinyint";
                                break;
                            case "Int64":
                                identityType = "bigint";
                                break;
                            case "Decimal":
                                identityType = "decimal";
                                break;
                            default:
                                identityType = "int";
                                break;
                        }
                    }
                    else
                    {
                        if (bFirst)
                        {
                            bFirst = false;
                        }
                        else
                        {
                            sql.Append(", ");
                            values.Append(", ");
                        }

                        sql.Append(column.ColumnName);
                        values.Append("@");
                        values.Append(column.ColumnName);
                    }
                }

                sql.Append(") ");
                sql.Append(values.ToString());
                sql.Append(")");

                if (bIdentity)
                {
                    sql.Append("; SELECT CAST(scope_identity() AS ");
                    sql.Append(identityType);
                    sql.Append(")");
                }

                return sql.ToString(); ;
            }
        }
    }
}
