using System.Data;

namespace BL
{
    public class FormasPagoBLL
    {
        public static void GrabarDB(DataTable tblFormasPago)
        {
            DAL.FormasPagoDAL.GrabarDB(tblFormasPago);
        }
    }
}
