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
        public FmReport()
        {
            InitializeComponent();

            daoReports = new DAOReports();
            //подписываемся
            daoReports.UpdateStatusStrip += SetStatusStrip;
            daoReports.RefreshProgressBar += RefreshStrip;
            // создаем новый поток
            //Thread myThread = new Thread(new ThreadStart(RefreshStrip));
            //myThread.Start(); // запускаем поток

        }

        private DAOReports daoReports = null;
        private bool isAuthorized = false;

        private void bt_OpenFolder_Click(object sender, EventArgs e)
        {
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

                //виконуємо начитку данних
                daoReports.SetReportData();
                //закриваємо формочку
                this.Close();
            }
        }

        private void FmReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isAuthorized)
                Environment.Exit(0);
        }

        public void SetStatusStrip(string text, int val)
        {
            //statusStrip1.BackColor = Color.Green;
            toolStripStatusLabel1.Text = text;
            // toolStripProgressBar1.Increment(val);
            //toolStripProgressBar1.Value = val;
            toolStripProgressBar1.Value = val;
            //toolStripProgressBar1.
            //toolStripProgressBar1.ProgressBar.Refresh();
            statusStrip1.Refresh();

            this.Refresh();

            //backgroundWorker1.RunWorkerAsync();
        }
        public void RefreshStrip()
        {
            //new Thread(new ThreadStart(() =>
            //{
            //    //statusStrip1.Refresh();
            //    this.Refresh();
            //})).Start();
        }
    }
}
