using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace DAL
{
    public class TesoreriaMovimientosDAL
    { 
        private static MySqlDataAdapter SqlDataAdapter1;
        private static MySqlCommand SqlInsertCommand1;
        private static MySqlCommand SqlUpdateCommand1;
        private static MySqlCommand SqlDeleteCommand1;
        public static DataSet dt;

        public static DataTable GetTabla()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "TesoreriaMovimientos";
            tbl.Columns.Add("IdMovTESM", typeof(int));
            tbl.Columns.Add("FechaTESM", typeof(DateTime));
            tbl.Columns.Add("IdPcTESM", typeof(int));
            tbl.Columns.Add("DetalleTESM", typeof(string));
            tbl.Columns.Add("ImporteTESM", typeof(double));
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdVentaVEN"] };
            return tbl;
        }

        public static void GrabarDB(DataSet dt, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorABM(conn, tr);
            da.Update(dt, "TesoreriaMovimientos");
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1, MySqlTransaction tr)
        {
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("TesoreriaMov_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("TesoreriaMov_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("TesoreriaMov_Borrar", SqlConnection1);
            SqlInsertCommand1.Transaction = tr;
            SqlUpdateCommand1.Transaction = tr;
            SqlDeleteCommand1.Transaction = tr;
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;



            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovTESM");
            SqlUpdateCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 20,"FechaTESM");
            SqlUpdateCommand1.Parameters.Add("p_pc", MySqlDbType.Int32, 11, "IdPcTESM");
            SqlUpdateCommand1.Parameters.Add("p_detalle", MySqlDbType.VarChar, 200, "DetalleTESM");
            SqlUpdateCommand1.Parameters.Add("p_importe", MySqlDbType.Double, 50, "ImporteTESM");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovTESM");
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 20, "FechaTESM");
            SqlInsertCommand1.Parameters.Add("p_pc", MySqlDbType.Int32, 11, "IdPcTESM");
            SqlInsertCommand1.Parameters.Add("p_detalle", MySqlDbType.VarChar, 200, "DetalleTESM");
            SqlInsertCommand1.Parameters.Add("p_importe", MySqlDbType.Double, 50, "ImporteTESM");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovTESM");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        public static void BorrarByPK(int PK)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlDeleteCommand1 = new MySqlCommand("TesoreriaMov_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDeleteCommand1.Parameters.AddWithValue("p_id", PK);
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            SqlDeleteCommand1.ExecuteNonQuery();
            SqlConnection1.Close();
        }

    }

}
