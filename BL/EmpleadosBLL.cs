using System.Data;

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