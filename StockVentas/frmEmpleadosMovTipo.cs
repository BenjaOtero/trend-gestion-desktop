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
    public partial class frmEmpleadosMovTipo : Form
    {
        private DataTable tblEmpleadosMovTipos;
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

        public frmEmpleadosMovTipo()
        {
            InitializeComponent();
        }

        private void frmEmpleadosMovTipos_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            this.Text = "Empleados tipos de movimientos";
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblEmpleadosMovTipos = BL.GetDataBLL.EmpleadosMovTipos();
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblEmpleadosMovTipos);
            bindingSource1.DataSource = tblEmpleadosMovTipos;
            bindingNavigator1.BindingSource = bindingSource1;
            tblLocales = BL.GetDataBLL.Locales();
            DataView viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <> 1 AND IdLocalLOC <> 2";
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["IdMovETIP"].HeaderText = "ID";
            gvwDatos.Columns["DescripcionETIP"].HeaderText = "Descripción";
            gvwDatos.Columns["RemuneracionETIP"].Visible = false;
            bindingSource1.Sort = "DescripcionETIP";
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            chkRemuneracionETIP.DataBindings.Add(BL.Utilitarios.DataBindingsCheckBoxAdd(bindingSource1, grpCampos));
            bindingSource1.BindingComplete += new BindingCompleteEventHandler(bindingSource1_BindingComplete);
            grpBotones.CausesValidation = false;
            btnCancelar.CausesValidation = false;
            btnSalir.CausesValidation = false;
            SetStateForm(FormState.inicial);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {            
            bindingSource1.AddNew();
            chkRemuneracionETIP.CheckState = CheckState.Checked; // Tildo el checkbox para bindearlo
            DataTable tmp = tblEmpleadosMovTipos.Copy();
            tmp.AcceptChanges();
            // utilizo tmp porque si hay filas borradas en tblColores el select max da error
            var maxValue = tmp.Rows.OfType<DataRow>().Select(row => row["IdMovETIP"]).Max();
            int clave = Convert.ToInt32(maxValue) + 1;
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdMovETIP.ReadOnly = false;
            txtIdMovETIP.Text = clave.ToString();
            txtIdMovETIP.ReadOnly = true;
            txtDescripcionETIP.Focus();
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
                bindingSource1.EndEdit();
                bindingSource1.Position = 0;
                bindingSource1.Sort = "DescripcionETIP";
                SetStateForm(FormState.inicial);
                bindingSource1.RemoveFilter();
            }
            catch (ConstraintException)
            {
                string mensaje = "No se puede agregar el empleado '" + txtDescripcionETIP.Text.ToUpper() + "' porque ya existe un empleado con el mismo número de DNI";
                MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
              //  txtNombreEMP.Focus();
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

        private void frmEmpleadosMovTipos_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false; //
            bindingSource1.EndEdit();
            if (tblEmpleadosMovTipos.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblEmpleadosMovTipos, "frmEmpleadosMovTipo", "grabar");
                progreso.ShowDialog();   
            }
            bindingSource1.RemoveFilter();
        }

        private void Validar(object sender, CancelEventArgs e)
        {
            if ((sender == (object)txtDescripcionETIP) && string.IsNullOrEmpty(txtDescripcionETIP.Text))
            {
                this.errorProvider1.SetError(txtDescripcionETIP, "Debe escribir una descripción.");
                e.Cancel = true;
            }
        }

        private void Validado(object sender, EventArgs e)
        {
            if ((sender == (object)txtDescripcionETIP)) this.errorProvider1.SetError(txtDescripcionETIP, "");
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
            this.errorProvider1.SetError(txtDescripcionETIP, "");
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
                gvwDatos.Enabled = true;
                txtIdMovETIP.ReadOnly = true;
                txtIdMovETIP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtDescripcionETIP.ReadOnly = true;
                txtDescripcionETIP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                chkRemuneracionETIP.Enabled = false;
                if (tblEmpleadosMovTipos.Rows.Count == 0)
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
                gvwDatos.Enabled = false;
                txtDescripcionETIP.ReadOnly = false;
                chkRemuneracionETIP.Enabled = true;
                chkRemuneracionETIP.CheckState = CheckState.Unchecked;
                txtDescripcionETIP.Clear();
                txtDescripcionETIP.Focus();
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
                txtDescripcionETIP.ReadOnly = false;
                chkRemuneracionETIP.Enabled = true;
                txtDescripcionETIP.Focus();
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
