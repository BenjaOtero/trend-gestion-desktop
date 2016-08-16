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
    public partial class frmFormasPago : Form
    {
        private DataTable tblFormasPago;
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

        public frmFormasPago()
        {
            InitializeComponent();
            tblFormasPago = BL.GetDataBLL.FormasPago();
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblFormasPago);
            bindingSource1.BindingComplete += new BindingCompleteEventHandler(bindingSource1_BindingComplete);
        }

        private void frmFormasPago_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            bindingSource1.Filter = "DescripcionFOR LIKE '*' AND IdFormaPagoFOR <> 99";
            bindingSource1.DataSource = tblFormasPago;
            bindingNavigator1.BindingSource = bindingSource1;
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["IdFormaPagoFOR"].HeaderText = "Nº forma";
            gvwDatos.Columns["DescripcionFOR"].HeaderText = "Descripción";
            bindingSource1.Sort = "DescripcionFOR";
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
            bindingSource1.Filter = "DescripcionFOR LIKE '" + parametros + "*' AND IdFormaPagoFOR <> 99";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            DataTable tmp = tblFormasPago.Copy();
            tmp.AcceptChanges();
            // utilizo tmp porque si hay filas borradas en tblFormasPago el select max da error
            var maxValue = tmp.Rows.OfType<DataRow>().Select(row => row["IdFormaPagoFOR"]).Max();
            int clave = Convert.ToInt32(maxValue) + 1;
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdFormaPagoFOR.ReadOnly = false;
            txtIdFormaPagoFOR.Text = clave.ToString();
            txtIdFormaPagoFOR.ReadOnly = true;
            txtDescripcionFOR.Focus();
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
            buscado = txtDescripcionFOR.Text;
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

        private void frmFormasPago_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.RemoveFilter();
        }

        private void Grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                bindingSource1.EndEdit();
                if (tblFormasPago.GetChanges() != null)
                {
                    BL.FormasPagoBLL.GrabarDB(tblFormasPago);
                }
                bindingSource1.RemoveFilter();
                int itemFound = bindingSource1.Find("DescripcionFOR", buscado);
                bindingSource1.Position = itemFound;
            }
            catch (ConstraintException)
            {
                string mensaje;
                if (insertando)
                {
                    mensaje = "No se puede agregar la forma de pago '" + txtDescripcionFOR.Text.ToUpper() + "' porque ya existe";
                    MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.RemoveCurrent();
                }

                if (editando)
                {
                    mensaje = "No se puede modificar la forma de pago  a '" + txtDescripcionFOR.Text.ToUpper() + "' porque ya existe";
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
            if ((sender == (object)txtDescripcionFOR))
            {
                if (string.IsNullOrEmpty(txtDescripcionFOR.Text))
                {
                    this.errorProvider1.SetError(txtDescripcionFOR, "Debe escribir una descripción.");
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
                txtDescripcionFOR.ReadOnly = true;
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
                txtDescripcionFOR.ReadOnly = false;
                txtDescripcionFOR.Clear();
                txtDescripcionFOR.Focus();
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
                txtDescripcionFOR.ReadOnly = false;
                txtDescripcionFOR.Focus();
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
