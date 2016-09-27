using System;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class frmRPTstock : Form
    {
        public frmRPTstock()
        {
            InitializeComponent();
        }

        private void frmRPTarticulos_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
