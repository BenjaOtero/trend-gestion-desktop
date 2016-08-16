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
    public class AlicuotasIvaBLL
    {
        public static void GrabarDB(DataTable tblAlicuotasIva, string id, string oldId)
        {
            DAL.AlicuotasIvaDAL.GrabarDB(tblAlicuotasIva, id, oldId);
        }
    }
}

