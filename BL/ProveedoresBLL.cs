using System.Data;

namespace BL
{
    public class ProveedoresBLL
    {
        public static void GrabarDB(DataTable tblProveedores)
        {
            DAL.ProveedoresDAL.GrabarDB(tblProveedores);
        }
    }
}
