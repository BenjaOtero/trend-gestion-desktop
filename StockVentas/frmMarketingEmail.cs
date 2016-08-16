using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Timers;
using System.Threading;

namespace StockVentas
{
    public partial class frmMarketingEmail : Form
    {
        string titulo;
        string strFileName;
        private static System.Timers.Timer timerInicializar;

        public frmMarketingEmail()
        {
            InitializeComponent();
            this.Location = new Point(50, 50);
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFilDlg = new OpenFileDialog();
            opFilDlg.Filter = "JPG (*.jpg)|*.jpg";
            if (opFilDlg.ShowDialog() == DialogResult.OK)
            {
                strFileName = opFilDlg.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(strFileName);

            }
        }

        private void EnviarCorreo(string mailTo)
        {
            MailAddress to = new MailAddress(mailTo);
            MailAddress from = new MailAddress("info@karminna.com", "Karminna");
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = titulo;
          //  mail.Subject = "20% de descuento en todos nuestros productos.";
            AlternateView plainView = AlternateView.CreateAlternateViewFromString("", null, "text/plain");

            //then we create the Html part
            //to embed images, we need to use the prefix 'cid' in the img src value
            //the cid value will map to the Content-Id of a Linked resource.
            //thus <img src='cid:companylogo'> will map to a LinkedResource with a ContentId of 'companylogo'
            string html = "<div align='center'>";
            html += "<a href='http://karminna.com'>";
            html += "<img src=cid:companylogo>";
            html += "</a><br><br>";
            html += "<a href='http://karminna.com'>Visitá nuestro sitio web</a>";
            html += "<br><br>";
            html += "<p>Para no recibir mas correos de KARMINNA hacé click ";
            html += "<a href='http://karminna.com?action=unsuscribeNews&correo=" + mailTo + "'>aquí</a></p>";
            html += "</div>";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, null, "text/html");

            //create the LinkedResource (embedded image)
            LinkedResource logo = new LinkedResource(strFileName);
            logo.ContentId = "companylogo";
            //add the LinkedResource to the appropriate view
            htmlView.LinkedResources.Add(logo);
            //add the views
            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);
            SmtpClient client = new SmtpClient("mail.karminna.com", 587);
          //  client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("info@karminna.com", "8953#AFjn");
            client.Send(mail);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            timerInicializar = new System.Timers.Timer(500);
            timerInicializar.Elapsed += new ElapsedEventHandler(InicializarEnvio);            
            timerInicializar.AutoReset = false;
            MessageBox.Show("Te avisaremos al finalizar todos los envíos", "Trend");
            timerInicializar.Enabled = true;
            this.Close();
        }

        private void InicializarEnvio(object source, ElapsedEventArgs e)
        {
            titulo = txtTitulo.Text;
            int nroCorreos = 0;
            DataTable tblClientes = BL.GetDataBLL.Clientes();
            foreach (DataRow row in tblClientes.Rows)
            {
                if (!string.IsNullOrEmpty(row["CorreoCLI"].ToString()))
                {
                    try
                    {
                        EnviarCorreo(row["CorreoCLI"].ToString());
                        nroCorreos++;
                    }
                    catch (Exception)
                    { 
                    }                                        
                    Random rand = new Random();
                    int tiempo = rand.Next(20000, 50000);
                    Thread.Sleep(tiempo);
                }
            }
            MessageBox.Show("Se enviaron " + nroCorreos + " correos correctamente", "Trend");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            EnviarCorreo("oterobenjamin@gmail.com");
            Cursor.Current = Cursors.Arrow;
        }

    }
}
