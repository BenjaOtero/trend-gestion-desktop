using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace DAL
{
    public class StockDAL
    {
        private static MySqlConnection SqlConnection1;
        private static MySqlDataAdapter SqlDataAdapter1;
        private static MySqlCommand SqlSelectCommand1;
        private static MySqlCommand SqlUpdateCommand1;
        public static DataSet dt;

        public static DataTable GetStock()
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("Stock_Listar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataTable tbl = new DataTable();
            SqlDataAdapter1.Fill(tbl);
            SqlConnection1.Close();
            return tbl;
        }

        public static DataSet CrearDataset(string whereLocales, string genero, int proveedor, string articulo, string descripcion, int activoWeb)
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("Stock_Cons2", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_locales", whereLocales);
            SqlSelectCommand1.Parameters.AddWithValue("p_genero", genero);
            SqlSelectCommand1.Parameters.AddWithValue("p_proveedor", proveedor);
            SqlSelectCommand1.Parameters.AddWithValue("p_articulo", articulo);
            SqlSelectCommand1.Parameters.AddWithValue("p_descripcion", descripcion);
            SqlSelectCommand1.Parameters.AddWithValue("p_activoWeb", activoWeb);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            dt = new DataSet();
            SqlDataAdapter1.Fill(dt, "Stock");
            SqlConnection1.Close();
            return dt;
        }

        public static void Update(DataTable tblStock, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorUpdate(conn, tr);
            da.Update(tblStock);
        }    

        private static MySqlDataAdapter AdaptadorUpdate(MySqlConnection SqlConnection1, MySqlTransaction tr)
        {
            SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlInsertCommand1;
            SqlUpdateCommand1 = new MySqlCommand("Stock_Actualizar", SqlConnection1);
            SqlInsertCommand1 = new MySqlCommand("Stock_Insertar", SqlConnection1);
            SqlUpdateCommand1.Transaction = tr;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            SqlInsertCommand1.Parameters.Add("p_id_articulo", MySqlDbType.VarChar, 50, "IdArticuloSTK");
            SqlInsertCommand1.Parameters.Add("p_id_local", MySqlDbType.Int32, 11, "IdLocalSTK");
            SqlInsertCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 11, "CantidadSTK");

            SqlUpdateCommand1.Parameters.Add("p_id_articulo", MySqlDbType.VarChar, 50, "IdArticuloSTK");
            SqlUpdateCommand1.Parameters.Add("p_id_local", MySqlDbType.Int32, 11, "IdLocalSTK");
            SqlUpdateCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 11, "CantidadSTK");

            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

    }

}
