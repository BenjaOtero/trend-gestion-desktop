using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;


namespace StockVentas
{
    public partial class PedidoRpt : Form
    {
        DataTable tblPedido;

        public PedidoRpt(DataTable tblPedido)
        {
            InitializeComponent();
            this.tblPedido = tblPedido;
        }

        private void rptPrueba_Load(object sender, EventArgs e)
        {
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            string path = Application.StartupPath + @"\Informes\Pedido.rdlc";
            this.reportViewer1.LocalReport.ReportPath = path;
            reportViewer1.LocalReport.DataSources.Add(
            new ReportDataSource("Dataset_informes", tblPedido));
            this.reportViewer1.RefreshReport();
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
