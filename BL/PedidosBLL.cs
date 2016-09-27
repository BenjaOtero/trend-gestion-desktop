using System.Data;

namespace BL
{
    public class PedidosBLL
    {
        public DataSet dt;
        public DataTable tblColores;

        public static DataSet CrearDataset(string fecha, string genero)
        {
            DataSet dt = DAL.PedidosDAL.CrearDataset(fecha, genero);
            return dt;
        }

    }
}
