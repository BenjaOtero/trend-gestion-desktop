using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class MantenimientoDAL
    {

        public static void Mantenimiento()
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlSelectCommand1 = new MySqlCommand("DatosServer_Borrar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            SqlSelectCommand1.ExecuteNonQuery();
            SqlConnection1.Close();
        }
    }
}
