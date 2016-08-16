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
    public partial class frmEmpleadosMov : Form
    {
        private DataTable tblEmpleadosMov;
        private DataTable tblEmpleadosMovTipos;
        private DataTable tblEmpleados;
        private DataView viewEmpleadosMov;
        public string PK = string.Empty;
        string strFecha;
        string detalle;
        string importe;
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

        public frmEmpleadosMov()
        {
            InitializeComponent();
            tblEmpleadosMov = BL.EmpleadosMovBLL.GetTabla();
            viewEmpleadosMov = new DataView(tblEmpleadosMov);
        }

        public frmEmpleadosMov(string strFecha, string detalle, string importe)
        {
            InitializeComponent();
            tblEmpleadosMov = BL.EmpleadosMovBLL.GetTabla();
            viewEmpleadosMov = new DataView(tblEmpleadosMov);
            this.strFecha = strFecha;
            this.detalle = detalle;
            this.importe = importe;            
        }

        public frmEmpleadosMov(DataTable tblEmpleadosMov, string PK)
        {
            InitializeComponent();
            this.tblEmpleadosMov = tblEmpleadosMov;
            this.PK = PK;
            viewEmpleadosMov = new DataView(tblEmpleadosMov);
            viewEmpleadosMov.RowFilter = "IdMovEMOV =" + PK;
        }

        private void frmEmpleadosMov_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Text = "Empleados movimientos";
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblEmpleadosMov);
            bindingSource1.DataSource = viewEmpleadosMov;
            bindingNavigator1.BindingSource = bindingSource1;
            tblEmpleados = BL.GetDataBLL.Empleados();
            if(!tblEmpleados.Columns.Contains("ApellidoNombreEMP")) tblEmpleados.Columns.Add("ApellidoNombreEMP", typeof(string));            
            foreach (DataRow rowEmpleado in tblEmpleados.Rows)
            {
                string apellido = rowEmpleado["ApellidoEMP"].ToString();
                string nombre = rowEmpleado["NombreEMP"].ToString();
                rowEmpleado["ApellidoNombreEMP"] = apellido + ", " + nombre;
            }
            cmbIdEmpleadoEMOV.DataSource = tblEmpleados;
            cmbIdEmpleadoEMOV.DisplayMember = "ApellidoNombreEMP";
            cmbIdEmpleadoEMOV.ValueMember = "IdEmpleadoEMP";
            cmbIdEmpleadoEMOV.SelectedValue = -1;
            AutoCompleteStringCollection empleadosColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblEmpleados.Rows)
            {
                empleadosColection.Add(Convert.ToString(row["ApellidoNombreEMP"]));
            }
            cmbIdEmpleadoEMOV.AutoCompleteCustomSource = empleadosColection;
            cmbIdEmpleadoEMOV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbIdEmpleadoEMOV.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbIdEmpleadoEMOV.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            tblEmpleadosMovTipos = BL.GetDataBLL.EmpleadosMovTipos();
            cmbIdMovTipoEMOV.DataSource = tblEmpleadosMovTipos;
            cmbIdMovTipoEMOV.DisplayMember = "DescripcionETIP";
            cmbIdMovTipoEMOV.ValueMember = "IdMovETIP";
            cmbIdMovTipoEMOV.SelectedValue = -1;
            AutoCompleteStringCollection condicionColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblEmpleadosMovTipos.Rows)
            {
                condicionColection.Add(Convert.ToString(row["DescripcionETIP"]));
            }
            cmbIdMovTipoEMOV.AutoCompleteCustomSource = condicionColection;
            cmbIdMovTipoEMOV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbIdMovTipoEMOV.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbIdMovTipoEMOV.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);  
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            chkLiquidadoEMOV.DataBindings.Add(BL.Utilitarios.DataBindingsCheckBoxAdd(bindingSource1, grpCampos));
            bindingSource1.BindingComplete += new BindingCompleteEventHandler(bindingSource1_BindingComplete);
            grpBotones.CausesValidation = false;
            btnCancelar.CausesValidation = false;
            btnSalir.CausesValidation = false;
            txtImporteEMOV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtCantidadEMOV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumeros);            
            SetStateForm(FormState.inicial);
        }

        private void frmEmpleadosMov_Activated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strFecha))
            {
                btnNuevo.PerformClick();
                mskFechaEMOV.Text = strFecha;
                txtDetalleEMOV.Text = detalle;
                double dblImporte = Convert.ToDouble(importe);
                if (dblImporte < 0) dblImporte = dblImporte * -1;
                txtImporteEMOV.Text = dblImporte.ToString();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            chkLiquidadoEMOV.CheckState = CheckState.Checked; // Tildo el checkbox para bindearlo
            Random rand = new Random();
            int clave = rand.Next(1, 2000000000);
            bool existe = true;
            while (existe == true)
            {
                DataRow[] foundRow2 = tblEmpleadosMov.Select("IdMovEMOV =" + clave);
                if (foundRow2.Count() == 0)
                {
                    existe = false;
                }
                else
                {
                    clave = rand.Next(1, 2000000000);
                }
            }
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdMovEMOV.ReadOnly = false;
            txtIdMovEMOV.Text = clave.ToString();
            txtIdMovEMOV.ReadOnly = true;
            txtDetalleEMOV.Focus();
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
            if (ValidarFormulario())
            {
                bindingSource1.EndEdit();
                SetStateForm(FormState.inicial);            
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            SetStateForm(FormState.inicial);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmEmpleadosMov_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false; //
            bindingSource1.EndEdit();
            if (tblEmpleadosMov.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblEmpleadosMov, "frmEmpleadosMov", "grabar");
                progreso.ShowDialog();                
            }
            bindingSource1.RemoveFilter();
        }

        private void Validar(object sender, CancelEventArgs e)
        {
            if ((sender == (object)mskFechaEMOV) && mskFechaEMOV.Text == "__/__/____")
            {
                this.errorProvider1.SetError(mskFechaEMOV, "Debe escribir una fecha.");
                e.Cancel = true;
            }
            if ((sender == (object)cmbIdEmpleadoEMOV) && string.IsNullOrEmpty(cmbIdEmpleadoEMOV.Text))
            {
                this.errorProvider1.SetError(cmbIdEmpleadoEMOV, "Debe seleccionar un empleado/a.");
                e.Cancel = true;
            }
            if ((sender == (object)cmbIdMovTipoEMOV) && string.IsNullOrEmpty(cmbIdMovTipoEMOV.Text))
            {
                this.errorProvider1.SetError(cmbIdMovTipoEMOV, "Debe seleccionar un tipo de movimiento.");
                e.Cancel = true;
            }
            if ((sender == (object)txtCantidadEMOV) && string.IsNullOrEmpty(txtCantidadEMOV.Text))
            {
                this.errorProvider1.SetError(txtCantidadEMOV, "Debe escribir una cantidad.");
                e.Cancel = true;
            }
            if ((sender == (object)txtImporteEMOV) && string.IsNullOrEmpty(txtImporteEMOV.Text))
            {
                this.errorProvider1.SetError(txtImporteEMOV, "Debe escribir un importe.");
                e.Cancel = true;
            }
        }

        private void Validado(object sender, EventArgs e)
        {
            if ((sender == (object)mskFechaEMOV)) this.errorProvider1.SetError(mskFechaEMOV, "");
            if ((sender == (object)cmbIdEmpleadoEMOV)) this.errorProvider1.SetError(cmbIdEmpleadoEMOV, "");
            if ((sender == (object)cmbIdMovTipoEMOV)) this.errorProvider1.SetError(cmbIdMovTipoEMOV, "");
            if ((sender == (object)txtCantidadEMOV)) this.errorProvider1.SetError(txtCantidadEMOV, "");
            if ((sender == (object)txtImporteEMOV)) this.errorProvider1.SetError(txtImporteEMOV, "");
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
            this.errorProvider1.SetError(mskFechaEMOV, "");
            this.errorProvider1.SetError(cmbIdEmpleadoEMOV, "");
            this.errorProvider1.SetError(cmbIdMovTipoEMOV, "");
            this.errorProvider1.SetError(txtCantidadEMOV, "");
            this.errorProvider1.SetError(txtImporteEMOV, "");
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(mskFechaEMOV.Text))
            {
                this.errorProvider1.SetError(mskFechaEMOV, "Debe escribir una fecha.");
                mskFechaEMOV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cmbIdEmpleadoEMOV.Text))
            {
                this.errorProvider1.SetError(cmbIdEmpleadoEMOV, "Debe seleccionar un empleado/a.");
                cmbIdEmpleadoEMOV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cmbIdMovTipoEMOV.Text))
            {
                this.errorProvider1.SetError(cmbIdMovTipoEMOV, "Debe seleccionar un tipo de movimientos.");
                cmbIdMovTipoEMOV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCantidadEMOV.Text))
            {
                this.errorProvider1.SetError(txtCantidadEMOV, "Debe escribir una cantidad.");
                txtCantidadEMOV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtImporteEMOV.Text))
            {
                this.errorProvider1.SetError(txtImporteEMOV, "Debe escribir un importe.");
                txtImporteEMOV.Focus();
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
                txtIdMovEMOV.ReadOnly = true;
                txtIdMovEMOV.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                mskFechaEMOV.ReadOnly = true;
                mskFechaEMOV.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                cmbIdEmpleadoEMOV.Enabled = false;
                cmbIdEmpleadoEMOV.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                cmbIdMovTipoEMOV.Enabled = false;
                cmbIdMovTipoEMOV.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtCantidadEMOV.ReadOnly = true;
                txtCantidadEMOV.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtDetalleEMOV.ReadOnly = true;
                txtDetalleEMOV.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtImporteEMOV.ReadOnly = true;
                txtImporteEMOV.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                chkLiquidadoEMOV.Enabled = false;
                if (tblEmpleadosMov.Rows.Count == 0)
                {
                    btnEditar.Enabled = false;
                    btnBorrar.Enabled = false;
                }
                else
                {
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
                mskFechaEMOV.ReadOnly = false;
                mskFechaEMOV.Clear();
                cmbIdEmpleadoEMOV.Enabled = true;
                cmbIdEmpleadoEMOV.SelectedValue = -1;
                cmbIdMovTipoEMOV.Enabled = true;
                cmbIdMovTipoEMOV.SelectedValue = -1;
                txtCantidadEMOV.ReadOnly = false;
                txtCantidadEMOV.Clear();
                txtDetalleEMOV.ReadOnly = false;
                txtDetalleEMOV.Clear();
                txtImporteEMOV.ReadOnly = false;                
                txtImporteEMOV.Clear();
                chkLiquidadoEMOV.Enabled = true;
                chkLiquidadoEMOV.CheckState = CheckState.Unchecked;
                mskFechaEMOV.Focus();

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
                mskFechaEMOV.ReadOnly = false;
                cmbIdEmpleadoEMOV.Enabled = true;
                cmbIdMovTipoEMOV.Enabled = true;
                txtCantidadEMOV.ReadOnly = false;
                txtDetalleEMOV.ReadOnly = false;
                txtDetalleEMOV.ReadOnly = false;
                txtImporteEMOV.ReadOnly = false;
                chkLiquidadoEMOV.Enabled = false;
                mskFechaEMOV.Focus();

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
