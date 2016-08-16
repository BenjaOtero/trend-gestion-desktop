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
    public class StockMovDetalleBLL
    {
        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.StockMovDetalleDAL.GetTabla();
            return tbl;
        }

    }
}
