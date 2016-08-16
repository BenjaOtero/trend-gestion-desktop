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
    public class EmpleadosMovTiposDAL
    {

        public static void GrabarDB(DataTable tblEmpleadosMovTipos)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblEmpleadosMovTipos);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("EmpleadosMovTipos_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("EmpleadosMovTipos_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("EmpleadosMovTipos_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 3, "IdMovETIP");
            SqlUpdateCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 50, "DescripcionETIP");
            SqlUpdateCommand1.Parameters.Add("p_remumeracion", MySqlDbType.Int32, 1, "RemuneracionETIP");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 3, "IdMovETIP");
            SqlInsertCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 50, "DescripcionETIP");
            SqlInsertCommand1.Parameters.Add("p_remumeracion", MySqlDbType.Int32, 1, "RemuneracionETIP");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 3, "IdMovETIP");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

    }

}

