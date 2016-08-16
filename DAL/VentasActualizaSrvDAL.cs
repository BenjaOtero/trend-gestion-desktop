using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Entities;

namespace DAL
{
    public class VentasActualizaSrvDAL 
    {
        private static MySqlConnection SqlConnection1;
        private static MySqlDataAdapter SqlDataAdapter1;
        private static MySqlCommand SqlSelectCommand1;
        private static MySqlCommand SqlInsertCommand1;
        public static DataSet dt;


        public static DataSet CrearDataset()
        {
            MySqlDataAdapter da = AdaptadorSELECT();
            dt = new DataSet();
            da.Fill(dt);
            return dt;
        }

        public static DataSet CrearDatasetArqueo(string where)
        {
            MySqlDataAdapter da = AdaptadorSelectArqueo(where);
            dt = new DataSet();
            da.Fill(dt);
            return dt;
        }

        public static void InsertarDT(DataTable tabla, Ventas entidad)
        {
            DataRowCollection cfilas = tabla.Rows;
            DataRow nuevaFila;
            try
            {
                nuevaFila = tabla.NewRow();
                nuevaFila[0] = entidad.IdVenta;
                nuevaFila[1] = entidad.IdPc;
                nuevaFila[2] = entidad.Fecha;
                nuevaFila[3] = entidad.IdCliente;
                cfilas.Add(nuevaFila);
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void EditarDT(DataRowView vistaFilaActual, Ventas entidad)
        {
            vistaFilaActual.BeginEdit();
            vistaFilaActual["IdPCVEN"] = entidad.IdPc.ToString();
            vistaFilaActual["FechaMSTK"] = entidad.Fecha.ToString();
            vistaFilaActual["IdClienteVEN"] = entidad.IdCliente.ToString();
            vistaFilaActual.EndEdit();
        }

        public static void EditarDT(string idVenta, DataTable tbl, Ventas entidad, DataSet dt)
        {
            DataRow[] foundRows;
            foundRows = dt.Tables["Ventas"].Select("IdVentaVEN = " + idVenta);
            DataRow filaActual = foundRows[0];
            filaActual.BeginEdit();
            filaActual["IdPCVEN"] = entidad.IdPc.ToString();
            filaActual["FechaMSTK"] = entidad.Fecha.ToString();
            filaActual["IdClienteVEN"] = entidad.IdCliente.ToString();
            filaActual.EndEdit();
        }

        public static void GrabarDB(DataSet dt)
        {
            MySqlDataAdapter da = AdaptadorABM(dt);
            da.Update(dt, "Ventas");
        }

        private static MySqlDataAdapter AdaptadorSELECT()
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("Ventas_Listar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        private static MySqlDataAdapter AdaptadorSelectArqueo(string where)
        {
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("Ventas_Arqueo", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_where", where);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        private static MySqlDataAdapter AdaptadorABM(DataSet dt)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Ventas_Insertar", SqlConnection1);
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;

            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdVentaVEN"); 
          //  SqlInsertCommand1.Parameters.AddWithValue("p_id", drvw["IdVentaVEN"]);        
            SqlInsertCommand1.Parameters.Add("p_id_pc", MySqlDbType.Int32, 11, "IdPCVEN");
          //    SqlInsertCommand1.Parameters.AddWithValue("p_id_pc", drvw["IdPCVEN"]);
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 11, "FechaVEN"); 
        //    SqlInsertCommand1.Parameters.AddWithValue("p_fecha", drvw["FechaVEN"]);
            SqlInsertCommand1.Parameters.Add("p_cliente", MySqlDbType.Int32, 11, "IdClienteVEN"); 
        //    SqlInsertCommand1.Parameters.AddWithValue("p_cliente", drvw["IdClienteVEN"]);
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            return SqlDataAdapter1;
        }

    }

}
