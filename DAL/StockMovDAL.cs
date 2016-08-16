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
    public class StockMovDAL
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
            tbl.TableName = "StockMov";
            tbl.Columns.Add("IdMovMSTK", typeof(int));
            tbl.Columns.Add("FechaMSTK", typeof(DateTime));
            tbl.Columns.Add("OrigenMSTK", typeof(int));
            tbl.Columns.Add("DestinoMSTK", typeof(int));
            tbl.Columns.Add("CompensaMSTK", typeof(int));
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdMovMSTK"] };  
            return tbl;
        }

        public static DataSet CrearDatasetCons(string fechaDesde, string fechaHasta, int idLocal, string tipoMov, string movimiento,
            string articulo, string descripcion)
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("StockMov_Cons", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_desde", fechaDesde);
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_hasta", fechaHasta);
            SqlSelectCommand1.Parameters.AddWithValue("p_id_local", idLocal);
            SqlSelectCommand1.Parameters.AddWithValue("p_tipo_mov", tipoMov);
            SqlSelectCommand1.Parameters.AddWithValue("p_movimiento", movimiento);
            SqlSelectCommand1.Parameters.AddWithValue("p_articulo", articulo);
            SqlSelectCommand1.Parameters.AddWithValue("p_descripcion", descripcion);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            dt = new DataSet();
            SqlDataAdapter1.Fill(dt);
            SqlConnection1.Close();
            return dt;
        }

        public static DataTable GetCompensacionesPesos(string fechaDesde, string fechaHasta, int idLocal)
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("StockMov_CompPesos", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_desde", fechaDesde);
            SqlSelectCommand1.Parameters.AddWithValue("p_fecha_hasta", fechaHasta);
            SqlSelectCommand1.Parameters.AddWithValue("p_id_local", idLocal);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            dt = new DataSet();
            SqlDataAdapter1.Fill(dt);
            SqlConnection1.Close();
            DataTable tbl = new DataTable();
            tbl = dt.Tables[0];
            return tbl;
        }

        public static void GrabarDB(DataSet dt, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorABM(dt, conn, tr);
            da.Update(dt, "StockMov");
        }        

        private static MySqlDataAdapter AdaptadorABM(DataSet dt, MySqlConnection SqlConnection1, MySqlTransaction tr)
        {
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("StockMov_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("StockMov_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("StockMov_Borrar", SqlConnection1);
            SqlInsertCommand1.Transaction = tr;
            SqlUpdateCommand1.Transaction = tr;
            SqlDeleteCommand1.Transaction = tr;
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            //     SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovMSTK"); Esta otra forma también funciona            
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovMSTK");
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime,20, "FechaMSTK");
            SqlInsertCommand1.Parameters.Add("p_origen", MySqlDbType.Int32, 11, "OrigenMSTK");
            SqlInsertCommand1.Parameters.Add("p_destino", MySqlDbType.Int32, 11, "DestinoMSTK");
            SqlInsertCommand1.Parameters.Add("p_compensa", MySqlDbType.Int32, 1, "CompensaMSTK");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovMSTK");
            SqlUpdateCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 20, "FechaMSTK");
            SqlUpdateCommand1.Parameters.Add("p_origen", MySqlDbType.Int32, 11, "OrigenMSTK");
            SqlUpdateCommand1.Parameters.Add("p_destino", MySqlDbType.Int32, 11, "DestinoMSTK");
            SqlUpdateCommand1.Parameters.Add("p_compensa", MySqlDbType.Int32, 11, "CompensaMSTK");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovMSTK");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        public static void BorrarByPK(int PK)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlDeleteCommand1 = new MySqlCommand("StockMov_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;

            SqlDeleteCommand1.Parameters.AddWithValue("p_id", PK);
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            SqlDeleteCommand1.ExecuteNonQuery();
            SqlConnection1.Close();
        }

    }

}
