using System;
using System.IO;
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
                SelectFolders();

                if (!Directory.Exists(textOrigemContratosPdf.Text + @"\config"))
                    Directory.CreateDirectory(textOrigemContratosPdf.Text + @"\config");

                if (!File.Exists(textOrigemContratosPdf.Text + @"\config\SITU115A.TXT"))
                {
                    MessageBox.Show("Não foi encontrato dentro da pasta 'config', o arquivo de SITUAÇÕES (SITU115A.TXT)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (!File.Exists(textOrigemContratosPdf.Text + @"\config\ARQ_GARANTIA.TXT"))
                {
                    MessageBox.Show("Não foi encontrato dentro da pasta 'config', o arquivo de PONTEIRO (ARQ_GARANTIA.TXT).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                btnIniciarConvercao.Enabled = (textDestinoLayout.TextLength > 0 && textOrigemContratosPdf.TextLength > 0);
            }
        }
        

        private void BtnSelectDiretorioDestino_Click(object sender, EventArgs e)
        {
            BtnSelectDiretorioOrigem_Click(null, null);
        }

        /// <summary>
        /// Seleciona os diretório de Origem e Destino
        /// </summary>
        void SelectFolders()
        {
            if (string.IsNullOrWhiteSpace(textOrigemContratosPdf.Text))
                textOrigemContratosPdf.Text = folderBrowserDialog1.SelectedPath;
            else
                textDestinoLayout.Text = folderBrowserDialog1.SelectedPath;

        }

        private void BtnIniciarConvercao_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(comboBoxTela.Text))
            {
                MessageBox.Show("Selecione uma tela para converção.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Hide();
            frmGerarLayoutAlta f = new frmGerarLayoutAlta(this.textOrigemContratosPdf.Text, this.textDestinoLayout.Text, comboBoxTela.Text);
            f.ShowDialog();
            this.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

          //  var sbDados2 = "1,00200.00";
          //  var match = Regex.IsMatch(sbDados2.ToString(), @"^[0-2,]$");

            frmGerarLayoutAlta f = new frmGerarLayoutAlta(@"D:\PDFSTombamento", "","TELA 16");
            f.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dt = Convert.ToDateTime("0001/01/01");
            frmGerarLayoutAlta f = new frmGerarLayoutAlta(@"D:\PDFSTombamento\Exceptions", "", "TELA16");
            f.ShowDialog();
            this.Show();
        }
    }
}
