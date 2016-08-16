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
    public partial class frmVentasDetalleInter : Form
    {

        public frmVentasDetalleInter()
        {
            InitializeComponent();
            cmbForma.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
        }

        private void frmVentasPesosInter_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            DataTable tblLocales = BL.GetDataBLL.Locales();
            DataTable tblFormasPago = BL.GetDataBLL.FormasPago();
            DataView viewLocales = new DataView(tblLocales);
            viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <>'2' AND IdLocalLOC <>'1'";
            lstLocales.DataSource = viewLocales;
            lstLocales.DisplayMember = "NombreLOC";
            lstLocales.ValueMember = "IdLocalLOC";
            tblFormasPago = BL.GetDataBLL.FormasPago();
            cmbForma.ValueMember = "IdFormaPagoFOR";
            cmbForma.DisplayMember = "DescripcionFOR";
            cmbForma.DropDownStyle = ComboBoxStyle.DropDown;
            cmbForma.DataSource = tblFormasPago;
            cmbForma.SelectedValue = -1;
            cmbForma.BackColor = Color.White;
            AutoCompleteStringCollection formasPagoColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblFormasPago.Rows)
            {
                formasPagoColection.Add(Convert.ToString(row["DescripcionFOR"]));
            }
            cmbForma.AutoCompleteCustomSource = formasPagoColection;
            cmbForma.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbForma.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbForma.SelectedValue = 99;
            lstLocales.SelectionMode = SelectionMode.One;
            lstLocales.SelectedIndex = -1;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lstLocales.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un local.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(cmbForma.Text))
            {
                MessageBox.Show("Debe seleccionar una forma de pago.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int forma = Convert.ToInt32(cmbForma.SelectedValue.ToString());
                string strDesde = dateTimeDesde.Value.ToString("yyyy-MM-dd 00:00:00");
                DateTime hasta = dateTimeHasta.Value;
                string strHasta = hasta.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
                int idLocal = Convert.ToInt32(lstLocales.SelectedValue.ToString());
                string parametros = txtParametros.Text;
                frmProgress frm = new frmProgress(forma, strDesde, strHasta, idLocal, "frmVentasDetalleInter", "cargar", parametros);
                frm.ShowDialog();
                DataTable tblVentasDetalleCons = frmProgress.tblEstatica;
                string nombreLocal = lstLocales.Text;
                VentasDetalleRpt ventas = new VentasDetalleRpt(tblVentasDetalleCons, nombreLocal);
                ventas.Show();
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
