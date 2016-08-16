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
    public class FondoCajaDAL
    {

        public static DataTable GetTabla()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "FondoCaja";
            tbl.Columns.Add("IdFondoFONP", typeof(int));
            tbl.Columns.Add("FechaFONP", typeof(DateTime));
            tbl.Columns.Add("IdPcFONP", typeof(int));
            tbl.Columns.Add("ImporteFONP", typeof(double));
            return tbl;
        }

        public static void GrabarDB(DataTable tbl)
        {
            using (MySqlConnection SqlConnection1 = DALBase.GetConnection())
            {
                MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
                da.Update(tbl);
            }       
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlInsertCommand1 = new MySqlCommand("FondoCaja_Insertar2", SqlConnection1);
            MySqlCommand SqlUpdateCommand1 = new MySqlCommand("FondoCaja_Actualizar2", SqlConnection1);
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdFondoFONP");
            SqlUpdateCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 20, "FechaFONP");
            SqlUpdateCommand1.Parameters.Add("p_pc", MySqlDbType.Int32, 11, "IdPcFONP");
            SqlUpdateCommand1.Parameters.Add("p_importe", MySqlDbType.Double, 50, "ImporteFONP");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdFondoFONP");
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 20, "FechaFONP");
            SqlInsertCommand1.Parameters.Add("p_pc", MySqlDbType.Int32, 11, "IdPcFONP");
            SqlInsertCommand1.Parameters.Add("p_importe", MySqlDbType.Double, 50, "ImporteFONP");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            return SqlDataAdapter1;
        }

        public static DataSet CrearDatasetCons()
        {
            using (MySqlConnection SqlConnection1 = DALBase.GetConnection())
            {
                MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
                MySqlCommand SqlSelectCommand1 = new MySqlCommand("FondoCaja_ListarCons", SqlConnection1);
                SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
                SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
                DataSet dt = new DataSet();
                SqlDataAdapter1.Fill(dt, "FondoCaja");
                return dt;
            }
        }

        public static void BorrarByPK(int PK)
        {
            using (MySqlConnection SqlConnection1 = DALBase.GetConnection())
            {
                SqlConnection1.Open();
                MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
                MySqlCommand SqlDeleteCommand1 = new MySqlCommand("FondoCaja_BorrarByPk", SqlConnection1);
                SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
                SqlDeleteCommand1.Parameters.AddWithValue("p_id", PK);
                SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
                SqlDeleteCommand1.ExecuteNonQuery();
            }
        }

    }

}
