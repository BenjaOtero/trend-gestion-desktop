using System.Data;

namespace BL
{
    public class GenerosBLL
    {

        public static void GrabarDB(DataTable tblGeneros)
        {
            DAL.GenerosDAL.GrabarDB(tblGeneros);
        }

    }
}
