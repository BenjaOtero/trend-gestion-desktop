using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Configuration;


namespace StockVentas
{
    public partial class frmVentasHistoricasInter : Form
    {
        [DllImport("user32.dll", SetLastError = true)] //dll necesaria para matar proceso excel
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr ProcessId);  //dll necesaria para matar proceso excel

        Excel.Application app = new Excel.Application();
        Excel.Workbook libro;

        public frmVentasHistoricasInter()
        {
            InitializeComponent();
        }

        private void frmExcel_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            dateTimePicker1.Value = dateTimePicker1.Value.AddYears(-1);
            int year = DateTime.Now.Year;
            year = year - 1;
            DateTime baseDate = new DateTime(year, 1, 1);
            dateTimePicker1.Value = baseDate;
        }

        private void frmVentasHistoricasInter_FormClosing(object sender, FormClosingEventArgs e)
        {
            killExcel();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            libro = app.Workbooks.Add();
            libro.DefaultPivotTableStyle = "PivotStyleLight26";
            Excel.PivotCache pivotCache = libro.PivotCaches().Add(Excel.XlPivotTableSourceType.xlExternal);
            string MyConString = ConfigurationManager.ConnectionStrings["ODBCExcel"].ConnectionString;
            //string MyConString = ConfigurationManager.ConnectionStrings["ODBCExcelLocal"].ConnectionString;
            //string MyConString = ConfigurationManager.ConnectionStrings["ODBCMinusculas"].ConnectionString;
            string strFecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string command = "SELECT * FROM ventash  WHERE Fecha >='" + strFecha + "'";
            pivotCache.Connection = MyConString;
            pivotCache.CommandText = command;

            #region ValorAgregado

            Excel.Worksheet sheetAgregado = libro.Sheets.Add();
            libro.Sheets[libro.ActiveSheet.Name].Select();
            libro.Sheets[libro.ActiveSheet.Name].Name = "Valor agregado";   
        
            Excel.PivotTables pivotTablesAgregado = sheetAgregado.PivotTables();
            Excel.PivotTable pivotTableAgregado = pivotTablesAgregado.Add(pivotCache, app.Range["A4"], "Valor agregado");
            sheetAgregado.PivotTables("Valor agregado").PivotFields("NombreLocal").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetAgregado.PivotTables("Valor agregado").PivotFields("NombreLocal").Position = 1;
            sheetAgregado.PivotTables("Valor agregado").PivotFields("FormaPago").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetAgregado.PivotTables("Valor agregado").PivotFields("FormaPago").Position = 2;
            sheetAgregado.PivotTables("Valor agregado").PivotFields("Fecha").Orientation = Excel.XlPivotFieldOrientation.xlRowField;

            app.Range["B6"].Select();
            sheetAgregado.PivotTables("Valor agregado").ColumnGrand = false;
            sheetAgregado.PivotTables("Valor agregado").RowGrand = false;
            sheetAgregado.PivotTables("Valor agregado").TableStyle2 = "PivotStyleLight26";
            sheetAgregado.PivotTables("Valor agregado").CalculatedFields.Add("ValorAgregado", "=IF(TotalPublico>0,(TotalPublico/TotalCosto)-1)", true);
            sheetAgregado.PivotTables("Valor agregado").PivotFields("ValorAgregado").Orientation = Excel.XlPivotFieldOrientation.xlDataField;
            sheetAgregado.PivotTables("Valor agregado").PivotFields("Suma de ValorAgregado").Caption = "Valor agregado";
            sheetAgregado.PivotTables("Valor agregado").PivotFields("Valor agregado").NumberFormat = "0,00%";
            sheetAgregado.Cells[6, 1].Select();
            object[] periodosValor = { false, false, false, false, true, false, true };
            Excel.Range rangeValor = sheetAgregado.get_Range("a6");
            rangeValor.Group(true, true, 1, periodosValor);
            libro.ShowPivotTableFieldList = false;
            app.Range["A5"].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlDown)].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight)].Select();
            Excel.Range rangoGrafico = app.Selection;
            app.ActiveSheet.Shapes.AddChart.Select();
            app.ActiveSheet.Shapes(1).Name = "Valor agregado";
            app.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumn;
            app.ActiveChart.SetSourceData(Source: rangoGrafico);
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveChart.ChartStyle = 42;
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveSheet.Shapes["Valor agregado"].Left = 0;
            app.ActiveSheet.Shapes["Valor agregado"].Top = 300;
            app.ActiveSheet.Shapes["Valor agregado"].ScaleWidth(1.663541776, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Valor agregado"].ScaleHeight(1.2777777778, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Valor agregado"].ThreeD.RotationX = -30;
            app.ActiveSheet.Shapes["Valor agregado"].ThreeD.RotationY = 100;
            app.ActiveSheet.Shapes["Valor agregado"].ThreeD.FieldOfView = 10;
            app.ActiveChart.ChartTitle.Text = "Valor agregado";
            app.Range["A1"].Select();
            #endregion

            #region Prendas
            Excel.Worksheet sheetPrendas = libro.Sheets.Add();
            string hojaPrendas = libro.ActiveSheet.Name;
            libro.Sheets[hojaPrendas].Select();
            libro.Sheets[hojaPrendas].Name = "Prendas";
            Excel.PivotTables pivotTablesPrendas = sheetPrendas.PivotTables();
            Excel.PivotTable pivotTablePrendas = pivotTablesPrendas.Add(pivotCache, app.Range["A4"], "Prendas");
            sheetPrendas.PivotTables("Prendas").TableStyle2 = "PivotStyleLight26";
            sheetPrendas.PivotTables("Prendas").PivotFields("NombreLocal").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetPrendas.PivotTables("Prendas").PivotFields("NombreLocal").Position = 1;
            sheetPrendas.PivotTables("Prendas").PivotFields("FormaPago").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetPrendas.PivotTables("Prendas").PivotFields("FormaPago").Position = 2;
            sheetPrendas.PivotTables("Prendas").PivotFields("Fecha").Orientation = Excel.XlPivotFieldOrientation.xlRowField;
            Excel.PivotField fldTotalPrendas = pivotTablePrendas.PivotFields("Prendas");
            fldTotalPrendas.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
            fldTotalPrendas.Function = Excel.XlConsolidationFunction.xlSum;
            fldTotalPrendas.Name = " Prendas";
            sheetPrendas.Cells[6, 1].Select();
            object[] periodosPrendas = { false, false, false, false, true, false, true };
            Excel.Range rangePrendas = sheetPrendas.get_Range("a6");
            rangePrendas.Group(true, true, 1, periodosPrendas);
            libro.ShowPivotTableFieldList = false;
            app.Range["A5"].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlDown)].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight)].Select();
            Excel.Range rangoGraficoPrendas = app.Selection;
            app.ActiveSheet.Shapes.AddChart.Select();
            app.ActiveSheet.Shapes(1).Name = "Prendas";
            app.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumn;
            app.ActiveChart.SetSourceData(Source: rangoGraficoPrendas);
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveChart.ChartStyle = 42;
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveSheet.Shapes["Prendas"].Left = 0;
            app.ActiveSheet.Shapes["Prendas"].Top = 300;
            app.ActiveSheet.Shapes["Prendas"].ScaleWidth(1.663541776, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Prendas"].ScaleHeight(1.2777777778, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Prendas"].ThreeD.RotationX = -30;
            app.ActiveSheet.Shapes["Prendas"].ThreeD.RotationY = 100;
            app.ActiveSheet.Shapes["Prendas"].ThreeD.FieldOfView = 10;
            app.ActiveSheet.PivotTables("Prendas").RowGrand = false;
            app.ActiveChart.ChartTitle.Text = "Unidades vendidas";
            app.Range["A1"].Select();
            #endregion

            #region Periodos

            Excel.Worksheet sheetDiferenciaPeriodos = libro.Sheets.Add();
            libro.Sheets[libro.ActiveSheet.Name].Select();
            libro.Sheets[libro.ActiveSheet.Name].Name = "Diferencia períodos";   

            Excel.PivotTables pivotTablesPeriodos = sheetDiferenciaPeriodos.PivotTables();
            Excel.PivotTable pivotTableDif = pivotTablesPeriodos.Add(pivotCache, app.Range["A4"], "Diferencia periodos");
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("NombreLocal").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("NombreLocal").Position = 1;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("FormaPago").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("FormaPago").Position = 2;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("Fecha").Orientation = Excel.XlPivotFieldOrientation.xlRowField;
            Excel.PivotField fldTotalPeriodo = pivotTableDif.PivotFields("TotalPublico");
            fldTotalPeriodo.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
            fldTotalPeriodo.Function = Excel.XlConsolidationFunction.xlSum;
            fldTotalPeriodo.Name = " Ventas períodos";
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("Años").Orientation = Excel.XlPivotFieldOrientation.xlColumnField;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("Años").Position = 1;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").NumberFormat = "$ #.##0";
            libro.ShowPivotTableFieldList = false;
            app.Range["B6"].Select();
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").ColumnGrand = false;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").RowGrand = false;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").TableStyle2 = "PivotStyleLight26";
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").Calculation = Excel.XlPivotFieldCalculation.xlPercentDifferenceFrom;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").BaseField = "Años";
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").BaseItem = "(anterior)";
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").NumberFormat = "0,00%";

            app.Range["A5"].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlDown)].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight)].Select();
            Excel.Range rangoGraficoPeriodos = app.Selection;
            app.ActiveSheet.Shapes.AddChart.Select();
            app.ActiveSheet.Shapes(1).Name = "Diferencia periodos";
            app.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumn;
            app.ActiveChart.SetSourceData(Source: rangoGraficoPeriodos);
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveChart.ChartStyle = 42;
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveSheet.Shapes["Diferencia periodos"].Left = 0;
            app.ActiveSheet.Shapes["Diferencia periodos"].Top = 300;
            app.ActiveSheet.Shapes["Diferencia periodos"].ScaleWidth(1.663541776, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Diferencia periodos"].ScaleHeight(1.2777777778, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Diferencia periodos"].ThreeD.RotationX = -30;
            app.ActiveSheet.Shapes["Diferencia periodos"].ThreeD.RotationY = 100;
            app.ActiveSheet.Shapes["Diferencia periodos"].ThreeD.FieldOfView = 10;
            app.Range["A1"].Select();
            #endregion

            #region Ventas2
            Excel.Worksheet sheetVentas2 = libro.Sheets.Add();
            string hojaVentas2 = libro.ActiveSheet.Name;
            libro.Sheets[hojaVentas2].Select();
            libro.Sheets[hojaVentas2].Name = "Ventas2";
            Excel.PivotTables pivotTablesVentas2 = sheetVentas2.PivotTables();
            Excel.PivotTable pivotTableVentas2 = pivotTablesVentas2.Add(pivotCache, app.Range["A4"], "Ventas2");
            sheetVentas2.PivotTables("Ventas2").TableStyle2 = "PivotStyleLight26";
            sheetVentas2.PivotTables("Ventas2").PivotFields("FormaPago").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetVentas2.PivotTables("Ventas2").PivotFields("FormaPago").Position = 1;
            sheetVentas2.PivotTables("Ventas2").PivotFields("Fecha").Orientation = Excel.XlPivotFieldOrientation.xlRowField;
            sheetVentas2.PivotTables("Ventas2").PivotFields("NombreLocal").Orientation = Excel.XlPivotFieldOrientation.xlColumnField;
            Excel.PivotField fldTotalVentas2 = pivotTableVentas2.PivotFields("TotalPublico");
            fldTotalVentas2.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
            fldTotalVentas2.Function = Excel.XlConsolidationFunction.xlSum;
            fldTotalVentas2.Name = " Ventas";

            Excel.PivotField fldTotalPrendas2 = pivotTableVentas2.PivotFields("Prendas");
            fldTotalPrendas2.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
            fldTotalPrendas2.Function = Excel.XlConsolidationFunction.xlSum;
            fldTotalPrendas2.Name = " Prendas";

            sheetVentas2.Cells[6, 1].Select();
            object[] periodosVentas2 = { false, false, false, false, true, false, true };
            Excel.Range rangeVentas2 = sheetVentas2.get_Range("a6");
            rangeVentas2.Group(true, true, 1, periodosVentas2);
            sheetVentas2.PivotTables("Ventas2").PivotFields(" Ventas").NumberFormat = "$ #.##0";
            libro.ShowPivotTableFieldList = false;
            app.ActiveSheet.PivotTables("Ventas2").RowGrand = false;
            app.Range["A1"].Select();
            #endregion

            #region Ventas
            Excel.Worksheet sheetVentas = libro.Sheets.Add();
            string hojaVentas = libro.ActiveSheet.Name;
            libro.Sheets[hojaVentas].Select();
            libro.Sheets[hojaVentas].Name = "Ventas";
            Excel.PivotTables pivotTablesVentas = sheetVentas.PivotTables();
            Excel.PivotTable pivotTableVentas = pivotTablesVentas.Add(pivotCache, app.Range["A4"], "Ventas");
            sheetVentas.PivotTables("Ventas").TableStyle2 = "PivotStyleLight26";
            sheetVentas.PivotTables("Ventas").PivotFields("NombreLocal").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetVentas.PivotTables("Ventas").PivotFields("NombreLocal").Position = 1;
            sheetVentas.PivotTables("Ventas").PivotFields("FormaPago").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetVentas.PivotTables("Ventas").PivotFields("FormaPago").Position = 2;
            sheetVentas.PivotTables("Ventas").PivotFields("Fecha").Orientation = Excel.XlPivotFieldOrientation.xlRowField;
            Excel.PivotField fldTotalVentas = pivotTableVentas.PivotFields("TotalPublico");
            fldTotalVentas.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
            fldTotalVentas.Function = Excel.XlConsolidationFunction.xlSum;
            fldTotalVentas.Name = " Ventas";
            sheetVentas.Cells[6, 1].Select();
            object[] periodosVentas = { false, false, false, false, true, false, true };
            Excel.Range rangeVentas = sheetVentas.get_Range("a6");
            rangeVentas.Group(true, true, 1, periodosVentas);
            sheetVentas.PivotTables("Ventas").PivotFields("Años").Orientation = Excel.XlPivotFieldOrientation.xlColumnField;
            sheetVentas.PivotTables("Ventas").PivotFields("Años").Position = 1;
            sheetVentas.PivotTables("Ventas").PivotFields(" Ventas").NumberFormat = "$ #.##0";
            libro.ShowPivotTableFieldList = false;
            app.Range["A5"].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlDown)].Select();
            app.Range[app.Selection, app.Selection.End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight)].Select();
            Excel.Range rangoGraficoVentas = app.Selection;
            app.ActiveSheet.Shapes.AddChart.Select();
            app.ActiveSheet.Shapes(1).Name = "Ventas";
            app.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumn;
            app.ActiveChart.SetSourceData(Source: rangoGraficoVentas);
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveChart.ChartStyle = 42;
            app.ActiveChart.ClearToMatchStyle();
            app.ActiveSheet.Shapes["Ventas"].Left = 0;
            app.ActiveSheet.Shapes["Ventas"].Top = 300;
            app.ActiveSheet.Shapes["Ventas"].ScaleWidth(1.663541776, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Ventas"].ScaleHeight(1.2777777778, Microsoft.Office.Core.MsoTriState.msoFalse
                    , Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
            app.ActiveSheet.Shapes["Ventas"].ThreeD.RotationX = -30;
            app.ActiveSheet.Shapes["Ventas"].ThreeD.RotationY = 100;
            app.ActiveSheet.Shapes["Ventas"].ThreeD.FieldOfView = 10;
            app.ActiveSheet.PivotTables("Ventas").RowGrand = false;
            app.Range["A1"].Select();
            #endregion

            sheetPrendas.PivotTables("Prendas").PivotFields("Años").Orientation = Excel.XlPivotFieldOrientation.xlColumnField;
            sheetPrendas.PivotTables("Prendas").PivotFields("Años").Position = 1;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("Años").Orientation = Excel.XlPivotFieldOrientation.xlColumnField;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields("Años").Position = 1;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").Calculation = Excel.XlPivotFieldCalculation.xlPercentDifferenceFrom;
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").BaseField = "Años";
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").BaseItem = "(anterior)";
            sheetDiferenciaPeriodos.PivotTables("Diferencia periodos").PivotFields(" Ventas períodos").NumberFormat = "0,00%";
            sheetAgregado.PivotTables("Valor agregado").PivotFields("Años").Orientation = Excel.XlPivotFieldOrientation.xlColumnField;
            sheetAgregado.PivotTables("Valor agregado").PivotFields("Años").Position = 1;
            sheetVentas2.PivotTables("Ventas2").PivotFields("Años").Orientation = Excel.XlPivotFieldOrientation.xlPageField;
            sheetVentas2.PivotTables("Ventas2").PivotFields("Años").Position = 2;
            app.ActiveWorkbook.Connections["Conexión"].ODBCConnection.Connection = "ODBC;DATABASE";                   
            libro.Sheets["Hoja1"].Select();
            app.ActiveWindow.SelectedSheets.Delete();
            libro.Sheets["Ventas"].Select();
            libro.Saved = true;     
            app.Visible = true;
            Cursor.Current = Cursors.Arrow;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void killExcel()
        {
            IntPtr hwnd = new IntPtr(app.Hwnd);
            IntPtr processId;
            IntPtr foo = GetWindowThreadProcessId(hwnd, out processId);
            Process proc = Process.GetProcessById(processId.ToInt32());
            proc.Kill(); // set breakpoint here and watch the Windows Task Manager kill this exact EXCEL.EXE            
        }        

    }
}
