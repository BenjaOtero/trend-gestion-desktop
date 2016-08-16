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
    public partial class frmEmpleadosMovConsInter : Form
    {
        private DataTable tblEmpleados;

        public frmEmpleadosMovConsInter()
        {
            InitializeComponent();
        }

        private void frmEmpleadosMovConsInter_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblEmpleados = BL.GetDataBLL.Empleados();
            if (!tblEmpleados.Columns.Contains("ApellidoNombreEMP")) tblEmpleados.Columns.Add("ApellidoNombreEMP", typeof(string));
            foreach (DataRow rowEmpleado in tblEmpleados.Rows)
            {
                string apellido = rowEmpleado["ApellidoEMP"].ToString();
                string nombre = rowEmpleado["NombreEMP"].ToString();
                rowEmpleado["ApellidoNombreEMP"] = apellido + ", " + nombre;
            }
            lstEmpleados.DataSource = tblEmpleados;
            lstEmpleados.DisplayMember = "ApellidoNombreEMP";
            lstEmpleados.ValueMember = "IdEmpleadoEMP";

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string strFechaDesde = dateTimeDesde.Value.ToString("yyyy-MM-dd");
            string strFechaHasta = dateTimeHasta.Value.ToString("yyyy-MM-dd");
            int idEmpleado;
            if (chkTodos.Checked != true)
            {
                idEmpleado = Convert.ToInt32(lstEmpleados.SelectedValue.ToString());
            }
            else
            {
                idEmpleado = 0;
            }
            int liquidado;
            if (chkLiquidado.Checked) liquidado = 1;
            else liquidado = 0;
            frmProgress progreso = new frmProgress(strFechaDesde, strFechaHasta, idEmpleado, liquidado, "frmEmpleadosMovConsInter", "cargar");
            progreso.ShowDialog();
            DataTable tbl = frmProgress.tblEstatica;
            frmEmpleadosMovCons frmCons = new frmEmpleadosMovCons(tbl);
            frmCons.Show();

        }

        private void chkTodos_Click(object sender, EventArgs e)
        {
            if (chkTodos.Checked) lstEmpleados.Enabled = false;
            else lstEmpleados.Enabled = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
