using System.Data;

namespace BL
{
    public class EmpleadosMovTiposBLL
    {

        public static void GrabarDB(DataTable tblEmpleadosMovTipos)
        {
            DAL.EmpleadosMovTiposDAL.GrabarDB(tblEmpleadosMovTipos);
        }

    }
}
