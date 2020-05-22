using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmFoders());
            //Application.Run(new FrmSelectFolder(""));
            //Application.Run(new FrmDownload(null, @"C:\!ZONA\testeDownload\"));
            //Application.Run(new FrmDuplicadoFiltro(@"C:\@TombamentoV1_01\TOMBAMENTOS\TOMBAMENTO2020-05-15\", new List<string>()));
        }
    }
}
