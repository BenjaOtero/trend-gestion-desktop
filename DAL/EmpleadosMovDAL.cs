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
    public class EmpleadosMovDAL
    {

        public static DataTable GetTabla()
        {
            DataTable tbl = new DataTable();
            tbl.TableName = "empleadosmovimientos";
            tbl.Columns.Add("IdMovEMOV", typeof(int));
            tbl.Columns.Add("FechaEMOV", typeof(DateTime));
            tbl.Columns.Add("IdEmpleadoEMOV", typeof(int));
            tbl.Columns.Add("IdMovTipoEMOV", typeof(int));
            tbl.Columns.Add("CantidadEMOV", typeof(int));
            tbl.Columns.Add("DetalleEMOV", typeof(string));
            tbl.Columns.Add("ImporteEMOV", typeof(double));
            tbl.Columns.Add("LiquidadoEMOV", typeof(int));
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["int"] };
            return tbl;
        }
           
        public static void GrabarDB(DataTable tblEmpleadosMov)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblEmpleadosMov);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("EmpleadosMovimientos_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("EmpleadosMovimientos_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("EmpleadosMovimientos_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovEMOV");
            SqlUpdateCommand1.Parameters.Add("p_fecha", MySqlDbType.Date, 10, "FechaEMOV");
            SqlUpdateCommand1.Parameters.Add("p_idEmpleado", MySqlDbType.Int32, 11, "IdEmpleadoEMOV");
            SqlUpdateCommand1.Parameters.Add("p_idTipoMov", MySqlDbType.Int32, 11, "IdMovTipoEMOV");
            SqlUpdateCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 3, "CantidadEMOV");
            SqlUpdateCommand1.Parameters.Add("p_detalle", MySqlDbType.VarChar, 50, "DetalleEMOV");
            SqlUpdateCommand1.Parameters.Add("p_importe", MySqlDbType.Double, 11, "ImporteEMOV");
            SqlUpdateCommand1.Parameters.Add("p_liquidado", MySqlDbType.Int32, 1, "LiquidadoEMOV");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovEMOV");
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.Date, 10, "FechaEMOV");
            SqlInsertCommand1.Parameters.Add("p_idEmpleado", MySqlDbType.Int32, 11, "IdEmpleadoEMOV");
            SqlInsertCommand1.Parameters.Add("p_idTipoMov", MySqlDbType.Int32, 11, "IdMovTipoEMOV");
            SqlInsertCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 3, "CantidadEMOV");
            SqlInsertCommand1.Parameters.Add("p_detalle", MySqlDbType.VarChar, 50, "DetalleEMOV");
            SqlInsertCommand1.Parameters.Add("p_importe", MySqlDbType.Double, 11, "ImporteEMOV");
            SqlInsertCommand1.Parameters.Add("p_liquidado", MySqlDbType.Int32, 1, "LiquidadoEMOV");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdMovEMOV");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        public static DataTable EmpleadosMovCons(string fechaDesde, string fechaHasta, int idEmpleado, int liquidado)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlSelectCommand1 = new MySqlCommand("EmpleadosMovimientos_Cons", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.Parameters.AddWithValue("p_fechaDesde", fechaDesde);
            SqlSelectCommand1.Parameters.AddWithValue("p_fechaHasta", fechaHasta);
            SqlSelectCommand1.Parameters.AddWithValue("p_idEmpleado", idEmpleado);
            SqlSelectCommand1.Parameters.AddWithValue("p_liquidado", liquidado);
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataTable tbl = new DataTable();
            SqlDataAdapter1.Fill(tbl);
            tbl.TableName = "empleadosmovimientos";
            SqlConnection1.Close();
            return tbl;
        }

    }

}

