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
    public class PedidosBLL
    {
        public DataSet dt;
        public DataTable tblColores;

        public static DataSet CrearDataset(string fecha, string genero)
        {
            DataSet dt = DAL.PedidosDAL.CrearDataset(fecha, genero);
            return dt;
        }

    }
}
