using System.Data;

namespace BL
{
    public class StockBLL
    {

        public static DataTable GetStock()
        {
            DataTable tbl;
            tbl = DAL.StockDAL.GetStock();
            return tbl;
        }

        public static DataSet CrearDataset(string whereLocales, string genero, int proveedor, string articulo, string descripcion, int activoWeb)
        {
            DataSet dt = new DataSet();          
            dt = DAL.StockDAL.CrearDataset(whereLocales, genero, proveedor, articulo, descripcion, activoWeb);            
            return dt;
        }

    }
}
