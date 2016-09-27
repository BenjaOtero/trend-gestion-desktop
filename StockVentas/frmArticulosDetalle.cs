using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using StockVentas.com.karminna;


namespace StockVentas
{
    public partial class frmArticulosDetalle : Form
    {
        DataTable tblArticulos;
        public DataSet dt = null;
        private DataRowView fila = null;
        string strFileName = null;
        string strFileNameBck = null;
        string strFileNameColor = null;
        string nombreServidor = null;
        string nombreServidorColor = null;
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;

        public frmArticulosDetalle()
        {
            InitializeComponent();
        }

        public frmArticulosDetalle(DataTable tablaArticulos, DataRowView fila)
            : this()
        {
            this.fila = fila;
            this.tblArticulos = tablaArticulos;
            BL.Utilitarios.AddEventosABM(grpCampos, ref btnGrabar, ref tblArticulos);
            cmbGenero.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
       //     cmbGenero.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            cmbProveedor.Validating += new System.ComponentModel.CancelEventHandler(BL.Utilitarios.ValidarComboBox);
      //      cmbProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(BL.Utilitarios.EnterTab);
            txtCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtPublico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
            txtMayor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BL.Utilitarios.SoloNumerosConComa);
        }

        private void frmArticulosDetalle_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50);
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            btnGrabar.Enabled = false;
            if (fila != null) // estoy editando
            {
                DataTable tblGeneros = BL.GetDataBLL.Generos();
                cmbGenero.ValueMember = "IdGeneroGEN";
                cmbGenero.DisplayMember = "DescripcionGEN";
                cmbGenero.DropDownStyle = ComboBoxStyle.DropDown;
                cmbGenero.DataSource = tblGeneros;
                cmbGenero.SelectedValue = -1;
                AutoCompleteStringCollection generosColection = new AutoCompleteStringCollection();
                foreach (DataRow row in tblGeneros.Rows)
                {
                    generosColection.Add(Convert.ToString(row["DescripcionGEN"]));
                }
                cmbGenero.AutoCompleteCustomSource = generosColection;
                cmbGenero.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbGenero.AutoCompleteSource = AutoCompleteSource.CustomSource;

                DataTable tblAlicuotasIva = BL.GetDataBLL.AlicuotasIva();
                cmbAlicuota.ValueMember = "IdAlicuotaALI";
                cmbAlicuota.DisplayMember = "PorcentajeALI"; //PorcentajeALI
                cmbAlicuota.DropDownStyle = ComboBoxStyle.DropDown;
                cmbAlicuota.DataSource = tblAlicuotasIva;
                cmbAlicuota.SelectedValue = -1;

                DataTable tblProveedores = BL.GetDataBLL.Proveedores();
                cmbProveedor.ValueMember = "IdProveedorPRO";
                cmbProveedor.DisplayMember = "RazonSocialPRO";
                cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProveedor.DataSource = tblProveedores;
                AutoCompleteStringCollection proveedorColection = new AutoCompleteStringCollection();
                foreach (DataRow row in tblProveedores.Rows)
                {
                    proveedorColection.Add(Convert.ToString(row["RazonSocialPRO"]));
                }
                cmbProveedor.AutoCompleteCustomSource = proveedorColection;
                cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtIdArticulo.Enabled = false;
                txtIdArticulo.DataBindings.Add("Text", fila, "IdArticuloART", false, DataSourceUpdateMode.OnPropertyChanged);
                txtDescripcion.DataBindings.Add("Text", fila, "DescripcionART", false, DataSourceUpdateMode.OnPropertyChanged);
                txtDescripcionWeb.DataBindings.Add("Text", fila, "DescripcionWebART", false, DataSourceUpdateMode.OnPropertyChanged);
                txtCosto.DataBindings.Add("Text", fila, "PrecioCostoART", false, DataSourceUpdateMode.OnPropertyChanged);
                txtPublico.DataBindings.Add("Text", fila, "PrecioPublicoART", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMayor.DataBindings.Add("Text", fila, "PrecioMayorART", false, DataSourceUpdateMode.OnPropertyChanged);
                cmbGenero.DataBindings.Add("SelectedValue", fila, "IdGeneroART", false, DataSourceUpdateMode.OnPropertyChanged);

                cmbAlicuota.DataBindings.Add("SelectedValue", fila, "IdAliculotaIvaART", false, DataSourceUpdateMode.OnPropertyChanged);

                cmbProveedor.DataBindings.Add("SelectedValue", fila, "IdProveedorART", false, DataSourceUpdateMode.OnPropertyChanged);

              //  chkActivo.CheckState = CheckState.Checked; // Tildo el checkbox para bindearlo
                Binding bind = new Binding("Checked", fila, "ActivoWebART", false, DataSourceUpdateMode.OnPropertyChanged);
                bind.Format += new ConvertEventHandler(binding_Format);
                bind.Parse += new ConvertEventHandler(binding_Parse);
                txtIdArticulo.Focus();

            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            MemoryStream ms;
            bool tratarImagenesServer = false;
            Cursor.Current = Cursors.WaitCursor;
            if (!BL.UtilDB.ValidarServicioMysql())
            {
                MessageBox.Show("NO SE ACTUALIZARON LOS DATOS." + '\r' + "No se pudo conectar con el servidor de base de datos."
                        + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                tblArticulos.RejectChanges();
                return;
            }
            try
            {
                if (strFileName != null)
                {
                    var image_large = Image.FromFile(strFileName);
                    if (image_large.Height < 1600 && image_large.Width < 1200)
                    {
                        MessageBox.Show("La imagen debe medir 1600px de alto por 1200px de ancho.", "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ms = new MemoryStream();
                    using (FileStream fs = File.OpenRead(strFileName))
                    {
                        fs.CopyTo(ms);
                    }
                    BL.UtilFTP.UploadFromMemoryStream(ms, nombreServidor + "_large.jpg", "karminna");
                    tratarImagenesServer = true;
                }
                if (strFileNameBck != null)
                {
                    var image_large = Image.FromFile(strFileNameBck);
                    if (image_large.Height < 1600 && image_large.Width < 1200)
                    {
                        MessageBox.Show("La imagen debe medir 1600px de alto por 1200px de ancho.", "Trend Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ms = new MemoryStream();
                    using (FileStream fs = File.OpenRead(strFileNameBck))
                    {
                        fs.CopyTo(ms);
                    }
                    BL.UtilFTP.UploadFromMemoryStream(ms, nombreServidor + "_bck_large.jpg", "karminna");
                    tratarImagenesServer = true;
                }
                if (strFileNameColor != null)
                {
                    ms = new MemoryStream();
                    using (FileStream fs = File.OpenRead(strFileNameColor))
                    {
                        fs.CopyTo(ms);
                    }
                    BL.UtilFTP.UploadFromMemoryStream(ms, nombreServidorColor, "karminna");
                }
                if (tratarImagenesServer)
                {
                    TratarImagenesService tis = new TratarImagenesService();
                    tis.TratarImagenes(nombreServidor);
                }
                fila.EndEdit();
                if (tblArticulos.GetChanges() != null)
                {
                    Grabar();
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Se produjo un error al subir las imagenes al servidor", "Trend");
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        private void Upload(string nombreLocal, string nombreServidor)
        {
            ftpServerIP = "";
            ftpUserID = "alguien@hosting.com";
            ftpPassword = "1234";

            FileInfo fileInf = new FileInfo(nombreLocal);
            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + nombreServidor));

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
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmArticulosDetalle_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fila.EndEdit();
            if (tblArticulos.GetChanges() != null)
            {
                DialogResult respuesta = MessageBox.Show("¿Confirma la grabación de datos?", "Trend Gestión",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (respuesta)
                {
                    case DialogResult.Yes:
                        if (!BL.UtilDB.ValidarServicioMysql())
                        {
                            MessageBox.Show("NO SE ACTUALIZARON LOS DATOS." + '\r' + "No se pudo conectar con el servidor de base de datos."
                                    + '\r' + "Consulte al administrador del sistema.", "Trend Sistemas", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            tblArticulos.RejectChanges();
                            return;
                        }
                        Grabar();
                        break;
                    case DialogResult.No:
                        tblArticulos.RejectChanges();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void Grabar()
        {
            DataRow row = tblArticulos.Rows.Find(txtIdArticulo.Text);
            row["RazonSocialPRO"] = cmbProveedor.Text;
            frmProgress progreso = new frmProgress(tblArticulos, "frmArticulos", "grabar");
            progreso.ShowDialog();
        }

        private void binding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value.ToString() == "True") e.Value = true;
            else e.Value = false;
        }

        private void binding_Parse(object sender, ConvertEventArgs e)
        {
            if ((bool)e.Value) e.Value = 1;
            else e.Value = 0;
        }


    }
}
