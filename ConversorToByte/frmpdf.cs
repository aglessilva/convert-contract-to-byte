
using ConversorToByte.DTO;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConversorToByte
{
    public partial class frmpdf : Form
    {
        private FileSafe _file = null;
        private string _pathFilePdf = string.Empty;
        public frmpdf(FileSafe _fileSafe)
        {
            InitializeComponent();
            _file = _fileSafe;
        }

        private void frmpdf_Load(object sender, EventArgs e)
        {
            this.Text =  "Contrato: " + _file.NameContract;
            _pathFilePdf = Path.ChangeExtension(Path.GetTempFileName(), "pdf");
            File.WriteAllBytes(_pathFilePdf, _file.FileEncryption);
            webBrowser1.Navigate(_pathFilePdf);
        }

        private void frmpdf_FormClosing(object sender, FormClosingEventArgs e)
        {

            webBrowser1.Stop();
            webBrowser1.Dispose();
            webBrowser1 = null;
        }
    }
}
