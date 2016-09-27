using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Net;

namespace BL
{
    public class DatosBLL
    {
        static List<string> credentials;
        static string server;
        static string user;
        static string database;
        static string pass;
        static string idRazonSocial;
        static string strFile;
        static int intentosGetPOS = 0;
        static int intentosDump = 0;
        static int intentosUpload = 0;

        // IMPORTAR MOVIMIENTOS POS

        public static void GetDataPOS(bool diarios)
        {
            List<string> directories = GetDirectoriesFTP();
            if (directories.Count() > 0)
            {
                DataTable tbl = BL.GetDataBLL.RazonSocial();
                string idRazonSocial = tbl.Rows[0][0].ToString() + "_";
                DescargarArchivos(directories, idRazonSocial, diarios);
                string[] archivos = Directory.GetFiles(@"c:\windows\temp\data_import", idRazonSocial + "*");
                FtpWebRequest ftpRequest;
                foreach (string archivo in archivos)
                {
                    if (RestaurarDatos(archivo))
                    {
                        DAL.DatosDAL.InsertarMovimientos();
                        Char delimitador = '\\';
                        String[] cadena = archivo.Split(delimitador);
                        string borrar = cadena[4];                        
                        ftpRequest = UtilFTP.FtpRequest(@"/datos/" + borrar);
                        ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                        FtpWebResponse respuesta = (FtpWebResponse)ftpRequest.GetResponse();
                    }
                    else
                    {
                        if (intentosGetPOS < 10)
                        {
                            intentosGetPOS++;
                            GetDataPOS(diarios);
                        }                        
                    }
                }
            }
            if (Directory.Exists(@"c:\windows\temp\data_import")) Directory.Delete(@"c:\windows\temp\data_import", true);
        }

        public static List<string> GetDirectoriesFTP()
        {
            FtpWebRequest ftpRequest = UtilFTP.FtpRequest("/datos");
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

        private static void DescargarArchivos(List<string> directories, string idRazonSocial, bool diarios)
        {
            if (Directory.Exists(@"c:\windows\temp\data_import")) Directory.Delete(@"c:\windows\temp\data_import", true);
            Directory.CreateDirectory(@"c:\windows\temp\data_import");
            credentials = UtilVarios.GetCredentialsFTP();
            server = credentials[0] + "/datos"; 
            user = credentials[1];
            pass = credentials[2];
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new System.Net.NetworkCredential(user, pass);
            using (ftpClient)
            {
                foreach (string archivo in directories)
                {
                    if (archivo.Contains(idRazonSocial))
                    {
                        if (!archivo.Contains("datos") && !archivo.Contains("locales") && !archivo.Contains("pcs") && !archivo.Contains("bck"))
                        {
                            Char delimitador = '_';
                            String[] cadena = archivo.Split(delimitador);
                            string strFecha =  cadena[2].Substring(0,10);
                            DateTime fecha = Convert.ToDateTime(strFecha);
                            if (diarios)
                            {
                                if (fecha == DateTime.Today)
                                {
                                    string ftpPath = "ftp://" + server + "/" + archivo;
                                    string localPath = @"c:\windows\temp\data_import\" + archivo;
                                    ftpClient.DownloadFile(ftpPath, localPath);
                                }
                            }
                            else
                            {
                                if (fecha < DateTime.Today)
                                {
                                    string ftpPath = "ftp://" + server + "/" + archivo;
                                    string localPath = @"c:\windows\temp\data_import\" + archivo;
                                    ftpClient.DownloadFile(ftpPath, localPath);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool RestaurarDatos(string archivo)
        {
            bool restaurarDatos = false;
            List<string> credentials = UtilVarios.GetCredentialsDB();
            string server = credentials[0];
            string user = credentials[1];
            string database = credentials[2];
            string pass = credentials[3];
            UtilDB.UnzipDB(archivo);
            archivo = archivo.Substring(0, archivo.Length - 3);
            UtilDB.RestoreDB(server, 3306, user, pass, database, archivo);
            // compruebo si se restauraron los datos
            Char delimiter = '_';
            String[] substrings = archivo.Split(delimiter);
            int pc =  Convert.ToInt32(substrings[2].Substring(2));
            string fecha = substrings[3].Substring(0,10);
            int registroRestaurado = DAL.DatosDAL.RegistroRestaurado(fecha, pc);
            if (registroRestaurado > 0) restaurarDatos = true;
            return restaurarDatos;
        }

        // EXPORTAR DATOS POS

        public static void ExportarDatos()
        {
            credentials = UtilVarios.GetCredentialsDB();
            server = credentials[0];
            user = credentials[1];
            database = credentials[2];
            pass = credentials[3];
            DataTable tbl = BL.GetDataBLL.RazonSocial();
            idRazonSocial = tbl.Rows[0][0].ToString();
            strFile = idRazonSocial + "_datos.sql";
            UtilDB.DumpDatos(server, user, pass, database, @"c:\windows\temp\" + strFile); 
            if (ValidarDump())
            {
                if(File.Exists(@"c:\windows\temp\" + strFile + ".xz")) File.Delete(@"c:\windows\temp\" + strFile + ".xz");
                UtilDB.ZipDB(@"c:\windows\temp\" + strFile);
                strFile = strFile + ".xz";
            Reintentar:
                UtilFTP.UploadFromFile(@"c:\windows\temp\" + strFile, "/datos/" + strFile);
                UtilFTP.DownloadFile(@"c:\windows\temp\tmp_" + strFile, "/datos/" + strFile);
                if (!UtilVarios.FileCompare(@"c:\windows\temp\tmp_" + strFile, @"c:\windows\temp\" + strFile))
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
            intentosUpload = 0;
            intentosDump = 0;
        }

        private static bool ValidarDump()
        {
            bool comprobarDump = true;
            DAL.DatosDAL.DeleteAll();
            UtilDB.RestoreDB(server, 3306, user, pass, "dump_admin", "C:\\Windows\\Temp\\" + strFile);
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