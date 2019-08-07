using ConvetPdfToLayoutAlta.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmTela25 : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        int contador = 0, totalArquivo = 0, totalPorPasta = 0;
        bool isErro = false;
        int countError = 0, countpercent = 0;
        IEnumerable<string> listContratoBlockPdf = null;
        IEnumerable<string> listDiretory = null;


        string diretorioOrigemPdf, diretorioDestinoLayout, tmp, tela;

        private void FrmTela25_Load(object sender, EventArgs e)
        {
            try
            {
#if !DEBUG
                if (!Directory.Exists(diretorioDestinoLayout))
                    Directory.CreateDirectory(diretorioDestinoLayout);
#endif

                listDiretory = Directory.GetDirectories(string.Format(@"{0}", diretorioOrigemPdf), tela, SearchOption.AllDirectories);

                if (listDiretory.Count() == 0)
                {
                    MessageBox.Show(string.Format("No diretório informado, não existe {0} para efetuar extração.", tela), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                listDiretory.ToList().ForEach(t =>
                {
                    totalArquivo += Directory.EnumerateFiles(string.Format(@"{0}", t), "*_25.pdf", SearchOption.AllDirectories).Count();
                });

                if (totalArquivo == 0)
                {
                    MessageBox.Show("No diretório informado, não existe contratos(pdf) para extração.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                lblContrato.Text = "-";
                lblTempo.Text = "";
                lblPendente.Text = "";
                lblQtd.Text = "Total: " + totalArquivo.ToString();
                progressBarReaderPdf.Maximum = totalArquivo;

                BackgroundWorkerTela25.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar iniciar o processo de leitura de contrato: " + ex.Message);
            }

            stopwatch.Restart();
        }

        public FrmTela25(string _diretoioPdf, string _diretorioDestino, string _tela)
        {
            diretorioOrigemPdf = _diretoioPdf; diretorioDestinoLayout = _diretorioDestino; tela = Regex.Replace(_tela, @"[^A-Z0-9$]", "");
            InitializeComponent();
        }

        private void BackgroundWorkerTela25_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            obj = (UserObject)e.UserState;

            lblLidos.Text = string.Format("Contratos lidos: {0}", e.ProgressPercentage.ToString());
            progressBarReaderPdf.Value = e.ProgressPercentage;

            if (obj != null)
            {
                tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
                lblQtd.Text = string.Format("Total de Contratos: {0} - Pendente: {1}", totalArquivo, (totalArquivo - e.ProgressPercentage));
                lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
                lblTempo.Text = tmp;
                lblContrato.Text = string.Format("Contrato: {0}", obj.Contrato);
                lblPendente.Text = string.Format("Arquivo: {0}", (string.IsNullOrWhiteSpace(obj.DescricaoPercentural) ? obj.PdfInfo.Name : obj.DescricaoPercentural));
                lblPasta.Text = string.Format("Diretorio: {0} - Total: {1}", obj.PdfInfo.Directory.Parent, obj.TotalArquivoPorPasta.ToString());
            }

        }

        private void BackgroundWorkerTela25_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isErro)
            {
                string result = string.Format("Resultado\n\n");
                result += string.Format("Total de Contratos: {0}\n", totalArquivo);
                result += string.Format("Total Processados: {0}\n", (totalArquivo - countError));
                result += string.Format("Total Rejeitados: {0}\n", countError);
                result += string.Format("{0}", lblTempo.Text);
                MessageBox.Show(result, "Erro de Converção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int err = 0;
                if (Directory.Exists(string.Format(@"{0}\!Erro", diretorioOrigemPdf)))
                {
                    err = Directory.EnumerateFiles(string.Format(@"{0}\!Erro", diretorioOrigemPdf), "*_25.pdf", SearchOption.TopDirectoryOnly).Count();
                }

                string result = string.Format("Resultado\n\n");
                result += string.Format("Total de Contratos: {0}\n", totalArquivo);
                result += string.Format("Total Processados: {0}\n", (totalArquivo - err));
                result += string.Format("Total Rejeitados: {0}\n", err);
                result += string.Format("{0}", lblTempo.Text);
                MessageBox.Show(result, "Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void BackgroundWorkerTela25_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> _Campos = new List<string>() { "Nome", "SANTANDER", "Empreendimento", "CPF" };
            string[] _ArrayLinha = { }, _ignraCampo = {"Page"};

            string pagina = string.Empty;
            int numberPage = 0;
            bool isBody = false, isNotTela25 = false;

            List<Tela25> lst25 = new List<Tela25>();
            Tela25 tela25 = null;
            BusinessTela25 businessTela25 = new BusinessTela25();

            FileInfo arquivoPdf = null;

            listDiretory.ToList().ForEach(d =>
            {
                listContratoBlockPdf = Directory.EnumerateFiles(string.Format(@"{0}", d), "*_25.pdf", SearchOption.TopDirectoryOnly);

                if (listContratoBlockPdf.Count() > 0)
                {
                    totalPorPasta = listContratoBlockPdf.Count();

                    listContratoBlockPdf.ToList().ForEach(w =>
                    {
                        arquivoPdf = new FileInfo(w);
                        using (PdfReader reader = new PdfReader(w))
                        {
                            ITextExtractionStrategy its;
                            pagina = string.Empty;
                            tela25 = new Tela25();
                            isBody = false;
                            isNotTela25 = false;

                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                numberPage = i;

                                its = new LocationTextExtractionStrategy();
                                pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                                pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));

                                using (StringReader strReader = new StringReader(pagina))
                                {
                                    string line = string.Empty;

                                    while ((line = strReader.ReadLine()) != null)
                                    {
                                        try
                                        {
                                            _ArrayLinha = businessTela25.GetArrayLine(line);
                                            if (i == 1 && !isBody)
                                            {
                                                if (_ArrayLinha.Any(ctn => ctn.Trim().Contains("CTFIN")))
                                                {
                                                    if (!_ArrayLinha.Any(ctn => ctn.Trim().Equals("CTFIN/O025A")))
                                                    {
                                                        isNotTela25 = true;
                                                        isErro = true;
                                                        countError++;
                                                        ExceptionError.TrataErros(arquivoPdf.Name, "O Arquivo não é do tipo CTFIN/O025A", diretorioDestinoLayout);
                                                        break;
                                                    }
                                                }
                                            }

                                            if (line.Contains("Movimento"))
                                            {
                                                isBody = true;

                                                _ArrayLinha = businessTela25.GetArrayLine(strReader.ReadLine());
                                                var x = _ArrayLinha.ToList();
                                                x.RemoveAll(r => char.IsPunctuation(r, 0));
                                                x.Add("Movimento");
                                                _ArrayLinha = x.ToArray();
                                                _Campos.Add("Movimento");

                                            }

                                            if (line.Contains("Vencimento"))
                                            {
                                                _ArrayLinha = businessTela25.GetArrayLine(strReader.ReadLine());
                                                var x = _ArrayLinha.ToList();
                                                x.RemoveAll(r => char.IsPunctuation(r, 0));
                                                x.Add("Vencimento");
                                                _ArrayLinha = x.ToArray();
                                                _Campos.Add("Vencimento");
                                            }

                                            if (_Campos.Any(c => _ArrayLinha.Any(l => l.Equals(c))))
                                                tela25 = businessTela25.TrataBoletim(tela25, _ArrayLinha);

                                            if (_ArrayLinha.Any(cc => Regex.IsMatch(cc.Trim(), @"(^\d{3}.\d{3}.\d{3}\-\d{2}$)")) || _ArrayLinha.Any(cc => Regex.IsMatch(cc.Trim(), @"(^\d{6}.\d{1}$)")))
                                                if (tela25.Agencia == null || tela25.Cpf == null)
                                                    tela25 = businessTela25.TrataBoletim(tela25, _ArrayLinha);

                                            if (tela25.IsFinal)
                                                break;
                                        }
                                        catch (ArgumentOutOfRangeException ex)
                                        {
                                            countError++;
                                            BackgroundWorkerTela25.ReportProgress(contador, null);

                                            isErro = true;
                                            if (!File.Exists(diretorioDestinoLayout + @"\LogErroContratos.txt"))
                                            {
                                                StreamWriter item = File.CreateText(diretorioDestinoLayout + @"\LogErroContratos.txt");
                                                item.Dispose();
                                            }
                                            using (StreamWriter sw = new StreamWriter(diretorioDestinoLayout + @"\LogErroContratos.txt", true, Encoding.UTF8))
                                            {
                                                StringBuilder strErro = new StringBuilder();
                                                strErro.AppendLine(string.Format("ARQUIVO: {0}", arquivoPdf.Name))
                                                        .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                                        .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", ex.Message))
                                                        .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                                                sw.Write(strErro);
                                                sw.WriteLine("================================================================================================================================================");
                                            }
                                        }
                                    }
                                }

                                if (isNotTela25)
                                    return;
                            }
                            obj = new UserObject { Contrato = tela25.Contrato, PdfInfo = arquivoPdf, TotalArquivoPorPasta = totalPorPasta };
                            lst25.Add(tela25);
                            tela25 = null;
                            contador++;
                            countpercent++;
                            if (contador == 1000)
                            {
                                BackgroundWorkerTela25.ReportProgress(countpercent, obj);

                                var tab = new
                                {
                                    item1 = lst25,
                                    item2 = diretorioDestinoLayout,
                                };

                                _thread = new Thread(new ParameterizedThreadStart(businessTela25.PopulaTela25));
                                _thread.Start(tab);

                                lst25 = new List<Tela25>();
                                contador = 0;
                            }
                            else
                                BackgroundWorkerTela25.ReportProgress(countpercent, obj);

                        }

                    });
                }
            });


            if (lst25.Count > 0)
            {
                BackgroundWorkerTela25.ReportProgress(countpercent, obj);

                var tab = new
                {
                    item1 = lst25,
                    item2 = diretorioDestinoLayout,
                };

                if (_thread != null)
                    if (_thread.ThreadState == System.Threading.ThreadState.Running)
                        _thread.Join();

                businessTela25.PopulaTela25(tab);

                lst25 = new List<Tela25>();
                contador = 0;
            }
        }
    }
}
