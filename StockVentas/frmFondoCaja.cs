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
    public partial class frmFondoCaja : Form
    {
        private DataTable tblFondoCaja;
        DataTable tblLocales;
        DataTable tblPcs;
        DataView viewFondoCaja;
        DataView viewLocal;
        DataView viewPc; 
        DataRowView rowView;
        public int PK;
        public DateTime fecha;
        public int idPc;
        public int idLocal;       
        

        

        public frmFondoCaja()
        {
            InitializeComponent();
            tblFondoCaja = BL.FondoCajaBLL.GetTabla();
        }

        public frmFondoCaja(DataTable tblFondoCaja, int PK)
            : this()
        {
            this.tblFondoCaja = tblFondoCaja;
            this.PK = PK;
        }

        private void frmFondoCaja_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblLocales = BL.GetDataBLL.Locales();
            viewLocal = new DataView(tblLocales);
            viewLocal.RowFilter = "IdLocalLOC <>'1' AND IdLocalLOC <>'2' AND IdLocalLOC <>'11' AND IdLocalLOC <>'12'";
            lstLocales.ValueMember = "IdLocalLOC";
            lstLocales.DisplayMember = "NombreLOC";
            lstLocales.DataSource = viewLocal;
            tblPcs = BL.GetDataBLL.Pc();
            string local = lstLocales.SelectedValue.ToString();
            viewPc = new DataView(tblPcs);
            viewPc.RowFilter = "IdLocalPC = '" + local + "'";
            viewPc.Sort = "Detalle ASC";
            lstPc.ValueMember = "IdPC";
            lstPc.DisplayMember = "Detalle";
            lstPc.DataSource = viewPc;
            viewFondoCaja = new DataView(tblFondoCaja);
            if (tblFondoCaja.Rows.Count == 0)
            {
                rowView = viewFondoCaja.AddNew();
                Random rand = new Random();
                int clave = rand.Next(1, 2000000000);
                rowView["IdFondoFONP"] = clave;
                rowView["FechaFONP"] = DateTime.Today;
                rowView.EndEdit();
            }
            else
            {
                viewFondoCaja.RowFilter = "IdFondoFONP = '" + PK + "'";
                rowView = viewFondoCaja[0];
                lstLocales.SelectedValue = rowView["IdLocalLOC"];
                lstPc.SelectedValue = rowView["IdPcFONP"];
            }
            dateTimePicker1.DataBindings.Add("Text", rowView, "FechaFONP", false, DataSourceUpdateMode.OnPropertyChanged);
            lstPc.DataBindings.Add("SelectedValue", rowView, "IdPcFONP", false, DataSourceUpdateMode.OnPropertyChanged);
            txtImporte.DataBindings.Add("Text", rowView, "ImporteFONP", false, DataSourceUpdateMode.OnPropertyChanged);
            if (lstPc.Items.Count > 0) lstPc.SetSelected(0, true);   
            this.lstLocales.SelectedValueChanged += new System.EventHandler(this.lstLocales_SelectedValueChanged);
            this.AcceptButton = btnAceptar;
            txtImporte.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lstPc.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un nro de caja.", "Trend Gestión");
                return;
            }
            if (txtImporte.Text == "") return;
            rowView.EndEdit();
            if (tblFondoCaja.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblFondoCaja, "frmFondoCaja", "grabar");
                progreso.ShowDialog();
                tblFondoCaja.AcceptChanges();
                Close();
            }
        }
         
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void lstLocales_SelectedValueChanged(object sender, EventArgs e)
        {
            string local = lstLocales.SelectedValue.ToString();
            viewPc = new DataView(tblPcs);
            viewPc.RowFilter = "IdLocalPC = '" + local + "'";
            viewPc.Sort = "Detalle ASC";
            lstPc.ValueMember = "IdPC";
            lstPc.DisplayMember = "Detalle";
            lstPc.DataSource = viewPc;
        }

        private void frmFondoCaja_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtImporte.Text == "") return;
            rowView.EndEdit();
            if (tblFondoCaja.GetChanges() != null)
            {
                DialogResult respuesta = MessageBox.Show("¿Confirma la grabación de datos?", "Trend Gestión",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (respuesta)
                {
                    case DialogResult.Yes:                        
                        frmProgress progreso = new frmProgress(tblFondoCaja, "frmFondoCaja", "grabar");
                        progreso.ShowDialog();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
        
    }
}
