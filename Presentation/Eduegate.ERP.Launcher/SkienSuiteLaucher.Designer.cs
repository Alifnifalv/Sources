namespace Eduegate.ERP.Launcher
{
    partial class EduegateLaucher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EduegateLaucher));
            this.RunPortal = new System.Windows.Forms.Button();
            this.UpdatePortal = new System.Windows.Forms.Button();
            this.SynchData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RestartTheISS = new System.Windows.Forms.Button();
            this.Installers = new System.Windows.Forms.Button();
            this.ProgressUpdate = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // RunPortal
            // 
            this.RunPortal.Location = new System.Drawing.Point(12, 58);
            this.RunPortal.Name = "RunPortal";
            this.RunPortal.Size = new System.Drawing.Size(117, 35);
            this.RunPortal.TabIndex = 0;
            this.RunPortal.Text = "Run ERP Portal";
            this.RunPortal.UseVisualStyleBackColor = true;
            this.RunPortal.Click += new System.EventHandler(this.RunPortal_Click);
            // 
            // UpdatePortal
            // 
            this.UpdatePortal.Location = new System.Drawing.Point(261, 58);
            this.UpdatePortal.Name = "UpdatePortal";
            this.UpdatePortal.Size = new System.Drawing.Size(117, 35);
            this.UpdatePortal.TabIndex = 1;
            this.UpdatePortal.Text = "Upate the Portal";
            this.UpdatePortal.UseVisualStyleBackColor = true;
            this.UpdatePortal.Click += new System.EventHandler(this.UpdatePortal_Click);
            // 
            // SynchData
            // 
            this.SynchData.Location = new System.Drawing.Point(138, 58);
            this.SynchData.Name = "SynchData";
            this.SynchData.Size = new System.Drawing.Size(117, 35);
            this.SynchData.TabIndex = 2;
            this.SynchData.Text = "Update your Data";
            this.SynchData.UseVisualStyleBackColor = true;
            this.SynchData.Click += new System.EventHandler(this.SynchData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(107, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Eduegate Suite";
            // 
            // RestartTheISS
            // 
            this.RestartTheISS.Location = new System.Drawing.Point(138, 103);
            this.RestartTheISS.Name = "RestartTheISS";
            this.RestartTheISS.Size = new System.Drawing.Size(117, 35);
            this.RestartTheISS.TabIndex = 4;
            this.RestartTheISS.Text = "Restart Services";
            this.RestartTheISS.UseVisualStyleBackColor = true;
            this.RestartTheISS.Click += new System.EventHandler(this.RestartTheISS_Click);
            // 
            // Installers
            // 
            this.Installers.Location = new System.Drawing.Point(275, 2);
            this.Installers.Name = "Installers";
            this.Installers.Size = new System.Drawing.Size(117, 35);
            this.Installers.TabIndex = 5;
            this.Installers.Text = "Setup your system";
            this.Installers.UseVisualStyleBackColor = true;
            this.Installers.Visible = false;
            this.Installers.Click += new System.EventHandler(this.Installers_Click);
            // 
            // ProgressUpdate
            // 
            this.ProgressUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressUpdate.ForeColor = System.Drawing.Color.Blue;
            this.ProgressUpdate.Location = new System.Drawing.Point(-1, 152);
            this.ProgressUpdate.Name = "ProgressUpdate";
            this.ProgressUpdate.Size = new System.Drawing.Size(405, 23);
            this.ProgressUpdate.TabIndex = 6;
            // 
            // EduegateLaucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(20)))), ((int)(((byte)(112)))));
            this.ClientSize = new System.Drawing.Size(402, 175);
            this.Controls.Add(this.ProgressUpdate);
            this.Controls.Add(this.Installers);
            this.Controls.Add(this.RestartTheISS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SynchData);
            this.Controls.Add(this.UpdatePortal);
            this.Controls.Add(this.RunPortal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EduegateLaucher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BAAS - Eduegate Suite - Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RunPortal;
        private System.Windows.Forms.Button UpdatePortal;
        private System.Windows.Forms.Button SynchData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RestartTheISS;
        private System.Windows.Forms.Button Installers;
        private System.Windows.Forms.ProgressBar ProgressUpdate;
    }
}

