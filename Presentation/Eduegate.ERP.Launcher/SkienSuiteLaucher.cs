using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eduegate.ERP.Launcher
{
    public partial class EduegateLaucher : Form
    {
        public EduegateLaucher()
        {
            InitializeComponent();

            if(ConfigurationManager.AppSettings["InitialSetup"] != "false")
            {
                Installers.Visible = false;
            }
        }

        private void RunPortal_Click(object sender, EventArgs e)
        {
            string chromeExePath = ConfigurationManager.AppSettings["ChromePath"];

            if (string.IsNullOrEmpty(chromeExePath))
            {
                chromeExePath = InstallerHelper.FindInstalledApplication("chrome.exe", "PROGRAMFILES");

                if(chromeExePath == null)
                {
                    chromeExePath = InstallerHelper.FindInstalledApplication("chrome.exe", "PROGRAMFILES(X86)");
                }

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["ChromePath"].Value = chromeExePath;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }

            string url = ConfigurationManager.AppSettings["PortalUrl"];

            if (!string.IsNullOrEmpty(chromeExePath))
            {
                Process.Start(chromeExePath, url);
            }
            else
            {
                Process.Start(url);
            }
        }



        private void SynchData_Click(object sender, EventArgs e)
        {

        }

        private void UpdatePortal_Click(object sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["EduegateDomainUrl"];
            string portalPhysicalPath = ConfigurationManager.AppSettings["PortalPath"];

            if (System.IO.File.Exists(Path.Combine(portalPhysicalPath, "skienportalupdates.zip")))
            {
                System.IO.File.Delete(Path.Combine(portalPhysicalPath, "skienportalupdates.zip"));
            }

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(url + "/Home/Updates/", Path.Combine(portalPhysicalPath, "skienportalupdates.zip"));
            }

            InstallationProcessHelper.ExtractToDirectory(Path.Combine(portalPhysicalPath, "skienportalupdates.zip"), portalPhysicalPath, true);
        }

       

        private void RestartTheISS_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("World Wide Web Publishing Service");
            if ((sc.Status.Equals(ServiceControllerStatus.Stopped) || sc.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                Console.WriteLine("Starting the service...");
                sc.Start();
            }

            Process.Start("iisreset");
            MessageBox.Show("Restarted you session.");
        }

        private void Installers_Click(object sender, EventArgs e)
        {
            Installers.Visible = false;
            ProgressUpdate.Maximum = 7;
            ProgressUpdate.Step = 1;
            ProgressUpdate.Value = 1;
            System.Windows.Forms.Application.DoEvents();
            InstallationProcessHelper.InstallIIS();
            ProgressUpdate.Value++;
            System.Windows.Forms.Application.DoEvents();
            InstallationProcessHelper.CreateNewWebSite();
            ProgressUpdate.Value++;
            System.Windows.Forms.Application.DoEvents();
            InstallationProcessHelper.ExtractThePortal(ConfigurationManager.AppSettings["PortalLocation"]);
            ProgressUpdate.Value++;
            System.Windows.Forms.Application.DoEvents();
            InstallerHelper.EncryptYourConfig();
            ProgressUpdate.Value++;
            System.Windows.Forms.Application.DoEvents();
            InstallationProcessHelper.RunTheDatabaseScripts("./SQLEXPRESS");
            ProgressUpdate.Value++;
            System.Windows.Forms.Application.DoEvents();
            InstallationProcessHelper.SetTheConfigToProcessed();
            ProgressUpdate.Value++;
            System.Windows.Forms.Application.DoEvents();
            MessageBox.Show("Setup completed.", "Success");
            ProgressUpdate.Value = 0;
        }

        public void IsSQLServerRunning()
        {
            var sc = new ServiceController("MSSQL$SQLEXPRESS");
            if ((sc.Status.Equals(ServiceControllerStatus.Stopped) || sc.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                Console.WriteLine("Starting the service...");
                sc.Start();
            }           
        }

        public void InstallIISUsingPackageManager()
        {
            var command = @"start /w pkgmgr /iu:IIS-WebServerRole;IIS-WebServer;IIS-CommonHttpFeatures;IIS-StaticContent;IIS-DefaultDocument;IIS-DirectoryBrowsing;IIS-HttpErrors;IIS-ApplicationDevelopment;IIS-ISAPIExtensions;IIS-ISAPIFilter;IIS-NetFxExtensibility45;IIS-ASPNET45;IIS-NetFxExtensibility;IIS-ASPNET;IIS-HealthAndDiagnostics;IIS-HttpLogging;IIS-RequestMonitor;IIS-Security;IIS-RequestFiltering;IIS-HttpCompressionStatic;IIS-WebServerManagementTools;IIS-ManagementConsole;WAS-WindowsActivationService;WAS-ProcessModel;WAS-NetFxEnvironment;WAS-ConfigurationAPI";
            var pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            var p = new Process();
            p.StartInfo = pStartInfo;
            p.Start();
            p.WaitForExit();
        }       

        private string ConnectionString
        {
            get
            {
                return "Data Source=./SQLEXPRESS;Initial Catalog=skiendberpmain;Persist Security Info=True;User Id=skienuser;Password=skiendberp@123;";
            }
        }       
    }
}
