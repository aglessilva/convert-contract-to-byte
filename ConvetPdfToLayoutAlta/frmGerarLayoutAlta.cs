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
    public partial class frmGerarLayoutAlta : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        List<string> _situacoesAtual = new List<string>();
        int contador = 0, countLote = 1, totalArquivo = 0, totalPorPasta = 0;
        bool isErro = false, isFinal = false;
        IEnumerable<string> listContratoBlockPdf = null;
        IEnumerable<string> listDiretory = null;
        string diretorioOrigemPdf, diretorioDestinoLayout, tmp, tela;

        public frmGerarLayoutAlta(string _diretoioPdf, string _diretorioDestino, string _tela)
        {
            diretorioOrigemPdf = _diretoioPdf; diretorioDestinoLayout = _diretorioDestino; tela = Regex.Replace(_tela, @"[^A-Z0-9$]", "");
            InitializeComponent();
        }

        private void FrmGerarLayoutAlta_Load(object sender, EventArgs e)
        {

            try
            {
                #if !DEBUG
                if (!Directory.Exists(diretorioDestinoLayout))
                    Directory.CreateDirectory(diretorioDestinoLayout);
                #endif
                listDiretory = Directory.GetDirectories(string.Format(@"{0}", diretorioOrigemPdf), tela, SearchOption.AllDirectories);

                if(listDiretory.Count() == 0)
                {
                    MessageBox.Show(string.Format( "No diretório informado, não existe {0} para efetuar extração.", tela), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                listDiretory.ToList().ForEach(t =>
                {
                    totalArquivo += Directory.EnumerateFiles(string.Format(@"{0}", t), "*.pdf", SearchOption.AllDirectories).Count();
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


                using (StreamReader lerTxt = new StreamReader(diretorioOrigemPdf + @"\config\SITU115A.TXT"))
                {
                    while (!lerTxt.EndOfStream)
                        _situacoesAtual.Add(lerTxt.ReadLine());
                };

                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar iniciar o processo de leitura de contrato: " + ex.Message);
            }

            stopwatch.Restart();
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            obj = (UserObject)e.UserState;

            lblLidos.Text = string.Format("Contratos lidos: {0}", e.ProgressPercentage.ToString());

            tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            progressBarReaderPdf.Value = e.ProgressPercentage;
            lblQtd.Text = string.Format("Total de Contratos: {0} - Pendente: {1}", totalArquivo, (totalArquivo - e.ProgressPercentage));
            lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
            lblTempo.Text = tmp;
            lblContrato.Text = string.Format("Contrato: {0}", obj.Contrato);
            lblPendente.Text = string.Format("Arquivo: {0}", (string.IsNullOrWhiteSpace(obj.DescricaoPercentural) ? obj.PdfInfo.Name : obj.DescricaoPercentural));
            lblPasta.Text = string.Format("Diretorio: {0} - Total: {1}", obj.PdfInfo.Directory.Parent, obj.TotalArquivoPorPasta.ToString());

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isErro)
                MessageBox.Show("Processo finalizado COM ERROS!", "Erro de Converção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                int err = 0;
                if (Directory.Exists(string.Format(@"{0}\!Erro", diretorioOrigemPdf)))
                {
                    err = Directory.EnumerateFiles(string.Format(@"{0}\!Erro", diretorioOrigemPdf), "*.pdf", SearchOption.TopDirectoryOnly).Count();
                }

                string result = string.Format("Resultado\n\n");
                result += string.Format("Total de Contratos: {0}\n", totalArquivo);
                result += string.Format("Total Processados: {0}\n",( totalArquivo - err));
                result += string.Format("Total Rejeitados: {0}\n", err );
                result += string.Format("{0}", lblTempo.Text);
                MessageBox.Show(result,"Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            int numberPage = 0,  countParcela = 0;
            List<string> lstSituacao = new List<string>();
            List<string> lstCronograma = new List<string>(); 
            List<string> lstGT = new List<string>();
            List<Cabecalho> lstCabecalho = null;
            List<Parcela> lstParcelas = null;
            List<Ocorrencia> lstOcorrencia = null;
            List<ContratoPdf> lstContratosPdf = new List<ContratoPdf>();
            UserObject userObject = null;

            StringBuilder strLayoutContrato = new StringBuilder();
            StringBuilder strLayoutOcorrencia = new StringBuilder();

            BusinessCabecalho businessCabecalho = null;
            BusinessParcelas businessParcelas = null;

            Cabecalho objCabecalho = null;
            Parcela objParcelas = null;
            ContratoPdf objContratoPdf = null;

            FileInfo arquivoPdf = null;

            string[] arrayIgnoraOcorrencia = { "Portabilidade", "BRADESCO","SAFRA", "ITAU","ITAÚ", "PACTUAL","BTG", "UNIBANCO","HSBC", "NORDESTE","SATNANDER","BNDS","CITIBANK","CITI", "CITY", "ECONOMICA", "FEDERAL" };
            string[] arrayIgnorParcelas = { "Encargo", "Índice", "Devedor", "Proc.Emi/Pag", "Mora", "Gerad", };
            string[] arrayIgnorCabecalho = { "End.Correspondência", "Demonstrativo", "Telefone", "Modalidade", "Nome", "Novo", };
            string[] arrayIgonorCampo = { "Nº", "CTFIN", "Emissão", "PRO","ANT", "Novo", "SANTANDER", "Carteira", "Data","CTFIN","Carteira","Contrato","v","Seguro","TAXA", };
            string[] arrayFinalParcela = { "TOTAL","Aberto", "SEGURO", "CONTRATO LIQUIDADO", "FGTS/Prestação", "QTD" };
            string pagina = string.Empty, readerContrato = string.Empty;
            string[] cabecalho = null;
            string[] arrayLinhaParcela = null;
            bool isParcelas = false, isNotiqual = false, hasTaxa = false, hasIof = false, isCabecalhoParcela = false;
            int padrao = 0, _countPercent = 0;


            using (StreamReader sr = new StreamReader(diretorioOrigemPdf + @"\config\ARQ_GARANTIA.TXT"))
            {
                while (!sr.EndOfStream)
                    lstGT.Add(sr.ReadLine().Trim());
            }

            businessCabecalho = new BusinessCabecalho();
            businessParcelas = new BusinessParcelas();
            lstCabecalho = new List<Cabecalho>();
            lstParcelas = new List<Parcela>();
            lstOcorrencia = new List<Ocorrencia>();

            listDiretory.ToList().ForEach(d =>
            {
                listContratoBlockPdf = Directory.EnumerateFiles(string.Format(@"{0}", d), "*.pdf", SearchOption.TopDirectoryOnly);
                if (listContratoBlockPdf.Count() == 0)
                    return;
                totalPorPasta = listContratoBlockPdf.Count();

                listContratoBlockPdf.ToList().ForEach(w =>
                {
                  
                    ITextExtractionStrategy its;
                    try
                    {
                        arquivoPdf = new FileInfo(w);
                        objContratoPdf = new ContratoPdf();
                        objCabecalho = new Cabecalho() { Id = (lstCabecalho.Count + 1) };
                        objParcelas = new Parcela() { IdCabecalho = objCabecalho.Id, Id = 0 };
                        objParcelas = businessParcelas.PreencheParcela(objParcelas);
                        objCabecalho = businessCabecalho.PreencheCabecalho(objCabecalho);

                        using (PdfReader reader = new PdfReader(w))
                        {
                            isFinal = false;
                            hasTaxa = false;
                            hasIof = false;
                            isParcelas = false;
                            isCabecalhoParcela = false;
                            

                            pagina = string.Empty;
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                numberPage = i;
                                padrao = 0;


                                its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                                pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                                pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));


                                if (!pagina.Substring(0, 10).Contains("Nº"))
                                    padrao = 1;

                            #region Padrão 1

                                using (StringReader strReader = new StringReader(pagina))
                                {
                                    string line;
                                    while ((line = strReader.ReadLine()) != null)
                                    {

                                        if (line.Trim().Equals("a") || line.Trim().Equals("***") || line.Length < 2) continue;
                                        if (string.IsNullOrWhiteSpace(line)) continue;
                                        if (Regex.IsMatch(line, @"(^\d{1,2}:\d{1,2}:\d{1,2}$)"))
                                            continue;

                                        if (!isCabecalhoParcela)
                                        {
                                            isCabecalhoParcela = line.Split(' ').Any(h => h.Equals("TAXA"));
                                            if (isCabecalhoParcela)
                                                hasTaxa = true;

                                            isCabecalhoParcela = line.Split(' ').Any(h => h.Equals("IOF.SEG"));
                                            if (isCabecalhoParcela)
                                                hasIof = true;
                                        }

                                        
                                        if (arrayIgnorCabecalho.Any(k => line.Contains(k)))
                                            continue;
                                        if (arrayIgnorParcelas.Any(k => line.Split(' ').Any(p => k.Equals(p))))
                                            continue;

                                        if (line.Contains("Cronograma"))
                                            lstCronograma.Add(objCabecalho.Contrato);

                                        if (i > 1 && line.Contains("C.P.F."))
                                        {
                                            var novoObj = objCabecalho;

                                            objCabecalho = new Cabecalho()
                                            {
                                                Carteira = novoObj.Carteira,
                                                Numero = novoObj.Numero,
                                                DataEmicao = novoObj.DataEmicao,
                                                DataBase = novoObj.DataBase,
                                                Contrato = novoObj.Contrato,
                                                Id = (lstCabecalho.Count + 1)
                                            };
                                            novoObj = null;
                                            isParcelas = false;

                                            objCabecalho = businessCabecalho.PreencheCabecalho(objCabecalho);
                                        }

                                        if (isParcelas)
                                        {

                                            
                                            if (line.Split('-').Length == 2)
                                            {
                                                string[] _arraySituacao = line.Split('-');
                                                if (_arraySituacao[0].Length == 4)
                                                {
                                                    isFinal = true;
                                                    if(objParcelas != null)
                                                        if (!lstParcelas.Any(j => j.Id == objParcelas.Id) && (!string.IsNullOrWhiteSpace(objParcelas.Vencimento)))
                                                            lstParcelas.Add(objParcelas);
                                                    objParcelas = null;
                                                }
                                            }

                                            if (objParcelas != null)
                                            {
                                                if (Regex.IsMatch(line, @"(^[0-9]{2}.[0-9]{3}\s\d{2}\/\d{2}\/\d{4}$)"))
                                                    continue;

                                                if (arrayIgonorCampo.Where(k => line.Contains(k)).Count() > 0) continue;

                                                // indica que não existe mais dados relevantes, e lê os restante das linhas sem qualquer ação
                                                // if (line.Contains("FGTS/Prestação") || line.Contains("Aberto") || line.Contains("CONTRATO LIQUIDADO"))
                                                if (arrayFinalParcela.Any(k => line.Split(' ').Any(p => k.Equals(p))))
                                                {
                                                    isFinal = true;
                                                    if (!lstParcelas.Any(j => j.Id == objParcelas.Id) && (!string.IsNullOrWhiteSpace(objParcelas.Vencimento)))
                                                        lstParcelas.Add(objParcelas);
                                                    objParcelas = null;

                                                }

                                                if (string.IsNullOrWhiteSpace(line))
                                                    continue;

                                                if (Regex.IsMatch(line.Trim(), @"(^\d{1,2}:\d{1,2}:\d{1,2}$)"))
                                                    continue;

                                                // Sair do loop após o preenchimento da lista de Situações indicando o final do arquivo
                                                if (string.IsNullOrWhiteSpace(line)) break;

                                                isNotiqual = businessParcelas.ValidaLinha(line);

                                                if (!isNotiqual)
                                                    arrayLinhaParcela = businessParcelas.TrataArray(line);
                                                else
                                                    arrayLinhaParcela = businessParcelas.TratArrayPadrao2(line, pagina);

                                                // PEGA A LINHA DE PAGAMENTO
                                                if (arrayLinhaParcela.Any(u => Regex.IsMatch(u, @"(^\d{3}\/\d{3}$)")))
                                                {
                                                    // SE A LINHA DE PAGAMENTO ESTIVER FORA DO PADRÃO, ENTRAR NESTE IF
                                                    if (arrayLinhaParcela.Length < 9)
                                                        arrayLinhaParcela = businessParcelas.TrataParcela2(line, pagina);
                                                   
                                                    if (!isFinal)
                                                    {
                                                       
                                                        if (arrayLinhaParcela.Any(k => k.Contains("INCORP")))
                                                        {
                                                            //var x = arrayLinhaParcela.ToList();
                                                            //x.RemoveAll(c => c.Contains("INCORP"));
                                                            //arrayLinhaParcela = x.ToArray();
                                                        }

                                                        if (lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                        {
                                                            objParcelas = new Parcela()
                                                            {
                                                                Id = countParcela++,
                                                                IdCabecalho = objCabecalho.Id
                                                            };
                                                        }

                                                        objParcelas = businessParcelas.PreencheParcela(objParcelas);
                                                        objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 2, hasTaxa, hasIof);
                                                    }
                                                    continue;
                                                }
                                                // PEGA A LINHA DE BANCO E AGENCIA
                                                if (arrayLinhaParcela.Any(g => Regex.IsMatch(g.Trim(), @"(^\d{6}.\d{1}$)")) )
                                                {
                                                    if (arrayLinhaParcela.Count(u => Regex.IsMatch(u, @"(^\d{2}\/\d{2}\/\d{4}$)")) == 2 && arrayLinhaParcela.Length == 2)
                                                        objParcelas.Proc_Emi_Pag = Regex.Replace(string.Join(" ", arrayLinhaParcela), @"[^0-9\/$]", "");
                                                    else
                                                        objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 3, hasTaxa, hasIof);

                                                    if (!lstParcelas.Any(j => j.Id == objParcelas.Id))
                                                    {
                                                        if (!string.IsNullOrWhiteSpace(objParcelas.Vencimento))
                                                            lstParcelas.Add(objParcelas);
                                                    }
                                                    else
                                                    {
                                                        lstParcelas.Remove(objParcelas);
                                                        lstParcelas.Add(objParcelas);
                                                    }

                                                    objParcelas = new Parcela()
                                                    {
                                                        Id = countParcela++,
                                                        IdCabecalho = objCabecalho.Id
                                                    };

                                                    continue;
                                                }
                                                // PEGA A LINHA DE CORREÇÃO
                                                if (arrayLinhaParcela.Any(x => x.Equals("COR")))
                                                {
                                                    // SE A LINHA DE OCORRENCIA ESTIVER FORA DO PADRÃO, ENTRAR NESTE IF
                                                    if (arrayLinhaParcela.Length < 4)
                                                        arrayLinhaParcela = businessParcelas.TrataArrayPadrao3(line, pagina);
                                                       
                                                    if (lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                    {
                                                        objParcelas = new Parcela()
                                                        {
                                                            Id = countParcela++,
                                                            IdCabecalho = objCabecalho.Id
                                                        };
                                                    }

                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 1, hasTaxa, hasIof);

                                                    continue;
                                                }
                                               
                                                // PEGA A LINHA DE DAMP (se houver DAMP na parcela)
                                                if (arrayLinhaParcela.Any(x => x.Equals("DAMP")))
                                                {
                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 5, hasTaxa, hasIof);
                                                    continue;
                                                }

                                                if (line.Contains("***"))
                                                {
                                                    if (arrayIgnoraOcorrencia.Any(k => line.Split(' ').Any(p => k.Equals(p))))
                                                        continue;

                                                    if (lstParcelas.Count > 0)
                                                    {
                                                        objParcelas = lstParcelas.LastOrDefault();
                                                    }
                                                    Ocorrencia objCorrencia = businessParcelas.TrataOcorrencia(arrayLinhaParcela, diretorioDestinoLayout);
                                                    objCorrencia.Contrato = objCabecalho.Contrato;
                                                    objCorrencia.IdParcela = objParcelas.Id;
                                                    objCorrencia.IdCabecalho = objCabecalho.Id;
                                                    lstOcorrencia.Add(objCorrencia);

                                                    continue;
                                                }


                                                if (arrayLinhaParcela.Any(v => v.Trim().Equals("00/00/0000")))
                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 4, hasTaxa, hasIof);
                                            }
                                            //  PEGAR AS SITUAÇOES DO CONTRATO PARA ATUALIZAR O ARQUIVO SITU115A.TXT
                                            if (i == reader.NumberOfPages)
                                            {
                                                if (isFinal)
                                                {
                                                  
                                                    if (!lstCabecalho.Any(z => z.Id == objCabecalho.Id))
                                                        lstCabecalho.Add(objCabecalho);

                                                    string statusContrato = string.Empty;
                                                    string[] _arraySituacao = null;

                                                    while ((line = strReader.ReadLine()) != null)
                                                    {
                                                        if (line.IndexOf(':') > 1) break;
                                                        if (line.Split('-').Length == 2)
                                                        {
                                                            _arraySituacao = line.Split('-');
                                                            if (_arraySituacao[0].Length == 4)
                                                            {
                                                                statusContrato = line.Replace("-", "").Trim();
                                                                if (!lstSituacao.Any(k => k.Equals(statusContrato)))
                                                                    lstSituacao.Add(statusContrato);
                                                                continue;
                                                            }
                                                        }
                                                    }

                                                    continue;
                                                }
                                            }
                                            continue;
                                        }

                                        if (padrao == 1)
                                        {
                                            // array de Campos que são ignorados, pois nao trazem dados relevantes
                                            string[] array2IgnorCampo = { "Nº", "Demonstrativo", "Emissão", "Carteira", "Nascimento", "Nome", "End.Imóvel", "Telefone", "Depósito", "Modalidade", "Bairro" };
                                            if (array2IgnorCampo.Where(k => line.Contains(k)).Count() > 0) continue;

                                            if (string.IsNullOrWhiteSpace(objCabecalho.Numero) || objCabecalho.Numero == "0")
                                            {
                                                cabecalho = businessCabecalho.TrataArray(line);
                                                objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 6);
                                                continue;
                                            }
                                            if (string.IsNullOrWhiteSpace(objCabecalho.DataEmicao) || objCabecalho.DataEmicao == "01/01/0001")
                                            {
                                                objCabecalho.DataEmicao = businessCabecalho.TrataArray(line)[1];
                                                continue;
                                            }
                                            if (string.IsNullOrWhiteSpace(objCabecalho.Carteira) || objCabecalho.Carteira == "0")
                                            {
                                                cabecalho = businessCabecalho.TrataArray(line);
                                                objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 1);
                                                continue;
                                            }

                                            if (line.Contains("C.P.F."))
                                            {
                                                cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 2);
                                                objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 2);
                                                continue;
                                            }
                                            if (string.IsNullOrWhiteSpace(objCabecalho.EnderecoImovel) || objCabecalho.EnderecoImovel == "")
                                            {
                                                cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 3);
                                                objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 3);
                                                continue;
                                            }

                                            if (line.Contains("Cliente"))
                                            {
                                                cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 5);
                                                objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 5);
                                                continue;
                                            }
                                            if (line.Contains("CET"))
                                            {
                                                cabecalho = businessCabecalho.TrataLinhaPDF(line, 13);
                                                objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 13, line);
                                                continue;
                                            }

                                            if (line.Contains("Categoria"))
                                            {
                                                line = strReader.ReadLine().Trim();
                                                cabecalho = businessCabecalho.TrataLinhaPDF(line, 22);
                                                objCabecalho.Categoria = cabecalho[0];
                                                objCabecalho.Modalidade = cabecalho[1];
                                                objCabecalho.ContaDeposito = cabecalho[2];
                                                continue;
                                            }
                                            if (line.Contains("Plano"))
                                            {
                                                cabecalho = businessCabecalho.TrataLinhaPDF(line, 14);
                                                objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 14);
                                                continue;
                                            }

                                            if (line.Contains("CADOC"))
                                            {
                                                cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 6);
                                                if (cabecalho.Length == 1 && line.Contains("CADOC"))
                                                    objCabecalho.DataCaDoc = cabecalho[0].Trim();
                                                continue;
                                            }

                                            if (string.IsNullOrWhiteSpace(objCabecalho.Categoria))
                                            {
                                                cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 6);
                                                if (cabecalho.Length == 1 && line.Contains("CADOC"))
                                                    objCabecalho.DataCaDoc = cabecalho[0].Trim();
                                                else
                                                    objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 6);
                                                continue;
                                            }
                                          
                                        }

                                        if (line.Contains("Nº"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 6);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 6);
                                            continue;
                                        }
                                        if (line.Contains("Emissão"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 7);

                                            if (!cabecalho[3].Substring(3, 11).Equals("CTFIN/O016A"))
                                            {
                                                MessageBox.Show("este arquivo nao é tela 16");
                                                break;
                                            }
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 7);
                                            continue;
                                        }
                                        if (line.Contains("Carteira"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 8);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 8);
                                            continue;
                                        }
                                        if (line.Contains("C.P.F.") || line.Contains("Nascimento"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 9);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 9);
                                            continue;
                                        }

                                        if (line.Contains("End.Imóvel"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 10);
                                            if (cabecalho.Length < 5)
                                            {
                                                var x = cabecalho.ToList();
                                                x.Insert(1, "");
                                                cabecalho = x.ToArray();
                                            }
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 10);
                                            continue;
                                        }
                                        if (line.Contains("Cliente"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(strReader.ReadLine(), 12);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 12);
                                            continue;
                                        }
                                        if (line.Contains("Categoria"))
                                        {
                                            line = strReader.ReadLine().Trim();
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 22);
                                            objCabecalho.Categoria = cabecalho[0];
                                            objCabecalho.Modalidade = cabecalho[1];
                                            objCabecalho.ContaDeposito = cabecalho[2];
                                            continue;
                                        }
                                        if (line.Contains("CET"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 13);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 13, line);
                                            continue;
                                        }
                                        if (line.Contains("Plano"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 14);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 14);
                                            continue;
                                        }
                                        if (line.Contains("Sistema"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 15);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 15);
                                            continue;
                                        }
                                        if (line.Contains("DFI"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 16);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 16);
                                            continue;
                                        }

                                        if (line.Contains("Data Garanta"))
                                        {
                                            string[] cab = line.Replace("Agência", "").Replace("Data Garanta", "").Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                                            objCabecalho.DataGarantia = cab[0];
                                            objCabecalho.Agencia = cab[1];
                                            continue;
                                        }


                                        if (line.Contains("Reajuste"))
                                        {
                                            objCabecalho.Reajuste = line.Replace("Reajuste", "").Trim();
                                            continue;
                                        }
                                        if (line.Contains("Prazo"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 17);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 17);
                                            continue;
                                        }
                                        if ((line.Contains("Razão") || line.Contains("Empreendimento") || line.Contains("1º")) && !line.Contains("Repactuação"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 18);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 18);
                                            continue;
                                        }
                                        if (line.Contains("Correção"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 19);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 19);
                                            continue;
                                        }
                                        if ((line.Contains("Ult.") || line.Contains("Re-")) && !line.Contains("Origem"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 20);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 20);
                                            continue;
                                        }

                                        if (line.Contains("Origem"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 21);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 21);
                                            continue;
                                        }
                                        if (line.Contains("Repactuação"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 23);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 23);
                                            continue;
                                        }
                                        if (line.Contains("Carência"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 24);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 24);
                                            continue;
                                        }
                                        if (line.Contains("Apólice") && !line.Contains("Seguro"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 25);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 25);
                                            continue;
                                        }
                                        if (line.Contains("Situações"))
                                        {
                                            objCabecalho.Situacao = Regex.Replace(line, @"[^0-9$]+", "");

                                            if (!lstCabecalho.Any(c => c.Id == objCabecalho.Id))
                                            {
                                                if (!string.IsNullOrWhiteSpace(objCabecalho.Situacao))
                                                {
                                                    objCabecalho = businessCabecalho.PreencheCabecalho(objCabecalho);
                                                    lstCabecalho.Add(objCabecalho);
                                                }
                                            }
                                            isParcelas = true;
                                            continue;
                                        }
                                    }
                                }

#endregion
                            }

                            if (objParcelas != null)
                                if (!lstParcelas.Any(j => j.Id == objParcelas.Id) && (!string.IsNullOrWhiteSpace(objParcelas.Vencimento)))
                                    lstParcelas.Add(objParcelas);

                            objParcelas = null;

                           var t1 = lstCabecalho.RemoveAll(a => string.IsNullOrWhiteSpace(a.Cpf));
                           var t2 = lstParcelas.RemoveAll(r => string.IsNullOrWhiteSpace(r.Vencimento) && !r.IsAnt);

                            userObject = new UserObject { Contrato = objCabecalho.Contrato, PdfInfo = arquivoPdf, TotalArquivoPorPasta = totalPorPasta };
                            objContratoPdf.Contrato = lstCabecalho.FirstOrDefault().Contrato;
                            objContratoPdf.Carteira = lstCabecalho.FirstOrDefault().Carteira;
                            objContratoPdf.Cabecalhos.AddRange(lstCabecalho);
                            objContratoPdf.Parcelas.AddRange(lstParcelas);
                            objContratoPdf.Ocorrencias.AddRange(lstOcorrencia);

                            if (lstCronograma.Count > 0)
                                objContratoPdf.Cronogramas.AddRange(lstCronograma);

                            lstContratosPdf.Add(objContratoPdf);

                            lstParcelas.Clear();
                            lstCabecalho.Clear();
                            lstOcorrencia.Clear();
                            lstCronograma.Clear();
                            objContratoPdf = null;
                            objCabecalho = null;
                            objCabecalho = null;
                            isParcelas = false;
                            isFinal = false;
                            isNotiqual = false;
                            pagina = string.Empty;
                            its = null;

                            padrao = 0;
                            contador++;
                            _countPercent++;
                            // a cada X contratos lidos, escreve o conteudo no arquivo texto de contratos, ocorrencia parcelas...etc
                            if (contador == 1000)
                            {
                                userObject.DescricaoPercentural = string.Format("Gerando o {0}º Lote.", countLote++);
                                backgroundWorker1.ReportProgress(_countPercent, userObject);
                                //  businessCabecalho.PopulaContrato(lstContratosPdf, lstGT, diretorioDestinoLayout, diretorioOrigemPdf);
                                var tab = new
                                {
                                    item1 = lstContratosPdf,
                                    item2 = lstGT,
                                    item3 = diretorioDestinoLayout,
                                    item4 = diretorioOrigemPdf
                                };

                                _thread = new Thread(new ParameterizedThreadStart(businessCabecalho.PopulaContrato));
                                _thread.Start(tab);
                                
                                lstParcelas.Clear();
                                lstCronograma.Clear();
                                lstCabecalho.Clear();
                                lstOcorrencia.Clear();
                                lstContratosPdf = new List<ContratoPdf>();
                                contador = 0;
                                countParcela = 1;
                            }
                            else
                                backgroundWorker1.ReportProgress(_countPercent, userObject);
                        }
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        _countPercent++;
                        backgroundWorker1.ReportProgress(_countPercent, userObject);

                        isErro = true;
                        if (!File.Exists(diretorioOrigemPdf + @"\LogErroContratos.txt"))
                        {
                            StreamWriter item = File.CreateText(diretorioOrigemPdf + @"\LogErroContratos.txt");
                            item.Dispose();
                        }
                        using (StreamWriter sw = new StreamWriter(diretorioOrigemPdf + @"\LogErroContratos.txt", true, Encoding.UTF8))
                        {
                            StringBuilder strErro = new StringBuilder();
                            strErro.AppendLine(string.Format("CONTRATO: {0}", arquivoPdf.Name))
                                    .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                    .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", ex.Message))
                                    .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                            sw.Write(strErro);
                            sw.WriteLine("================================================================================================================================================");
                        }

                        if(!Directory.Exists(string.Format(@"{0}\!Erro", diretorioOrigemPdf)))
                            Directory.CreateDirectory(string.Format(@"{0}\!Erro", diretorioOrigemPdf));

                        File.Move(string.Format(@"{0}\{1}", arquivoPdf.DirectoryName, arquivoPdf.Name), string.Format(@"{0}\!Erro\{1}", diretorioOrigemPdf, arquivoPdf.Name));

                        // SE HOUVER ALGUM CONTRATO PARA SER FORMATADO PARA ALTA, O PROCESSO FINALIZA A FORMATAÇÃO APÓS A GERAR O LOG DE ERROS 
                        if (lstContratosPdf.Count() > 0)
                        {
                            var tab = new
                            {
                                item1 = lstContratosPdf,
                                item2 = lstGT,
                                item3 = diretorioDestinoLayout,
                                item4 = diretorioOrigemPdf
                            };
                            businessCabecalho.PopulaContrato(tab);
                            // businessCabecalho.PopulaContrato(lstContratosPdf, lstGT, diretorioDestinoLayout, diretorioOrigemPdf);
                        }

                        padrao = 0;
                        contador++;
                    }
                });

            });
            if (lstContratosPdf.Count() > 0)
            {
                userObject = new UserObject
                {
                    Contrato ="Salvando Lotes gerados",
                    PdfInfo = new FileInfo(listDiretory.Last()),
                    DescricaoPercentural = "Finalizando Formatação"
                };
                backgroundWorker1.ReportProgress(_countPercent, userObject);

                var tab = new
                {
                    item1 = lstContratosPdf,
                    item2 = lstGT,
                    item3 = diretorioDestinoLayout,
                    item4 = diretorioOrigemPdf
                };

                if(_thread != null)
                if (_thread.ThreadState == System.Threading.ThreadState.Running)
                    _thread.Join();

                businessCabecalho.PopulaContrato(tab);
               // businessCabecalho.PopulaContrato(lstContratosPdf, lstGT, diretorioDestinoLayout, diretorioOrigemPdf);
                lstContratosPdf.Clear();
            }

            // ATUALIZA O AQUIVO DE SITUAÇÕES SE HOUVER ALGUMA SITUAÇÃO DE CONTRATO QUE NAO ESTEJA NO ARQUIVO SITU115A.TXT
            using (StreamWriter escrever = new StreamWriter(diretorioOrigemPdf + @"\config\SITU115A.TXT", true, Encoding.UTF8))
            {
                lstSituacao.ForEach(f =>
                {
                    if (!_situacoesAtual.Contains(f))
                        escrever.WriteLine(f);
                });
            }

        }
    }
}
