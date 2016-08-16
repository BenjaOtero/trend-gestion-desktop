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
    public partial class frmEmpleadosMovCons : Form
    {
        DataTable tblEmpleadosMov;

        public frmEmpleadosMovCons(DataTable tblEmpleadosMov)
        {
            InitializeComponent();
            this.tblEmpleadosMov = tblEmpleadosMov;
            DataGridViewImageColumn imageColumn2 = new DataGridViewImageColumn();
            Image image2 = global::StockVentas.Properties.Resources.document_edit;
            imageColumn2.Image = image2;
            imageColumn2.Name = "Editar";
            dgvEmpleados.Columns.Add(imageColumn2);
        }

        private void frmEmpleadosMovCons_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            bindingSource1.DataSource = tblEmpleadosMov;
            bindingNavigator1.BindingSource = bindingSource1;
            dgvEmpleados.DataSource = bindingSource1;
            dgvEmpleados.AllowUserToAddRows = false;
            dgvEmpleados.AllowUserToDeleteRows = false;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEmpleados.Columns["FechaEMOV"].HeaderText = "Fecha";
            dgvEmpleados.Columns["FechaEMOV"].ReadOnly = true;
            dgvEmpleados.Columns["ApellidoEMP"].HeaderText = "Apellido";
            dgvEmpleados.Columns["ApellidoEMP"].ReadOnly = true;
            dgvEmpleados.Columns["NombreEMP"].HeaderText = "Nombre";
            dgvEmpleados.Columns["NombreEMP"].ReadOnly = true;
            dgvEmpleados.Columns["CantidadEMOV"].HeaderText = "Cantidad";
            dgvEmpleados.Columns["CantidadEMOV"].ReadOnly = true;
            dgvEmpleados.Columns["DetalleEMOV"].HeaderText = "Detalle";
            dgvEmpleados.Columns["DetalleEMOV"].ReadOnly = true;
            dgvEmpleados.Columns["ImporteEMOV"].HeaderText = "Importe";
            dgvEmpleados.Columns["ImporteEMOV"].ReadOnly = true;
            dgvEmpleados.Columns["LiquidadoEMOV"].HeaderText = "Liquidado";
            dgvEmpleados.Columns["IdMovEMOV"].Visible = false;
            dgvEmpleados.Columns["IdEmpleadoEMOV"].Visible = false;
            dgvEmpleados.Columns["IdMovTipoEMOV"].Visible = false;
            btnGrabar.Enabled = false;
            tblEmpleadosMov.ColumnChanged += new DataColumnChangeEventHandler(HabilitarGrabar);
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == dgvEmpleados.Columns["Editar"].Index)
            {
                string PK = dgvEmpleados.CurrentRow.Cells["IdMovEMOV"].Value.ToString();
                frmEmpleadosMov frm = new frmEmpleadosMov(tblEmpleadosMov, PK);
                frm.ShowDialog();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            bindingSource1.EndEdit();
            if (tblEmpleadosMov.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblEmpleadosMov, "frmEmpleadosMov", "grabar");
                progreso.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void HabilitarGrabar(object sender, EventArgs e)
        {
            btnGrabar.Enabled = true;
        }

        private void dgvEmpleados_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }
}
