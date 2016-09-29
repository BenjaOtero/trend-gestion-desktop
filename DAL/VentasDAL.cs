using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class VentasDAL
    {
        private static MySqlConnection SqlConnection1;
        private static MySqlDataAdapter SqlDataAdapter1;
        private static MySqlCommand SqlSelectCommand1;
        private static MySqlCommand SqlInsertCommand1;
        private static MySqlCommand SqlUpdateCommand1;
        private static MySqlCommand SqlDeleteCommand1;
        public static DataSet dt;


        public static void GrabarVentas(DataSet dsVentas)
        {
            MySqlTransaction tr = null;
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            tr = SqlConnection1.BeginTransaction();
            DataTable tblVentas = dsVentas.Tables[0];
            DataTable tblVentasDetalle = dsVentas.Tables[1];
        reintetarVentas:
            try
            {
                GrabarDbVentas(dsVentas, SqlConnection1, tr);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // clave principal duplicada
                {
                    Random rand = new Random();
                    int clave = rand.Next(-2000000000, 2000000000);
                    tblVentas.Rows[0][0] = clave;
                    foreach (DataRow row in tblVentasDetalle.Rows)
                    {
                        row["IdVentaDVEN"] = clave;
                    }
                    goto reintetarVentas;
                }
                else
                {
                    tr.Rollback();
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        reintetarDetalle:
            try
            {
                GrabarDbDetalle(dsVentas, SqlConnection1, tr);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // clave principal duplicada
                {
                    Random rand = new Random();
                    int clave;
                    foreach (DataRow row in tblVentasDetalle.Rows)
                    {
                        clave = rand.Next(-2000000000, 2000000000);
                        row["IdDVEN"] = clave;
                    }
                    goto reintetarDetalle;
                }
                else
                {
                    tr.Rollback();
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            tr.Commit();
            SqlConnection1.Close();
        }

        public static DataTable GetTablaVentas()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "Ventas";
            tbl.Columns.Add("IdVentaVEN", typeof(int));
            tbl.Columns.Add("IdPCVEN", typeof(int));
            tbl.Columns.Add("FechaVEN", typeof(DateTime));
            tbl.Columns.Add("IdClienteVEN", typeof(int));
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdVentaVEN"] };
            return tbl;
        }

        public static DataSet CrearDatasetArqueo(string fechaDesde, string fechaHasta, int pc)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlSelectCommand1 = new MySqlCommand("Ventas_Arqueo2", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_desde", fechaDesde);
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_hasta", fechaHasta);
            SqlSelectCommand1.Parameters.AddWithValue("p_pc", pc);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataSet dt = new DataSet();
            SqlDataAdapter1.Fill(dt);
            SqlConnection1.Close();
            return dt;
        }

        public static DataSet CrearDatasetVentasPesos(int forma, string desde, string hasta, string locales, string genero)
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("VentasPesosCons_Listar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_forma", forma);
            SqlSelectCommand1.Parameters.AddWithValue("p_locales", locales);
            SqlSelectCommand1.Parameters.AddWithValue("p_fechaDesde", desde);
            SqlSelectCommand1.Parameters.AddWithValue("p_fechaHasta", hasta);
            SqlSelectCommand1.Parameters.AddWithValue("p_genero", genero);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            dt = new DataSet();
            SqlDataAdapter1.Fill(dt);
            SqlConnection1.Close();
            return dt;
        }

        public static DataTable GetVentasPesosDiarias(string desde, string hasta, int local, string forma)
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("Ventas_Diarias", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_desde", desde);
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_hasta", hasta);
            SqlSelectCommand1.Parameters.AddWithValue("p_local", local);
            SqlSelectCommand1.Parameters.AddWithValue("p_forma", forma);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataTable tbl = new DataTable();
            SqlDataAdapter1.Fill(tbl);
            SqlConnection1.Close();
            return tbl;
        }

        public static DataTable GetVentasDetalle(int forma, string desde, string hasta, int idLocal, string parametros)
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("VentasDetalle_Cons", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_forma", forma);
            SqlSelectCommand1.Parameters.AddWithValue("p_local", idLocal);
            SqlSelectCommand1.Parameters.AddWithValue("p_fechaDesde", desde);
            SqlSelectCommand1.Parameters.AddWithValue("p_fechaHasta", hasta);
            SqlSelectCommand1.Parameters.AddWithValue("p_parametros", parametros);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            dt = new DataSet();
            SqlDataAdapter1.Fill(dt);
            SqlConnection1.Close();
            DataTable tbl = dt.Tables[0];
            return tbl;
        }

        public static void GrabarDbVentas(DataSet dt, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorAbmVentas(dt, conn, tr);
            da.Update(dt, "Ventas");
        }        

        private static MySqlDataAdapter AdaptadorAbmVentas(DataSet dt, MySqlConnection SqlConnection1, MySqlTransaction tr)
        {
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Ventas_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Ventas_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("Ventas_Borrar", SqlConnection1);
            SqlInsertCommand1.Transaction = tr;
            SqlUpdateCommand1.Transaction = tr;
            SqlDeleteCommand1.Transaction = tr;
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdVentaVEN");
            SqlInsertCommand1.Parameters.Add("p_id_pc", MySqlDbType.Int32, 11, "IdPCVEN");
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 20, "FechaVEN");
            SqlInsertCommand1.Parameters.Add("p_cliente", MySqlDbType.Int32, 11, "IdClienteVEN");
            SqlInsertCommand1.Parameters.AddWithValue("p_cupon", "0");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdVentaVEN");
            SqlUpdateCommand1.Parameters.Add("p_id_pc", MySqlDbType.Int32, 11, "IdPCVEN");
            SqlUpdateCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 20, "FechaVEN");
            SqlUpdateCommand1.Parameters.Add("p_cliente", MySqlDbType.Int32, 11, "IdClienteVEN");
            SqlUpdateCommand1.Parameters.AddWithValue("p_cupon", "0");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdVentaVEN");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        public static void BorrarByPK(int PK)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlDeleteCommand1 = new MySqlCommand("Ventas_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDeleteCommand1.Parameters.AddWithValue("p_id", PK);
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            SqlDeleteCommand1.ExecuteNonQuery();
            SqlConnection1.Close();
        }

        public static void VentasHistoricasMantener()
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("VentasH_Mantener", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            SqlSelectCommand1.ExecuteNonQuery();
            SqlConnection1.Close();
        }


            //-------------------------------- VentasDetalle -------------------------------//

        public static DataTable GetTablaDetalle()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "VentasDetalle";
            tbl.Columns.Add("IdDVEN", typeof(int));
            tbl.Columns.Add("IdVentaDVEN", typeof(int));
            tbl.Columns.Add("IdLocalDVEN", typeof(int));
            tbl.Columns.Add("IdArticuloDVEN", typeof(string));
            tbl.Columns.Add("DescripcionDVEN", typeof(string));
            tbl.Columns.Add("CantidadDVEN", typeof(int));
            tbl.Columns.Add("PrecioPublicoDVEN", typeof(double));
            tbl.Columns.Add("PrecioCostoDVEN", typeof(double));
            tbl.Columns.Add("PrecioMayorDVEN", typeof(double));
            tbl.Columns.Add("IdFormaPagoDVEN", typeof(int));
            tbl.Columns.Add("NroCuponDVEN", typeof(int));
            tbl.Columns.Add("NroFacturaDVEN", typeof(int));
            tbl.Columns.Add("IdEmpleadoDVEN", typeof(int));
            tbl.Columns.Add("LiquidadoDVEN", typeof(int));
            tbl.Columns.Add("EsperaDVEN", typeof(int));
            tbl.Columns.Add("DevolucionDVEN", typeof(int));
            tbl.Columns.Add("OrdenarDVEN", typeof(int));
            //    tbl.Columns.Add("Subtotal", typeof(double));
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdDVEN"] };
            return tbl;
        }

        public static void GrabarDbDetalle(DataSet dt, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorAbmDetalle(conn, tr);
            da.Update(dt, "VentasDetalle");
        }

        private static MySqlDataAdapter AdaptadorAbmDetalle(MySqlConnection SqlConnection1, MySqlTransaction tr)
        {
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("VentasDetalle_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("VentasDetalle_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("VentasDetalle_Borrar", SqlConnection1);
            SqlInsertCommand1.Transaction = tr;
            SqlUpdateCommand1.Transaction = tr;
            SqlDeleteCommand1.Transaction = tr;
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id_detalle", MySqlDbType.Int32, 11, "IdDVEN");
            SqlInsertCommand1.Parameters.Add("p_id_venta", MySqlDbType.Int32, 11, "IdVentaDVEN");
            SqlInsertCommand1.Parameters.Add("p_id_local", MySqlDbType.Int32, 11, "IdLocalDVEN");
            SqlInsertCommand1.Parameters.Add("p_articulo", MySqlDbType.VarChar, 50, "IdArticuloDVEN");
            SqlInsertCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 11, "CantidadDVEN");
            SqlInsertCommand1.Parameters.Add("p_publico", MySqlDbType.Double, 11, "PrecioPublicoDVEN");
            SqlInsertCommand1.Parameters.Add("p_costo", MySqlDbType.Double, 11, "PrecioCostoDVEN");
            SqlInsertCommand1.Parameters.Add("p_mayor", MySqlDbType.Double, 11, "PrecioMayorDVEN");
            SqlInsertCommand1.Parameters.Add("p_forma_pago", MySqlDbType.Int32, 11, "IdFormaPagoDVEN");
            SqlInsertCommand1.Parameters.Add("p_nro_cupon", MySqlDbType.Int32, 11, "NroCuponDVEN");
            SqlInsertCommand1.Parameters.Add("p_nro_factura", MySqlDbType.Int32, 11, "NroFacturaDVEN");
            SqlInsertCommand1.Parameters.Add("p_id_empleado", MySqlDbType.Int32, 11, "IdEmpleadoDVEN");
            SqlInsertCommand1.Parameters.Add("p_liquidado", MySqlDbType.Bit, 11, "LiquidadoDVEN");
            SqlInsertCommand1.Parameters.Add("p_devolucion", MySqlDbType.Bit, 11, "DevolucionDVEN");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id_detalle", MySqlDbType.Int32, 11, "IdDVEN");
            SqlUpdateCommand1.Parameters.Add("p_id_venta", MySqlDbType.Int32, 11, "IdVentaDVEN");
            SqlUpdateCommand1.Parameters.Add("p_id_local", MySqlDbType.Int32, 11, "IdLocalDVEN");
            SqlUpdateCommand1.Parameters.Add("p_articulo", MySqlDbType.VarChar, 50, "IdArticuloDVEN");
            SqlUpdateCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 11, "CantidadDVEN");
            SqlUpdateCommand1.Parameters.Add("p_publico", MySqlDbType.Double, 11, "PrecioPublicoDVEN");
            SqlUpdateCommand1.Parameters.Add("p_costo", MySqlDbType.Double, 11, "PrecioCostoDVEN");
            SqlUpdateCommand1.Parameters.Add("p_mayor", MySqlDbType.Double, 11, "PrecioMayorDVEN");
            SqlUpdateCommand1.Parameters.Add("p_forma_pago", MySqlDbType.Int32, 11, "IdFormaPagoDVEN");
            SqlUpdateCommand1.Parameters.Add("p_nro_cupon", MySqlDbType.Int32, 11, "NroCuponDVEN");
            SqlUpdateCommand1.Parameters.Add("p_nro_factura", MySqlDbType.Int32, 11, "NroFacturaDVEN");
            SqlUpdateCommand1.Parameters.Add("p_id_empleado", MySqlDbType.Int32, 11, "IdEmpleadoDVEN");
            SqlUpdateCommand1.Parameters.Add("p_liquidado", MySqlDbType.Bit, 11, "LiquidadoDVEN");
            SqlUpdateCommand1.Parameters.Add("p_devolucion", MySqlDbType.Bit, 11, "DevolucionDVEN");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id_detalle", MySqlDbType.Int32, 11, "IdDVEN");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

    }

}
