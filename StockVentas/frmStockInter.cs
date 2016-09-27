using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class frmStockInter : Form
    {
        DataTable tblStock;
        DataTable tblLocales;
        DataTable tblProveedores;
        DataTable tblGeneros;
        DataTable dtCruzada;        
        int proveedor;

        public frmStockInter()
        {
            InitializeComponent();
            cmbGenero.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbProveedor.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
        }

        private void frmStockMovInter_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblLocales = BL.GetDataBLL.Locales();
            DataView viewLocales = new DataView(tblLocales);
            viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <>'1' AND IdLocalLOC <>'2'";
            lstLocales.DataSource = viewLocales;
            lstLocales.DisplayMember = "NombreLOC";
            lstLocales.ValueMember = "IdLocalLOC";
            tblGeneros = BL.GetDataBLL.Generos();
            cmbGenero.ValueMember = "IdGeneroGEN";
            cmbGenero.DisplayMember = "DescripcionGEN";
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDown;
            cmbGenero.DataSource = tblGeneros;
            cmbGenero.SelectedValue = -1;

            AutoCompleteStringCollection generoColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblGeneros.Rows)
            {
                generoColection.Add(Convert.ToString(row["DescripcionGEN"]));
            }
            cmbGenero.AutoCompleteCustomSource = generoColection;
            cmbGenero.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbGenero.AutoCompleteSource = AutoCompleteSource.CustomSource;

            tblProveedores = BL.GetDataBLL.Proveedores();
            cmbProveedor.ValueMember = "IdProveedorPRO";
            cmbProveedor.DisplayMember = "RazonSocialPRO";
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;
            cmbProveedor.DataSource = tblProveedores;
            cmbProveedor.SelectedValue = -1;
            AutoCompleteStringCollection proveedorColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblProveedores.Rows)
            {
                proveedorColection.Add(Convert.ToString(row["RazonSocialPRO"]));
            }
            cmbProveedor.AutoCompleteCustomSource = proveedorColection;
            cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lstLocales.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un local.", "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!BL.UtilDB.ValidarServicioMysql())
            {
                MessageBox.Show("No se pudo conectar con el servidor de base de datos."
                        + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            string whereLocales = null;
            string articulo = "";
            string descripcion = "";
            int activoWeb = 0;
            Cursor.Current = Cursors.WaitCursor;
            string idLocal;
            foreach (DataRowView filaLocal in lstLocales.SelectedItems)
            {
                idLocal = filaLocal.Row[0].ToString();
                whereLocales += "IdLocalSTK LIKE '" + idLocal + "' OR ";
            }
            whereLocales = whereLocales.Substring(0, whereLocales.Length - 4);
            string genero;
            if (!string.IsNullOrEmpty(cmbGenero.Text)) genero = cmbGenero.SelectedValue.ToString();
            else genero = string.Empty;            
            proveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            if (rdArticulo.Checked == true)
            {
                articulo = txtParametros.Text;    
            }
            else
            {
                descripcion = txtParametros.Text;
            }
            if (chkActivo.Checked == true)
            {
                activoWeb = 1;
            }
            DataTable sortedDT;
            DataView dv;
            try
            {
                string origen = "frmStock";
                string accion = "cargar";
                frmProgress newMDIChild = new frmProgress(origen, accion, whereLocales, genero, proveedor, articulo, descripcion, activoWeb);
                newMDIChild.ShowDialog();
                tblStock = frmProgress.dtEstatico.Tables[0];
                // Ordeno por NombreLOC para que aparezcan ordenadas las columnas local en el informe de stock
                dv = tblStock.DefaultView;
                dv.Sort = "NombreLOC asc";
                sortedDT = dv.ToTable();
            }
            catch (NullReferenceException)
            {
                return;
            }
            DataColumn columnaPivot = tblStock.Columns["NombreLOC"];
            DataColumn valorPivot = tblStock.Columns["Cantidad"];
            dtCruzada = BL.Utilitarios.Pivot(sortedDT, columnaPivot, valorPivot);
            dv = dtCruzada.DefaultView;
            dv.Sort = "Descripcion asc";
            if (rdPantalla.Checked == true)
            {
                try
                {
                    frmStockInforme stockInforme = new frmStockInforme(dv);
                    stockInforme.Show();
                }
                catch
                {
                    if (whereLocales == null)
                    {
                        MessageBox.Show("Debe seleccionar un local.", "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron artículos coincidentes", "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }                    
                    return;
                }
            }
            else
            {
                StockRpt rpt = new StockRpt(tblStock);
                rpt.ShowDialog();
            }

            Cursor.Current = Cursors.Arrow;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rdImpresora_Click(object sender, EventArgs e)
        {
            lstLocales.SelectedValue = -1;
            lstLocales.SelectionMode = SelectionMode.One;
        }

        private void rdPantalla_Click(object sender, EventArgs e)
        {
            lstLocales.SelectionMode = SelectionMode.MultiSimple;
        }

    }
}
