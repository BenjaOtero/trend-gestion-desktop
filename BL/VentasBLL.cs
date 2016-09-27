using System.Data;

namespace BL
{
    public class VentasBLL
    {
        public static DataSet dt;
        public DataTable tblVentas;
        public DataTable tblVentasDetalle;

        public static DataTable GetTablaVentas()
        {
            DataTable tbl = DAL.VentasDAL.GetTablaVentas();
            return tbl;
        }

        public static DataTable GetTablaDetalle()
        {
            DataTable tbl = DAL.VentasDAL.GetTablaDetalle();
            return tbl;
        }

        public static void GrabarVentas(DataSet dsVentas)
        {
            DAL.VentasDAL.GrabarVentas(dsVentas);
        }

        public static DataSet CrearDatasetArqueo(string fechaDesde, string fechaHasta, int pc)
        {            
            dt = DAL.VentasDAL.CrearDatasetArqueo(fechaDesde, fechaHasta, pc);
            dt.Tables[0].TableName = "Ventas";
            dt.Tables[1].TableName = "VentasDetalle";
            dt.Tables[4].TableName = "TesoreriaMovimientos";
            return dt;
        }

        public static DataSet CrearDatasetVentasPesos(int forma, string desde, string hasta, string locales, string genero)
        {
            dt = DAL.VentasDAL.CrearDatasetVentasPesos(forma, desde, hasta, locales, genero);
            return dt;
        }

        public static DataTable GetVentasPesosDiarias(string desde, string hasta, int local, string forma)
        {
            DataTable tbl = DAL.VentasDAL.GetVentasPesosDiarias(desde, hasta, local, forma);
            return tbl;
        }

        public static DataTable GetVentasDetalle(int forma, string desde, string hasta, int idLocal, string parametros)
        {
            DataTable tbl = DAL.VentasDAL.GetVentasDetalle(forma, desde, hasta, idLocal, parametros);
            return tbl;
        }

        public static void BorrarByPK(int PK)
        {
            DAL.VentasDAL.BorrarByPK(PK);
        }

        public static void VentasHistoricasMantener()
        {
            DAL.VentasDAL.VentasHistoricasMantener();
        }

    }
}

