using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Timers;
using System.Configuration;
using BL;
using DAL;

namespace StockVentas
{

    public partial class frmInicio : Form
    {
        BackgroundWorker bckIniciarComponetes;
        bool existeServicio;
        Label label1;
        public static DataSet ds;
        public static DataTable tblArticulos;
        public static DataTable tblArticulosCons;
        bool seExportaronDatos = false;

        public frmInicio()
        {
            InitializeComponent();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
          /*  Configuration cm = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection cs = cm.GetSection("connectionStrings");
            if (cs != null)
            {
                if (!cs.SectionInformation.IsProtected)
                {
                    cs.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    cs.SectionInformation.ForceSave = true;
                    cm.Save(ConfigurationSaveMode.Full);
                }
            }*/
        }

        private void frmInicio_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            Control.CheckForIllegalCrossThreadCalls = false; // permite asignar un valor a label1.text en un subproceso diferente al principal
            label1 = new Label();            
            label1.Location = new System.Drawing.Point(28, 190);
            label1.AutoSize = true;
            Controls.Add(label1);
        /*    string cs = ConnectionStringManager.GetFirstConnectionString();
            string db = ConnectionStringManager.GetDatabaseName();
            if (cs == "nuevo_cliente")
            {
                label1.Text = "Iniciando Trend Gestión por primera vez. . .";
                frmGetDatosCliente frmDatos = new frmGetDatosCliente();
                frmDatos.ShowDialog();
            }
            else
            {
                this.Visible = true;
                label1.Text = "Comprobando conexión de red. . .";
                bckIniciarComponetes = new BackgroundWorker();
                bckIniciarComponetes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckIniciarComponetes_DoWork);
                bckIniciarComponetes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckIniciarComponetes_RunWorkerCompleted);
                bckIniciarComponetes.RunWorkerAsync();
            }*/
            // despues de descomentar lo de arriba borrar lo siguiente
            this.Visible = true;
            bckIniciarComponetes = new BackgroundWorker();
            bckIniciarComponetes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckIniciarComponetes_DoWork);
            bckIniciarComponetes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckIniciarComponetes_RunWorkerCompleted);
            bckIniciarComponetes.RunWorkerAsync();
            /////////////////////////////////
        }

        private void bckIniciarComponetes_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!ExisteServicioMySQL())
            {
                label1.Text = "Configurando servidor de base de datos . . .";
                ConfigurarMySQL();
            }
            label1.Text = "Obteniendo datos del servidor . . .";
        reiniciar:
            try
            {
                ds = BL.GetDataBLL.GetData();                
                string idRazonSocial = BL.GetDataBLL.RazonSocial().Rows[0][0].ToString();
                Mantenimiento();
                label1.Text = "Exportando datos . . .";
                BL.DatosBLL.ExportarDatos();
                label1.Text = "Importando datos . . .";
                BL.DatosBLL.GetDataPOS();
            }
            catch (ServidorMysqlInaccesibleException ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Visible = false;
                    MessageBox.Show(ex.Message, "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
                System.Environment.Exit(1);
            }
            catch (System.TimeoutException)
            {
                this.Invoke((Action)delegate
                {
                    this.Visible = false;
                    MessageBox.Show("El tiempo de conexión con el servidor ha expirado. Intente nuevamente.", "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Environment.Exit(1);
                });
            }
            catch (IndexOutOfRangeException) // se produce la excepción si no existe el registro en la tabla. Lo agrego 
            {
                try
                {
                    DataTable tblRazon = BL.GetDataBLL.RazonSocial();
                    DataRow row = tblRazon.NewRow();
                    Random rand = new Random();
                    int clave = rand.Next(147483647, 2147483647);
                    row["IdRazonSocialRAZ"] = clave;
                    row["RazonSocialRAZ"] = "";
                    row["NombreFantasiaRAZ"] = "";
                    row["DomicilioRAZ"] = "";
                    row["LocalidadRAZ"] = "";
                    row["ProvinciaRAZ"] = "";
                    row["IdCondicionIvaRAZ"] = DBNull.Value;
                    row["CuitRAZ"] = "";
                    row["IngresosBrutosRAZ"] = "";
                    row["InicioActividadRAZ"] = DBNull.Value;
                    tblRazon.Rows.Add(row);
                    BL.RazonSocialBLL.GrabarDB(tblRazon);
                }
                catch (ServidorMysqlInaccesibleException ex)
                {
                    this.Invoke((Action)delegate
                    {
                        this.Visible = false;
                        MessageBox.Show(ex.Message, "Trend Gestión",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                    System.Environment.Exit(1);
                }
                goto reiniciar;
            }
            catch (WebException)
            {
                this.Invoke((Action)delegate
                {
                    this.Visible = false;
                    MessageBox.Show("No se pudo establecer conexión con el servidor remoto. No se actualizaron los datos.", "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            catch (Exception ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Visible = false;
                    string error = ex.Message;
                    MessageBox.Show(error, "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void bckIniciarComponetes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Visible = false;
            frmPrincipal principal = new frmPrincipal();
            principal.Show();
        }

        private void Mantenimiento()
        {
            BL.MantenimientoBLL.Mantenimiento();
            BL.VentasBLL.VentasHistoricasMantener();
        }

        private bool ExisteServicioMySQL()
        {
            existeServicio = false;
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

        private void ConfigurarMySQL()
        {
            if (File.Exists("c:\\Windows\\Temp\\config_mysql.bat")) File.Delete("c:\\Windows\\Temp\\config_mysql.bat");
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\config_mysql.bat"); // creo el archivo .bat
            sw.Close();
            string programFiles;
            if (Directory.Exists(@"C:\Program files"))
            {
                programFiles = "Program files";
            }
            else if (Directory.Exists(@"C:\Archivos de programa"))
            {
                programFiles = "Archivos de programa";
            }
            else if (Directory.Exists(@"C:\Program files(x86)"))
            {
                programFiles = "Program files(x86)";
            }
            else
            {
                programFiles = "Archivos de programa(x86)";
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("C:");
            string configMysql = "cd " + "\"C:\\" + programFiles + "\\MySQL\\MySQL Server 5.5\\bin\"";
            sb.AppendLine(configMysql);
            configMysql = "mysqlinstanceconfig.exe -i -q ServiceName=MySQL ServerType=DEVELOPER DatabaseType=INODB Port=3306 Charset=utf8 RootPassword=8953#AFjn";
//            configMysql = "mysqlinstanceconfig.exe -i -q ServiceName=MySQL root Password=8953#AFjn ServerType=DEVELOPER DatabaseType=INODB Port=myport Charset=utf8";
            sb.AppendLine(configMysql);
            string usuario = "mysql.exe -u root -p8953#AFjn -e \"GRANT ALL ON *.* TO 'ncsoftwa_re'@'%' IDENTIFIED BY '8953#AFjn' WITH GRANT OPTION; FLUSH PRIVILEGES;\"";
            sb.AppendLine(usuario);
            string rutaDB = Application.StartupPath.ToString() + @"\MySql\ncsoftwa_re.sql";
            string restaurarDB = "mysql.exe -u ncsoftwa_re -p8953#AFjn < \"" + rutaDB + "\"";
            sb.AppendLine(restaurarDB);
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\config_mysql.bat", true)) // escribo en el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\config_mysql.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.EnableRaisingEvents = true;
            process.WaitForExit();
            StringBuilder sb_myIni = new StringBuilder();
            sb_myIni.AppendLine("");
            sb_myIni.AppendLine("[mysqld]");
            sb_myIni.AppendLine("lower_case_table_names = 0");
            using (StreamWriter file = new StreamWriter("C:\\" + programFiles + "\\MySQL\\MySQL Server 5.5\\my.ini", true))
            {
                file.Write(sb_myIni.ToString());
            }
        }

        private void frmInicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (seExportaronDatos) return;
            try
            {
                if (BL.RazonSocialBLL.GetActualizarDatos())
                {
                    e.Cancel = true;
                    this.Visible = true;
                    label1.Text = "Exportando datos. . .";
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (ServidorMysqlInaccesibleException ex)
            {
                this.Invoke((Action)delegate
                {
                    MessageBox.Show(ex.Message, "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
                System.Environment.Exit(1);
            }            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {            
            try
            {
                BL.DatosBLL.ExportarDatos();
            }
            catch (ServidorMysqlInaccesibleException ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Visible = false;
                    MessageBox.Show(ex.Message, "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
                System.Environment.Exit(1);
            }
            catch (WebException)
            {
                this.Invoke((Action)delegate
                {
                    this.Visible = false;
                    MessageBox.Show("No se pudo establecer conexión con el servidor remoto. No se exportaron los datos.", "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Environment.Exit(1);  // cierro la aplicacion así no ejecuta backgroundWorker1_RunWorkerCompleted
                });
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            try
            {
                BL.RazonSocialBLL.SetActualizarDatos();
            }
            catch (ServidorMysqlInaccesibleException ex)
            {
                System.Environment.Exit(1);
            }
            seExportaronDatos = true;
            System.Environment.Exit(1);
        }

        public static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
               // result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

    }

}
