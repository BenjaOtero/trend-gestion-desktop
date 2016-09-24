using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using BL;
using Entities;
using System.Media;
using Microsoft.VisualBasic;
using System.IO;
using System.Threading;
using DAL;

namespace StockVentas
{

    public partial class frmStockEntradas : Form
    {
        private frmStockEntradas instancia;
        public DataSet dsStockMov;
        public DataSet dsStock;
        DataTable tblStockMov;
        DataTable tblStockEntradas;
        DataTable tblStockMovDetalle;
        DataTable tblEntradasDetalle;
        DataTable tblEtiquetas;
        DataView viewStockMov;
        DataView viewStockMovDetalle;
        DataRowView rowView;
        DataTable tblLocales;
        DataTable tblArticulos;
        DataView viewOrigen;
        DataView viewDestino;
        public string PK = string.Empty;
        int idMov;
        int claveDetalle;
        Random rand;
        public string idArticulo;
        DataRowCollection cfilas;
        DataRow nuevaFila;
        bool imprimePrecios;

        public frmStockEntradas()
        {
            InitializeComponent();
            instancia = this;
            tblLocales = BL.GetDataBLL.Locales();
            viewOrigen = new DataView(tblLocales);
            viewOrigen.RowFilter = "IdLocalLOC ='1'";
            cmbOrigen.ValueMember = "IdLocalLOC";
            cmbOrigen.DisplayMember = "NombreLOC";
            cmbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrigen.DataSource = viewOrigen;
            viewDestino = new DataView(tblLocales);
            viewDestino.RowFilter = "IdLocalLOC <>'1' AND IdLocalLOC <>'2'";
            cmbDestino.ValueMember = "IdLocalLOC";
            cmbDestino.DisplayMember = "NombreLOC";
            cmbDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDestino.DataSource = viewDestino;
            cmbOrigen.SelectedIndex = -1;
            cmbDestino.SelectedIndex = -1;
            cmbDestino.SelectedValueChanged += new EventHandler(this.validarMaestro);
            cmbDestino.SelectedIndexChanged += new EventHandler(this.cmbDestino_SelectedIndexChanged);
            rand = new Random();
        }

        private void frmStockMov_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblStockMov = BL.StockMovBLL.GetTablaMov();
            tblStockMovDetalle = BL.StockMovBLL.GetTablaDetalle();
            DataColumn col = new DataColumn();
            col.ColumnName = "Precio";
            tblStockMovDetalle.Columns.Add(col);
            dsStockMov = new DataSet();
            dsStockMov.DataSetName = "dsStockMov";
            dsStockMov.Tables.Add(tblStockMov);
            dsStockMov.Tables.Add(tblStockMovDetalle);
            viewStockMov = new DataView(tblStockMov);
            viewStockMovDetalle = new DataView(tblStockMovDetalle);
            lblNro.ForeColor = System.Drawing.Color.DarkRed;
            Random rand = new Random();
            idMov = rand.Next(-2000000000, 2000000000);
            lblNro.Text = idMov.ToString();
            viewStockMov.RowStateFilter = DataViewRowState.Added;
            rowView = viewStockMov.AddNew();
            rowView["IdMovMSTK"] = idMov.ToString();
            rowView["OrigenMSTK"] = 1;
            rowView["FechaMSTK"] = DateTime.Today;
            rowView["CompensaMSTK"] = 0;
            rowView.EndEdit();
            dgvDatos.Enabled = false;
            dateTimePicker1.DataBindings.Add("Text", rowView, "FechaMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbOrigen.DataBindings.Add("SelectedValue", rowView, "OrigenMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbDestino.DataBindings.Add("SelectedValue", rowView, "DestinoMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
            bindingSource1.DataSource = viewStockMovDetalle;
            bindingNavigator1.BindingSource = bindingSource1;
            dgvDatos.DataSource = bindingSource1;
            dgvDatos.AllowUserToOrderColumns = false;
            dgvDatos.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvDatos.Columns["IdArticuloMSTKD"].HeaderText = "Código";
            dgvDatos.Columns["IdArticuloMSTKD"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDatos.Columns["DescripcionART"].HeaderText = "Descripción";
            dgvDatos.Columns["DescripcionART"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDatos.Columns["IdMovMSTKD"].Visible = false;
            dgvDatos.Columns["IdMSTKD"].Visible = false;
            dgvDatos.Columns["CompensaMSTKD"].Visible = false;
            dgvDatos.Columns["OrigenMSTKD"].Visible = false;
            dgvDatos.Columns["DestinoMSTKD"].Visible = false;
            dgvDatos.Columns["Precio"].Visible = false;
            dgvDatos.Columns["CantidadMSTKD"].Width = 100;
            dgvDatos.Columns["CantidadMSTKD"].HeaderText = "Cantidad";
            dgvDatos.Columns["CantidadMSTKD"].SortMode = DataGridViewColumnSortMode.NotSortable;
            tblArticulos = BL.GetDataBLL.Articulos();
            tblArticulos.TableName = "Articulos";
            dgvDatos.Columns[3].Width = 500;
            dgvDatos.Columns[3].ReadOnly = true;
            dgvDatos.Enabled = false;
            btnArticulos.Enabled = false;
            cmbDestino.Focus();
        }

        private void cmbDestino_SelectedIndexChanged(object sender, EventArgs e) // hacer lo mismo para los combos del frmStockMov
        {
            if (cmbDestino.SelectedValue != null)
            {
                if (Convert.ToInt32(cmbDestino.SelectedValue.ToString()) == -1) return;
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Cells["DestinoMSTKD"].Value = cmbDestino.SelectedValue;
                    }
                }
                dgvDatos.Focus();
                if (dgvDatos.Rows.Count == 1 && dgvDatos.CurrentRow.IsNewRow)
                {
                    dgvDatos.CurrentCell = dgvDatos.Rows[0].Cells["IdArticuloMSTKD"];
                }
                else
                {
                    dgvDatos.CurrentCell = dgvDatos.Rows[0].Cells["CantidadMSTKD"];
                }
            }
        }

        private void validarMaestro(object sender, EventArgs e)
        {
            try
            {
                if (cmbOrigen.SelectedIndex == -1 || cmbDestino.SelectedIndex == -1)
                {
                    dgvDatos.Enabled = false;
                }
                else
                {
                    dgvDatos.Enabled = true;
                }
            }
            catch (NullReferenceException)
            {
                dgvDatos.Enabled = false;
            }
        }

        private void dgvDatos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.RowCount > 1)
            {
                btnGrabar.Enabled = true;
            }
            btnArticulos.Enabled = true;
        }

        private void dgvDatos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["CompensaMSTKD"].Value = 0;
        }

        private void dgvDatos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvDatos.CurrentCell.OwningColumn.Name == "DescripcionART")
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dgvDatos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (String.IsNullOrEmpty(dgvDatos["IdMSTKD", e.RowIndex].Value.ToString()))
            {
                Random rand = new Random();
                int clave = rand.Next(-2000000000, 2000000000);
                dgvDatos["IdMSTKD", e.RowIndex].Value = clave;
                dgvDatos["IdMovMSTKD", e.RowIndex].Value = Convert.ToInt32(lblNro.Text.ToString());
                dgvDatos["OrigenMSTKD", e.RowIndex].Value = cmbOrigen.SelectedValue;
                dgvDatos["DestinoMSTKD", e.RowIndex].Value = cmbDestino.SelectedValue;
            }
            if (String.IsNullOrEmpty(dgvDatos["CompensaMSTKD", e.RowIndex].Value.ToString()))
            {
                dgvDatos["CompensaMSTKD", e.RowIndex].Value = 0;
            }
        }

        private void dgvDatos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            }
            TextBox txt = e.Control as TextBox;
            if (dgvDatos.CurrentCell.OwningColumn.Name == "IdArticuloMSTKD" && txt != null)
            {
                var source = new AutoCompleteStringCollection();
                String[] stringArray =
                    Array.ConvertAll<DataRow, String>(tblArticulos.Select(), delegate(DataRow row) { return (String)row["IdArticuloART"]; });
                source.AddRange(stringArray);
                if (txt != null)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteCustomSource = source;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
            else if (dgvDatos.CurrentCell.OwningColumn.Name != "IdArticuloMSTKD" && txt != null)
            {
                if (txt != null)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.None;
                    txt.AutoCompleteSource = AutoCompleteSource.None;
                }
            }
        }

        private void dgvDatos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvDatos.CurrentRow.IsNewRow)
            {
                return;
            }
            dgvDatos.Rows[e.RowIndex].ErrorText = String.Empty;
            if (dgvDatos.CurrentCell.OwningColumn.Name == "IdArticuloMSTKD")
            {
                string articulo = dgvDatos.CurrentCell.EditedFormattedValue.ToString();
                DataRow[] foundRow = tblArticulos.Select("IdArticuloART = '" + articulo + "'");
                if (foundRow.Length != 0)
                {
                    DataRow filaActual = foundRow[0];
                    var fila = dgvDatos.CurrentCell.RowIndex;
                    this.dgvDatos[2, fila].Value = filaActual["IdArticuloART"].ToString();
                    this.dgvDatos[3, fila].Value = filaActual["DescripcionART"].ToString();
                    /*  if(cmbDestino.Text == "JESUS MARIA")
                          this.dgvDatos["Precio", fila].Value = filaActual["PrecioMayorART"].ToString();
                      else if(cmbDestino.Text == "MAKRO")*/
                    this.dgvDatos["Precio", fila].Value = filaActual["PrecioPublicoART"].ToString();
                }
                else if (foundRow.Length == 0)
                {
                    dgvDatos.Rows[e.RowIndex].ErrorText = "El código de artículo es inexistente.";
                    e.Cancel = true;
                }
            }
            if (dgvDatos.CurrentCell.OwningColumn.Name == "CantidadMSTKD")
            {
                var dato = dgvDatos.CurrentCell.EditedFormattedValue;
                bool result = Information.IsNumeric(dato);
                if (result == false && dato != null)
                {
                    dgvDatos.Rows[e.RowIndex].ErrorText = "Debe ingresar un valor numérico";
                    e.Cancel = true;
                }
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (tblStockMovDetalle.GetChanges() != null)
            {
                if (ValidarGrid())
                {
                    if (MessageBox.Show("¿Confirma la grabación de datos?", "Trend",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    grabar();
                    Cursor.Current = Cursors.WaitCursor;
                    bool imprimir = ImprimirEtiquetas();
                    Zero();
                    if (imprimir)
                    {
                        EtiquetasRpt informeEtiquetas = new EtiquetasRpt(tblEtiquetas, imprimePrecios);
                        informeEtiquetas.Show();
                    }
                    Cursor.Current = Cursors.Arrow;                
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 1) return;
            if (MessageBox.Show("¿Confirma el borrado de datos?", "Trend",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            tblStockMovDetalle.Clear();
            cmbDestino.Focus();
            cmbDestino.DroppedDown = true;
        }

        private void btnArticulos_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmArticulos articulos = new frmArticulos(ref instancia, tblArticulos);
            articulos.Show(this);
            articulos.FormClosed += frmArticulos_FormClosed;
            // Vuelvo a activar el evento para futuras busquedas de artículos
            Cursor.Current = Cursors.Arrow;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmStockEntradas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (tblStockMovDetalle.GetChanges() != null)
            {
                DialogResult respuesta = MessageBox.Show("¿Confirma la grabación de datos?", "Trend Gestión",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (respuesta)
                {
                    case DialogResult.Yes:
                        if (ValidarGrid())
                        {
                            grabar();
                            if (ImprimirEtiquetas())
                            {
                                EtiquetasRpt informeEtiquetas = new EtiquetasRpt(tblEtiquetas, imprimePrecios);
                                informeEtiquetas.Show();
                            }                         
                        }
                        else 
                            e.Cancel = true;
                        break;
                    case DialogResult.No:
                        if (ImprimirEtiquetas())
                        {
                            if (ValidarGrid())
                            {
                                EtiquetasRpt informeEtiquetas = new EtiquetasRpt(tblEtiquetas, imprimePrecios);
                                informeEtiquetas.Show();
                            }
                            else
                                e.Cancel = true;
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            Cursor = Cursors.Arrow;
        }

        private void grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            rowView.EndEdit();
            if (tblStockMovDetalle.GetChanges() == null) return;
            try
            {
                BL.StockMovBLL.GrabarStockMovimientos(dsStockMov);
            }
            catch (ServidorMysqlInaccesibleException ex)
            {
                MessageBox.Show(ex.Message, "Trend Gestión",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                dsStockMov.RejectChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\r' + "Es posible que no se grabaran los datos.", "Trend Gestión",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
            }            
        }

        private bool ImprimirEtiquetas()
        {
            bool imprimir = false;
            if (MessageBox.Show("¿Imprime etiquetas?", "Trend", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                imprimePrecios = false;
                tblEtiquetas = new DataTable();
                tblEtiquetas.Columns.Add("IdArticuloMSTKD", typeof(string));
                tblEtiquetas.Columns.Add("DescripcionART", typeof(string));
                tblEtiquetas.Columns.Add("Precio", typeof(string));
                tblEtiquetas.Columns.Add("IdArticuloMSTKD1", typeof(string));
                tblEtiquetas.Columns.Add("DescripcionART1", typeof(string));
                tblEtiquetas.Columns.Add("Precio1", typeof(string));
                cfilas = tblEtiquetas.Rows;
                bool validar = true;
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (string.IsNullOrEmpty(row.Cells["IdArticuloMSTKD"].Value.ToString()) || string.IsNullOrEmpty(row.Cells["CantidadMSTKD"].Value.ToString()))
                        {
                            validar = false;
                        }
                    }
                }
                if (validar) tblStockMovDetalle.AcceptChanges();                                      
                // hago el foreach para borrar la fila extra que me genera tblEntradasDetalle.AcceptChanges()
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (string.IsNullOrEmpty(row.Cells["IdMSTKD"].Value.ToString()) && string.IsNullOrEmpty(row.Cells["IdArticuloMSTKD"].Value.ToString()))
                        {
                            dgvDatos.Rows.Remove(row);
                        }
                    }
                }
                int i = 1;
                foreach (DataRow row in tblStockMovDetalle.Rows)
                {
                    int x = Convert.ToInt32(row["CantidadMSTKD"].ToString());
                    if (x != 0)
                    {
                        if (i > 1 && (i %= 2) == 0)
                        {
                            nuevaFila[3] = "*" + row["IdArticuloMSTKD"].ToString() + "*";
                            nuevaFila[4] = row["DescripcionART"].ToString();
                            nuevaFila[5] = row["Precio"].ToString();
                            cfilas.Add(nuevaFila);
                            nuevaFila = null;
                            x = x - 1;
                        }
                        for (i = 1; i <= x; i++)
                        {
                            int j;
                            if ((j = i % 2) != 0)
                            {
                                nuevaFila = tblEtiquetas.NewRow();
                                nuevaFila[0] = "*" + row["IdArticuloMSTKD"].ToString() + "*";
                                nuevaFila[1] = row["DescripcionART"].ToString();
                                nuevaFila[2] = row["Precio"].ToString();
                            }
                            else
                            {
                                nuevaFila[3] = "*" + row["IdArticuloMSTKD"].ToString() + "*";
                                nuevaFila[4] = row["DescripcionART"].ToString();
                                nuevaFila[5] = row["Precio"].ToString();
                                cfilas.Add(nuevaFila);
                                nuevaFila = null;
                            }
                        }
                    }
                }
                if (nuevaFila != null)
                {
                    nuevaFila[3] = string.Empty;
                    nuevaFila[4] = string.Empty;
                    nuevaFila[5] = string.Empty;
                    cfilas.Add(nuevaFila);
                }
                imprimir = true;
            }
            return imprimir;
        }

        private void Zero()
        {
            Random rand = new Random();
            idMov = rand.Next(-2000000000, 2000000000);
            rowView.BeginEdit();
            rowView[0] = idMov.ToString();
            lblNro.Text = idMov.ToString();
            this.cmbDestino.SelectedIndexChanged -= new System.EventHandler(this.cmbDestino_SelectedIndexChanged);
            cmbDestino.SelectedValue = -1;
            this.cmbDestino.SelectedIndexChanged += new System.EventHandler(this.cmbDestino_SelectedIndexChanged);
            rowView.EndEdit();
            tblStockMov.AcceptChanges();
            tblStockMov.Rows[0].SetAdded();
            Random randDetalle = new Random();
            foreach (DataRow row in tblStockMovDetalle.Rows)
            {
                try
                {
                    claveDetalle = randDetalle.Next(1, 2000000000);
                    row.BeginEdit();
                    row["IdMSTKD"] = claveDetalle;
                    row["IdMovMSTKD"] = idMov;
                    row["CantidadMSTKD"] = 0;
                    row.EndEdit();
                    tblStockMovDetalle.AcceptChanges();
                }
                catch (DeletedRowInaccessibleException)
                {
                    continue;
                }
            }
            tblStockMovDetalle.AcceptChanges();
            // hago el foreach para borrar la fila extra que me genera tblStockMovDetalle.AcceptChanges()
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (string.IsNullOrEmpty(row.Cells["IdMSTKD"].Value.ToString()) && string.IsNullOrEmpty(row.Cells["IdArticuloMSTKD"].Value.ToString()))
                    {
                        dgvDatos.Rows.Remove(row);
                    }
                }
            }
            foreach (DataRow row in tblStockMovDetalle.Rows)
            {
                try
                {
                    row.SetAdded();
                }
                catch (DeletedRowInaccessibleException)
                {
                    continue;
                }
            }
            tblStockMovDetalle.AcceptChanges();
        }

        private void frmArticulos_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                dgvDatos.EndEdit();
                dgvDatos.CurrentCell = dgvDatos.CurrentRow.Cells["IdArticuloMSTKD"];
                if (!string.IsNullOrEmpty(idArticulo))
                {
                    Clipboard.SetDataObject(idArticulo);
                    dgvDatos.BeginEdit(true);
                    SendKeys.Send("^(v)");
                }
            }
            /*if (dgvDatos.CurrentRow.IsNewRow)
            {
                if (dgvDatos.BeginEdit(true))
                {
                    dgvDatos.EndEdit();
                }
                DataRow newRow = tblEntradasDetalle.NewRow();
                newRow["IdArticuloMSTKD"] = idArticulo;
                tblEntradasDetalle.Rows.Add(newRow);
                dgvDatos.Rows.Remove(dgvDatos.CurrentRow);
                dgvDatos.BeginEdit(true);                
            }
            else
            {
                dgvDatos.CurrentRow.Cells["IdArticuloMSTKD"].Value = idArticulo;
                dgvDatos.BeginEdit(true);
            }
            int nextRow = dgvDatos.CurrentRow.Index + 1;
            dgvDatos.CurrentCell = dgvDatos["IdArticuloMSTKD", nextRow];*/
        }

        private bool ValidarGrid()
        {
            bool validado = true;
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (string.IsNullOrEmpty(row.Cells["IdArticuloMSTKD"].Value.ToString()) || string.IsNullOrEmpty(row.Cells["CantidadMSTKD"].Value.ToString()))
                    {
                        MessageBox.Show("Todas las columnas deben contener valores.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        validado = false;
                        break;
                    }
                }
            }
            return validado;
        }

        private void dgvDatos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }
}
