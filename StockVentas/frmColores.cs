using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BL;
using System.Data.Objects.DataClasses;

namespace StockVentas
{
    public partial class frmColores : Form
    {
        private DataTable tblColores;
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

        public frmColores()
        {
            InitializeComponent();
            tblColores = BL.GetDataBLL.Colores();
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblColores);
        }

        private void BindingSource_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            bindingSource1.DataSource = tblColores;
            bindingNavigator1.BindingSource = bindingSource1;
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["HexCOL"].Visible = false;
            gvwDatos.Columns["IdColorCOL"].HeaderText = "Nº color";
            gvwDatos.Columns["DescripcionCOL"].HeaderText = "Descripción";
            bindingSource1.Sort = "DescripcionCOL";
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
            bindingSource1.Filter = "DescripcionCOL LIKE '" + parametros + "*'";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            DataTable tmp = tblColores.Copy();
            tmp.AcceptChanges();
            // utilizo tmp porque si hay filas borradas en tblColores el select max da error
            var maxValue = tmp.Rows.OfType<DataRow>().Select(row => row["IdColorCOL"]).Max();
            int clave = Convert.ToInt32(maxValue) + 1;
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdColorCOL.ReadOnly = false;
            txtIdColorCOL.Text = clave.ToString();
            txtIdColorCOL.ReadOnly = true;
            txtDescripcionCOL.Focus();
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
            buscado = txtDescripcionCOL.Text;
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

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                string color = System.Drawing.ColorTranslator.ToHtml(MyDialog.Color);
                txtHexCOL.Text = color;
            }
        }

        private void frmBindingSource_FormClosing(object sender, FormClosingEventArgs e)
        {
          /*  bindingSource1.EndEdit();
            if (tblColores.GetChanges() != null)
            {
                DialogResult respuesta =
                        MessageBox.Show("¿Desea guardar los cambios?", "Trend", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (respuesta)
                {
                    case DialogResult.Yes:
                        Grabar();
                        bindingSource1.RemoveFilter();
                        break;
                    case DialogResult.No:
                        tblColores.RejectChanges();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }*/
            bindingSource1.RemoveFilter();
        }

        private void Grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                bindingSource1.EndEdit();
                if (tblColores.GetChanges() != null)
                {
                    BL.ColoresBLL.GrabarDB(tblColores);
                }
                bindingSource1.RemoveFilter();
                int itemFound = bindingSource1.Find("DescripcionCOL", buscado);
                bindingSource1.Position = itemFound;
            }
            catch (ConstraintException)
            {
                string mensaje;
                if (insertando)
                {
                    mensaje = "No se puede agregar el color '" + txtDescripcionCOL.Text.ToUpper() + "' porque ya existe";
                    MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.RemoveCurrent();
                }

                if (editando)
                {
                    mensaje = "No se puede modificar el color  a '" + txtDescripcionCOL.Text.ToUpper() + "' porque ya existe";
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
            if ((sender == (object)txtDescripcionCOL))
            {
                if (string.IsNullOrEmpty(txtDescripcionCOL.Text))
                {
                    this.errorProvider1.SetError(txtDescripcionCOL, "Debe escribir una descripción.");
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
                txtIdColorCOL.ReadOnly = true;
                txtDescripcionCOL.ReadOnly = true;
                txtHexCOL.ReadOnly = true;
                btnBuscar.Enabled = true;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnBorrar.Enabled = true;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = false;
                btnColor.Enabled = false;
                btnSalir.Enabled = true;
                DelEventosValidacion();
                insertando = false;
                editando = false;
                txtParametros.Focus();
            }
            if (state == FormState.insercion)
            {
                txtDescripcionCOL.ReadOnly = false;
                txtHexCOL.ReadOnly = false;
                txtDescripcionCOL.Clear();
                txtDescripcionCOL.Focus();
                txtHexCOL.Clear();
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnColor.Enabled = true;
                btnSalir.Enabled = false;
                AddEventosValidacion();
                insertando = true;
            }
            if (state == FormState.edicion)
            {
                gvwDatos.Enabled = false;
                txtDescripcionCOL.ReadOnly = false;
                txtHexCOL.ReadOnly = false;
                txtDescripcionCOL.Focus();
                btnBuscar.Enabled = false;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnBorrar.Enabled = false;
                btnGrabar.Enabled = false;
                btnCancelar.Enabled = true;
                btnColor.Enabled = true;
                btnSalir.Enabled = false;
                AddEventosValidacion();
                editando = true;
            }
        }

    }
}
