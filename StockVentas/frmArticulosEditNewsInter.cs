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
    public partial class frmArticulosEditNewsInter : Form
    {
        private DataTable tblArticulos;

        public frmArticulosEditNewsInter(DataTable tblArticulos)
        {
            InitializeComponent();
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.tblArticulos = tblArticulos;
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime dTimeDesde = DateTime.Parse(dtpDesde.Value.ToString("dd/MM/yyyy"));
            DateTime dTimeHasta = DateTime.Parse(dtpHasta.Value.AddDays(1).ToString("dd/MM/yyyy"));

            frmArticulosEditNews frm = new frmArticulosEditNews(tblArticulos, dTimeDesde, dTimeHasta);
            frm.Show();
            Cursor.Current = Cursors.Arrow;
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
