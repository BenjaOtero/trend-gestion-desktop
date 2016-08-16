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
    public partial class frmFondoCajaCons : Form
    {
        public DataSet dt = null;

        public frmFondoCajaCons()
        {
            InitializeComponent();
        }

        public frmFondoCajaCons(DataSet dt): this()
        {
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.Text = "  Fondos de caja";
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.dt = dt;
            dt.DataSetName = "dsFondoCaja";
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            Image image = global::StockVentas.Properties.Resources.delete16;
            imageColumn.Image = image;
            imageColumn.Name = "Borrar";
            dgvDatos.Columns.Add(imageColumn);
            DataGridViewImageColumn imageColumn2 = new DataGridViewImageColumn();
            Image image2 = global::StockVentas.Properties.Resources.document_edit;
            imageColumn2.Image = image2;
            imageColumn2.Name = "Editar";
            dgvDatos.Columns.Add(imageColumn2);
            dgvDatos.CellClick += new DataGridViewCellEventHandler(dgvDatos_CellClick);            
        }

        private void frmFondoCajaCons_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            dt.Tables[0].TableName = "FondoCaja";
            bindingSource1.DataSource = dt.Tables[0];
            bindingNavigator1.BindingSource = bindingSource1;
            dgvDatos.DataSource = bindingSource1;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.Columns["FechaFONP"].HeaderText = "Fecha";
            dgvDatos.Columns["NombreLOC"].HeaderText = "Local";
            dgvDatos.Columns["Detalle"].HeaderText = "Caja";
            dgvDatos.Columns["ImporteFONP"].HeaderText = "Importe";
            dgvDatos.Columns["IdFondoFONP"].Visible = false;
            dgvDatos.Columns["IdPcFONP"].Visible = false;
            dgvDatos.Columns["IdLocalLOC"].Visible = false;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == dgvDatos.Columns["Editar"].Index)
            {
                int PK = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdFondoFONP"].Value.ToString());
                frmFondoCaja frm = new frmFondoCaja(dt.Tables[0], PK);
                frm.fecha = Convert.ToDateTime(dgvDatos.CurrentRow.Cells["FechaFONP"].Value.ToString());
                frm.idPc = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdPcFONP"].Value.ToString());
                frm.idLocal = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdLocalLOC"].Value.ToString());
                frm.ShowDialog();
            }
            if (e.ColumnIndex == dgvDatos.Columns["Borrar"].Index)
            {
                if (MessageBox.Show("¿Desea borrar este registro?", "Buscar",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int PK = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdFondoFONP"].FormattedValue.ToString());
                    string formularioOrigen = "frmFondoCajaCons";
                    string accionProgress = "grabar";
                    frmProgress progreso = new frmProgress(PK, formularioOrigen, accionProgress, ref bindingSource1);
                    progreso.ShowDialog();
                }
            }
        }

        private void dgvDatos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }
    }
}
