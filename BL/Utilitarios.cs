using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Management;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration;
using System.IO;
using System.Net;
using System.Diagnostics;



namespace BL
{
    public class Utilitarios
    {
        static DataTable tblTabla;
        static Button grabar;
        static bool conexion;
        static string razonSocial;

        public static void ValidarComboBox(object sender, CancelEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (!string.IsNullOrEmpty(cmb.Text))
            {
                if (cmb.SelectedValue == null)
                {
                    e.Cancel = true;
                }
            }
        }

        public static void SoloNumerosConComa(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',')
            {
                e.Handled = false;
            }
            if (e.KeyChar == '.')
            {
                // si se pulsa en el punto se convertirá en coma
                e.Handled = true; //anula la tecla "." pulsada
                SendKeys.Send(",");
            }
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }

        public static void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }

        public static void puntoPorComa(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                // si se pulsa en el punto se convertirá en coma
                e.Handled = true; //anula la tecla "." pulsada
                SendKeys.Send(",");
            }
        }

        public static DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue)
        {
            // find primary key columns 
            //(i.e. everything but pivot column and pivot value)
            DataTable temp = dt.Copy();
            temp.Columns.Remove(pivotColumn.ColumnName);
            temp.Columns.Remove(pivotValue.ColumnName);
            string[] pkColumnNames = temp.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();

            // prep results table
            DataTable result = temp.DefaultView.ToTable(true, pkColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();

            dt.AsEnumerable()
                .Select(r => r[pivotColumn.ColumnName].ToString())
                .Distinct().ToList()
                .ForEach(c => result.Columns.Add(c, pivotColumn.DataType));

            // load it
            foreach (DataRow row in dt.Rows)
            {
                // find row to update
                DataRow aggRow = result.Rows.Find(
                    pkColumnNames
                        .Select(c => row[c])
                        .ToArray());
                // the aggregate used here is LATEST 
                // adjust the next line if you want (SUM, MAX, etc...)
                aggRow[row[pivotColumn.ColumnName].ToString()] = row[pivotValue.ColumnName];
            }

            return result;
        }

        public static bool HayInternet()
        {
            conexion = false;
            Ping Pings = new Ping();
            int timeout = 10000; //cambiar el valor a 10000
            try
            {
                if (Pings.Send("google.com", timeout).Status == IPStatus.Success) // cambiar ip 127.0.0.1 a google.com
                {
                    conexion = true;
                }
            }
            catch (PingException)
            {
                conexion = false;
            }
            return conexion;
        }

        public static bool ExisteServicio(string name)
        {
            bool existeServicio = false;
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == name)
                {
                    existeServicio = true;
                    continue;
                }
            }
            return existeServicio;
        }

        public static void StartService(string name)
        {
            ServiceController sc = new ServiceController(name);
            try
            {
                if (sc != null && sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                }
                sc.WaitForStatus(ServiceControllerStatus.Running);
                sc.Close();
            }
            catch (Exception)
            {
            }
        }

        public static void AddEventosABM(Control grpCampos, ref Button btnGrabar, ref CheckBox chk, ref DataTable tbl)
        {
            tblTabla = tbl;
            tblTabla.ColumnChanged += new DataColumnChangeEventHandler(HabilitarGrabar);
            grabar = btnGrabar;
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox)
                {
                    ctl.Enter += new System.EventHandler(SelTextoTextBox);
                    ctl.KeyDown += new System.Windows.Forms.KeyEventHandler(EnterTab);
                }
                else if (ctl is CheckBox)
                {
                    ctl.KeyDown += new System.Windows.Forms.KeyEventHandler(EnterTab);
                }
            }
        }

        public static void AddEventosABM(Control grpCampos, ref Button btnGrabar, ref DataTable tbl)
        {
            tblTabla = tbl;
            tblTabla.ColumnChanged += new DataColumnChangeEventHandler(HabilitarGrabar);
            grabar = btnGrabar;
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox || ctl is ComboBox)
                {
                    ctl.Enter += new System.EventHandler(SelTextoTextBox);
                    ctl.KeyDown += new System.Windows.Forms.KeyEventHandler(EnterTab);
                }
                if (ctl is CheckBox)
                {
                    ctl.KeyDown += new System.Windows.Forms.KeyEventHandler(EnterTab);
                }
            }
        }

        public static void AddEventosABM(Control grpCampos)
        {
            foreach (Control ctl in grpCampos.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox)
                {
                    ctl.Enter += new System.EventHandler(SelTextoTextBox);
                    ctl.KeyDown += new System.Windows.Forms.KeyEventHandler(EnterTab);
                }
            }
        }

        public static void SelTextoTextBox(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                TextBox tb = (TextBox)sender;
                tb.SelectionStart = 0;
                tb.SelectionLength = tb.Text.Length;
            }
            /*   if (sender.GetType() == typeof(ComboBox))
               {
                   ComboBox cb = (ComboBox)sender;
                   cb.SelectionStart = 0;
                   cb.SelectionLength = cb.Text.Length;
               }*/
            if (sender.GetType() == typeof(MaskedTextBox))
            {
                MaskedTextBox tb = (MaskedTextBox)sender;
                tb.SelectionStart = 0;
                tb.SelectionLength = tb.Text.Length;
            }
        }

        public static void EnterTab(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) SendKeys.Send("{TAB}");
        }

        public static void HabilitarGrabar(object sender, EventArgs e)
        {
            if (grabar.Enabled == false)
            {
                grabar.Enabled = true;
            }
        }

        public static void DataBindingsAdd(BindingSource bnd, GroupBox grp)
        {
            foreach (Control ctl in grp.Controls)
            {
                if (ctl is TextBox || ctl is MaskedTextBox)
                {
                    string campo = ctl.Name.Substring(3, ctl.Name.Length - 3);
                    ctl.DataBindings.Add("Text", bnd, campo, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                else if (ctl is CheckBox)
                {
                    string campo = ctl.Name.Substring(3, ctl.Name.Length - 3);
                  //  ctl.DataBindings.Add("Checked", bnd, campo, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                else if (ctl is ComboBox)
                {
                    string campo = ctl.Name.Substring(3, ctl.Name.Length - 3);
                    ctl.DataBindings.Add("SelectedValue", bnd, campo, false, DataSourceUpdateMode.OnPropertyChanged);
                }
            }
        }

        public static Binding DataBindingsCheckBoxAdd(BindingSource bnd, GroupBox grp)
        {
            Binding bind = null;
            foreach (Control ctl in grp.Controls)
            {
                 if (ctl is CheckBox)
                {
                    string campo = ctl.Name.Substring(3, ctl.Name.Length - 3);
                    bind = new Binding("Checked", bnd, campo, false, DataSourceUpdateMode.OnPropertyChanged);
                    bind.Format += new ConvertEventHandler(binding_Format);
                    bind.Parse += new ConvertEventHandler(binding_Parse);

                }

            }
            return bind;
        }

        private static void binding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value.ToString() == "True") e.Value = true;
            else e.Value = false;
        }

        private static void binding_Parse(object sender, ConvertEventArgs e)
        {
            if ((bool)e.Value) e.Value = 1;
            else e.Value = 0;
        }

        public static bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the e-mail address string is not empty.
            if (emailAddress.Length == 0)
            {
                errorMessage = "Debe indicar una dirección de correo.";
                return false;
            }

            // Confirm that there is an "@" and a "." in the e-mail address, and in the correct order.
            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }
            errorMessage = "Debe indicar una dirección de correo válida.\n" +
               "Por ejemplo 'alguien@dominio.com' ";
            return false;
        }

        public static void UploadFromFile(string nombreLocal, string nombreServidor)
        {            
            string connectionString = ConfigurationManager.ConnectionStrings["FtpLocal"].ConnectionString;
            //  string connectionString = ConfigurationManager.ConnectionStrings["Ftp"].ConnectionString;
            Char delimiter = ';';
            String[] substrings = connectionString.Split(delimiter);
            string ftpServerIP = substrings[0];
            string ftpUserID = substrings[1];
            string ftpPassword = substrings[2];
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + nombreServidor);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            byte[] fileContents = File.ReadAllBytes(nombreLocal);
            request.ContentLength = fileContents.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }

        public static void DownloadFile(string nombreLocal, string nombreServidor)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FtpLocal"].ConnectionString;
            //string connectionString = ConfigurationManager.ConnectionStrings["Ftp"].ConnectionString;
            Char delimiter = ';';
            String[] substrings = connectionString.Split(delimiter);
            string ftpServerIP = substrings[0];
            string ftpUserID = substrings[1];
            string ftpPassword = substrings[2];
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + nombreServidor);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            FtpWebResponse objResponse = (FtpWebResponse)request.GetResponse();
            byte[] buffer = new byte[32768];
            using (Stream input = objResponse.GetResponseStream())
            {
                if (File.Exists(nombreLocal)) File.Delete(nombreLocal);
                using (FileStream output = new FileStream(nombreLocal, FileMode.CreateNew))
                {
                    int bytesRead;
                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, bytesRead);
                    }
                }
            }
            objResponse.Close();
        }

        public static void UploadFromMemoryStream(MemoryStream memoryStream, string nombreRemoto, string servidor)
        {
            string ftpServerIP;
            string ftpUserID;
            string ftpPassword;
            if (servidor == "karminna")
            {
                ftpServerIP = "karminna.com/public_html/images";
                ftpUserID = "benja@karminna.com";
                ftpPassword = "8953#AFjn";
            }
            else
            {
                ftpServerIP = "trendsistemas.com/datos";
                ftpUserID = "benja@trendsistemas.com";
                ftpPassword = "8953#AFjn";
            }
              /*// FTP local
               ftpServerIP = "127.0.0.1:22";
                ftpUserID = "Benja";
                ftpPassword = "8953#AFjn";*/
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + nombreRemoto));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = memoryStream.Length;


            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                memoryStream.Seek(0, SeekOrigin.Begin);
                contentLen = memoryStream.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = memoryStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                memoryStream.Close();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message, "Upload Error");
                return;
            }
        }

        public static FtpWebRequest FtpRequest(string path)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FtpLocal"].ConnectionString;
            //string connectionString = ConfigurationManager.ConnectionStrings["Ftp"].ConnectionString;
            Char delimiter = ';';
            String[] substrings = connectionString.Split(delimiter);
            string ftpServerIP = substrings[0];
            string ftpUserID = substrings[1];
            string ftpPassword = substrings[2];
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + path);
            ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            return ftpRequest;
        }

        public static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;
            if (file1 == file2)
            {
                return true;
            }
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);
            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();
                return false;
            }
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));
            fs1.Close();
            fs2.Close();
            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }



    }
}
