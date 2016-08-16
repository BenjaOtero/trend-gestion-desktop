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
    public partial class StockRpt : Form
    {
        DataTable cruzadas;

        public StockRpt(DataTable cruzadas)
        {
            InitializeComponent();
            this.cruzadas = cruzadas;
        }

        private void rptPrueba_Load(object sender, EventArgs e)
        {
            DataRow row = cruzadas.Rows[0];
            string local = row["NombreLOC"].ToString();
            ReportParameter parameters = new ReportParameter("parametroLocal", local);
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            string path = Application.StartupPath + @"\Informes\Stock.rdlc";
            this.reportViewer1.LocalReport.ReportPath = path;
            reportViewer1.LocalReport.DataSources.Add(
            new ReportDataSource("Dataset_informes", cruzadas));
            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.RefreshReport();
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
