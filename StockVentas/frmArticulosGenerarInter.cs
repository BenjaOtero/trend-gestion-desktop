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
    public partial class frmArticulosGenerarInter : Form
    {
        private BindingSource bindingSource;

        public frmArticulosGenerarInter(BindingSource bindingSource)
        {
            InitializeComponent();
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.Text = "  Generar artículos";
            this.ControlBox = false;
            this.bindingSource = bindingSource;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rdNuevo.Checked == true)
            {
                frmArticulosGenerar articulosGenerar = new frmArticulosGenerar();
                articulosGenerar.ShowDialog();
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                frmArticulosGenerarApartir articulosGenerarApartir = new frmArticulosGenerarApartir(bindingSource);
                articulosGenerarApartir.ShowDialog();
                Cursor.Current = Cursors.Arrow;
            }
        }

    }
}
