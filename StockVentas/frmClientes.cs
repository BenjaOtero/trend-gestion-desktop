using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BL;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;

namespace StockVentas
{
    public partial class frmClientes : Form
    {
        frmVentas instanciaVentas = null;
        DataSet dsClientes;
        private DataTable tblClientes;
        DataTable tblFallidas;
        bool editando;
        bool insertando;
        string buscado = string.Empty;
        private const int CP_NOCLOSE_BUTTON = 0x200;  //junto con protected override CreateParams inhabilitan el boton cerrar del formularios

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 

        public enum FormState
        {
            inicial,
            edicion,
            insercion,
            eliminacion
        }

        public frmClientes()
        {
            InitializeComponent();
            gvwDatos.EnableHeadersVisualStyles = false;
         //   gvwDatos.RowHeadersVisible = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush =
                new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(0, 0, 0), Color.FromArgb(57, 128, 227), 65f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        public frmClientes(ref frmVentas instanciaVentas)
        {
            InitializeComponent();
            this.instanciaVentas = instanciaVentas;
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            dsClientes = BL.ClientesBLL.GetClientes(1);
            tblClientes = dsClientes.Tables[0];
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblClientes);
            tblFallidas = new DataTable();
            tblFallidas.TableName = "ClientesFallidas";
            tblFallidas.Columns.Add("Id", typeof(int));
            tblFallidas.Columns.Add("Accion", typeof(string));
            tblFallidas.Columns["Id"].Unique = true;
            tblFallidas.PrimaryKey = new DataColumn[] { tblFallidas.Columns["Id"] };
            DataView viewClientes = new DataView(tblClientes);
            bindingSource1.DataSource = tblClientes;
            bindingNavigator1.BindingSource = bindingSource1;
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            grpBotones.CausesValidation = false;
            btnCancelar.CausesValidation = false;
            btnSalir.CausesValidation = false;
            Dictionary<Int32, String> condiciones = new Dictionary<int, string>();
            condiciones.Add(1, "Consumidor Final");
            condiciones.Add(2, "Responsable Inscripto");
            condiciones.Add(3, "Responsable Monotributo");
            cmbCondicion.DataSource = new BindingSource(condiciones, null);
            cmbCondicion.DisplayMember = "Value";
            cmbCondicion.ValueMember = "Value";
            cmbCondicion.DataBindings.Add("SelectedValue", bindingSource1, "CondicionIvaCLI", false, DataSourceUpdateMode.OnPropertyChanged);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["IdClienteCLI"].DisplayIndex = 0;
            gvwDatos.Columns["ApellidoCLI"].DisplayIndex = 1;
            gvwDatos.Columns["NombreCLI"].DisplayIndex = 2;
            gvwDatos.Columns["CorreoCLI"].DisplayIndex = 3;
            gvwDatos.Columns["IdClienteCLI"].HeaderText = "Nº cliente";
            gvwDatos.Columns["NombreCLI"].HeaderText = "Nombre";
            gvwDatos.Columns["ApellidoCLI"].HeaderText = "Apellido";
            gvwDatos.Columns["CorreoCLI"].HeaderText = "Correo";
            gvwDatos.Columns["RazonSocialCLI"].Visible = false;
            gvwDatos.Columns["CUIT"].Visible = false;
            gvwDatos.Columns["DireccionCLI"].Visible = false;
            gvwDatos.Columns["LocalidadCLI"].Visible = false;
            gvwDatos.Columns["ProvinciaCLI"].Visible = false;
            gvwDatos.Columns["TransporteCLI"].Visible = false;
            gvwDatos.Columns["ContactoCLI"].Visible = false;
            gvwDatos.Columns["TelefonoCLI"].Visible = false;
            gvwDatos.Columns["MovilCLI"].Visible = false;
            gvwDatos.Columns["FechaNacCLI"].Visible = false;
            gvwDatos.Columns["CondicionIvaCLI"].Visible = false;
            gvwDatos.Columns["NombreCompletoCLI"].Visible = false;
            bindingSource1.Sort = "ApellidoCLI ASC, NombreCLI ASC";
            int itemFound = bindingSource1.Find("RazonSocialCLI", "PUBLICO");
            bindingSource1.Position = itemFound;
            SetStateForm(FormState.inicial);            
        }

        private void txtParametros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) btnBuscar.PerformClick();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string parametros = txtParametros.Text;
            bindingSource1.Filter = "NombreCLI LIKE '" + parametros + "*' OR ApellidoCLI LIKE '" + parametros + "*' OR CorreoCLI LIKE '" + parametros + "*'";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            Random rand = new Random();
            int clave = rand.Next(1, 2000000000);
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdClienteCLI.ReadOnly = false;
            txtIdClienteCLI.Text = clave.ToString();
            txtIdClienteCLI.ReadOnly = true;
            txtRazonSocialCLI.Focus();
            SetStateForm(FormState.insercion);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtRazonSocialCLI.Text == "PUBLICO")
            {
                MessageBox.Show("No se puede modificar el registro porque es un registro propio del sistema.", "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (bindingSource1.Count == 0) return;
            SetStateForm(FormState.edicion);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Count == 0) return;
            if (txtRazonSocialCLI.Text == "PUBLICO")
            {
                MessageBox.Show("No se puede borrar el registro porque es un registro propio del sistema.", "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            
            if (MessageBox.Show("¿Desea borrar este registro?", "Trend Gestión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingSource1.RemoveCurrent();
                Grabar();
            }
            SetStateForm(FormState.inicial); 
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            buscado = txtCorreoCLI.Text;
            Grabar();
            SetStateForm(FormState.inicial);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (insertando) bindingSource1.RemoveCurrent();
            bindingSource1.CancelEdit();
            SetStateForm(FormState.inicial);
            errorProvider1.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.RemoveFilter();
            if (instanciaVentas != null) instanciaVentas.idCliente = Convert.ToInt32(txtIdClienteCLI.Text);
        }

        private void Grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                bindingSource1.EndEdit();
                if (tblClientes.GetChanges() != null)
                {                    
                    BL.ClientesBLL.GrabarDB(tblClientes);
                }
                bindingSource1.RemoveFilter();
                if (insertando || editando)
                {
                    int itemFound = bindingSource1.Find("CorreoCLI", buscado);
                    bindingSource1.Position = itemFound;
                }
                bindingSource1.Sort = "ApellidoCLI ASC, NombreCLI ASC";
            }
            catch (ConstraintException)
            {
                string mensaje;
                if (insertando)
                {
                    mensaje = "No se puede agregar el cliente '" + txtNombreCLI.Text.ToUpper() + " " + txtApellidoCLI.Text.ToUpper() +
                        "' porque ya existe otro con el mismo correo electrónico.";
                    MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.RemoveCurrent();
                }
                if (editando)
                {
                    mensaje = "No se puede modificar el cliente '" + txtNombreCLI.Text.ToUpper() + " " + txtApellidoCLI.Text.ToUpper() +
                        "' porque ya existe otro con el mismo correo electrónico.";
                    MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.CancelEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Arrow;
        } 

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtNombreCLI.Text))
            {
                this.errorProvider1.SetError(txtNombreCLI, "Debe escribir un nombre.");
                txtNombreCLI.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtApellidoCLI.Text))
            {
                this.errorProvider1.SetError(txtApellidoCLI, "Debe escribir un apellido.");
                txtApellidoCLI.Focus();
                return false;
            }
            if (!IsValidEmail(txtCorreoCLI.Text))
            {
                this.errorProvider1.SetError(txtCorreoCLI, "Verifique la dirección de correo electrónico.");
                return false;
            }
            return true;
        }

        private void ValidarCampos(object sender, CancelEventArgs e)
        {
            if ((sender == (object)txtNombreCLI))
            {
                if (string.IsNullOrEmpty(txtNombreCLI.Text))
                {
                    this.errorProvider1.SetError(txtNombreCLI, "Debe escribir un nombre.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtApellidoCLI))
            {
                if (string.IsNullOrEmpty(txtApellidoCLI.Text))
                {
                    this.errorProvider1.SetError(txtApellidoCLI, "Debe escribir un apellido.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtCorreoCLI))
            {
                if (!IsValidEmail(txtCorreoCLI.Text))
                {
                    this.errorProvider1.SetError(txtCorreoCLI, "Verifique la dirección de correo electrónico.");
                    e.Cancel = true;
                }
            }
        }

        private void CamposValidado(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void AddEventosValidacion()
        {
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox || ctl is ComboBox)
                {
                    ctl.Validating += new System.ComponentModel.CancelEventHandler(this.ValidarCampos);
                    ctl.Validated += new System.EventHandler(this.CamposValidado);
                }
            }
        }

        private void DelEventosValidacion()
        {
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox || ctl is ComboBox)
                {
                    ctl.Validating -= new System.ComponentModel.CancelEventHandler(this.ValidarCampos);
                    ctl.Validated -= new System.EventHandler(this.CamposValidado);
                }
            }
            this.errorProvider1.Clear();
        }

        public static bool IsValidEmail(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void SetStateForm(FormState state)
        {
            if (state == FormState.inicial)
            {
                gvwDatos.Enabled = true;
                txtIdClienteCLI.ReadOnly = true;
                txtRazonSocialCLI.ReadOnly = true;
                txtNombreCLI.ReadOnly = true;
                txtApellidoCLI.ReadOnly = true;
                txtCUIT.ReadOnly = true;
                txtDireccionCLI.ReadOnly = true;
                txtLocalidadCLI.ReadOnly = true;
                txtProvinciaCLI.ReadOnly = true;
                txtTransporteCLI.ReadOnly = true;
                txtContactoCLI.ReadOnly = true;
                txtTelefonoCLI.ReadOnly = true;
                txtMovilCLI.ReadOnly = true;
                txtCorreoCLI.ReadOnly = true;
                txtFechaNacCLI.ReadOnly = true;
                btnBuscar.Enabled = true;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnBorrar.Enabled = true;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = false;
                btnSalir.Enabled = true;
                DelEventosValidacion();
                editando = false;
                insertando = false;
                txtParametros.Focus();
            }
            if (state == FormState.insercion)
            {
                gvwDatos.Enabled = false;
                txtRazonSocialCLI.ReadOnly = false;
                txtNombreCLI.ReadOnly = false;
                txtApellidoCLI.ReadOnly = false;
                txtCUIT.ReadOnly = false;
                txtDireccionCLI.ReadOnly = false;
                txtLocalidadCLI.ReadOnly = false;
                txtProvinciaCLI.ReadOnly = false;
                txtTransporteCLI.ReadOnly = false;
                txtContactoCLI.ReadOnly = false;
                txtTelefonoCLI.ReadOnly = false;
                txtMovilCLI.ReadOnly = false;
                txtCorreoCLI.ReadOnly = false;
                txtFechaNacCLI.ReadOnly = false;
                txtIdClienteCLI.Clear();
                txtRazonSocialCLI.Clear();
                txtCUIT.Clear();
                txtDireccionCLI.Clear();
                txtLocalidadCLI.Clear();
                txtProvinciaCLI.Clear();
                txtTransporteCLI.Clear();
                txtContactoCLI.Clear();
                txtTelefonoCLI.Clear();
                txtMovilCLI.Clear();
                txtCorreoCLI.Clear();
                txtFechaNacCLI.Clear();
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnSalir.Enabled = false;
                txtNombreCLI.Focus();
                AddEventosValidacion();
                insertando = true;
            }
            if (state == FormState.edicion)
            {
                gvwDatos.Enabled = false;
                txtRazonSocialCLI.ReadOnly = false;
                txtNombreCLI.ReadOnly = false;
                txtApellidoCLI.ReadOnly = false;
                txtCUIT.ReadOnly = false;
                txtDireccionCLI.ReadOnly = false;
                txtLocalidadCLI.ReadOnly = false;
                txtProvinciaCLI.ReadOnly = false;
                txtTransporteCLI.ReadOnly = false;
                txtContactoCLI.ReadOnly = false;
                txtTelefonoCLI.ReadOnly = false;
                txtMovilCLI.ReadOnly = false;
                txtCorreoCLI.ReadOnly = false;
                txtFechaNacCLI.ReadOnly = false;
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnSalir.Enabled = false;
                txtNombreCLI.Focus();
                AddEventosValidacion();
                editando = true;
            }
        }

    }
}
