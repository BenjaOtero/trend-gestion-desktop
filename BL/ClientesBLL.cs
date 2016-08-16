using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using DAL;
using System.Threading;

namespace BL
{
    public class ClientesBLL
    {
        private static object _sync = new object();

        public static DataSet GetClientes(sbyte frm)
        {
            DataSet dt = DAL.ClientesDAL.GetClientes(frm);
            return dt;
        }

        public static void GrabarDB(DataTable tbl)
        {
            DAL.ClientesDAL.GrabarDB(tbl);
        }

    }
}
