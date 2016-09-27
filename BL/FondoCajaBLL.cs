using System.Data;

namespace BL
{
    public class FondoCajaBLL
    {
        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.FondoCajaDAL.GetTabla();
            return tbl;
        }

        public static void GrabarDB(DataTable tbl)
        {
            DAL.FondoCajaDAL.GrabarDB(tbl);
        }

        public static DataSet CrearDatasetCons()
        {
            DataSet dt = DAL.FondoCajaDAL.CrearDatasetCons();
            return dt;
        }

        public static void BorrarByPK(int PK)
        {
            DAL.FondoCajaDAL.BorrarByPK(PK);
        }

    }
}
