using System.Data;

namespace BL
{
    public class EmpleadosMovBLL
    {

        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.EmpleadosMovDAL.GetTabla();
            return tbl;
        }

        public static void GrabarDB(DataTable tblEmpleadosMov)
        {
            DAL.EmpleadosMovDAL.GrabarDB(tblEmpleadosMov);
        }

        public static DataTable EmpleadosMovCons(string fechaDesde, string fechaHasta, int idEmpleado, int liquidado)
        {
            DataTable tbl = DAL.EmpleadosMovDAL.EmpleadosMovCons(fechaDesde, fechaHasta, idEmpleado, liquidado);
            return tbl;
        }

    }
}

