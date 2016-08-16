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
    public class StockBLL
    {

        public static DataTable GetStock()
        {
            DataTable tbl;
            tbl = DAL.StockDAL.GetStock();
            return tbl;
        }

        public static DataSet CrearDataset(string whereLocales, string genero, int proveedor, string articulo, string descripcion, int activoWeb)
        {
            DataSet dt = new DataSet();          
            dt = DAL.StockDAL.CrearDataset(whereLocales, genero, proveedor, articulo, descripcion, activoWeb);            
            return dt;
        }

    }
}
