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
    public partial class FmLogin : Form
    {
        public FmLogin()
        {
            InitializeComponent();
            //string qHead = @"select  ';;;;;;;;;;;;;;;;;;;;""Дожиття"";""Дожиття"";""Смерть"";""Смерть"";""Пенсія"";""Пенсія"";;;;;""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Попередній ІД поточного року"";""Попередній ІД на РНВ поточного року"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв додаткових ризиків"";""Резерв додаткових ризиків"";""Резерв додаткових ризиків"";;""Резерв бонусів"";""Резерв бонусів"";""Резерв бонусів"";""Сумарний резерв"";""Сумарний резерв"";""Сумарний резерв"";""Резерв перестрахування"";""Резерв перестрахування"";""Резерв перестрахування"";""Сумарний резерв за вирахуванням відданого на перестрахування"";;;;;""Резерв напередсплачених внесків"";;;""Інвестиційний прибуток від резерву напередсплачених внесків"";;;""Різниця між модифікованим та немодифікованим резервами"";;;""Різниця між проспективним брутто-резервом та модифікованим резервом"";;;""Страхова агенція(Брокер)"";""Агентська винагорода""'from dual";

            //MessageBox.Show(qHead);
        }
        private bool isAuthorized = false;

        private void bt_in_Click(object sender, EventArgs e)
        {
            if (tb_login.Text != "" && tb_pass.Text != "")
            {
                DBUtils.user = tb_login.Text;
                DBUtils.password = tb_pass.Text;
                isAuthorized = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Уведіть логін та пароль!");
            }
        }

        private void Fm_login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!isAuthorized)
                Environment.Exit(0);
        }
    }
}
