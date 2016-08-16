using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;
using DAL;
using Entities;
using System.Net;
using System.Configuration;
using System.Diagnostics;

namespace BL
{
    public class DatosBLL
    {
        static string idRazonSocial;
        static string strFile;
        static int intentosDump = 0;
        static int intentosUpload = 0;

        //
        // IMPORTAR MOVIMIENTOS POS           COMPROBAR
        //
        public static void GetDataPOS()
        {
            List<string> directories = GetDirectoriesFTP();
            if (directories.Count() > 0)
            {
                DataTable tbl = BL.GetDataBLL.RazonSocial();
                string idRazonSocial = tbl.Rows[0][0].ToString() + "_";
                DescargarArchivos(directories, idRazonSocial);
                string[] archivos = Directory.GetFiles(@"c:\windows\temp\data_import", idRazonSocial + "*");
                FtpWebRequest ftpRequest;
                foreach (string archivo in archivos)
                {
                    if (RestaurarDatos(archivo))
                    {
                        BL.DatosBLL.InsertarMovimientos();
                        // borro archivos en el servidor ftp
                        Char delimitador = '\\';
                        String[] cadena = archivo.Split(delimitador);
                        string borrar = cadena[4];                        
                        ftpRequest = Utilitarios.FtpRequest(@"/datos/" + borrar);
                        ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                        FtpWebResponse respuesta = (FtpWebResponse)ftpRequest.GetResponse();
                    }
                    else
                    {
                        GetDataPOS();
                    }
                }
            }  
        }

        public static List<string> GetDirectoriesFTP()
        {
            FtpWebRequest ftpRequest = Utilitarios.FtpRequest("/datos");
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            List<string> directories = new List<string>();
            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();
            return directories;
        }

        private static void DescargarArchivos(List<string> directories, string idRazonSocial)
        {
            if (Directory.Exists(@"c:\windows\temp\data_import")) Directory.Delete(@"c:\windows\temp\data_import", true);
            Directory.CreateDirectory(@"c:\windows\temp\data_import");
            string connectionString = ConfigurationManager.ConnectionStrings["FtpLocal"].ConnectionString;
            //string connectionString = ConfigurationManager.ConnectionStrings["Ftp"].ConnectionString;
            Char delimiter = ';';
            String[] substrings = connectionString.Split(delimiter);
            string ftpServerIP = substrings[0] + "/datos";
            string ftpUserID = substrings[1];
            string ftpPassword = substrings[2];
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new System.Net.NetworkCredential(ftpUserID, ftpPassword);
            using (ftpClient)
            {
                foreach (string archivo in directories)
                {
                    if (archivo.Contains(idRazonSocial))
                    {
                        if (!archivo.Contains("datos") && !archivo.Contains("locales") && !archivo.Contains("pcs") && !archivo.Contains("bck"))
                        {
                            string ftpPath = "ftp://" + ftpServerIP + "/" + archivo;
                            string localPath = @"c:\windows\temp\data_import\" + archivo;
                            ftpClient.DownloadFile(ftpPath, localPath);
                        }
                    }
                }
            }
        }

        private static bool RestaurarDatos(string archivo)
        {
            bool restaurarDatos = false;
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\data_import\\restore.bat"); // creo el archivo .bat
            sw.Close();
            StringBuilder sb = new StringBuilder();
            string path = Application.StartupPath;
            string unidad = path.Substring(0, 2);
            sb.AppendLine(unidad);
            sb.AppendLine(@"cd " + path + @"\Mysql");
            sb.AppendLine(@"gzip -d " + archivo);
            archivo = archivo.Substring(0, archivo.Length - 3);
            sb.AppendLine(@"mysql -u ncsoftwa_re -p8953#AFjn ncsoftwa_re < " + archivo);
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\data_import\\restore.bat", true)) // escribo el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\data_import\\restore.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;  // permite disparar el evento process_Exited
            process.Exited += new EventHandler(RestaurarDatos_Exited);
            process.Start();
            process.WaitForExit();
            // compruebo si se restauraron los datos
            Char delimiter = '_';
            String[] substrings = archivo.Split(delimiter);
            int pc =  Convert.ToInt32(substrings[2].Substring(2));
            string fecha = substrings[3].Substring(0,10);
            int registroRestaurado = DAL.DatosDAL.RegistroRestaurado(fecha, pc);
            if (registroRestaurado > 0) restaurarDatos = true;
            return restaurarDatos;
        }

        private static void RestaurarDatos_Exited(object sender, System.EventArgs e)
        {
            if (File.Exists("c:\\Windows\\Temp\\data_import\\restore.bat")) File.Delete("c:\\Windows\\Temp\\data_import\\restore.bat");
            if (File.Exists("c:\\Windows\\Temp\\datos.sql")) File.Delete("c:\\Windows\\Temp\\datos.sql");
            if (File.Exists("c:\\Windows\\Temp\\datos.sql.gz")) File.Delete("c:\\Windows\\Temp\\datos.sql.gz");
        }

        public static void InsertarMovimientos()
        {
            DAL.DatosDAL.InsertarMovimientos();
        }


        // EXPORTAR DATOS POS

        public static void ExportarDatos()
        {
            DumpBD();
            if (ComprobarDump())
            {
            Reintentar:
                Utilitarios.UploadFromFile(@"c:\windows\temp\" + strFile, "/datos/" + strFile);
                Utilitarios.DownloadFile(@"c:\windows\temp\tmp_" + strFile, "/datos/" + strFile);
                if (!Utilitarios.FileCompare(@"c:\windows\temp\tmp_" + strFile, @"c:\windows\temp\" + strFile))
                {
                    if (intentosUpload < 5)
                    {
                        intentosUpload++;
                        goto Reintentar;
                    } 
                }
            }
            else
            {
                if (intentosDump < 5)
                {
                    intentosDump++;
                    ExportarDatos();
                }                
            }
        }

        private static void DumpBD()
        {
            DataTable tbl = BL.GetDataBLL.RazonSocial();
            idRazonSocial = tbl.Rows[0][0].ToString();
            strFile = idRazonSocial + "_datos.sql.xz";
            if (File.Exists("c:\\Windows\\Temp\\backup.bat")) File.Delete("c:\\Windows\\Temp\\backup.bat");
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\backup.bat"); //MO creo el archivo .bat
            sw.Close();
            StringBuilder sb = new StringBuilder();
            string path = Application.StartupPath;
            string unidad = path.Substring(0, 2);
            sb.AppendLine(unidad);
            sb.AppendLine(@"cd " + path + @"\Backup");
            sb.AppendLine(@"mysqldump -t --skip-comments -u ncsoftwa_re -p8953#AFjn -h localhost --opt ncsoftwa_re alicuotasiva articulos clientes formaspago generos razonsocial stock | xz > c:\windows\temp\" + strFile);
            //sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h localhost --opt ncsoftwa_re alicuotasiva articulos clientes formaspago generos razonsocial stock | gzip > c:\windows\temp\" + razonSocial);
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\backup.bat", true)) // escribo el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\backup.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;  // permite disparar el evento process_Exited
         //   process.Exited += new EventHandler(ExportarDatos_Exited);
            process.Start();
            process.WaitForExit();
        }

        private static void ExportarDatos_Exited(object sender, System.EventArgs e)
        {
            if (File.Exists("c:\\Windows\\Temp\\backup.bat")) File.Delete("c:\\Windows\\Temp\\backup.bat");
        }

        private static bool ComprobarDump()
        {
            bool comprobarDump = true;
            DAL.DatosDAL.DeleteAll();
            if (!Directory.Exists(@"c:\windows\temp\data"))
            {
                DirectoryInfo di = Directory.CreateDirectory(@"c:\windows\temp\data");
            }
              // copio el archivo para ejecutar el dump desde la copia porque al descomprimirlo se borra y no lo puedo subir
            if (File.Exists(@"c:\windows\temp\data\" + strFile)) File.Delete(@"c:\windows\temp\data\" + strFile);
            File.Copy(@"c:\windows\temp\" + strFile, @"c:\windows\temp\data\" + strFile);    
            string restaurar = strFile.Substring(0, strFile.Length - 3);
            if (File.Exists("C:\\Windows\\Temp\\data\\" + restaurar)) File.Delete("C:\\Windows\\Temp\\data\\" + restaurar);
            if (File.Exists(@"C:\Windows\Temp\restore.bat")) File.Delete(@"C:\Windows\Temp\restore.bat");
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\restore.bat"); // creo el archivo .bat
            sw.Close();
            StringBuilder sb = new StringBuilder();
            string path = Application.StartupPath;
            string unidad = path.Substring(0, 2);
            sb.AppendLine(unidad);
            sb.AppendLine(@"cd " + path + @"\Backup");
            sb.AppendLine("xz -d \"C:\\Windows\\Temp\\data\\" + strFile + "\"");
            sb.AppendLine("mysql -u ncsoftwa_re -p8953#AFjn dump_admin < \"C:\\Windows\\Temp\\data\\" + restaurar + "\"");
          //  sb.AppendLine("pause");
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\restore.bat", true)) // escribo el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\restore.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;  // permite disparar el evento process_Exited
         //   process.Exited += new EventHandler(RestaurarDatos_Exited);
            process.Start();
            process.WaitForExit();

            DataSet ds = DAL.DatosDAL.ControlarUpdate();
            int records;
            foreach (DataTable tbl in ds.Tables)
            {
                records = Convert.ToInt16(tbl.Rows[0][0].ToString());
                if (records == 0)
                {
                    comprobarDump = false;
                    break;
                }
            }
            return comprobarDump;
        }

    }
}