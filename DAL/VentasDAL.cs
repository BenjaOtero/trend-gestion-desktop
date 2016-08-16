using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Entities;

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

        public static DataTable GetTabla()
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

        public static void GrabarDB(DataSet dt, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorABM(dt, conn, tr);
            da.Update(dt, "Ventas");
        }        

        private static MySqlDataAdapter AdaptadorABM(DataSet dt, MySqlConnection SqlConnection1, MySqlTransaction tr)
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
            SqlConnection1.Open();
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
            SqlConnection1.Open();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("VentasH_Mantener", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            SqlSelectCommand1.ExecuteNonQuery();
            SqlConnection1.Close();
        }

    }

}
