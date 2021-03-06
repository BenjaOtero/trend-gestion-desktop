﻿using System.Data;

namespace BL
{
    public class TrendBLL
    {
        public static DataSet GetDataPopup(int razon)
        {
            DataSet dt = DAL.TrendDAL.GetDataPopup(razon);
            return dt;
        }

        public static DataTable GetTablaCliente()
        {
            DataTable tbl = DAL.TrendDAL.GetTablaCliente();
            return tbl;
        }

        public static DataTable GetTablaProductosCliente()
        {
            DataTable tbl = DAL.TrendDAL.GetTablaProductosCliente();
            return tbl;
        }

        public static void GrabarDB(DataSet dsAlta)
        {
            DataTable tblClientes = dsAlta.Tables[0];
            DataTable tblProductosClientes = dsAlta.Tables[1];
            DAL.TrendDAL.GrabarDB(tblClientes, tblProductosClientes);
        }
    }
}
