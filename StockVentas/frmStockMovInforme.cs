using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StockVentas
{
    public partial class frmStockMovInforme : Form
    {
        public DataSet dsStockMov;
        DataTable tblStockMov;
        DataTable tblStockMovDetalle;
        DataTable tblLocales;
        DataView viewStockMov;
        private string tipo;
        string articulo;
        string descripcion;
        int PK;
        frmProgress progreso;
        private int? codigoError = null;
        DataRowCollection cfilas;
        DataRow nuevaFila;


        public frmStockMovInforme(DataSet dsStockMov, string tipo, string articulo, string descripcion)
        {
            InitializeComponent();
            this.dsStockMov = dsStockMov;
            this.tipo = tipo;
            this.articulo = articulo;
            this.descripcion = descripcion;
        }

        private void frmStockMovInforme_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Cursor.Current = Cursors.WaitCursor;
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            try
            {
                tblStockMov = dsStockMov.Tables[0];
            }
            catch (NullReferenceException)
            {
                return;
            }
            tblStockMov.TableName = "StockMov";
            tblStockMovDetalle = dsStockMov.Tables[1];
            tblStockMovDetalle.TableName = "StockMovDetalle";
            if (tipo == "compensaciones")
            {
                label1.Text = "Compensaciones de stock";
                label2.Text = "Detalle de compensaciones";
                this.Text = "Informe de compensaciones de stock";
            }
            tblLocales = BL.GetDataBLL.Locales();
            tblLocales.PrimaryKey = new DataColumn[] { tblLocales.Columns["IdLocalLOC"] };
            tblStockMov.Columns.Add("Origen", typeof(string));
            tblStockMov.Columns.Add("Destino", typeof(string));
            viewStockMov = new DataView(tblStockMov);
            int local;
            foreach (DataRowView fila in viewStockMov)
            {
                local = Convert.ToInt32(fila["OrigenMSTK"].ToString());
                DataRow foundRow = tblLocales.Rows.Find(local);
                fila["Origen"] = foundRow[1].ToString();
                local = Convert.ToInt32(fila["DestinoMSTK"].ToString());
                DataRow foundRow2 = tblLocales.Rows.Find(local);
                fila["Destino"] = foundRow2[1].ToString();
            }
            dgvStockMov.AllowUserToAddRows = false;
            bindingSource1.DataSource = dsStockMov;
            bindingSource1.DataMember = dsStockMov.Tables[0].ToString();
            bindingNavigator1.BindingSource = bindingSource1;
            dgvStockMov.DataSource = bindingSource1;
            dgvStockMov.Columns["CompensaMSTK"].Visible = false;
            dgvStockMov.Columns["OrigenMSTK"].Visible = false;
            dgvStockMov.Columns["DestinoMSTK"].Visible = false;
            if (tipo == "compensaciones")
            {
                dgvStockMov.Columns["Origen"].Visible = false;
                dgvStockMov.Columns["Destino"].HeaderText = "Local";
            }
            dgvStockMov.Columns["IdMovMSTK"].HeaderText = "Nº mov.";
            dgvStockMov.Columns["FechaMSTK"].HeaderText = "Fecha";
            dgvStockMov.Columns["DestinoMSTK"].Visible = false;
            dgvStockMov.ReadOnly = true;
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "StockMovDetalle"; // StockMovDetalle es el nombre de la DataRelation creada en CrearDatasetCons en StockMov.BLL
            bindingSource2.Sort = "ordenar ASC";
            bindingNavigator2.BindingSource = bindingSource2;
            dgvStockDet.DataSource = bindingSource2;
            dgvStockDet.Columns["ordenar"].Visible = false;
            dgvStockDet.Columns["IdMovMSTKD"].Visible = false;
            dgvStockDet.Columns["FechaMSTK"].Visible = false;
            dgvStockDet.Columns["CompensaMSTKD"].Visible = false;
            dgvStockDet.Columns["OrigenMSTKD"].Visible = false;
            dgvStockDet.Columns["DestinoMSTKD"].Visible = false;
            dgvStockDet.Columns["IdMSTKD"].HeaderText = "Nº mov.";
            dgvStockDet.Columns["IdArticuloMSTKD"].HeaderText = "Artículo";
            dgvStockDet.Columns["DescripcionART"].HeaderText = "Descripción";
            dgvStockDet.Columns["CantidadMSTKD"].HeaderText = "Cantidad";
            dgvStockDet.ReadOnly = true;
            Cursor.Current = Cursors.Arrow;
        }

        private void btnModifcar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(articulo) || !string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Las opciones 'Modificar' y/o 'Borrar' no están disponibles cuando se usaron criterios de búsqueda",
                    "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dsStockMov.Tables[0].Rows.Count == 0) return;
            if (tipo == "movimientos")
            {
                frmStockMov frm = new frmStockMov(dsStockMov);
                frm.PK = dgvStockMov.CurrentRow.Cells["IdMovMSTK"].Value.ToString();
                frm.ShowDialog();
            }
            else
            {
                frmStockComp frm = new frmStockComp(dsStockMov);
                frm.PK = dgvStockMov.CurrentRow.Cells["IdMovMSTK"].Value.ToString();
                frm.ShowDialog();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(articulo) || !string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Las opciones 'Modificar' y/o 'Borrar' no están disponibles cuando se usaron criterios de búsqueda",
                    "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dsStockMov.Tables[0].Rows.Count == 0) return;
            if (MessageBox.Show("¿Desea borrar este registro y todos los movimientos relacionados?", "Trend Gestión",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                PK = Convert.ToInt32(dgvStockMov.CurrentRow.Cells["IdMovMSTK"].Value.ToString());
                progreso = new frmProgress(PK, "frmStockMov_borrar", "grabar");
                progreso.ShowDialog();
                if (codigoError != null)
                {
                    Close();
                    return;
                }
                viewStockMov.RowFilter = "IdMovMSTK = '" + PK + "'";
                foreach (DataRowView row in viewStockMov)
                {
                    row.Delete();
                }
                tblStockMov.AcceptChanges();
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DataTable tblArticulos = BL.GetDataBLL.Articulos();
            bool imprimePrecios;
            if (MessageBox.Show("¿Imprime el precio en las etiquetas?", "Trend", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                imprimePrecios = true;
            else
                imprimePrecios = false;
            DataView viewStockMovDetalle = new DataView(tblStockMovDetalle);
            string pk = dgvStockMov.CurrentRow.Cells["IdMovMSTK"].Value.ToString();
            viewStockMovDetalle.RowFilter = "IdMovMSTKD = '" + pk + "'";
            viewStockMovDetalle.Sort = "ordenar";

            Cursor.Current = Cursors.WaitCursor;
            DataTable tblEtiquetas = new DataTable();
            tblEtiquetas.Columns.Add("IdArticuloMSTKD", typeof(string));
            tblEtiquetas.Columns.Add("DescripcionART", typeof(string));
            tblEtiquetas.Columns.Add("Precio", typeof(string));
            tblEtiquetas.Columns.Add("IdArticuloMSTKD1", typeof(string));
            tblEtiquetas.Columns.Add("DescripcionART1", typeof(string));
            tblEtiquetas.Columns.Add("Precio1", typeof(string));
            cfilas = tblEtiquetas.Rows;
            int i = 1;
            foreach (DataRowView row in viewStockMovDetalle)
            {
                string precio;
                DataRow[] foundRow = tblArticulos.Select("IdArticuloART = '" + row["IdArticuloMSTKD"].ToString() + "'");
                DataRow filaActual = foundRow[0];
                precio = filaActual["PrecioPublicoART"].ToString();
                int x = Convert.ToInt32(row["CantidadMSTKD"].ToString());
                if (x != 0)
                {
                    if (i > 1 && (i %= 2) == 0)
                    {
                        nuevaFila[3] = "*" + row["IdArticuloMSTKD"].ToString() + "*";
                        nuevaFila[4] = row["DescripcionART"].ToString();
                        nuevaFila[5] = precio;
                        cfilas.Add(nuevaFila);
                        nuevaFila = null;
                        x = x - 1;
                    }
                    for (i = 1; i <= x; i++)
                    {
                        int j;
                        if ((j = i % 2) != 0)
                        {
                            nuevaFila = tblEtiquetas.NewRow();
                            nuevaFila[0] = "*" + row["IdArticuloMSTKD"].ToString() + "*";
                            nuevaFila[1] = row["DescripcionART"].ToString();
                            nuevaFila[2] = precio;
                        }
                        else
                        {
                            nuevaFila[3] = "*" + row["IdArticuloMSTKD"].ToString() + "*";
                            nuevaFila[4] = row["DescripcionART"].ToString();
                            nuevaFila[5] = precio;
                            cfilas.Add(nuevaFila);
                            nuevaFila = null;
                        }
                    }
                }
            }
            if (nuevaFila != null)
            {
                nuevaFila[3] = string.Empty;
                nuevaFila[4] = string.Empty;
                nuevaFila[5] = string.Empty;
                cfilas.Add(nuevaFila);
            }
            EtiquetasRpt frm = new EtiquetasRpt(tblEtiquetas, imprimePrecios);
            frm.Show();
            Cursor.Current = Cursors.Arrow;
        }

        private void dgvStockMov_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }
}
