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

namespace ITAS
{
    public partial class FmLogin : Form
    {
        public FmLogin()
        {
            InitializeComponent();
        }
        private bool isAuthorized = false;

        private void bt_in_Click(object sender, EventArgs e)
        {
            if (tb_login.Text != "" && tb_pass.Text != "")
            {
                DBUtils.user = tb_login.Text;
                DBUtils.password = tb_pass.Text;

                OracleConnection conn = DBUtils.GetDBConnection();
                //підключаємося до БД
                try
                {
                    conn.Open();
                    conn.Close();
                    isAuthorized = true;
                    this.Close();
                    //MessageBox.Show(conn.ConnectionString, "Successful Connection");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невірний логін чи пароль!", "Помилка!");
                    //MessageBox.Show("#1 ERROR: " + ex.Message);
                    //Environment.Exit(0);
                }
            }
            else
            {
                MessageBox.Show("Уведіть логін та пароль!");
            }
        }

        private void Fm_login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isAuthorized)
                Environment.Exit(0);
        }
    }
}
