using System.Data;

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
