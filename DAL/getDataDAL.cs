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
    public static class GetDataDAL
    {
        public static DataSet datos;

        public static DataSet GetData()
        {
            MySqlConnection SqlConnection1;
            MySqlDataAdapter SqlDataAdapter1;
            MySqlCommand SqlSelectCommand1;
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("DatosServer_Listar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            datos = new DataSet();
            SqlDataAdapter1.Fill(datos);
            SqlConnection1.Close();
            return datos;
        }

        public static DataTable GetArticulos()
        {
            MySqlConnection SqlConnection1;
            MySqlDataAdapter SqlDataAdapter1;
            MySqlCommand SqlSelectCommand1;
            SqlConnection1 = DALBase.GetConnection();
            SqlDataAdapter1 = new MySqlDataAdapter();
            SqlSelectCommand1 = new MySqlCommand("DatosServer_Listar", SqlConnection1);
            SqlDataAdapter1.SelectCommand = SqlSelectCommand1;
            SqlSelectCommand1.CommandType = CommandType.StoredProcedure;
            DataTable datos = new DataTable();
            SqlDataAdapter1.Fill(datos);
            SqlConnection1.Close();
            return datos;
        }

        public static DataTable Articulos()
        {
            DataTable tblArticulos = datos.Tables[0];
            tblArticulos.TableName = "Articulos";        
            return tblArticulos;
        }

        public static DataTable ArticulosItems()
        {
            DataTable tblArticulosItems = datos.Tables[1];
            tblArticulosItems.TableName = "ArticulosItems";
            if (!tblArticulosItems.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblArticulosItems.Columns["DescripcionITE"]);
                tblArticulosItems.Constraints.Add(uniqueConstraint);
            }
            return tblArticulosItems;
        }

        public static DataTable Clientes()
        {
            DataTable tblClientes = datos.Tables[2];
            tblClientes.TableName = "Clientes";
            if (!tblClientes.Constraints.Contains("idConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("idConstraint", tblClientes.Columns["CorreoCLI"]);
                tblClientes.Constraints.Add(uniqueConstraint);
            }
            return tblClientes;
        }

        public static DataTable Colores()
        {
            DataTable tblColores = datos.Tables[3];
            tblColores.TableName = "Colores";
            if (!tblColores.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblColores.Columns["DescripcionCOL"]);
                tblColores.Constraints.Add(uniqueConstraint);
            }
            return tblColores;
        }

        public static DataTable FormasPago()
        {
            DataTable tblFormasPago = datos.Tables[4];
            tblFormasPago.TableName = "FormasPago";
            if (!tblFormasPago.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblFormasPago.Columns["DescripcionFOR"]);
                tblFormasPago.Constraints.Add(uniqueConstraint);
            }
            return tblFormasPago;
        }

        public static DataTable Locales()
        {
            DataTable tblLocales = datos.Tables[5];
            tblLocales.TableName = "Locales";
            if (!tblLocales.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblLocales.Columns["NombreLOC"]);
                tblLocales.Constraints.Add(uniqueConstraint);
            }
            return tblLocales;
        }

        public static DataTable Pc()
        {
            DataTable tblPc = datos.Tables[6];
            tblPc.TableName = "Pc";
            return tblPc;
        }

        public static DataTable Proveedores()
        {
            DataTable tblProveedores = datos.Tables[7];
            tblProveedores.TableName = "Proveedores";
            if (!tblProveedores.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblProveedores.Columns["RazonSocialPRO"]);
                tblProveedores.Constraints.Add(uniqueConstraint);
            }
            return tblProveedores;
        }

        public static DataTable Generos()
        {
            DataTable tblGeneros = datos.Tables[8];
            tblGeneros.TableName = "generos";
            if (!tblGeneros.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblGeneros.Columns["DescripcionGEN"]);
                tblGeneros.Constraints.Add(uniqueConstraint);
            }
            return tblGeneros;
        }

        public static DataTable AlicuotasIva()
        {
            DataTable tblAlicuotasIva = datos.Tables[9];
            tblAlicuotasIva.TableName = "alicuotasiva";
            if (!tblAlicuotasIva.Constraints.Contains("idConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("idConstraint", tblAlicuotasIva.Columns["IdAlicuotaALI"]);
                tblAlicuotasIva.Constraints.Add(uniqueConstraint);
            }
            if (!tblAlicuotasIva.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblAlicuotasIva.Columns["PorcentajeALI"]);
                tblAlicuotasIva.Constraints.Add(uniqueConstraint);
            }
            return tblAlicuotasIva;
        }

        public static DataTable CondicionIva()
        {
            DataTable tblCondicionIva = datos.Tables[10];
            tblCondicionIva.TableName = "condicioniva";
            if (!tblCondicionIva.Constraints.Contains("idConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("idConstraint", tblCondicionIva.Columns["IdCondicionIvaCIVA"]);
                tblCondicionIva.Constraints.Add(uniqueConstraint);
            }
            if (!tblCondicionIva.Constraints.Contains("descripcionConstraint"))
            {
                UniqueConstraint uniqueConstraint = new UniqueConstraint("descripcionConstraint", tblCondicionIva.Columns["DescripcionCIVA"]);
                tblCondicionIva.Constraints.Add(uniqueConstraint);
            }
            return tblCondicionIva;
        }

        public static DataTable RazonSocial()
        {
            DataTable tblRazonSocial = datos.Tables[11];
            tblRazonSocial.TableName = "razonsocial";
            return tblRazonSocial;
        }

        public static DataTable Empleados()
        {
            DataTable tblEmpleados = datos.Tables[12];
            tblEmpleados.TableName = "empleados";
            return tblEmpleados;
        }

        public static DataTable EmpleadosMovTipos()
        {
            DataTable tblEmpleadosMovTipos = datos.Tables[13];
            tblEmpleadosMovTipos.TableName = "empleadosmovtipos";
            return tblEmpleadosMovTipos;
        }

    }
}
