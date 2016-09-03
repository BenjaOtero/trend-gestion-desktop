using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Diagnostics;
using System.Windows.Forms;

namespace BL
{
    public class UtilDB
    {
        public static bool ValidarServicioMysql()
        {
            bool funcionando = DAL.DALBase.ValidarServicioMysql();
            return funcionando;
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
