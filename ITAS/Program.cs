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
        /// Змінюємо відповідно до csv формату
        /// </summary>
        /// <param name="st">Рядок</param>
        /// <returns>Повертаємо рядок обрамлений лапками</returns>
        static string EscapeComma(string st)
        {
            //return "\"" + st + "\";";
            //st = "\"" + st + "\"";
            return st.Replace("'", "") + ";";
        }

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

            OracleConnection conn = DBUtils.GetDBConnection();
            //підключаємося до БД
            try
            {
                conn.Open();
                //MessageBox.Show(conn.ConnectionString, "Successful Connection");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невірний логін чи пароль!", "Помилка!");
                MessageBox.Show("#1 ERROR: " + ex.Message);
                return;
            }

            //вікно Звіту
            Application.Run(new FmReport());

            try
            {
                //виконуємо процедуру заповнення таблиці
                using (OracleCommand command = new OracleCommand("pkg_tas_reports.reservereport_test", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("ip_arcDate", OracleDbType.Date).Value = Parameters.DateReport;
                    command.Parameters.Add("ip_reportIid", OracleDbType.Int32).Value = 1;
                    command.ExecuteNonQuery();
                }

                string qCurr = @"select '""Дата Звіту"";""Валюта"";""Курс"";;""Валюта"";""Курс""' from dual
                                 union
                                 select reportdate || ';' || USDRATESTRING || ';' || EURRATESTRING from RESERVEREPORT504_SUM_TAS_ALL";
                string qHead = @"select  ';;;;;;;;;;;;;;;;;;;;""Дожиття"";""Дожиття"";""Смерть"";""Смерть"";""Пенсія"";""Пенсія"";;;;;""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Попередній ІД поточного року"";""Попередній ІД на РНВ поточного року"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв додаткових ризиків"";""Резерв додаткових ризиків"";""Резерв додаткових ризиків"";;""Резерв бонусів"";""Резерв бонусів"";""Резерв бонусів"";""Сумарний резерв"";""Сумарний резерв"";""Сумарний резерв"";""Резерв перестрахування"";""Резерв перестрахування"";""Резерв перестрахування"";""Сумарний резерв за вирахуванням відданого на перестрахування"";;;;;""Резерв напередсплачених внесків"";;;""Інвестиційний прибуток від резерву напередсплачених внесків"";;;""Різниця між модифікованим та немодифікованим резервами"";;;""Різниця між проспективним брутто-резервом та модифікованим резервом"";;;""Страхова агенція(Брокер)"";""Агентська винагорода""'from dual";
                string qSelect = @"select '""Код полісу"";""Код першої версії"";""Номер полісу"";""Прізвище страхувальника"";""Ім''я страхувальника"";""По-батькові страхувальника"";""Прізвище застрахованої особи"";""Ім''я застрахованої особи"";""По-батькові застрахованої особи"";""Дата народження ЗО"";""Стать ЗО"";""Андерайтерська надбавка (rating-life)"";""Надбавка ADB"";""Термін дії надбавки  ADB"";""Програма страхування"";""Статус полісу"";""Початок дії полісу"";""Завершення дії полісу"";""Валюта страхування"";""Курс валюти"";""СС у валюті страхування, вал"";""Величина однієї брутто-премії, вал"";""СС у валюті страхування, вал"";""Величина однієї брутто-премії, вал"";""СС у валюті страхування, вал"";""Величина однієї брутто-премії, вал"";""Термін сплати внесків"";""Періодичність внесків (кількість внесків на рік)"";""Номер поточного року дії договору"";""Днів від попередньої річниці"";""Резерв на початок страхового року, вал"";""Резерв на кінець страхового року, вал"";""UAH"";""USD"";""EUR"";""у валюті страхування"";""у валюті страхування"";""Резерв на початок страхового року, вал"";""Резерв на кінець страхового року, вал"";""UAH"";""USD"";""EUR"";""Резерв на початок страхового року, вал"";""Резерв на кінець страхового року, вал"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""Отримані бонуси, у валюті страхування"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""Фактично отримано внесків за полісом"";""Фактично отримані внески окрім напередсплачених внесків"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";;' from dual
                                   union
                                   select R  from RESERVEREPORT504_SUM_TAS_ALL";
                string qSum = @"select ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;""Сума у валюті страхування""' from dual
                                union
                                select ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || S from RESERVEREPORT504_SUM_TAS_ALL";
                string qSumNbu = @"select ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;""Сума в гривнях по курсу НБУ на звітну дату""' from dual
                                union
                                select ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || SS from RESERVEREPORT504_SUM_TAS_ALL";
                //OracleCommand oraComm = new OracleCommand(qCurr, conn);
                //OracleDataReader reader = oraComm.ExecuteReader();
                OracleDataReader readerCurr = new OracleCommand(qCurr, conn).ExecuteReader();
                OracleDataReader readerHead = new OracleCommand(qHead, conn).ExecuteReader();
                OracleDataReader readerSelect = new OracleCommand(qSelect, conn).ExecuteReader();
                OracleDataReader readerSum = new OracleCommand(qSum, conn).ExecuteReader();
                OracleDataReader readerSumNbu = new OracleCommand(qSumNbu, conn).ExecuteReader();

                DataSet ds = new DataSet();

                if (readerCurr != null) { ds.Load(readerCurr, LoadOption.OverwriteChanges, string.Empty); }
                if (readerHead != null) { ds.Load(readerHead, LoadOption.OverwriteChanges, string.Empty); }
                if (readerSelect != null) { ds.Load(readerSelect, LoadOption.OverwriteChanges, string.Empty); }
                if (readerSum != null) { ds.Load(readerSum, LoadOption.OverwriteChanges, string.Empty); }
                if (readerSumNbu != null) { ds.Load(readerSumNbu, LoadOption.OverwriteChanges, string.Empty); }

                //MessageBox.Show(Parameters.DateReport.ToString());
                //MessageBox.Show(Parameters.FilePath.ToString());
                //MessageBox.Show(Parameters.ReportId.ToString());

                using (StreamWriter sw = new StreamWriter(Parameters.FilePath, true, System.Text.Encoding.UTF8, 10))
                {
                    for (int t = 0; t < ds.Tables.Count; t++)
                    {
                        //foreach (DataColumn column in ds.Tables[0].Columns)
                        //{
                        //    sw.Write(EscapeComma(column.ColumnName));
                        //}
                        //sw.WriteLine();

                        foreach (DataRow row in ds.Tables[t].Rows)
                        {
                            for (int i = 0; i < ds.Tables[t].Columns.Count; i++)
                            {
                                sw.Write(EscapeComma(row[i].ToString()));
                            }
                            sw.WriteLine();
                        }
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("#2 ERROR: " + ex.Message);
                return;
            }

            conn.Close();

        }
    }
}
