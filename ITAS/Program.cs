using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Data;
using System.IO;

namespace ITAS
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //вікно входу в систему
            Application.Run(new FmLogin());

            //вікно параметрів Звіту Актуаріїв
            Application.Run(new FmReport());

            //вікно параметрів звіту для корпоративного страхування
            //Application.Run(new FmMain());
        }
    }
}
