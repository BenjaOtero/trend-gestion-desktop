using System.Data;

namespace BL
{
    public class ColoresBLL
    {
        public static void GrabarDB(DataTable tblColores)
        {
            DAL.ColoresDAL.GrabarDB(tblColores);
        }
    }
}
