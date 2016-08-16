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
    public partial class frmEmpleados : Form
    {
        private DataTable tblEmpleados;
        private DataTable tblLocales;
        private const int CP_NOCLOSE_BUTTON = 0x200;  //junto con protected override CreateParams inhabilitan el boton cerrar de frmProgress

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

        public frmEmpleados()
        {
            InitializeComponent();
            txtDniEMP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumeros);
            txtTelefonoEMP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumeros);
            txtSalarioEMP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtCargasSocialesEMP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            this.Text = "Empleados";
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblEmpleados = BL.GetDataBLL.Empleados();
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblEmpleados);
            bindingSource1.DataSource = tblEmpleados;
            bindingNavigator1.BindingSource = bindingSource1;
            tblLocales = BL.GetDataBLL.Locales();
            DataView viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <> 1 AND IdLocalLOC <> 2";
            cmbIdLocalEMP.DataSource = viewLocales;
            cmbIdLocalEMP.DisplayMember = "NombreLOC";
            cmbIdLocalEMP.ValueMember = "IdLocalLOC";
            cmbIdLocalEMP.SelectedValue = -1;
            AutoCompleteStringCollection localesColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblLocales.Rows)
            {
                localesColection.Add(Convert.ToString(row["NombreLOC"]));
            }
            cmbIdLocalEMP.AutoCompleteCustomSource = localesColection;
            cmbIdLocalEMP.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbIdLocalEMP.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbIdLocalEMP.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["IdEmpleadoEMP"].HeaderText = "Nº empleado";
            gvwDatos.Columns["ApellidoEMP"].HeaderText = "Apellido";
            gvwDatos.Columns["NombreEMP"].HeaderText = "Nombre"; ;
            gvwDatos.Columns["DniEMP"].Visible = false;
            gvwDatos.Columns["DireccionEMP"].Visible = false;
            gvwDatos.Columns["TelefonoEMP"].Visible = false;
            gvwDatos.Columns["FechaNacEMP"].Visible = false;
            gvwDatos.Columns["FechaIngresoEMP"].Visible = false;
            gvwDatos.Columns["IdLocalEMP"].Visible = false;
            gvwDatos.Columns["SalarioEMP"].Visible = false;
            gvwDatos.Columns["CargasSocialesEMP"].Visible = false;
            gvwDatos.Columns["Activa"].Visible = false;
            bindingSource1.Sort = "ApellidoEMP";
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            chkActiva.DataBindings.Add(BL.Utilitarios.DataBindingsCheckBoxAdd(bindingSource1, grpCampos));
            bindingSource1.BindingComplete += new BindingCompleteEventHandler(bindingSource1_BindingComplete);
            grpBotones.CausesValidation = false;
            btnCancelar.CausesValidation = false;
            btnSalir.CausesValidation = false;
            txtParametros.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            SetStateForm(FormState.inicial);
        }

        private void txtParametros_Enter(object sender, EventArgs e)
        {
            txtParametros.Clear();
            txtParametros.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void txtParametros_MouseClick(object sender, MouseEventArgs e)
        {
            txtParametros.Clear();
            txtParametros.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string parametros = txtParametros.Text;
            bindingSource1.Filter = "ApellidoEMP LIKE '" + parametros + "*' OR DniEMP LIKE '" + parametros + "'";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox || ctl is ComboBox)
                {
                    ctl.Validating += new System.ComponentModel.CancelEventHandler(this.Validar);
                    ctl.Validated += new System.EventHandler(this.Validado);
                }
            }
            bindingSource1.AddNew();
            Random rand = new Random();
            int clave = rand.Next(1, 2000000000);
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdEmpleadoEMP.ReadOnly = false;
            txtIdEmpleadoEMP.Text = clave.ToString();
            txtIdEmpleadoEMP.ReadOnly = true;
            txtDniEMP.Focus();
            SetStateForm(FormState.insercion);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SetStateForm(FormState.edicion);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea borrar este registro?", "Buscar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingSource1.RemoveCurrent();
                bindingSource1.EndEdit();
            }
            SetStateForm(FormState.inicial);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarFormulario())
                {
                    bindingSource1.EndEdit();
                    bindingSource1.Position = 0;
                    bindingSource1.Sort = "ApellidoEMP";
                    SetStateForm(FormState.inicial);
                    bindingSource1.RemoveFilter();                
                }
            }
            catch (ConstraintException)
            {
                string mensaje = "No se puede agregar el empleado '" + txtDniEMP.Text.ToUpper() + "' porque ya existe un empleado con el mismo número de DNI";
                MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombreEMP.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox || ctl is ComboBox)
                {
                    ctl.Validating -= new System.ComponentModel.CancelEventHandler(this.Validar);
                    ctl.Validated -= new System.EventHandler(this.Validado);
                }
            }
            bindingSource1.CancelEdit();
            SetStateForm(FormState.inicial);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmEmpleados_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false; //
            bindingSource1.EndEdit();
            if (tblEmpleados.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblEmpleados, "frmEmpleados", "grabar");
                progreso.ShowDialog();       
            }
            bindingSource1.RemoveFilter();
        }

        private void Validar(object sender, CancelEventArgs e)
        {
            if ((sender == (object)txtDniEMP))
            {
                if (string.IsNullOrEmpty(txtDniEMP.Text))
                {
                    this.errorProvider1.SetError(txtDniEMP, "Debe escribir un número de DNI.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtNombreEMP))
            {
                if (string.IsNullOrEmpty(txtNombreEMP.Text))
                {
                    this.errorProvider1.SetError(txtNombreEMP, "Debe escribir un nombre.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtApellidoEMP))
            {
                if (string.IsNullOrEmpty(txtApellidoEMP.Text))
                {
                    this.errorProvider1.SetError(txtApellidoEMP, "Debe escribir un nombre.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)cmbIdLocalEMP))
            {
                if (string.IsNullOrEmpty(cmbIdLocalEMP.Text))
                {
                    this.errorProvider1.SetError(cmbIdLocalEMP, "Debe seleccionar un local.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtDireccionEMP))
            {
                if (string.IsNullOrEmpty(txtDireccionEMP.Text))
                {
                    this.errorProvider1.SetError(txtDireccionEMP, "Debe escribir un domicilio.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtTelefonoEMP))
            {
                if (string.IsNullOrEmpty(txtTelefonoEMP.Text))
                {
                    this.errorProvider1.SetError(txtTelefonoEMP, "Debe escribir un teléfono.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtFechaNacEMP))
            {
                if (string.IsNullOrEmpty(txtFechaNacEMP.Text))
                {
                    this.errorProvider1.SetError(txtFechaNacEMP, "Debe escribir una fecha de nacimiento.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtFechaIngresoEMP) && string.IsNullOrEmpty(txtFechaIngresoEMP.Text))
            {
                this.errorProvider1.SetError(txtFechaNacEMP, "Debe escribir una fecha de ingreso.");
                e.Cancel = true;
            }
        }

        private void Validado(object sender, EventArgs e)
        {
            if ((sender == (object)txtDniEMP)) this.errorProvider1.SetError(txtDniEMP, "");
            if ((sender == (object)txtNombreEMP)) this.errorProvider1.SetError(txtNombreEMP, "");
            if ((sender == (object)txtApellidoEMP)) this.errorProvider1.SetError(txtNombreEMP, "");
            if ((sender == (object)cmbIdLocalEMP)) this.errorProvider1.SetError(cmbIdLocalEMP, "");
            if ((sender == (object)txtDireccionEMP)) this.errorProvider1.SetError(txtDireccionEMP, "");
            if ((sender == (object)txtTelefonoEMP)) this.errorProvider1.SetError(txtTelefonoEMP, "");
            if ((sender == (object)txtFechaNacEMP)) this.errorProvider1.SetError(txtFechaNacEMP, "");
            if ((sender == (object)txtFechaIngresoEMP)) this.errorProvider1.SetError(txtFechaIngresoEMP, "");
        }

        private void AddEventosValidacion()
        {
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox || ctl is ComboBox)
                {
                    ctl.Validating += new System.ComponentModel.CancelEventHandler(this.Validar);
                    ctl.Validated += new System.EventHandler(this.Validado);
                }
            }
        }

        private void DelEventosValidacion()
        {
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox || ctl is ComboBox)
                {
                    ctl.Validating -= new System.ComponentModel.CancelEventHandler(this.Validar);
                    ctl.Validated -= new System.EventHandler(this.Validado);
                }
            }
            this.errorProvider1.SetError(txtDniEMP, "");
            this.errorProvider1.SetError(txtNombreEMP, "");
            this.errorProvider1.SetError(txtApellidoEMP, "");
            this.errorProvider1.SetError(cmbIdLocalEMP, "");
            this.errorProvider1.SetError(txtDireccionEMP, "");
            this.errorProvider1.SetError(txtTelefonoEMP, "");
            this.errorProvider1.SetError(txtFechaNacEMP, "");
            this.errorProvider1.SetError(txtFechaIngresoEMP, "");
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtDniEMP.Text))
            {
                this.errorProvider1.SetError(txtDniEMP, "Debe escribir un DNI.");
                txtDniEMP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNombreEMP.Text))
            {
                this.errorProvider1.SetError(txtNombreEMP, "Debe escribir un nombre.");
                txtNombreEMP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtApellidoEMP.Text))
            {
                this.errorProvider1.SetError(txtApellidoEMP, "Debe escribir un apellido.");
                txtApellidoEMP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDireccionEMP.Text))
            {
                this.errorProvider1.SetError(txtDireccionEMP, "Debe escribir un domicilio.");
                txtDireccionEMP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTelefonoEMP.Text))
            {
                this.errorProvider1.SetError(txtTelefonoEMP, "Debe escribir un teléfono.");
                txtTelefonoEMP.Focus();
                return false;
            }
            if (txtFechaNacEMP.Text == "__/__/____")
            {
                this.errorProvider1.SetError(txtFechaNacEMP, "Debe escribir una fecha de nacimiento.");
                txtFechaNacEMP.Focus();
                return false;
            }
            if (txtFechaIngresoEMP.Text == "__/__/____")
            {
                this.errorProvider1.SetError(txtFechaIngresoEMP, "Debe escribir una fecha de ingreso.");
                txtFechaIngresoEMP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cmbIdLocalEMP.Text))
            {
                this.errorProvider1.SetError(cmbIdLocalEMP, "Debe seleccionar un local.");
                cmbIdLocalEMP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSalarioEMP.Text))
            {
                this.errorProvider1.SetError(txtSalarioEMP, "Debe escribir un salario.");
                txtSalarioEMP.Focus();
                return false;
            }
            return true;
        }

        private void bindingSource1_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            // Check if the data source has been updated, and that no error has occured.
            if (e.BindingCompleteContext ==
                BindingCompleteContext.DataSourceUpdate && e.Exception == null)

                // If not, end the current edit.
                e.Binding.BindingManagerBase.EndCurrentEdit();
        }

        public void SetStateForm(FormState state)
        {
            if (state == FormState.inicial)
            {
                txtParametros.Focus();
                txtParametros.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
                txtParametros.Text = "Ingrese DNI o apellido";
                gvwDatos.Enabled = true;
                txtIdEmpleadoEMP.ReadOnly = true;
                txtIdEmpleadoEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtDniEMP.ReadOnly = true;
                txtDniEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtNombreEMP.ReadOnly = true;
                txtNombreEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtDireccionEMP.ReadOnly = true;
                txtDireccionEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtTelefonoEMP.ReadOnly = true;
                txtTelefonoEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtFechaNacEMP.ReadOnly = true;
                txtFechaNacEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtCargasSocialesEMP.ReadOnly = true;
                txtCargasSocialesEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                chkActiva.Enabled = false;
                chkActiva.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtApellidoEMP.ReadOnly = true;
                txtApellidoEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtSalarioEMP.ReadOnly = true;
                txtSalarioEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtFechaIngresoEMP.ReadOnly = true;
                txtFechaIngresoEMP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                if (tblEmpleados.Rows.Count == 0)
                {
                    btnBuscar.Enabled = false;
                    btnEditar.Enabled = false;
                    btnBorrar.Enabled = false;
                }
                else
                {
                    btnBuscar.Enabled = true;
                    btnEditar.Enabled = true;
                    btnBorrar.Enabled = true;
                }
                btnNuevo.Enabled = true;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = false;
                btnSalir.Enabled = true;
                DelEventosValidacion();
            }
            if (state == FormState.insercion)
            {
                gvwDatos.Enabled = false;
                txtDniEMP.ReadOnly = false;
                txtNombreEMP.ReadOnly = false;
                txtDireccionEMP.ReadOnly = false;
                txtTelefonoEMP.ReadOnly = false;
                txtFechaNacEMP.ReadOnly = false;
                txtCargasSocialesEMP.ReadOnly = false;
                chkActiva.Enabled = true;
                txtApellidoEMP.ReadOnly = false;
                txtSalarioEMP.ReadOnly = false;
                txtFechaIngresoEMP.ReadOnly = false;
                txtDniEMP.Clear();
                txtNombreEMP.Clear();
                txtDireccionEMP.Clear();
                txtTelefonoEMP.Clear();
                txtFechaNacEMP.Clear();
                txtCargasSocialesEMP.Clear();
                txtApellidoEMP.Clear();
                txtSalarioEMP.Clear();
                txtFechaIngresoEMP.Clear();
                txtDniEMP.Focus();
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnSalir.Enabled = false;
                AddEventosValidacion();
            }
            if (state == FormState.edicion)
            {
                gvwDatos.Enabled = false;
                txtDniEMP.ReadOnly = false;
                txtNombreEMP.ReadOnly = false;
                txtDireccionEMP.ReadOnly = false;
                txtTelefonoEMP.ReadOnly = false;
                txtFechaNacEMP.ReadOnly = false;
                txtCargasSocialesEMP.ReadOnly = false;
                chkActiva.Enabled = true;
                txtApellidoEMP.ReadOnly = false;
                txtSalarioEMP.ReadOnly = false;
                txtFechaIngresoEMP.ReadOnly = false;
                txtDniEMP.Focus();
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnSalir.Enabled = false;
                AddEventosValidacion();
            }
        }

    }
}
