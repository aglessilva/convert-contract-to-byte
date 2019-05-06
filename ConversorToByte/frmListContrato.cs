using ConversorToByte.BLL;
using ConversorToByte.DTO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConversorToByte
{
    public partial class frmListContrato : Form
    {
        FileSafeOperations fsf;

        public frmListContrato()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            //byte[] arq = File.ReadAllBytes(@"C:\Users\x194262\Desktop\Agles\70455230001430\70455230001430_16.PDF");

            //using (MemoryStream mStream = new MemoryStream(arq))
            //{
            //    var document = new iTextSharp.text.Document();
            //    var pdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, mStream);
            //    pdfWriter.Open();
            //    //document.Open();

                
            //}

            
            fsf = new FileSafeOperations();
            Users obj = fsf.CheckUser(Environment.UserName);

            ValidaPermissao(obj);
            List<FileSafe> lst =  fsf.GetFilesSafe();
            dataGridViewContract.AutoGenerateColumns = false;

            if(!SessionUser.IsDownload)
            {
                dataGridViewContract.Columns.RemoveAt(3);
            }

            dataGridViewContract.DataSource = lst.ToList();
        }

        private void ValidaPermissao(Users obj)
        {
            SessionUser.IsDownload = obj.IsDownload;
            SessionUser.IsGestorApp = obj.IsGestorApp;
            SessionUser.UserLogin = Environment.UserName;

            if (SessionUser.IsGestorApp)
            {
                menuStrip1.Enabled = true;
            }

        }



        private void dataGridViewContract_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                FileSafe obj = (FileSafe)dataGridViewContract.Rows[e.RowIndex].DataBoundItem;
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Compactado|*.zip|*.rar|*.7z";
                   
                fsf = new FileSafeOperations();
                List<FileSafe> lst = fsf.GetFilesSafe(_id:obj.Id.ToString(),typeFilte:"1");
                obj = lst.First();
                byte[] arq = obj.FileEncryption;

                saveFile.FileName = obj.NameContract;

                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(saveFile.FileName, FileMode.Create)))
                    { writer.Write(arq); }

                   

                    string _path =  new FileInfo(saveFile.FileName).DirectoryName;
                    if (MessageBox.Show(string.Format("Deseja abrir o diretório onde o contrato ({0}) foi salvo?", obj.NameContract), "Contratos Liquidados", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Process.Start("explorer.exe", _path);
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }


        private void btnLimpar_Click(object sender, EventArgs e)
        {
            textBoxContratocpf.Text = string.Empty;
            textBoxContratocpf.Focus();


            
        }

        private void textBoxContratocpf_TextChanged(object sender, EventArgs e)
        {
            if (textBoxContratocpf.Text.Length > 2)
            {
                dataGridViewContract.DataSource = null;
                List<FileSafe> lst = fsf.GetFilesSafe(textBoxContratocpf.Text);
                dataGridViewContract.DataSource = lst.ToList();
            }
            else
                if(dataGridViewContract.Rows.Count < 50 && textBoxContratocpf.Text.Length < 2)
                {
                    dataGridViewContract.DataSource = null;
                    List<FileSafe> lst = fsf.GetFilesSafe();
                    dataGridViewContract.DataSource = lst.ToList();
                }
        }
    }
}
