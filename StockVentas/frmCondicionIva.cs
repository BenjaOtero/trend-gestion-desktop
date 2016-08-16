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
    public partial class frmCondicionIva : Form
    {
        private DataTable tblCondicionIva;

        public enum FormState
        {
            inicial,
            edicion,
            insercion,
            eliminacion
        }

        public frmCondicionIva()
        {
            InitializeComponent();
            tblCondicionIva = BL.GetDataBLL.CondicionIva();
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblCondicionIva);
            bindingSource1.BindingComplete += new BindingCompleteEventHandler(bindingSource1_BindingComplete);
        }

        private void frmCondicionIva_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            bindingSource1.DataSource = tblCondicionIva;
            bindingNavigator1.BindingSource = bindingSource1;
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["IdCondicionIvaCIVA"].HeaderText = "ID";
            gvwDatos.Columns["DescripcionCIVA"].HeaderText = "Descripción";
            bindingSource1.Sort = "DescripcionCIVA";
            grpBotones.CausesValidation = false;
            btnCancelar.CausesValidation = false;
            SetStateForm(FormState.inicial);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            DataTable tmp = tblCondicionIva.Copy();
            tmp.AcceptChanges();
            // utilizo tmp porque si hay filas borradas en tblCondicionIva el select max da error
            var maxValue = tmp.Rows.OfType<DataRow>().Select(row => row["IdCondicionIvaCIVA"]).Max();
            int clave = Convert.ToInt32(maxValue) + 1;
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdCondicionIvaCIVA.ReadOnly = false;
            txtIdCondicionIvaCIVA.Text = clave.ToString();
            txtIdCondicionIvaCIVA.ReadOnly = true;
            txtDescripcionCIVA.Focus();
            SetStateForm(FormState.insercion);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Count == 0) return;
            if(MessageBox.Show("La edición errónea de los datos puede alterar el buen funcionamiento de la facturación. ¿Desea continuar?","Trend",
                    MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)
                == DialogResult.OK) SetStateForm(FormState.edicion);
            
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Count == 0) return;
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
                bindingSource1.Sort = "DescripcionCIVA";
                SetStateForm(FormState.inicial);
                //  bindingSource1.RemoveFilter();
            }
            catch (ConstraintException)
            {
                string mensaje = "No se puede agregar la condición frente al IVA '" + txtDescripcionCIVA.Text.ToUpper() + "' porque ya existe";
                MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcionCIVA.Focus();
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

        private void frmCondicionIva_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.EndEdit();
            if (tblCondicionIva.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblCondicionIva, "frmCondicionIva", "grabar");
                progreso.ShowDialog();
            }
            bindingSource1.RemoveFilter();
        }

        private void bindingSource1_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            // Check if the data source has been updated, and that no error has occured.
            if (e.BindingCompleteContext ==
                BindingCompleteContext.DataSourceUpdate && e.Exception == null)

                // If not, end the current edit.
                e.Binding.BindingManagerBase.EndCurrentEdit();
        }

        private void ValidarCampos(object sender, CancelEventArgs e)
        {
            if ((sender == (object)txtDescripcionCIVA))
            {
                if (string.IsNullOrEmpty(txtDescripcionCIVA.Text))
                {
                    this.errorProvider1.SetError(txtDescripcionCIVA, "Debe escribir una descripción.");
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
                txtIdCondicionIvaCIVA.ReadOnly = true;
                txtDescripcionCIVA.ReadOnly = true;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnBorrar.Enabled = true;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = false;
                btnSalir.Enabled = true;
                DelEventosValidacion();
                gvwDatos.Focus();
            }

            if (state == FormState.insercion)
            {
                gvwDatos.Enabled = false;
                txtDescripcionCIVA.ReadOnly = false;
                txtDescripcionCIVA.Clear();
                txtDescripcionCIVA.Focus();
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
                txtDescripcionCIVA.ReadOnly = false;
                txtDescripcionCIVA.Focus();
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
