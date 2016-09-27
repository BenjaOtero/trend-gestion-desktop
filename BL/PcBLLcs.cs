﻿using System.Data;
using System.Windows.Forms;

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
