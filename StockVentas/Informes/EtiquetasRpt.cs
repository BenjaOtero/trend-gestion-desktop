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
    public partial class EtiquetasRpt : Form
    {
        private DataTable tblEtiquetas;
        private bool imprimePrecio;

        public EtiquetasRpt(DataTable tblEtiquetas, bool imprimePrecio)
        {
            InitializeComponent();
            this.tblEtiquetas = tblEtiquetas;
            this.imprimePrecio = imprimePrecio;
        }

        private void EtiquetasRpt_Load(object sender, EventArgs e)
        {
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            string path;
            if (imprimePrecio)
            {
                path = Application.StartupPath + @"\Informes\Etiquetasx2_precio.rdlc";
            }
            else
            {
                path = Application.StartupPath + @"\Informes\Etiquetasx2.rdlc";
            }
            this.reportViewer1.LocalReport.ReportPath = path;
            reportViewer1.LocalReport.DataSources.Add(
            new ReportDataSource("Dataset_informes", tblEtiquetas));
            reportViewer1.PrinterSettings.PrinterName = "SATO CG408";

            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            // Set margins
            pg.Margins = new System.Drawing.Printing.Margins(9, 0, 0, 7); //centesimas de pulgada
            pg.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 417, 110); //centesimas de pulgada
            pg.PrinterResolution.Kind = System.Drawing.Printing.PrinterResolutionKind.High;
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();
        }

    }
}
