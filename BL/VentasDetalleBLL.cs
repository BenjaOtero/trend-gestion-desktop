using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using DAL;

namespace BL
{
    public class VentasDetalleBLL
    {
        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.VentasDetalleDAL.GetTabla();
            return tbl;
        }

        public static void GrabarDB(DataSet dt)
        {
            MySqlTransaction tr = null;
            try
            {
                MySqlConnection SqlConnection1 = DALBase.GetConnection();
                
                tr = SqlConnection1.BeginTransaction();
                DAL.VentasDetalleDAL.GrabarDB(dt, SqlConnection1, tr);
            //    string strFilePath = Application.StartupPath + "\\Datasets_xml\\";
                string strFilePath = Application.StartupPath + "\\Datasets_xml\\";
                dt.WriteXml(strFilePath + "Ventas.xml", XmlWriteMode.WriteSchema);
                tr.Commit();
                SqlConnection1.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString(), "NcSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dt.RejectChanges();
                tr.Rollback();
            }
        }
    }
}
