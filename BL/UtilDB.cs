using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace BL
{
    public class UtilDB
    {
        public static bool ValidarServicioMysql()
        {
            bool funcionando = DAL.DALBase.ValidarServicioMysql();
            return funcionando;
        }

        public static void InstalarMySQL()
        {
            string path = Application.StartupPath + @"\MySql\mysql-essential-5.5.0-m2-win32.msi";
            Process process = new Process();
            process.StartInfo.FileName = path;
            string args = "/quiet";
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        public static void ConfigurarMySQL()
        {
            List<string> credentials = UtilVarios.GetCredentialsDB();
            string server = credentials[0];
            string user = credentials[1];
            string database = credentials[2];
            string pass = credentials[3];
            string pathMysql = string.Empty;
            if (Directory.Exists(@"C:\Program Files (x86)\MySQL\MySQL Server 5.5\bin"))
            {
                pathMysql = @"C:\Program Files (x86)\MySQL\MySQL Server 5.5\bin";
            }
            else if (Directory.Exists(@"C:\Program Files\MySQL\MySQL Server 5.5\bin"))
            {
                pathMysql = @"C:\Program Files\MySQL\MySQL Server 5.5\bin";
            }
            else if (Directory.Exists(@"C:\Archivos de programa\MySQL\MySQL Server 5.5\bin"))
            {
                pathMysql = @"C:\Archivos de programa\MySQL\MySQL Server 5.5\bin";
            }
            else if (Directory.Exists(@"C:\Archivos de programa (x86)\MySQL\MySQL Server 5.5\bin"))
            {
                pathMysql = @"C:\Archivos de programa (x86)\MySQL\MySQL Server 5.5\bin";
            }
            Process process = new Process();
            process.StartInfo.FileName = pathMysql + @"\mysqlinstanceconfig.exe";
            string args = "-i -q ServiceName=MySQL ServerType=DEVELOPER DatabaseType=INODB Port=3306 Charset=utf8 RootPassword=" + pass;         
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();            

            string pathEjecutable = Application.StartupPath + @"\Mysql\mysql.exe";
            string filename = Application.StartupPath + @"\Mysql\ncsoftwa_re_install.sql";
            process = new Process();
            process.StartInfo.FileName = pathEjecutable;
            args = "-C -B --host=" + server + " -P 3306 --user=" + user + " --password=" + pass + " -e \"\\. " + filename + "\"";
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

            filename = Application.StartupPath + @"\Mysql\dump_admin.sql";
            process = new Process();
            process.StartInfo.FileName = pathEjecutable;
            args = "-C -B --host=" + server + " -P 3306 --user=" + user + " --password=" + pass + " -e \"\\. " + filename + "\"";
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

        }

        public static void DumpDatos(string server, string user, string password, string database, string filename)
        {
            string path = Application.StartupPath + @"\Mysql\mysqldump.exe";
            Process process = new Process();
            process.StartInfo.FileName = path;
            string args = String.Format("-t --skip-comments -u {0} -p{1} -h {2} --opt {3} alicuotasiva articulos clientes formaspago generos razonsocial stock -r {4}",
                                           user, password, server, database, filename);
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        public static void DumpDB(string server, int port, string user, string password, string database, string filename)
        {
            string path = Application.StartupPath + @"\Mysql\mysqldump.exe";
            Process process = new Process();
            process.StartInfo.FileName = path;
            string args = String.Format("--skip-comments -u {0} -p{1} -h {2} --routines --opt {3} -r \"{4}\"",
                                           user, password, server, database, filename);
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        public static void RestoreDB(string server, int port, string user, string password, string database, string filename)
        {
            string path = Application.StartupPath + @"\Mysql\mysql.exe";
            Process process = new Process();
            process.StartInfo.FileName = path;
            string args = String.Format("-C -B --host={0} -P {1} --user={2} --password={3} --database={4} -e \"\\. {5}\"",
                                           server, port, user, password, database, filename);
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        public static void ZipDB(string filename)
        {
            string path = Application.StartupPath + @"\Mysql\xz.exe";
            Process process = new Process();
            process.StartInfo.FileName = path;
            string args = filename;
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        public static void UnzipDB(string filename)
        {
            string path = Application.StartupPath + @"\Mysql\xz.exe";
            Process process = new Process();
            process.StartInfo.FileName = path;
            string args = "-d " + filename;
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }
    }
}
