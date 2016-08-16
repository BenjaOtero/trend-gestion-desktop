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
    public partial class frmArticulosEditNews : Form
    {
        private DataView viewNuevos;
        private DataTable tblArticulos;
        DateTime desde;
        DateTime hasta;

        public frmArticulosEditNews(DataTable tblArticulos, DateTime desde, DateTime hasta)
        {
            InitializeComponent();
            this.tblArticulos = tblArticulos;
            tblArticulos.ColumnChanged += new DataColumnChangeEventHandler(HabilitarGrabar);
            this.desde = desde;
            this.hasta = hasta;
            viewNuevos = new DataView(tblArticulos);
            viewNuevos.RowFilter = ("FechaART>='" + desde + "' AND FechaART< '" + hasta + "'");
        }

        private void frmArticulosEditNews_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            viewNuevos.Sort = "FechaART ASC";
            bindingSource1.DataSource = viewNuevos;
            bindingNavigator1.BindingSource = bindingSource1;
            dgvDatos.DataSource = bindingSource1;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDatos.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvDatos.Columns["IdItemART"].Visible = false;
            dgvDatos.Columns["IdGeneroART"].Visible = false;
            dgvDatos.Columns["IdColorART"].Visible = false;
            dgvDatos.Columns["IdAliculotaIvaART"].Visible = false;
            dgvDatos.Columns["TalleART"].Visible = false;
            dgvDatos.Columns["IdProveedorART"].Visible = false;
            dgvDatos.Columns["PrecioCostoART"].Visible = false;
            dgvDatos.Columns["PrecioPublicoART"].Visible = false;
            dgvDatos.Columns["PrecioMayorART"].Visible = false;
            dgvDatos.Columns["DescripcionWebART"].Visible = false;
            dgvDatos.Columns["ActivoWebART"].Visible = false;
            dgvDatos.Columns["ImagenART"].Visible = false;
            dgvDatos.Columns["ImagenBackART"].Visible = false;
            dgvDatos.Columns["ImagenColorART"].Visible = false;
            dgvDatos.Columns["NuevoART"].Visible = false;
            dgvDatos.Columns["RazonSocialPRO"].Visible = false;
            dgvDatos.Columns["FechaART"].HeaderText = "Fecha";
            dgvDatos.Columns["FechaART"].ReadOnly = true;
            dgvDatos.Columns["IdArticuloART"].HeaderText = "Codigo";
            dgvDatos.Columns["IdArticuloART"].ReadOnly = true;
            dgvDatos.Columns["DescripcionART"].HeaderText = "Descripcion";
            dgvDatos.Columns["DescripcionART"].ReadOnly = true;
            dgvDatos.Columns["NuevoART"].HeaderText = "Nuevo";
            dgvDatos.Columns.Remove("NuevoART");
            DataGridViewCheckBoxColumn chkNuevo = new DataGridViewCheckBoxColumn();
            chkNuevo.Name = "NuevoART";
            chkNuevo.Width = 40;
            chkNuevo.HeaderText = "Nuevo";
            chkNuevo.DataPropertyName = "NuevoART";
            chkNuevo.TrueValue = 1;
            chkNuevo.FalseValue = 0;
            dgvDatos.Columns.Insert(15, chkNuevo);
            viewNuevos.Sort = "FechaArt ASC";
            btnGrabar.Enabled = false;
            checkBox1.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            bindingSource1.EndEdit();
            Grabar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    dgvDatos.BeginEdit(true);
                    row.Cells["NuevoART"].Value = 1;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    dgvDatos.BeginEdit(true);
                    row.Cells["NuevoART"].Value = 0;
                }
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void frmArticulosEditNews_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.EndEdit();
            if (tblArticulos.GetChanges() != null)
            {
                DialogResult respuesta = 
                        MessageBox.Show("¿Desea guardar los cambios?", "Trend", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (respuesta)
                { 
                    case DialogResult.Yes:
                        Grabar();
                        break;
                    case DialogResult.No:
                        tblArticulos.RejectChanges();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void dgvDatos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            btnGrabar.Enabled = true;
        }

        private void Grabar()
        {
            if (tblArticulos.GetChanges() != null)
            {
            //    bindingSource1.MoveLast();
                frmProgress progreso = new frmProgress(tblArticulos, "frmArticulos", "grabar");
                progreso.ShowDialog();
                btnGrabar.Enabled = false;
            }
        }

        public void HabilitarGrabar(object sender, EventArgs e)
        {
            if (btnGrabar.Enabled == false)
            {
                btnGrabar.Enabled = true;
            }
        }

        private void dgvDatos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }
}
