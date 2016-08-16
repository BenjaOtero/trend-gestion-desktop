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
    public partial class VentasDetalleRpt : Form
    {
        DataTable tblVentasDetalle;
        string nombreLocal;

        public VentasDetalleRpt(DataTable tblVentasDetalle, string nombreLocal)
        {
            InitializeComponent();
            this.tblVentasDetalle = tblVentasDetalle;
            this.nombreLocal = nombreLocal;
        }

        private void VentasDetalleRpt_Load(object sender, EventArgs e)
        {
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Text = "  Ventas en detalle";
            this.Icon = ico;
            this.ControlBox = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            ReportParameter parameters = new ReportParameter("parametroLocal", nombreLocal);
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            string path = Application.StartupPath + @"\Informes\VentasDetalle.rdlc";
            this.reportViewer1.LocalReport.ReportPath = path;
            reportViewer1.LocalReport.DataSources.Add(
            new ReportDataSource("Dataset_informes", tblVentasDetalle));
            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.RefreshReport();
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
