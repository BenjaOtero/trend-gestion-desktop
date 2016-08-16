using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Collections;

namespace StockVentas
{
    public partial class frmGetDatosCliente : Form
    {
        ArrayList usuario = null;

        public frmGetDatosCliente()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            btnSiguiente.Enabled = false;
        }

        public frmGetDatosCliente(ArrayList usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.usuario = usuario;
            txtNombre.Text = usuario[1].ToString();
            txtApellido.Text = usuario[2].ToString();
            txtCorreo.Text = usuario[3].ToString();
            txtPass.Text = usuario[4].ToString();
            txtPassConfirm.Text = usuario[4].ToString();
        }

        private void frmGetDatosCliente_Load(object sender, EventArgs e)
        {
            System.Drawing.Icon ico = Properties.Resources.icono_app;            
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            txtPass.PasswordChar = '*';
            txtPassConfirm.PasswordChar = '*';
            btnAtras.Enabled = false;           
            txtNombre.Validating += new CancelEventHandler(txtNombre_Validating);
            txtNombre.Validated += new EventHandler(txtNombre_Validated);
            txtApellido.Validating += new CancelEventHandler(txtApellido_Validating);
            txtApellido.Validated += new EventHandler(txtApellido_Validated);
            txtCorreo.Validating += new CancelEventHandler(txtCorreo_Validating);
            txtCorreo.Validated += new EventHandler(txtCorreo_Validated);
            txtPass.Validating += new CancelEventHandler(txtPass_Validating);
            txtPass.Validated += new EventHandler(txtPass_Validated);
            txtPassConfirm.Validating += new CancelEventHandler(txtPassConfirm_Validating);
            txtPassConfirm.Validated += new EventHandler(txtPassConfirm_Validated);
            this.groupBox1.CausesValidation = false;
            this.btnCancelar.CausesValidation = false;
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text)) e.Cancel = true;
            this.errorProvider1.SetError(txtNombre, "Debe indicar un nombre.");
            btnSiguiente.Enabled = false;
        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNombre, "");
            if (ValidarRegistro()) btnSiguiente.Enabled = true;
        }

        private void txtApellido_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApellido.Text)) e.Cancel = true;
            this.errorProvider1.SetError(txtApellido, "Debe indicar un apellido.");
            btnSiguiente.Enabled = false;
        }

        private void txtApellido_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtApellido, "");
            if (ValidarRegistro()) btnSiguiente.Enabled = true;
        }

        private void txtCorreo_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!BL.Utilitarios.ValidEmailAddress(txtCorreo.Text, out errorMsg))
            {
                e.Cancel = true;
                txtCorreo.Select(0, txtCorreo.Text.Length);
                this.errorProvider1.SetError(txtCorreo, errorMsg);
                btnSiguiente.Enabled = false;
            }
        }

        private void txtCorreo_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtCorreo, "");
            if (ValidarRegistro()) btnSiguiente.Enabled = true;
        }

        private void txtPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                e.Cancel = true;
                this.errorProvider1.SetError(txtPass, "Debe indicar una contraseña.");
                btnSiguiente.Enabled = false;
            }
            /*   if (txtPass.Text.Length <8 || txtPass.Text.Length >10)
               {
                   e.Cancel = true;
                   this.errorProvider1.SetError(txtPass, "La contraseña debe entre 8 y 10 caracteres.");
                   btnSiguiente.Enabled = false;
               } */
        }

        private void txtPass_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtPass, "");
            if (ValidarRegistro()) btnSiguiente.Enabled = true;
        }

        private void txtPassConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassConfirm.Text))
            {
                e.Cancel = true;
                this.errorProvider1.SetError(txtPassConfirm, "Debe confirmar la contraseña.");
                btnSiguiente.Enabled = false;
            }
        }

        private void txtPassConfirm_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtPassConfirm, "");
            if (ValidarRegistro())
            {
                btnSiguiente.Enabled = true;
                btnSiguiente.Focus();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            int comparar = String.Compare(txtPass.Text, txtPassConfirm.Text, false);
            if (comparar == 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                Guid guid = Guid.NewGuid();
                MailAddress to = new MailAddress(txtCorreo.Text);
                MailAddress from = new MailAddress("info@trendsistemas.com", "Trend Sistemas");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Clave de producto";
                message.Body = @"Clave: " + guid.ToString();
                SmtpClient client = new SmtpClient("mail.trendsistemas.com", 587);
                client.Credentials = new System.Net.NetworkCredential("info@trendsistemas.com", "8953#AFjn");
                try
                {
                  //  client.Send(message);
                    ArrayList usuario = new ArrayList();
                    usuario.Add(guid.ToString());
                    usuario.Add(txtNombre.Text);
                    usuario.Add(txtApellido.Text);
                    usuario.Add(txtCorreo.Text);
                    usuario.Add(txtPass.Text);                    
                    this.Close();
                    frmGetDatosCliente1 getDatosClientes1 = new frmGetDatosCliente1(usuario);
                    getDatosClientes1.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateTestMessage1(): {0}",
                          ex.ToString());
                }
                finally
                {
                    Cursor.Current = Cursors.Arrow;
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Focus();
                txtPass.Select(0, txtPass.TextLength);
            }
        }

        private bool ValidarRegistro()
        {
            bool validado = false;
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtCorreo.Text)
                    && !string.IsNullOrEmpty(txtPass.Text) && !string.IsNullOrEmpty(txtPassConfirm.Text))
            {
                validado = true;
            }
            return validado;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("La configuración de usuario no se completó. Si cancela no se iniciará la aplicación. ¿Confirma cancelar?", 
                "Trend Gestión", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }
}
