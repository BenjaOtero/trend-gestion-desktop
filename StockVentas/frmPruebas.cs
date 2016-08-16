using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using BL;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Net;
 

namespace StockVentas
{
    public partial class frmPruebas : Form
    {
        [DllImport("user32.dll", SetLastError = true)] //dll necesaria para matar proceso excel
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr ProcessId);  //dll necesaria para matar proceso excel
        string razonSocial;
        bool exportaronDatos = false;

        Excel.Application app = new Excel.Application();
        Excel.Workbook libro;

        public frmPruebas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable tblArticulos = BL.GetDataBLL.GetArticulos();
            /*   int i = 0;
               for (i = 0; i < 10; i++)
               { 
            
               }*/
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataRow row in tblArticulos.Rows)
            {
                string oldId = row["IdArticuloART"].ToString();
                string id = "0" + oldId;
                BL.ArticulosBLL.ActualizarArticulos(id, oldId);
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void killExcel()
        {
            IntPtr hwnd = new IntPtr(app.Hwnd);
            IntPtr processId;
            IntPtr foo = GetWindowThreadProcessId(hwnd, out processId);
            Process proc = Process.GetProcessById(processId.ToInt32());
            proc.Kill(); // set breakpoint here and watch the Windows Task Manager kill this exact EXCEL.EXE            
        }        

        private void button5_Click(object sender, EventArgs e)
        {
            string line;

            // Read the file and display it line by line.
            ArrayList db = new ArrayList();
            System.IO.StreamReader file = new System.IO.StreamReader("n:\\ncsoftwa_re.sql");
            while ((line = file.ReadLine()) != null)
            {
                db.Add(line);
            }
            foreach (object o in db)
            {
                string linea = o.ToString();
                if (linea.Contains("ncsoftwa_re"))
                {

                    //aqui reemplazas

                }
            }
            file.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string Connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Benja\\Desktop\\liquidacion.xlsx;Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\";";
            OleDbConnection con = new OleDbConnection(Connection);
            System.Data.DataTable tblPrecioNuevos = new System.Data.DataTable();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Hoja1$]", con);
            myCommand.Fill(tblPrecioNuevos);
            DataTable tblArticulos = BL.GetDataBLL.Articulos();
            foreach (DataRow rowPreciosNuevos in tblPrecioNuevos.Rows)
            {
                string articulo = rowPreciosNuevos["Articulo"].ToString();
                if(!string.IsNullOrEmpty(articulo))
                {
                    if (articulo.Length == 8)
                        articulo = "00" + articulo;
                    if (articulo.Length == 9)
                        articulo = "0" + articulo;
                  //  articulo = rowPreciosNuevos["Articulo"].ToString().Substring(0, rowPreciosNuevos["Articulo"].ToString().Length - 4);
                    articulo = articulo.Substring(0, articulo.Length - 4);
                    DataRow[] foundRow = tblArticulos.Select("IdArticuloART LIKE '" + articulo + "*'");
                    foreach (DataRow rowArticulos in foundRow)
                    {
                        if (!string.IsNullOrEmpty(rowPreciosNuevos["Makro"].ToString()) && !string.IsNullOrEmpty(rowPreciosNuevos["Jesus"].ToString()))
                        {
                            rowArticulos["PrecioPublicoART"] = rowPreciosNuevos["Makro"];
                            rowArticulos["PrecioMayorART"] = rowPreciosNuevos["Jesus"];
                        }

                    }                
                }
            }
            if (tblArticulos.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblArticulos, "frmArticulosGenerar", "grabar");
                progreso.ShowDialog();
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void btnCompararStock_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string Connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Benja\\Desktop\\libro1.xlsx;Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\";";
            OleDbConnection con = new OleDbConnection(Connection);
            System.Data.DataTable tblOriginal = new System.Data.DataTable();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [original$]", con);
            myCommand.Fill(tblOriginal);
            System.Data.DataTable tblModificado = new System.Data.DataTable();
            myCommand = new OleDbDataAdapter("select * from [modificado$]", con);
            myCommand.Fill(tblModificado);
            int contador = 0;
            ArrayList miArray = new ArrayList();
            foreach (DataRow rowOriginal in tblOriginal.Rows)
            {
                string articulo = rowOriginal["Articulo"].ToString();
                DataRow[] rowfound = tblModificado.Select("Articulo LIKE '" + articulo + "'");
                if (rowfound.Count() == 0)
                {
                    miArray.Add(articulo);
                }
                foreach (DataRow rowModificado in rowfound)
                {
                    string originalMakro = rowOriginal["Makro"].ToString();
                    string modificadoMakro = rowModificado["Makro"].ToString();
                    string originalJesus = rowOriginal["Jesus Maria"].ToString();
                    string modificadoJesus = rowModificado["Jesus Maria"].ToString();
                    if (originalMakro != modificadoMakro || originalJesus != modificadoJesus)
                        MessageBox.Show(rowOriginal["Articulo"].ToString());
                }
                contador++;
            }
            MessageBox.Show(contador.ToString());
            Cursor.Current = Cursors.Arrow;
        }

        private void txtWebRequest_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string Connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Benja\\Desktop\\clientes_face.xlsx;Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\";";
            OleDbConnection con = new OleDbConnection(Connection);
            System.Data.DataTable tblClientesFace = new System.Data.DataTable();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [clientes$]", con);
            myCommand.Fill(tblClientesFace);

            DataTable tblClientes = BL.GetDataBLL.Clientes();
            int clientesAntes = tblClientes.Rows.Count;
            int clave;
            foreach (DataRow rowClientesFace in tblClientesFace.Rows)
            {
                string clienteFace = rowClientesFace["E-mail"].ToString();
                DataRow[] foundRow = tblClientes.Select("CorreoCLI LIKE '" + clienteFace + "'");
                if (foundRow.Count() > 0)
                {
                    DataRow rowCliente = tblClientes.NewRow();
                    Random rand = new Random();
                    clave = rand.Next(1000000000, 2000000000);
                    rowCliente["IdClienteCLI"] = clave;
                    rowCliente["NombreCLI"] = rowClientesFace["Nombre"].ToString().ToUpper();
                    rowCliente["ApellidoCLI"] = rowClientesFace["Apellido"].ToString().ToUpper();
                    rowCliente["CorreoCLI"] = rowClientesFace["E-mail"].ToString();
                    tblClientes.Rows.Add(rowCliente);
                }
            }
            int clientesDespues = tblClientes.Rows.Count;
            if (tblClientes.GetChanges() != null)
            {
                frmProgress progreso = new frmProgress(tblClientes, "frmClientes", "grabar");
                progreso.ShowDialog();
            }
            Cursor.Current = Cursors.Arrow;
            
        }

        private void btnUploadImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFilDlg = new OpenFileDialog();
            opFilDlg.Filter = "JPG (*.jpg)|*.jpg";
            if (opFilDlg.ShowDialog() == DialogResult.OK)
            {
                string strFileName = opFilDlg.FileName;
                byte[] ImageData;
                FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                ImageData = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                DAL.AlicuotasIvaDAL.SaveImage(ImageData);
            }            
        }

        private void btnDownLoadImage_Click(object sender, EventArgs e)
        {
            DataTable tbl = DAL.AlicuotasIvaDAL.RecuperarImage();
            byte[] imgBytes = (byte[])tbl.Rows[0]["Imagen"];
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap MyBitmap = (Bitmap)tc.ConvertFrom(imgBytes);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = MyBitmap;

        }

        private void btnLeftBottom_Click(object sender, EventArgs e)
        {
            DatosBLL.GetDataPOS();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            backgroundWorker1.RunWorkerAsync();           
        }

        public static void UploadFromFile(string nombreLocal, string nombreServidor)
        {
            string ftpServerIP;
            string ftpUserID;
            string ftpPassword;

            ftpServerIP = "karminna.com/public_html/images";
            ftpUserID = "benja@karminna.com";
            ftpPassword = "8953#AFjn";

            // FTP local
            /* ftpServerIP = "127.0.0.1:22";
              ftpUserID = "Benja";
              ftpPassword = "8953#AFjn";*/
            

            FileInfo fileInf = new FileInfo(@"C:\Trend\Ecommerce\trunk\public_html\images\00813605_large.jpg");
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/prueba.jpg"));

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


        private void frmPruebas_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable tbl = BL.GetDataBLL.RazonSocial();
            string idRazonSocial = tbl.Rows[0][0].ToString() + "_datos.sql.gz";
            ExportarDatos(idRazonSocial);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            exportaronDatos = true;
            Application.Exit();
            Cursor.Current = Cursors.Arrow;
        }

        public void ExportarDatos(string idRazonSocial)
        {
            razonSocial = idRazonSocial;
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\backup.bat"); //MO creo el archivo .bat
            sw.Close();
            StringBuilder sb = new StringBuilder();
            string path = Application.StartupPath;
            string unidad = path.Substring(0, 2);
            sb.AppendLine(unidad);
            sb.AppendLine(@"cd " + path + @"\Backup");
            sb.AppendLine(@"mysqldump -t --skip-comments -u ncsoftwa_re -p8953#AFjn -h localhost --opt ncsoftwa_re articulos clientes formaspago generos alicuotasiva razonsocial | gzip > c:\windows\temp\" + razonSocial);
            //sb.AppendLine(@"mysqldump --skip-comments -u ncsoftwa_re -p8953#AFjn -h localhost --opt ncsoftwa_re articulos clientes formaspago generos alicuotasiva razonsocial | gzip > c:\windows\temp\" + razonSocial);
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\backup.bat", true)) // escribo el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\backup.bat";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;  // permite disparar el evento process_Exited
            process.Exited += new EventHandler(ExportarDatos_Exited);
            process.Start();
            process.WaitForExit();
        }

        private void ExportarDatos_Exited(object sender, System.EventArgs e)
        {
            UploadDatosPos(@"c:\windows\temp\" + razonSocial, razonSocial);
            if (File.Exists("c:\\Windows\\Temp\\backup.bat")) File.Delete("c:\\Windows\\Temp\\backup.bat");
        }

        private static void UploadDatosPos(string nombreLocal, string nombreServidor)
        {
            string ftpServerIP;
            string ftpUserID;
            string ftpPassword;

                ftpServerIP = "trendsistemas.com/datos";
               ftpUserID = "benja@trendsistemas.com";
               ftpPassword = "8953#AFjn";

            // FTP local
               /*  ftpServerIP = "127.0.0.1:22";
                 ftpUserID = "Benja";
                 ftpPassword = "8953#AFjn";*/

            FileInfo fileInf = new FileInfo(nombreLocal);
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

    }
}

