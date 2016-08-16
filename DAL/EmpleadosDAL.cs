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
    public class EmpleadosDAL
    {
        public static DataSet GetEmpleados()
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlSelectCommand1 = new MySqlCommand("Empleados_Listar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataSet dt = new DataSet();
            SqlDataAdapter1.Fill(dt, "Empleados");
            SqlConnection1.Close();
            return dt;
        }

        public static void GrabarDB(DataTable tblEmpleados)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter da = AdaptadorABM(SqlConnection1);
            da.Update(tblEmpleados);
            SqlConnection1.Close();
        }

        private static MySqlDataAdapter AdaptadorABM(MySqlConnection SqlConnection1)
        {
            MySqlCommand SqlInsertCommand1;
            MySqlCommand SqlUpdateCommand1;
            MySqlCommand SqlDeleteCommand1;
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("Empleados_Insertar", SqlConnection1);
            SqlUpdateCommand1 = new MySqlCommand("Empleados_Actualizar", SqlConnection1);
            SqlDeleteCommand1 = new MySqlCommand("Empleados_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdEmpleadoEMP");
            SqlUpdateCommand1.Parameters.Add("p_dni", MySqlDbType.VarChar, 10, "DniEMP");
            SqlUpdateCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "NombreEMP");
            SqlUpdateCommand1.Parameters.Add("p_apellido", MySqlDbType.VarChar, 25, "ApellidoEMP");
            SqlUpdateCommand1.Parameters.Add("p_direccion", MySqlDbType.VarChar, 50, "DireccionEMP");
            SqlUpdateCommand1.Parameters.Add("p_telefono", MySqlDbType.VarChar, 15, "TelefonoEMP");
            SqlUpdateCommand1.Parameters.Add("p_fechaNac", MySqlDbType.Date, 50, "FechaNacEMP");
            SqlUpdateCommand1.Parameters.Add("p_fechaIngreso", MySqlDbType.Date, 50, "FechaIngresoEMP");
            SqlUpdateCommand1.Parameters.Add("p_idLocal", MySqlDbType.Int32, 3, "IdLocalEMP");
            SqlUpdateCommand1.Parameters.Add("p_salario", MySqlDbType.Double, 12, "SalarioEMP");
            SqlUpdateCommand1.Parameters.Add("p_cargas", MySqlDbType.Double, 12, "CargasSocialesEMP");
            SqlUpdateCommand1.Parameters.Add("p_activo", MySqlDbType.Int32, 1, "Activa");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdEmpleadoEMP");
            SqlInsertCommand1.Parameters.Add("p_dni", MySqlDbType.VarChar, 10, "DniEMP");
            SqlInsertCommand1.Parameters.Add("p_nombre", MySqlDbType.VarChar, 50, "NombreEMP");
            SqlInsertCommand1.Parameters.Add("p_apellido", MySqlDbType.VarChar, 25, "ApellidoEMP");
            SqlInsertCommand1.Parameters.Add("p_direccion", MySqlDbType.VarChar, 50, "DireccionEMP");
            SqlInsertCommand1.Parameters.Add("p_telefono", MySqlDbType.VarChar, 15, "TelefonoEMP");
            SqlInsertCommand1.Parameters.Add("p_fechaNac", MySqlDbType.Date, 50, "FechaNacEMP");
            SqlInsertCommand1.Parameters.Add("p_fechaIngreso", MySqlDbType.Date, 50, "FechaIngresoEMP");
            SqlInsertCommand1.Parameters.Add("p_idLocal", MySqlDbType.Int32, 3, "IdLocalEMP");
            SqlInsertCommand1.Parameters.Add("p_salario", MySqlDbType.Double, 12, "SalarioEMP");
            SqlInsertCommand1.Parameters.Add("p_cargas", MySqlDbType.Double, 12, "CargasSocialesEMP");
            SqlInsertCommand1.Parameters.Add("p_activo", MySqlDbType.Int32, 1, "Activa");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.Int32, 11, "IdClienteCLI");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        public static DataTable GetLiquidacion()
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlSelectCommand1 = new MySqlCommand("EmpleadosLiquidacion", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataTable tbl = new DataTable();
            SqlDataAdapter1.Fill(tbl);
            SqlConnection1.Close();
            return tbl;
        }

    }

}
