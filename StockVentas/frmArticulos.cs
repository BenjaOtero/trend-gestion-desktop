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

namespace StockVentas
{
    public partial class frmArticulos : Form
    {
        public DataSet dt = null;
        private DataTable tblGeneros;
        private DataTable tblProveedores;
        private DataView viewArticulos;
        public frmProgress progreso;
        private frmStockEntradas formEntradas = null;
        private frmStockMov formStockMov = null;
        private frmStockComp formStockComp = null;
        private frmVentas formVentas = null;
        private DataTable tblArticulos;
        private bool origenMenu = false;

        public enum FormState
        {
            inicial,
            edicion,
            insercion,
            eliminacion
        }

        public frmArticulos()
        {
            InitializeComponent();
            this.tblArticulos = BL.GetDataBLL.Articulos();
            origenMenu = true;
        }

        public frmArticulos(ref frmStockEntradas f, DataTable tblArticulos)
        {
            InitializeComponent();
            formEntradas = f;
            this.tblArticulos = tblArticulos;
            btnBorrar.Enabled = false;
            btnEditar.Enabled = false;
            btnGenerar.Enabled = false;
        }

        public frmArticulos(ref frmStockMov f, DataTable tblArticulos)
        {
            InitializeComponent();
            formStockMov = f;
            this.tblArticulos = tblArticulos;
            btnBorrar.Enabled = false;
            btnEditar.Enabled = false;
            btnGenerar.Enabled = false;
        }

        public frmArticulos(ref frmStockComp f, DataTable tblArticulos)
        {
            InitializeComponent();
            formStockComp = f;
            this.tblArticulos = tblArticulos;
            btnBorrar.Enabled = false;
            btnEditar.Enabled = false;
            btnGenerar.Enabled = false;
        }

        public frmArticulos(ref frmVentas f, DataTable tblArticulos)
        {
            InitializeComponent();
            formVentas = f;
            this.tblArticulos = tblArticulos;
            btnBorrar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            cmbGenero.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbGenero.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            cmbProveedor.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Cursor.Current = Cursors.WaitCursor;
            Clipboard.Clear();

            tblGeneros = BL.GetDataBLL.Generos();
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

            tblProveedores = BL.GetDataBLL.Proveedores();
            cmbProveedor.ValueMember = "IdProveedorPRO";
            cmbProveedor.DisplayMember = "RazonSocialPRO";
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;
            cmbProveedor.DataSource = tblProveedores;
            cmbProveedor.SelectedValue = -1;
            AutoCompleteStringCollection proveedorColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblProveedores.Rows)
            {
                proveedorColection.Add(Convert.ToString(row["RazonSocialPRO"]));
            }
            cmbProveedor.AutoCompleteCustomSource = proveedorColection;
            cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tblArticulos.TableName = "Articulos";
            bindingSource1.DataSource = tblArticulos;
            bindingNavigator1.BindingSource = bindingSource1;
            viewArticulos = new DataView(tblArticulos);
            tblArticulos.PrimaryKey = new DataColumn[] { tblArticulos.Columns["IdArticuloART"] };
            bindingSource1.Filter = "IdArticuloART LIKE '000000000'";
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.Columns["IdArticuloART"].HeaderText = "Código";
            gvwDatos.Columns["DescripcionART"].HeaderText = "Descripción";
            gvwDatos.Columns["PrecioCostoART"].HeaderText = "Precio costo";
            gvwDatos.Columns["PrecioPublicoART"].HeaderText = "Precio público";
            gvwDatos.Columns["PrecioMayorART"].HeaderText = "Precio mayor";
            gvwDatos.Columns["RazonSocialPRO"].HeaderText = "Proveedor";
            gvwDatos.Columns["IdItemART"].Visible = false;
            gvwDatos.Columns["IdGeneroART"].Visible = false;
            gvwDatos.Columns["IdColorART"].Visible = false;
            gvwDatos.Columns["IdAliculotaIvaART"].Visible = false;
            gvwDatos.Columns["TalleART"].Visible = false;
            gvwDatos.Columns["IdProveedorART"].Visible = false;
            gvwDatos.Columns["DescripcionWebART"].Visible = false;
            gvwDatos.Columns["FechaART"].Visible = false;
            gvwDatos.Columns["ImagenART"].Visible = false;
            gvwDatos.Columns["ImagenBackART"].Visible = false;
            gvwDatos.Columns["ImagenColorART"].Visible = false;
            gvwDatos.Columns["ActivoWebART"].Visible = false;
            gvwDatos.Columns["NuevoART"].Visible = false;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7);
            if (origenMenu) SetStateForm(FormState.inicial);    
            Cursor.Current = Cursors.Arrow;
            txtParametros.Focus();
        }

        private void txtParametros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) btnBuscar.PerformClick();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string parametros = this.txtParametros.Text;
            string proveedor = cmbProveedor.Text;
            string genero = string.Empty;            
            if (cmbGenero.SelectedValue != null) genero = cmbGenero.SelectedValue.ToString();
            if (rdArticulo.Checked == true)
            {
                try
                {
                    bindingSource1.Filter = "IdArticuloART LIKE '" + parametros + "*' AND RazonSocialPRO LIKE '" + proveedor + "*' AND IdGeneroART LIKE '" + genero + "*'";
                    if (bindingSource1.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (EvaluateException) {
                    MessageBox.Show("Introdujo un caracter no válido");
                }

            }
            else
            {
                try
                {
                    bindingSource1.Filter = "DescripcionART LIKE '*" + parametros + "*' AND RazonSocialPRO LIKE '" + proveedor + "*' AND IdGeneroART LIKE '" + genero + "*'";
                    if (bindingSource1.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (EvaluateException)
                {
                    MessageBox.Show("Introdujo un caracter no válido");
                }
            }
            SetStateForm(FormState.inicial);
            Cursor.Current = Cursors.Arrow;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            frmArticulosGenerarInter generar = new frmArticulosGenerarInter(bindingSource1);
            generar.FormClosed += frmArticulosGenerarInter_FormClosed;
            generar.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataRowView vistaFilaActual = (DataRowView)gvwDatos.CurrentRow.DataBoundItem;
            string cod = vistaFilaActual["IdArticuloART"].ToString();
            if(cod.StartsWith("00000000")) return;
            frmArticulosDetalle detalle = new frmArticulosDetalle(tblArticulos, vistaFilaActual);
            detalle.Show();
            Cursor.Current = Cursors.Arrow;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            frmProgress frm = new frmProgress(tblArticulos, "frmArticulosBorrar", "cargar");
            frm.Show();
        }        

        private void btnEditarNuevos_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmArticulosEditNewsInter frmNewsInter = new frmArticulosEditNewsInter(tblArticulos);
            frmNewsInter.Show();
            Cursor.Current = Cursors.Arrow;
        }

        private void btnActualizarPrecios_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string codigo = gvwDatos.CurrentRow.Cells["IdArticuloART"].Value.ToString();
            if (codigo.StartsWith("00000000")) return;
            codigo = codigo.Substring(0, codigo.Length - 4);
            frmArticulosPrecios frm = new frmArticulosPrecios(tblArticulos, codigo);
            frm.FormClosed += frmArticulosPrecios_FormClosed;
            frm.Show();
            Cursor.Current = Cursors.Arrow;
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            frmProgress frm = new frmProgress("frmArticulosAgrupar", "cargar");
            frm.Show();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (formEntradas != null)
            {
                if (gvwDatos.CurrentRow != null)
                {
                    formEntradas.idArticulo = ((DataRowView)gvwDatos.CurrentRow.DataBoundItem)["IdArticuloART"].ToString();
                }   
            }
            if (formStockMov != null)
            {
                if (gvwDatos.CurrentRow != null)
                {
                    formStockMov.idArticulo = ((DataRowView)gvwDatos.CurrentRow.DataBoundItem)["IdArticuloART"].ToString();
                }         
            }
            if (formStockComp != null)
            {
                if (gvwDatos.CurrentRow != null)
                {
                    formStockComp.idArticulo = ((DataRowView)gvwDatos.CurrentRow.DataBoundItem)["IdArticuloART"].ToString();
                }   
            }
            if (formVentas != null)
            {
                if (gvwDatos.CurrentRow != null)
                {
                    formVentas.idArticulo = ((DataRowView)gvwDatos.CurrentRow.DataBoundItem)["IdArticuloART"].ToString();
                }   
            }
            Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvwDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAceptar.PerformClick();
        }

        public void SetStateForm(FormState state)
        {
            if (state == FormState.inicial)
            {
                if (gvwDatos.RowCount == 0)
                {
                    btnEditar.Enabled = false;
                    btnAceptar.Enabled = false;
                    btnActualizarPrecios.Enabled = false;
                }
                else
                {
                    gvwDatos.Enabled = true;
                    btnBuscar.Enabled = true;
                    btnEditar.Enabled = true;
                    btnAceptar.Enabled = true;
                    btnActualizarPrecios.Enabled = true;
                }
            }
            if (state == FormState.insercion)
            {
                gvwDatos.Enabled = false;
                btnBuscar.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
            }
            if (state == FormState.edicion)
            {
                btnBuscar.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
            }
        }

        void frmArticulosPrecios_FormClosed(object sender, FormClosedEventArgs e)
        {
            string parametros = this.txtParametros.Text;
            if (rdArticulo.Checked == true)
            {
                bindingSource1.Filter = "IdArticuloART LIKE '" + parametros + "*'";
                if (bindingSource1.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                bindingSource1.Filter = "DescripcionART LIKE '*" + parametros + "*'";
                if (bindingSource1.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros coincidentes", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        void frmArticulosGenerarInter_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bindingSource1.Sort = "DescripcionART";
            Cursor.Current = Cursors.Arrow;
        }

    }
}
