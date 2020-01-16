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
    public partial class FrmTela18 : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        int contador = 0, totalArquivo = 0, totalPorPasta = 0, countPercent = 0;
        bool isErro = false;
        IEnumerable<string> listContratoBlockPdf = null;
        IEnumerable<string> listDiretory = null;


        string diretorioOrigemPdf, diretorioDestinoLayout, tmp, tela;

        private void FrmTela18_Load(object sender, EventArgs e)
        {
#if !DEBUG
                if (!Directory.Exists(diretorioDestinoLayout))
                    Directory.CreateDirectory(diretorioDestinoLayout);
#endif
            try
            {
                ExceptionError.countError = 0;

                listDiretory = Directory.GetDirectories(string.Format(@"{0}", diretorioOrigemPdf), tela, SearchOption.AllDirectories);

                if (listDiretory.Count() == 0)
                {
                    MessageBox.Show(string.Format("No diretório informado, não existe {0} para efetuar extração.", tela), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                listDiretory.ToList().ForEach(t =>
                {
                    totalArquivo += Directory.EnumerateFiles(string.Format(@"{0}", t), "*_18.pdf", SearchOption.AllDirectories).Count();
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

                BackgroundWorkerTela18.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar iniciar o processo de leitura de contrato: " + ex.Message);
            }

            stopwatch.Restart();
        }

        public FrmTela18(string _diretoioPdf, string _diretorioDestino, string _tela)
        {
            diretorioOrigemPdf = _diretoioPdf; diretorioDestinoLayout = _diretorioDestino; tela = Regex.Replace(_tela, @"[^A-Z0-9$]", "");
            InitializeComponent();
        }



        private void BackgroundWorkerTela18_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        private void BackgroundWorkerTela18_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int err = Directory.EnumerateFiles(string.Format(@"{0}\", diretorioOrigemPdf), "*_18.err", SearchOption.TopDirectoryOnly).Count();
            if (isErro)
            {
                string result = string.Format("Resultado\n\n");
                result += string.Format("Total de Contratos: {0}\n", totalArquivo);
                result += string.Format("Total Processados: {0}\n", (totalArquivo - ExceptionError.countError));
                result += string.Format("Total Erros: {0}\n", ExceptionError.countError);
                result += string.Format("Total Arq. Rejeitado: {0}\n", err);
                result += string.Format("{0}", lblTempo.Text);
                MessageBox.Show(result, "Erro de Converção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string result = string.Format("Resultado\n\n");
                result += string.Format("Total de Contratos: {0}\n", totalArquivo);
                result += string.Format("Total Processados: {0}\n", totalArquivo - err);
                result += string.Format("Total Arq. Rejeitado: {0}\n", err);
                result += string.Format("{0}", lblTempo.Text);
                MessageBox.Show(result, "Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Close(); ;
        }

        private void BackgroundWorkerTela18_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] _ignoraCampo = { "Page", "Nº", "Demonstrativo", "Data", "Agência", "Obs", "Vencimento", "INI" };
            string[] _campo = { "UTI", "JAM", "QUO","SOB",  "SANTANDER" };
            string[] _ArrayLinha = { };
            string pagina = string.Empty, readerContrato = string.Empty;
            int numberPage = 0;
            BusinessTela18 bussinessTela18 = new BusinessTela18();

            List<Tela18> lstTela18 = new List<Tela18>();
            Tela18 tela18 = null;
            Damp damp = null;
            bool isBody = false , isNotTela18 = false;

            FileInfo arquivoPdf = null;

            listDiretory.ToList().ForEach(d =>
            {
                listContratoBlockPdf = Directory.EnumerateFiles(string.Format(@"{0}", d), "*_18.pdf", SearchOption.TopDirectoryOnly);

                if (listContratoBlockPdf.Count() > 0)
                {
                    totalPorPasta = listContratoBlockPdf.Count();

                    listContratoBlockPdf.ToList().ForEach(w =>
                    {
                        try
                        {
                            ParcelaFgts parcelaFgts = new ParcelaFgts();
                            arquivoPdf = new FileInfo(w);
                            damp = new Damp();
                            using (PdfReader reader = new PdfReader(w))
                            {
                                ITextExtractionStrategy its;
                                pagina = string.Empty;
                                tela18 = new Tela18();
                                isBody = false;
                                isNotTela18 = false;

                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    numberPage = i;
                                    its = new LocationTextExtractionStrategy();
                                    pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                                    pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));

                                    using (StringReader strReader = new StringReader(pagina))
                                    {
                                        string line;
                                        while ((line = strReader.ReadLine()) != null)
                                        {
                                            try
                                            {
                                                _ArrayLinha = bussinessTela18.GetArrayLine(line);

                                                if (i == 1 && !isBody)
                                                {
                                                    if (_ArrayLinha.Any(ctn => ctn.Trim().Contains("CTFIN")))
                                                    {
                                                        if (!_ArrayLinha.Any(ctn => ctn.Trim().Equals("CTFIN/O018A")))
                                                        {
                                                            isNotTela18 = true;
                                                            isErro = true;
                                                            ExceptionError.countError++;
                                                            ExceptionError.TrataErros(null, arquivoPdf.Name, "O Arquivo não é do tipo CTFIN/O018A", diretorioDestinoLayout);
                                                            break;
                                                        }
                                                    }
                                                }

                                                // Ignora os campos que não devem ser lidos e lê a proxima linha
                                                if (_ArrayLinha.Any(k => _ignoraCampo.Any(p => k.Equals(p))))
                                                    continue;
                                                if (_ArrayLinha.Any(x => Regex.IsMatch(x, @"(^\d{1,2}:\d{1,2}$)")))
                                                    continue;

                                                if (_ArrayLinha.Any(k => _campo.Any(p => k.Equals(p))))
                                                {
                                                    isBody = true;

                                                    if (_ArrayLinha.Any(c => Regex.IsMatch(c, @"^(\d{4}.\d{5}.\d{3}-\d{1})$")))
                                                    {
                                                        var _carteira = _ArrayLinha.FirstOrDefault(ct => Regex.IsMatch(ct, @"(^[A-Z]{2}.\d{4}-\w{1,10}$?)"));
                                                        var _contrato = _ArrayLinha.FirstOrDefault(c => Regex.IsMatch(c, @"^(\d{4}.\d{5}.\d{3}-\d{1})$"));

                                                        tela18.Carteira = Regex.Replace(_carteira, @"[^0-9$]", "");
                                                        tela18.Contrato = Regex.Replace(_contrato, @"[^0-9$]", "");
                                                        continue;
                                                    }

                                                    if (_ArrayLinha[0].Trim().Equals("UTI"))
                                                    {
                                                        damp = new Damp();
                                                        _ArrayLinha = _ArrayLinha.Where(x => Regex.IsMatch(x, @"[0-9]")).ToArray();
                                                        damp = bussinessTela18.GetDamp(_ArrayLinha);
                                                        tela18.Damps.Add(damp);

                                                        continue;
                                                    }
                                                    if (_ArrayLinha.Any(t => _campo.Any(c => t.Equals(c))))
                                                    {
                                                        if (_ArrayLinha[0].Trim().Equals("JAM"))
                                                            parcelaFgts = bussinessTela18.GetParcelaFgts(_ArrayLinha, parcelaFgts);
                                                        else
                                                        {
                                                            parcelaFgts = bussinessTela18.GetParcelaFgts(_ArrayLinha, parcelaFgts);
                                                            damp.ParcelaFgts.Add(parcelaFgts);
                                                            parcelaFgts = new ParcelaFgts();
                                                        }
                                                        continue;
                                                    }

                                                }
                                            }

                                            catch (Exception ex)
                                            {
                                                reader.Dispose();
                                                ExceptionError.countError++;
                                                BackgroundWorkerTela18.ReportProgress(countPercent, null);

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
                                                            .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", ex.Message + " [ou Contrato com número de DAMP ausente]"))
                                                            .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                                                    sw.Write(strErro);
                                                    sw.WriteLine("================================================================================================================================================");
                                                }

                                                ExceptionError.RemoverTela(arquivoPdf, diretorioOrigemPdf);
                                                break;
                                            }

                                        }

                                        if (ExceptionError.countError > 0)
                                            break;
                                    }

                                    if (isNotTela18)
                                        return;
                                }

                                if (tela18.Damps.Count == 0)
                                {
                                    isErro = true;
                                    ExceptionError.countError++;
                                    reader.Dispose();
                                    ExceptionError.SemNumeroDamp(arquivoPdf, diretorioDestinoLayout , diretorioOrigemPdf);
                                    return;
                                }
                                obj = new UserObject { Contrato = tela18.Contrato, PdfInfo = arquivoPdf, TotalArquivoPorPasta = totalPorPasta };
                                lstTela18.Add(tela18);
                                tela18 = null;
                                contador++;
                                countPercent++;

                                if (contador == 1000)
                                {
                                    BackgroundWorkerTela18.ReportProgress(countPercent, obj);

                                    var tab = new
                                    {
                                        item1 = lstTela18,
                                        item2 = diretorioDestinoLayout,
                                    };

                                    _thread = new Thread(new ParameterizedThreadStart(bussinessTela18.PopulaTela18));
                                    _thread.Start(tab);

                                    lstTela18 = new List<Tela18>();
                                    contador = 0;
                                }
                                else
                                    BackgroundWorkerTela18.ReportProgress(countPercent, obj);
                            }
                        }
                        catch (iTextSharp.text.exceptions.InvalidPdfException pdfExeception)
                        {
                            ExceptionError.countError++;
                            BackgroundWorkerTela18.ReportProgress(countPercent, null);

                            isErro = true;
                            if (!File.Exists(diretorioDestinoLayout + @"\LogErroContratos.txt"))
                            {
                                StreamWriter item = File.CreateText(diretorioDestinoLayout + @"\LogErroContratos.txt");
                                item.Dispose();
                            }
                            using (StreamWriter sw = new StreamWriter(diretorioDestinoLayout + @"\LogErroContratos.txt", true, Encoding.UTF8))
                            {
                                string Erro_ = string.Format("{0} - mensagem original: {1}", "Arquivo danificado, não é possivel fazer a leitura ", pdfExeception.Message);
                                StringBuilder strErro = new StringBuilder();
                                strErro.AppendLine(string.Format("ARQUIVO: {0}", arquivoPdf.Name))
                                        .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                        .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", Erro_))
                                        .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                                sw.Write(strErro);
                                sw.WriteLine("================================================================================================================================================");
                            }

                            ExceptionError.RemoverTela(arquivoPdf, diretorioOrigemPdf);
                        }
                    });
                }
            });

            if (lstTela18.Count > 0)
            {
                BackgroundWorkerTela18.ReportProgress(countPercent, obj);

                var tab = new
                {
                    item1 = lstTela18,
                    item2 = diretorioDestinoLayout,
                };

                if (_thread != null)
                    if (_thread.ThreadState == System.Threading.ThreadState.Running)
                        _thread.Join();

                bussinessTela18.PopulaTela18(tab);

                lstTela18 = new List<Tela18>();
                contador = 0;
            }
        }
    }
}
