using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Frameworks
{
    public class ErrorBL
    {
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public ErrorBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public async Task<string> Recent(int count)
        {
            string connectionString = Environment.GetEnvironmentVariable("EduegatedERP_LoggerContext");

            if (string.IsNullOrEmpty(connectionString))
            {
                var connection = ConfigurationManager.ConnectionStrings["EduegatedERP_LoggerContext"];
                connectionString = connection?.ConnectionString;
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"No error logs configured to read");
            }

            try
            {
                var tableHtml = new StringBuilder();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(connectionString);
                    cmd.Connection.Open();
                    cmd.CommandText = $"select top {count} ExceptionIID,Message,SerializedException,exType.TypeName,CreatedDate " +
                        $" from [errors].[Exceptions] ex inner join [errors].[ExceptionTypes] exType on exType.ExceptionTypeID = ex.ExceptionTypeID " +
                        $" WHERE Message NOT LIKE '%Globalization%' ORDER BY ExceptionIID DESC";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        tableHtml.AppendLine("<div style='overflow-x: auto;'>");  // Add a container div with horizontal scroll
                        tableHtml.AppendLine("<table style='border-colla" +
                            "" +
                            "pse: collapse; width: 100%; white-space: nowrap;'>");
                        tableHtml.AppendLine("<tr>");
                        tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>ExceptionID</th>");
                        tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Message</th>");
                        tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Type</th>");
                        tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Created on</th>");
                        tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Call stack</th>");
                        // Add more table headers based on your result set

                        tableHtml.AppendLine("</tr>");

                        while (reader.Read())
                        {
                            tableHtml.AppendLine("<tr>");
                            tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["ExceptionIID"]}</td>");
                            tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["Message"]}</td>");
                            tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["TypeName"]}</td>");
                            tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["CreatedDate"]}</td>");
                            tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; white-space: normal;'>{reader["SerializedException"]}</td>");
                            // Add more table cells based on your result set

                            tableHtml.AppendLine("</tr>");
                        }

                        tableHtml.AppendLine("</table>");
                        tableHtml.AppendLine("</div>");
                    }
                }

                return tableHtml.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve or execute the error logs. Exception: {ex.Message}");
            }
        }

        public async Task<string> Recent(int count, string type, string appId)
        {
            string connectionString = Environment.GetEnvironmentVariable("EduegatedERP_LoggerContext");

            if (string.IsNullOrEmpty(connectionString))
            {
                var connection = ConfigurationManager.ConnectionStrings["EduegatedERP_LoggerContext"];
                connectionString = connection?.ConnectionString;
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"No error logs configured to read");
            }

            try
            {
                var tableHtml = new StringBuilder();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(connectionString);
                    cmd.Connection.Open();
                    cmd.CommandText = appId == null ? $"select top {count} ExceptionIID,Message,SerializedException,exType.TypeName,CreatedDate,AppId " +
                        $" from [errors].[Exceptions] ex inner join [errors].[ExceptionTypes] exType on exType.ExceptionTypeID = ex.ExceptionTypeID " +
                        $" WHERE Message NOT LIKE '%Globalization%' " + 
                        $" order by ExceptionIID desc" : 
                        $"select top {count} ExceptionIID,Message,SerializedException,exType.TypeName,CreatedDate,AppId " +
                        $" from [errors].[Exceptions] ex inner join [errors].[ExceptionTypes] exType on exType.ExceptionTypeID = ex.ExceptionTypeID " +
                        $" where AppId = '{appId}' AND WHERE Message NOT LIKE '%Globalization%' order by ExceptionIID desc";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (type != null && type == "json")
                        {
                            var exceptionList = new List<Dictionary<string, object>>();

                            while (reader.Read())
                            {
                                var exceptionData = new Dictionary<string, object>
                                {
                                    { "ExceptionID", reader["ExceptionIID"] },
                                    { "Message", reader["Message"] },
                                    { "Type", reader["TypeName"] },
                                    { "CreatedOn", reader["CreatedDate"] },
                                    { "CallStack", reader["SerializedException"] },
                                    { "AppId", reader["AppId"] }
                                    // Add more fields as needed
                                };

                                exceptionList.Add(exceptionData);
                            }

                            return JsonConvert.SerializeObject(exceptionList);
                        }
                        else
                        {
                            tableHtml.AppendLine("<div style='overflow-x: auto;'>");  // Add a container div with horizontal scroll
                            tableHtml.AppendLine("<table style='border-collapse: collapse; width: 100%; white-space: nowrap;'>");
                            tableHtml.AppendLine("<tr>");
                            tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>ExceptionID</th>");
                            tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Message</th>");
                            tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Type</th>");
                            tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Created on</th>");
                            tableHtml.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left; background-color: #f2f2f2;'>Call stack</th>");
                            // Add more table headers based on your result set

                            tableHtml.AppendLine("</tr>");

                            while (reader.Read())
                            {
                                tableHtml.AppendLine("<tr>");
                                tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["ExceptionIID"]}</td>");
                                tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["Message"]}</td>");
                                tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["TypeName"]}</td>");
                                tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{reader["CreatedDate"]}</td>");
                                tableHtml.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; white-space: normal;'>{reader["SerializedException"]}</td>");
                                // Add more table cells based on your result set

                                tableHtml.AppendLine("</tr>");
                            }

                            tableHtml.AppendLine("</table>");
                            tableHtml.AppendLine("</div>");
                        }
                    }
                }

                return tableHtml.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve or execute the error logs. Exception: {ex.Message}");
            }
        }
    }
}
