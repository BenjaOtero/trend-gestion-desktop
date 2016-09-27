using System;
using System.Windows.Forms;
using System.Threading;

namespace StockVentas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionHandler);
       //     Application.ThreadException += new ThreadExceptionEventHandler(frmInicio.Form1_UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmInicio());

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
      //      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        }

        private static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs args)
        {
            try
            {
                // Log error here or prompt user...
            }
            catch { }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                // Log error here or prompt user...
            }
            catch { }
        }
    }
}
