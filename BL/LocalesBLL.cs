using System.Data;

namespace BL
{
    public class LocalesBLL
    {
        public static void GrabarDB(DataTable tblLocales)
        {
            DAL.LocalesDAL.GrabarDB(tblLocales);
        }
    }
}
