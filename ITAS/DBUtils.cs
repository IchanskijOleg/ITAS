using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
//using System.Data.OracleClient;

namespace ITAS
{
    class DBUtils
    {
        //private static string host = "192.168.1.36"; // BO
        //private static string host = "192.168.1.37"; //TEST
        //private static int port = 1521;
        //private static string sid = "lisa";
        public static string user = "";
        public static string password = "";
        public static string tns;

        public static OracleConnection GetDBConnection()
        {
            //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["lisa_test"].ConnectionString;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[tns].ConnectionString;
            OracleConnectionStringBuilder connectionBuilder = new OracleConnectionStringBuilder(connectionString);
            connectionBuilder.UserID = user;
            connectionBuilder.Password = password;
            return new OracleConnection(connectionBuilder.ConnectionString);
            //return DBOracleUtils.GetDBConnection(host, port, sid, user, password);
        }
    }
}
