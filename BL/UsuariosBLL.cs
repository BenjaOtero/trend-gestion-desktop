using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

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
