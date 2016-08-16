using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using BL;
using Entities;
using System.Media;
using Microsoft.VisualBasic;

namespace StockVentas
{
    public partial class frmStockComp : Form
    {
        private frmStockComp instanciaStockComp;
        public DataSet dsStockMov;
        public DataSet dsStock;
        DataTable tblStockMov;
        DataTable tblStockMovDetalle;
        public DataView viewStockMov;
        DataView viewStockMovDetalle;
        public DataRowView rowView;
        DataTable tblLocales;
        DataTable tblArticulos;
        DataView viewOrigen;
        DataView viewDestino;
        public string PK = string.Empty;
        frmProgress progreso;
        public string idArticulo;
        private bool grabacionCorrecta;
        bool formClosing = false;
        private int? codigoError = null;

        public frmStockComp()
        {
            InitializeComponent();
        }

        public frmStockComp(DataSet dsStockMov)
        {
            InitializeComponent();
            this.dsStockMov = dsStockMov;
            dsStockMov.DataSetName = "dsStockMov";
        }

        public void cargarCombos()
        {
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
            cmbDestino.SelectedIndex = -1;
            cmbDestino.SelectedIndexChanged += new EventHandler(this.validarMaestro);
        }

        private void frmStockMov_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            instanciaStockComp = this;
            grabacionCorrecta = true;
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.CausesValidation = true;
            dgvDatos.ShowCellErrors = true;
            cargarCombos();
            tblArticulos = BL.GetDataBLL.Articulos();
            tblArticulos.TableName = "Articulos";
            if (PK == "") //registro nuevo
            {
                tblStockMov = BL.StockMovBLL.GetTabla();
                tblStockMovDetalle = BL.StockMovDetalleBLL.GetTabla();
                tblStockMovDetalle.PrimaryKey = new DataColumn[] { tblStockMovDetalle.Columns["IdMSTKD"] };
                dsStockMov = new DataSet();
                dsStockMov.DataSetName = "dsStockMov";
                dsStockMov.Tables.Add(tblStockMov);
                dsStockMov.Tables.Add(tblStockMovDetalle);
                viewStockMov = new DataView(tblStockMov);
                viewStockMovDetalle = new DataView(tblStockMovDetalle);
                lblNro.ForeColor = System.Drawing.Color.DarkRed;
                Random rand = new Random();
                int clave = rand.Next(1, 2000000000);
                lblNro.Text = clave.ToString();
                rowView = viewStockMov.AddNew();
                rowView["IdMovMSTK"] = clave.ToString();
                rowView["FechaMSTK"] = DateTime.Today;
                rowView["OrigenMSTK"] = 1;
                rowView["CompensaMSTK"] = 1;
                rowView.EndEdit();
            }
            else // editar registros
            {
                tblStockMov = dsStockMov.Tables[0];
                tblStockMovDetalle = dsStockMov.Tables[1];
                tblStockMovDetalle.PrimaryKey = new DataColumn[] { tblStockMovDetalle.Columns["IdMSTKD"] };
                viewStockMov = new DataView(tblStockMov);
                viewStockMovDetalle = new DataView(tblStockMovDetalle);
                viewStockMov.RowFilter = "IdMovMSTK = '" + PK + "'";
                rowView = viewStockMov[0];
                viewStockMovDetalle.RowFilter = "IdMovMSTKD = '" + PK + "'";
                lblNro.Text = viewStockMov[0]["IdMovMSTK"].ToString();
                cmbOrigen.Enabled = false;
                cmbDestino.Enabled = false;
                dgvDatos.Enabled = true;
                cmbDestino.SelectedIndexChanged -= new EventHandler(this.validarMaestro);
            }
            dateTimePicker1.DataBindings.Add("Text", rowView, "FechaMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbOrigen.DataBindings.Add("SelectedValue", rowView, "OrigenMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbDestino.DataBindings.Add("SelectedValue", rowView, "DestinoMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
            rowView.CancelEdit();
            bindingSource1.DataSource = viewStockMovDetalle;
            bindingNavigator1.BindingSource = bindingSource1;
            dgvDatos.AutoGenerateColumns = false;
            AddColumns();
            dgvDatos.DataSource = bindingSource1;
            dgvDatos.AllowUserToOrderColumns = false;
            dgvDatos.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvDatos.Enabled = false;
            cmbOrigen.Visible = false;
            btnArticulos.Enabled = false;
            this.dgvDatos.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.ValidarFila);
        }

        private void frmStockMov_Activated(object sender, EventArgs e)
        {
            if (PK != "")
            {
                foreach (DataGridViewRow fila in dgvDatos.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        string articulo = fila.Cells["IdArticuloMSTKD"].Value.ToString();
                        DataRow[] foundRow = tblArticulos.Select("IdArticuloART = '" + articulo + "'");
                        fila.Cells["Descripcion"].Value = foundRow[0]["DescripcionART"].ToString();
                    }
                }
                dgvDatos.Enabled = true;
            }
            dgvDatos.Columns["IdMSTKD"].Visible = false;
        }

        public void ValidarOrigenDestino(object sender, EventArgs e)
        {
            ComboBox cmb;
            if (sender is ComboBox)
            {
                cmb = (ComboBox)sender;
                if (cmb.Name == cmbOrigen.Name)
                {
                    if (cmb.SelectedValue != null)
                    {
                        Object selectedItem = cmbDestino.SelectedItem;
                        if (cmb.SelectedValue.ToString() == "1") //1 es Local entradas
                        {
                            viewDestino.RowFilter = "IdLocalLOC <>'1' AND IdLocalLOC <>'2'";
                            cmbDestino.ValueMember = "IdLocalLOC";
                            cmbDestino.DisplayMember = "NombreLOC";
                            cmbDestino.DropDownStyle = ComboBoxStyle.DropDownList;
                            cmbDestino.DataSource = viewDestino;
                            if (selectedItem != null)
                            {
                                cmbDestino.SelectedItem = selectedItem;
                            }
                            else
                            {
                                cmbDestino.SelectedIndex = -1;
                            }
                        }
                        else
                        {
                            viewDestino.RowFilter = "IdLocalLOC <>'1' AND IdLocalLOC <>'" + cmb.SelectedValue.ToString() + "'";
                            cmbDestino.ValueMember = "IdLocalLOC";
                            cmbDestino.DisplayMember = "NombreLOC";
                            cmbDestino.DropDownStyle = ComboBoxStyle.DropDownList;
                            cmbDestino.DataSource = viewDestino;
                            if (selectedItem != null)
                            {
                                cmbDestino.SelectedItem = selectedItem;
                            }
                            else
                            {
                                cmbDestino.SelectedIndex = -1;
                            }
                        }
                    }
                    if (cmbOrigen.SelectedValue != null)
                    {
                        if (dgvDatos.RowCount > 0)
                        {
                            foreach (DataGridViewRow row in dgvDatos.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    row.Cells["OrigenMSTKD"].Value = cmbOrigen.SelectedValue;
                                }
                            }
                        }
                    }
                }
                if (cmb.Name == cmbDestino.Name)
                {
                    if (cmb.SelectedValue != null)
                    {
                        Object selectedItem = cmbOrigen.SelectedItem;
                        if (cmb.SelectedValue.ToString() == "2") //2 es local Salidas
                        {
                            viewOrigen.RowFilter = "IdLocalLOC <>'1' AND IdLocalLOC <>'2'";
                            cmbOrigen.ValueMember = "IdLocalLOC";
                            cmbOrigen.DisplayMember = "NombreLOC";
                            cmbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
                            cmbOrigen.DataSource = viewOrigen;
                            if (selectedItem != null)
                            {
                                cmbOrigen.SelectedItem = selectedItem;
                            }
                            else
                            {
                                cmbOrigen.SelectedIndex = -1;
                            }
                        }
                        else
                        {
                            viewOrigen.RowFilter = "IdLocalLOC <>'2' AND IdLocalLOC <>'" + cmb.SelectedValue.ToString() + "'";
                            cmbOrigen.ValueMember = "IdLocalLOC";
                            cmbOrigen.DisplayMember = "NombreLOC";
                            cmbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
                            cmbOrigen.DataSource = viewOrigen;
                            if (selectedItem != null)
                            {
                                cmbOrigen.SelectedItem = selectedItem;
                            }
                            else
                            {
                                cmbOrigen.SelectedIndex = -1;
                            }
                        }
                    }
                    if (cmbDestino.SelectedValue != null)
                    {
                        if (dgvDatos.RowCount > 0)
                        {
                            foreach (DataGridViewRow row in dgvDatos.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    row.Cells["DestinoMSTKD"].Value = cmbDestino.SelectedValue;
                                }
                            }
                        }
                    }
                }
              //  validarMaestro();
            }

        }

        public void validarMaestro(object sender, EventArgs e)
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

        private void dgvDatos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["CompensaMSTKD"].Value = 0;
        }

        private void dgvDatos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.RowCount > 1)
            {
                btnGrabar.Enabled = true;
            }
            btnArticulos.Enabled = true;
        }

        private void dgvDatos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.CurrentCell.OwningColumn.Name == "Descripcion")
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dgvDatos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (String.IsNullOrEmpty(dgvDatos["IdMSTKD", e.RowIndex].Value.ToString()))
            {
                Random rand = new Random();
                int clave = rand.Next(1, 2000000000);
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
            if (dgvDatos.CurrentRow.IsNewRow) return;
            dgvDatos.Rows[e.RowIndex].ErrorText = String.Empty;
            if (dgvDatos.CurrentCell.OwningColumn.Name == "IdArticuloMSTKD")
            {
                string articulo = dgvDatos.CurrentCell.EditedFormattedValue.ToString();
                DataRow[] foundRow = tblArticulos.Select("IdArticuloART = '" + articulo + "'");
                if (foundRow.Length != 0)
                {
                    DataRow filaActual = foundRow[0];
                    var fila = dgvDatos.CurrentCell.RowIndex;
                    this.dgvDatos["IdArticuloMSTKD", fila].Value = filaActual["IdArticuloART"].ToString();
                    this.dgvDatos["Descripcion", fila].Value = filaActual["DescripcionART"].ToString();
                    this.dgvDatos["DescripcionART", fila].Value = filaActual["DescripcionART"].ToString();
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
                if (result == false)
                {
                    dgvDatos.Rows[e.RowIndex].ErrorText = "Debe ingresar un valor numérico";
                    e.Cancel = true;
                }
            }
        }

        private void ValidarFila(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvDatos.CurrentRow.IsNewRow)
            {
                return;
            }
            if (dgvDatos["IdArticuloMSTKD", e.RowIndex].Value == null || dgvDatos["IdArticuloMSTKD", e.RowIndex].Value.ToString() == "") // Articulo
            {
                dgvDatos.Rows[e.RowIndex].ErrorText = "Debe ingresar un código de artículo";
                dgvDatos.CurrentCell = dgvDatos["IdArticuloMSTKD", e.RowIndex];
                e.Cancel = true;
            }
            var dato = dgvDatos["CantidadMSTKD", e.RowIndex].Value;
            bool result = Information.IsNumeric(dato);
            if (result == false)
            {
                dgvDatos.Rows[e.RowIndex].ErrorText = "Debe ingresar un valor numérico en la columna CANTIDAD";
                dgvDatos.CurrentCell = dgvDatos["CantidadMSTKD", e.RowIndex];
                e.Cancel = true;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (tblStockMovDetalle.GetChanges() != null)
            {
                if (MessageBox.Show("¿Confirma la grabación de datos?", "Trend Gestión",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                grabar();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnArticulos_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmArticulos articulos = new frmArticulos(ref instanciaStockComp, tblArticulos);
            articulos.Show(this);
            articulos.FormClosed += frmArticulos_FormClosed;
            Cursor.Current = Cursors.Arrow;
        }

        private void frmStockMov_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tblStockMovDetalle.GetChanges() != null)
            {
                DialogResult respuesta = MessageBox.Show("¿Confirma la grabación de datos?", "Trend Gestión",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (respuesta)
                {
                    case DialogResult.Yes:
                        formClosing = true;
                        grabar();
                        break;
                    case DialogResult.No:
                        tblStockMovDetalle.RejectChanges();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            rowView.EndEdit();
            BL.TransaccionesBLL.GrabarStockMovimientos(dsStockMov, ref codigoError);
            Cursor.Current = Cursors.WaitCursor;
            switch (codigoError)
            {
                case null:
                    break;
                case 0:
                    MessageBox.Show("Procedure or function cannot be found in database. No se grabaron los datos. Consulte al administrador del sistema.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 1042:
                    MessageBox.Show("Unable to connect to any of the specified MySQL hosts. No se grabaron los datos. Consulte al administrador del sistema.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Se produjo un error inesperado. No se grabaron los datos. Consulte al administrador del sistema."
                                    , "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            ResetForm();
        }

        private void ResetForm()
        {
            if (string.IsNullOrEmpty(PK) && !formClosing)
            {
                Random rand = new Random();
                int clave = rand.Next(1, 2000000000);
                lblNro.Text = clave.ToString();
                rowView = null;
                rowView = viewStockMov.AddNew();
                rowView["IdMovMSTK"] = clave.ToString();
                rowView["FechaMSTK"] = DateTime.Today;
                rowView["OrigenMSTK"] = 1;
                rowView["CompensaMSTK"] = 1;
                rowView.EndEdit();
                dateTimePicker1.DataBindings.Clear();
                cmbOrigen.DataBindings.Clear();
                cmbDestino.DataBindings.Clear();
                dateTimePicker1.DataBindings.Add("Text", rowView, "FechaMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
                cmbOrigen.DataBindings.Add("SelectedValue", rowView, "OrigenMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
                cmbDestino.DataBindings.Add("SelectedValue", rowView, "DestinoMSTK", false, DataSourceUpdateMode.OnPropertyChanged);
                //     cmbDestino.SelectedIndexChanged -= new EventHandler(validarMaestro);
                cargarCombos();
                foreach (DataRow row in tblStockMovDetalle.Rows)
                {
                    row.Delete();
                }
                tblStockMovDetalle.AcceptChanges();
                cmbDestino.Focus();
            }
            if (!string.IsNullOrEmpty(PK)) Close();
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
                    SendKeys.Send("{TAB}");
                } 
            }
        }

        private void AddColumns()
        {
            DataGridViewTextBoxColumn IdMSTKD = new DataGridViewTextBoxColumn();
            IdMSTKD.Name = "IdMSTKD";
            IdMSTKD.DataPropertyName = "IdMSTKD";
            IdMSTKD.Visible = false;

            DataGridViewTextBoxColumn IdMovMSTKD = new DataGridViewTextBoxColumn();
            IdMovMSTKD.Name = "IdMovMSTKD";
            IdMovMSTKD.DataPropertyName = "IdMovMSTKD";
            IdMovMSTKD.Visible = false;

            DataGridViewTextBoxColumn IdArticuloMSTKD = new DataGridViewTextBoxColumn();
            IdArticuloMSTKD.Name = "IdArticuloMSTKD";
            IdArticuloMSTKD.HeaderText = "Código";
            IdArticuloMSTKD.DataPropertyName = "IdArticuloMSTKD";

            DataGridViewTextBoxColumn DescripcionART = new DataGridViewTextBoxColumn();
            DescripcionART.Name = "DescripcionART";
            DescripcionART.DataPropertyName = "DescripcionART";
            DescripcionART.Visible = false;

            DataGridViewTextBoxColumn columnaDescripcion = new DataGridViewTextBoxColumn();
            columnaDescripcion.Name = "Descripcion";
            columnaDescripcion.HeaderText = "Descripción";
            columnaDescripcion.Width = 500;
            columnaDescripcion.ReadOnly = true;

            DataGridViewTextBoxColumn CantidadMSTKD = new DataGridViewTextBoxColumn();
            CantidadMSTKD.Name = "CantidadMSTKD";
            CantidadMSTKD.DataPropertyName = "CantidadMSTKD";
            CantidadMSTKD.Width = 100;
            CantidadMSTKD.HeaderText = "Cantidad";

            DataGridViewTextBoxColumn CompensaMSTKD = new DataGridViewTextBoxColumn();
            CompensaMSTKD.Name = "CompensaMSTKD";
            CompensaMSTKD.DataPropertyName = "CompensaMSTKD";
            CompensaMSTKD.Visible = false;

            DataGridViewTextBoxColumn OrigenMSTKD = new DataGridViewTextBoxColumn();
            OrigenMSTKD.Name = "OrigenMSTKD";
            OrigenMSTKD.DataPropertyName = "OrigenMSTKD";
            OrigenMSTKD.Visible = false;

            DataGridViewTextBoxColumn DestinoMSTKD = new DataGridViewTextBoxColumn();
            DestinoMSTKD.Name = "DestinoMSTKD";
            DestinoMSTKD.DataPropertyName = "DestinoMSTKD";
            DestinoMSTKD.Visible = false;

            dgvDatos.Columns.Add(IdMSTKD);
            dgvDatos.Columns.Add(IdMovMSTKD);
            dgvDatos.Columns.Add(IdArticuloMSTKD);
            dgvDatos.Columns.Add(DescripcionART);
            dgvDatos.Columns.Add(columnaDescripcion);
            dgvDatos.Columns.Add(CantidadMSTKD);
            dgvDatos.Columns.Add(CompensaMSTKD);
            dgvDatos.Columns.Add(OrigenMSTKD);
            dgvDatos.Columns.Add(DestinoMSTKD);
        }

        public bool GrabacionCorrecta
        {
            get
            {
                return grabacionCorrecta;
            }
            set
            {
                grabacionCorrecta = value;
            }
        }

    }
}
