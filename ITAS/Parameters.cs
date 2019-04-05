using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAS
{
    public static class Parameters
    {
        private const string fileType = ".CSV";
        private static string filePath;
        public static DateTime DateReport { get; set; }
        public static string FilePath
        {
            get { return AddFileName(filePath); }
            set { filePath = value; }
        }
        public static int ReportId { get; set; }

        private static string AddFileName(string path)
        {
            return path + "\\" + ReportId + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;
        }
    }
}
