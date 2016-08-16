using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace StockVentas
{
    public partial class StockMovRpt : Form
    {
        public DataSet dsStockMov;
        DataTable tblStockMovDetalle;
        DataTable tblLocales;
        DataView viewStockMov;  
        public int idLocal;
        public DateTime fechaDesde;
        public DateTime fechaHasta;
        public string opcVista;
        public string opcMov;
        public string tipo;
        string opcOrden;

        public StockMovRpt(DataSet dsStockMov, string opcOrden)
        {
            InitializeComponent();
            this.dsStockMov = dsStockMov;
            this.opcOrden = opcOrden;
        }

        private void frmStockMovInforme_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
          /*  strFechaDesde = fechaDesde.ToString("yyyy-MM-dd 00:00:00");
            strFechaHasta = fechaHasta.ToString("yyyy-MM-dd 00:00:00");
            formularioOrigen = "frmStockMovInforme";
            accionProgress = "cargar";
            progreso = new frmProgress(strFechaDesde, strFechaHasta, idLocal, tipo, opcMov, formularioOrigen, accionProgress);
            progreso.ShowDialog();
            dsStockMov = frmProgress.dsStockMovCons;
            if (dsStockMov == null) return; // no hay conexion*/
            tblStockMovDetalle = dsStockMov.Tables[1];
            tblStockMovDetalle.TableName = "StockMovDetalle";
            tblLocales = BL.GetDataBLL.Locales();
            tblLocales.PrimaryKey = new DataColumn[] { tblLocales.Columns["IdLocalLOC"] };
            tblStockMovDetalle.Columns.Add("OrigenMSTK", typeof(string));
            tblStockMovDetalle.Columns.Add("DestinoMSTK", typeof(string));
            viewStockMov = new DataView(tblStockMovDetalle);
            int local;
            foreach (DataRowView fila in viewStockMov)
            {
                local = Convert.ToInt32(fila["OrigenMSTKD"].ToString());
                DataRow foundRow = tblLocales.Rows.Find(local);
                fila["OrigenMSTK"] = foundRow[1].ToString();
                local = Convert.ToInt32(fila["DestinoMSTKD"].ToString());
                DataRow foundRow2 = tblLocales.Rows.Find(local);
                fila["DestinoMSTK"] = foundRow2[1].ToString();
            }
            Cursor.Current = Cursors.Arrow;
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            string path = Application.StartupPath + @"\Informes\StockMov.rdlc";
            DataView view = new DataView(tblStockMovDetalle);
            if (opcOrden == "movimiento") view.Sort = "FechaMSTK, ordenar";
            else view.Sort = "FechaMSTK, DescripcionART";
            this.reportViewer1.LocalReport.ReportPath = path;
            
            
            reportViewer1.LocalReport.DataSources.Add(
            new ReportDataSource("DataSet1", view));
            //      this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.RefreshReport();
        }

    }
}
