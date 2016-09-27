using System.Data;

namespace BL
{
    public class CondicionIvaBLL
    {
        public static void GrabarDB(DataTable tblCondicionIva)
        {
            DAL.CondicionIvaDAL.GrabarDB(tblCondicionIva);
        }
    }
}
