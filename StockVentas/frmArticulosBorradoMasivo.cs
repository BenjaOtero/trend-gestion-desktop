using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class frmArticulosBorradoMasivo : Form
    {
        DataTable tblPedidos;
        string strFecha;

        public frmArticulosBorradoMasivo()
        {
            InitializeComponent();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.Text = "  Borrado de artículos sin stock";
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            cmbGenero.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbGenero.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            DataTable tblGeneros = BL.GetDataBLL.Generos();
            cmbGenero.ValueMember = "IdGeneroGEN";
            cmbGenero.DisplayMember = "DescripcionGEN";
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDown;
            cmbGenero.DataSource = tblGeneros;
            cmbGenero.SelectedValue = -1;
            AutoCompleteStringCollection generosColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblGeneros.Rows)
            {
                generosColection.Add(Convert.ToString(row["DescripcionGEN"]));
            }
            cmbGenero.AutoCompleteCustomSource = generosColection;
            cmbGenero.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbGenero.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cmbGenero.Text)){
                MessageBox.Show("Debe indicar un género.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbGenero.Focus();
            }
                
            try
            {
                strFecha = dateTimeDesde.Value.ToString("yyyy-MM-dd");
                string genero = cmbGenero.SelectedValue.ToString();
                frmProgress frm = new frmProgress(strFecha, "frmArticulosBorradoMasivo", "cargar", genero);
                frm.ShowDialog();
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

    }
}
