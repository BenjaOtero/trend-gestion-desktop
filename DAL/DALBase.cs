using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace DAL
{
    public class DALBase
    {
        public DALBase()
        {

        }

        public static MySqlConnection GetConnection()
        {
            const string server = "ns21a.cyberneticos.com";
            const string user = "ncsoftwa_re";
            string db = ConfigurationManager.ConnectionStrings["DBMain"].ConnectionString;
            const string pass = "8953#AFjn";

           // "server=ns21a.cyberneticos.com;User Id=ncsoftwa_re;Persist Security Info=False;database=ncsoftwa_trend;Pwd=8953#AFjn" />

            string connectionString;
           MySqlConnection objCon;
            connectionString = "server=" + server + ";";
            connectionString += "User Id=" + user + ";";
            connectionString += "Persist Security Info=False;";
            connectionString += "database=" + db + ";";
            connectionString += "Pwd=" + pass;
            connectionString = ConfigurationManager.ConnectionStrings["DBMainLocal"].ConnectionString;        
            objCon = new MySqlConnection(connectionString);
            return objCon;
        }

        public static MySqlConnection GetDumpAdminConnection()
        {
            string connectionString;
            MySqlConnection objCon;
            connectionString = ConfigurationManager.ConnectionStrings["DBDumpAdmin"].ConnectionString;
            objCon = new MySqlConnection(connectionString);
            return objCon;
        }

        public static MySqlConnection GetTrendConnection()
        {
            string connectionString;
            MySqlConnection objCon;
            //  connectionString = ConfigurationManager.ConnectionStrings["DBTrend"].ConnectionString;
            connectionString = ConfigurationManager.ConnectionStrings["DBTrendLocal"].ConnectionString;
            objCon = new MySqlConnection(connectionString);
            return objCon;
        }

        public static OleDbConnection GetConnectionAccess()
        {
            string connectionString;
            OleDbConnection objCon;
            connectionString = ConfigurationManager.ConnectionStrings["DBAccess"].ConnectionString;
            objCon = new OleDbConnection(connectionString);
            return objCon;
        }
    }
}