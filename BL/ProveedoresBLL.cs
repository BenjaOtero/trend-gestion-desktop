using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL;

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
