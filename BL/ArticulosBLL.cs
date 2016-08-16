using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;
using DAL;
using Entities;

namespace BL
{
    public class ArticulosBLL
    {
        public static bool actualizar = false;

        public static void InsertarDT(DataTable tabla, Articulos entidad)
        {
            DAL.ArticulosDAL.InsertarDT(tabla, entidad);
        }

        public static void GrabarDB(DataTable tblArticulos)
        {
            DAL.ArticulosDAL.GrabarDB(tblArticulos);
        }

        public static void ActualizarPrecios(string id, decimal precio)
        {
            DAL.ArticulosDAL.ActualizarPrecios(id, precio);
        }

        public static DataTable GetArticulosStock()
        {
            DataTable tbl;
            tbl = DAL.ArticulosDAL.GetArticulosStock();
            return tbl;
        }

        public static void ActualizarArticulos(string id, string oldId)
        {
            DAL.ArticulosDAL.ActualizarArticulos(id, oldId);
        }
    }
}
