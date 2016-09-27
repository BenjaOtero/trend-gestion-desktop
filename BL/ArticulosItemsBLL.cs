using System.Data;

namespace BL
{
    public class ArticulosItemsBLL
    {     
   
        public static void GrabarDB(DataTable tblArticulosItems)
        {
            DAL.ArticulosItemsDAL.GrabarDB(tblArticulosItems);
        }
    
    }
}
