using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BL;

namespace StockVentas
{
    public partial class frmProveedores : Form
    {
        private DataTable tblProveedores;
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

        public frmProveedores()
        {
            InitializeComponent();
            tblProveedores = BL.GetDataBLL.Proveedores();
            DataColumn[] primaryKey;
            primaryKey = new DataColumn[1];
            DataColumn razonSocial = tblProveedores.Columns["RazonSocialPRO"];
            primaryKey[0] = razonSocial;
            tblProveedores.PrimaryKey = primaryKey;
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblProveedores);
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            bindingSource1.DataSource = tblProveedores;
            bindingNavigator1.BindingSource = bindingSource1;
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["IdProveedorPRO"].HeaderText = "Nº prov.";
            gvwDatos.Columns["RazonSocialPRO"].HeaderText = "Razon social";
            gvwDatos.Columns["DireccionPRO"].Visible = false;
            gvwDatos.Columns["CodigoPostalPRO"].Visible = false;
            gvwDatos.Columns["TelefonoPRO"].Visible = false;
            gvwDatos.Columns["ContactoPRO"].Visible = false;
            bindingSource1.Sort = "RazonSocialPRO";
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
            bindingSource1.Filter = "RazonSocialPRO LIKE '" + parametros + "*'";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            DataTable tmp = tblProveedores.Copy();
            tmp.AcceptChanges();
            // utilizo tmp porque si hay filas borradas en tblColores el select max da error
            var maxValue = tmp.Rows.OfType<DataRow>().Select(row => row["IdProveedorPRO"]).Max();
            int clave = Convert.ToInt32(maxValue) + 1;
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdProveedorPRO.ReadOnly = false;
            txtIdProveedorPRO.Text = clave.ToString();
            txtIdProveedorPRO.ReadOnly = true;
            txtRazonSocialPRO.Focus();
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
            buscado = txtRazonSocialPRO.Text;
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

        private void frmProveedores_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.RemoveFilter();
        }

        private void Grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                bindingSource1.EndEdit();
                if (tblProveedores.GetChanges() != null)
                {
                    BL.ProveedoresBLL.GrabarDB(tblProveedores);
                }
                bindingSource1.RemoveFilter();
                int itemFound = bindingSource1.Find("RazonSocialPRO", buscado);
                bindingSource1.Position = itemFound;
            }
            catch (ConstraintException)
            {
                string mensaje;
                if (insertando)
                {
                    mensaje = "No se puede agregar el proveedor '" + txtRazonSocialPRO.Text.ToUpper() + "' porque ya existe.";
                    MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.RemoveCurrent();
                }

                if (editando)
                {
                    mensaje = "No se puede modificar el proveedor  a '" + txtRazonSocialPRO.Text.ToUpper() + "' porque ya existe.";
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

        private void ValidarCampos(object sender, CancelEventArgs e)
        {
            if ((sender == (object)txtRazonSocialPRO))
            {
                if (string.IsNullOrEmpty(txtRazonSocialPRO.Text))
                {
                    this.errorProvider1.SetError(txtRazonSocialPRO, "Debe escribir una razón social.");
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
                txtIdProveedorPRO.ReadOnly = true;
                txtIdProveedorPRO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtRazonSocialPRO.ReadOnly = true;
                txtRazonSocialPRO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtDireccionPRO.ReadOnly = true;
                txtDireccionPRO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtCodigoPostalPRO.ReadOnly = true;
                txtCodigoPostalPRO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtTelefonoPRO.ReadOnly = true;
                txtTelefonoPRO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                txtContactoPRO.ReadOnly = true;
                txtContactoPRO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
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
                txtRazonSocialPRO.ReadOnly = false;
                txtDireccionPRO.ReadOnly = false;
                txtCodigoPostalPRO.ReadOnly = false;
                txtTelefonoPRO.ReadOnly = false;
                txtContactoPRO.ReadOnly = false;
                txtRazonSocialPRO.Clear();
                txtDireccionPRO.Clear();
                txtCodigoPostalPRO.Clear();
                txtTelefonoPRO.Clear();
                txtContactoPRO.Clear();
                txtRazonSocialPRO.Focus();
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
                txtRazonSocialPRO.ReadOnly = false;
                txtDireccionPRO.ReadOnly = false;
                txtCodigoPostalPRO.ReadOnly = false;
                txtTelefonoPRO.ReadOnly = false;
                txtContactoPRO.ReadOnly = false;
                txtRazonSocialPRO.Focus();
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
