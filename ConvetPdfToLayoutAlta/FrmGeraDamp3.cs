using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmGeraDamp3 : Form
    {
        string arquivoCTO068A = string.Empty;
        string tmp = string.Empty;
        Stopwatch stopwatch = new Stopwatch();
        UserObject obj = null;
        int countpercent = 0, MaximumProgress = 0, countDamp = 0;
        List<string> listContratoDamp = new List<string>();

        public FrmGeraDamp3(string _arquivoCTO068A, List<string> lstDamp3)
        {
            InitializeComponent();
            arquivoCTO068A = _arquivoCTO068A;
            listContratoDamp = lstDamp3;
            countDamp = lstDamp3.Count;
        }

        private void backgroundWorkerDamp3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory()+@"\config\RELADAMP.txt", Encoding.Default, true))
                {
                    string _linha = string.Empty;
                    List<string> _arrayLinha = sr.ReadToEnd().Split('\n').Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
                    string[] _arrayItem = { };

                    //_arrayLinha.RemoveAt(0);
                    MaximumProgress = _arrayLinha.Count();

                    _arrayLinha.ForEach(p =>
                    {
                        _linha = Encoding.ASCII.GetString(Encoding.Convert(Encoding.ASCII, Encoding.ASCII, Encoding.ASCII.GetBytes(p)));
                        _arrayItem = _linha.Split(';').Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                        if (_arrayItem.Any(t => t.Contains("PGTO")))
                            if (_arrayItem.Any(t => t.Contains("ABERTURA")))
                                if (!listContratoDamp.Any(c => c.Equals(_arrayItem[0].Split('-')[0].Trim())))
                                    listContratoDamp.Add(_arrayItem[0].Split('-')[0].Trim());

                        countpercent++;
                        obj = new UserObject() { DescricaoPercentural = "Realizando leitura de arquivo e consistindo filtro  -  Contrato: " + _arrayItem[0].Split('-')[0].Trim(), TotalArquivoPorPasta = MaximumProgress };
                        backgroundWorkerDamp3.ReportProgress(countpercent, obj);
                        Thread.Sleep(1);
                    });
                }

                obj = new UserObject() { DescricaoPercentural = "Ordenando Lista de contratos filtrados....aguarde!", TotalArquivoPorPasta = MaximumProgress };
                backgroundWorkerDamp3.ReportProgress(countpercent, obj);

                Thread.Sleep(2000);
                countpercent = 0;
                MaximumProgress = listContratoDamp.Count();
                listContratoDamp.OrderBy(o => o).ToList();

                // se entrar um novo contrado na lista de Damp atual, então atualiza o arquivo de damp
                if (listContratoDamp.Count != countDamp)
                {
                    FileInfo f = new FileInfo(Directory.GetCurrentDirectory() + @"\config\DAMP03.TXT");

                    if (f.Exists)
                        f.Delete();

                    using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\config\DAMP03.TXT"))
                    {
                        listContratoDamp.ForEach(n =>
                        {
                            sw.WriteLine(n);
                            countpercent++;
                            obj = new UserObject() { DescricaoPercentural = $"Atualizando Arquivo de Damp - Contratos: {n}", TotalArquivoPorPasta = MaximumProgress };
                            backgroundWorkerDamp3.ReportProgress(countpercent, obj);
                            Thread.Sleep(1);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no processo para gerar o arquivo Damp\nErro: " + ex.Message);
            }
        }

        private void FrmGeraDamp3_Load(object sender, EventArgs e)
        {
            lblPendente.Text = "";
            IncrementaRegistroDamp();
          //  stopwatch.Restart();
         //   backgroundWorkerDamp3.RunWorkerAsync();

        }

        private void backgroundWorkerDamp3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

                progressBarReaderPdf.Maximum = MaximumProgress;
                progressBarReaderPdf.Value = e.ProgressPercentage;
                tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
                lblQtd.Text = string.Format("Total de Registros: {0}  ", obj.TotalArquivoPorPasta);
                lblLidos.Text = e.ProgressPercentage.ToString();
                lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
                lblTempo.Text = tmp;
                lblPendente.Text = string.Format("{0}", obj.DescricaoPercentural);
            }
            catch (Exception exProge)
            {
                MessageBox.Show("Ocorreu um erro ao tentar Atualizar o arquivo DAMP03\n" + exProge.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void backgroundWorkerDamp3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            MessageBox.Show("Arquivo de Damp Atualizado!!!", "Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }


        void IncrementaRegistroDamp()
        {
            try
            {
                List<RelaDamp> lstRelaDamp = new List<RelaDamp>();
                using (StreamReader streamReader = new StreamReader(arquivoCTO068A, Encoding.Default))
                {
                    streamReader.ReadLine();
                    string[] linha = null;

                    RelaDamp objDamp = null;
                    while (!streamReader.EndOfStream)
                    {
                        linha = streamReader.ReadLine().Split(';');
                        if (linha.Length < 11)
                            continue;

                        objDamp = new RelaDamp()
                        {
                            MIECDAMP_CONTRATO = linha[3].Trim(),
                            MIECDAMP_DT_ABERT = Convert.ToDateTime(linha[1].Trim()).ToString("yyyy-MM-dd"),
                            MIECDAMP_TP_OPER = linha[2].Trim(),
                            MIECDAMP_TT_FGTS = Regex.Replace(linha[4].Trim(), @"[^0-9$]", ""),
                            MIECDAMP_AMB_OPER = linha[5].Trim(),
                            MIECDAMP_CTA_EMPR = linha[8].Trim(),
                            MIECDAMP_PIS_PASEP = linha[9].Trim(),
                            MIECDAMP_CTA_TRAB = linha[10].Trim(),
                            MIECDAMP_VL_UTILZ = Regex.Replace(linha[11].Trim(), @"[^0-9$]", ""),
                            MIECDAMP_STATUS = linha[6].Trim(),
                            MIECDAMP_TP_REQUS = linha[7].Trim(),
                            MIECDAMP_FILLER = linha[0].Trim().Substring(6, 2),
                            MIECDAMP_NR_DAMP = linha[2].Trim(),

                        };

                        lstRelaDamp.Add(objDamp);
                    }

                }


                using (StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory()+@"\config\RELADAMP.txt", true, Encoding.Default))
                {
                    string linhaFormatada = string.Empty;

                    lstRelaDamp.ForEach(g => {

                        linhaFormatada += g.MIECDAMP_CONTRATO.Trim() + g.MIECDAMP_DT_ABERT.Trim() + g.MIECDAMP_TP_OPER.Trim().PadRight(50, ' ');
                        linhaFormatada += g.MIECDAMP_TT_FGTS.Trim().PadLeft(18, '0') + g.MIECDAMP_AMB_OPER.Trim().PadRight(50, ' ');
                        linhaFormatada += g.MIECDAMP_CTA_EMPR.Trim().PadLeft(16, '0') + g.MIECDAMP_PIS_PASEP.Trim().PadLeft(11, '0');
                        linhaFormatada += g.MIECDAMP_CTA_TRAB.Trim().Substring(3) + g.MIECDAMP_VL_UTILZ.Trim().PadLeft(18, '0');
                        linhaFormatada += g.MIECDAMP_STATUS.Trim().PadRight(30, ' ') + g.MIECDAMP_TP_REQUS.Trim().PadRight(12, ' ');
                        linhaFormatada += g.MIECDAMP_FILLER.Trim();

                        streamWriter.WriteLine(linhaFormatada);
                        linhaFormatada = string.Empty;
                    });
                }

            }
            catch (Exception exDamp)
            {
                MessageBox.Show("Ocorreu um erro ao tentar Atualizar o arquivo RELADAMP\n" + exDamp.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
