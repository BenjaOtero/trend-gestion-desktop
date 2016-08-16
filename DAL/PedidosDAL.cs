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
    public class PedidosDAL
    {
        private static MySqlConnection SqlConnection1;
        private static MySqlDataAdapter SqlDataAdapter1;
        private static MySqlCommand SqlSelectCommand1;
        public static DataSet dt;

        public static DataSet CrearDataset(string fecha, string genero)
        {
            SqlConnection1 = DALBase.GetConnection();            
            SqlSelectCommand1 = new MySqlCommand("Pedido_Cons", SqlConnection1);
            SqlSelectCommand1.Parameters.AddWithValue("p_fechaDesde", fecha);
            SqlSelectCommand1.Parameters.AddWithValue("p_genero", genero);            
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            dt = new DataSet();
            SqlDataAdapter1.Fill(dt, "Pedidos");
            SqlConnection1.Close();
            return dt;
        }

    }

}
