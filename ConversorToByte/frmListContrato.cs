using ConversorToByte.BLL;
using ConversorToByte.DTO;
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

            IEnumerable<string> lstItem = Directory.EnumerateFiles(Path.GetTempPath(), "*.pdf", SearchOption.TopDirectoryOnly)
              .Where(x => new FileInfo(x).CreationTime.Date.ToShortDateString().Equals(DateTime.Today.Date.ToShortDateString()));

            lstItem.ToList().ForEach(f => { File.Delete(f); });

            
            fsf = new FileSafeOperations();
            Users obj = fsf.CheckUser(Environment.UserName);

            ValidaPermissao(obj);
            List<FileSafe> lst =  fsf.GetFilesSafe(null,null);
            dataGridViewContract.AutoGenerateColumns = false;


            dataGridViewContract.DataSource = lst.ToList();
        }

        private void ValidaPermissao(Users obj)
        {
            SessionUser.IsGestorApp = obj.IsGestorApp;
            SessionUser.UserLogin = Environment.UserName;

            if (SessionUser.IsGestorApp)
            {
                menuStrip1.Visible = true;
            }

        }



        private void dataGridViewContract_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex != 3 && e.ColumnIndex != 4)
                return;

            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                FileSafe obj = (FileSafe)dataGridViewContract.Rows[e.RowIndex].DataBoundItem;
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Compactado|*.zip|*.rar|*.7z";
                   
                fsf = new FileSafeOperations();
                obj = fsf.GetFilesSafe(null,obj.Id.ToString()).First();
               
                saveFile.FileName = obj.NameContract;

                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(saveFile.FileName, FileMode.Create)))
                    { writer.Write(obj.FileEncryption); }

                    string _path =  new FileInfo(saveFile.FileName).DirectoryName;
                    if (MessageBox.Show(string.Format("Deseja abrir o diretório onde o contrato ({0}) foi salvo?", obj.NameContract), "Contratos Liquidados", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Process.Start("explorer.exe", _path);
                        Cursor.Current = Cursors.Default;
                    }
                }
            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0 && e.ColumnIndex == 4)
             {
                 string _idContract = senderGrid[0, e.RowIndex].Value.ToString();
                 fsf = new FileSafeOperations();
                 FileSafe _file = fsf.GetFilesSafe(_idContract);
                 frmpdf _frmPdf = new frmpdf(_file);
                 _frmPdf.ShowDialog();
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
                List<FileSafe> lst = fsf.GetFilesSafe(_contractCpf: textBoxContratocpf.Text);
                dataGridViewContract.DataSource = lst.ToList();
            }
            else
                if(dataGridViewContract.Rows.Count < 50 && textBoxContratocpf.Text.Length < 2)
                {
                    dataGridViewContract.DataSource = null;
                    List<FileSafe> lst = fsf.GetFilesSafe(null, null);
                    dataGridViewContract.DataSource = lst.ToList();
                }
        }



        private void addUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUsuariosAD f = new FrmAddUsuariosAD();
            f.ShowDialog();
        }
    }
}
