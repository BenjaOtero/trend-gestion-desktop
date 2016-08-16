using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;
using BL;
using StockVentas.com.karminna;
using System.Net;


namespace StockVentas
{
    public partial class frmProgress : Form
    {
        public frmArqueoCajaAdmin frmInstanciaArqueo = null;
        public frmStockComp instaciaStockComp = null;
        private frmStockMov instanciaStockMov = null;
        private frmArticulosAgrupar instanciaArticulosAgrupar = null;
        private DataSet dtStock = null;
        private DataSet dsStockMov = null;
        private DataSet dsVentas = null;
        private DataSet dsTesoreriaMov = null;
        private DataSet dsFondoCaja = null;
        public DataSet dsArqueo = null;     
        public static DataSet dtEstatico = null;
        public static DataSet dsStockMovCons = null;
        public static DataSet dsVentasPesosCons = null;
        private DataSet dt = null;
        private DataTable tabla;
        private DataTable tblArticulos;
        private DataTable tblArticulosBorrar;
        private DataTable tblStock;
        private DataTable tblArticulosStock;
        public static DataTable tblEstatica = null;
        public DataView viewStockMov;
        public DataRowView rowView;
        public ComboBox cmbOrigen;
        public ComboBox cmbDestino;
        private string origen = null;
        private string accion = null;
        private string locales = null;
        private string formaPago = null;
        private string parametros;
        private string genero;
        private int proveedor;
        private string articulo = null;
        private string descripcion = null;
        private int? codigoError = null;
        private int idLocal;
        private int idEmpleado;
        private int liquidado;
        private string nombreLocal;
        private int idPc;
        private int PK;
        private DateTime fecha;
        private string strFecha;
        private string strFechaDesde;
        private string strFechaHasta;
        private string opcMov;
        private string tipo;
        private int forma;
        public bool actualizarArticulos;
        string where;
        int activoWeb;
        BindingSource bindingSource1;
        string imagenesBorrar;
        string idAlicuota;
        string oldIdAlicuota;

        private const int CP_NOCLOSE_BUTTON = 0x200;  //junto con protected override CreateParams inhabilitan el boton cerrar de frmProgress
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 

        public frmProgress()
        {
            InitializeComponent();
        }

        public frmProgress(string origen, string accion): this()
        {
            this.origen = origen;
            this.accion = accion;
        }

        // Constructor para frmEmpleadosMovConsInter
        public frmProgress(string strFechaDesde, string strFechaHasta, int idEmpleado, int liquidado, string origen, string accion)
            : this()
        {
            this.strFechaDesde = strFechaDesde;
            this.strFechaHasta = strFechaHasta;
            this.idEmpleado = idEmpleado;
            this.liquidado = liquidado;
            this.origen = origen;
            this.accion = accion;
        }

        //Constructor frmPedido
        public frmProgress(string fecha, string origen, string accion, string genero)
            : this()
        {
            this.origen = origen;
            this.accion = accion;
            this.strFecha = fecha;
            this.genero = genero;
        }

        //Constructor frmStockInter
        public frmProgress(string origen, string accion, string locales, string genero, int proveedor, string articulo, string descripcion, int activo)
            : this()
        {
            this.origen = origen;
            this.accion = accion;
            this.locales = locales;
            this.genero = genero;
            this.proveedor = proveedor;
            this.articulo = articulo;
            this.descripcion = descripcion;
            this.activoWeb = activo;
        }

        public frmProgress(DataTable tabla, string origen, string accion): this()
        {
            this.tabla = tabla;
            this.origen = origen;
            this.accion = accion;
        }

        public frmProgress(DataTable tabla, string origen, string accion, string imagenesBorrar)
            : this()
        {
            this.tabla = tabla;
            this.origen = origen;
            this.accion = accion;
            this.imagenesBorrar = imagenesBorrar;
        }

        public frmProgress(DataTable tabla, string origen, string accion, string idAlicuota, string oldIdAlicuota)
            : this()
        {
            this.tabla = tabla;
            this.origen = origen;
            this.accion = accion;
            this.idAlicuota = idAlicuota;
            this.oldIdAlicuota = oldIdAlicuota;
        }
        //Constructor frmArticulosAgrupar
        public frmProgress(DataTable tblArticulos, DataTable tblStock, string origen, string accion, frmArticulosAgrupar instanciaArticulosAgrupar)
            : this()
        {
            this.tblArticulos = tblArticulos;
            this.tblStock = tblStock;
            this.origen = origen;
            this.accion = accion;
            this.instanciaArticulosAgrupar = instanciaArticulosAgrupar;
        }

        // Constructor para frmStockEntradas, frmVentas, frmTesoreriaMov
        public frmProgress(DataSet dt, string origen, string accion)
            : this()
        {
            this.origen = origen;
            this.accion = accion;
            if (dt.DataSetName == "dsVentas")
            {
                this.dsVentas = dt;
            }
            else if (dt.DataSetName == "dsStockMov") // Constructor para frmStockEntradas
            {
                this.dsStockMov = dt;
            }
            else if (dt.DataSetName == "dsTesoreriaMov")
            {
                this.dsTesoreriaMov = dt;
            }
            else if (dt.DataSetName == "dsFondoCaja")
            {
                this.dsFondoCaja = dt;
                this.tabla = dt.Tables[0];
            }
        }

        // Constructor para frmStockMov
        public frmProgress(DataSet dt, string origen, string accion, frmStockMov instanciaStockMov)
            : this()
        {
            if (dt.DataSetName == "dsStockMov")
            {
                this.dsStockMov = dt;
                this.origen = origen;
                this.accion = accion;
                this.instanciaStockMov = instanciaStockMov;
            }
        }

        // Constructor para frmStockComp
        public frmProgress(DataSet dt, string origen, string accion, frmStockComp instaciaStockComp)
            : this()
        {
            if (dt.DataSetName == "dsStockMov")
            {
                this.dsStockMov = dt;
                this.origen = origen;
                this.accion = accion;
                this.instaciaStockComp = instaciaStockComp;
            }
        }

        public frmProgress(DataSet dt, string origen, string accion, string where): this()
        {
            this.dt = dt;
            this.origen = origen;
            this.accion = accion;
            this.where = where;
        }

        public frmProgress(DataSet dt, DataSet dtStock, string origen, string accion): this()
        {
            this.dt = dt;
            this.dtStock = dtStock;
            this.origen = origen;
            this.accion = accion;
        }

        // Constructor para frmStockMovInforme
        public frmProgress(string strFechaDesde, string strFechaHasta, int idLocal, string tipo, string opcMov, string origen, string accion,
            string articulo, string descripcion): this()
        {
            this.strFechaDesde = strFechaDesde;
            this.strFechaHasta = strFechaHasta;
            this.idLocal = idLocal;
            this.tipo = tipo;
            this.opcMov = opcMov;
            this.origen = origen;
            this.accion = accion;
            this.articulo = articulo;
            this.descripcion = descripcion;
        }

        // Constructor para frmStockCompPesos
        public frmProgress(string strFechaDesde, string strFechaHasta, int idLocal, string origen, string accion)
            : this()
        {
            this.strFechaDesde = strFechaDesde;
            this.strFechaHasta = strFechaHasta;
            this.idLocal = idLocal;
            this.origen = origen;
            this.accion = accion;
        }

        // Constructor para frmVentasPesosCons
        public frmProgress(int forma, string strFechaDesde, string strFechaHasta, string locales, string origen, string accion, string genero)
            : this()
        {
            this.forma = forma;
            this.strFechaDesde = strFechaDesde;
            this.strFechaHasta = strFechaHasta;
            this.locales = locales;
            this.origen = origen;
            this.accion = accion;
            this.genero = genero;
        }

        // Constructor para frmVentasPesosDiarias
        public frmProgress(string strFechaDesde, string strFechaHasta, int local, string forma, string origen, string accion)
            : this()
        {
            this.strFechaDesde = strFechaDesde;
            this.strFechaHasta = strFechaHasta;
            this.idLocal = local;
            this.formaPago = forma;
            this.origen = origen;
            this.accion = accion;
        }

        // Constructor para frmVentasDetalleInter
        public frmProgress(int forma, string strFechaDesde, string strFechaHasta, int idLocal, string origen, string accion, string parametros)
            : this()
        {
            this.forma = forma;
            this.strFechaDesde = strFechaDesde;
            this.strFechaHasta = strFechaHasta;
            this.idLocal = idLocal;
            this.origen = origen;
            this.accion = accion;
            this.parametros = parametros;
        }

        // Constructor para frmArqueoCajaAdmin
        public frmProgress(DateTime fecha, int idLocal, string nombreLocal, int idPc, string origen, string accion)
            : this()
        {
            this.fecha = fecha;
            strFechaDesde = fecha.ToString("yyyy-MM-dd 00:00:00"); //fecha string para mysql
            strFechaHasta = fecha.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
            this.idLocal = idLocal;
            this.nombreLocal = nombreLocal;
            this.idPc = idPc;
            this.origen = origen;
            this.accion = accion;
        }

        // Constructor para frmArqueoCajaAdmin
        public frmProgress(DateTime fecha, int idLocal, string nombreLocal, int idPc, string origen, string accion, 
            frmArqueoCajaAdmin frmInstanciaArqueo): this()
        {
            this.fecha = fecha;
            strFechaDesde = fecha.ToString("yyyy-MM-dd 00:00:00"); //fecha string para mysql
            strFechaHasta = fecha.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
            this.idLocal = idLocal;
            this.nombreLocal = nombreLocal;
            this.idPc = idPc;
            this.origen = origen;
            this.accion = accion;
            this.frmInstanciaArqueo = frmInstanciaArqueo;
        }

        // Constructor para frmArqueoCajaAdmin (borrar ventas y movimientos de tesoreria), frmStockMovInforme (borrar movimientos), frmFondoCajaCons
        public frmProgress(int PK, string origen, string accion)
            : this()
        {
            this.PK = PK;
            this.origen = origen;
            this.accion = accion;
        }

        // frmFondoCajaCons (borrar registro)
        public frmProgress(int PK, string origen, string accion, ref BindingSource bindingSource1)
            : this()
        {
            this.PK = PK;
            this.origen = origen;
            this.accion = accion;
            this.bindingSource1 = bindingSource1;
        } 

        private void frmProgress_Shown(object sender, EventArgs e)
        {
            this.CenterToScreen();
            if (accion == "cargar")
            {
                if (origen == "VentasHistoricasUpdate")
                {
                    label1.Text = "Actualizando datos...";
                    label1.Left = 108;
                }
                else if (origen == "backup")
                {
                    label1.Text = "Copiando datos...";
                    label1.Left = 108;
                }
                else
                {
                    label1.Text = "Cargando datos...";
                    label1.Left = 108;
                }
            }
            else
            { 
                if (origen == "ExportarDatos")
                {
                    label1.Text = "Exportando datos...";
                    label1.Left = 108;
                }
            }
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (accion == "cargar")
                {
                    switch (origen)
                    {
                        case "backup":
                            if (BL.Utilitarios.HayInternet())
                            {
                                Process process = new Process();
                                process.StartInfo.FileName = "c:\\Windows\\Temp\\backup.bat";
                                process.StartInfo.CreateNoWindow = false;
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.Start();
                                process.WaitForExit();
                            }
                            else
                            {
                                this.Invoke((Action)delegate
                                {
                                    MessageBox.Show("Verifique la conexión a internet. No se realizo al copia de seguridad.", "Trend",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                });
                            }
                            break;
                        case "frmArqueoInter":
                            dsArqueo = BL.VentasBLL.CrearDatasetArqueo(strFechaDesde, strFechaHasta, idPc);
                            break;
                        case "frmArticulosAgrupar":
                            tabla = BL.StockBLL.GetStock();
                            tblArticulosStock = BL.ArticulosBLL.GetArticulosStock();
                            break;
                        case "frmArticulosBorradoMasivo":
                            dtEstatico = BL.PedidosBLL.CrearDataset(strFecha, genero);
                            DataTable tblArticulos = BL.GetDataBLL.Articulos();
                            DataTable tblPedidos = dtEstatico.Tables[0];
                            DataTable tblArticuloBorrar = dtEstatico.Tables[1];
                            foreach (DataRow row in tblArticuloBorrar.Rows)
                            {
                                string articulos = row["IdArticulo"].ToString();
                                DataRow[] rowfound = tblPedidos.Select("Articulo LIKE '" + articulos + "*'");
                                DataRow[] rowfoundZero = tblPedidos.Select("[Stock] = 0 AND Articulo LIKE '" + articulos + "*' AND [Venta] is null");
                                if (rowfoundZero.Count() == rowfound.Count())
                                {
                                    DataRow[] rowArticulo = tblArticulos.Select("IdArticuloART LIKE '" + articulos + "*'");
                                    foreach (DataRow rowBorrar in rowArticulo)
                                    {
                                        rowBorrar.Delete();                                    
                                    }
                                }
                            }
                            BL.ArticulosBLL.GrabarDB(tblArticulos);
                            break;
                        case "frmArticulosBorrar":
                            tblArticulosBorrar = BL.ArticulosBLL.GetArticulosStock();
                            break;
                        case "frmFondoCajaCons":
                            dt = BL.FondoCajaBLL.CrearDatasetCons();
                            break;
                        case "frmEmpleadosMovConsInter":
                            tblEstatica = BL.EmpleadosMovBLL.EmpleadosMovCons(strFechaDesde, strFechaHasta, idEmpleado, liquidado);
                            break;
                        case "frmPedido":
                            dtEstatico = BL.PedidosBLL.CrearDataset(strFecha, genero);
                            DataTable tblPedido = dtEstatico.Tables[0];
                            DataTable tblBorrar = dtEstatico.Tables[1];
                            foreach (DataRow row in tblBorrar.Rows)
                            {
                                string articulos = row["IdArticulo"].ToString();
                                DataRow[] rowfound = tblPedido.Select("Articulo LIKE '" + articulos + "*'");
                                DataRow[] rowfoundZero = tblPedido.Select("[Stock] = 0 AND Articulo LIKE '" + articulos + "*' AND [Venta] is null");
                                if (rowfoundZero.Count() == rowfound.Count())
                                {
                                    foreach (DataRow rowBorrar in rowfound)
                                    {
                                      rowBorrar.Delete();                                    
                                    }
                                }
                            }
                            break;
                        case "frmStockCompPesos":
                            tblEstatica = BL.StockMovBLL.GetCompensacionesPesos(strFechaDesde, strFechaHasta, idLocal);
                            break;
                        case "frmStockMovInforme":
                            dsStockMovCons = BL.StockMovBLL.CrearDatasetCons(strFechaDesde, strFechaHasta, idLocal, tipo, opcMov, articulo, descripcion);
                            break;
                        case "frmStock":
                            dtEstatico = BL.StockBLL.CrearDataset(locales, genero, proveedor, articulo, descripcion, activoWeb);
                            break;
                        case "frmVentasDetalleInter":
                            tblEstatica = BL.VentasBLL.GetVentasDetalle(forma, strFechaDesde, strFechaHasta, idLocal, parametros);
                            break;
                        case "frmVentasPesosCons":
                            dsVentasPesosCons = BL.VentasBLL.CrearDatasetVentasPesos(forma, strFechaDesde, strFechaHasta, locales, genero);
                            break;
                        case "frmVentasPesosInter_diarias":
                            tblEstatica = BL.VentasBLL.GetVentasPesosDiarias(strFechaDesde, strFechaHasta, idLocal, formaPago);
                            break;
                    }
                }
                else //grabar en base de datos
                {
                    switch (origen)
                    {
                        case "frmAlicuotasIva":
                            BL.AlicuotasIvaBLL.GrabarDB(tabla, idAlicuota, oldIdAlicuota);
                            break;
                        case "frmArqueoCajaAdmin_borrarTesoreria":
                            BL.TesoreriaMovimientosBLL.BorrarByPK(PK);
                            break;
                        case "frmArqueoCajaAdmin_borrarVenta":
                            BL.VentasBLL.BorrarByPK(PK);
                            break;
                        case "frmArticulos":
                            BL.ArticulosBLL.GrabarDB(tabla);
                            break;
                        case "frmArticulosAgrupar":
                            BL.TransaccionesBLL.GrabarArticulosAgrupar(tblStock, tblArticulos, ref codigoError);
                            break;
                        case "frmArticulosBorrar":
                            BL.ArticulosBLL.GrabarDB(tabla);
                            TratarImagenesService tis = new TratarImagenesService();
                            tis.BorrarImagenes(imagenesBorrar);
                            break;
                        case "frmArticulosGenerar":
                            BL.ArticulosBLL.GrabarDB(tabla);
                            break;
                        case "frmArticulosItems":
                            BL.ArticulosItemsBLL.GrabarDB(tabla);
                            break;
                        case "frmClientes":
                            BL.ClientesBLL.GrabarDB(tabla);
                            break;
                        case "frmColores":
                            BL.ColoresBLL.GrabarDB(tabla);
                            break;
                        case "frmCondicionIva":
                            BL.CondicionIvaBLL.GrabarDB(tabla);
                            break;
                        case "frmEmpleados":
                            BL.EmpleadosBLL.GrabarDB(tabla);
                            break;
                        case "frmEmpleadosMov":
                            BL.EmpleadosMovBLL.GrabarDB(tabla);
                            break;
                        case "frmEmpleadosMovTipo":
                            BL.EmpleadosMovTiposBLL.GrabarDB(tabla);
                            break;
                        case "ExportarDatos":
                            BL.DatosBLL.ExportarDatos();
                            break;
                        case "ImportarDatos":
                            BL.DatosBLL.GetDataPOS();
                            break;
                        case "frmFondoCaja":
                            BL.FondoCajaBLL.GrabarDB(tabla);
                            break;
                        case "frmFondoCajaCons":
                            BL.FondoCajaBLL.BorrarByPK(PK);
                            bindingSource1.RemoveCurrent();
                            bindingSource1.EndEdit();
                            break;
                        case "frmFormasPago":
                            BL.FormasPagoBLL.GrabarDB(tabla);
                            break;
                        case "frmGeneros":
                            BL.GenerosBLL.GrabarDB(tabla);
                            break;
                        case "frmLocales":
                            BL.LocalesBLL.GrabarDB(tabla);
                            break;
                        case "frmProveedores":
                            BL.ProveedoresBLL.GrabarDB(tabla);
                            break;
                        case "frmRazonSocial":
                            BL.RazonSocialBLL.GrabarDB(tabla);
                            break;
                        case "frmStockMov":
                            BL.TransaccionesBLL.GrabarStockMovimientos(dsStockMov);
                            break;
                        case "frmStockMov_borrar":
                            BL.StockMovBLL.BorrarByPK(PK);
                            break;
                        case "frmStockEntradas":
                            BL.TransaccionesBLL.GrabarStockMovimientos(dsStockMov);
                            break;
                        case "frmVentas":
                            BL.TransaccionesBLL.GrabarVentas(dsVentas, ref codigoError);
                            break;
                        case "frmTesoreriaMov":
                            BL.TesoreriaMovimientosBLL.GrabarDB(dsTesoreriaMov);
                            break;
                    }
                }
            }
            catch (MySqlException ex)
            {
                codigoError = ex.Number;
            }
            catch (TimeoutException)
            {
                codigoError = 8953; // El número 8953 lo asigné al azar
            }
            catch (NullReferenceException)
            {
                codigoError = 8954; // El número 8954 lo asigné al azar
            }
            catch (WebException)
            {
                this.Invoke((Action)delegate
                {
                    this.Visible = false;
                    MessageBox.Show("No se pudo establecer conexión con el servidor remoto. No se actualizaron los datos.", "Trend Gestión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            catch (Exception)
            {
                codigoError = 8955; // El número 8955 lo asigné al azar
            }
            finally
            {
                if (accion == "grabar") DeshacerCambios();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (codigoError != null && origen == "frmStockMov")
            {
                if (instaciaStockComp != null) instaciaStockComp.GrabacionCorrecta = false;
                if (instanciaStockMov != null) instanciaStockMov.GrabacionCorrecta = false;               
            }
            if (codigoError != null && origen == "frmArticulosAgrupar")
            {
                if (instanciaArticulosAgrupar != null) instanciaArticulosAgrupar.grabacionCorrecta = false;             
            }
            
            if (codigoError == null)
            {
                if (accion == "cargar")
                {
                    switch (origen)
                    {
                         case "backup":
                            this.Close();
                            if (File.Exists("c:\\Windows\\Temp\\backup.bat"))
                            {
                                File.Delete("c:\\Windows\\Temp\\backup.bat");
                            }  
                            break;
                        case "frmArqueoInter":
                            if (frmInstanciaArqueo == null) // Estoy abriendo frmArqueoCajaAdmin desde el menú
                            {
                                this.Close();
                                frmArqueoCajaAdmin frmArqueo = new frmArqueoCajaAdmin(dsArqueo, fecha, idLocal, nombreLocal, idPc);
                                frmArqueo.ShowDialog();
                            }
                            else // Estoy actualizando frmArqueoCajaAdmin despues de editar una venta
                            {
                                frmInstanciaArqueo.dt = dsArqueo;
                                frmInstanciaArqueo.OrganizarTablas();
                                frmInstanciaArqueo.CargarDatos();
                            }
                            break;
                        case "frmArticulosAgrupar":
                            this.Close();
                            frmArticulosAgrupar frm = new frmArticulosAgrupar(tabla, tblArticulosStock);
                            frm.ShowDialog();
                            break;
                        case "frmArticulosBorrar":
                            this.Close();
                            frmArticulosBorrar articulosBorrar = new frmArticulosBorrar(tblArticulosBorrar, tabla);
                            articulosBorrar.ShowDialog();
                            break;
                        case "frmFondoCajaCons":
                            frmFondoCajaCons fondo = new frmFondoCajaCons(dt);
                            fondo.Show();
                            break;
                        case "VentasHistoricasUpdate":    
                            DataTable tbl = dt.Tables[0];
                            string registros = tbl.Rows[0][0].ToString();
                            MessageBox.Show("Se actualizaron " + registros + " registros", "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Cursor.Current = Cursors.Arrow;
                            break;
                    }
                }
                else // grabar
                {
                    switch (origen)
                    {
                        case "ExportarDatos":
                            BL.RazonSocialBLL.ActualizarDatos();
                            break;
                        case "frmStockMov":
                            break;
                        case "frmVentas":
                            break;
                    }
                }
                this.Close();
            }
            else if (codigoError == 1042) //Unable to connect to any of the specified MySQL hosts.
            {
                this.Visible = false;
                if (accion == "grabar")
                {
                    MessageBox.Show("No se pudo establecer la conexión con el servidor (verifique la conexión a internet). No se guardaron los cambios.",
                        "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("No se pudo establecer la conexión con el servidor (verifique la conexión a internet).",
                            "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else if (codigoError == 1062) //Clave principal duplicada
            {
                this.Visible = false;
                switch (origen)
                {
                    case "frmFondoCaja":
                        MessageBox.Show("Ya existe un fondo de caja para dicha fecha. No se guardaron los cambios.",
                        "Trend Gestion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                this.Close();
            }
            else if (codigoError == 0) // Procedure or function cannot be found in database 
            {
                this.Visible = false;
                if (accion == "grabar")
                {
                    MessageBox.Show("Ocurrió un error al ejecutar la consulta MySQL (consulte al administrador del sistema). No se guardaron los cambios.",
                        "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al ejecutar la consulta MySQL (consulte al administrador del sistema).",
                        "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else if (codigoError == 8953) //TimeOutException
            {
                this.Visible = false;
                if (accion == "grabar")
                {
                    MessageBox.Show("Se excedió el tiempo de espera para la consulta al servidor. Intente nuevamente. No se guardaron los cambios.",
                        "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Se excedió el tiempo de espera para la consulta al servidor. Intente nuevamente.",
                        "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                this.Visible = false;
                MessageBox.Show("Se produjo un error inesperado. Consulte al administrador del sistema",
                "Trend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DeshacerCambios()
        {
            switch (origen)
            {
                case "frmAlicuotasIva":
                    tabla.RejectChanges();
                    break;
                case "frmArticulos":
                    tabla.RejectChanges();
                    break;
                case "frmArticulosAgrupar":
                    tblStock.RejectChanges();
                    tblArticulos.RejectChanges();
                    break;
                case "frmArticulosBorrar":
                    tabla.RejectChanges();
                    break;
                case "frmArticulosGenerar":
                    tabla.RejectChanges();
                    break;
                case "frmArticulosItems":
                    tabla.RejectChanges();
                    break;
                case "frmClientes":
                    tabla.RejectChanges();
                    break;
                case "frmColores":
                    tabla.RejectChanges();
                    break;
                case "frmFormasPago":
                    tabla.RejectChanges();
                    break;
                case "frmGeneros":
                    tabla.RejectChanges();
                    break;
                case "frmProveedores":
                    tabla.RejectChanges();
                    break;
                case "frmTesoreriaMov":
                    dsTesoreriaMov.RejectChanges();
                    break;
                case "frmFondoCaja":
                    tabla.RejectChanges();
                    break;
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
