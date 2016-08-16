using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;
using DAL;
using Entities;

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

        public static void ActualizarDatos()
        {
            DAL.RazonSocialDAL.ActualizarDatos();
        }
    }
}
