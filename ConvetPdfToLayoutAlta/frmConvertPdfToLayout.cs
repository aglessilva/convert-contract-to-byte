using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ConvetPdfToLayoutAlta
{
    public partial class FrmSelectFolder : Form
    {
        List<string> lstDamp3 = new List<string>();
        bool[] consistencia = { false, false, false, false };
        bool[] isProcessado = { false, false, false, false };
        bool[] orderExcute = { false, false, false, false };

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
                    btnDuplicata.Enabled = comboBoxTela.Enabled = (textDestinoLayout.TextLength > 0 && textOrigemContratosPdf.TextLength > 0);
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

            if (!new string[] { "C", "D" }.Any(f => dirInfor.Root.Name.ToUpper().Contains(f)))
            {
                result = MessageBox.Show("O processo de conversão ficará muito lento se executado pela rede. \nDeseja seguir com esse mapeamento? ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return result;
            }
            else
                result = DialogResult.Yes;

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
            if (comboBoxTela.SelectedIndex.Equals(4))
            {
                MessageBox.Show("Selecione uma tela para converção.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = (comboBoxTela.SelectedIndex - 1); i >= 0; i--)
            {
                if (!orderExcute[i])
                {
                    MessageBox.Show("A converção deve ser realizada em ordem crescente de tela.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

           
            if (!isProcessado[comboBoxTela.SelectedIndex])
            {
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


                orderExcute[comboBoxTela.SelectedIndex] = true;
                isProcessado[comboBoxTela.SelectedIndex] = true;

                Text += "-" + comboBoxTela.Text + $" - (V{Application.ProductVersion})"; ;
                f.ShowDialog();
                panelSpinner.Visible = !panelSpinner.Visible;
                Text = Text.Split('-')[0];
            }
            else
            {
                string msg = string.Format("A {0} já foi processada!", comboBoxTela.Text.ToUpper());
                MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FrmTela16 f = new FrmTela16(@"D:\Testes", @"D:\Testes\txt", "TELA 16");
            f.ShowDialog();
           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //FrmTela16 f = new FrmTela16(@"C:\TombTesteUnitarios", @"C:\TombTesteUnitarios\ALTA", "TELA16");
            FrmTela16 f = new FrmTela16(@"C:\TombTesteUnitarios", @"C:\TombTesteUnitarios\ALTA", "TELA16");
            f.ShowDialog();
           
        }

        private void UpdateApp()
        {
            Text += $" (V{Application.ProductVersion})";
            try
            {
                var updateManager = NAppUpdate.Framework.UpdateManager.Instance;
                updateManager.UpdateSource = new NAppUpdate.Framework.Sources.SimpleWebSource($@"\\bsbrsp1010\apps\MI\RELATORIOS\ClickOnceExtratorPdf\NAppUpdate.xml");
                updateManager.ReinstateIfRestarted();

                if (updateManager.State == NAppUpdate.Framework.UpdateManager.UpdateProcessState.NotChecked)
                {
                    updateManager.CheckForUpdates();
                    if (updateManager.UpdatesAvailable > 0)
                    {
                        updateManager.PrepareUpdates();
                        updateManager.ApplyUpdates(true);
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar atualizar a ferramenta\nDescrição: {exc.Message}", "Erro de atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSelectFolder_Load(object sender, EventArgs e)
        {
            //UpdateApp();

            comboBoxTela.SelectedIndex = 4;
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnDuplicata, "Filtra os arquivos com base no PONTEIRO e remove duplicidade de pdfs das VM's.");
#if DEBUG
            button1.Visible = button10.Visible = button9.Visible = button2.Visible = button3.Visible = button4.Visible = button5.Visible = button6.Visible = button7.Visible = button8.Visible = true;
#endif

            string _pathDamp = string.Format("{0}{1}", Directory.GetCurrentDirectory(), @"\config\DAMP03.TXT");
            using (StreamReader sw = new StreamReader(_pathDamp, Encoding.Default))
            {
                while (!sw.EndOfStream)
                    lstDamp3.Add(sw.ReadLine());

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FrmTela18 f = new FrmTela18(@"D:\PDFSTombamento\Exceptions18", @"D:\PDFSTombamento\txt", "TELA18");
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmTela18 f = new FrmTela18(@"D:\testes\", @"D:\testes\ALTA", "TELA18");
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmTela20 f = new FrmTela20(@"D:\PDFSTombamento\Exceptions20", @"D:\PDFSTombamento\txt", "TELA20");
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmTela20 f = new FrmTela20(@"D:\testes\", @"D:\testes\ALTA", "TELA20");
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmTela25 f = new FrmTela25(@"D:\PDFSTombamento\Exceptions25", @"D:\PDFSTombamento\txt", "TELA25");
            f.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmTela25 f = new FrmTela25(@"C:\TombamentoV1_01\ENSAIO2019-10-04\", @"D:\PDFSTombamento\txt", "TELA25");
            f.ShowDialog();
        }

        private void BtnDuplicata_Click(object sender, EventArgs e)
        {
            if (comboBoxTela.SelectedIndex == 4)
            {
                MessageBox.Show("Selecione um tipo de tela.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            consistencia[comboBoxTela.SelectedIndex] = true;

            Text += "-" + comboBoxTela.Text;
            panelSpinner.Visible = !panelSpinner.Visible;
            FrmDuplicadoFiltro f = new FrmDuplicadoFiltro(textOrigemContratosPdf.Text, comboBoxTela.Text, lstDamp3);
            f.ShowDialog();
            panelSpinner.Visible = !panelSpinner.Visible;
            Text = Text.Split('-')[0];

            btnIniciarConvercao.Enabled = consistencia.ToList().TrueForAll(iis => iis);

        }

        private void comboBoxTela_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTela.SelectedIndex.Equals(4))
                btnDuplicata.Enabled = false;
            else
                btnDuplicata.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {

            bool[] x = { true, false, false, false };

            for (int i = (comboBoxTela.SelectedIndex-1) ; i > 0 ; i--)
            {
                if (!x[i])
                {
                    break;
                }
            }
            //List<string> lstArquiPoint = new List<string>();
            //using (StreamReader sw = new StreamReader(@"D:\HomologacaoFerramenta\config\ARQUPONT.TXT", Encoding.UTF8))
            //{
            //    while (!sw.EndOfStream)
            //        lstArquiPoint.Add(sw.ReadLine());
            //}

            //List<string> lst16 = Directory.GetFiles(@"D:\HomologacaoFerramenta", "*_16.pdf", SearchOption.AllDirectories).ToList();
            //    List<string> listaTela16 = new List<string>();
            //    lst16.ForEach(h => { listaTela16.Add(new FileInfo(h).Name.Split('_')[0].Trim()); });

            ////string[] l1 = { "1", "2", "3", "4", "5", "6","7", "9", "10", "11" };
            ////string[] l2 = { "1", "2", "3", "4", "5", "8" };

            ////var result = l1.GroupJoin(l2, k => k, y => y, (k, y) => new { t1 = k, t2 = y.FirstOrDefault()}).ToList();
            //var result1 = lstArquiPoint.GroupJoin(listaTela16, k => k, y => y, (k, y) => new { t1 = k, t2 = y.FirstOrDefault()}).Where(g => string.IsNullOrWhiteSpace(g.t1)).ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmConsolidacaoAlta f = new FrmConsolidacaoAlta(@"C:\TombamentoV1_01\ALTA\", @"C:\TombamentoV1_01\ENSAIO2019-10-04");
            f.ShowDialog();
        }
    }
}
