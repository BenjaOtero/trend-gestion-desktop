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
    public class TesoreriaMovimientosBLL
    {
        public DataSet dt;
        public DataTable tblColores;

        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.TesoreriaMovimientosDAL.GetTabla();
            return tbl;
        }

        public static void GrabarDB(DataSet dt)
        {
            MySqlTransaction tr = null;
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            tr = SqlConnection1.BeginTransaction();
            DAL.TesoreriaMovimientosDAL.GrabarDB(dt, SqlConnection1, tr);
            tr.Commit();
            SqlConnection1.Close();
            dt.AcceptChanges();
        }

        public static void BorrarByPK(int PK)
        {
            DAL.TesoreriaMovimientosDAL.BorrarByPK(PK);
        }
    }
}
