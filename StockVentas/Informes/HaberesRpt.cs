using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class HaberesRpt : Form
    {
        public HaberesRpt()
        {
            InitializeComponent();
        }

        private void HaberesRpt_Load(object sender, EventArgs e)
        {
            DataTable tlbHaberes = BL.EmpleadosBLL.GetLiquidacion();
        }
    }
}
