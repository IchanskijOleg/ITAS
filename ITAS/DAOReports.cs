using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace ITAS
{
    class DAOReports
    {
        // Объявляем делегат
        public delegate void ProgressStateHandler(string message, int value);
        // Событие, возникающее при смене статуса прогресса
        public event ProgressStateHandler UpdateStatusStrip;

        //public event Action<string> backgroundWorker1;
        //Thread myThread;
        //event Action<string> UpdateStatusStrip;

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

        public void SetReportData(BackgroundWorker worker)
        {
            //BackgroundWorker m_search_bgw = new BackgroundWorker();
            //m_search_bgw.RunWorkerAsync();
            try
            {
                OracleConnection conn = DBUtils.GetDBConnection();
                conn.Open();

                //if (UpdateStatusStrip != null)
                {
                    worker.ReportProgress(20, "Начитка даних в БД.");
                    //UpdateStatusStrip.Invoke("Начитка даних в БД.", 20);
                    //Thread myThread = new Thread(() => { UpdateStatusStrip.Invoke("Начитка даних в БД.", 20); });
                    //myThread.Start();
                }
                //if (RefreshProgressBar != null) { RefreshProgressBar.Invoke(); }

                //підключаємося до БД
                //виконуємо процедуру заповнення таблиці
                //using (OracleCommand command = new OracleCommand("pkg_tas_reports.reservereport_test", conn)) //на тестовій Лізі для тесту
                using (OracleCommand command = new OracleCommand("lisa.pkg_tas_reports.ReserveReportMain503_504", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("ip_arcDate", OracleDbType.Date).Value = Parameters.DateReport;
                    command.Parameters.Add("ip_reportIid", OracleDbType.Int32).Value = Parameters.ReportId;
                    command.ExecuteNonQuery();
                }

                //if (UpdateStatusStrip != null)
                {
                    //Thread myThread = new Thread(() => { UpdateStatusStrip.Invoke("Завантаження в файл.", 85); });
                    //myThread.Start();
                    //UpdateStatusStrip.Invoke("Завантаження в файл.", 85);
                    worker.ReportProgress(85, "Завантаження в файл.");
                }
                //if (RefreshProgressBar != null) { RefreshProgressBar.Invoke(); }

                string qCurr = @"select '""Дата Звіту"";""Валюта"";""Курс"";;""Валюта"";""Курс""' from dual
                             union
                             select reportdate || ';' || USDRATESTRING || ';' || EURRATESTRING from RESERVEREPORT504_SUM_TAS_ALL";
                string qHead;
                string qSelect;
                string qSum;
                string qSumNbu;

                List<string> sqlList = new List<string>() { qCurr };

                if (Parameters.ReportId == 503)  //503
                {
                    qSelect = @"select '""Код полісу"";""Код першої версії"";""Номер полісу"";""Прізвище страхувальника"";""Ім''я страхувальника"";""По-батькові страхувальника"";""Прізвище застрахованої особи"";""Ім''я застрахованої особи"";""По-батькові застрахованої особи"";""Валюта страхування"";""Курс валюти"";""Резерв по ризику дожиття"";""Резерв по ризику смерть"";""Резерв по ризику пенсія"";""Резерв за іншими ризиками"";""Резерв бонусів"";""Попередній ІД поточного року"";""Попередній ІД на РНВ поточного року"";""Сумарний резерв"";""Резерв відданого на перестрахування"";""Сумарний резерв за вирахуванням відданого на перестрахування"";""Резерв напередсплачених внесків"";""Інвестиційний прибуток від резерву напередсплачених внесків"";""Різниця між модифікованим та немодифікованим резервами"";""Різниця між проспективним брутто-резервом та модифікованим резервом"";""Борг"";""Страхова агенція"";""Агентська винагорода"";""Программа страхування""' from dual
                            union
                            select R from RESERVEREPORT500_SUM_TAS_ALL";
                    qSum = @"select ';;;;;;;;;;""Всього грн"";""Резерв по ризику дожиття"";""Резерв по ризику смерть"";""Резерв по ризику пенсія"";""Резерв за іншими ризиками"";""Резерв бонусів"";""Попередній ІД поточного року"";""Попередній ІД на РНВ поточного року"";""Сумарний резерв"";""Резерв відданого на перестрахування"";""Сумарний резерв за вирахуванням відданого на перестрахування"";""Резерв напередсплачених внесків"";""Інвестиційний прибуток від резерву напередсплачених внесків"";""Різниця між модифікованим та немодифікованим резервами"";""Різниця між проспективним брутто-резервом та модифікованим резервом"";""Борг""' from dual
                        union
                        select ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || S from RESERVEREPORT500_SUM_TAS_ALL";
                    sqlList.Add(qSelect);
                    sqlList.Add(qSum);
                }
                else //504
                {
                    qHead = @"select  ';;;;;;;;;;;;;;;;;;;;""Дожиття"";""Дожиття"";""Смерть"";""Смерть"";""Пенсія"";""Пенсія"";;;;;""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Резерв дожиття"";""Попередній ІД поточного року"";""Попередній ІД на РНВ поточного року"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв смерть"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв пенсія"";""Резерв додаткових ризиків"";""Резерв додаткових ризиків"";""Резерв додаткових ризиків"";;""Резерв бонусів"";""Резерв бонусів"";""Резерв бонусів"";""Сумарний резерв"";""Сумарний резерв"";""Сумарний резерв"";""Резерв перестрахування"";""Резерв перестрахування"";""Резерв перестрахування"";""Сумарний резерв за вирахуванням відданого на перестрахування"";;;;;""Резерв напередсплачених внесків"";;;""Інвестиційний прибуток від резерву напередсплачених внесків"";;;""Різниця між модифікованим та немодифікованим резервами"";;;""Різниця між проспективним брутто-резервом та модифікованим резервом"";;;""Страхова агенція(Брокер)"";""Агентська винагорода""'from dual";
                    qSelect = @"select '""Код полісу"";""Код першої версії"";""Номер полісу"";""Прізвище страхувальника"";""Ім''я страхувальника"";""По-батькові страхувальника"";""Прізвище застрахованої особи"";""Ім''я застрахованої особи"";""По-батькові застрахованої особи"";""Дата народження ЗО"";""Стать ЗО"";""Андерайтерська надбавка (rating-life)"";""Надбавка ADB"";""Термін дії надбавки  ADB"";""Програма страхування"";""Статус полісу"";""Початок дії полісу"";""Завершення дії полісу"";""Валюта страхування"";""Курс валюти"";""СС у валюті страхування, вал"";""Величина однієї брутто-премії, вал"";""СС у валюті страхування, вал"";""Величина однієї брутто-премії, вал"";""СС у валюті страхування, вал"";""Величина однієї брутто-премії, вал"";""Термін сплати внесків"";""Періодичність внесків (кількість внесків на рік)"";""Номер поточного року дії договору"";""Днів від попередньої річниці"";""Резерв на початок страхового року, вал"";""Резерв на кінець страхового року, вал"";""UAH"";""USD"";""EUR"";""у валюті страхування"";""у валюті страхування"";""Резерв на початок страхового року, вал"";""Резерв на кінець страхового року, вал"";""UAH"";""USD"";""EUR"";""Резерв на початок страхового року, вал"";""Резерв на кінець страхового року, вал"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""Отримані бонуси, у валюті страхування"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""Фактично отримано внесків за полісом"";""Фактично отримані внески окрім напередсплачених внесків"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";""UAH"";""USD"";""EUR"";;' from dual
                               union
                               select R  from RESERVEREPORT504_SUM_TAS_ALL";
                    qSum = @"select ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;""Сума у валюті страхування""' from dual
                            union
                            select ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || S from RESERVEREPORT504_SUM_TAS_ALL";
                    qSumNbu = @"select ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;""Сума в гривнях по курсу НБУ на звітну дату""' from dual
                            union
                            select ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || ';' || SS from RESERVEREPORT504_SUM_TAS_ALL";
                    sqlList.Add(qHead);
                    sqlList.Add(qSelect);
                    sqlList.Add(qSum);
                    sqlList.Add(qSumNbu);
                }

                DataSet ds = new DataSet();

                foreach (var item in sqlList)
                {
                    OracleCommand oraComm = new OracleCommand(item, conn);
                    OracleDataReader reader = oraComm.ExecuteReader();
                    //OracleDataReader reader = new OracleCommand(item, conn).ExecuteReader(); скорочена форма
                    if (reader != null)
                    {
                        ds.Load(reader, LoadOption.OverwriteChanges, string.Empty);
                    }
                }
                //if (qHead != null) { OracleDataReader readerHead = new OracleCommand(qHead, conn).ExecuteReader(); }
                //if (qSelect != null) { OracleDataReader readerSelect = new OracleCommand(qSelect, conn).ExecuteReader(); }
                //if (qSum != null) { OracleDataReader readerSum = new OracleCommand(qSum, conn).ExecuteReader(); }
                //if (qSumNbu != null) { OracleDataReader readerSumNbu = new OracleCommand(qSumNbu, conn).ExecuteReader(); }

                //if (readerCurr != null) { ds.Load(readerCurr, LoadOption.OverwriteChanges, string.Empty); }
                //if (readerHead != null) { ds.Load(readerHead, LoadOption.OverwriteChanges, string.Empty); }
                //if (readerSelect != null) { ds.Load(readerSelect, LoadOption.OverwriteChanges, string.Empty); }
                //if (readerSum != null) { ds.Load(readerSum, LoadOption.OverwriteChanges, string.Empty); }
                //if (readerSumNbu != null) { ds.Load(readerSumNbu, LoadOption.OverwriteChanges, string.Empty); }

                //MessageBox.Show(Parameters.DateReport.ToString());
                //MessageBox.Show(Parameters.FilePath.ToString());
                //MessageBox.Show(Parameters.ReportId.ToString());

                using (StreamWriter sw = new StreamWriter(Parameters.FilePath, true, System.Text.Encoding.UTF8, 10))
                {
                    for (int t = 0; t < ds.Tables.Count; t++)
                    {
                        //проходимо по шапці таблиці
                        //foreach (DataColumn column in ds.Tables[0].Columns)
                        //{
                        //    sw.Write(EscapeComma(column.ColumnName));
                        //}
                        //sw.WriteLine();

                        //проходимо по рядкам таблиці
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
                conn.Close();

                // if (UpdateStatusStrip != null) { UpdateStatusStrip.Invoke("Фініш.", 100); }
                //if (RefreshProgressBar != null) { RefreshProgressBar.Invoke(); }
                //if (UpdateStatusStrip != null)
                {
                    //myThread = new Thread(() => { UpdateStatusStrip.Invoke("Фініш.", 100); });
                    //UpdateStatusStrip.Invoke("Фініш.", 100);
                    worker.ReportProgress(100, "Фініш.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("#2 ERROR: " + ex.Message);
                return;
            }
        }
    }
}
