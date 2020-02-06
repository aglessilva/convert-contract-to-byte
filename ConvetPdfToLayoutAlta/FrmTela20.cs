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
    public partial class FrmTela20 : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        int contador = 0, totalArquivo = 0, totalPorPasta = 0 , countpercent = 0;
        IEnumerable<string> listContratoBlockPdf = null;
        IEnumerable<string> listDiretory = null;


        string diretorioOrigemPdf, diretorioDestinoLayout, tmp, tela;

        private void FrmTela20_Load(object sender, EventArgs e)
        {
#if !DEBUG
                if (!Directory.Exists(diretorioDestinoLayout))
                    Directory.CreateDirectory(diretorioDestinoLayout);
#endif
            try
            {
                listDiretory = Directory.GetDirectories(string.Format(@"{0}", diretorioOrigemPdf), tela, SearchOption.AllDirectories);

                if (listDiretory.Count() == 0)
                {
                  //  MessageBox.Show(string.Format("No diretório informado, não existe {0} para efetuar extração.", tela), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                listDiretory.ToList().ForEach(t =>
                {
                    totalArquivo += Directory.EnumerateFiles(string.Format(@"{0}", t), "*_20.pdf", SearchOption.AllDirectories).Count();
                });

                if (totalArquivo == 0)
                {
                    //MessageBox.Show("No diretório informado, não existe contratos(pdf) para extração.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                lblContrato.Text = "-";
                lblTempo.Text = "";
                lblPendente.Text = "";
                lblQtd.Text = "Total: " + totalArquivo.ToString();
                progressBarReaderPdf.Maximum = totalArquivo;

                BackgroundWorkerTela20.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar iniciar o processo de leitura de contrato: " + ex.Message);
            }

            stopwatch.Restart();
        }

        public FrmTela20(string _diretoioPdf, string _diretorioDestino, string _tela)
        {
            diretorioOrigemPdf = _diretoioPdf; diretorioDestinoLayout = _diretorioDestino; tela = Regex.Replace(_tela, @"[^A-Z0-9$]", "");
            InitializeComponent();
        }

        private void BackgroundWorkerTela20_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        private void BackgroundWorkerTela20_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //int err = Directory.EnumerateFiles(string.Format(@"{0}\", diretorioOrigemPdf), "*_20.err", SearchOption.TopDirectoryOnly).Count();
            //if (isErro)
            //{
            //    string result = string.Format("Resultado\n\n");
            //    result += string.Format("Total de Contratos: {0}\n", totalArquivo);
            //    result += string.Format("Total Processados: {0}\n", (totalArquivo - ExceptionError.countError));
            //    result += string.Format("Total Erros: {0}\n", ExceptionError.countError);
            //    result += string.Format("Total Arq. Rejeitado: {0}\n", err);
            //    result += string.Format("{0}", lblTempo.Text);
            //    MessageBox.Show(result, "Erro de Converção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else
            //{
            //    string result = string.Format("Resultado\n\n");
            //    result += string.Format("Total de Contratos: {0}\n", totalArquivo);
            //    result += string.Format("Total Processados: {0}\n", totalArquivo - err);
            //    result += string.Format("Total Arq. Rejeitado: {0}\n", err);
            //    result += string.Format("{0}", lblTempo.Text);
            //    MessageBox.Show(result, "Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            Close();
        }

        private void BackgroundWorkerTela20_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] _Campos = { "Atualizado", "SANTANDER", "Moratórios", "Motivo" };
            string[] _chave = { "Complemento", "Moratórios", "Histórico", "Suporte", "CTFIN/O020A", "Líquido" };
            string[] _ArrayLinha = { };
            string pagina = string.Empty, readerContrato = string.Empty;
            int numberPage = 0;

            bool isBody = false, isNotTela20 = false;

            List<Tela20> lst20 = new List<Tela20>();
            bool _gatilho = false;
            Tela20 tela20 = null;
            BusinessTela20 businessTela20 = new BusinessTela20();

            FileInfo arquivoPdf = null;

            listDiretory.ToList().ForEach(d =>
            {
                listContratoBlockPdf = Directory.EnumerateFiles(string.Format(@"{0}", d), "*_20.pdf", SearchOption.TopDirectoryOnly);

                if (listContratoBlockPdf.Count() > 0)
                {
                    totalPorPasta = listContratoBlockPdf.Count();

                    listContratoBlockPdf.ToList().ForEach(w =>
                    {
                        try
                        {
                            arquivoPdf = new FileInfo(w);
                            using (PdfReader reader = new PdfReader(w))
                            {
                                ITextExtractionStrategy its;
                                pagina = string.Empty;
                                tela20 = new Tela20();
                                isBody = false;
                                isNotTela20 = false;

                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    numberPage = i;

                                    its = new LocationTextExtractionStrategy();
                                    pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                                    pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));

                                    using (StringReader strReader = new StringReader(pagina))
                                    {
                                        string line = strReader.ReadLine();
                                        bool isDefault = line.Contains("Nº");

                                        while ((line = strReader.ReadLine()) != null)
                                        {
                                            if (line.Contains("Erros")) break;

                                            _ArrayLinha = businessTela20.GetArrayLine(line);

                                            if (i == 1 && !isBody)
                                            {
                                                if (_ArrayLinha.Any(ctn => ctn.Trim().Contains("CTFIN")))
                                                {
                                                    if (!_ArrayLinha.Any(ctn => ctn.Trim().Equals("CTFIN/O020A")))
                                                    {
                                                        isNotTela20 = true;
                                                        ExceptionError.TrataErros(arquivoPdf.Name, "O Arquivo não é do tipo CTFIN/O020A", diretorioDestinoLayout);
                                                        break;
                                                    }
                                                }
                                            }
                                          
                                            if (isDefault)
                                            {
                                                // Ignora os campos que não devem ser lidos e lê a proxima linha
                                                if (_ArrayLinha.Any(k => _Campos.Any(p => k.Equals(p))))
                                                {
                                                    isBody = true;
                                                    if (_ArrayLinha.Any(n => n.Trim().Equals("Moratórios")))
                                                    {

                                                        tela20.Acrescimo = Regex.Replace(line.Split(':')[1], @"[^0-9$]", "");
                                                        tela20 = businessTela20.GetValores(tela20, strReader.ReadLine() + " ValorPago");
                                                    }
                                                    else
                                                        tela20 = businessTela20.GetValores(tela20, line);
                                                }

                                                if (tela20.IsOk)
                                                {
                                                    lst20.Add(tela20);
                                                    break;
                                                }
                                            }
                                            else
                                            {

                                                if (_gatilho)
                                                {
                                                    isBody = true;
                                                    if (tela20.Contrato == null)
                                                    {
                                                        tela20.Carteira = _ArrayLinha[3];
                                                        tela20.Contrato = Regex.Replace(_ArrayLinha[5], @"[^0-9$]", "");

                                                        _gatilho = _ArrayLinha.Any(k => _chave.Any(p => k.Equals(p)));
                                                        continue;
                                                    }

                                                    if (tela20.ValorComplemento == null)
                                                    {
                                                        tela20.ValorComplemento = Regex.Replace(_ArrayLinha[5], @"[^0-9$]", "");
                                                        _gatilho = _ArrayLinha.Any(k => _chave.Any(p => k.Equals(p)));
                                                        continue;
                                                    }

                                                    if (tela20.Liquido == null)
                                                    {
                                                        tela20.Liquido = Regex.Replace(_ArrayLinha[0], @"[^0-9$]", "");
                                                        tela20.ValorAtualizado = Regex.Replace(_ArrayLinha[1], @"[^0-9\-$]", "");
                                                        _gatilho = _ArrayLinha.Any(k => _chave.Any(p => k.Equals(p)));
                                                        continue;
                                                    }

                                                    if (tela20.Acrescimo == null)
                                                    {
                                                        tela20.Acrescimo = Regex.Replace(_ArrayLinha[0], @"[^0-9$]", "");
                                                        _gatilho = _ArrayLinha.Any(k => _chave.Any(p => k.Equals(p)));
                                                        continue;
                                                    }

                                                    if (tela20.ValorPago == null)
                                                    {
                                                        tela20.ValorPago = Regex.Replace(_ArrayLinha[0], @"[^0-9$]", "");
                                                        _gatilho = _ArrayLinha.Any(k => _chave.Any(p => k.Equals(p)));
                                                        continue;
                                                    }

                                                    if (tela20.MotivoRejeicao == null)
                                                    {
                                                        if (char.IsNumber(_ArrayLinha.LastOrDefault(), 0))
                                                        {
                                                            var x = _ArrayLinha.ToList();
                                                            x.RemoveAt((_ArrayLinha.Length - 1));
                                                            x.RemoveAt((_ArrayLinha.Length - 2));
                                                            _ArrayLinha = x.ToArray();
                                                        }

                                                        tela20.MotivoRejeicao = string.Join(" ", _ArrayLinha).Replace("-", "").Replace("Observação", "").Trim();
                                                        _gatilho = _ArrayLinha.Any(k => _chave.Any(p => k.Equals(p)));
                                                        tela20.IsOk = true;
                                                        continue;
                                                    }
                                                }
                                                _gatilho = _ArrayLinha.Any(k => _chave.Any(p => k.Equals(p)));

                                                if (tela20.IsOk)
                                                {
                                                    lst20.Add(tela20);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (isNotTela20)
                                        return;
                                }
                                obj = new UserObject { Contrato = tela20.Contrato, PdfInfo = arquivoPdf, TotalArquivoPorPasta = totalPorPasta };

                                tela20 = null;
                                contador++;
                                countpercent++;

                                if (contador == 1000)
                                {
                                    BackgroundWorkerTela20.ReportProgress(contador, obj);

                                    var tab = new
                                    {
                                        item1 = lst20,
                                        item2 = diretorioDestinoLayout,
                                    };

                                    _thread = new Thread(new ParameterizedThreadStart(businessTela20.Populatela20));
                                    _thread.Start(tab);

                                    lst20 = new List<Tela20>();
                                    contador = 0;
                                }
                                else
                                    BackgroundWorkerTela20.ReportProgress(contador, obj);

                            }
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            BackgroundWorkerTela20.ReportProgress(countpercent, null);

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

                            ExceptionError.RemoverTela(arquivoPdf, diretorioOrigemPdf);
                        }

                        catch (iTextSharp.text.exceptions.InvalidPdfException pdfExeception)
                        {
                            BackgroundWorkerTela20.ReportProgress(countpercent, null);

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


            if (lst20.Count > 0)
            {
                BackgroundWorkerTela20.ReportProgress(contador, obj);

                var tab = new
                {
                    item1 = lst20,
                    item2 = diretorioDestinoLayout,
                };

                if (_thread != null)
                    if (_thread.ThreadState == System.Threading.ThreadState.Running)
                        _thread.Join();

                businessTela20.Populatela20(tab);

                lst20 = new List<Tela20>();
                contador = 0;
            }
        }
    }
}
