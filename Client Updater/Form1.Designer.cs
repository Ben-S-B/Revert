namespace Client_Updater
{
    partial class Form1
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
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxDownloadClient = new MetroFramework.Controls.MetroCheckBox();
            this.textBoxDomain = new System.Windows.Forms.TextBox();
            this.labelDomain = new System.Windows.Forms.Label();
            this.buttonUpdateAndMod = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(11, 138);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(88, 13);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Status: Waiting...";
            // 
            // checkBoxDownloadClient
            // 
            this.checkBoxDownloadClient.AutoSize = true;
            this.checkBoxDownloadClient.Checked = true;
            this.checkBoxDownloadClient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDownloadClient.Location = new System.Drawing.Point(14, 120);
            this.checkBoxDownloadClient.Name = "checkBoxDownloadClient";
            this.checkBoxDownloadClient.Size = new System.Drawing.Size(136, 15);
            this.checkBoxDownloadClient.TabIndex = 4;
            this.checkBoxDownloadClient.Text = "Download new Client";
            this.checkBoxDownloadClient.UseSelectable = true;
            // 
            // textBoxDomain
            // 
            this.textBoxDomain.Location = new System.Drawing.Point(63, 57);
            this.textBoxDomain.Name = "textBoxDomain";
            this.textBoxDomain.Size = new System.Drawing.Size(213, 20);
            this.textBoxDomain.TabIndex = 0;
            this.textBoxDomain.Text = "c453.pw";
            // 
            // labelDomain
            // 
            this.labelDomain.AutoSize = true;
            this.labelDomain.Location = new System.Drawing.Point(11, 60);
            this.labelDomain.Name = "labelDomain";
            this.labelDomain.Size = new System.Drawing.Size(46, 13);
            this.labelDomain.TabIndex = 6;
            this.labelDomain.Text = "Domain:";
            // 
            // buttonUpdateAndMod
            // 
            this.buttonUpdateAndMod.Location = new System.Drawing.Point(14, 83);
            this.buttonUpdateAndMod.Name = "buttonUpdateAndMod";
            this.buttonUpdateAndMod.Size = new System.Drawing.Size(262, 23);
            this.buttonUpdateAndMod.TabIndex = 1;
            this.buttonUpdateAndMod.Text = "Update && Mod Client";
            this.buttonUpdateAndMod.UseSelectable = true;
            this.buttonUpdateAndMod.Click += new System.EventHandler(this.c453_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 161);
            this.Controls.Add(this.labelDomain);
            this.Controls.Add(this.textBoxDomain);
            this.Controls.Add(this.checkBoxDownloadClient);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonUpdateAndMod);
            this.Name = "Form1";
            this.Resizable = false;
            this.Text = "Client Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelStatus;
        private MetroFramework.Controls.MetroCheckBox checkBoxDownloadClient;
        private System.Windows.Forms.TextBox textBoxDomain;
        private System.Windows.Forms.Label labelDomain;
        private MetroFramework.Controls.MetroButton buttonUpdateAndMod;
    }
}

