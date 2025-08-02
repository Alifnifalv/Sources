namespace Eduegate.ERP.Launcher
{
    partial class EduegateInstallerWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EduegateInstallerWizard));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.chkUpdatePortal = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AppPoolName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PortNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.WebSiteName = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnPick = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInstallationPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAuthentication = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.chkFreshDB = new System.Windows.Forms.CheckBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInstance = new System.Windows.Forms.TextBox();
            this.chkConfigureIIS = new System.Windows.Forms.CheckBox();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.lblSubTasks = new System.Windows.Forms.Label();
            this.lblTotalTaskStatus = new System.Windows.Forms.Label();
            this.prBarSub = new System.Windows.Forms.ProgressBar();
            this.prBarMain = new System.Windows.Forms.ProgressBar();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fldBrowser = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.wizardPage1);
            this.wizardControl1.Pages.Add(this.wizardPage2);
            this.wizardControl1.Pages.Add(this.wizardPage3);
            this.wizardControl1.Size = new System.Drawing.Size(598, 538);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "Welcome to Eduegate Suite";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            this.wizardControl1.Finished += new System.EventHandler(this.wizardControl1_Finished);
            this.wizardControl1.SelectedPageChanged += new System.EventHandler(this.wizardControl1_SelectedPageChanged);
            this.wizardControl1.TabIndexChanged += new System.EventHandler(this.wizardControl1_TabIndexChanged);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.chkUpdatePortal);
            this.wizardPage1.Controls.Add(this.groupBox2);
            this.wizardPage1.Controls.Add(this.checkBox2);
            this.wizardPage1.Controls.Add(this.btnPick);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.txtInstallationPath);
            this.wizardPage1.Controls.Add(this.groupBox1);
            this.wizardPage1.Controls.Add(this.chkConfigureIIS);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(551, 384);
            this.wizardPage1.TabIndex = 0;
            this.wizardPage1.Text = "Configuration";
            // 
            // chkUpdatePortal
            // 
            this.chkUpdatePortal.AutoSize = true;
            this.chkUpdatePortal.Checked = true;
            this.chkUpdatePortal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdatePortal.Location = new System.Drawing.Point(146, 306);
            this.chkUpdatePortal.Name = "chkUpdatePortal";
            this.chkUpdatePortal.Size = new System.Drawing.Size(111, 19);
            this.chkUpdatePortal.TabIndex = 14;
            this.chkUpdatePortal.Text = "Upate the portal";
            this.chkUpdatePortal.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.AppPoolName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.PortNumber);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.WebSiteName);
            this.groupBox2.Location = new System.Drawing.Point(24, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 93);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Application pool ";
            // 
            // AppPoolName
            // 
            this.AppPoolName.Location = new System.Drawing.Point(115, 53);
            this.AppPoolName.Name = "AppPoolName";
            this.AppPoolName.Size = new System.Drawing.Size(259, 23);
            this.AppPoolName.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Port";
            // 
            // PortNumber
            // 
            this.PortNumber.Location = new System.Drawing.Point(415, 25);
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.Size = new System.Drawing.Size(53, 23);
            this.PortNumber.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Website Name";
            // 
            // WebSiteName
            // 
            this.WebSiteName.Location = new System.Drawing.Point(115, 24);
            this.WebSiteName.Name = "WebSiteName";
            this.WebSiteName.Size = new System.Drawing.Size(259, 23);
            this.WebSiteName.TabIndex = 8;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(143, 153);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(107, 19);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "Database Setup";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged_1);
            // 
            // btnPick
            // 
            this.btnPick.Location = new System.Drawing.Point(411, 332);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(45, 23);
            this.btnPick.TabIndex = 11;
            this.btnPick.Text = "Pick";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Installation path";
            // 
            // txtInstallationPath
            // 
            this.txtInstallationPath.Location = new System.Drawing.Point(146, 331);
            this.txtInstallationPath.Name = "txtInstallationPath";
            this.txtInstallationPath.Size = new System.Drawing.Size(259, 23);
            this.txtInstallationPath.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAuthentication);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtUserID);
            this.groupBox1.Controls.Add(this.chkFreshDB);
            this.groupBox1.Controls.Add(this.btnTestConnection);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtInstance);
            this.groupBox1.Location = new System.Drawing.Point(31, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 127);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // chkAuthentication
            // 
            this.chkAuthentication.AutoSize = true;
            this.chkAuthentication.Checked = true;
            this.chkAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuthentication.Location = new System.Drawing.Point(117, 48);
            this.chkAuthentication.Name = "chkAuthentication";
            this.chkAuthentication.Size = new System.Drawing.Size(155, 19);
            this.chkAuthentication.TabIndex = 15;
            this.chkAuthentication.Text = "Windows authentication";
            this.chkAuthentication.UseVisualStyleBackColor = true;
            this.chkAuthentication.CheckedChanged += new System.EventHandler(this.chkAuthentication_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(249, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(125, 23);
            this.txtPassword.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "User && Password";
            // 
            // txtUserID
            // 
            this.txtUserID.Enabled = false;
            this.txtUserID.Location = new System.Drawing.Point(115, 70);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(125, 23);
            this.txtUserID.TabIndex = 12;
            // 
            // chkFreshDB
            // 
            this.chkFreshDB.AutoSize = true;
            this.chkFreshDB.Checked = true;
            this.chkFreshDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFreshDB.Location = new System.Drawing.Point(115, 99);
            this.chkFreshDB.Name = "chkFreshDB";
            this.chkFreshDB.Size = new System.Drawing.Size(198, 19);
            this.chkFreshDB.TabIndex = 11;
            this.chkFreshDB.Text = "Fresh database (drop and create)";
            this.chkFreshDB.UseVisualStyleBackColor = true;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(380, 24);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(116, 23);
            this.btnTestConnection.TabIndex = 10;
            this.btnTestConnection.Text = "Test connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Database instance";
            // 
            // txtInstance
            // 
            this.txtInstance.Location = new System.Drawing.Point(115, 24);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.Size = new System.Drawing.Size(259, 23);
            this.txtInstance.TabIndex = 8;
            // 
            // chkConfigureIIS
            // 
            this.chkConfigureIIS.AutoSize = true;
            this.chkConfigureIIS.Location = new System.Drawing.Point(143, 14);
            this.chkConfigureIIS.Name = "chkConfigureIIS";
            this.chkConfigureIIS.Size = new System.Drawing.Size(94, 19);
            this.chkConfigureIIS.TabIndex = 7;
            this.chkConfigureIIS.Text = "Configure IIS";
            this.chkConfigureIIS.UseVisualStyleBackColor = true;
            this.chkConfigureIIS.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.lblSubTasks);
            this.wizardPage2.Controls.Add(this.lblTotalTaskStatus);
            this.wizardPage2.Controls.Add(this.prBarSub);
            this.wizardPage2.Controls.Add(this.prBarMain);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(551, 384);
            this.wizardPage2.TabIndex = 1;
            this.wizardPage2.Text = "Setting up your system";
            // 
            // lblSubTasks
            // 
            this.lblSubTasks.AutoSize = true;
            this.lblSubTasks.Location = new System.Drawing.Point(27, 135);
            this.lblSubTasks.Name = "lblSubTasks";
            this.lblSubTasks.Size = new System.Drawing.Size(76, 15);
            this.lblSubTasks.TabIndex = 3;
            this.lblSubTasks.Text = "Setting up IIS";
            // 
            // lblTotalTaskStatus
            // 
            this.lblTotalTaskStatus.AutoSize = true;
            this.lblTotalTaskStatus.Location = new System.Drawing.Point(27, 60);
            this.lblTotalTaskStatus.Name = "lblTotalTaskStatus";
            this.lblTotalTaskStatus.Size = new System.Drawing.Size(147, 15);
            this.lblTotalTaskStatus.TabIndex = 2;
            this.lblTotalTaskStatus.Text = "Processing 3(1) processing";
            // 
            // prBarSub
            // 
            this.prBarSub.Location = new System.Drawing.Point(25, 155);
            this.prBarSub.Name = "prBarSub";
            this.prBarSub.Size = new System.Drawing.Size(501, 23);
            this.prBarSub.TabIndex = 1;
            // 
            // prBarMain
            // 
            this.prBarMain.Location = new System.Drawing.Point(25, 83);
            this.prBarMain.Name = "prBarMain";
            this.prBarMain.Size = new System.Drawing.Size(501, 23);
            this.prBarMain.TabIndex = 0;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.txtLogs);
            this.wizardPage3.Controls.Add(this.label6);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(551, 384);
            this.wizardPage3.TabIndex = 2;
            this.wizardPage3.Text = "Installation completed";
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(23, 49);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogs.Size = new System.Drawing.Size(508, 317);
            this.txtLogs.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(32, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(226, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Instllation completed!!!";
            // 
            // EduegateInstallerWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 538);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EduegateInstallerWizard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EduegateInstallerWizard_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage2;
        private AeroWizard.WizardPage wizardPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInstallationPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFreshDB;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInstance;
        private System.Windows.Forms.CheckBox chkConfigureIIS;
        private System.Windows.Forms.Button btnPick;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox AppPoolName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PortNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WebSiteName;
        private System.Windows.Forms.Label lblSubTasks;
        private System.Windows.Forms.Label lblTotalTaskStatus;
        private System.Windows.Forms.ProgressBar prBarSub;
        private System.Windows.Forms.ProgressBar prBarMain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkUpdatePortal;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.CheckBox chkAuthentication;
        private System.Windows.Forms.FolderBrowserDialog fldBrowser;
        private System.Windows.Forms.TextBox txtLogs;
    }
}