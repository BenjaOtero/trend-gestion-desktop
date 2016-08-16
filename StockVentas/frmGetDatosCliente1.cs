using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using BL;

namespace StockVentas
{
    public partial class frmGetDatosCliente1 : Form
    {
        ArrayList usuario;
        string correo;
        string clave;
        string mensaje;
        ErrorProvider errorProvider1 = new ErrorProvider();

        public frmGetDatosCliente1(ArrayList usuario)
        {
            InitializeComponent();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.TopMost = true;
            this.usuario = usuario;
            correo = usuario[3].ToString();
            clave = usuario[0].ToString();
            mensaje = "Se envío un e-mail a \'" + correo + "\' con la clave \n" + "del producto.\n" +
                        "Copiela y peguela en 'Clave del producto'. Si no encuentra el e-mail\n" +
                        "verifique el correo no deseado y configurelo como \'correo deseado\'.";
            label1.Text = mensaje;
            txtClave.Text = clave;
            txtClave.Validating += new CancelEventHandler(txtClave_Validating);
            txtClave.Validated +=new EventHandler(txtClave_Validated);
        }

        private void txtClave_Validating(object sender, CancelEventArgs e)
        {
            int comparar = String.Compare(txtClave.Text, clave, false);
            if (comparar != 0)
            {
                this.errorProvider1.SetError(txtClave, "La clave es invalida.");
                e.Cancel = true;
            }
        }

        private void txtClave_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtClave, "");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("La configuración de usuario no se completó. Si cancela no se iniciará la aplicación. ¿Confirma cancelar?",
                "Trend Gestión", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
            frmGetDatosCliente frm = new frmGetDatosCliente(usuario);
            frm.Show();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            DataTable tblClientes = BL.TrendBLL.GetTablaCliente();
            tblClientes.TableName = "clientes";
            DataRow row = tblClientes.NewRow();
            row["nombre"] = usuario[1].ToString();
            row["apellido"] = usuario[2].ToString();
            row["correo"] = usuario[3].ToString();
            row["clave"] = usuario[4].ToString();
            tblClientes.Rows.Add(row);

            DataTable tblProductosClientes = BL.TrendBLL.GetTablaProductosCliente();
            tblProductosClientes.TableName = "productos_clientes";
            DataRow rowProducto = tblProductosClientes.NewRow();
            rowProducto["fecha_alta"] = DateTime.Today.ToString("yyyy-MM-dd");
            rowProducto["correo_cliente"] = usuario[3].ToString();
            rowProducto["id_producto"] = 1; // 1 es Gestion
            rowProducto["clave_producto"] = usuario[0].ToString();
            tblProductosClientes.Rows.Add(rowProducto);

            DataSet dsAlta = new DataSet();
            dsAlta.Tables.Add(tblClientes);
            dsAlta.Tables.Add(tblProductosClientes);


            frmProgress1 frm = new frmProgress1(dsAlta, "frmGetDatosCliente1", "grabar", correo);
            frm.ShowDialog();

        }
    }
}
