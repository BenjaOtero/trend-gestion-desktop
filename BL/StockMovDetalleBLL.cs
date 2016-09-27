using System.Data;

namespace BL
{
    public class StockMovDetalleBLL
    {
        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.StockMovDetalleDAL.GetTabla();
            return tbl;
        }

    }
}
