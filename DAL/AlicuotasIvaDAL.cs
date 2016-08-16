using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace DAL
{
    public class AlicuotasIvaDAL
    {
        public static void GrabarDB(DataTable tblAlicuotasIva, string id, string oldId)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1, id, oldId);
            da.Update(tblAlicuotasIva);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1, string id, string oldId)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("AlicuotasIva_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("AlicuotasIva_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("AlicuotasIva_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;


            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.AddWithValue("p_id", id);
            SqlUpdateCommand1.Parameters.AddWithValue("p_old_id", oldId);
            SqlUpdateCommand1.Parameters.Add("p_porcentaje", MySqlDbType.String, 10, "PorcentajeALI");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.AddWithValue("p_id", id);
            SqlInsertCommand1.Parameters.AddWithValue("p_old_id", oldId);
            SqlInsertCommand1.Parameters.Add("p_porcentaje", MySqlDbType.String, 10, "PorcentajeALI");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdAlicuotaALI");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        public static void SaveImage(byte[] ImageData)
        {
            string stringCommand;
            stringCommand = "UPDATE alicuotasiva SET Imagen = @Image";
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            MySqlCommand command = new MySqlCommand(stringCommand, SqlConnection1);
            command.Parameters.AddWithValue("@Image", ImageData); 
            command.ExecuteNonQuery();
            SqlConnection1.Close();
        }

        public static DataTable RecuperarImage()
        {
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            string stringCommand;
            stringCommand = "SELECT * FROM alicuotasiva";
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            MySqlCommand command = new MySqlCommand(stringCommand, SqlConnection1);
            SqlDataAdapter1.SelectCommand = command;
            DataTable tbl = new DataTable();
            SqlDataAdapter1.Fill(tbl);
            SqlConnection1.Close();
            return tbl;
        }




    }
}
