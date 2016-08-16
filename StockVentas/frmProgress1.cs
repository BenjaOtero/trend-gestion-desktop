using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;
using BL;


namespace StockVentas
{
    public partial class frmProgress1 : Form
    {
        private DataSet dataset;
        private string origen = null;
        private string accion = null;
        private string correo;
        private int? codigoError = null;
        private const int CP_NOCLOSE_BUTTON = 0x200;  //junto con protected override CreateParams inhabilitan el boton cerrar de frmProgress

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmProgress1()
        {
            InitializeComponent();
        }

        public frmProgress1(DataSet dataset, string origen, string accion, string correo)
            : this()
        {
            this.dataset = dataset;
            this.origen = origen;
            this.accion = accion;
            this.correo = correo;
        }

        private void frmProgress_Shown(object sender, EventArgs e)
        {
            if (accion == "cargar")
            {
                label1.Text = "Cargando datos...";
                label1.Left = 108;
            }
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                switch (origen)
                {
                    case "frmGetDatosCliente1":
                        //   BL.TrendBLL.GrabarDB(dataset);
                        // crear base de datos
                            string path = Application.StartupPath;
                            using (StreamWriter fileWrite = new StreamWriter(path + "\\Backup\\temp.sql"))
                            {
                                using (StreamReader fielRead = new StreamReader(path + "\\Backup\\db_base.sql"))
                                {
                                    String line;

                                    while ((line = fielRead.ReadLine()) != null)
                                    {
                                        if (line.Contains("db_base"))
                                        {
                                            string newLine = line.Replace("db_base", correo);
                                            fileWrite.WriteLine(newLine);
                                        }
                                        else
                                            fileWrite.WriteLine(line);
                                    }
                                }       
                            }
                        break;
                }
            }
            catch (MySqlException ex)
            {
                codigoError = ex.Number;
            }
            catch (TimeoutException)
            {
                codigoError = 8888;
            }
            catch (Exception)
            {
                codigoError = 9999;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (codigoError == 1042) //Unable to connect to any of the specified MySQL hosts.
            {
                this.Visible = false;
                MessageBox.Show("No se pudo establecer la conexión con el servidor (verifique la conexión a internet).",
                        "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else if (codigoError == 8888) //TimeOutException
            {
                this.Visible = false;
                MessageBox.Show("Se excedió el tiempo de espera para la consulta al servidor.",
                    "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else if (codigoError == 9999)
            {
                this.Visible = false;
                MessageBox.Show("Se produjo un error inesperado.",
                    "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

    }
}
