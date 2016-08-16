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

namespace BL
{
    public class EmpleadosBLL
    {

        public static void GrabarDB(DataTable tblEmpleados)
        {
            DAL.EmpleadosDAL.GrabarDB(tblEmpleados);
        }

        public static DataTable GetLiquidacion()
        {
            DataTable tbl = DAL.EmpleadosDAL.GetLiquidacion();
            return tbl;
        }

    }
}