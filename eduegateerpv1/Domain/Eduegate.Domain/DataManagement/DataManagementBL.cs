using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Mappers.DataManagement;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.SignUp.SignUps;
using Eduegate.Domain.Setting;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.SignUp.SignUps;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using static Eduegate.Domain.Mappers.DataManagement.DataManagementMapper;

namespace Eduegate.Domain.DataManagement
{
    public class DataManagementBL
    {
        private CallContext Context { get; set; }

        public DataManagementBL(CallContext context)
        {
            Context = context;
        }

        public List<TableInfo> GetAllDataTables()
        {
            var result = Mapper(Context).GetAllDataTables();

            return result;
        }

        public ServerInfo CheckTempDirectory()
        {
            var serverInfo = new ServerInfo();

            string documentsPhysicalPath = new SettingBL().GetSettingValue<string>("DocumentsPhysicalPath");

            string tempFilePath = string.Format(documentsPhysicalPath, "Files", "Temp");

            if (Directory.Exists(tempFilePath))
            {
                // Directory exists
                Console.WriteLine($"Directory exists: {tempFilePath}");

                DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(tempFilePath));
                var driveName = driveInfo.Name;
                var totalSpace = driveInfo.TotalSize;
                var availableSpace = driveInfo.AvailableFreeSpace;

                var size = GetDirectorySize(tempFilePath);
                var tempSize = size;

                serverInfo.TotalSize = Convert.ToString(totalSpace);
                serverInfo.AvailableSize = Convert.ToString(availableSpace);
                serverInfo.TempSize = Convert.ToString(tempSize);
            }

            return serverInfo;

        }

        private long GetDirectorySize(string directoryPath)
        {
            long size = 0;
            string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);

            // Sum up the size of all files
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                size += fileInfo.Length;
            }

            return size;
        }

        private string FormatSize(long size)
        {
            // Format the size to a more readable format (KB, MB, GB, etc.)
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double formattedSize = size;
            int order = 0;
            while (formattedSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                formattedSize = formattedSize / 1024;
            }

            return $"{formattedSize:0.##} {sizes[order]}";
        }

        public OperationResultDTO ArchiveTable(string tableName, DateTime Date)
        {
            var result = Mapper(Context).ArchiveTable(tableName, Date);

            return result;
        }

        public List<DatabaseInfo> DatabaseDetails()
        {
            var result = Mapper(Context).DatabaseDetails();

            return result;
        }

        public OperationResultDTO BackupDB(string databaseName, int typeID)
        {
            var result = Mapper(Context).BackupDB(databaseName, typeID);

            return result;
        }
    }
}
