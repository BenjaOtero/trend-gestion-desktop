using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BL;

namespace StockVentas
{
    public partial class frmPedido : Form
    {
        DataTable tblPedidos;
        string strFecha;

        public frmPedido()
        {
            InitializeComponent();
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.Text = "  Pedidos";
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            cmbGenero.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbGenero.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            DataTable tblGeneros = BL.GetDataBLL.Generos();
            cmbGenero.ValueMember = "IdGeneroGEN";
            cmbGenero.DisplayMember = "DescripcionGEN";
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDown;
            cmbGenero.DataSource = tblGeneros;
            cmbGenero.SelectedValue = -1;
            AutoCompleteStringCollection generosColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblGeneros.Rows)
            {
                generosColection.Add(Convert.ToString(row["DescripcionGEN"]));
            }
            cmbGenero.AutoCompleteCustomSource = generosColection;
            cmbGenero.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbGenero.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cmbGenero.Text)){
                MessageBox.Show("Debe indicar un género.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbGenero.Focus();
            }
                
            try
            {
                strFecha = dateTimeDesde.Value.ToString("yyyy-MM-dd");
                string genero = cmbGenero.SelectedValue.ToString();
                frmProgress frm = new frmProgress(strFecha, "frmPedido", "cargar", genero);
                frm.ShowDialog();
                tblPedidos = frmProgress.dtEstatico.Tables[0];
                tblPedidos.AcceptChanges();
                PedidoRpt frmPedido = new PedidoRpt(tblPedidos);
                frmPedido.Show();
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        private void ExportarDataGridViewExcel(DataTable tbl)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo = libros_trabajo.Worksheets.Add();
                int h = 1;
                foreach (DataColumn col in tbl.Columns)
                {
                    hoja_trabajo.Cells[1, h] = col.ColumnName.ToString();
                    h++;
                }
                hoja_trabajo.Cells[1, h] = "Pedido";
                //Recorremos el datatable rellenando la hoja de trabajo
                int i = 0;
                int r =1;
                foreach(DataRow row in tblPedidos.Rows)
                {
                    for (int j = 0; j < tbl.Columns.Count; j++)
                    {
                        hoja_trabajo.Cells[r + 1, j + 1] = tbl.Rows[i][j].ToString();
                    }
                    i++;
                    r++;
                }


                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
            }
        }
    }
}
