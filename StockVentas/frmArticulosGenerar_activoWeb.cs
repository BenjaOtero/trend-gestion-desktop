using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BL;
using Entities;

namespace StockVentas
{
    public partial class frmArticulosGenerar : Form
    {
        private DataTable tblArticulos;
        private DataTable tblGeneros;
        private DataTable tblArticulosItems;
        private DataTable tblColores;
        private DataTable tblProveedores;
        private DataTable tblAlicuotas;       
        private DataView viewColores;
        private Articulos entidad;
        string NroItem;

        public frmArticulosGenerar()
        {
            InitializeComponent();
            cmbItem.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbProveedor.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbGenero.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            cmbAlicuota.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
            txtDescripcion.Enter += new System.EventHandler(BL.Utilitarios.SelTextoTextBox);
            txtDescripcionWeb.Enter += new System.EventHandler(BL.Utilitarios.SelTextoTextBox);
            txtDesde.Enter += new System.EventHandler(BL.Utilitarios.SelTextoTextBox);
            txtHasta.Enter += new System.EventHandler(BL.Utilitarios.SelTextoTextBox);
            txtCosto.Enter += new System.EventHandler(BL.Utilitarios.SelTextoTextBox);
            txtPublico.Enter += new System.EventHandler(BL.Utilitarios.SelTextoTextBox);
            txtMayor.Enter += new System.EventHandler(BL.Utilitarios.SelTextoTextBox);
            cmbGenero.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            cmbItem.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtDescripcionWeb.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtDesde.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtHasta.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtCosto.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtPublico.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtMayor.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            cmbProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            cmbAlicuota.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);            
            txtDesde.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumeros);
            txtHasta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumeros);
            txtCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtPublico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtMayor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            cmbAlicuota.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
        }

        private void frmArticulosGenerar_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblArticulos = BL.GetDataBLL.Articulos();
            rd1.Checked = true;
            tblColores = BL.GetDataBLL.Colores();
            viewColores = new DataView(tblColores);
            viewColores.RowFilter = "IdColorCOL <> 0";
            lstColores.ValueMember = "IdColorCOL";
            lstColores.DisplayMember = "DescripcionCOL";
            lstColores.DataSource = viewColores;
            lstColores.SelectedValue = -1;
            tblGeneros = BL.GetDataBLL.Generos();
            cmbGenero.ValueMember = "IdGeneroGEN";
            cmbGenero.DisplayMember = "DescripcionGEN";
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDown;
            cmbGenero.DataSource = tblGeneros;
            cmbGenero.SelectedValue = -1;
            tblArticulosItems = BL.GetDataBLL.ArticulosItems();
            cmbItem.ValueMember = "IdItemITE";
            cmbItem.DisplayMember = "DescripcionITE";
            cmbItem.DropDownStyle = ComboBoxStyle.DropDown;
            cmbItem.DataSource = tblArticulosItems;
            cmbItem.SelectedValue = -1;
            tblProveedores = BL.GetDataBLL.Proveedores();
            cmbProveedor.ValueMember = "IdProveedorPRO";
            cmbProveedor.DisplayMember = "RazonSocialPRO";
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;
            cmbProveedor.DataSource = tblProveedores;
            cmbProveedor.SelectedValue = -1;

            tblAlicuotas = BL.GetDataBLL.AlicuotasIva();
            cmbAlicuota.FormatString = "##0%";
            cmbAlicuota.ValueMember = "IdAlicuotaALI";
            cmbAlicuota.DisplayMember = "PorcentajeALI";
            cmbAlicuota.DropDownStyle = ComboBoxStyle.DropDown;
            cmbAlicuota.DataSource = tblAlicuotas;            
            cmbAlicuota.SelectedValue = -1;
            AutoCompleteStringCollection alicuotasColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblAlicuotas.Rows)
            {
                alicuotasColection.Add(Convert.ToString(row["PorcentajeALI"]));
            }
            cmbAlicuota.AutoCompleteCustomSource = alicuotasColection;
            cmbAlicuota.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAlicuota.AutoCompleteSource = AutoCompleteSource.CustomSource;
            chkActivoWeb.Checked = true;
            txtCosto.Text = "0";
            txtPublico.Text = "0";
            txtMayor.Text = "0";
        }

        private void frmArticulosGenerar_Activated(object sender, EventArgs e)
        {
            AutoCompleteStringCollection generosColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblGeneros.Rows)
            {
                generosColection.Add(Convert.ToString(row["DescripcionGEN"]));
            }
            cmbGenero.AutoCompleteCustomSource = generosColection;
            cmbGenero.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbGenero.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection itemsColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblArticulosItems.Rows)
            {
                itemsColection.Add(Convert.ToString(row["DescripcionITE"]));
            }
            cmbItem.AutoCompleteCustomSource = itemsColection;
            cmbItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbItem.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection proveedorColection = new AutoCompleteStringCollection();
            foreach (DataRow row in tblProveedores.Rows)
            {
                proveedorColection.Add(Convert.ToString(row["RazonSocialPRO"]));
            }
            cmbProveedor.AutoCompleteCustomSource = proveedorColection;
            cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string idArticulo = null;
            string codigoColor;
            string codigoTalle;
            string nombreColor;
            int activoWeb;
            int incrementar;
            if (chkActivoWeb.Checked == true)
            {
                activoWeb = 1;
            }
            else
            {
                activoWeb = 0;
            }
            if (validarControles())
            {
                Cursor.Current = Cursors.WaitCursor;
                string codigo = GenerarCodigo();
                DataRow selectedDataRow = ((DataRowView)cmbItem.SelectedItem).Row;
                int idItem = Convert.ToInt32(cmbItem.SelectedValue.ToString());
                int intProveedor = Convert.ToInt32(cmbProveedor.SelectedValue.ToString());
                int intAlicuota = Convert.ToInt32(cmbAlicuota.SelectedValue.ToString());
                string idGenero = cmbGenero.SelectedValue.ToString();
                if (lstColores.SelectedIndices.Count > 0)
                {
                    if (txtDesde.Text != "")
                    {
                        if (rd1.Checked)
                        {
                            incrementar = 1;
                        }
                        else
                        {
                            incrementar = 2;    
                        }
                        foreach(DataRowView filaColor in lstColores.SelectedItems)
                        {
                            for (int i = Convert.ToInt32(txtDesde.Text); i <= Convert.ToInt32(txtHasta.Text); i += incrementar)
                            {
                                codigoColor = filaColor.Row[0].ToString();
                                nombreColor = filaColor.Row[1].ToString();
                                if (codigoColor.Length == 1)
                                {
                                    codigoColor = "0" + codigoColor;
                                }
                                codigoTalle = i.ToString();
                                if (codigoTalle.Length == 1)
                                {
                                    codigoTalle = "0" + codigoTalle;
                                }
                                idArticulo = codigo + codigoColor + codigoTalle;
                                string strItem = selectedDataRow["DescripcionITE"].ToString();
                                string descripcion = strItem + " " + txtDescripcion.Text + " " + nombreColor + " T" + codigoTalle;
                                int intCodigoColor = Convert.ToInt32(codigoColor);
                                entidad = new Articulos();
                                entidad.IdArticulo = idArticulo;
                                entidad.IdItem = idItem;
                                entidad.IdGenero = idGenero;
                                entidad.IdColor = intCodigoColor;
                                entidad.IdAlicuota = intAlicuota;
                                entidad.Talle = codigoTalle;
                                entidad.IdProveedor = intProveedor;
                                entidad.Descripcion = descripcion;
                                entidad.DescripcionWeb = strItem + " " + txtDescripcionWeb.Text;                               
                                entidad.PrecioCosto = Convert.ToDecimal(txtCosto.Text);
                                entidad.PrecioPublico = Convert.ToDecimal(txtPublico.Text);
                                entidad.PrecioMayor = Convert.ToDecimal(txtMayor.Text);
                                entidad.Fecha = DateTime.Now;
                                entidad.Imagen = "";
                                entidad.ImagenBack = "";
                                entidad.ImagenColor = "";
                                entidad.ActivoWeb = activoWeb;
                                entidad.NuevoWeb = 1;
                                entidad.Proveedor = cmbProveedor.Text;
                                BL.ArticulosBLL.InsertarDT(tblArticulos, entidad);
                            }                            
                        }
                    }
                    else
                    {
                        foreach (DataRowView filaColor in lstColores.SelectedItems)
                        {
                            codigoColor = filaColor.Row[0].ToString();
                            nombreColor = filaColor.Row[1].ToString();
                            if (codigoColor.Length == 1)
                            {
                                codigoColor = "0" + codigoColor;
                            }
                            idArticulo = codigo + codigoColor + "00";
                            string strItem = selectedDataRow["DescripcionITE"].ToString();
                            string descripcion = strItem + " " + txtDescripcion.Text + " " + nombreColor;
                            int intCodigoColor = Convert.ToInt32(codigoColor);
                            codigoTalle = string.Empty;
                            entidad = new Articulos();
                            entidad.IdArticulo = idArticulo;
                            entidad.IdItem = idItem;
                            entidad.IdGenero = idGenero;
                            entidad.IdColor = intCodigoColor;
                            entidad.IdAlicuota = intAlicuota;
                            entidad.Talle = codigoTalle;
                            entidad.IdProveedor = intProveedor;
                            entidad.Descripcion = descripcion;
                            entidad.DescripcionWeb = strItem + " " + txtDescripcionWeb.Text;
                            entidad.PrecioCosto = Convert.ToDecimal(txtCosto.Text);
                            entidad.PrecioPublico = Convert.ToDecimal(txtPublico.Text);
                            entidad.PrecioMayor = Convert.ToDecimal(txtMayor.Text);
                            entidad.Fecha = DateTime.Now;
                            entidad.Imagen = "";
                            entidad.ImagenBack = "";
                            entidad.ImagenColor = "";
                            entidad.ActivoWeb = activoWeb;
                            entidad.NuevoWeb = 1;
                            entidad.Proveedor = cmbProveedor.Text;
                            BL.ArticulosBLL.InsertarDT(tblArticulos, entidad);
                        }
                    }
                }
                else
                {
                    if (txtDesde.Text != "")
                    {
                        if (rd1.Checked)
                        {
                            incrementar = 1;
                        }
                        else 
                        {
                            incrementar = 2;
                        }
                        for (int i = Convert.ToInt32(txtDesde.Text); i <= Convert.ToInt32(txtHasta.Text); i += incrementar)
                        {
                            codigoTalle = i.ToString();
                            if (codigoTalle.Length == 1)
                            {
                                codigoTalle = "0" + codigoTalle;
                            }
                            idArticulo = codigo + "00" + codigoTalle;
                            string strItem = selectedDataRow["DescripcionITE"].ToString();
                            string descripcion = strItem + " " + txtDescripcion.Text +  " T" + codigoTalle;
                            int intCodigoColor = 0;
                            entidad = new Articulos();
                            entidad.IdArticulo = idArticulo;
                            entidad.IdItem = idItem;
                            entidad.IdGenero = idGenero;
                            entidad.IdColor = intCodigoColor;
                            entidad.IdAlicuota = intAlicuota;
                            entidad.Talle = codigoTalle;
                            entidad.IdProveedor = intProveedor;
                            entidad.Descripcion = descripcion;
                            entidad.DescripcionWeb = strItem + " " + txtDescripcionWeb.Text;
                            entidad.PrecioCosto = Convert.ToDecimal(txtCosto.Text);
                            entidad.PrecioPublico = Convert.ToDecimal(txtPublico.Text);
                            entidad.PrecioMayor = Convert.ToDecimal(txtMayor.Text);
                            entidad.Fecha = DateTime.Now;
                            entidad.Imagen = "";
                            entidad.ImagenBack = "";
                            entidad.ImagenColor = "";
                            entidad.ActivoWeb = activoWeb;
                            entidad.NuevoWeb = 1;
                            entidad.Proveedor = cmbProveedor.Text;
                            BL.ArticulosBLL.InsertarDT(tblArticulos, entidad);
                        }
                    }
                    else
                    {
                        idArticulo = codigo + "00" + "00";
                        string strItem = selectedDataRow["DescripcionITE"].ToString();
                        string descripcion = strItem + " " + txtDescripcion.Text;
                        int intCodigoColor = 0;
                        codigoTalle = string.Empty;
                        entidad = new Articulos();
                        entidad.IdArticulo = idArticulo;
                        entidad.IdItem = idItem;
                        entidad.IdGenero = idGenero;
                        entidad.IdColor = intCodigoColor;
                        entidad.IdAlicuota = intAlicuota;
                        entidad.Talle = codigoTalle;
                        entidad.IdProveedor = intProveedor;
                        entidad.Descripcion = descripcion;
                        entidad.DescripcionWeb = strItem + " " + txtDescripcionWeb.Text;
                        entidad.PrecioCosto = Convert.ToDecimal(txtCosto.Text);
                        entidad.PrecioPublico = Convert.ToDecimal(txtPublico.Text);
                        entidad.PrecioMayor = Convert.ToDecimal(txtMayor.Text);
                        entidad.Fecha = DateTime.Now;
                        entidad.Imagen = "";
                        entidad.ImagenBack = "";
                        entidad.ImagenColor = "";
                        entidad.ActivoWeb = activoWeb;
                        entidad.NuevoWeb = 1;
                        entidad.Proveedor = cmbProveedor.Text;
                        BL.ArticulosBLL.InsertarDT(tblArticulos, entidad);
                    }
                }                
                MessageBox.Show(idArticulo);
         //       cmbGenero.SelectedValue = -1;
                lstColores.SelectedIndex = -1;
                cmbItem.SelectedValue = -1;
                txtDescripcion.Text = "";
                txtDescripcionWeb.Text = "";
                txtDesde.Text = "";
                txtHasta.Text = "";
                txtCosto.Text = "0";
                txtPublico.Text = "0";
                txtMayor.Text = "0";
                cmbItem.Focus();
                Cursor.Current = Cursors.Arrow;
            }            
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            frmArticulosItems frm = new frmArticulosItems();
            frm.Show();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            frmColores frm = new frmColores();
            frm.Show();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            frmProveedores frm = new frmProveedores();
            frm.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmArticulosGenerar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tblArticulos.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblArticulos, "frmArticulosGenerar", "grabar");
                progreso.ShowDialog();          
            }
        }

        private bool validarControles() 
        {
            if (String.IsNullOrEmpty(cmbGenero.Text))
            {
                MessageBox.Show("Debe seleccionar un género.", "Trend Gestión");
                cmbGenero.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(cmbItem.Text))
            {
                MessageBox.Show("Debe seleccionar un item.", "Trend Gestión");
                cmbItem.Focus();
                return false;
            }
            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Debe escribir una descripción.","Trend Gestión");
                txtDescripcion.Focus();
                return false;
            }
            if (txtDesde.Text != "" && txtHasta.Text == "")
            {
                MessageBox.Show("Debe indicar 'HASTA' que talle quiere generar.", "Trend Gestión");
                txtHasta.Focus();
                return false;
            }
            if (txtDesde.Text == "" && txtHasta.Text != "")
            {
                MessageBox.Show("Debe indicar 'DESDE' que talle quiere generar.", "Trend Gestión");
                txtDesde.Focus();
                return false;
            }
            if (txtDesde.Text != "" && txtHasta.Text != "")
            {
                var desde = Convert.ToInt32(txtDesde.Text);
                var hasta = Convert.ToInt32(txtHasta.Text);
                if (desde > hasta)
                {
                    MessageBox.Show("'Desde talle' debe ser menor o igual que 'hasta talle'.", "Trend Gestión");
                    txtDesde.Focus();
                    return false;

                }
            }
            if (String.IsNullOrEmpty(txtCosto.Text))
            {
                MessageBox.Show("Debe indicar un precio de costo.", "Trend Gestión");
                txtCosto.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtPublico.Text))
            {
                MessageBox.Show("Debe indicar un precio al público.", "Trend Gestión");
                txtPublico.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtMayor.Text))
            {
                MessageBox.Show("Debe indicar un precio al por mayor.", "Trend Gestión");
                txtMayor.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(cmbAlicuota.Text))
            {
                MessageBox.Show("Debe seleccionar una alícuota de iva.", "Trend Gestión");
                cmbAlicuota.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(cmbProveedor.Text))
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Trend Gestión");
                cmbProveedor.Focus();
                return false;
            }
            return true;
        }

        private string GenerarCodigo()
        {            
            string nroItemDescripcion;
            int nroItemDescripcion2;
            NroItem = cmbItem.SelectedValue.ToString();
            if (NroItem.Length == 1)
            {
                NroItem = "00" + NroItem;
            }
            else if (NroItem.Length == 2)
            {
                NroItem = "0" + NroItem;
            }
            nroItemDescripcion = NroItem + "001"; //001001
            bool existe = true;
            while (existe == true)
            {
                DataRow[] foundRows;
                foundRows = tblArticulos.Select("IdArticuloART Like '" + nroItemDescripcion + "%'");
                if (foundRows.Count() == 0)
                {
                    existe = false;
                }
                else
                {
                    nroItemDescripcion2 = Convert.ToInt32(nroItemDescripcion);
                    nroItemDescripcion2++;
                    nroItemDescripcion = Convert.ToString(nroItemDescripcion2);
                    if (nroItemDescripcion.Length == 4)
                    {
                        nroItemDescripcion = "00" + nroItemDescripcion;
                    }
                    else if (nroItemDescripcion.Length == 5)
                    {
                        nroItemDescripcion = "0" + nroItemDescripcion;
                    }
                }
            }
            return nroItemDescripcion;
        }

    }
}
