using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using BL;
using System.Net;
using System.Xml;
using System.Timers;
using System.Threading;


namespace StockVentas
{
    public partial class frmPrincipal : Form
    {
        public frmProgress progreso;
        string origen, accion;
        string idRazonSocial;
        private System.Timers.Timer tmrSilenceBck;
        string fileSilenceBck;
        System.Windows.Forms.Timer tmrPopup = new System.Windows.Forms.Timer();
        private static System.Timers.Timer timer;

        public frmPrincipal()
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {
                    client.BackColor = this.BackColor;
                    break;
                }
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            tmrPopup.Tick += new EventHandler(Popup);
            tmrPopup.Interval = 3000;
            tmrPopup.Start();
            tmrSilenceBck = new System.Timers.Timer(1000);
            tmrSilenceBck.Elapsed += new ElapsedEventHandler(SilenceBackup);
            tmrSilenceBck.AutoReset = false;
            tmrSilenceBck.Enabled = true;
         //   alícuotasIVAToolStripMenuItem.Visible = false;
            //   condiciónIVAToolStripMenuItem.Visible = false;
            //   empleadosToolStripMenuItem.Visible = false;
            //  movimientosDeEmpleadosToolStripMenuItem.Visible = false;
            //    empleadosToolStripMenuItem2.Visible = false;
            //    actualizarServidorToolStripMenuItem.Visible = false;
            //     pruebasToolStripMenuItem.Visible = false;
            //     exportarDatosToolStripMenuItem.Visible = false;
            //     borradoMasivoArtículosToolStripMenuItem.Visible = false;                                    
        }

        private void alícuotasIVAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlicuotasIva newMDIChild = new frmAlicuotasIva();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            origen = "frmArticulos";
            accion = "cargar";
            frmArticulos newMDIChild = new frmArticulos();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void artículosItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosItems newMDIChild = new frmArticulosItems();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes newMDIChild = new frmClientes();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void coloresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmColores newMDIChild = new frmColores();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void condiciónIVAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCondicionIva newMDIChild = new frmCondicionIva();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void datosComercialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRazonSocial newMDIChild = new frmRazonSocial();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void empleadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEmpleados newMDIChild = new frmEmpleados();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void tipoMovimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpleadosMovTipo newMDIChild = new frmEmpleadosMovTipo();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void formasDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFormasPago newMDIChild = new frmFormasPago();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void génerosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGeneros newMDIChild = new frmGeneros();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void localesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocales newMDIChild = new frmLocales();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProveedores newMDIChild = new frmProveedores();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockMov newMDIChild = new frmStockMov();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void StockEntradas_Click(object sender, EventArgs e)
        {
            frmStockEntradas newMDIChild = new frmStockEntradas();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void StockCompensaciones_Click(object sender, EventArgs e)
        {
            frmStockComp newMDIChild = new frmStockComp();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVentas newMDIChild = new frmVentas();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void movimientosDeEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpleadosMov newMDIChild = new frmEmpleadosMov();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void movimientosDeTesoreríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmTesoreriaMov
            //frmTesoreriaBinding
            frmTesoreriaMov newMDIChild = new frmTesoreriaMov();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void movimientosEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpleadosMovConsInter newMDIChild = new frmEmpleadosMovConsInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void liquidaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            HaberesRpt newMDIChild = new HaberesRpt();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void emailMarketingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMarketingEmail newMDIChild = new frmMarketingEmail();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void fondoDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFondoCaja newMDIChild = new frmFondoCaja();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void stockDeArtículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockInter newMDIChild = new frmStockInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void movimientosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmStockMovInter newMDIChild = new frmStockMovInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void compensacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmStockCompInter newMDIChild = new frmStockCompInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void arqueoDeCajaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmArqueoInter newMDIChild = new frmArqueoInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void ventasEnPesos_Click(object sender, EventArgs e)
        {
            frmVentasPesosInter newMDIChild = new frmVentasPesosInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void ventasDetalle_Click(object sender, EventArgs e)
        {
           frmVentasDetalleInter newMDIChild = new frmVentasDetalleInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void ventasHistoricas_Click(object sender, EventArgs e)
        {
            frmVentasHistoricasInter newMDIChild = new frmVentasHistoricasInter();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void fondosDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            origen = "frmFondoCajaCons";
            accion = "cargar";
            frmProgress newMDIChild = new frmProgress(origen, accion);
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void pedidos_Click(object sender, EventArgs e)
        {
            frmPedido newMDIChild = new frmPedido();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void actualizarServidorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        /*   frmActualizarStock newMDIChild = new frmActualizarStock();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();*/
        }

        private void backup_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void processBackupExited(object sender, System.EventArgs e)
        {
            if (File.Exists("c:\\Windows\\Temp\\backup.bat"))
            {
                File.Delete("c:\\Windows\\Temp\\backup.bat");
            }            
        }

        private void pruebasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPruebas newMDIChild = new frmPruebas();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void Backup()
        {            
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "SQL (*.sql)|*.sql";
            fichero.FileName = "Backup";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\backup.bat"); // creo el archivo .bat
                sw.Close();
                StringBuilder sb = new StringBuilder();
                string path = Application.StartupPath;
                string unidad = path.Substring(0, 2);
                sb.AppendLine(unidad);
                sb.AppendLine(@"cd " + path + @"\Backup");
                //  sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h ns21a.cyberneticos.com --opt ncsoftwa_re > " + fichero.FileName);
                sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h localhost --routines --opt ncsoftwa_re > " + fichero.FileName);              
                //mysqldump -u... -p... mydb t1 t2 t3 > mydb_tables.sql
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
                process.WaitForExit();

            }
        }

        private void process_Exited(object sender, System.EventArgs e)
        {
            if (File.Exists("c:\\Windows\\Temp\\backup.bat")) File.Delete("c:\\Windows\\Temp\\backup.bat");
            Cursor = Cursors.Arrow;
        }

        private void Popup(Object myObject, EventArgs myEventArgs)
        {
            tmrPopup.Stop();
            DataTable tbl = BL.GetDataBLL.RazonSocial();
            int razon = Convert.ToInt32(tbl.Rows[0][0].ToString());
            DataSet ds = BL.TrendBLL.GetDataPopup(razon);
            DataTable tblProductos = ds.Tables[0];
            if (tblProductos.Rows.Count == 0) return;
            DataTable tblProductos_users = ds.Tables[1]; 
            DataTable tblProductos_top = ds.Tables[2];
            DataTable tblPromocionarProducto = new DataTable();
            int producto_top = Convert.ToInt32(tblProductos_top.Rows[0][0].ToString());
            if (producto_top == 0)
            {
                tblPromocionarProducto.Columns.Add("id");
                DataRow[] foundRow;
                foreach (DataRow rowProducto in tblProductos.Rows)
                {
                    foundRow = tblProductos_users.Select("Producto_id_PRUS = " + rowProducto["Producto_id_PRD"]);
                    if (foundRow.Count() == 0)
                    {
                        DataRow row = tblPromocionarProducto.NewRow();
                        row["id"] = rowProducto["Producto_id_PRD"];
                        tblPromocionarProducto.Rows.Add(row);
                    }
                }
                int filas = tblPromocionarProducto.Rows.Count - 1;
                Random rand = new Random();
                int fila = rand.Next(0, filas);
                DataRow rowPromocionable = tblPromocionarProducto.Rows[fila];
                int idPromocionable = Convert.ToInt32(rowPromocionable[0].ToString());
                DataRow[] rowProductoElegido = tblProductos.Select("Producto_id_PRD = " + idPromocionable);
                byte[] imgBytes = (byte[])rowProductoElegido[0]["Imagen_PRD"];
                string url = rowProductoElegido[0]["Url"].ToString();
                frmPopupTrend frm = new frmPopupTrend(imgBytes, url);
                frm.Show();
            }
            else
            {
                DataRow[] rowProductoElegido = tblProductos.Select("Producto_id_PRD = " + producto_top);
                byte[] imgBytes = (byte[])rowProductoElegido[0]["Imagen_PRD"];
                string url = rowProductoElegido[0]["Url"].ToString();
                frmPopupTrend frm = new frmPopupTrend(imgBytes, url);
                frm.Show();
            }
            tmrPopup.Enabled = false;
        }

        private void SilenceBackup(object source, ElapsedEventArgs e)
        {
            DataTable tbl = BL.GetDataBLL.RazonSocial();
            fileSilenceBck = tbl.Rows[0][0].ToString() + "_bck.sql.gz";
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\backup.bat"); // creo el archivo .bat
            sw.Close();
            StringBuilder sb = new StringBuilder();
            string path = Application.StartupPath;
            string unidad = path.Substring(0, 2);
            sb.AppendLine(unidad);
            sb.AppendLine(@"cd " + path + @"\Backup");
            //  sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h ns21a.cyberneticos.com --opt ncsoftwa_re > " + fichero.FileName);
            sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h localhost --routines --opt ncsoftwa_re | gzip > c:\windows\temp\" + fileSilenceBck);
            //mysqldump -u... -p... mydb t1 t2 t3 > mydb_tables.sql
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\backup.bat", true)) // escribo el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\backup.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;  // permite disparar el evento process_Exited
            process.Exited += new EventHandler(process_Exited_silence);
            process.Start();
            process.WaitForExit();

        }

        private void process_Exited_silence(object sender, System.EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            using (FileStream fs = File.OpenRead(@"c:\windows\temp\" + fileSilenceBck))
            {
                fs.CopyTo(ms);
            }
            BL.Utilitarios.UploadFromMemoryStream(ms, fileSilenceBck, "trendsistemas");
            if (File.Exists("c:\\Windows\\Temp\\backup.bat")) File.Delete("c:\\Windows\\Temp\\backup.bat");
        }

        private void exportarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProgress frm = new frmProgress("ExportarDatos", "grabar");
            frm.ShowDialog();
        }

        private void borradoMasivoArtículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosBorradoMasivo newMDIChild = new frmArticulosBorradoMasivo();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void empleadosToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BL.Utilitarios.HayInternet())
            {
                frmProgress frm = new frmProgress("ImportarDatos", "grabar");
                frm.ShowDialog();

            }
            else
                MessageBox.Show("Verifique la conexión a internet. No se importaron datos.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RestaurarDatos(string archivo)
        {
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\datos\\restore.bat"); // creo el archivo .bat
            sw.Close();
            StringBuilder sb = new StringBuilder();
            string path = Application.StartupPath;
            string unidad = path.Substring(0, 2);
            sb.AppendLine(unidad);
            sb.AppendLine(@"cd " + path + @"\Mysql");
            sb.AppendLine(@"gzip -d " + archivo);
            archivo = archivo.Substring(0, archivo.Length - 3);
            sb.AppendLine(@"mysql -u ncsoftwa_re -p8953#AFjn ncsoftwa_re < " + archivo);
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\datos\\restore.bat", true)) // escribo el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\datos\\restore.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;  // permite disparar el evento process_Exited
            process.Exited += new EventHandler(RestaurarDatos_Exited);
            process.Start();
            process.WaitForExit();
        }

        private void RestaurarDatos_Exited(object sender, System.EventArgs e)
        {
            if (File.Exists("c:\\Windows\\Temp\\datos\\restore.bat")) File.Delete("c:\\Windows\\Temp\\datos\\restore.bat");
            if (File.Exists("c:\\Windows\\Temp\\datos.sql")) File.Delete("c:\\Windows\\Temp\\datos.sql");
            if (File.Exists("c:\\Windows\\Temp\\datos.sql.gz")) File.Delete("c:\\Windows\\Temp\\datos.sql.gz");
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  if (MessageBox.Show("¿Realiza copia de seguridad de los datos?", "Trend", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    Backup();
            Application.Exit();
        }

        private void restaurarBaseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rutaDB;
            OpenFileDialog opFilDlg = new OpenFileDialog();
            opFilDlg.Filter = "SQL (*.sql)|*.sql";
            if (opFilDlg.ShowDialog() == DialogResult.OK) rutaDB = opFilDlg.FileName;
            else return;
            Cursor.Current = Cursors.WaitCursor;
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\restore_db.bat"); // creo el archivo .bat
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
          //  string rutaDB = Application.StartupPath.ToString() + @"\MySql\ncsoftwa_re.sql";
            string restaurarDB = "mysql.exe -u ncsoftwa_re -p8953#AFjn ncsoftwa_re < \"" + rutaDB + "\"";
            sb.AppendLine(restaurarDB);
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\restore_db.bat", true)) // escribo en el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\restore_db.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.EnableRaisingEvents = true;
            process.WaitForExit();
            Application.Restart();
        }

    }
}
