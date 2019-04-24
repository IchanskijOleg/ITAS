namespace ITAS
{
    partial class FmReport
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.bt_report = new System.Windows.Forms.Button();
            this.tb_folder = new System.Windows.Forms.TextBox();
            this.bt_OpenFolder = new System.Windows.Forms.Button();
            this.chb_504 = new System.Windows.Forms.CheckBox();
            this.chb_503 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dt_report = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_report
            // 
            this.bt_report.Location = new System.Drawing.Point(174, 148);
            this.bt_report.Name = "bt_report";
            this.bt_report.Size = new System.Drawing.Size(134, 23);
            this.bt_report.TabIndex = 0;
            this.bt_report.Text = "Випустити звіт";
            this.bt_report.UseVisualStyleBackColor = true;
            this.bt_report.Click += new System.EventHandler(this.bt_report_Click);
            // 
            // tb_folder
            // 
            this.tb_folder.Location = new System.Drawing.Point(109, 104);
            this.tb_folder.Name = "tb_folder";
            this.tb_folder.Size = new System.Drawing.Size(335, 20);
            this.tb_folder.TabIndex = 1;
            // 
            // bt_OpenFolder
            // 
            this.bt_OpenFolder.Location = new System.Drawing.Point(28, 102);
            this.bt_OpenFolder.Name = "bt_OpenFolder";
            this.bt_OpenFolder.Size = new System.Drawing.Size(75, 23);
            this.bt_OpenFolder.TabIndex = 2;
            this.bt_OpenFolder.Text = "Директорія";
            this.bt_OpenFolder.UseVisualStyleBackColor = true;
            this.bt_OpenFolder.Click += new System.EventHandler(this.bt_OpenFolder_Click);
            // 
            // chb_504
            // 
            this.chb_504.AutoSize = true;
            this.chb_504.Location = new System.Drawing.Point(28, 35);
            this.chb_504.Name = "chb_504";
            this.chb_504.Size = new System.Drawing.Size(44, 17);
            this.chb_504.TabIndex = 3;
            this.chb_504.Text = "504";
            this.chb_504.UseVisualStyleBackColor = true;
            this.chb_504.CheckedChanged += new System.EventHandler(this.chb_504_CheckedChanged);
            // 
            // chb_503
            // 
            this.chb_503.AutoSize = true;
            this.chb_503.Checked = true;
            this.chb_503.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_503.Location = new System.Drawing.Point(28, 12);
            this.chb_503.Name = "chb_503";
            this.chb_503.Size = new System.Drawing.Size(44, 17);
            this.chb_503.TabIndex = 4;
            this.chb_503.Text = "503";
            this.chb_503.UseVisualStyleBackColor = true;
            this.chb_503.CheckedChanged += new System.EventHandler(this.chb_503_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Дата звіту";
            // 
            // dt_report
            // 
            this.dt_report.Location = new System.Drawing.Point(109, 69);
            this.dt_report.Name = "dt_report";
            this.dt_report.Size = new System.Drawing.Size(200, 20);
            this.dt_report.TabIndex = 6;
            this.dt_report.Value = new System.DateTime(2019, 4, 4, 0, 0, 0, 0);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 193);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(533, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "Статус виконання";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "...";
            // 
            // FmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 215);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dt_report);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chb_503);
            this.Controls.Add(this.chb_504);
            this.Controls.Add(this.bt_OpenFolder);
            this.Controls.Add(this.tb_folder);
            this.Controls.Add(this.bt_report);
            this.Name = "FmReport";
            this.Text = "Параметри звіту";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FmReport_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button bt_report;
        private System.Windows.Forms.TextBox tb_folder;
        private System.Windows.Forms.Button bt_OpenFolder;
        private System.Windows.Forms.CheckBox chb_504;
        private System.Windows.Forms.CheckBox chb_503;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dt_report;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}