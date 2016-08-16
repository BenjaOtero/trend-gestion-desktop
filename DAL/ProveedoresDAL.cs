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
    public class ProveedoresDAL
    {
        public static void GrabarDB(DataTable tblProveedores)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblProveedores);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Proveedores_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Proveedores_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("Proveedores_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;


            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdProveedorPRO");
            SqlUpdateCommand1.Parameters.Add("p_razon", MySqlDbType.VarChar, 50, "RazonSocialPRO");
            SqlUpdateCommand1.Parameters.Add("p_direccion", MySqlDbType.VarChar, 50, "DireccionPRO");
            SqlUpdateCommand1.Parameters.Add("p_codigo", MySqlDbType.VarChar, 50, "CodigoPostalPRO");
            SqlUpdateCommand1.Parameters.Add("p_telefono", MySqlDbType.VarChar, 50, "TelefonoPRO");
            SqlUpdateCommand1.Parameters.Add("p_contacto", MySqlDbType.VarChar, 50, "ContactoPRO");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdProveedorPRO");
            SqlInsertCommand1.Parameters.Add("p_razon", MySqlDbType.VarChar, 50, "RazonSocialPRO");
            SqlInsertCommand1.Parameters.Add("p_direccion", MySqlDbType.VarChar, 50, "DireccionPRO");
            SqlInsertCommand1.Parameters.Add("p_codigo", MySqlDbType.VarChar, 50, "CodigoPostalPRO");
            SqlInsertCommand1.Parameters.Add("p_telefono", MySqlDbType.VarChar, 50, "TelefonoPRO");
            SqlInsertCommand1.Parameters.Add("p_contacto", MySqlDbType.VarChar, 50, "ContactoPRO");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdProveedorPRO");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

    }

}
