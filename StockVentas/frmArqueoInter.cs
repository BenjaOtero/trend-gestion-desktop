using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StockVentas
{
    public partial class frmArqueoInter : Form
    {
        DataTable tblLocales;
        DataTable tblPc;
        DataView viewLocales;
        DataView viewPc;
        private int idLocal;
        private string nombreLocal;
        private int idPc;
        private DateTime fecha;

        public frmArqueoInter()
        {
            InitializeComponent();
        }

        private void frmArqueoInter_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            tblLocales = BL.GetDataBLL.Locales();
            viewLocales = new DataView(tblLocales);
            viewLocales.RowFilter = "IdLocalLOC <>'1' AND IdLocalLOC <>'2' AND IdLocalLOC <>'11' AND IdLocalLOC <>'12'";
            lstLocales.ValueMember = "IdLocalLOC";
            lstLocales.DisplayMember = "NombreLOC";
            lstLocales.DataSource = viewLocales;
            tblPc = BL.GetDataBLL.Pc();
            string local = lstLocales.SelectedValue.ToString();
            viewPc = new DataView(tblPc);
            viewPc.RowFilter = "IdLocalPC = '" + local + "'";
            viewPc.Sort = "Detalle ASC";
            lstPc.ValueMember = "IdPC";
            lstPc.DisplayMember = "Detalle";
            lstPc.DataSource = viewPc;
            this.lstLocales.SelectedValueChanged += new System.EventHandler(this.lstLocales_SelectedValueChanged);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            fecha = dateTimePicker1.Value;
            idLocal = Convert.ToInt32(lstLocales.SelectedValue.ToString());
            nombreLocal = lstLocales.Text;
            idPc = Convert.ToInt32(lstPc.SelectedValue.ToString());
            frmProgress frm = new frmProgress(fecha, idLocal, nombreLocal, idPc, "frmArqueoInter", "cargar");
            frm.Show();
        }

        private void lstLocales_SelectedValueChanged(object sender, EventArgs e)
        {
            string local = lstLocales.SelectedValue.ToString();
            viewPc = new DataView(tblPc);
            viewPc.RowFilter = "IdLocalPC = '" + local + "'";
            viewPc.Sort = "Detalle ASC";
            lstPc.ValueMember = "IdPC";
            lstPc.DisplayMember = "Detalle";
            lstPc.DataSource = viewPc;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
