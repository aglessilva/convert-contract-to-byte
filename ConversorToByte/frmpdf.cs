
using ConversorToByte.DTO;
using System;
using System.IO;
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

#if !DEBUG
            _pathFilePdf = Directory.GetCurrentDirectory();

            if (!Directory.Exists($@"{_pathFilePdf}\tmp"))
                Directory.CreateDirectory($@"{_pathFilePdf}\tmp");
            _pathFilePdf = $@"{_pathFilePdf}\tmp";
#else
            _pathFilePdf = @"C:\Lixo\tmp";
#endif

           Text = "Contrato: " + _file.NameContract;
            _pathFilePdf = Path.ChangeExtension(Path.GetTempFileName(), "pdf");
            File.WriteAllBytes(_pathFilePdf, _file.FileEncryption);
            webBrowser1.Navigate(_pathFilePdf);
        }

        private void Frmpdf_FormClosing(object sender, FormClosingEventArgs e)
        {
            webBrowser1.Stop();
            webBrowser1.Dispose();
            webBrowser1 = null;
        }
    }
}
