using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Net;
using BL;
using DAL;

namespace StockVentas
{

    public partial class frmInicio : Form
    {
        BackgroundWorker bckIniciarComponetes;
        Label label2;
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
         /*   Configuration cm = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
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
            label2 = new Label();
            label2.Location = new System.Drawing.Point(28, 140);
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.AutoSize = true;
            Controls.Add(label2);
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
            if (!UtilVarios.ExisteServicio("MySQL"))
            {
                this.pictureBox2.Location = new System.Drawing.Point(29, 55);
                label2.Text = "Iniciando el sistema por primera vez." + '\r' + "Este proceso puede tomar unos minutos.";
             //   label1.Text = "Instalando servidor de base de datos . . .";
              //  UtilDB.InstalarMySQL();
                label1.Text = "Configurando servidor de base de datos . . .";
                UtilDB.ConfigurarMySQL();
            }
            label1.Text = "Obteniendo datos del servidor . . .";
        reiniciar:
            try
            {                
                ds = BL.GetDataBLL.GetData();                
                string idRazonSocial = BL.GetDataBLL.RazonSocial().Rows[0][0].ToString();
                BL.MantenimientoBLL.Mantenimiento();
                BL.VentasBLL.VentasHistoricasMantener();
                label1.Text = "Importando datos . . .";
           //     BL.DatosBLL.GetDataPOS(false);  // false no importa los datos de hoy
                label1.Text = "Exportando datos . . .";
             //   BL.DatosBLL.ExportarDatos();
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
            catch (ServidorMysqlInaccesibleException)
            {
                System.Environment.Exit(1);
            }
            seExportaronDatos = true;
            System.Environment.Exit(1);
        }

    }

}
