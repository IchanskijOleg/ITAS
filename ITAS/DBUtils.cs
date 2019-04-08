using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace ITAS
{
    class DBUtils
    {  
        private static string host = "192.168.1.36"; // BO
        //private static string host = "192.168.1.37"; //TEST
        private static int port = 1521;
        private static string sid = "lisa";
        public static string user = "";
        public static string password = "";

        public static OracleConnection GetDBConnection()
        {
            return DBOracleUtils.GetDBConnection(host, port, sid, user, password);
        }
    }
}
