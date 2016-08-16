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
using Entities;

namespace BL
{
    public class PcBLL
    {
        public DataSet dt;

        public static void CrearDataset()
        {
          //  string strFilePath = Application.StartupPath + "\\Datasets_xml\\";
            string strFilePath = Application.StartupPath + "\\Datasets_xml\\";
            DataSet dt = DAL.PcDAL.CrearDataset();
            dt.Tables[0].WriteXml(strFilePath + "Pc.xml", XmlWriteMode.WriteSchema);
        }
    }
}
