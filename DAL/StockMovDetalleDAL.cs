﻿using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class StockMovDetalleDAL
    {
        private static MySqlDataAdapter SqlDataAdapter1;
        private static MySqlCommand SqlInsertCommand1;
        private static MySqlCommand SqlUpdateCommand1;
        private static MySqlCommand SqlDeleteCommand1;
        public static DataSet dt;

        public static DataTable GetTabla()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "StockMovDetalle";
            tbl.Columns.Add("IdMSTKD", typeof(int));
            tbl.Columns.Add("IdMovMSTKD", typeof(int));
            tbl.Columns.Add("IdArticuloMSTKD", typeof(string));
            tbl.Columns.Add("DescripcionART", typeof(string));
            tbl.Columns.Add("CantidadMSTKD", typeof(int));
            tbl.Columns.Add("CompensaMSTKD", typeof(int));
            tbl.Columns.Add("OrigenMSTKD", typeof(int));
            tbl.Columns.Add("DestinoMSTKD", typeof(int));            
      //      tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdMSTKD"] };
            return tbl;
        }

        public static void GrabarDB(DataSet dt, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorABM(conn, tr);
            da.Update(dt, "StockMovDetalle");
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1, MySqlTransaction tr)
        {
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("StockMovDetalle_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("StockMovDetalle_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("StockMovDetalle_Borrar", SqlConnection1);
            SqlInsertCommand1.Transaction = tr;
            SqlUpdateCommand1.Transaction = tr;
            SqlDeleteCommand1.Transaction = tr;
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id_detalle", MySqlDbType.Int32, 11, "IdMSTKD");
            SqlInsertCommand1.Parameters.Add("p_id_mov", MySqlDbType.Int32, 11, "IdMovMSTKD");
            SqlInsertCommand1.Parameters.Add("p_articulo", MySqlDbType.VarChar, 50, "IdArticuloMSTKD");
            SqlInsertCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 11, "CantidadMSTKD");
            SqlInsertCommand1.Parameters.Add("p_compensa", MySqlDbType.Int32, 11, "CompensaMSTKD");
            SqlInsertCommand1.Parameters.Add("p_origen", MySqlDbType.Int32, 11, "OrigenMSTKD");
            SqlInsertCommand1.Parameters.Add("p_destino", MySqlDbType.Int32, 11, "DestinoMSTKD");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id_detalle", MySqlDbType.Int32, 11, "IdMSTKD");
            SqlUpdateCommand1.Parameters.Add("p_id_mov", MySqlDbType.Int32, 11, "IdMovMSTKD");
            SqlUpdateCommand1.Parameters.Add("p_articulo", MySqlDbType.VarChar, 50, "IdArticuloMSTKD");
            SqlUpdateCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 11, "CantidadMSTKD");
            SqlUpdateCommand1.Parameters.Add("p_compensa", MySqlDbType.Int32, 11, "CompensaMSTKD");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id_detalle", MySqlDbType.Int32, 11, "IdMSTKD");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }
    }
}
