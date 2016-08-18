using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.ServiceProcess;
using System.Windows.Forms;

namespace DAL
{
    public class DALBase
    {
        static int intentos = 0;

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
        abrirConexion:
            try
            {
                objCon.Open();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                {
                    if (intentos == 0)
                    {
                        if (ExisteServicioMySQL())
                        {
                         //   IniciarServicioMysql();
                            intentos++;
                            goto abrirConexion;
                        }
                    }
                    else
                    {                        
                        intentos = 0;
                        throw new ServidorMysqlInaccesibleException("No se pudo conectar con el servidor de base de datos.", ex);
                    }
                }
            }
            return objCon;
        }

        public static MySqlConnection GetDumpAdminConnection()
        {
            string connectionString;
            MySqlConnection objCon;
            connectionString = ConfigurationManager.ConnectionStrings["DBDumpAdmin"].ConnectionString;
            objCon = new MySqlConnection(connectionString);
        abrirConexion:
            try
            {
                objCon.Open();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                {
                    if (intentos == 0)
                    {
                        if (ExisteServicioMySQL())
                        {
                            IniciarServicioMysql();
                            intentos++;
                            goto abrirConexion;
                        }
                    }
                    else
                    {
                        intentos = 0;
                        throw new ServidorMysqlInaccesibleException("No se pudo conectar con el servidor de base de datos.", ex);
                    }
                }
            }
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

        private static void IniciarServicioMysql()
        {
            ServiceController sc = new ServiceController("MySQL");
            if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) || (sc.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                sc.Start();
            }
        }

        private static bool ExisteServicioMySQL()
        {
            bool existeServicio = false;
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == "MySQL")
                {
                    existeServicio = true;
                    continue;
                }
            }
            return existeServicio;
        }
    }

}