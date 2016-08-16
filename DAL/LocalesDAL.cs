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
    public class LocalesDAL
    {
        public static void GrabarDB(DataTable tblLocales)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblLocales);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Locales_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Locales_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("Locales_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;


            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdLocalLOC");
            SqlUpdateCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "NombreLOC");
            SqlUpdateCommand1.Parameters.Add("p_direccion", MySqlDbType.VarChar, 50, "DireccionLOC");
            SqlUpdateCommand1.Parameters.Add("p_telefono", MySqlDbType.VarChar, 50, "TelefonoLOC");
            SqlUpdateCommand1.Parameters.Add("p_activoWeb", MySqlDbType.Int32, 1, "ActivoWebLOC");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdLocalLOC");
            SqlInsertCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "NombreLOC");
            SqlInsertCommand1.Parameters.Add("p_direccion", MySqlDbType.VarChar, 50, "DireccionLOC");
            SqlInsertCommand1.Parameters.Add("p_telefono", MySqlDbType.VarChar, 50, "TelefonoLOC");
            SqlInsertCommand1.Parameters.Add("p_activoWeb", MySqlDbType.Int32, 1, "ActivoWebLOC");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdLocalLOC");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

    }

}
