using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BL
{
    public static class GetDataBLL
    {

        public static DataSet GetData()
        {
            DataSet datos = DAL.GetDataDAL.GetData();
            return datos;
        }

        public static DataTable GetArticulos()
        {
            DataTable datos = DAL.GetDataDAL.GetArticulos();
            return datos;
        }

        public static DataTable Articulos()
        {
            DataTable tblArticulos = DAL.GetDataDAL.Articulos();
            return tblArticulos;
        }

        public static DataTable ArticulosItems()
        {
            DataTable tblArticulosItems = DAL.GetDataDAL.ArticulosItems();
            return tblArticulosItems;
        }

        public static DataTable Clientes()
        {
            DataTable tblClientes = DAL.GetDataDAL.Clientes();
            return tblClientes;
        }

        public static DataTable Colores()
        {
            DataTable tblColores = DAL.GetDataDAL.Colores();
            return tblColores;
        }

        public static DataTable FormasPago()
        {
            DataTable tblFormasPago = DAL.GetDataDAL.FormasPago();
            return tblFormasPago;
        }

        public static DataTable Locales()
        {
            DataTable tblLocales = DAL.GetDataDAL.Locales();
            return tblLocales;
        }

        public static DataTable Pc()
        {
            DataTable tblPc = DAL.GetDataDAL.Pc();
            return tblPc;
        }

        public static DataTable Proveedores()
        {
            DataTable tblProveedores = DAL.GetDataDAL.Proveedores();
            return tblProveedores;
        }

        public static DataTable Generos()
        {
            DataTable tblGeneros = DAL.GetDataDAL.Generos();
            return tblGeneros;
        }

        public static DataTable AlicuotasIva()
        {
            DataTable tblAlicuotas = DAL.GetDataDAL.AlicuotasIva();
            return tblAlicuotas;
        }

        public static DataTable CondicionIva()
        {
            DataTable tblCondicionIva = DAL.GetDataDAL.CondicionIva();
            return tblCondicionIva;
        }

        public static DataTable RazonSocial()
        {
            DataTable tblRazonSocial = DAL.GetDataDAL.RazonSocial();
            return tblRazonSocial;
        }

        public static DataTable Empleados()
        {
            DataTable tblEmpleados = DAL.GetDataDAL.Empleados();
            return tblEmpleados;
        }

        public static DataTable EmpleadosMovTipos()
        {
            DataTable tblEmpleadosMovTipos = DAL.GetDataDAL.EmpleadosMovTipos();
            return tblEmpleadosMovTipos;
        }
    }
}
