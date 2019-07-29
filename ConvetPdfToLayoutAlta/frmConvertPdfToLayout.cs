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
                if (SelectFolders(1) == DialogResult.Yes)
                {
                    string[] _arquivos = { textOrigemContratosPdf.Text + @"\config\SITU115A.TXT", textOrigemContratosPdf.Text + @"\config\ARQ_GARANTIA.TXT" };
                    string msg = string.Format("--- INFORME OS ARQUIVOS NO DIRETORIO ABAIXO ---\n\n");

                    if (!Directory.Exists(textOrigemContratosPdf.Text + @"\config"))
                        Directory.CreateDirectory(textOrigemContratosPdf.Text + @"\config");

                    if (!File.Exists(textOrigemContratosPdf.Text + @"\config\SITU115A.TXT"))
                    {
                        msg += string.Format("--> {0}\n--> {1}", _arquivos[0], _arquivos[1]);

                        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (!File.Exists(textOrigemContratosPdf.Text + @"\config\ARQ_GARANTIA.TXT"))
                    {
                        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    btnIniciarConvercao.Enabled = (textDestinoLayout.TextLength > 0 && textOrigemContratosPdf.TextLength > 0);
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
                textDestinoLayout.Text = folderBrowserDialog1.SelectedPath + @"\ALTA";
            }
            else
                textDestinoLayout.Text = folderBrowserDialog1.SelectedPath + @"\ALTA";



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

            this.Text += "-" + comboBoxTela.Text;
            f.ShowDialog();
            panelSpinner.Visible = !panelSpinner.Visible;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

          //  var sbDados2 = "1,00200.00";
          //  var match = Regex.IsMatch(sbDados2.ToString(), @"^[0-2,]$");

            FrmTela16 f = new FrmTela16(@"D:\PDFSTombamento", "","TELA 16");
            f.ShowDialog();
            this.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FrmTela16 f = new FrmTela16(@"D:\PDFSTombamento\Exceptions", "", "TELA16");
            f.ShowDialog();
            this.Show();
        }

        private void FrmSelectFolder_Load(object sender, EventArgs e)
        {
            comboBoxTela.SelectedIndex = 4;
#if DEBUG
            button1.Visible = button2.Visible = button3.Visible = true;
#endif
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmTela18 f = new FrmTela18(@"D:\PDFSTombamento\Exceptions18", "", "TELA18");
            f.ShowDialog();
            this.Show();
        }
    }
}
