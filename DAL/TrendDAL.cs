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
    public class TrendDAL
    {

        public static DataSet GetDataPopup(int razon)
        {
            MySqlConnection SqlConnection1 = DALBase.GetTrendConnection();
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlSelectCommand1 = new MySqlCommand("Productos_Usuarios", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_user", razon);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataSet dt = new DataSet();
            SqlDataAdapter1.Fill(dt);
            SqlConnection1.Close();
            return dt;        
        }

        // CODIGO ANTERIOR

        public static DataTable GetTablaCliente()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "clientes";
            tbl.Columns.Add("nombre", typeof(string));
            tbl.Columns.Add("apellido", typeof(string));
            tbl.Columns.Add("correo", typeof(string));
            tbl.Columns.Add("clave", typeof(string));
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["id_cliente"] };
            return tbl;
        }

        public static DataTable GetTablaProductosCliente()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "productos_clientes";
            tbl.Columns.Add("id", typeof(int));
            tbl.Columns.Add("fecha_alta", typeof(string));
            tbl.Columns.Add("correo_cliente", typeof(string));
            tbl.Columns.Add("id_producto", typeof(int));
            tbl.Columns.Add("clave_producto", typeof(string));
            return tbl;
        }

        public static void GrabarDB(DataTable tblClientes, DataTable tblProductosClientes)
        {
            MySqlConnection SqlConnection1 = DALBase.GetTrendConnection();
            MySqlDataAdapter da = AdaptadorClientes(SqlConnection1);
            da.Update(tblClientes);
            da = AdaptadorProductosClientes(SqlConnection1);
            da.Update(tblProductosClientes);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorClientes(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Clientes_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Clientes_Actualizar", SqlConnection1);
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "nombre");
            SqlUpdateCommand1.Parameters.Add("p_apellido", MySqlDbType.VarChar, 50, "apellido");
            SqlUpdateCommand1.Parameters.Add("p_correo", MySqlDbType.VarChar, 50, "correo");
            SqlUpdateCommand1.Parameters.Add("p_clave", MySqlDbType.VarChar, 50, "clave");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "nombre");
            SqlInsertCommand1.Parameters.Add("p_apellido", MySqlDbType.VarChar, 50, "apellido");
            SqlInsertCommand1.Parameters.Add("p_correo", MySqlDbType.VarChar, 50, "correo");
            SqlInsertCommand1.Parameters.Add("p_clave", MySqlDbType.VarChar, 50, "clave");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            return SqlDataAdapter1;
        }

        private static MySqlDataAdapter AdaptadorProductosClientes(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Productos_Clientes_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Productos_Clientes_Actualizar", SqlConnection1);
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int64, 11, "id");
            SqlUpdateCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 50, "fecha_alta");
            SqlUpdateCommand1.Parameters.Add("p_correo_cliente", MySqlDbType.String, 50, "correo_cliente");
            SqlUpdateCommand1.Parameters.Add("p_id_producto", MySqlDbType.Int16, 2, "id_producto");
            SqlUpdateCommand1.Parameters.Add("p_clave_producto", MySqlDbType.String, 50, "id_producto");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 50, "fecha_alta");
            SqlInsertCommand1.Parameters.Add("p_correo_cliente", MySqlDbType.String, 50, "correo_cliente");
            SqlInsertCommand1.Parameters.Add("p_id_producto", MySqlDbType.Int16, 2, "id_producto");
            SqlInsertCommand1.Parameters.Add("p_clave_producto", MySqlDbType.String, 50, "id_producto");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            return SqlDataAdapter1;
        }
    }
}
