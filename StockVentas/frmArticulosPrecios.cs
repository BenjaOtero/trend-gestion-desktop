using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class frmArticulosPrecios : Form
    {
        private DataView viewNuevos;
        private DataTable tblArticulos;
        private string idArticulo;

        public frmArticulosPrecios(DataTable tblArticulos, string idArticulo)
        {
            InitializeComponent();
            this.tblArticulos = tblArticulos;
            this.idArticulo = idArticulo;
            BL.Utilitarios.AddEventosABM(grpCampos);
            txtCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtPublico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtMayor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);         
        }

        private void frmArticulosEditNews_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            viewNuevos = new DataView(tblArticulos, "", "IdArticuloART", DataViewRowState.CurrentRows);
            viewNuevos.RowFilter = "IdArticuloART LIKE '" + idArticulo + "*'";
            dgvDatos.DataSource = viewNuevos;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvDatos.Columns["IdItemART"].Visible = false;
            dgvDatos.Columns["IdGeneroART"].Visible = false;
            dgvDatos.Columns["IdColorART"].Visible = false;
            dgvDatos.Columns["IdAliculotaIvaART"].Visible = false;
            dgvDatos.Columns["TalleART"].Visible = false;
            dgvDatos.Columns["IdProveedorART"].Visible = false;
            dgvDatos.Columns["FechaART"].Visible = false;
            dgvDatos.Columns["DescripcionWebART"].Visible = false;
            dgvDatos.Columns["ActivoWebART"].Visible = false;
            dgvDatos.Columns["ImagenART"].Visible = false;
            dgvDatos.Columns["ImagenBackART"].Visible = false;
            dgvDatos.Columns["ImagenColorART"].Visible = false;
            dgvDatos.Columns["NuevoART"].Visible = false;
            dgvDatos.Columns["RazonSocialPRO"].Visible = false;
            dgvDatos.Columns["PrecioCostoART"].HeaderText = "Costo";
            dgvDatos.Columns["PrecioCostoART"].ReadOnly = true;
            dgvDatos.Columns["PrecioPublicoART"].HeaderText = "Público";
            dgvDatos.Columns["PrecioPublicoART"].ReadOnly = true;
            dgvDatos.Columns["PrecioMayorART"].HeaderText = "Mayorista";
            dgvDatos.Columns["PrecioMayorART"].ReadOnly = true;
            dgvDatos.Columns["IdArticuloART"].HeaderText = "Codigo";
            dgvDatos.Columns["IdArticuloART"].ReadOnly = true;
            dgvDatos.Columns["DescripcionART"].HeaderText = "Descripcion";
            DataGridViewCheckBoxColumn chkNuevo = new DataGridViewCheckBoxColumn();
            chkNuevo.Name = "Actualizar";
            chkNuevo.Width = 40;
            chkNuevo.HeaderText = "Actualizar";
            chkNuevo.TrueValue = 1;
            chkNuevo.FalseValue = 0;
            dgvDatos.Columns.Insert(15, chkNuevo);
            txtCosto.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    row.Cells["Actualizar"].Value = 1;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    row.Cells["Actualizar"].Value = 0;
                }
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (ValidarControles())
            {
                Grabar();
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    row.Cells["Actualizar"].Value = 0;
                }
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Close();
            Cursor.Current = Cursors.Arrow;
        }

        private void frmArticulosPrecios_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool checkeado = false;
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (row.Cells["Actualizar"].Value != null)
                {
                    if (row.Cells["Actualizar"].Value.ToString() == "1")
                    {
                        checkeado = true;
                        continue;
                    }
                }
            }
            if (checkeado)
            {
                DialogResult respuesta =
                        MessageBox.Show("¿Desea guardar los cambios?", "Trend", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (respuesta)
                {
                    case DialogResult.Yes:
                        if (ValidarControles()) Grabar();
                        else e.Cancel = true;                        
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }        

        private bool ValidarControles()
        {
            if (String.IsNullOrEmpty(txtCosto.Text) && String.IsNullOrEmpty(txtPublico.Text) && String.IsNullOrEmpty(txtMayor.Text))
            {
                return false;
            }
            return true;
        }        

        private void Grabar()
        {
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (row.Cells["Actualizar"].Value != null)
                {
                    if (row.Cells["Actualizar"].Value.ToString() == "1")
                    {
                        DataRowView[] foundRows = viewNuevos.FindRows(new object[] { row.Cells["IdArticuloART"].Value.ToString() });
                        foundRows[0].BeginEdit();
                        if (!String.IsNullOrEmpty(txtCosto.Text))
                        {
                            foundRows[0]["PrecioCostoART"] = txtCosto.Text;
                        }
                        if (!String.IsNullOrEmpty(txtPublico.Text))
                        {
                            foundRows[0]["PrecioPublicoART"] = txtPublico.Text;
                        }
                        if (!String.IsNullOrEmpty(txtMayor.Text))
                        {
                            foundRows[0]["PrecioMayorART"] = txtMayor.Text;
                        }
                        foundRows[0].EndEdit();
                    }
                }
            }
            if (tblArticulos.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblArticulos, "frmArticulos", "grabar");
                progreso.ShowDialog();
            }
        }

        private void dgvDatos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }
}
