using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Configuration;
using System.IO;

namespace BL
{

    //-------------- TAREAS --------------//

    // NORMAL: ofuscar codigo
    // ALTA: Cargar ventas desde Gestion para probar
    // NORMAL: exportar datos(tbl stock) a un nombre de archivo con fecha
    // NORMAL: agregar link sp3 windows xp en página web
    // NORMAL: desarrolar ayuda
    // NORMAL: instalar Team Viewer
    // NORMAL: frmAlicuotasIva, txtPorcentajeALI_KeyPress(permitir un solo punto y dos decimales)
    // ALTA: claves duplicadas en todas las tablas
    /*
        `pc`
     * */

    public class UtilVarios
    {
        public static List<string> GetCredentialsFTP()
        {
            string connectionString;
            //connectionString = ConfigurationManager.ConnectionStrings["Ftp"].ConnectionString;
            connectionString = ConfigurationManager.ConnectionStrings["FtpLocal"].ConnectionString;
            Char delimiter = ';';
            String[] substrings = connectionString.Split(delimiter);
            string server = substrings[0];
            string user = substrings[1];
            string pass = substrings[2];
            List<string> credentials = new List<string>();
            credentials.Add(server);
            credentials.Add(user);
            credentials.Add(pass);
            return credentials;
        }       

        public static List<string> GetCredentialsDB()
        {
            string connectionString;
            connectionString = ConfigurationManager.ConnectionStrings["LocalCredentials"].ConnectionString;
            Char delimiter = ';';
            String[] substrings = connectionString.Split(delimiter);
            string server = substrings[0];
            string user = substrings[1];
            string database = substrings[2];
            string pass = substrings[3];
            List<string> credentials = new List<string>();
            credentials.Add(server);
            credentials.Add(user);
            credentials.Add(database);
            credentials.Add(pass);
            return credentials;
        }

        public static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;
            if (file1 == file2)
            {
                return true;
            }
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);
            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();
                return false;
            }
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));
            fs1.Close();
            fs2.Close();
            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }

        public static bool HayInternet()
        {
            bool conexion = false;
            Ping Pings = new Ping();
            int timeout = 10000; //cambiar el valor a 10000
            try
            {
                if (Pings.Send("google.com", timeout).Status == IPStatus.Success) // cambiar ip 127.0.0.1 a google.com
                {
                    conexion = true;
                }
            }
            catch (PingException)
            {
                conexion = false;
            }
            return conexion;
        }

        public static bool ExisteServicio(string name)
        {
            bool existeServicio = false;
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == name)
                {
                    existeServicio = true;
                    continue;
                }
            }
            return existeServicio;
        }

        public static void StartService(string name)
        {
            ServiceController sc = new ServiceController(name);
            try
            {
                if (sc != null && sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                }
                sc.WaitForStatus(ServiceControllerStatus.Running);
                sc.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
