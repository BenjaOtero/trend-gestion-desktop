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
    public partial class frmStockInforme : Form
    {
        public frmStockInforme(DataTable datos)
        {
            InitializeComponent();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.WindowState = FormWindowState.Maximized;
            this.VScroll = true;
            dgvDatos.DataSource = datos;
            dgvDatos.AutoSize = true;            
        }

        private void dgvDatos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }
    }
}
