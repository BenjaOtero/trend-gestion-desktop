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
    public class UsuariosDAL
    {
        public static DataTable GetTabla()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "usuarios";
            tbl.Columns.Add("id_usuario", typeof(Int64));
            tbl.Columns.Add("nombre", typeof(string));
            tbl.Columns.Add("apellido", typeof(string));
            tbl.Columns.Add("correo", typeof(string));
            tbl.Columns.Add("clave", typeof(string));
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["id_usuario"] };
            return tbl;
        }

        public static void GrabarDB(DataTable tblUsuarios)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblUsuarios);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Usuarios_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Usuarios_Actualizar", SqlConnection1);
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int64, 10, "id_usuario");
            SqlUpdateCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "nombre");
            SqlUpdateCommand1.Parameters.Add("p_apellido", MySqlDbType.VarChar, 50, "apellido");
            SqlUpdateCommand1.Parameters.Add("p_correo", MySqlDbType.VarChar, 50, "correo");
            SqlUpdateCommand1.Parameters.Add("p_clave", MySqlDbType.VarChar, 50, "clave");
            SqlUpdateCommand1.Parameters.Add("p_nivel", MySqlDbType.Int16, 1, "nivel_seguridad");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int64, 10, "id_usuario");
            SqlInsertCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "nombre");
            SqlInsertCommand1.Parameters.Add("p_apellido", MySqlDbType.VarChar, 50, "apellido");
            SqlInsertCommand1.Parameters.Add("p_correo", MySqlDbType.VarChar, 50, "correo");
            SqlInsertCommand1.Parameters.Add("p_clave", MySqlDbType.VarChar, 50, "clave");
            SqlInsertCommand1.Parameters.Add("p_nivel", MySqlDbType.Int16, 1, "nivel_seguridad");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            return SqlDataAdapter1;
        }
    }
}
