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
    public partial class frmLocales : Form
    {
        private DataTable tblLocales;
        bool editando;
        bool insertando;
        string buscado = string.Empty;
        private const int CP_NOCLOSE_BUTTON = 0x200;

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

        public frmLocales()
        {
            InitializeComponent();
            tblLocales = BL.GetDataBLL.Locales();
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref chkActivoWebLOC, ref tblLocales);
        }

        private void BindingSource_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            bindingSource1.DataSource = tblLocales;
            bindingSource1.Filter = "IdLocalLOC <> 1 AND IdLocalLOC <> 2";
            bindingNavigator1.BindingSource = bindingSource1;
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            Binding bind = new Binding("Checked", bindingSource1, "ActivoWebLOC", false, DataSourceUpdateMode.OnPropertyChanged);
            bind.Format += new ConvertEventHandler(binding_Format);
            bind.Parse += new ConvertEventHandler(binding_Parse);
            chkActivoWebLOC.DataBindings.Add(bind);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["DireccionLOC"].Visible = false;
            gvwDatos.Columns["TelefonoLOC"].Visible = false;
            gvwDatos.Columns["ActivoWebLOC"].Visible = false;
            gvwDatos.Columns["IdLocalLOC"].HeaderText = "Nº local";
            gvwDatos.Columns["NombreLOC"].HeaderText = "Nombre";
            bindingSource1.Sort = "NombreLOC";
            grpBotones.CausesValidation = false;
            btnCancelar.CausesValidation = false;
            SetStateForm(FormState.inicial);   
        }

        private void txtParametros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) btnBuscar.PerformClick();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string parametros = txtParametros.Text;
            bindingSource1.Filter = "NombreLOC LIKE '" + parametros + "*' AND IdLocalLOC <> 1 AND IdLocalLOC <> 2";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            // tildo el checkbox para disparar el evento parse del objeto bind
            chkActivoWebLOC.CheckState = CheckState.Checked;
            DataTable tmp = tblLocales.Copy();
            tmp.AcceptChanges();
            // utilizo tmp porque si hay filas borradas en tblLocales el select max da error
            var maxValue = tmp.Rows.OfType<DataRow>().Select(row => row["IdLocalLOC"]).Max();
            int clave = Convert.ToInt32(maxValue) + 1;
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdLocalLOC.ReadOnly = false;
            txtIdLocalLOC.Text = clave.ToString();
            txtIdLocalLOC.ReadOnly = true;
            txtNombreLOC.Focus();
            SetStateForm(FormState.insercion);                   
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Count == 0) return;
            SetStateForm(FormState.edicion);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Count == 0) return;
            if (MessageBox.Show("¿Desea borrar este registro?", "Trend Gestión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingSource1.RemoveCurrent();
                Grabar();
            }
            SetStateForm(FormState.inicial);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            buscado = txtNombreLOC.Text;
            Grabar();
            SetStateForm(FormState.inicial);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (insertando) bindingSource1.RemoveCurrent();
            bindingSource1.CancelEdit();
            SetStateForm(FormState.inicial);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmBindingSource_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.RemoveFilter();
        }

        private void Grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                bindingSource1.EndEdit();
                if (tblLocales.GetChanges() != null)
                {
                    BL.LocalesBLL.GrabarDB(tblLocales);
                }
                bindingSource1.RemoveFilter();
                int itemFound = bindingSource1.Find("NombreLOC", buscado);
                bindingSource1.Position = itemFound;
            }
            catch (ConstraintException)
            {
                string mensaje;
                if (insertando)
                {
                    mensaje = "No se puede agregar el local '" + txtNombreLOC.Text.ToUpper() + "' porque ya existe";
                    MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.RemoveCurrent();
                }

                if (editando)
                {
                    mensaje = "No se puede modificar el local  a '" + txtNombreLOC.Text.ToUpper() + "' porque ya existe";
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

        void binding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value.ToString() == "True") e.Value = true;
            else e.Value = false;
        }

        void binding_Parse(object sender, ConvertEventArgs e)
        {
            if ((bool)e.Value) e.Value = 1;
            else e.Value = 0;
        }

        private void ValidarCampos(object sender, CancelEventArgs e)
        {
            if ((sender == (object)txtNombreLOC))
            {
                if (string.IsNullOrEmpty(txtNombreLOC.Text))
                {
                    this.errorProvider1.SetError(txtNombreLOC, "Debe escribir un nombre.");
                    e.Cancel = true;
                }
            }
            if ((sender == (object)txtDireccionLOC))
            {
                if (string.IsNullOrEmpty(txtDireccionLOC.Text))
                {
                    this.errorProvider1.SetError(txtDireccionLOC, "Debe escribir una dirección.");
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

        public void SetStateForm(FormState state)
        {

            if (state == FormState.inicial)
            {
                gvwDatos.Enabled = true;
                txtIdLocalLOC.ReadOnly = true;
                txtNombreLOC.ReadOnly = true;
                txtDireccionLOC.ReadOnly = true;
                txtTelefonoLOC.ReadOnly = true;
                chkActivoWebLOC.Enabled = false;
                btnBuscar.Enabled = true;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnBorrar.Enabled = true;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = false;
                btnSalir.Enabled = true;
                DelEventosValidacion();
                insertando = false;
                editando = false;
                txtParametros.Focus();
            }

            if (state == FormState.insercion)
            {
                gvwDatos.Enabled = false;
                txtNombreLOC.ReadOnly = false;
                txtDireccionLOC.ReadOnly = false;
                txtTelefonoLOC.ReadOnly = false;
                chkActivoWebLOC.Enabled = true;
                txtNombreLOC.Clear();
                txtNombreLOC.Focus();
                txtDireccionLOC.Clear();
                txtTelefonoLOC.Clear();
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnSalir.Enabled = false;
                AddEventosValidacion();
                insertando = true;                
            }

            if (state == FormState.edicion)
            {
                gvwDatos.Enabled = false;
                txtNombreLOC.ReadOnly = false;
                txtDireccionLOC.ReadOnly = false;
                txtTelefonoLOC.ReadOnly = false;
                chkActivoWebLOC.Enabled = true;
                txtNombreLOC.Focus();
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnSalir.Enabled = false;
                AddEventosValidacion();
                editando = true;
            }
        }

    }
}
