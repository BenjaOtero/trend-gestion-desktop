using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class frmStockCompInter : Form
    {
        DataTable tblLocales;

        public frmStockCompInter()
        {
            InitializeComponent();
        }

        private void frmStockMovInter_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblLocales = BL.GetDataBLL.Locales();
            DataView viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <>'2' AND IdLocalLOC <>'1'";
            lstLocales.DataSource = viewLocales;
            lstLocales.DisplayMember = "NombreLOC";
            lstLocales.ValueMember = "IdLocalLOC";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!BL.UtilDB.ValidarServicioMysql())
            {
                MessageBox.Show("No se pudo conectar con el servidor de base de datos."
                        + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            DataRow selectedDataRow = ((DataRowView)lstLocales.SelectedItem).Row;
            int idLocal = Convert.ToInt32(selectedDataRow["IdLocalLOC"]);
            string strFechaDesde = dateTimeDesde.Value.ToString("yyyy-MM-dd 00:00:00");
            string strFechaHasta = dateTimeHasta.Value.ToString("yyyy-MM-dd 00:00:00");
            if (rdDetalle.Checked == true) //informe detalle
            {
                try
                {
                    string tipoMov = "compensaciones";
                    string opcMov = "entradas";
                    string articulo = string.Empty;
                    string descripcion = string.Empty;
                    if (rdArticulo.Checked) articulo = txtParametros.Text;
                    else descripcion = txtParametros.Text;
                    string formularioOrigen = "frmStockMovInforme";
                    string accionProgress = "cargar";
                    frmProgress progreso = new frmProgress(strFechaDesde, strFechaHasta, idLocal, tipoMov, opcMov, formularioOrigen, accionProgress, 
                        articulo, descripcion);
                    progreso.ShowDialog();
                    DataSet dsStockMov = frmProgress.dsStockMovCons;
                        frmStockMovInforme frm = new frmStockMovInforme(dsStockMov, tipoMov, articulo, descripcion);
                        frm.Show();
                }
                catch (NullReferenceException)
                {
                    return;
                }  
            }
            else // informe en pesos
            {
                try
                {
                    frmProgress frm = new frmProgress(strFechaDesde, strFechaHasta, idLocal, "frmStockCompPesos", "cargar");
                    frm.ShowDialog();
                    DataTable tblStockCompPesos = frmProgress.tblEstatica;
                    strFechaDesde = dateTimeDesde.Value.ToShortDateString();
                    strFechaHasta = dateTimeHasta.Value.ToShortDateString();
                    StockCompPesosRpt frmStockComp = new StockCompPesosRpt(tblStockCompPesos, strFechaDesde, strFechaHasta);
                    frmStockComp.Show();
                }
                catch (NullReferenceException)
                {
                    return;
                }             
            }         

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            tblLocales.RejectChanges();
            Close();
        }

        private void rdPesos_Click(object sender, EventArgs e)
        {
            grpBuscarPor.Enabled = false;
            txtParametros.Enabled = false;
            txtParametros.Text = string.Empty;
        }

        private void rdDetalle_Click(object sender, EventArgs e)
        {
            grpBuscarPor.Enabled = true;
            txtParametros.Enabled = true;
        }

    }
}
