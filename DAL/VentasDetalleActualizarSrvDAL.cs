using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;
using System.IO;

namespace DAL
{
    public class VentasDetalleActualizarSrvDAL
    {
        private static MySqlDataAdapter SqlDataAdapter1;
        private static MySqlCommand SqlInsertCommand1;
        private static MySqlCommand SqlSelectCommand1;
        public static DataSet dt;

        public static void CrearDataset()
        {
            MySqlDataAdapter da = AdaptadorSELECT();
            dt = new DataSet();
            da.Fill(dt);
            string strFilePath = Application.StartupPath + "\\Datasets_xml\\";
            dt.Tables[0].WriteXml(strFilePath + "VentasDetalle.xml", XmlWriteMode.WriteSchema);
        }

        public static void GrabarDB(DataSet dt)
        {
            MySqlDataAdapter da = AdaptadorABM();
            da.Update(dt);
        }

        private static MySqlDataAdapter AdaptadorSELECT()
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("VentasDetalle_Listar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        private static MySqlDataAdapter AdaptadorABM()
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlInsertCommand1 = new MySqlCommand("VentasDetalle_Insertar", SqlConnection1);
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id_detalle", MySqlDbType.Int32, 11, "IdDVEN");
            SqlInsertCommand1.Parameters.Add("p_id_venta", MySqlDbType.Int32, 11, "IdVentaDVEN");
            SqlInsertCommand1.Parameters.Add("p_articulo", MySqlDbType.VarChar, 50, "IdArticuloDVEN");
            SqlInsertCommand1.Parameters.Add("p_cantidad", MySqlDbType.Int32, 11, "CantidadDVEN");
            SqlInsertCommand1.Parameters.Add("p_publico", MySqlDbType.Double, 11, "PrecioPublicoDVEN");
            SqlInsertCommand1.Parameters.Add("p_costo", MySqlDbType.Double, 11, "PrecioCostoDVEN");
            SqlInsertCommand1.Parameters.Add("p_mayor", MySqlDbType.Double, 11, "PrecioMayorDVEN");
            SqlInsertCommand1.Parameters.Add("p_forma_pago", MySqlDbType.Int32, 11, "IdFormaPagoDVEN");
            SqlInsertCommand1.Parameters.Add("p_nro_cupon", MySqlDbType.Int32, 11, "NroCuponDVEN");
            SqlInsertCommand1.Parameters.Add("p_nro_factura", MySqlDbType.Int32, 11, "NroFacturaDVEN");
            SqlInsertCommand1.Parameters.Add("p_id_empleado", MySqlDbType.Int32, 11, "IdEmpleadoDVEN");
            SqlInsertCommand1.Parameters.Add("p_liquidado", MySqlDbType.Bit, 11, "LiquidadoDVEN");
            SqlInsertCommand1.Parameters.Add("p_devolucion", MySqlDbType.Bit, 11, "DevolucionDVEN");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            return SqlDataAdapter1;
        }
    }
}
