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
    public class ColoresDAL
    {
        public static void GrabarDB(DataTable tblColores)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblColores);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {            
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Colores_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Colores_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("Colores_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;


            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdColorCOL");
            SqlUpdateCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 50, "DescripcionCOL");
            SqlUpdateCommand1.Parameters.Add("p_hex", MySqlDbType.VarChar, 50, "HexCOL");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdColorCOL");
            SqlInsertCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 50, "DescripcionCOL");
            SqlInsertCommand1.Parameters.Add("p_hex", MySqlDbType.VarChar, 50, "HexCOL");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdColorCOL");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

    }

}
