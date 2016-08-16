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
    public partial class frmArticulosAgrupar : Form
    {
        private frmArticulosAgrupar instanciaArticulosAgrupar;
        private DataView viewOrigen;
        private DataView viewDestino;
        private DataTable tblStock;
        private DataTable tblArticulosStock;
        private DataTable tblArticulos;
        public bool grabacionCorrecta;

        public frmArticulosAgrupar(DataTable tblStock, DataTable tblArticulosStock)
        {
            InitializeComponent();
            this.tblStock = tblStock;
            this.tblArticulosStock = tblArticulosStock;
        }

        private void frmArticulosAgrupar_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblArticulos = BL.GetDataBLL.Articulos();
            viewOrigen = new DataView(tblArticulosStock);
            viewOrigen.RowFilter = "IdArticuloART LIKE '000000000'";
            dgvDatosOrigen.DataSource = viewOrigen;
            dgvDatosOrigen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDatosOrigen.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvDatosOrigen.Columns["IdArticuloART"].ReadOnly = true;
            dgvDatosOrigen.Columns["IdArticuloART"].HeaderText = "Artículo";
            dgvDatosOrigen.Columns["DescripcionART"].ReadOnly = true;
            dgvDatosOrigen.Columns["DescripcionART"].HeaderText = "Descripción";
            dgvDatosOrigen.Columns["PrecioCostoART"].ReadOnly = true;
            dgvDatosOrigen.Columns["PrecioCostoART"].HeaderText = "Costo";
            dgvDatosOrigen.Columns["PrecioPublicoART"].ReadOnly = true;
            dgvDatosOrigen.Columns["PrecioPublicoART"].HeaderText = "Público";
            dgvDatosOrigen.Columns["PrecioMayorART"].ReadOnly = true;
            dgvDatosOrigen.Columns["PrecioMayorART"].HeaderText = "Mayorista";
            DataGridViewCheckBoxColumn chkAgrupar = new DataGridViewCheckBoxColumn();
            chkAgrupar.Name = "Agrupar";
            chkAgrupar.Width = 40;
            chkAgrupar.HeaderText = "Agrupar";
            chkAgrupar.TrueValue = 1;
            chkAgrupar.FalseValue = 0;
            dgvDatosOrigen.Columns.Insert(6, chkAgrupar);
            dgvDatosOrigen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            viewDestino = new DataView(tblArticulos);
            viewDestino.RowFilter = "IdArticuloART LIKE '000000000'";
            dgvDatosDestino.DataSource = viewDestino; 
            dgvDatosDestino.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDatosDestino.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvDatosDestino.Columns["IdItemART"].Visible = false;
            dgvDatosDestino.Columns["IdGeneroART"].Visible = false;
            dgvDatosDestino.Columns["IdColorART"].Visible = false;
            dgvDatosDestino.Columns["IdAliculotaIvaART"].Visible = false;
            dgvDatosDestino.Columns["TalleART"].Visible = false;
            dgvDatosDestino.Columns["IdProveedorART"].Visible = false;
            dgvDatosDestino.Columns["FechaART"].Visible = false;
            dgvDatosDestino.Columns["DescripcionWebART"].Visible = false;
            dgvDatosDestino.Columns["ActivoWebART"].Visible = false;
            dgvDatosDestino.Columns["ImagenART"].Visible = false;
            dgvDatosDestino.Columns["ImagenBackART"].Visible = false;
            dgvDatosDestino.Columns["ImagenColorART"].Visible = false;
            dgvDatosDestino.Columns["NuevoART"].Visible = false;
            dgvDatosDestino.Columns["RazonSocialPRO"].Visible = false;
            dgvDatosDestino.Columns["PrecioCostoART"].HeaderText = "Costo";
            dgvDatosDestino.Columns["PrecioCostoART"].ReadOnly = true;
            dgvDatosDestino.Columns["PrecioPublicoART"].HeaderText = "Público";
            dgvDatosDestino.Columns["PrecioPublicoART"].ReadOnly = true;
            dgvDatosDestino.Columns["PrecioMayorART"].HeaderText = "Mayorista";
            dgvDatosDestino.Columns["PrecioMayorART"].ReadOnly = true;
            dgvDatosDestino.Columns["IdArticuloART"].HeaderText = "Codigo";
            dgvDatosDestino.Columns["IdArticuloART"].ReadOnly = true;
            dgvDatosDestino.Columns["DescripcionART"].HeaderText = "Descripcion";
            dgvDatosDestino.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            txtParametrosOrigen.Focus();
        }

        private void txtParametrosOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) btnBuscarOrigen.PerformClick();
        }

        private void btnBuscarOrigen_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rdArticuloOrigen.Checked == true)
            {
                viewOrigen.RowFilter = "IdArticuloART LIKE '" + txtParametrosOrigen.Text + "*'";
                if (viewOrigen.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                viewOrigen.RowFilter = "DescripcionART LIKE '*" + txtParametrosOrigen.Text + "*'";
                if (viewOrigen.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtParametrosDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) btnBuscarDestino.PerformClick();
        }

        private void btnBuscarDestino_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rdArticuloDestino.Checked == true)
            {
                viewDestino.RowFilter = "IdArticuloART LIKE '" + txtParametrosDestino.Text + "*'";
                if (viewDestino.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                viewDestino.RowFilter = "DescripcionART LIKE '*" + txtParametrosDestino.Text + "*'";
                if (viewDestino.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (dgvDatosOrigen.Rows.Count == 0)
            {
                MessageBox.Show("Debe indicar un artículo de origen", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtParametrosOrigen.Focus();
                return;
            }
            if (dgvDatosDestino.Rows.Count == 0)
            {
                MessageBox.Show("Debe indicar un artículo de destino", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtParametrosDestino.Focus();
                return;
            }
            if (MessageBox.Show("La agrupación de artículos eliminará los artículos de origen. ¿Desea continuar?", "Trend", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;
            Cursor.Current = Cursors.WaitCursor;
            DataView viewStock = new DataView(tblStock);
            DataView viewArticulos = new DataView(tblArticulos);
            DataTable tblLocales = BL.GetDataBLL.Locales();
            DataView viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <> 1 AND IdLocalLOC <> 2";
            string articuloDestino = dgvDatosDestino.CurrentRow.Cells["IdArticuloART"].Value.ToString();
            int cantidadArticulos = 0;
            int cantidadDestino = 0;
            ArrayList articulosBorrar = new ArrayList();
            foreach (DataGridViewRow rowOrigen in dgvDatosOrigen.Rows)
            {
                if (rowOrigen.Cells["Agrupar"].Value != null)
                {
                    if (rowOrigen.Cells["Agrupar"].Value.ToString() == "1")
                    {
                        string articuloOrigen = rowOrigen.Cells["IdArticuloART"].Value.ToString();
                        foreach (DataRowView rowLocales in viewLocales)
                        {
                            string local = rowLocales["IdLocalLOC"].ToString();
                            if (local != "1" && local != "2")
                            {
                                viewStock.RowFilter = "IdLocalSTK = '" + local + "' AND IdArticuloSTK = '" + articuloOrigen + "'";
                                int cantidadOrigen = Convert.ToInt32(viewStock[0]["CantidadSTK"].ToString());
                                viewStock.RowFilter = "IdLocalSTK = '" + local + "' AND IdArticuloSTK = '" + articuloDestino + "'";
                                if (viewStock.Count > 0)
                                {
                                    cantidadDestino = Convert.ToInt32(viewStock[0]["CantidadSTK"].ToString());
                                    viewStock[0]["CantidadSTK"] = cantidadOrigen + cantidadDestino;
                                }                                    
                                else // no existe el registro en la tabla stock. Lo agrego
                                {
                                    DataRowView rowView = viewStock.AddNew();
                                    rowView["IdArticuloSTK"] = articuloDestino;
                                    rowView["IdLocalSTK"] = local;
                                    rowView["CantidadSTK"] = cantidadOrigen;
                                    rowView.EndEdit();
                                }

                            }

                        }
                        articulosBorrar.Add(articuloOrigen); // agrego los articulos agrupados para luego borrarlos
                    }
                    cantidadArticulos++;
                }
            }
            if (cantidadArticulos == 0)
            {
                MessageBox.Show("Debe seleccionar un artículo de origen", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // borro los articulos agrupados
            foreach (string borrado in articulosBorrar)
            {
                DataRow[] found = tblArticulos.Select("IdArticuloART = '" + borrado + "'");
                found[0].Delete();
            }
            //Actualizo el viewOrigen para reflejar los cambios en pantalla
            foreach (string borrado in articulosBorrar)
            {
                DataRow[] found = tblArticulosStock.Select("IdArticuloART = '" + borrado + "'");
                found[0].Delete();
            }
            Cursor.Current = Cursors.Arrow;
            if(tblArticulos.GetChanges() != null)
            {
                frmProgress frm = new frmProgress(tblArticulos, tblStock, "frmArticulosAgrupar", "grabar", instanciaArticulosAgrupar);
                frm.FormClosed += frmProgress_FormClosed;
                frm.ShowDialog();
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmArticulosGenerar generar = new frmArticulosGenerar();
            generar.ShowDialog();
            Cursor.Current = Cursors.Arrow;
        }

        private void frmProgress_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!grabacionCorrecta)
            {
                Close();
                return;
            }
        }

        private void dgvDatosOrigen_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dgvDatosDestino_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }
}
