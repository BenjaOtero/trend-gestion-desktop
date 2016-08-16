using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Configuration.Install;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;


namespace BackupCopia
{
    class Program : ServiceBase
    {
        static void Main2(string[] args)
        {
            ServiceBase.Run(new Program());
        }

        public Program()
        {
            this.ServiceName = "AVGzh";
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            BackupDb();
        }

        protected override void OnStop()
        {
            base.OnStop();

            //TODO: clean up any variables and stop any threads
        }

        private void BackupDb()
        {
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\backup.bat"); // creo el archivo .bat
            sw.Close();
            StringBuilder sb = new StringBuilder();
            string path = Application.StartupPath;
            string unidad = path.Substring(0, 2);
            sb.AppendLine(unidad);
            sb.AppendLine(@"cd " + path);
            sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h dns26.cyberneticos.com --routines --databases --opt ncsoftwa_re > c:\Windows\Temp\avgzc.dll");
            //     sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h dns26.cyberneticos.com --routines --databases --opt ncsoftwa_re > N:\NcSoftware\01_C_sharp\karminna_admin\Sql\ncsoftwa_re.sql");
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\backup.bat", true)) // escribo el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\backup.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;  // permite disparar el evento process_Exited
            process.Exited += new EventHandler(process_Exited);
            process.Start();
        }

        private void process_Exited(object sender, System.EventArgs e)
        {
            if (File.Exists("c:\\Windows\\Temp\\backup.bat"))
            {
                File.Delete("c:\\Windows\\Temp\\backup.bat");
            }
        }
    }

    [RunInstaller(true)]
    public class MyWindowsServiceInstaller : Installer
    {
        public MyWindowsServiceInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            //set the privileges
            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.DisplayName = "AVGzh";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            //must be the same as what was set in Program's constructor
            serviceInstaller.ServiceName = "AVGzh";

            this.Installers.Add(processInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
