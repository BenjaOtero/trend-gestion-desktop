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
using System.IO.Compression;
using BL;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Net;

namespace Pruebas
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)] //dll necesaria para matar proceso excel
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr ProcessId);  //dll necesaria para matar proceso excel
        string razonSocial;
        bool exportaronDatos = false;

        Excel.Application app = new Excel.Application();
        Excel.Workbook libro;

        public Form1()
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
                if (!string.IsNullOrEmpty(articulo))
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
                StockVentas.frmProgress progreso = new StockVentas.frmProgress(tblArticulos, "frmArticulosGenerar", "grabar");
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

            Process process = new Process();
            process.StartInfo.FileName = @"N:\Mis documentos\Programas\MySql\mysql-essential-5.5.0-m2-win32.msi";
            process.StartInfo.Arguments = "/quiet";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

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

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Mysql\mysqldump.exe";
            Process.Start(path, ("-t --skip-comments -u ncsoftwa_re -p8953#AFjn -h localhost --opt ncsoftwa_re alicuotasiva articulos clientes formaspago generos razonsocial stock -r \"c:\\windows\\temp\\prueba.sql\""));



            /*   var bytes = File.ReadAllBytes("c:\\windows\\temp\\prueba.sql");
               using (FileStream fs = new FileStream("c:\\prueba.sql.zip", FileMode.CreateNew))
               using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Compress, false))
               {
                   zipStream.Write(bytes, 0, bytes.Length);
               }*/

        }

        private void btnCrearUsuarioMysql_Click(object sender, EventArgs e)
        {
            List<string> credentials = UtilVarios.GetCredentialsDB();
            string server = credentials[0];
            string user = credentials[1];
            string database = credentials[2];
            string pass = credentials[3];
            Process process = new Process();
            process.StartInfo.FileName = Application.StartupPath + @"\Mysql\mysql.exe";
            string usuario = string.Format("-u root -e \"GRANT ALL ON *.* TO '{0}'@'%' IDENTIFIED BY '{1}' WITH GRANT OPTION;\"",user, pass);

          //  string usuario = string.Format("-u root -e \"GRANT ALL ON *.* TO 'dale_che'@'%' IDENTIFIED BY '8953#AFjn' WITH GRANT OPTION;\"");
            process.StartInfo.Arguments = usuario;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        private void ConfigurarMySQL()
        {
            System.IO.StreamWriter sw = System.IO.File.CreateText("c:\\Windows\\Temp\\config_mysql.bat"); // creo el archivo .bat
            sw.Close();
            string programFiles;
            if (Directory.Exists(@"C:\Program files"))
            {
                programFiles = "Program files";
            }
            else if (Directory.Exists(@"C:\Archivos de programa"))
            {
                programFiles = "Archivos de programa";
            }
            else if (Directory.Exists(@"C:\Program files(x86)"))
            {
                programFiles = "Program files(x86)";
            }
            else
            {
                programFiles = "Archivos de programa(x86)";
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("C:");
            string configMysql = "cd " + "\"C:\\" + programFiles + "\\MySQL\\MySQL Server 5.5\\bin\"";
            sb.AppendLine(configMysql);
            string usuario = "mysql.exe -u root -p8953#AFjn -e \"GRANT ALL ON *.* TO 'ncsoftwa_re'@'%' IDENTIFIED BY '8953#AFjn' WITH GRANT OPTION; FLUSH PRIVILEGES;\"";
            sb.AppendLine(usuario);
            string rutaDB = Application.StartupPath.ToString() + @"\MySql\ncsoftwa_re.sql";
            string restaurarDB = "mysql.exe -u ncsoftwa_re -p8953#AFjn < \"" + rutaDB + "\"";
            sb.AppendLine(restaurarDB);
            using (StreamWriter outfile = new StreamWriter("c:\\Windows\\Temp\\config_mysql.bat", true)) // escribo en el archivo .bat
            {
                outfile.Write(sb.ToString());
            }
            Process process = new Process();
            process.StartInfo.FileName = "c:\\Windows\\Temp\\config_mysql.bat";

            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.EnableRaisingEvents = true;
            process.WaitForExit();
            StringBuilder sb_myIni = new StringBuilder();
            sb_myIni.AppendLine("");
            sb_myIni.AppendLine("[mysqld]");
            sb_myIni.AppendLine("lower_case_table_names = 0");
            using (StreamWriter file = new StreamWriter("C:\\" + programFiles + "\\MySQL\\MySQL Server 5.5\\my.ini", true))
            {
                file.Write(sb_myIni.ToString());
            }
        }

    }
}

