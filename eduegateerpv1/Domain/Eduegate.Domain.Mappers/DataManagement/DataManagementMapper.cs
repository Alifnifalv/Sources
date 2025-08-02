using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.UserDevice;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace Eduegate.Domain.Mappers.DataManagement
{
    public class DataManagementMapper : DTOEntityDynamicMapper
    {
        public static DataManagementMapper Mapper(CallContext context)
        {
            var mapper = new DataManagementMapper();
            mapper._context = context;
            return mapper;
        }
        string connectionString = Environment.GetEnvironmentVariable("dbEduegateERPContext");

        public class TableInfo
        {
            public string TableName { get; set; }
            public long Size { get; set; } // Size in bytes
            public int IsForArchive { get; set; } // Size in bytes
        }

        public class ServerInfo
        {
            public string TotalSize { get; set; }
            public string AvailableSize { get; set; }
            public string TempSize { get; set; }

        }

        public class DatabaseInfo
        {
            public string DatabaseName { get; set; }
            public int UsedSize { get; set; }

        }

        public List<TableInfo> GetAllDataTables()
        {
            var tableInfos = new List<TableInfo>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
                SELECT * FROM setting.TableInformation
                ORDER BY TotalSpaceKB DESC", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tableInfos.Add(new TableInfo
                    {
                        TableName = reader["TableName"].ToString(),
                        Size = (long)reader["TotalSpaceKB"] / 1024,
                        IsForArchive = (int)reader["IsForArchive"],
                    });
                }
            }

            return tableInfos;
        }

        public OperationResultDTO ArchiveTable(string tableName, DateTime Date)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");

                var tableDetails = dbContext.ArchiveTables.Where(a => a.TableName == tableName).FirstOrDefault();

                var data = ArchiveTableSPC(tableName, Date.ToString(dateFormat, CultureInfo.InvariantCulture), tableDetails);

                return data;
            }
        }

        public OperationResultDTO ArchiveTableSPC(string tableName, string Date, ArchiveTable tableDetails)
        {
            var result = new OperationResultDTO();

            string message = "Table Archieved";

            var tableSplit = tableName.Split('.');
            var dbName = "Backup_2024";

            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            _sBuilder.ConnectTimeout = 30; // Set Timedout
            using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch (Exception ex)
                {
                    result = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message,
                    };
                }
                using (SqlCommand sqlCommand = new SqlCommand("[setting].[SPS_ARCHIVE_TABLE]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@TABLE_SCHEMA", SqlDbType.NVarChar));
                    sqlCommand.Parameters["@TABLE_SCHEMA"].Value = tableSplit[0];

                    sqlCommand.Parameters.Add(new SqlParameter("@TABLE_NAME", SqlDbType.VarChar));
                    sqlCommand.Parameters["@TABLE_NAME"].Value = tableSplit[1];

                    sqlCommand.Parameters.Add(new SqlParameter("@DBName", SqlDbType.VarChar));
                    sqlCommand.Parameters["@DBName"].Value = dbName;

                    sqlCommand.Parameters.Add(new SqlParameter("@ASONDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@ASONDATE"].Value = Date;

                    sqlCommand.Parameters.Add(new SqlParameter("@ParameterDate", SqlDbType.NVarChar));
                    sqlCommand.Parameters["@ParameterDate"].Value = tableDetails.DateParameter;

                    sqlCommand.Parameters.Add(new SqlParameter("@TableIID", SqlDbType.NVarChar));
                    sqlCommand.Parameters["@TableIID"].Value = tableDetails.TableIDParameter;

                }
            }

            result = new OperationResultDTO()
            {
                operationResult = OperationResult.Success,
                Message = message,
            };

            return result;
        }

        public List<DatabaseInfo> DatabaseDetails()
        {
            var tableInfos = new List<DatabaseInfo>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
                SELECT * FROM setting.TableSizeDashboardView
                ORDER BY SizeMB DESC", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tableInfos.Add(new DatabaseInfo
                    {
                        DatabaseName = reader["DatabaseName"].ToString(),
                        UsedSize = (int)reader["SizeMB"],
                    });
                }
            }

            return tableInfos;
        }

        public OperationResultDTO BackupDB(string databaseName, int typeID)
        {
            var result = new OperationResultDTO();
            string message = "Database backup started";
            string status = string.Empty;

            string device_dir;
            string sqlProxy;
            string backupName;
            string date = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            var deviceDirURL = new Domain.Setting.SettingBL(null).GetSettingValue<string>("BACKUP_URL_DEVICE_DIR");
            var deviceDirDisk = new Domain.Setting.SettingBL(null).GetSettingValue<string>("BACKUP_DISK_DEVICE_DIR");
            var sqlAzureProxy = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SQL_AZURE_PROXY");

            if (typeID == 0)
            {
                device_dir = deviceDirDisk;
                sqlProxy = string.Empty;
                backupName = databaseName + "_backup_" + date;
            }
            else if (typeID == 1)
            {
                device_dir = deviceDirURL;
                sqlProxy = sqlAzureProxy;
                backupName = databaseName + "_backup_" + date;
            }
            else
            {
                throw new ArgumentException("Invalid typeID. Must be 0 or 1.", nameof(typeID));
            }

            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString())
            {
                //ConnectTimeout = 30
            };

            _sBuilder.InitialCatalog = "master";

            try
            {
                using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("[SPS_BACKUP_DATABASE]", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@DATABASENAME", SqlDbType.NVarChar, 128) { Value = databaseName });
                        sqlCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int) { Value = typeID });
                        sqlCommand.Parameters.Add(new SqlParameter("@DEVICE_DIR", SqlDbType.NVarChar, 512) { Value = device_dir });
                        sqlCommand.Parameters.Add(new SqlParameter("@SQLPROXY", SqlDbType.NVarChar, 128) { Value = sqlProxy });
                        sqlCommand.Parameters.Add(new SqlParameter("@BACKUPNAME", SqlDbType.NVarChar, 128) { Value = backupName });

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                status = Convert.ToString(reader["STATUS"] ?? string.Empty);
                                message = Convert.ToString(reader["MESSAGE"] ?? string.Empty);

                                result.operationResult = status == "SUCCESS" ? OperationResult.Success : OperationResult.Error;
                                result.Message = message;
                            }
                        }
                    }
                }

                if (status == "SUCCESS")
                {
                    long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;
                    var settings = NotificationSetting.GetEmployeeAppSettings();
                    string title = "Database Backup Completed";
                    string notificationMessage = "Backup for the database " + databaseName + " has been completed and downloaded according to the specified type. Please check!";
                    long toLoginID = (long)_context?.LoginID;

                    PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                }
            }
            catch (Exception ex)
            {
                result.operationResult = OperationResult.Error;
                result.Message = ex.Message;
            }

            return result;
        }

    }
}