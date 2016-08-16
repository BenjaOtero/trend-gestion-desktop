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
    public partial class frmArqueoCajaAdmin : Form
    {
        public frmArqueoCajaAdmin frmInstanciaArqueo;
        public DataSet dt = null;
        DataTable tblArqueo;
        DataTable tblVentas;
        DataTable tblVentasDetalle;
        DataTable tblArticulos;
        DataTable tblTesoreria;
        DataTable tblSumaTesoreria;
        DataTable tblFondoCajaInicial;
        DataTable tblFondoCajaFinal;
        DataTable tblEfectivo;
        DataTable tblTarjeta;
        private int idLocal;
        private string nombreLocal;
        private int idPc;
        private DateTime fecha;

        public frmArqueoCajaAdmin(DataSet dsArqueo, DateTime fecha, int idLocal, string nombreLocal, int idPc)
        {
            InitializeComponent();
            frmInstanciaArqueo = this;
            this.dt = dsArqueo;
            this.fecha = fecha;
            this.idLocal = idLocal;
            this.nombreLocal = nombreLocal;
            this.idPc = idPc;
            Cursor.Current = Cursors.WaitCursor;
            DataGridViewImageColumn imageColumn2 = new DataGridViewImageColumn();
            Image image2 = global::StockVentas.Properties.Resources.document_edit;
            imageColumn2.Image = image2;
            imageColumn2.Name = "Editar";
            dgvTesoreria.Columns.Add(imageColumn2);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            Image image = global::StockVentas.Properties.Resources.delete16;
            imageColumn.Image = image;
            imageColumn.Name = "Borrar";
            dgvTesoreria.Columns.Add(imageColumn);
            dgvTesoreria.CellClick += new DataGridViewCellEventHandler(dgvTesoreria_CellClick);
            DataGridViewImageColumn imageColumn3 = new DataGridViewImageColumn();
            imageColumn3.Image = image2;
            imageColumn3.Name = "Editar";
            dgvVentas.Columns.Add(imageColumn3);
            DataGridViewImageColumn imageColumn4 = new DataGridViewImageColumn();
            imageColumn4.Image = image;
            imageColumn4.Name = "Borrar";
            dgvVentas.Columns.Add(imageColumn4);
            dgvVentas.CellClick += new DataGridViewCellEventHandler(dgvVentas_CellClick);
            tblArticulos = BL.GetDataBLL.Articulos();
            OrganizarTablas();
            CargarDatos();
        }

        private void frmArqueoCaja_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            lblLocal.Text = nombreLocal;
            lblFecha.Text = fecha.ToLongDateString();
            Control.CheckForIllegalCrossThreadCalls = false; // permite asignar un valor a label1.text en un subproceso diferente al principal
        }

        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == dgvVentas.Columns["Editar"].Index)
            {
                string idVenta = dgvVentas.CurrentRow.Cells["IdVentaVEN"].Value.ToString();
                frmVentas ventas = new frmVentas(idVenta, idPc, tblVentas, tblVentasDetalle);
                ventas.FormClosed += editVentas_FormClosed;
                ventas.ShowDialog();
            }
            if (e.ColumnIndex == dgvVentas.Columns["Borrar"].Index)
            {
                if (MessageBox.Show("¿Desea borrar este registro y todos los movimientos relacionados?", "Trend Gestión",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    int PK = Convert.ToInt32(dgvVentas.CurrentRow.Cells["IdVentaVEN"].Value.ToString());
                    frmProgress frm = new frmProgress(PK, "frmArqueoCajaAdmin_borrarVenta", "grabar");
                    frm.FormClosed += progreso_FormClosed;
                    frm.Show();
                }
            }
        }

        private void dgvTesoreria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == dgvTesoreria.Columns["Editar"].Index)
            {
                string PK = dgvTesoreria.CurrentRow.Cells["IdMovTESM"].Value.ToString();
                frmTesoreriaMov frm = new frmTesoreriaMov(idLocal, idPc, PK, tblTesoreria.Copy());
                frm.FormClosed += editTesoreria_FormClosed;
                frm.Show();
            }
            if (e.ColumnIndex == dgvTesoreria.Columns["Borrar"].Index)
            {
                if (MessageBox.Show("¿Desea borrar este registro?", "Trend Gestión",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    int PK = Convert.ToInt32(dgvTesoreria.CurrentRow.Cells["IdMovTESM"].Value.ToString());
                    frmProgress frm = new frmProgress(PK, "frmArqueoCajaAdmin_borrarTesoreria", "grabar");
                    frm.FormClosed += progreso_FormClosed;
                    frm.Show();
                }
            }
        }

        void editVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmVentas formVentas = (frmVentas)sender;
            if (formVentas.Tag != null && formVentas.Tag.ToString() == "ActualizarArqueo")
            {
                DataSet dsVentas = formVentas.dsVentas;
                frmProgress progreso = new frmProgress(dsVentas, "frmVentas", "grabar");
                progreso.FormClosed += progreso_FormClosed;
                progreso.Show();
            }
        }

        void BorrarVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            ActualizarArqueo();
        }

        void editTesoreria_FormClosed(object sender, FormClosedEventArgs e)
        {
            ActualizarArqueo();
        }

        void progreso_FormClosed(object sender, FormClosedEventArgs e)
        {
            ActualizarArqueo();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        void ActualizarArqueo()
        {
            frmProgress frm = new frmProgress(fecha, idLocal, nombreLocal, idPc, "frmArqueoInter", "cargar", frmInstanciaArqueo);
            frm.ShowDialog();
        }

        public void OrganizarTablas()
        {
            tblVentas = dt.Tables[0].Copy();
            tblVentas.TableName = "Ventas";
            tblVentas.AcceptChanges();

            tblVentasDetalle = dt.Tables[1].Copy();
            tblVentasDetalle.TableName = "VentasDetalle";
            tblVentasDetalle.Columns.Remove("IdVentaVEN");
            tblVentasDetalle.Columns.Remove("FechaVEN");
            tblVentasDetalle.Columns.Remove("IdPCVEN");
            tblVentasDetalle.Columns.Remove("IdClienteVEN");
            tblVentasDetalle.Columns.Remove("Forma pago");
            tblVentasDetalle.Columns.Remove("Subtotal");
            tblVentasDetalle.Columns["Descripcion"].ColumnName = "DescripcionDVEN";
            tblVentasDetalle.AcceptChanges();

            tblArqueo = dt.Tables[1].Copy();
            tblArqueo.Columns.Remove("IdClienteVEN");
            tblArqueo.Columns.Remove("IdDVEN");
            tblArqueo.Columns.Remove("IdVentaDVEN");
            tblArqueo.Columns.Remove("IdLocalDVEN");
            tblArqueo.Columns.Remove("PrecioCostoDVEN");
            tblArqueo.Columns.Remove("PrecioMayorDVEN");
            tblArqueo.Columns.Remove("IdFormaPagoDVEN");
            tblArqueo.Columns.Remove("NroCuponDVEN");
            tblArqueo.Columns.Remove("IdEmpleadoDVEN");
            tblArqueo.Columns.Remove("NroFacturaDVEN");
            tblArqueo.Columns.Remove("LiquidadoDVEN");
            tblArqueo.Columns.Remove("EsperaDVEN");
            tblArqueo.Columns.Remove("DevolucionDVEN");
        }

        public void CargarDatos()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dt == null)
            {
                Close();
                return;
            }
            tblFondoCajaInicial = dt.Tables[2];
            tblFondoCajaFinal = dt.Tables[3];
            tblTesoreria = dt.Tables[4];
            tblEfectivo = dt.Tables[5];
            tblTarjeta = dt.Tables[6];
            tblSumaTesoreria = dt.Tables[7];
            tblArqueo.DefaultView.Sort = "FechaVEN DESC";
            dgvVentas.DataSource = tblArqueo;
            dgvVentas.Columns["IdPCVEN"].Visible = false;
            dgvVentas.Columns["Subtotal"].Visible = false;
            dgvVentas.Columns["Subtotal"].Visible = false;
            dgvVentas.Columns["OrdenarDVEN"].Visible = false;
            dgvVentas.Columns["IdVentaVEN"].HeaderText = "Nº venta";
            dgvVentas.Columns["FechaVEN"].HeaderText = "Fecha";
            dgvVentas.Columns["IdArticuloDVEN"].HeaderText = "Artículo";
            dgvVentas.Columns["CantidadDVEN"].HeaderText = "Cantidad";
            dgvVentas.Columns["PrecioPublicoDVEN"].HeaderText = "Precio";
            dgvTesoreria.DataSource = tblTesoreria;
            dgvTesoreria.Columns["FechaTESM"].Visible = false;
            dgvTesoreria.Columns["IdPcTESM"].Visible = false;
            dgvTesoreria.Columns["IdMovTESM"].HeaderText = "Nº mov.";
            dgvTesoreria.Columns["DetalleTESM"].HeaderText = "Detalle";
            dgvTesoreria.Columns["ImporteTESM"].HeaderText = "Importe";
            double dblEfectivo;
            if (String.IsNullOrEmpty(tblEfectivo.Rows[0][0].ToString()))
            {
                dblEfectivo = 0;
            }
            else
            {
                dblEfectivo = Convert.ToDouble(tblEfectivo.Rows[0][0].ToString());
            }
            double dblTarjeta;
            if (String.IsNullOrEmpty(tblTarjeta.Rows[0][0].ToString()))
            {
                dblTarjeta = 0;
            }
            else
            {
                dblTarjeta = Convert.ToDouble(tblTarjeta.Rows[0][0].ToString());
            }
            double dblCajaInicial;
            if (tblFondoCajaInicial.Rows.Count > 0)
            {
                dblCajaInicial = Convert.ToDouble(tblFondoCajaInicial.Rows[0][0].ToString());
            }
            else
            {
                dblCajaInicial = 0;
            }
            double dblTesoreria;
            if (String.IsNullOrEmpty(tblSumaTesoreria.Rows[0][0].ToString()))
            {
                dblTesoreria = 0;
            }
            else
            {
                dblTesoreria = Convert.ToDouble(tblSumaTesoreria.Rows[0][0].ToString());
            }
            double dblEftvoExistente = dblEfectivo + dblCajaInicial + dblTesoreria;
            double dblCajaFinal;
            if (tblFondoCajaFinal.Rows.Count > 0)
            {
                dblCajaFinal = Convert.ToDouble(tblFondoCajaFinal.Rows[0][0].ToString());
            }
            else
            {
                dblCajaFinal = 0;
            }
            double dblEftvoEntregar = dblEftvoExistente - dblCajaFinal;
            double dblVentaTotal = dblEfectivo + dblTarjeta;
            lblEfectivo.Text = "$ " + dblEfectivo.ToString();
            lblTarjeta.Text = "$ " + dblTarjeta.ToString();
            lblCajaInicial.Text = "$ " + dblCajaInicial.ToString();
            lblTesoreria.Text = "$ " + tblSumaTesoreria.Rows[0][0].ToString();
            lblEfectivoExistente.Text = "$ " + dblEftvoExistente.ToString();
            lblCajaFinal.Text = "$ " + dblCajaFinal.ToString();
            lblEfectivoEntregar.Text = "$ " + dblEftvoEntregar.ToString();
            lblTotal.Text = "$ " + dblVentaTotal.ToString();
        }

        private void dgvTesoreria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                string strFecha = fecha.ToShortDateString();
                string detalle = dgvTesoreria.CurrentRow.Cells["DetalleTESM"].Value.ToString();
                string importe = dgvTesoreria.CurrentRow.Cells["ImporteTESM"].Value.ToString();

                frmEmpleadosMov frm = new frmEmpleadosMov(strFecha, detalle, importe);
                frm.Show();
            }
        }

        private void dgvVentas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dgvTesoreria_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

    }

}
