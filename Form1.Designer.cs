namespace SpotifyDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.trackName = new System.Windows.Forms.Label();
            this.pnlShowErrLog = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbErrorLog = new System.Windows.Forms.TextBox();
            this.lblIndicator = new System.Windows.Forms.Label();
            this.pnlErrLog = new System.Windows.Forms.Panel();
            this.cbResumeRecord = new System.Windows.Forms.CheckBox();
            this.pnlErrLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Marker", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "NOW PLAYING ON SPOTIFY";
            // 
            // trackName
            // 
            this.trackName.BackColor = System.Drawing.SystemColors.WindowText;
            this.trackName.Font = new System.Drawing.Font("Segoe Marker", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackName.ForeColor = System.Drawing.Color.Chartreuse;
            this.trackName.Location = new System.Drawing.Point(10, 32);
            this.trackName.Name = "trackName";
            this.trackName.Size = new System.Drawing.Size(335, 51);
            this.trackName.TabIndex = 3;
            this.trackName.Text = "No Track";
            this.trackName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlShowErrLog
            // 
            this.pnlShowErrLog.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlShowErrLog.Location = new System.Drawing.Point(10, 86);
            this.pnlShowErrLog.Name = "pnlShowErrLog";
            this.pnlShowErrLog.Size = new System.Drawing.Size(335, 6);
            this.pnlShowErrLog.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Error Log:";
            // 
            // tbErrorLog
            // 
            this.tbErrorLog.Location = new System.Drawing.Point(7, 20);
            this.tbErrorLog.Multiline = true;
            this.tbErrorLog.Name = "tbErrorLog";
            this.tbErrorLog.ReadOnly = true;
            this.tbErrorLog.Size = new System.Drawing.Size(335, 70);
            this.tbErrorLog.TabIndex = 6;
            // 
            // lblIndicator
            // 
            this.lblIndicator.ForeColor = System.Drawing.Color.Red;
            this.lblIndicator.Location = new System.Drawing.Point(175, 1);
            this.lblIndicator.Name = "lblIndicator";
            this.lblIndicator.Size = new System.Drawing.Size(174, 31);
            this.lblIndicator.TabIndex = 7;
            this.lblIndicator.Text = "Stopped";
            this.lblIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlErrLog
            // 
            this.pnlErrLog.Controls.Add(this.cbResumeRecord);
            this.pnlErrLog.Controls.Add(this.label2);
            this.pnlErrLog.Controls.Add(this.tbErrorLog);
            this.pnlErrLog.Location = new System.Drawing.Point(3, 101);
            this.pnlErrLog.Name = "pnlErrLog";
            this.pnlErrLog.Size = new System.Drawing.Size(352, 97);
            this.pnlErrLog.TabIndex = 8;
            // 
            // cbResumeRecord
            // 
            this.cbResumeRecord.AutoSize = true;
            this.cbResumeRecord.Location = new System.Drawing.Point(233, 3);
            this.cbResumeRecord.Name = "cbResumeRecord";
            this.cbResumeRecord.Size = new System.Drawing.Size(117, 17);
            this.cbResumeRecord.TabIndex = 7;
            this.cbResumeRecord.Text = "Enable Recording";
            this.cbResumeRecord.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(357, 200);
            this.Controls.Add(this.pnlErrLog);
            this.Controls.Add(this.lblIndicator);
            this.Controls.Add(this.pnlShowErrLog);
            this.Controls.Add(this.trackName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spotify Downloader";
            this.TopMost = true;
            this.pnlErrLog.ResumeLayout(false);
            this.pnlErrLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label trackName;
        private System.Windows.Forms.Panel pnlShowErrLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbErrorLog;
        private System.Windows.Forms.Label lblIndicator;
        private System.Windows.Forms.Panel pnlErrLog;
        private System.Windows.Forms.CheckBox cbResumeRecord;
    }
}

