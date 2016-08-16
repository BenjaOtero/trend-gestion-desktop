using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;
using DAL;

namespace BL
{
    public class UsuariosBLL
    {
        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.UsuariosDAL.GetTabla();
            return tbl;
        }


        public static void GrabarDB(DataTable tblUsuarios)
        {
            try
            {
                DAL.ColoresDAL.GrabarDB(tblUsuarios);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString(), "NcSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tblUsuarios.RejectChanges();
            }
        }
    }
}
