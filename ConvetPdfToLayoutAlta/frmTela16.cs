﻿using ConvetPdfToLayoutAlta.Models;
using iTextSharp.text.exceptions;
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
    public partial class FrmTela16 : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        List<string> _situacoesAtual = new List<string>();

        int contador = 0, countLote = 1, totalArquivo = 0, totalPorPasta = 0;
        bool isFinal = false;
        IEnumerable<string> listContratoBlockPdf = null;
        IEnumerable<string> listDiretory = null;
        List<ItensDamp> itensFgts = null;
        Dictionary<string, string> dicionario = new Dictionary<string, string>();
        BusinessParcelas businessParcelas = null;
        string diretorioOrigemPdf, diretorioDestinoLayout, tmp, tela;

        public FrmTela16(string _diretoioPdf, string _diretorioDestino, string _tela)
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
                    Close();
                    return;
                }

                listDiretory.ToList().ForEach(t =>
                {
                    totalArquivo += Directory.EnumerateFiles(string.Format(@"{0}", t), "*_16.pdf", SearchOption.AllDirectories).Count();
                });

                if (totalArquivo == 0)
                {
                    Close();
                    return;
                }

                if(!File.Exists(Directory.GetCurrentDirectory() + @"\config\CFF00101.ARQ"))
                {
                    MessageBox.Show(@"Não foi encontrado o arquivo config\CFF00101.ARQ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                lblContrato.Text = "-";
                lblTempo.Text = "";
                lblPendente.Text = "";
                lblQtd.Text = "Total: " + totalArquivo.ToString();
                progressBarReaderPdf.Maximum = totalArquivo;

                businessParcelas = new BusinessParcelas();

                // CARREGA OS ITENS (CONTRATO, /DATAVENCIMENTO)DA TELA 18 PARA VERIFICAÇÃO DE DAMPS(FGTS)
                itensFgts = businessParcelas.GetParcelaFgts();

                #region FAZ A LEITURA DOS CONTRATOS EM ATRASO
                
                //FAZ A LEITURA DO CONTRATOS EM ATRASO
                using (StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + @"\config\CFF00101.ARQ"))
                {
                    string[] _linha = { };
                    streamReader.ReadLine();

                    while (!streamReader.EndOfStream)
                    {
                        _linha = streamReader.ReadLine().Split(';');
                        dicionario.Add(string.Join(";", _linha.Skip(1).Take(3)).Replace(";", "").Substring(1), _linha[10].Trim());
                    }
                }

                #endregion

                // FAZ A LEITURA DO ARQUIVO QUE CONTEM AS  SISTUAÇÕES DOS CONTRATOS
                using (StreamReader lerTxt = new StreamReader(string.Format("{0}{1}", Directory.GetCurrentDirectory(), @"\config\SITU115A.TXT")))
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

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Close();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string DatacontratoCreditoAtraso = string.Empty;
            string _numeroContrato = string.Empty;
            int numberPage = 0,  countParcela = 0;
            List<string> lstSituacao = new List<string>();
            List<string> lstCronograma = new List<string>(); 
            List<ItensDamp> parcelaFgts =  null;
            List<Cabecalho> lstCabecalho = null;
            List<Parcela> lstParcelas = null;
            List<Ocorrencia> lstOcorrencia = null;
            List<ContratoPdf> lstContratosPdf = new List<ContratoPdf>();
            UserObject userObject = null;

            StringBuilder strLayoutContrato = new StringBuilder();
            StringBuilder strLayoutOcorrencia = new StringBuilder();

            BusinessCabecalho businessCabecalho = null;
           

            Cabecalho objCabecalho = null;
            Parcela objParcelas = null;
            ContratoPdf objContratoPdf = null;

            FileInfo arquivoPdf = null;

            string[] arrayIgnoraOcorrencia = { "Portabilidade:", "BRADESCO","SAFRA", "ITAU","ITAÚ", "PACTUAL","BTG", "UNIBANCO","HSBC", "NORDESTE","SATNANDER","BNDS","CITIBANK","CITI", "CITY", "ECONOMICA", "FEDERAL", "PAN004", "PAN005", "PAN001", "PAN002", "PAN003", "PAN006", "PAN007", "PAN008", "PAN009", "PAN010", "PAN011", "PAN011", "PAN0" };
            string[] arrayIgnorParcelas = { "Encargo", "Índice", "Devedor", "Proc.Emi/Pag", "Mora", "Gerad", };
            string[] arrayIgnorCabecalho = { "End.Correspondência", "Demonstrativo", "Telefone", "Modalidade", "Nome","Profisional" };
            string[] arrayIgonorCampo = { "Nº", "CTFIN", "Emissão", "PRO","ANT", "SANTANDER", "Carteira","CTFIN","Carteira","Contrato","Seguro","TAXA", "Categoria" };
            string[] arrayFinalParcela = { "TOTAL","Aberto", "SEGURO", "CONTRATO LIQUIDADO", "FGTS/Prestação", "QTD" };
            string[] arrayGetCampos = { "Plano", "Sistema", "DFI", "Data Garanta", "Reajuste", "Prazo", "Razão", "Empreendimento", "1º", "Repactuação", "Ult.", "Re-", "Repactuação", "Correção", "Origem", "Carência", "Apólice", "Seguro", "Taxa Juros", "Data Inclusao", "Agência", "Garanta" };
            string[] mapOcorrenciaNovoPrazo = { "011","020","021", "029", "010", "30" };
            string pagina = string.Empty, readerContrato = string.Empty;
            string[] cabecalho = null;
            string[] arrayLinhaParcela = null;
            bool isParcelas = false, isNotiqual = false, hasIof = false, isCabecalhoParcela = false, isBody = false, isNotTela16 = false;
            int padrao = 0, _countPercent = 0;
            

            businessCabecalho = new BusinessCabecalho();
            businessParcelas = new BusinessParcelas();
            lstCabecalho = new List<Cabecalho>();
            lstParcelas = new List<Parcela>();
            lstOcorrencia = new List<Ocorrencia>();
            parcelaFgts = new List<ItensDamp>();
          

            listDiretory.ToList().ForEach(d =>
            {
                listContratoBlockPdf = Directory.EnumerateFiles(string.Format(@"{0}", d), "*_16.pdf", SearchOption.TopDirectoryOnly);
                if (listContratoBlockPdf.Count() == 0)
                    return;
                totalPorPasta = listContratoBlockPdf.Count();

                foreach (string w in listContratoBlockPdf)
                {

                    ITextExtractionStrategy its;
                    try
                    {
                        arquivoPdf = new FileInfo(w);
                        _numeroContrato = arquivoPdf.Name.Split('_')[0].Trim();

                        parcelaFgts = itensFgts.Where(fgts => fgts.Contrato.Equals(_numeroContrato)).ToList();

                        DatacontratoCreditoAtraso = dicionario.FirstOrDefault(s => s.Key.Equals(_numeroContrato)).Value;
                        
                        //APOÓS A CONSULTA, SE O CONTRATO FOR LOCALIZADO, REMOVE DA LISTA PARA FACILITAR A NOVA PESQUISA
                        if (!string.IsNullOrWhiteSpace(DatacontratoCreditoAtraso))
                                dicionario.Remove(_numeroContrato);


                        //VERIFICA O TAMANHO DO ARQUIVO
                        if (arquivoPdf.Length <= 10519)
                        {
                            ExceptionError.RemoverTela(arquivoPdf, diretorioDestinoLayout);
                            ExceptionError.TrataErros(_numeroContrato, "Layout de Arquivo desconhecido", diretorioDestinoLayout);
                            _countPercent++;
                            backgroundWorker1.ReportProgress(_countPercent,null);
                            contador++;
                            continue;
                        }

                        objContratoPdf = new ContratoPdf();
                        objCabecalho = new Cabecalho() { Id = (lstCabecalho.Count + 1), DataTransferencia = DatacontratoCreditoAtraso };
                        objParcelas = new Parcela() { IdCabecalho = objCabecalho.Id, Id = 0 };
                        objParcelas = businessParcelas.PreencheParcela(objParcelas);
                        objCabecalho = businessCabecalho.PreencheCabecalho(objCabecalho);

                        _numeroContrato = string.Empty;

                        using (PdfReader reader = new PdfReader(w))
                        {
                            isFinal = hasIof = isParcelas = isCabecalhoParcela = isNotTela16 = false;
                            int numberLine;

                            pagina = string.Empty;
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                               
                                numberPage = i;
                                padrao = 0;
                                numberLine = -1;

                                its = new LocationTextExtractionStrategy();
                                pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                                pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));

                                if (pagina.Length < 30)
                                    break;

                                if (!pagina.Substring(0, 10).Contains("Nº"))
                                    padrao = 1;

                                #region Padrão 1

                                using (StringReader strReader = new StringReader(pagina))
                                {
                                    string line;
                                    while ((line = strReader.ReadLine()) != null)
                                    {
                                        if (string.IsNullOrWhiteSpace(line))
                                            continue;
                                        numberLine++;

                                        string[] arrayCabecalho = businessCabecalho.TrataArray(line);

                                        if (isCabecalhoParcela)
                                        {
                                            if (arrayCabecalho.Any(g => g.Equals("Mora")))
                                                isCabecalhoParcela = false;
                                            else continue;
                                        }

                                        if (line.Trim().Equals("a") || line.Trim().Equals("v") || line.Trim().Equals("***") || line.Length < 2)
                                            continue;
                                        if (string.IsNullOrWhiteSpace(line)) continue;
                                        if (Regex.IsMatch(line, @"(^\d{1,2}:\d{1,2}:\d{1,2}$)"))
                                            continue;

                                        if (arrayIgnorCabecalho.Any(k => line.Contains(k)))
                                            continue;

                                        if (arrayIgnorParcelas.Any(k => line.Split(' ').Any(p => k.Equals(p))))
                                            continue;

                                        if (Regex.IsMatch(line, @"(^\d{4}\-[\s\w\-]+$)"))
                                            isFinal = true;

                                        if (line.Contains("Cronograma"))
                                            lstCronograma.Add(objCabecalho.Contrato);



                                        if (i > 1 && arrayCabecalho.Any(doc => Regex.IsMatch(doc, @"(^\d{3}.\d{3}.\d{3}\/\d{4}\-\d{2}$)") || Regex.IsMatch(doc, @"(^\d{3}.\d{3}.\d{3}\-\d{2}$)")))
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

                                        // VERIFICA SE O CONTRATO É DO TIPO TELA 16
                                        if(i == 1)
                                        if (line.Contains("Emissão"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 7);

                                            if (i == 1 && !isBody)
                                            {
                                                if (line.Contains("CTFIN"))
                                                {
                                                    if (!cabecalho.Any(c => c.Contains("CTFIN/O016A")))
                                                    {
                                                        _countPercent++;
                                                        backgroundWorker1.ReportProgress(_countPercent, null);
                                                        isNotTela16 = true;
                                                        ExceptionError.TrataErros(arquivoPdf.Name, "O Arquivo não é do tipo CTFIN/O016A", diretorioDestinoLayout);
                                                        break;
                                                    }
                                                }
                                            }
                                              objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 7);
                                            continue;
                                        }
                                        if (isParcelas)
                                        {
                                            if (Regex.IsMatch(line, @"(^\d{4}\-[\s\w\-]+$)"))
                                            {
                                                string[] _arraySituacao = line.Split('-');
                                                if (_arraySituacao[0].Length == 4)
                                                {
                                                    isFinal = true;
                                                    if (objParcelas != null)
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

                                                // INDICA QUE NÃO EXISTE MAIS DADOS RELEVANTES, E LÊ OS RESTANTE DAS LINHAS SEM QUALQUER AÇÃO
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

                                                // SAIR DO LOOP APÓS O PREENCHIMENTO DA LISTA DE SITUAÇÕES INDICANDO O FINAL DO ARQUIVO
                                                if (string.IsNullOrWhiteSpace(line)) break;

                                                isNotiqual = businessParcelas.ValidaLinha(line);

                                                if (!isNotiqual)
                                                    arrayLinhaParcela = businessParcelas.TrataArray(line);
                                                else
                                                    arrayLinhaParcela = businessParcelas.TratArrayPadrao2(line, pagina, numberLine);

                                                // PEGA A LINHA DE PAGAMENTO
                                                if (arrayLinhaParcela.Any(u => Regex.IsMatch(u, @"(^\d{3}\/\d{3}$)")))
                                                {
                                                    // SE A LINHA DE PAGAMENTO ESTIVER FORA DO PADRÃO, ENTRAR NESTE IF
                                                    if (arrayLinhaParcela.Length <= 9)
                                                        arrayLinhaParcela = businessParcelas.TrataParcela2(line, pagina, numberLine);

                                                    if (!isFinal)
                                                    {
                                                        if (lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                        {
                                                            objParcelas = new Parcela()
                                                            {
                                                                Id = countParcela++,
                                                                IdCabecalho = objCabecalho.Id,
                                                                Contrato = objCabecalho.Contrato
                                                            };
                                                        }

                                                        objParcelas = businessParcelas.PreencheParcela(objParcelas);
                                                        objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 2,/* hasTaxa, hasIof*/ objCabecalho);
                                                    }
                                                    continue;
                                                }
                                                // PEGA A LINHA DE BANCO E AGENCIA
                                                if (arrayLinhaParcela.Any(g => Regex.IsMatch(g.Trim(), @"(^\d{6}.\d{1}$)")) || (Regex.IsMatch(line, @"(^?\d{2}\/\d{2}\/\d{4}\s\d{2}\/\d{2}\/\d{4}$?)") && arrayLinhaParcela.Length > 5))
                                                {

                                                    if(objParcelas.Proc_Emi_Pag != null)
                                                        if (!objParcelas.Proc_Emi_Pag.Equals("0/00/0000"))
                                                            continue;

                                                    if (arrayLinhaParcela.Count(u => Regex.IsMatch(u, @"(^\d{2}\/\d{2}\/\d{4}$)")) == 2 && arrayLinhaParcela.Length == 2)
                                                        objParcelas.Proc_Emi_Pag = Regex.Replace(string.Join(" ", arrayLinhaParcela), @"[^0-9\/$]", "");
                                                    else
                                                    {
                                                        List<string> lstValores = arrayLinhaParcela.ToList();
                                                        if (!Regex.IsMatch(lstValores[3].Trim(), @"(^\d{2}\/\d{2}\/\d{4}$)"))
                                                            lstValores.RemoveAt(3);

                                                        objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, lstValores.ToArray(), 3, /*hasTaxa, hasIof*/ objCabecalho, lstDampFgts: parcelaFgts);
                                                    }

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
                                                        IdCabecalho = objCabecalho.Id,
                                                        Contrato = objCabecalho.Contrato
                                                    };

                                                    continue;
                                                }
                                                // PEGA A LINHA DE CORREÇÃO
                                                if (arrayLinhaParcela.Any(x => x.Equals("COR")))
                                                {
                                                    // SE A LINHA DE OCORRENCIA ESTIVER FORA DO PADRÃO, ENTRAR NESTE IF
                                                    if (arrayLinhaParcela.Length < 4)
                                                        arrayLinhaParcela = businessParcelas.TrataArrayPadrao3(line, pagina, numberLine);

                                                    if (lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                    {
                                                        objParcelas = new Parcela()
                                                        {
                                                            Id = countParcela++,
                                                            IdCabecalho = objCabecalho.Id,
                                                            Contrato = objCabecalho.Contrato
                                                        };
                                                    }

                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 1,/* hasTaxa, hasIof*/ objCabecalho );

                                                    continue;
                                                }

                                                // PEGA AS LINHAS DE OCORRENCIAS
                                                if (line.Contains("***"))
                                                {
                                                    if (arrayIgnoraOcorrencia.Any(k => line.Split(' ').Any(p => k.Equals(p))))
                                                    continue;

                                                    Ocorrencia objCorrencia = null;

                                                    if (line.Contains("Novo prazo"))
                                                    {
                                                        objCorrencia = lstOcorrencia.LastOrDefault();
                                                        if (mapOcorrenciaNovoPrazo.Any(j => j.Equals( objCorrencia.CodigoOcorrencia.Trim())))
                                                        {
                                                            objCorrencia.IsNovoPrazo = true;
                                                            objCorrencia.NovoNumeroPrazo = line.Split(' ')[3];
                                                        }

                                                        continue;
                                                    }

                                                    if (arrayLinhaParcela.Any(dmp => dmp.ToUpper().Equals("PENDENCIA")))
                                                    {
                                                        objCorrencia = lstOcorrencia.LastOrDefault();
                                                        objCorrencia.CodigoOcorrencia = "065";
                                                        objCorrencia.Descricao = "***065Pendencia de Complemento";
                                                        continue;
                                                    }


                                                    if (arrayLinhaParcela.Any(dmp => dmp.Equals("DAMP")))
                                                    {
                                                        objCorrencia = lstOcorrencia.LastOrDefault();
                                                        objCorrencia.Damp = businessParcelas.TrataOcorrencia(arrayLinhaParcela, diretorioDestinoLayout).Damp;
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        if (lstParcelas.Count > 0)
                                                        {
                                                            objParcelas = lstParcelas.LastOrDefault();
                                                        }

                                                        objCorrencia = businessParcelas.TrataOcorrencia(arrayLinhaParcela, diretorioDestinoLayout, objCabecalho.Contrato);
                                                        objCorrencia.NaoTemParcela = lstParcelas.Count == 0;
                                                        objCorrencia.Contrato = objCabecalho.Contrato;
                                                        objCorrencia.IdParcela = objParcelas.Id;
                                                        objCorrencia.IdCabecalho = objCabecalho.Id;
                                                    }

                                                    // SE NÃO HOUVER PARCELA PARA OCORRENCIA, CRIA UMA PARCELA FAKE DE REFERENCIA
                                                    if (lstParcelas.Count == 0)
                                                        objCorrencia.NaoTemParcela = true;

                                                    if (!string.IsNullOrWhiteSpace(objCorrencia.CodigoOcorrencia))
                                                        lstOcorrencia.Add(objCorrencia);

                                                    continue;
                                                }


                                                if (arrayLinhaParcela.Any(v => v.Trim().Equals("00/00/0000")))
                                                {
                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 4, /*hasTaxa, hasIof*/ objCabecalho, lstParcelas.LastOrDefault(), lstDampFgts: parcelaFgts);
                                                    if (!lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                        lstParcelas.Add(objParcelas);
                                                }
                                            }
                                            //  PEGAR AS SITUAÇOES DO CONTRATO PARA ATUALIZAR O ARQUIVO SITU115A.TXT
                                            if (i == reader.NumberOfPages)
                                            {
                                                if (isFinal)
                                                {
                                                    if (!lstCabecalho.Any(z => z.Id == objCabecalho.Id))
                                                        lstCabecalho.Add(objCabecalho);

                                                    string statusContrato = string.Empty;
                                                    int valor = 0;
                                                    do
                                                    {
                                                        if (line.IndexOf(':') > 1) break;
                                                        if (Regex.IsMatch(line, @"(^\d{4}\-[\s\w\-]+$)"))
                                                        {
                                                            if (int.TryParse(line.Split('-')[0], out valor))
                                                            {
                                                                statusContrato = line.Replace("-", "").Trim().Substring(1);
                                                                if (!lstSituacao.Any(k => k.Equals(statusContrato)))
                                                                    lstSituacao.Add(statusContrato);
                                                                continue;
                                                            }
                                                        }
                                                    } while ((line = strReader.ReadLine()) != null);
                                                    continue;
                                                }
                                            }
                                            continue;
                                        }
                                        if (padrao == 1)
                                        {

                                            // ARRAY DE CAMPOS QUE SÃO IGNORADOS, POIS NAO TRAZEM DADOS RELEVANTES
                                            string[] array2IgnorCampo = { "Nº", "Demonstrativo", "Emissão", "Carteira", "Nascimento", "Nome", "End.Imóvel", "Telefone", "Depósito", "Modalidade", "Bairro", "Page 1 of 1" };
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

                                            // SE FOR UM CLIENTE DO TIPO CPF (PESSOA FISICA)
                                            if (line.Split(' ').Any(c =>  Regex.IsMatch(c, @"(^\d{3}.\d{3}.\d{3}-\d{2}$)")))
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
                                        // FAZ A LEITURA DOS DADOS DO CABEÇALHO DO CONTRATO
                                        #region 
                                        if (line.Contains("Nº"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 6);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 6);
                                            continue;
                                        }

                                        if (line.Contains("Carteira"))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 8);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 8);
                                            continue;
                                        }


                                        // SE FOR UM CLIENTE DO TIPO CNPJ (JURIDICO)
                                        if(line.Split(' ').Any(y => Regex.IsMatch(y, @"(^\d{3}.\d{3}.\d{3}\/\d{4}\-\d{2}$)")))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 90);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 90);
                                            continue;
                                        }

                                        // SE FOR UM CLIENTE DO TIPO CPF (FISICA)
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

                                        if (arrayGetCampos.Any(y => line.Split(' ').Contains(y)))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDF(line, 100);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 100);
                                            continue;
                                        }

                                        if (line.Contains("Situações"))
                                        {
                                            if (line.Split(' ').Length == 1)
                                                line = strReader.ReadLine();

                                            objCabecalho.Situacao = Regex.Replace(line, @"[^0-9$]+", "");

                                            if (!lstCabecalho.Any(c => c.Id == objCabecalho.Id))
                                            {
                                                if (!string.IsNullOrWhiteSpace(objCabecalho.TipoOrigem))
                                                {
                                                    objCabecalho = businessCabecalho.PreencheCabecalho(objCabecalho);
                                                    lstCabecalho.Add(objCabecalho);
                                                }
                                            }
                                            isCabecalhoParcela = isParcelas = true;
                                            continue;
                                        }
                                        #endregion
                                    }
                                }

                                if (isNotTela16)
                                    break;
                                #endregion
                            }

                            if (isNotTela16)
                            {
                                isNotTela16 = false;
                                continue;
                            }

                            if (objParcelas != null)
                                if (!lstParcelas.Any(j => j.Id == objParcelas.Id) && (!string.IsNullOrWhiteSpace(objParcelas.Vencimento)))
                                    lstParcelas.Add(objParcelas);

                            objParcelas = null;

                            var t1 = lstCabecalho.RemoveAll(a => string.IsNullOrWhiteSpace(a.Cpf));
                            var t2 = lstParcelas.RemoveAll(r => string.IsNullOrWhiteSpace(r.Vencimento) && !r.IsAnt);
                            var t3 = lstOcorrencia.RemoveAll(o => string.IsNullOrWhiteSpace(o.Vencimento));


                            // REMOVE DA LISTA DE FGTS O CONTRATO QUE JA FOI EXTRAIDO
                            if (parcelaFgts.Count > 0)
                                itensFgts.RemoveAll(r => r.Contrato.Equals(parcelaFgts[0].Contrato.Trim()));

                            userObject = new UserObject { Contrato = objCabecalho.Contrato, PdfInfo = arquivoPdf, TotalArquivoPorPasta = totalPorPasta };
                            objContratoPdf.Contrato = lstCabecalho.FirstOrDefault().Contrato;
                            objContratoPdf.Carteira = lstCabecalho.FirstOrDefault().Carteira;
                            objContratoPdf.Bem = Ambiente.listGTBem.Find(kv => kv.Key.Contains(objContratoPdf.Contrato)).Value;


                            objContratoPdf.Cabecalhos.AddRange(lstCabecalho);
                            objContratoPdf.Parcelas.AddRange(lstParcelas);
                            objContratoPdf.Ocorrencias.AddRange(lstOcorrencia);

                            if (lstCronograma.Count > 0)
                                objContratoPdf.Cronogramas.AddRange(lstCronograma);


                            if (!lstContratosPdf.Any(pdf => pdf.Contrato.Trim().Equals(objContratoPdf.Contrato.Trim())))
                                lstContratosPdf.Add(objContratoPdf);

                            lstParcelas.Clear();
                            lstCabecalho.Clear();
                            lstOcorrencia.Clear();
                            lstCronograma.Clear();
                            parcelaFgts.Clear();
                            objContratoPdf = null;
                            objCabecalho = null;
                            its = null;
                            isParcelas = isFinal = isNotiqual = false;
                            pagina = string.Empty;

                            padrao = 0;
                            contador++;
                            _countPercent++;
                         
                            // A CADA X CONTRATOS LIDOS, ESCREVE O CONTEUDO NO ARQUIVO TEXTO DE CONTRATOS, OCORRENCIA PARCELAS...ETC
                            if (contador == 1000)
                            {
                                userObject.DescricaoPercentural = string.Format("Gerando o {0}º Lote.", countLote++);
                                backgroundWorker1.ReportProgress(_countPercent, userObject);
                                var tab = new
                                {
                                    item1 = lstContratosPdf,
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
                    catch (InvalidPdfException exFileload)
                    {
                        userObject = new UserObject { Contrato = arquivoPdf.Name, PdfInfo = arquivoPdf, TotalArquivoPorPasta = totalPorPasta, DescricaoPercentural = "** Arquivo Danificado **" };
                        _countPercent++;
                        backgroundWorker1.ReportProgress(_countPercent, userObject);

                        if (!File.Exists(diretorioDestinoLayout + @"\LogErroContratos.txt"))
                        {
                            StreamWriter item = File.CreateText(diretorioDestinoLayout + @"\LogErroContratos.txt");
                            item.Dispose();
                        }
                        using (StreamWriter sw = new StreamWriter(diretorioDestinoLayout + @"\LogErroContratos.txt", true, Encoding.UTF8))
                        {
                            StringBuilder strErro = new StringBuilder();
                            strErro.AppendLine(string.Format("CONTRATO: {0}", arquivoPdf.Name))
                                    .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                    .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", "Arquivo danificado e não pode ser carregado para leitura: " + exFileload.Message))
                                    .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                            sw.Write(strErro);
                            sw.WriteLine("================================================================================================================================================");
                        }

                        ExceptionError.RemoverTela(arquivoPdf, diretorioOrigemPdf);

                        padrao = 0;
                        contador++;

                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        _countPercent++;
                        backgroundWorker1.ReportProgress(_countPercent, null);

                        if (!File.Exists(diretorioDestinoLayout + @"\LogErroContratos.txt"))
                        {
                            StreamWriter item = File.CreateText(diretorioDestinoLayout + @"\LogErroContratos.txt");
                            item.Dispose();
                        }

                        using (StreamWriter sw = new StreamWriter(diretorioDestinoLayout + @"\LogErroContratos.txt", true, Encoding.UTF8))
                        {
                            StringBuilder strErro = new StringBuilder();
                            strErro.AppendLine(string.Format("CONTRATO: {0}", arquivoPdf.Name))
                                    .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                    .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", ex.Message))
                                    .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                            sw.Write(strErro);
                            sw.WriteLine("================================================================================================================================================");
                        }

                        ExceptionError.RemoverTela(arquivoPdf, diretorioOrigemPdf);
                        padrao = 0;
                        contador++;
                        
                    }


                    catch (Exception ex)
                    {
                        _countPercent++;
                        backgroundWorker1.ReportProgress(_countPercent, null);

                        if (!File.Exists(diretorioDestinoLayout + @"\LogErroContratos.txt"))
                        {
                            StreamWriter item = File.CreateText(diretorioDestinoLayout + @"\LogErroContratos.txt");
                            item.Dispose();
                        }
                        using (StreamWriter sw = new StreamWriter(diretorioDestinoLayout + @"\LogErroContratos.txt", true, Encoding.UTF8))
                        {
                            StringBuilder strErro = new StringBuilder();
                            strErro.AppendLine(string.Format("CONTRATO: {0}", arquivoPdf.Name))
                                    .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                    .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", ex.Message))
                                    .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                            sw.Write(strErro);
                            sw.WriteLine("================================================================================================================================================");
                        }
                        ExceptionError.RemoverTela(arquivoPdf, diretorioOrigemPdf);
                        padrao = 0;
                        contador++;

                    }
                }

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
                    item3 = diretorioDestinoLayout,
                    item4 = diretorioOrigemPdf
                };

                if(_thread != null)
                if (_thread.ThreadState == System.Threading.ThreadState.Running)
                    _thread.Join();

                businessCabecalho.PopulaContrato(tab);
                lstContratosPdf.Clear();
            }

            // ATUALIZA O AQUIVO DE SITUAÇÕES SE HOUVER ALGUMA SITUAÇÃO DE CONTRATO QUE NAO ESTEJA NO ARQUIVO SITU115A.TXT
            using (StreamWriter escrever = new StreamWriter(Directory.GetCurrentDirectory() + @"\config\SITU115A.TXT", true, Encoding.Default))
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
