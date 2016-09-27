using System.Data;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class frmStockInforme : Form
    {
        public frmStockInforme(DataView datos)
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
