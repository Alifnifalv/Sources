using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Eduegate.ERP.Launcher
{
    public partial class EduegateInstallerWizard : Form
    {
        public EduegateInstallerWizard()
        {
            InitializeComponent();
            groupBox1.Enabled = checkBox2.Checked;
            groupBox2.Enabled = chkConfigureIIS.Checked;
            txtInstance.Text = ConfigurationManager.AppSettings["SQLInstance"];
            txtInstallationPath.Text = ConfigurationManager.AppSettings["PortalLocation"];

            WebSiteName.Text = ConfigurationManager.AppSettings["WebSiteName"];
            AppPoolName.Text = ConfigurationManager.AppSettings["APPPOOLNAME"];
            PortNumber.Text = ConfigurationManager.AppSettings["PORTNUMBER"];
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox2.Checked;
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox2.Checked;
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            DialogResult result = fldBrowser.ShowDialog();
            txtInstallationPath.Text = fldBrowser.SelectedPath;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = chkConfigureIIS.Checked;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                InstallerHelper.CreateConnection(GetConnectionString());
                MessageBox.Show(this, "Database connection success.");
            }
            catch
            {
                MessageBox.Show(this, "Database connection failed.");
            }
        }

        private string GetConnectionString()
        {
            var cmdString = new StringBuilder();

            if (chkAuthentication.Checked)
            {
                //return windows authentication
                cmdString.Append("Server=");
                cmdString.Append(txtInstance.Text);
                cmdString.Append(";Database=skiendberpmain;Integrated Security=True;");
            }
            else
            {
                cmdString.Append("Data Source=");
                cmdString.Append(txtInstance.Text);
                cmdString.Append(";Initial Catalog=skiendberpmain;Persist Security Info=True;");

                if (!string.IsNullOrEmpty(txtUserID.Text))
                {
                    cmdString.Append("User Id=");
                    cmdString.Append(txtUserID.Text);
                    cmdString.Append(";");
                }

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    cmdString.Append("Password=");
                    cmdString.Append(txtPassword.Text);
                    cmdString.Append(";");
                }
            }

            return cmdString.ToString();
        }

        private void chkAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtUserID.Enabled = !chkAuthentication.Checked;
            txtPassword.Enabled = !chkAuthentication.Checked;
        }

        private void wizardControl1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void wizardControl1_Finished(object sender, EventArgs e)
        {

        }

        private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            switch(wizardControl1.SelectedPage.Name)
            {
                case "wizardPage1":
                    break;
                case "wizardPage2":
                    wizardPage2.AllowNext = false;
                    StartProcess();
                    break;
                case "wizardPage3":
                    wizardPage2.ShowNext = false;
                    break;
            }
        }

        private void StartProcess()
        {
            prBarMain.Maximum = 5;
            prBarMain.Step = 1;
            prBarMain.Value = 1;
            lblTotalTaskStatus.Text = "Processing 5(1) processing";
            lblSubTasks.Text = "Setting up configure webserver.";
            System.Windows.Forms.Application.DoEvents();
            
            if (chkConfigureIIS.Checked)
            {
                InstallationProcessHelper.InstallIIS();
            }

            System.Windows.Forms.Application.DoEvents();
            prBarMain.Value++;
            lblTotalTaskStatus.Text = "Processing 5(2) processing";
            lblSubTasks.Text = "Setting up the portal.";
            System.Windows.Forms.Application.DoEvents();

            if (chkUpdatePortal.Checked)
            {
                InstallationProcessHelper.CreateNewWebSite();
                InstallationProcessHelper.ExtractThePortal(txtInstallationPath.Text);
                InstallerHelper.EncryptYourConfig();
            }

            System.Windows.Forms.Application.DoEvents();
            prBarMain.Value++;
            lblTotalTaskStatus.Text = "Processing 5(3) processing";
            lblSubTasks.Text = "Setting up the database.";
            System.Windows.Forms.Application.DoEvents();

            if (chkFreshDB.Checked)
            {
                txtLogs.Text = txtLogs.Text + InstallationProcessHelper.RunTheDatabaseScripts(txtInstance.Text, txtUserID.Text, txtPassword.Text) + "\n";
            }

            System.Windows.Forms.Application.DoEvents();
            lblTotalTaskStatus.Text = "Processing 5(4) processing";
            lblSubTasks.Text = "Update the configure.";
            prBarMain.Value++;
            System.Windows.Forms.Application.DoEvents();

            InstallationProcessHelper.SetTheConfigToProcessed();

            System.Windows.Forms.Application.DoEvents();
            lblTotalTaskStatus.Text = "Processing 5(5) processing";
            lblSubTasks.Text = "Processing completing.";
            prBarMain.Value++;
            System.Windows.Forms.Application.DoEvents();

            wizardControl1.NextPage(wizardPage3);
        }

        private void EduegateInstallerWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            var pStartInfo = new ProcessStartInfo("Eduegate.ERP.Launcher.exe");
            var p = new Process();

            //pStartInfo.RedirectStandardOutput = true;
            pStartInfo.UseShellExecute = false;
            pStartInfo.CreateNoWindow = true;
            p.StartInfo = pStartInfo;
            p.Start();
            //var stdOut = p.StandardOutput;
            //var output = new StringBuilder();
        }
    }
}
