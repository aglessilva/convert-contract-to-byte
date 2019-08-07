using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ConvetPdfToLayoutAlta
{
    public partial class FrmSelectFolder : Form
    {
        public FrmSelectFolder()
        {
            InitializeComponent();
        }

        private void BtnSelectDiretorioOrigem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (SelectFolders(1) == DialogResult.Yes)
                {
                    string[] _arquivos = { Directory.GetCurrentDirectory() + @"\config\SITU115A.TXT", Directory.GetCurrentDirectory() + @"\config\ARQ_GARANTIA.TXT" };
                    string msg = string.Format("--- COPIE OS ARQUIVOS NO DIRETORIO ABAIXO ---\n\n");

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\config"))
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\config");

                    if(!Directory.Exists(Directory.GetCurrentDirectory() + @"\ALTA"))
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\ALTA");

                    if (!File.Exists(Directory.GetCurrentDirectory() + @"\config\SITU115A.TXT"))
                    {
                        msg += string.Format("--> {0}\n--> {1}", _arquivos[0], _arquivos[1]);

                        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (!File.Exists(Directory.GetCurrentDirectory() + @"\config\ARQ_GARANTIA.TXT"))
                    {
                        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    btnIniciarConvercao.Enabled = btnDuplicata.Enabled = (textDestinoLayout.TextLength > 0 && textOrigemContratosPdf.TextLength > 0);
                }
            }
        }
        

        private void BtnSelectDiretorioDestino_Click(object sender, EventArgs e)
        {
            //BtnSelectDiretorioOrigem_Click(null, null);
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectFolders(0);
            }
        }

        /// <summary>
        /// Seleciona os diretório de Origem e Destino
        /// </summary>
        DialogResult SelectFolders(int campo)
        {
            DialogResult result = DialogResult.Cancel;
            DirectoryInfo dirInfor = new DirectoryInfo(folderBrowserDialog1.SelectedPath);

            if (!dirInfor.Root.Name.ToUpper().Contains("C"))
            {
                result = MessageBox.Show("O processo de conversão ficará muito lento se executado pela rede. \nDeseja seguir com esse mapeamento? ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return result;
            }

            if (campo == 1) 
            {
                textOrigemContratosPdf.Text = folderBrowserDialog1.SelectedPath;
                textDestinoLayout.Text = Directory.GetCurrentDirectory() + @"\ALTA";
            }
            else
                textDestinoLayout.Text = Directory.GetCurrentDirectory() + @"\ALTA";



            return result;
        }

        private void BtnIniciarConvercao_Click(object sender, EventArgs e)
        {

            if (comboBoxTela.Text.Equals("Selecione o tipo de arquivo"))
            {
                MessageBox.Show("Selecione uma tela para converção.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            panelSpinner.Visible = !panelSpinner.Visible;


            Form f = null;

            if (comboBoxTela.Text.ToUpper().Equals("TELA 16"))
                f = new FrmTela16(textOrigemContratosPdf.Text, textDestinoLayout.Text, comboBoxTela.Text);

            if (comboBoxTela.Text.ToUpper().Equals("TELA 18"))
                f = new FrmTela18(textOrigemContratosPdf.Text, textDestinoLayout.Text, comboBoxTela.Text);

            if (comboBoxTela.Text.ToUpper().Equals("TELA 20"))
                f = new FrmTela20(textOrigemContratosPdf.Text, textDestinoLayout.Text, comboBoxTela.Text);

            if (comboBoxTela.Text.ToUpper().Equals("TELA 25"))
                f = new FrmTela25(textOrigemContratosPdf.Text, textDestinoLayout.Text, comboBoxTela.Text);

            this.Text += "-" + comboBoxTela.Text;
            f.ShowDialog();
            panelSpinner.Visible = !panelSpinner.Visible;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
          //  var sbDados2 = "1,00200.00";
          //  var match = Regex.IsMatch(sbDados2.ToString(), @"^[0-2,]$");

            FrmTela16 f = new FrmTela16(@"D:\PDFSTombamento", @"D:\PDFSTombamento\txt", "TELA 16");
            f.ShowDialog();
            this.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FrmTela16 f = new FrmTela16(@"D:\PDFSTombamento\Exceptions", @"D:\PDFSTombamento\txt", "TELA16");
            f.ShowDialog();
            this.Show();
        }

        private void FrmSelectFolder_Load(object sender, EventArgs e)
        {
            comboBoxTela.SelectedIndex = 4;
#if DEBUG
            button1.Visible = button2.Visible = button3.Visible = button4.Visible = button5.Visible = button6.Visible = button7.Visible = button8.Visible = true;
#endif
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FrmTela18 f = new FrmTela18(@"D:\PDFSTombamento\Exceptions18", @"D:\PDFSTombamento\txt", "TELA18");
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmTela18 f = new FrmTela18(@"D:\PDFSTombamento\", @"D:\PDFSTombamento\txt", "TELA18");
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmTela20 f = new FrmTela20(@"D:\PDFSTombamento\Exceptions20", @"D:\PDFSTombamento\txt", "TELA20");
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmTela20 f = new FrmTela20(@"D:\PDFSTombamento\", @"D:\PDFSTombamento\txt", "TELA20");
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmTela25 f = new FrmTela25(@"D:\PDFSTombamento\Exceptions25", @"D:\PDFSTombamento\txt", "TELA25");
            f.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmTela25 f = new FrmTela25(@"D:\PDFSTombamento\", @"D:\PDFSTombamento\txt", "TELA25");
            f.ShowDialog();
        }

        private void BtnDuplicata_Click(object sender, EventArgs e)
        {
            List<string> lstArquiPoint = new List<string>();

            string _tela = string.Format("*_{0}.hbt", Regex.Replace(comboBoxTela.Text, @"[^0-9$]", ""));
            var arr = Directory.GetFiles(textOrigemContratosPdf.Text, _tela , SearchOption.AllDirectories).ToList();


            //for (int i = 0; i < arr.Count; i++)
            //{
            //    FileInfo f = new FileInfo(arr[i]);

            //    string[] lst = Directory.EnumerateFiles(@"D:\PDFSTombamento\2019-06-27", string.Format("*{0}_16.pdf", f.Name.Split('_')[0]), SearchOption.AllDirectories).ToArray();

            //    if (lst.Length > 0)
            //        if (File.Exists(lst[0]))
            //        {
            //            if (File.Exists(@"D:\PDFSTombamento\filtro\" + f.Name.Split('.')[0] + ".Err"))
            //                File.Delete(@"D:\PDFSTombamento\filtro\" + f.Name.Split('.')[0] + ".Err");

            //            File.Move(lst[0], @"D:\PDFSTombamento\filtro\" + f.Name.Split('.')[0] + ".Err");
            //        }
            //}
        }
    }
}
