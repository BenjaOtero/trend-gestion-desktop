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
    public partial class StockCompPesosRpt : Form
    {
        DataTable tblStockCompPesos;
        string fechaDesde;
        string fechaHasta;

        public StockCompPesosRpt(DataTable tbl, string desde, string hasta)
        {
            InitializeComponent();
            tblStockCompPesos = tbl;
            fechaDesde = desde;
            fechaHasta = hasta;
        }

        private void StockCompPesosRpt_Load(object sender, EventArgs e)
        {
            if (tblStockCompPesos == null)
            {
                this.Close();
                return;
            }
            if (tblStockCompPesos.Rows.Count == 0)
            {
                MessageBox.Show("No existen movimientos entre las fechas especificadas", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            DataTable tblLocales = BL.GetDataBLL.Locales();
            DataRow row = tblStockCompPesos.Rows[0];
            int idLocal = Convert.ToInt32(row["DestinoMSTK"].ToString());            
            DataRow[] rowLocales = tblLocales.Select("IdLocalLOC = " + idLocal);
            string strLocal = rowLocales[0][1].ToString();
            ReportParameter[] parametros = new ReportParameter[3];
            parametros[0] = new ReportParameter("parametroLocal", strLocal);
            parametros[1] = new ReportParameter("parametroDesde", fechaDesde);
            parametros[2] = new ReportParameter("parametroHasta", fechaHasta);
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            string path = Application.StartupPath + @"\Informes\StockCompPesos.rdlc";
            this.reportViewer1.LocalReport.ReportPath = path;
            reportViewer1.LocalReport.DataSources.Add(
            new ReportDataSource("Dataset_informes", tblStockCompPesos));
            this.reportViewer1.LocalReport.SetParameters(parametros);
            this.reportViewer1.RefreshReport();
            this.HorizontalScroll.Enabled = false;
        }
    }
}
