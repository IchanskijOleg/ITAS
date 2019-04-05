using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITAS
{
    public partial class FmReport : Form
    {
        public FmReport()
        {
            InitializeComponent();
        }
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
            Parameters.ReportId = (chb_503.Checked) ? 1 : 2;
            Parameters.DateReport = dt_report.Value;
            isAuthorized = true;
            this.Close();
        }

        private void FmReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isAuthorized)
                Environment.Exit(0);
        }
    }
}
