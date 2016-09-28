using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using BL;
using System.Net;
using System.Timers;
using System.Diagnostics;

namespace StockVentas
{
    public partial class frmPrincipal : Form
    {
        public frmProgress progreso;
        string origen, accion;
        private System.Timers.Timer tmrSilenceBck;
        System.Windows.Forms.Timer tmrPopup = new System.Windows.Forms.Timer();

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
            List<String> credentials = UtilVarios.GetCredentialsFTP();
            string server = credentials[0];
            string user = credentials[1];
            string pass = credentials[2];
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
            if (!BL.UtilDB.ValidarServicioMysql())
            {
                MessageBox.Show("No se pudo conectar con el servidor de base de datos."
                        + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
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
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "SQL (*.sql)|*.sql";
            fichero.FileName = "Backup";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                List<string> credentials = UtilVarios.GetCredentialsDB();
                string server = credentials[0];
                string user = credentials[1];
                string database = credentials[2];
                string pass = credentials[3];
                UtilDB.DumpDB(server, 3306, user, pass, database, fichero.FileName);
            }
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

            List<string> credentials = UtilVarios.GetCredentialsDB();
            string server = credentials[0];
            string user = credentials[1];
            string database = credentials[2];
            string pass = credentials[3];
            DataTable tbl = BL.GetDataBLL.RazonSocial();
            string fileSilenceBck = @"c:\windows\temp\" + tbl.Rows[0][0].ToString() + "_bck.sql";
            string remoteFile = tbl.Rows[0][0].ToString() + "_bck.sql.xz";
            UtilDB.DumpDB(server, 3306, user, pass, database, fileSilenceBck);
            if (File.Exists(fileSilenceBck + ".xz")) File.Delete(fileSilenceBck + ".xz");
            UtilDB.ZipDB(fileSilenceBck);
            MemoryStream ms = new MemoryStream();
            using (FileStream fs = File.OpenRead(fileSilenceBck + ".xz"))
            {
                fs.CopyTo(ms);
            }
            try
            {
                BL.UtilFTP.UploadFromMemoryStream(ms, remoteFile, "trendsistemas");
            }
            catch (WebException)
            {
                return;
            }
        }

        private void exportarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!BL.UtilDB.ValidarServicioMysql())
            {
                MessageBox.Show("No se pudo conectar con el servidor de base de datos."
                        + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            frmProgress frm = new frmProgress("ExportarDatos", "grabar");
            try
            {
                frm.ShowDialog();
            }
            catch (WebException)
            {
                MessageBox.Show("No se pudo establecer conexión con el servidor remoto. No se exportaron los datos.", "Trend Gestión",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void borradoMasivoArtículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosBorradoMasivo newMDIChild = new frmArticulosBorradoMasivo();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!BL.UtilDB.ValidarServicioMysql())
            {
                MessageBox.Show("No se pudo conectar con el servidor de base de datos."
                        + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            frmProgress frm = new frmProgress("ImportarDatos", "grabar", false);
            try
            {
                frm.ShowDialog();
            }
            catch (WebException)
            {
                MessageBox.Show("No se pudo establecer conexión con el servidor remoto. No se actualizaron los datos.", "Trend Gestión",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                
        }

        private void importarDatosActualesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!BL.UtilDB.ValidarServicioMysql())
            {
                MessageBox.Show("No se pudo conectar con el servidor de base de datos."
                        + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            frmProgress frm = new frmProgress("ImportarDatos", "grabar", true);
            try
            {
                frm.ShowDialog();
            }
            catch (WebException)
            {
                MessageBox.Show("No se pudo establecer conexión con el servidor remoto. No se actualizaron los datos.", "Trend Gestión",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, @"N:\NcSoftware\02_Access\HelpNc\NcPunto.chm");

        }

        private void ayudaEnLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p = Process.Start(@"C:\Trend\trend-gestion-desktop\StockVentas\bin\Debug\Soporte\TeamViewer11.exe");

        }

        private void restaurarBaseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName;
            OpenFileDialog opFilDlg = new OpenFileDialog();
            opFilDlg.Filter = "SQL (*.sql)|*.sql";
            if (opFilDlg.ShowDialog() == DialogResult.OK) fileName = opFilDlg.FileName;
            else return;
            Cursor.Current = Cursors.WaitCursor;
            List<string> credentials = UtilVarios.GetCredentialsDB();
            string server = credentials[0];
            string user = credentials[1];
            string database = credentials[2];
            string pass = credentials[3];
            BL.UtilDB.RestoreDB(server, 3306, user, pass, database, fileName);
            Application.Restart();
        }        


    }
}
