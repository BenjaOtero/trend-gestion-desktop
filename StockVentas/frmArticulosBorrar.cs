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
    public partial class frmArticulosBorrar : Form
    {
        DataTable tblArticulos;
        DataTable tblArticulosBorrar;
        DataView viewArticulos;
        DataView viewArticulosBorrar;
        DataGridViewCheckBoxColumn chkBorrar;

        public frmArticulosBorrar(DataTable tblArticulosBorrar, DataTable tblArticulos)
        {
            InitializeComponent();
            this.tblArticulosBorrar = tblArticulosBorrar;
            this.tblArticulos = tblArticulos;
        }

        private void frmArticulosBorrar_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            viewArticulosBorrar = new DataView(tblArticulosBorrar);
            viewArticulosBorrar.RowFilter = "IdArticuloART LIKE '000000000'";
            dgvDatos.DataSource = viewArticulosBorrar;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDatos.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvDatos.Columns["IdArticuloART"].HeaderText = "Artículo";
            dgvDatos.Columns["DescripcionART"].HeaderText = "Descripción";
            dgvDatos.Columns["PrecioCostoART"].HeaderText = "Costo";
            dgvDatos.Columns["PrecioPublicoART"].HeaderText = "Público";
            dgvDatos.Columns["PrecioMayorART"].HeaderText = "Mayorista";
            dgvDatos.Columns["IdArticuloART"].ReadOnly = true;
            dgvDatos.Columns["DescripcionART"].ReadOnly = true;
            dgvDatos.Columns["PrecioCostoART"].ReadOnly = true;
            dgvDatos.Columns["PrecioPublicoART"].ReadOnly = true;
            dgvDatos.Columns["PrecioMayorART"].ReadOnly = true;
            chkBorrar = new DataGridViewCheckBoxColumn();
            chkBorrar.Name = "Borrar";
            chkBorrar.Width = 40;
            chkBorrar.HeaderText = "Borrar";
            chkBorrar.TrueValue = 1;
            chkBorrar.FalseValue = 0;
            dgvDatos.Columns.Insert(6, chkBorrar);
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            txtParametros.Focus();
        }

        private void txtParametros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) btnBuscar.PerformClick();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rdArticulo.Checked == true)
            {

                viewArticulosBorrar.RowFilter = "IdArticuloART LIKE '" + txtParametros.Text + "*'";
                if (viewArticulosBorrar.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                viewArticulosBorrar.RowFilter = "DescripcionART LIKE '*" + txtParametros.Text + "*'";
                if (viewArticulosBorrar.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ArrayList articulosBorrar = new ArrayList();
            foreach (DataGridViewRow rowOrigen in dgvDatos.Rows)
            {
                if (rowOrigen.Cells["Borrar"].Value != null)
                {
                    if (rowOrigen.Cells["Borrar"].Value.ToString() == "1")
                    {
                        string articuloOrigen = rowOrigen.Cells["IdArticuloART"].Value.ToString();
                        articulosBorrar.Add(articuloOrigen); // agrego los articulos agrupados para luego borrarlos
                    }
                }
            }
            if (articulosBorrar.Count > 0)
            {
                if (MessageBox.Show(
                 "Si elimina este artículo/s, se eliminarán las ventas, movimientos de stock y stock relacionados. ¿Desea continuar?",
                 "Trend", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string oldBorrado = string.Empty;
                    string imagenesBorrar = string.Empty;
                    viewArticulos = new DataView(tblArticulos);
                    foreach (string borrado in articulosBorrar)
                    {
                        viewArticulos.RowFilter = "IdArticuloART = '" + borrado + "'";
                        viewArticulos[0].Delete();
                        if (string.IsNullOrEmpty(oldBorrado))
                        {
                            imagenesBorrar += borrado.Substring(0, borrado.Length - 2) + ";";
                            oldBorrado = borrado;
                        }
                        else
                        {
                            if (oldBorrado.Substring(0, oldBorrado.Length - 2) != borrado.Substring(0, borrado.Length - 2))
                            {
                                imagenesBorrar += borrado.Substring(0, borrado.Length - 2) + ";";
                                oldBorrado = borrado;
                            }
                        }
                    }
                    foreach (string borrado in articulosBorrar)
                    {
                        viewArticulosBorrar.RowFilter = "IdArticuloART = '" + borrado + "'";
                        viewArticulosBorrar[0].Delete();
                    }
                    if (rdArticulo.Checked == true)
                    {
                        viewArticulosBorrar.RowFilter = "IdArticuloART LIKE '" + txtParametros.Text + "*'";
                    }
                    else
                    {
                        viewArticulosBorrar.RowFilter = "DescripcionART LIKE '*" + txtParametros.Text + "*'";
                    }
                    if (tblArticulos.GetChanges() != null)
                    {
                        frmProgress frm = new frmProgress(tblArticulos, "frmArticulosBorrar", "grabar", imagenesBorrar);
                        frm.ShowDialog();
                    }
                }
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDatos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }
}
