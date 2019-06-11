using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.IO;
using System.Threading;

namespace ITAS
{
    public partial class FmReport : Form
    {
        //Thread th;
        public FmReport()
        {
            InitializeComponent();

            daoReports = new DAOReports();
            //подписываемся
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (s, e) =>
            {
                daoReports.SetReportData(worker);
            };
            worker.ProgressChanged += (sender, e) =>
            {
                var text = e.UserState as string;

                toolStripStatusLabel1.Text = text;
                toolStripProgressBar1.Value = e.ProgressPercentage;
                //statusStrip1.Refresh();
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                this.Close();
            };
        } 

        private DAOReports daoReports = null;
        private bool isAuthorized = false;
        private readonly BackgroundWorker worker;

        private void bt_OpenFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Reset();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_folder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void chb_503_CheckedChanged(object sender, EventArgs e)
        {
            chb_504.Checked = !chb_503.Checked;
        }

        private void chb_504_CheckedChanged(object sender, EventArgs e)
        {
            chb_503.Checked = !chb_504.Checked;
        }

        private void bt_report_Click(object sender, EventArgs e)
        {
            Parameters.FilePath = tb_folder.Text;
            Parameters.ReportId = (chb_503.Checked) ? 503 : 504;
            Parameters.DateReport = dt_report.Value;

            if (tb_folder.Text == "")
            {
                MessageBox.Show("Виберіть шлях збереження файлу!");
            }
            else
            {
                isAuthorized = true;
                bt_report.Enabled = false;
                //th = new Thread(()=> daoReports.SetReportData());
                //th.Start();
                //виконуємо начитку данних
                worker.RunWorkerAsync();
                //закриваємо формочку
            }
        }

        private void FmReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isAuthorized)
                Environment.Exit(0);
        }

        public void RefreshStrip()
        {
            statusStrip1.Refresh();  
            this.Refresh();
        }  
    }
}
