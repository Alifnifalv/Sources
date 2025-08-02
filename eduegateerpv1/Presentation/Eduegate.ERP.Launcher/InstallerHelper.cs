using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eduegate.ERP.Launcher
{
    public enum CommentType
    {
        Message
    }

    public class InstallerHelper
    {
        private const string Comment = "--";
        private const char CommandKey = '!';

        public static void ProcessFile(string file, SqlConnection connection)
        {
            using (SqlTransaction trans = connection.BeginTransaction("_DATA_"))
            {
                string line = null;
                StringBuilder builder = new StringBuilder();
                using (StreamReader reader = new StreamReader(file))
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();

                        if (line.Length == 0)
                            continue;

                        if (line.StartsWith(Comment))
                        {
                            if (line.Length < 4)
                                continue;

                            if (line[2] != CommandKey)
                                continue;

                            int endIndex = line.IndexOf(' ');

                            if (endIndex < 0)
                                continue;

                            int fullLength = Comment.Length + 1;

                            string commentTypeString = line.Substring(fullLength, endIndex - fullLength);

                            if (!Enum.IsDefined(typeof(CommentType), commentTypeString))
                                continue;

                            CommentType type = (CommentType)Enum.Parse(typeof(CommentType), commentTypeString);

                            switch (type)
                            {
                                case CommentType.Message:
                                    ShowStatusMessage(line.Substring(endIndex));
                                    continue;
                            }
                        }

                        Application.DoEvents();

                        if (String.Compare(line, "GO", true) == 0)
                        {
                            using (SqlCommand command = new SqlCommand(builder.ToString(), connection, trans))
                                command.ExecuteNonQuery();
                            builder.Length = 0;
                            continue;
                        }

                        builder.AppendLine(line);
                    }

                if (builder.Length > 0)
                    using (SqlCommand command = new SqlCommand(builder.ToString(), connection, trans))
                        command.ExecuteNonQuery();

                trans.Commit();
            }
        }

        private static void ShowStatusMessage(string text)
        {
            //txtLog.AppendText(text + "\r\n");
            //txtLog.Select(txtLog.Text.Length - 1, 0);
        }

        public static string GetFromResources(string resourceName)
        {
            Assembly assem = Assembly.GetExecutingAssembly();

            using (Stream stream = assem.GetManifestResourceStream(assem.GetName().Name + '.' + resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string WriteResourceToFile(string resourceName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(resourceName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                    return resourceName;
                }
            }
        }

        public static string FindInstalledApplication(string application, string path)
        {
            DirectoryInfo programFiles = new DirectoryInfo(Environment.GetEnvironmentVariable(path));//Find your Programs folder
            DirectoryInfo[] dirs = programFiles.GetDirectories();
            List<FileInfo> files = new List<FileInfo>();
            Parallel.ForEach(dirs, (dir) =>
            {
                try
                {
                    files.AddRange(dir.GetFiles(application, SearchOption.AllDirectories)); //Search for Chrome.exe
                }
                catch
                {

                }
            });
            //files should only contain 1 entry
            //Return path of chrom.exe or null
            return (files.Count > 0) ? files[0].FullName : null;
        }

        public static void EncryptYourConfig()
        {
            var path = FindInstalledApplication("ASPNET_REGIIS.exe", "windir");
            var command = path + @" -PEF ""connectionStrings"" """
                + ConfigurationManager.AppSettings["PortalLocation"].ToString() + "\"";
            var pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            var p = new Process();
            p.StartInfo = pStartInfo;
            p.Start();
            p.WaitForExit();
        }

        public static SqlConnection CreateConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static void ShowSqlError(SqlException ex)
        {
            MessageBox.Show(ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
    }
}
