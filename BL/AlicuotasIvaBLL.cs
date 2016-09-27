using System.Data;

namespace BL
{
    public class AlicuotasIvaBLL
    {
        public static void GrabarDB(DataTable tblAlicuotasIva, string id, string oldId)
        {
            DAL.AlicuotasIvaDAL.GrabarDB(tblAlicuotasIva, id, oldId);
        }
    }
}

