using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.ERP.Launcher
{
    public class InstallationProcessHelper
    {
        public static void InstallIIS()
        {
            var features = new List<string>() {"/FeatureName:IIS-ApplicationDevelopment",
                                        "/FeatureName:IIS-ASP",
                                        "/FeatureName:IIS-ASPNET",
                                        "/FeatureName:IIS-ASPNET47",
                                        "/FeatureName:IIS-ASPNET45",
                                        "/FeatureName:IIS-WebSockets",
                                        "/FeatureName:IIS-BasicAuthentication",
                                        "/FeatureName:IIS-CGI",
                                        "/FeatureName:IIS-CommonHttpFeatures",
                                        "/FeatureName:IIS-CustomLogging",
                                        "/FeatureName:IIS-DefaultDocument",
                                        "/FeatureName:IIS-DigestAuthentication",
                                        "/FeatureName:IIS-DirectoryBrowsing",
                                        //"/FeatureName:IIS-FTPExtensibility",
                                        //"/FeatureName:IIS-FTPServer",
                                        //"/FeatureName:IIS-FTPSvc",
                                        "/FeatureName:IIS-HealthAndDiagnostics",
                                        "/FeatureName:IIS-HostableWebCore",
                                        "/FeatureName:IIS-HttpCompressionDynamic",
                                        "/FeatureName:IIS-HttpCompressionStatic",
                                        "/FeatureName:IIS-HttpErrors",
                                        "/FeatureName:IIS-HttpLogging",
                                        "/FeatureName:IIS-HttpRedirect",
                                        "/FeatureName:IIS-HttpTracing",
                                        "/FeatureName:IIS-IIS6ManagementCompatibility",
                                        "/FeatureName:IIS-IISCertificateMappingAuthentication",
                                        "/FeatureName:IIS-IPSecurity",
                                        "/FeatureName:IIS-ISAPIExtensions",
                                        "/FeatureName:IIS-ISAPIFilter",
                                        "/FeatureName:IIS-LegacyScripts",
                                        "/FeatureName:IIS-LegacySnapIn",
                                        "/FeatureName:IIS-LoggingLibraries",
                                        "/FeatureName:IIS-ManagementConsole",
                                        "/FeatureName:IIS-ManagementScriptingTools",
                                        "/FeatureName:IIS-ManagementService",
                                        "/FeatureName:IIS-Metabase",
                                        "/FeatureName:IIS-NetFxExtensibility",
                                        "/FeatureName:IIS-ODBCLogging",
                                        "/FeatureName:IIS-Performance",
                                        "/FeatureName:IIS-RequestFiltering",
                                        "/FeatureName:IIS-RequestMonitor",
                                        "/FeatureName:IIS-Security",
                                        "/FeatureName:IIS-ServerSideIncludes",
                                        "/FeatureName:IIS-StaticContent",
                                        "/FeatureName:IIS-URLAuthorization",
                                        "/FeatureName:IIS-WebDAV",
                                        "/FeatureName:IIS-WebServer",
                                        "/FeatureName:IIS-WebServerManagementTools",
                                        "/FeatureName:IIS-WebServerRole",
                                        "/FeatureName:IIS-WindowsAuthentication",
                                        "/FeatureName:IIS-WMICompatibility",
                                        "/FeatureName:WAS-ConfigurationAPI",
                                        "/FeatureName:WAS-NetFxEnvironment",
                                        "/FeatureName:WAS-ProcessModel",
                                        "/FeatureName:WAS-WindowsActivationService" };

            foreach (var feature in features)
            {
                var command = @"START /WAIT DISM /Online /Enable-Feature " + feature;
                var pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
                var p = new Process();
                p.StartInfo = pStartInfo;
                p.Start();
                p.WaitForExit();
            }
        }

        public static void CreateNewWebSite()
        {
            var iisManager = new ServerManager();

            if (!System.IO.Directory.Exists(Path.Combine(ConfigurationManager.AppSettings["PortalLocation"])))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(ConfigurationManager.AppSettings["PortalLocation"]));
            }

            try
            {
                var webSiteName = ConfigurationManager.AppSettings["WebSiteName"];
                iisManager.Sites.Add(webSiteName, "http", "*:8080:", ConfigurationManager.AppSettings["PortalLocation"]);
                iisManager.ApplicationPools.Add(webSiteName);
                iisManager.Sites[webSiteName].Applications[0].ApplicationPoolName = webSiteName;
                ApplicationPool apppool = iisManager.ApplicationPools[webSiteName];
                apppool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
                iisManager.CommitChanges();
            }
            catch
            {

            }
        }

        public static void ExtractThePortal(string installationPath)
        {
            var fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Setup.portal.zip");
            ExtractToDirectory(fileName, installationPath, true);
            System.IO.File.Delete(fileName);
        }

        public static void ExtractToDirectory(string destinationDirectoryName, string physicalPath, bool overwrite)
        {
            using (ZipArchive archive = ZipFile.OpenRead(destinationDirectoryName))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (System.IO.File.Exists(Path.Combine(physicalPath, entry.FullName)))
                    {
                        System.IO.File.Delete(Path.Combine(physicalPath, entry.FullName));
                    }

                    if (!System.IO.Directory.Exists(Path.GetDirectoryName(Path.GetFullPath(Path.Combine(physicalPath, entry.FullName)))))
                    {
                        System.IO.Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(Path.Combine(physicalPath, entry.FullName))));
                    }

                    //if it's a directory it will be already created..
                    if (!string.IsNullOrEmpty(Path.GetFileName(entry.FullName)))
                    {
                        entry.ExtractToFile(Path.Combine(physicalPath, entry.FullName));
                    }
                }
            }
        }

        public static string RunTheDatabaseScripts(string serverName, string userid = "", string password = "")
        {
            var dataExcutionLogs = new StringBuilder();

            try
            {
                if (!System.IO.Directory.Exists("C:\\BAAS\\DB\\"))
                {
                    System.IO.Directory.CreateDirectory("C:\\BAAS\\DB\\");
                }

                var fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.InitialSettings.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-schema-scripts-schema.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-schema-scripts-tables.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-schema-scripts-view.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-schema-scripts-proc.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-scripts.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-view-scripts.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-scripts-viewcolumns.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-scripts-filtercolumns.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-screenmetadata-scripts.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-scripts-settings.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-scripts-menulinks.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-all-data-scripts-claims.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));

                fileName = InstallerHelper.WriteResourceToFile("Eduegate.ERP.Launcher.Scripts.eduegate-user-data-scripts.sql");
                dataExcutionLogs.AppendLine(ExecuteScript(fileName, serverName, userid, password));
                return dataExcutionLogs.ToString();
            }
            catch (SqlException ex)
            {
                InstallerHelper.ShowSqlError(ex);
                return dataExcutionLogs.ToString();
            }
        }

        public static string ExecuteScript(string fileName, string serverName, string userid = "", string password = "")
        {
            var cmdString = new StringBuilder();
            cmdString.Append("sqlcmd -S ");
            cmdString.Append(serverName);

            if(!string.IsNullOrEmpty(userid))
            {
                cmdString.Append(" -U ");
                cmdString.Append(userid);
            }

            if (!string.IsNullOrEmpty(password))
            {
                cmdString.Append(" -P ");
                cmdString.Append(password);
            }

            cmdString.Append(" -i ");
            cmdString.Append(fileName);

            var pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + cmdString.ToString());
            var p = new Process();

            //pStartInfo.RedirectStandardOutput = true;
            pStartInfo.UseShellExecute = false;
            pStartInfo.CreateNoWindow = true;
            p.StartInfo = pStartInfo;
            p.Start();
            //var stdOut = p.StandardOutput;
            //var output = new StringBuilder();
            p.WaitForExit();

            //while (!stdOut.EndOfStream)
            //    output.AppendLine(stdOut.ReadLine());

            System.IO.File.Delete(fileName);
            return "Processed " + fileName + ".";
        }

        public static void SetTheConfigToProcessed()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["InitialSetup"].Value = "true";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
