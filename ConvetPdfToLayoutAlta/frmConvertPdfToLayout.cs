using ConvetPdfToLayoutAlta.Models;
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
        List<string> telas = new List<string> { "TELA 18", "TELA 16", "TELA 20", "TELA 25" };

        public FrmSelectFolder()
        {
            InitializeComponent();
        }

        private void BtnSelectDiretorioOrigem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (SelectFolders(1) == DialogResult.Yes)
                {
                    string[] _arquivos = { Directory.GetCurrentDirectory() + @"\config\SITU115A.TXT", Directory.GetCurrentDirectory() + @"\config\ARQ_GARANTIA.TXT", Directory.GetCurrentDirectory() + @"\config\ARQUPONT.TXT" };
                    string msg = string.Format("--- COPIE OS ARQUIVOS NO DIRETORIO ABAIXO ---\n\n");

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\config"))
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\config");

                    if (!Directory.Exists(textOrigemContratosPdf.Text + @"\ALTA"))
                        Directory.CreateDirectory(textOrigemContratosPdf.Text + @"\ALTA");

                    if (!File.Exists(Directory.GetCurrentDirectory() + @"\config\SITU115A.TXT"))
                    {
                        msg += string.Format("--> {0}", _arquivos[0]);
                        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (!File.Exists(Directory.GetCurrentDirectory() + @"\config\ARQ_GARANTIA.TXT"))
                    {
                        msg += string.Format("--> {0}", _arquivos[1]);
                        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    //btnDuplicata.Enabled = comboBoxTela.Enabled = (textDestinoLayout.TextLength > 0 && textOrigemContratosPdf.TextLength > 0);
                    btnDuplicata.Enabled = (textDestinoLayout.TextLength > 0 && textOrigemContratosPdf.TextLength > 0);
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
                textDestinoLayout.Text = textOrigemContratosPdf.Text + @"\ALTA";
            }
            else
                textDestinoLayout.Text = textOrigemContratosPdf.Text + @"\ALTA";

            return result;
        }

        private void BtnIniciarConvercao_Click(object sender, EventArgs e)
        {
            panelSpinner.Visible = !panelSpinner.Visible;
            telas.ForEach(t =>
            {
                Text += "-" + t + $" - (V{Application.ProductVersion})"; ;
                Form f = null;

                if (t.Equals("TELA 18"))
                    f = new FrmTela18(textOrigemContratosPdf.Text, textDestinoLayout.Text, t);

                if (t.Equals("TELA 16"))
                    f = new FrmTela16(textOrigemContratosPdf.Text, textDestinoLayout.Text, t);

                if (t.Equals("TELA 20"))
                    f = new FrmTela20(textOrigemContratosPdf.Text, textDestinoLayout.Text, t);

                if (t.Equals("TELA 25"))
                    f = new FrmTela25(textOrigemContratosPdf.Text, textDestinoLayout.Text, t);

                f.ShowDialog();
                Text = Text.Split('-')[0].Trim();
            });
            panelSpinner.Visible = !panelSpinner.Visible;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FrmTela16 f = new FrmTela16(textOrigemContratosPdf.Text, textDestinoLayout.Text, "TELA 16");
            f.ShowDialog();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FrmTela16 f = new FrmTela16(@"C:\@TombTesteUnitarios", @"C:\@TombTesteUnitarios\ALTA", "TELA16");
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

            // comboBoxTela.SelectedIndex = 4;
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
            FrmTela18 f = new FrmTela18(@"C:\@TombTesteUnitarios\", @"C:\@TombTesteUnitarios\ALTA", "TELA18");
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmTela18 f = new FrmTela18(@"C:\TombamentoV1_01\SIMULADOS\SIMULADO2019-11-20", @"C:\TombTesteUnitarios\ALTA", "TELA18");
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
            //bool isFilter = false;
            //if (comboBoxTela.SelectedIndex == 4)
            //{
            //    MessageBox.Show("Selecione um tipo de tela.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            // Inicia o processo para renomear os arquivos antes de filtrar
            FileInfo ffRename = new FileInfo(Directory.GetCurrentDirectory() + @"\config\ARQUPONT.txt");

            if (!ffRename.Exists)
            {
                DialogResult result = MessageBox.Show("Arquivo de PONTEIRO, não encontrado\nDeseja criar um arquivo de ponteiro a partir dos contratos?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    ExceptionError.GerarPonteiro(textOrigemContratosPdf.Text);
                else
                    return;
            }

            string _tela = string.Empty;
            panelSpinner.Visible = !panelSpinner.Visible;

            telas.ForEach(t =>
            {
                Text += " - " + t;
                _tela = Regex.Replace(t, @"[^0-9$]", "");
                var extencao = new List<string>() { $"*_{_tela}.dup", $"*_{_tela}.damp", $"*_{_tela}.fil", $"*_{_tela}.err", $"*_{_tela}.rej" };
                foreach (var item in extencao)
                {
                    if (Directory.EnumerateFiles(textOrigemContratosPdf.Text, item, SearchOption.AllDirectories).Count() > 0)
                    {
                        FrmRenomearPdf frmRenomear = new FrmRenomearPdf(textOrigemContratosPdf.Text, _tela);
                        frmRenomear.ShowDialog();
                        //panelSpinner.Visible = !panelSpinner.Visible;
                        // break;
                    }
                }

                // panelSpinner.Visible = !panelSpinner.Visible;
                //Text = Text.Split('-')[0];
                FrmDuplicadoFiltro f = new FrmDuplicadoFiltro(textOrigemContratosPdf.Text, t, lstDamp3);
                f.ShowDialog();

                //consistencia[comboBoxTela.SelectedIndex] = true;

                // btnIniciarConvercao.Enabled = consistencia.ToList().TrueForAll(iis => iis);

                Text = Text.Split('-')[0].Trim(); ;
            });

            panelSpinner.Visible = !panelSpinner.Visible;
            FileInfo fileInfo = new FileInfo(Directory.GetCurrentDirectory() + @"\DbTombamento.sdf");
            if (fileInfo.Exists)
                fileInfo.Delete();

            btnDuplicata.Enabled = false;
            btnIniciarConvercao.Enabled = true;
        }


        //private void comboBoxTela_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (comboBoxTela.SelectedIndex.Equals(4))
        //        btnDuplicata.Enabled = false;
        //    else
        //        btnDuplicata.Enabled = true;
        //}

        private void button9_Click(object sender, EventArgs e)
        {

            //bool[] x = { true, false, false, false };

            //for (int i = (comboBoxTela.SelectedIndex - 1); i > 0; i--)
            //{
            //    if (!x[i])
            //    {
            //        break;
            //    }
            //}

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
            FrmConsolidacaoAlta f = new FrmConsolidacaoAlta(textDestinoLayout.Text, textOrigemContratosPdf.Text);
            f.ShowDialog();
        }



        //private void btnLocalizar_Click(object sender, EventArgs e)
        //{
        //    openFileDialog1.Filter = "Arquivo de Damp|*.txt";
        //    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        textBoxDamp3.Text = openFileDialog1.FileName;
        //        btnDamp3.Enabled = true;
        //    }
        //}

        //private void btnDamp3_Click(object sender, EventArgs e)
        //{
        //    panelSpinner.Visible = !panelSpinner.Visible;
        //    FrmGeraDamp3 f = new FrmGeraDamp3(textBoxDamp3.Text, lstDamp3);
        //    f.ShowDialog();
        //    panelSpinner.Visible = !panelSpinner.Visible;
        //}



        private void btnLocalizarHistoricoParcela_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "08 Hist Parcelas|*.txt|Ocorrência|*.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxHistoricoParcelas.Text = openFileDialog1.FileName;
                button11.Enabled = true;
            }
        }



        private void gerarArquivoDeDamp3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label4.Text = "68 - Selecione o arquivo CTO068A.txt";
            button11.Text = "Atualizar Arquivo RelaDamp";
            Painel();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo = new FileInfo(textBoxHistoricoParcelas.Text);
            Painel();
            panelSpinner.Visible = !panelSpinner.Visible;

            Form f = null;
            if (label4.Text.Split('-')[0].Trim().Equals("8"))
            {
                if (!fileInfo.Name.Equals("ARQ.EXT.08.HIST.PARCELAS.TXT"))
                    MessageBox.Show("O conteúdo deste arquivo não é compativel com o arquivo Hitorico de Parcelas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                    f = new FrmHistoricoParcelas(textBoxHistoricoParcelas.Text);
            }

            if (label4.Text.Split('-')[0].Trim().Equals("16"))
            {
                if (!fileInfo.Name.Equals("TL16PARC.txt"))
                    MessageBox.Show("O conteúdo deste arquivo não é compativel com o arquivo de parcelas da tela 16", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                    f = new FrmCarregaParcelas(textBoxHistoricoParcelas.Text);
            }

            if (label4.Text.Split('-')[0].Trim().Equals("32"))
            {
                if (!fileInfo.Name.Equals("TL16OCOR.txt"))
                    MessageBox.Show("O conteúdo deste arquivo não é compativel com o arquivo de ocorrências da tela 16", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                    f = new FrmCarregaOcorrencia(textBoxHistoricoParcelas.Text);
            }

            if (label4.Text.Split('-')[0].Trim().Equals("68"))
            {
                if (!fileInfo.Name.Equals("CTO068A.txt"))
                    MessageBox.Show("O conteúdo deste arquivo não é compativel com o arquivo CTO068A.txt", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                {
                    f = new FrmGeraDamp3(textBoxHistoricoParcelas.Text, lstDamp3);
                    Painel(true);
                }
            }

            if (f != null)
                f.ShowDialog();

            panelSpinner.Visible = !panelSpinner.Visible;
        }

        private void MenuItemGravarHistoricoParcelas_Click(object sender, EventArgs e)
        {

            label4.Text = "8 - Selecione o arquivo ARQ.EXT.08.HIST.PARCELAS.TXT";
            button11.Text = "Gravar Histórico de Parcelas";
            button11.Width = 182;
            Painel();
        }

        private void consultarHistóricoDeParcelasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCabecalho frmCabecalho = new FrmCabecalho();
            frmCabecalho.ShowDialog();
        }

        private void gravarParcelasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            label4.Text = "16 - Selecione o arquivo TL16PARC.txt";
            button11.Text = "Gravar Parcelas (pdf)";
            button11.Width = 155;
            Painel();
        }

        private void consultaFgtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConsultaFgts f = new FrmConsultaFgts();
            f.ShowDialog();
        }

        private void consultarParcelasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParcelas frmParcelas = new FrmParcelas();
            frmParcelas.ShowDialog();
        }

        void Painel(bool _is = false)
        {
            groupBox1.Visible = _is;
            pnlHistoricoParcela.Visible = !groupBox1.Visible;
            voltarToolStripMenuItem.Enabled = !groupBox1.Visible;
        }

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Painel(true);
            textBoxHistoricoParcelas.Text = string.Empty;
        }

        private void gravarOcorrênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label4.Text = "32 - Selecione o arquivo TL16OCOR.txt";
            button11.Text = "Gravar Ocorrências (pdf)";
            button11.Width = 165;
            Painel();
        }

        private void consultarOcorrênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCabecalho frmCabecalho = new FrmCabecalho(0);
            frmCabecalho.ShowDialog();
        }

       
        private void gerarPonteiroFullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Selecione o diretório das VM's que contém os PDFs";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                panelSpinner.Enabled = !panelSpinner.Enabled;
                IEnumerable<string> fileContract = Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath, "*_16.pdf", SearchOption.AllDirectories);
                fileContract.ToList().Sort();
                FileInfo f = null;
                using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\config\ARQUPONT.txt", true, Encoding.ASCII))
                {
                    {
                        fileContract.ToList().ForEach(w =>
                        {
                            f = new FileInfo(w);

                            sw.WriteLine(f.Name.Split('_')[0]);
                        });

                    }

                }
                MessageBox.Show("Ponteiro Full criado com sucesso", "Ponteiro Full", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelSpinner.Enabled = !panelSpinner.Enabled;
            }
        }

        
    }
}

