using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class ArticulosItemsDAL
    {

        public static void GrabarDB(DataTable tblArticulosItems)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblArticulosItems);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("ArticulosItems_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("ArticulosItems_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("ArticulosItems_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdItemITE");
            SqlInsertCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 50, "DescripcionITE");
            SqlInsertCommand1.Parameters.Add("p_descripcionWeb", MySqlDbType.VarChar, 50, "DescripcionWebITE");
            SqlInsertCommand1.Parameters.Add("p_activoWeb", MySqlDbType.Int32, 1, "ActivoWebITE");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdItemITE");
            SqlUpdateCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 50, "DescripcionITE");
            SqlUpdateCommand1.Parameters.Add("p_descripcionWeb", MySqlDbType.VarChar, 50, "DescripcionWebITE");
            SqlUpdateCommand1.Parameters.Add("p_activoWeb", MySqlDbType.Int32, 1, "ActivoWebITE");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdItemITE");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

    }

}
