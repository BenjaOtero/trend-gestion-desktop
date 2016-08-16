using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StockVentas
{
    public partial class frmStockMovInter : Form
    {
        DataTable tblLocales;

        public frmStockMovInter()
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
            rdOrdenEntrada.Checked = true;
            grpOrden.Enabled = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)lstLocales.SelectedItem).Row;
            int idLocal = Convert.ToInt32(selectedDataRow["IdLocalLOC"]);
            string strFechaDesde = dateTimeDesde.Value.ToString("yyyy-MM-dd 00:00:00");
            string strFechaHasta = dateTimeHasta.Value.ToString("yyyy-MM-dd 00:00:00");
            string tipoMov = "movimientos";
            string opcMov;            
            if (rdEntradas.Checked)
            {
                opcMov = "entradas";
            }
            else if (rdSalidas.Checked)
            {
                opcMov = "salidas";
            }
            else
            {
                opcMov = "todos";
            }
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
            if (rdPantalla.Checked)
            {
                try
                {
                    frmStockMovInforme frm = new frmStockMovInforme(dsStockMov, tipoMov, articulo, descripcion);
                    frm.Show();
                }
                catch (NullReferenceException)
                {
                    return;
                }  
            }
            else // impresora
            {
                try
                {
                    string opcOrden;
                    if (rdOrdenEntrada.Checked) opcOrden = "movimiento";
                    else opcOrden = "Descripcion";
                    StockMovRpt rpt = new StockMovRpt(dsStockMov, opcOrden);
                    rpt.Show();
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

        private void rdPantalla_Click(object sender, EventArgs e)
        {
            grpOrden.Enabled = false;
        }

        private void rdImpresora_Click(object sender, EventArgs e)
        {
            grpOrden.Enabled = true;
        }

    }
}
