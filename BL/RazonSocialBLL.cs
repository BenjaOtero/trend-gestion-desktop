using System.Data;

namespace BL
{
    public class RazonSocialBLL
    {
        public static void GrabarDB(DataTable tblRazonSocial)
        {
            DAL.RazonSocialDAL.GrabarDB(tblRazonSocial);
        }

        public static bool GetActualizarDatos()
        {
            bool actualizar = DAL.RazonSocialDAL.GetActualizarDatos();
            return actualizar;
        }

        public static void SetActualizarDatos()
        {
            DAL.RazonSocialDAL.SetActualizarDatos();
        }
    }
}
