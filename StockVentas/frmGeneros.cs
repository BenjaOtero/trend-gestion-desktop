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
    public partial class frmGeneros : Form
    {
        private DataTable tblGeneros;
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

        public frmGeneros()
        {
            InitializeComponent();
            tblGeneros = BL.GetDataBLL.Generos();
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref chkActivoWebGEN, ref tblGeneros);
        }

        private void frmGeneros_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            ToolTip tooltip = new ToolTip();
            txtIdGeneroGEN.TabStop = false;
            txtParametros.TabStop = false;
            tooltip.SetToolTip(chkActivoWebGEN, "Esta información se utiliza para determinar si el género producirá un elemento de menú en el sitio web.");
            bindingSource1.DataSource = tblGeneros;
            tblGeneros.ColumnChanged += new DataColumnChangeEventHandler(tblGeneros_ColumnChanged);
            bindingNavigator1.BindingSource = bindingSource1;
            BL.Utilitarios.DataBindingsAdd(bindingSource1, grpCampos);
            Binding bind = new Binding("Checked", bindingSource1, "ActivoWebGEN", false, DataSourceUpdateMode.OnPropertyChanged);
            bind.Format += new ConvertEventHandler(binding_Format);
            bind.Parse += new ConvertEventHandler(binding_Parse);
            chkActivoWebGEN.DataBindings.Add(bind);
            gvwDatos.DataSource = bindingSource1;
            gvwDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwDatos.Columns["IdGeneroGEN"].HeaderText = "Nº Género";
            gvwDatos.Columns["DescripcionGEN"].HeaderText = "Descripción";
            gvwDatos.Columns["ActivoWebGEN"].Visible = false;
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
            bindingSource1.Filter = "DescripcionGEN LIKE '" + parametros + "*'";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            // tildo el checkbox para disparar el evento parse del objeto bind
            chkActivoWebGEN.CheckState = CheckState.Checked;
            DataTable tmp = tblGeneros.Copy();
            tmp.AcceptChanges();
            // utilizo tmp porque si hay filas borradas en tblFormasPago el select max da error
            var maxValue = tmp.Rows.OfType<DataRow>().Select(row => row["IdGeneroGEN"]).Max();
            int clave = Convert.ToInt32(maxValue) + 1;
            bindingSource1.Position = bindingSource1.Count - 1;
            txtIdGeneroGEN.ReadOnly = false;
            txtIdGeneroGEN.Text = clave.ToString();
            txtIdGeneroGEN.ReadOnly = true;
            txtDescripcionGEN.Focus();
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
            buscado = txtDescripcionGEN.Text;
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

        private void frmGeneros_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.RemoveFilter();
        }

        private void Grabar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                bindingSource1.EndEdit();
                if (tblGeneros.GetChanges() != null)
                {
                    BL.GenerosBLL.GrabarDB(tblGeneros);
                }
                bindingSource1.RemoveFilter();
                int itemFound = bindingSource1.Find("DescripcionGEN", buscado);
                bindingSource1.Position = itemFound;
            }
            catch (ConstraintException)
            {
                string mensaje;
                if (insertando)
                {
                    mensaje = "No se puede agregar el género '" + txtDescripcionGEN.Text.ToUpper() + "' porque ya existe";
                    MessageBox.Show(mensaje, "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.RemoveCurrent();
                }

                if (editando)
                {
                    mensaje = "No se puede modificar el género  a '" + txtDescripcionGEN.Text.ToUpper() + "' porque ya existe";
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
            if (e.Value.ToString() == "True")  e.Value = true;
            else e.Value = false;
        }

        void binding_Parse(object sender, ConvertEventArgs e)
        {
           if ((bool)e.Value) e.Value = 1;
            else e.Value = 0;
        }

        private void tblGeneros_ColumnChanged(object sender, EventArgs e)
        {
            if (btnGrabar.Enabled != true) btnGrabar.Enabled = true;
        }

        private void ValidarCampos(object sender, CancelEventArgs e)
        {
            if ((sender == (object)txtDescripcionGEN))
            {
                if (string.IsNullOrEmpty(txtDescripcionGEN.Text))
                {
                    this.errorProvider1.SetError(txtDescripcionGEN, "Debe escribir una descripción.");
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
                txtDescripcionGEN.ReadOnly = true;
                chkActivoWebGEN.Enabled = false;
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
                txtDescripcionGEN.ReadOnly = false;
                chkActivoWebGEN.Enabled = true;
                txtDescripcionGEN.Clear();
                txtDescripcionGEN.Focus();
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
                txtDescripcionGEN.ReadOnly = false;
                chkActivoWebGEN.Enabled = true;
                txtDescripcionGEN.Focus();
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
