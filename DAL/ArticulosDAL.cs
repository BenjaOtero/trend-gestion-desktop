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
    public class ArticulosDAL
    {
        public static DataSet dt;
      
        public static void InsertarDT(DataTable tabla, Entities.Articulos entidad)
        {
            DataRowCollection cfilas = tabla.Rows;
            DataRow nuevaFila;
            try
            {
                nuevaFila = tabla.NewRow();
                nuevaFila[0] = entidad.IdArticulo.ToString();
                nuevaFila[1] = entidad.IdItem.ToString();
                nuevaFila[2] = entidad.IdGenero.ToString();
                nuevaFila[3] = entidad.IdColor;
                nuevaFila[4] = entidad.Talle.ToString();
                nuevaFila[5] = entidad.IdProveedor.ToString();
                nuevaFila[6] = entidad.Descripcion.ToString();
                nuevaFila[7] = entidad.DescripcionWeb.ToString();
                nuevaFila[8] = entidad.PrecioCosto.ToString();
                nuevaFila[9] = entidad.PrecioPublico.ToString();
                nuevaFila[10] = entidad.PrecioMayor.ToString();
                nuevaFila[11] = entidad.Fecha;
                nuevaFila[12] = entidad.Imagen.ToString();
                nuevaFila[13] = entidad.ImagenBack.ToString();
                nuevaFila[14] = entidad.ImagenColor.ToString();
              //  nuevaFila[15] = entidad.ActivoWeb;
                nuevaFila[16] = entidad.NuevoWeb;
                nuevaFila[17] = entidad.Proveedor;
                nuevaFila[18] = entidad.IdAlicuota;
                cfilas.Add(nuevaFila);
            }
            catch (ConstraintException)
            {
                MessageBox.Show("El artículo '" + entidad.IdArticulo.ToString() + " " + entidad.Descripcion.ToString() + "' ya existe. No se creó el artículo.", 
                    "NcSoft",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);                
            }
        }

        public static DataTable GetArticulosStock()
        {
            DataTable tbl = new DataTable();
            MySqlDataAdapter da = AdaptadorGetArticulosBorrar();
            da.Fill(tbl);
            return tbl;
        }

        private static MySqlDataAdapter AdaptadorGetArticulosBorrar()
        {
            MySqlConnection SqlConnection1;
            MySqlDataAdapter SqlDataAdapter1;
            MySqlCommand SqlSelectCommand1;
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("Articulos_BorrarCons", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            SqlConnection1.Close();
            return SqlDataAdapter1;
        }

        public static void GrabarDB(DataTable tblArticulos)
        {
            MySqlDataAdapter da = AdaptadorABM();
            da.Update(tblArticulos);
        }

        private static MySqlDataAdapter AdaptadorABM()
        {
            MySqlConnection SqlConnection1 = DAL.DALBase.GetConnection();
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlInsertCommand1 = new MySqlCommand("Articulos_Insertar", SqlConnection1);
            MySqlCommand SqlUpdateCommand1 = new MySqlCommand("Articulos_Actualizar", SqlConnection1);
            MySqlCommand SqlDeleteCommand1 = new MySqlCommand("Articulos_Borrar", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDataAdapter1.InsertCommand = SqlInsertCommand1;
            SqlDataAdapter1.UpdateCommand = SqlUpdateCommand1;

            // IMPLEMENTACIÓN DE LA ORDEN UPDATE
            SqlUpdateCommand1.Parameters.Add("p_id", MySqlDbType.VarChar, 55, "IdArticuloART");
            SqlUpdateCommand1.Parameters.Add("p_idItem", MySqlDbType.Int32, 11, "IdItemART");
            SqlUpdateCommand1.Parameters.Add("p_idGenero", MySqlDbType.Int32, 11, "IdGeneroART");
            SqlUpdateCommand1.Parameters.Add("p_idColor", MySqlDbType.Int32, 11, "IdColorART");
            SqlUpdateCommand1.Parameters.Add("p_idAlicuota", MySqlDbType.Int16, 3, "IdAliculotaIvaART");
            SqlUpdateCommand1.Parameters.Add("p_talle", MySqlDbType.VarChar, 50, "TalleART");
            SqlUpdateCommand1.Parameters.Add("p_idProveedor", MySqlDbType.Int32, 11, "IdProveedorART");
            SqlUpdateCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 55, "DescripcionART");
            SqlUpdateCommand1.Parameters.Add("p_descripcionWeb", MySqlDbType.VarChar, 50, "DescripcionWebART");
            SqlUpdateCommand1.Parameters.Add("p_precioCosto", MySqlDbType.String, 19, "PrecioCostoART");
            SqlUpdateCommand1.Parameters.Add("p_precioPublico", MySqlDbType.String, 19, "PrecioPublicoART");
            SqlUpdateCommand1.Parameters.Add("p_precioMayor", MySqlDbType.String, 19, "PrecioMayorART");
            SqlUpdateCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 19, "FechaART");
            SqlUpdateCommand1.Parameters.Add("p_imagen", MySqlDbType.VarChar, 50, "ImagenART");
            SqlUpdateCommand1.Parameters.Add("p_imagenBack", MySqlDbType.VarChar, 50, "ImagenBackART");
            SqlUpdateCommand1.Parameters.Add("p_imagenColor", MySqlDbType.VarChar, 50, "ImagenColorART");
            SqlUpdateCommand1.Parameters.Add("p_activoWeb", MySqlDbType.Int32, 1, "ActivoWebART");
            SqlUpdateCommand1.Parameters.Add("p_nuevo", MySqlDbType.Int32, 1, "NuevoART");
            SqlUpdateCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN INSERT
            SqlInsertCommand1.Parameters.Add("p_id", MySqlDbType.VarChar, 55, "IdArticuloART");
            SqlInsertCommand1.Parameters.Add("p_idItem", MySqlDbType.Int32, 11, "IdItemART");
            SqlInsertCommand1.Parameters.Add("p_idGenero", MySqlDbType.Int32, 11, "IdGeneroART");
            SqlInsertCommand1.Parameters.Add("p_idColor", MySqlDbType.Int32, 11, "IdColorART");
            SqlInsertCommand1.Parameters.Add("p_idAlicuota", MySqlDbType.Int16, 3, "IdAliculotaIvaART");
            SqlInsertCommand1.Parameters.Add("p_talle", MySqlDbType.VarChar, 50, "TalleART");
            SqlInsertCommand1.Parameters.Add("p_idProveedor", MySqlDbType.Int32, 11, "IdProveedorART");
            SqlInsertCommand1.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 55, "DescripcionART");
            SqlInsertCommand1.Parameters.Add("p_descripcionWeb", MySqlDbType.VarChar, 50, "DescripcionWebART");
            SqlInsertCommand1.Parameters.Add("p_precioCosto", MySqlDbType.String, 19, "PrecioCostoART");
            SqlInsertCommand1.Parameters.Add("p_precioPublico", MySqlDbType.String, 19, "PrecioPublicoART");
            SqlInsertCommand1.Parameters.Add("p_precioMayor", MySqlDbType.String, 19, "PrecioMayorART");
            SqlInsertCommand1.Parameters.Add("p_fecha", MySqlDbType.DateTime, 19, "FechaART");
            SqlInsertCommand1.Parameters.Add("p_imagen", MySqlDbType.VarChar, 50, "ImagenART");
            SqlInsertCommand1.Parameters.Add("p_imagenBack", MySqlDbType.VarChar, 50, "ImagenBackART");
            SqlInsertCommand1.Parameters.Add("p_imagenColor", MySqlDbType.VarChar, 50, "ImagenColorART");
            SqlInsertCommand1.Parameters.Add("p_activoWeb", MySqlDbType.Int32, 1, "ActivoWebART");
            SqlInsertCommand1.Parameters.Add("p_nuevo", MySqlDbType.Int32, 1, "NuevoART");
            SqlInsertCommand1.CommandType = CommandType.StoredProcedure;

            // IMPLEMENTACIÓN DE LA ORDEN DELETE
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.VarChar, 55, "IdArticuloART");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;

            return SqlDataAdapter1;
        }

        public static void BorrarArticulosAgrupar(DataTable tblArticulos, MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter da = AdaptadorBorrarArticulos(conn, tr);
            da.Update(tblArticulos);
        }

        private static MySqlDataAdapter AdaptadorBorrarArticulos(MySqlConnection conn, MySqlTransaction tr)
        {
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlDeleteCommand1 = new MySqlCommand("Articulos_Borrar", conn);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;
            SqlDeleteCommand1.Transaction = tr;
            SqlDeleteCommand1.Parameters.Add("p_id", MySqlDbType.VarChar, 55, "IdArticuloART");
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            return SqlDataAdapter1;
        }

        public static void ActualizarPrecios(string id, decimal precio)
        {
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            MySqlDataAdapter SqlDataAdapter1 = new MySqlDataAdapter();
            MySqlCommand SqlDeleteCommand1 = new MySqlCommand("Articulos_Actualizar_Precio", SqlConnection1);
            SqlDataAdapter1.DeleteCommand = SqlDeleteCommand1;

            SqlDeleteCommand1.Parameters.AddWithValue("p_id", id);
            SqlDeleteCommand1.Parameters.AddWithValue("p_precioPublico", precio);
            SqlDeleteCommand1.CommandType = CommandType.StoredProcedure;
            SqlDeleteCommand1.ExecuteNonQuery();
            SqlConnection1.Close();
        }

        public static void ActualizarArticulos(string id, string oldId)
        {
            string stringCommand;
            stringCommand = "UPDATE articulos SET IdArticuloART = '" + id + "' WHERE IdArticuloART = '" + oldId + "'";
          //  UPDATE articulos SET IdArticuloART = '0030031803' WHERE IdArticuloART = '030031803';
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            MySqlCommand command = new MySqlCommand(stringCommand, SqlConnection1);
            command.ExecuteNonQuery();
            SqlConnection1.Close();
        }
    }

}
