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
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class frmGerarLayoutAlta : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        List<string> _situacoesAtual = new List<string>();
        int contador = 0, countLote = 1, totalArquivo = 0;
        bool isErro = false, isFinal = false;
        IEnumerable<string> listContratoBlockPdf = null;
        string diretorioOrigemPdf, diretorioDestinoLayout, tmp;

        public frmGerarLayoutAlta(string _diretoioPdf, string _diretorioDestino)
        {
            diretorioOrigemPdf = _diretoioPdf; diretorioDestinoLayout = _diretorioDestino;
            InitializeComponent();
        }

        private void FrmGerarLayoutAlta_Load(object sender, EventArgs e)
        {
            try
            {
                // LOCALIZA SOMENTE OS ARQUIVOS PDF
                listContratoBlockPdf = Directory.EnumerateFiles(diretorioOrigemPdf, "*.pdf", SearchOption.TopDirectoryOnly);

                if (listContratoBlockPdf.Count() == 0)
                {
                    MessageBox.Show("No diretório " + diretorioOrigemPdf + ", não existe contratos(pdf) para extração.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }

                lblContrato.Text = "-";
                lblTempo.Text = "";
                lblPendente.Text = "";
                lblQtd.Text = "Total: " + listContratoBlockPdf.Count().ToString();
                totalArquivo = listContratoBlockPdf.Count();
                progressBarReaderPdf.Maximum = listContratoBlockPdf.Count();


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
            tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            progressBarReaderPdf.Value = e.ProgressPercentage;
            lblQtd.Text = string.Format("Total de Contratos:{0} - Pendente:{1}", totalArquivo, (totalArquivo - e.ProgressPercentage));
            lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
            lblTempo.Text = tmp;
            lblContrato.Text = string.Format("Contrato: {0}", e.UserState.ToString().Split('#')[0]);
            lblPendente.Text = string.Format("Arquivo: {0}", e.UserState.ToString().Split('#')[1].Split('\\')[2]);
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isErro)
                MessageBox.Show("Processo finalizado COM erros!", "Erro de Converção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("Processo finalizado SEM erros!", "Sucesso de Converção", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            int numberPage = 0, countExption = 0;
            List<string> lstSituacao = new List<string>();
            List<string> lstGT = new List<string>();
            List<Cabecalho> lstCabecalho = null;
            List<Parcela> lstParcelas = null;
            List<Ocorrencia> lstOcorrencia = null;
            List<ContratoPdf> lstContratosPdf = new List<ContratoPdf>();

            StringBuilder strLayoutContrato = new StringBuilder();
            StringBuilder strLayoutOcorrencia = new StringBuilder();

            BusinessCabecalho businessCabecalho = null;
            BusinessParcelas businessParcelas = null;

            Cabecalho objCabecalho = null;
            Parcela objParcelas = null;
            ContratoPdf objContratoPdf = null;

            FileInfo arquivoPdf = null;

            string[] lnhasDesconsideradas = { "Telefone", "Modalidade", "Encargo", "Índice", "Devedor", "Proc.Emi/Pag", "Gerado", "Demonstrativo", "Nome", "QTDE", "TOTAL", "Aberto", "End.Correspondência", "CONTRATO LIQUIDADO" };
            string[] arrayIgonorCampo = { "Nº", "CTFIN", "Emissão",  "TAXA", "ANT", "PRO", "Novo"  };
            string[] arrayOcorrencia = { "vencimento", "Amortização", "Sinistro", "Consolidação", "juros", "Transf.Parte", "contratual", "extra", "saldo","garantia", "Liquidação" };
            string pagina = string.Empty, readerContrato = string.Empty;
            string[] cabecalho = null;
            string[] arrayLinhaParcela = null;
            bool isParcelas = false, isNotiqual = false, hasTaxa = true; 
            int padrao = 0, _countPercent = 1; 
         

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

            listContratoBlockPdf.ToList().ForEach(w =>
            {

                ITextExtractionStrategy its;
                try
                {
                    isFinal = false;
                    arquivoPdf = new FileInfo(w);
                    objContratoPdf = new ContratoPdf();
                    objCabecalho = new Cabecalho() { Id = (lstCabecalho.Count + 1) };
                    objParcelas = new Parcela() { IdCabecalho = objCabecalho.Id, Id = 0 };
                    objParcelas = businessParcelas.PreencheParcela(objParcelas);

                    using (PdfReader reader = new PdfReader(w))
                    {
                        pagina = string.Empty;
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            countExption = 0;
                            numberPage = i;
                            hasTaxa = true;
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
                                    if (line.Trim().Equals("a")) continue;
                                    if (string.IsNullOrWhiteSpace(line)) continue;
                                    if (lnhasDesconsideradas.Where(k => line.Contains(k)).Count() > 0)
                                    {
                                        if (line.Contains("Índice"))
                                            hasTaxa = line.Contains("TAXA");

                                        continue;
                                    }

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
                                    }

                                    if (isParcelas)
                                    {
                                        if (arrayIgonorCampo.Where(k => line.Contains(k)).Count() > 0) continue;

                                        // indica que não existe mais dados relevantes, e lê os restante das linhas sem qualquer ação
                                        if (line.Contains("FGTS/Prestação") || line.Contains("Aberto"))
                                            while ((line = strReader.ReadLine()) != null)
                                            {
                                                if (line.Split('-').Length == 2)
                                                {
                                                    string[] _arraySituacao = line.Split('-');
                                                    if (_arraySituacao[0].Length == 4)
                                                        break;
                                                }
                                                continue;
                                            }

                                        // Sair do loop após o preenchimento da lista de Situações indicando o final do arquivo
                                        if (string.IsNullOrWhiteSpace(line)) break;

                                        isNotiqual = businessParcelas.ValidaLinha(line);

                                        if (!isNotiqual)
                                            arrayLinhaParcela = businessParcelas.TrataArray(line);
                                        else
                                        {
                                            arrayLinhaParcela = businessParcelas.TratArrayPadrao2(line, pagina, countExption);
                                            countExption++;
                                        }

                                        if (arrayLinhaParcela.Any(x => arrayOcorrencia.Any(c => c.Equals(x))))
                                        {
                                            if (!arrayLinhaParcela.Contains("Quota"))
                                            {
                                                Ocorrencia objCorrencia = businessParcelas.TrataOcorrencia(arrayLinhaParcela);
                                                objCorrencia.Contrato = objCabecalho.Contrato;
                                                objCorrencia.IdParcela = objParcelas.Id;
                                                objCorrencia.IdCabecalho = objCabecalho.Id;
                                                lstOcorrencia.Add(objCorrencia);
                                            }
                                            continue;
                                        }

                                        // PEGA A LINHA DE DAMP (se houver DAMP na parcela)
                                        if (arrayLinhaParcela.Any(x => x.Equals("DAMP")))
                                        {
                                            objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 5, hasTaxa);
                                            continue;
                                        }

                                        // PEGA A LINHA DE PAGAMENTO
                                        if (arrayLinhaParcela.Length >= 9)
                                        {
                                            if (!isFinal)
                                            {
                                                objParcelas = new Parcela()
                                                {
                                                    Id = (lstParcelas.Count + 1),
                                                    IdCabecalho = objCabecalho.Id
                                                };

                                                objParcelas = businessParcelas.PreencheParcela(objParcelas);
                                                objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 2, hasTaxa);
                                            }
                                            continue;
                                        }
                                        // PEGA A LINHA DE BANCO E AGENCIA
                                        if (arrayLinhaParcela.Any(g => g.Trim().Equals("033") || g.Trim().Equals("999")))
                                        {
                                            objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 3, hasTaxa);
                                            if (!lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                lstParcelas.Add(objParcelas);
                                            else
                                            {
                                                lstParcelas.Remove(objParcelas);
                                                lstParcelas.Add(objParcelas);
                                            }
                                            continue;
                                        }
                                        // PEGA A LINHA DE CORREÇÃO
                                        if (arrayLinhaParcela.Any(x => x.Equals("COR")))
                                        {
                                            if (lstParcelas.Count > 0)
                                            {
                                                // Se o objeto "objParcela" estiver preenchido mas não contar na lista, então add o mesmo na lista
                                                // para ser pesquisado as ocorrencias referente a parcela
                                                if (!lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                    lstParcelas.Add(objParcelas);
                                                else
                                                {
                                                    lstParcelas.Remove(objParcelas);
                                                    lstParcelas.Add(objParcelas);
                                                }

                                                objParcelas = lstParcelas.FirstOrDefault(p => p.Id == objParcelas.Id);
                                            }

                                            if (arrayLinhaParcela.Length > 4)
                                                objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 1, hasTaxa);
                                        }
                                       
                                        if (arrayLinhaParcela.Any(v => v.Trim().Equals("00/00/0000")))
                                        {
                                            if (lstParcelas.Count > 0)
                                                if (lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                {
                                                    objParcelas = lstParcelas.SingleOrDefault(p => p.Id == objParcelas.Id);
                                                }
                                                else
                                                {
                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 4, hasTaxa);
                                                    if (!lstParcelas.Any(p => p.Id == objParcelas.Id))
                                                        lstParcelas.Add(objParcelas);
                                                    else
                                                    {
                                                        lstParcelas.Remove(objParcelas);
                                                        lstParcelas.Add(objParcelas);
                                                    }
                                                }

                                            isFinal = true;
                                        }


                                        //  PEGAR AS SITUAÇOES DO CONTRATO PARA ATUALIZAR O ARQUIVO SITU115A.TXT
                                        if (i == reader.NumberOfPages)
                                        {
                                            if (isFinal)
                                            {
                                                string statusContrato = string.Empty;
                                                string[] _arraySituacao = null;

                                                while ((line = strReader.ReadLine()) != null)
                                                {
                                                   if (line.IndexOf(':') > 1) continue;
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
                                                
                                            }
                                        }
                                        continue;
                                    }



                                    if (padrao == 1)
                                    {
                                        // array de Campos que são ignorados, pois nao trazem dados relevantes
                                        string[] array2IgnorCampo = { "Nº", "Demonstrativo", "Emissão", "Carteira", "Nascimento", "Nome", "End.Imóvel", "Telefone", "Depósito", "Modalidade", "Bairro" };
                                        if (array2IgnorCampo.Where(k => line.Contains(k)).Count() > 0) continue;

                                        if (string.IsNullOrWhiteSpace(objCabecalho.Numero))
                                        {
                                            cabecalho = businessCabecalho.TrataArray(line);
                                            objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 6);
                                            continue;
                                        }
                                        if (string.IsNullOrWhiteSpace(objCabecalho.DataEmicao))
                                        {
                                            objCabecalho.DataEmicao = businessCabecalho.TrataArray(line)[1];
                                            continue;
                                        }
                                        if (string.IsNullOrWhiteSpace(objCabecalho.Carteira))
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
                                        if (string.IsNullOrWhiteSpace(objCabecalho.EnderecoImovel))
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

                                        if (string.IsNullOrWhiteSpace(objCabecalho.Categoria))
                                        {
                                            cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 6);
                                            if (cabecalho.Length == 1 && line.Contains("CADOC"))
                                                objCabecalho.DataCaDoc = cabecalho[0].Trim();
                                            else
                                                objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 6);
                                            padrao = 0;
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
                                    if (line.Contains("C.P.F."))
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
                                    if (line.Contains("Situações"))
                                    {
                                        isParcelas = true;
                                        objCabecalho.Situacao = Regex.Replace(line, @"[^0-9$]+", "");
                                        lstCabecalho.Add(objCabecalho);
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
                                        //objCabecalho.Apolice = Regex.Replace(line, @"[^0-9$]", "");
                                        continue;
                                    }
                                }
                            }

                            #endregion
                        }


                        objParcelas = null;

                        readerContrato = objCabecalho.Contrato + "#" + w;
                        objContratoPdf.Contrato = lstCabecalho.FirstOrDefault().Contrato;
                        objContratoPdf.Carteira = lstCabecalho.FirstOrDefault().Carteira;
                        objContratoPdf.Cabecalhos.AddRange(lstCabecalho);
                        objContratoPdf.Parcelas.AddRange(lstParcelas);
                        objContratoPdf.Ocorrencias.AddRange(lstOcorrencia);
                        lstContratosPdf.Add(objContratoPdf);


                        lstParcelas.Clear();
                        lstCabecalho.Clear();
                        lstOcorrencia.Clear();
                        objContratoPdf = null;
                        objCabecalho = null;
                        objCabecalho = null;
                        isParcelas = false;
                        isFinal = false;
                        isNotiqual = false;
                        
                        padrao = 0;

                        contador++;
                        pagina = string.Empty;

                        its = null;

                        backgroundWorker1.ReportProgress(_countPercent++, readerContrato);

                        // a cada X contratos lidos, escreve o conteudo no arquivo texto de contratos, ocorrencia parcelas...etc
                        if (contador == 1000)
                        {
                            string strStatus = string.Format("Gerando o {0}º Lote.#{1}", countLote++, w);
                            backgroundWorker1.ReportProgress(_countPercent, strStatus);
                            businessCabecalho.PopulaContrato(lstContratosPdf, lstGT, diretorioDestinoLayout);

                            lstParcelas.Clear();
                            lstCabecalho.Clear();
                            lstOcorrencia.Clear();
                            lstContratosPdf.Clear();
                            contador = 0;
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    isErro = true;
                    if (!File.Exists(arquivoPdf.DirectoryName + @"\LogErroContratos.txt"))
                    {
                        StreamWriter item = File.CreateText(arquivoPdf.DirectoryName + @"\LogErroContratos.txt");
                        item.Dispose();
                    }
                    using (StreamWriter sw = new StreamWriter(arquivoPdf.DirectoryName + @"\LogErroContratos.txt", true, Encoding.UTF8))
                    {
                        StringBuilder strErro = new StringBuilder();
                        strErro.AppendLine(string.Format("ARQUIVO: {0}", arquivoPdf.Name))
                                .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", ex.Message))
                                .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                        sw.Write(strErro);
                        sw.WriteLine("================================================================================================================================================");
                    }

                    // SE HOUVER ALGUEM CONTRATO PARA SER FORMATADO PARA ALTA, O PROCESSO FINALIZA A FORMATAÇÃO APÓS A GERAR O LOG DE ERROS 
                    if (lstContratosPdf.Count() > 0)
                        businessCabecalho.PopulaContrato(lstContratosPdf, lstGT, diretorioDestinoLayout);

                    padrao = 0;
                    contador++;
                 

                }
            });

            if (lstContratosPdf.Count() > 0)
            {
                _countPercent--;
                string strStatus = string.Format("Salvando Lotes gerados.#{0}{1}", countLote++, "*\\*\\ --- Formatação sequencial concluída! ---");
                backgroundWorker1.ReportProgress(_countPercent, strStatus);

                businessCabecalho.PopulaContrato(lstContratosPdf, lstGT, diretorioDestinoLayout);
                lstContratosPdf.Clear();
                contador = 0;
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
