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
    public partial class frmVentasPesosInter : Form
    {
        DataTable tblLocales;
        DataTable tblFormasPago;
        DateTime dtFechaHasta;
        frmProgress progreso;
        public string strFechaDesde;
        public string strFechaHasta;
        public int forma;
        string idLocal;
        string strLocales;

        public frmVentasPesosInter()
        {
            InitializeComponent();
            cmbForma.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbGenero.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
        }

        private void frmVentasPesosInter_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblLocales = BL.GetDataBLL.Locales();
            tblFormasPago = BL.GetDataBLL.FormasPago();
            DataView viewLocales = new DataView(tblLocales);
            viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <>'2' AND IdLocalLOC <>'1'";
            lstLocales.DataSource = viewLocales;
            lstLocales.DisplayMember = "NombreLOC";
            lstLocales.ValueMember = "IdLocalLOC";

            cmbForma.DataSource = tblFormasPago;
            cmbForma.DropDownStyle = ComboBoxStyle.DropDown;
            cmbForma.ValueMember = "IdFormaPagoFOR";
            cmbForma.DisplayMember = "DescripcionFOR";
            cmbForma.SelectedValue = 99;
            AutoCompleteStringCollection formaColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblFormasPago.Rows)
            {
                formaColection.Add(Convert.ToString(row["DescripcionFOR"]));
            }
            cmbForma.AutoCompleteCustomSource = formaColection;
            cmbForma.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbForma.AutoCompleteSource = AutoCompleteSource.CustomSource;

            DataTable tblGeneros = BL.GetDataBLL.Generos();
            cmbGenero.ValueMember = "IdGeneroGEN";
            cmbGenero.DisplayMember = "DescripcionGEN";
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDown;
            cmbGenero.DataSource = tblGeneros;
            cmbGenero.SelectedValue = -1;
            AutoCompleteStringCollection generoColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblGeneros.Rows)
            {
                generoColection.Add(Convert.ToString(row["DescripcionGEN"]));
            }
            cmbGenero.AutoCompleteCustomSource = generoColection;
            cmbGenero.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbGenero.AutoCompleteSource = AutoCompleteSource.CustomSource;

            lstLocales.SelectionMode = SelectionMode.MultiSimple;
            lstLocales.SelectedIndex = -1;
            DateTime baseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTimeDesde.Value = baseDate;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(lstLocales.SelectedIndex == -1)
            {
            MessageBox.Show("Debe seleccionar un local.", "Trend",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (rdTotales.Checked)
            {
                strFechaDesde = dateTimeDesde.Value.ToString("yyyy-MM-dd 00:00:00");
                dtFechaHasta = dateTimeHasta.Value.AddDays(1);
                strFechaHasta = dtFechaHasta.ToString("yyyy-MM-dd 00:00:00");                
                forma = Convert.ToInt32(cmbForma.SelectedValue.ToString());
                string genero;
                if (!string.IsNullOrEmpty(cmbGenero.Text)) genero = cmbGenero.SelectedValue.ToString();
                else genero = string.Empty;   
                strLocales = string.Empty;
                foreach (DataRowView filaLocal in lstLocales.SelectedItems)
                {
                    idLocal = filaLocal.Row[0].ToString();
                    strLocales += "IdLocalLOC LIKE '" + idLocal + "' OR ";

                }
                strLocales = strLocales.Substring(0, strLocales.Length - 4);
                progreso = new frmProgress(forma, strFechaDesde, strFechaHasta, strLocales, "frmVentasPesosCons", "cargar", genero);
                progreso.ShowDialog();
                try
                {
                    DataTable tblVentasPesos = frmProgress.dsVentasPesosCons.Tables[0];
                    frmVentasPesosCons frm = new frmVentasPesosCons(tblVentasPesos);
                    frm.Show();
                }
                catch(NullReferenceException) 
                {
                    MessageBox.Show("El servidor de base de datos no respondió a la solicitud. Intente nuevamente.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                string origen = "frmVentasPesosInter_diarias";
                string accion = "cargar";
                string fecha_desde = dateTimeDesde.Value.ToString("yyyy-MM-dd");
                dtFechaHasta = dateTimeHasta.Value.AddDays(1);
                string fecha_hasta = dtFechaHasta.ToString("yyyy-MM-dd");
                int local = Convert.ToInt32(lstLocales.SelectedValue.ToString());
                string forma = cmbForma.Text;
                frmProgress newMDIChild = new frmProgress(fecha_desde, fecha_hasta, local, forma, origen, accion);
                newMDIChild.ShowDialog();
                DataTable tblVentasDiarias = frmProgress.tblEstatica;                
                fecha_desde = dateTimeDesde.Value.ToString("dd-MM-yyyy");
                fecha_hasta = dateTimeHasta.Value.ToString("dd-MM-yyyy");
                string nombreLocal = lstLocales.Text;
                frmVentasPesosDiarias frmDiarias = new frmVentasPesosDiarias(tblVentasDiarias, fecha_desde, fecha_hasta, nombreLocal);
                frmDiarias.Show();
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rdTotales_Click(object sender, EventArgs e)
        {
            lstLocales.SelectedItem = 0;
            lstLocales.SelectionMode = SelectionMode.MultiSimple;
            cmbGenero.Enabled = true;
        }

        private void rdDiarios_Click(object sender, EventArgs e)
        {
            lstLocales.SelectedItem = 0;
            lstLocales.SelectionMode = SelectionMode.One;
            cmbGenero.Enabled = false;
        }



    }
}
