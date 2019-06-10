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
        int contador = 0;
        IEnumerable<string> listContratoBlockPdf = null;
        string diretorioOrigemPdf, diretorioDestinoLayout;
       
        public frmGerarLayoutAlta(string _diretoioPdf, string _diretorioDestino)
        {
            diretorioOrigemPdf = _diretoioPdf; diretorioDestinoLayout = _diretorioDestino;

            InitializeComponent();
        }

        private void frmGerarLayoutAlta_Load(object sender, EventArgs e)
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
                lblRotuloTelas.Text = "";
                lblPendente.Text = "";
                lblQtd.Text = listContratoBlockPdf.Count().ToString();
                progressBarReaderPdf.Maximum = listContratoBlockPdf.Count();


                using (StreamReader lerTxt = new StreamReader(diretorioOrigemPdf + @"\SITU115A.TXT"))
                {
                    while(!lerTxt.EndOfStream)
                        _situacoesAtual.Add(lerTxt.ReadLine());
                };

                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception)
            {
                
                throw;
            }
          
            
            stopwatch.Restart();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isErro = false;
            int numberPage = 0;
            List<string> lstSituacao = new List<string>();
            List<string> lstGT = new List<string>();
            List<Cabecalho> lstCabecalho = null;
            List<Parcela> lstParcelas = null;
            List<Ocorrencia> lstOcorrencia = null;
            List<ContratoPdf> lstContratosPdf = new List<ContratoPdf>();

            StringBuilder strLayoutContrato = new StringBuilder();
            StringBuilder strLayoutOcorrencia = new StringBuilder();

            BusinessCabecalho businessCabecalho =  null;
            BusinessParcelas businessParcelas =  null;

            Cabecalho objCabecalho = null;
            Parcela objParcelas =  null;
            ContratoPdf objContratoPdf = null;

            FileInfo arquivoPdf = null;

            string[] indices = { "Telefone", "Modalidade", "Encargo", "Índice", "Devedor", "Proc.Emi/Pag", "Gerado", "Demonstrativo", "Nome","QTDE","TOTAL", "Aberto" };
            string[] arrayIgonorCampo = { "Nº", "CTFIN", "Emissão", "SANTANDER", "TAXA", "ANT","PRO", "Novo" };
            string[] arrayOcorrencia = { "vencimento", "Amortização", "Sinistro", "Consolidação", "juros", "Transf.Parte", "DAMP", "contratual", "extra", "saldo" };
            string pagina = string.Empty;
            string[] cabecalho = null;
            string[] arrayLinhaParcela =  null;
            bool isParcelas = false;
            int padrao = 0;


            using (StreamReader sr = new StreamReader(diretorioOrigemPdf+@"\ARQ_GARANTIA.TXT"))
            {
                while(!sr.EndOfStream)
                lstGT.Add(sr.ReadLine().Trim());
            }
                listContratoBlockPdf.ToList().ForEach(w =>
                    {
                       
                       ITextExtractionStrategy its;
                        try
                        {
                            arquivoPdf = new FileInfo(w);
                            businessCabecalho = new BusinessCabecalho();
                            businessParcelas = new BusinessParcelas();
                            lstCabecalho = new List<Cabecalho>();
                            lstParcelas = new List<Parcela>();
                            lstOcorrencia = new List<Ocorrencia>();

                            objCabecalho = new Cabecalho();
                            objContratoPdf = new ContratoPdf();

                            using (PdfReader reader = new PdfReader(w))
                            {

                                pagina = string.Empty;
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    numberPage = i;
                                    its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                                    pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                                    // pagina = Regex.Replace(PdfTextExtractor.GetTextFromPage(reader, i, its).Trim(), @"[^áéíóúàèìòùâêîôûãõç:\\sA-Za-z0-9$]+", " ");
                                    pagina = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));

                                    if (!pagina.Substring(0, 10).Contains("Nº"))
                                        padrao = 1;


                                    #region Padrão 1

                                    using (StringReader strReader = new StringReader(pagina))
                                    {
                                        string line;
                                        while ((line = strReader.ReadLine()) != null)
                                        {
                                            if (line.IndexOf(':') == 2) continue;
                                            if (string.IsNullOrWhiteSpace(line)) continue;
                                            if (indices.Where(k => line.Contains(k)).Count() > 0) continue;

                                            if (i > 1 && line.Contains("C.P.F."))
                                            {
                                                var novoObj = objCabecalho;
                                                objCabecalho = new Cabecalho();
                                                objCabecalho.Carteira = novoObj.Carteira;
                                                objCabecalho.Numero = novoObj.Numero;
                                                objCabecalho.DataEmicao = novoObj.DataEmicao;
                                                objCabecalho.DataBase = novoObj.DataBase;
                                                objCabecalho.Contrato = novoObj.Contrato;
                                                novoObj = null;
                                                isParcelas = false;
                                            }

                                            if (isParcelas)
                                            {
                                                if (arrayIgonorCampo.Where(k => line.Contains(k)).Count() > 0) continue;

                                                // VERIFICA SE É A ULTIMA PAGINA PARA PEGAR AS SITUAÇOES DO CONTRATO
                                                if (i == reader.NumberOfPages)
                                                    if (line.Split('-').Length == 2)
                                                    {
                                                        string[] _arraySituacao = line.Split('-');
                                                        if (_arraySituacao[0].Length == 4)
                                                        {
                                                            string statusContrato = line.Replace("-", "").Trim();

                                                            if (!lstSituacao.Any(k => k.Equals(statusContrato)))
                                                                lstSituacao.Add(statusContrato);
                                                            continue;
                                                        }
                                                    }

                                                if (objParcelas == null)
                                                    objParcelas = new Parcela();
                                                arrayLinhaParcela = businessParcelas.TrataArray(line);

                                                
                                                if (arrayLinhaParcela.Any(x => arrayOcorrencia.Any(c => c.Equals(x))))
                                                {
                                                    if (arrayLinhaParcela.Contains("Validade") && arrayLinhaParcela.Contains("Quota"))
                                                    {
                                                        arrayLinhaParcela = businessParcelas.TrataArray(strReader.ReadLine());
                                                        Ocorrencia objCorrencia = businessParcelas.TrataOcorrencia(arrayLinhaParcela, "DAMP0");
                                                        lstOcorrencia.Add(objCorrencia);
                                                        continue;
                                                    }

                                                    if (!arrayLinhaParcela.Contains("Quota"))
                                                    {
                                                        Ocorrencia objCorrencia = businessParcelas.TrataOcorrencia(arrayLinhaParcela, arrayLinhaParcela[2].Trim());
                                                        objCorrencia.Contrato = objCabecalho.Contrato;

                                                        lstOcorrencia.Add(objCorrencia);
                                                    }
                                                    continue;
                                                }


                                                // PEGA A LINHA DE CORREÇÃO
                                                if (arrayLinhaParcela.Any(x => x.Equals("COR")))
                                                {
                                                    if (arrayLinhaParcela.Length < 4)
                                                    {
                                                        var x = arrayLinhaParcela.ToList();
                                                         arrayLinhaParcela = businessParcelas.TrataArray(strReader.ReadLine());
                                                         x.Insert(2, arrayLinhaParcela[0]);
                                                         x.Insert(3, arrayLinhaParcela[1]);
                                                         arrayLinhaParcela = x.ToArray();
                                                    }
                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 1);
                                                    continue;
                                                }


                                                // PEGA A LINHA DE PAGAMENTO
                                                if (arrayLinhaParcela.Length >= 9)
                                                {
                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 2);
                                                    continue;
                                                }

                                                // PEGA A LINHA DE BANCO E AGENCIA
                                                if (arrayLinhaParcela.Any(g => g.Trim().Equals("033") || g.Trim().Equals("999")))
                                                {
                                                    objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 3);

                                                }

                                                if (arrayLinhaParcela.Length > 1)
                                                    if (arrayLinhaParcela[1].Trim().Equals("00/00/0000"))
                                                    {
                                                        objParcelas = businessParcelas.TrataLinhaParcelas(objParcelas, arrayLinhaParcela, 4);
                                                    }

                                                if (objParcelas.Vencimento != null && objParcelas.Proc_Emi_Pag != null)
                                                {
                                                    lstParcelas.Add(objParcelas);
                                                    objParcelas = null;
                                                }

                                                continue;
                                            }


                                            if (padrao == 1)
                                            {
                                                string[] array2IgnorCampo = { "Nº", "Demonstrativo", "Emissão", "Carteira", "Nascimento", "Nome", "End.Imóvel", "Telefone", "Categoria", "Depósito", "Modalidade", "TAXA" };
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
                                                if (string.IsNullOrWhiteSpace(objCabecalho.CorrespondenciaUF))
                                                {
                                                    objCabecalho.CorrespondenciaUF = line.Split(':')[1].Trim();
                                                    continue;
                                                }
                                                if (line.Contains("Bairro"))
                                                {
                                                    cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 4);
                                                    objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 4);
                                                    continue;
                                                }
                                                if (line.Contains("Cliente"))
                                                {
                                                    cabecalho = businessCabecalho.TrataLinhaPDFPadrao2(line, 5);
                                                    objCabecalho = businessCabecalho.TrataCabecalhoPadrao2(objCabecalho, cabecalho, 5);
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
                                                objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 10);
                                                continue;
                                            }
                                            if (line.Contains("End.Correspondência"))
                                            {
                                                objCabecalho.CorrespondenciaUF = line.Split(':')[1];
                                                cabecalho = businessCabecalho.TrataLinhaPDF(strReader.ReadLine(), 11);
                                                objCabecalho = businessCabecalho.TrataCabecalho(objCabecalho, cabecalho, 11);
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
                                                string texto = Regex.Replace(strReader.ReadLine().Trim(), @"[^0-9$]+", "");
                                                objCabecalho.Categoria = texto.Substring(0, 1);
                                                objCabecalho.Modalidade = texto.Substring(1, 2);
                                                objCabecalho.ContaDeposito = texto.Substring(3);
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
                                            if (line.Contains("Razão") || line.Contains("Empreendimento"))
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
                                        }
                                    }

                                    #endregion

                                }

                                objContratoPdf.Contrato = lstCabecalho.FirstOrDefault().Contrato;
                                objContratoPdf.Cabecalhos.AddRange(lstCabecalho);
                                objContratoPdf.Parcelas.AddRange(lstParcelas);
                                objContratoPdf.Ocorrencias.AddRange(lstOcorrencia);
                                lstContratosPdf.Add(objContratoPdf);

                                lstParcelas = null;
                                lstCabecalho = null;
                                lstOcorrencia = null;
                                objCabecalho = null;
                                isParcelas = false;
                                padrao = 0;

                                contador++;
                                pagina = string.Empty;

                                its = null;
                                int countItem = 0;
                                string strAlta = string.Empty;
                                string _contratoGT = string.Empty;
                                lstContratosPdf.ForEach(q => {

                                    Cabecalho c = q.Cabecalhos.LastOrDefault();
                                    _contratoGT = lstGT.Find(p => p.Equals(c.Carteira.Substring(2, 2) + c.Contrato.Trim()));
                                    strAlta =  string.Format("{0}{1}{2}", c.Carteira.Trim().Substring(2), c.Contrato.Trim(), c.DataPrimeiroVencimento.Trim().Substring(0, 2)).PadRight(24, ' ');
                                    strAlta += string.Format("{0}{1}{2}", c.Nome.Trim().PadRight(40, ' '),  c.DataPrimeiroVencimento.Trim().PadRight(24, ' '), c.EnderecoImovel.Trim().PadRight(80, ' '));
                                    strAlta += string.Format("{0}{1}",c.Cpf.Trim().PadRight(17, ' ') , _contratoGT.Trim().PadRight(40, ' '));
                                    strAlta += string.Format("{0}{1}", c.Modalidade.Trim().PadRight(40, ' '), c.Cidade.Trim().PadRight(31, ' '));
                                    strAlta += string.Format("{0}{1}", c.Plano.Trim().PadRight(10, ' '), c.DataContrato.Trim().PadRight(12, ' '));
                                    strAlta += string.Format("{0}{1}{2}", c.OrigemRecurso.Trim(), c.Prestacao.Trim(), c.Sistema).PadLeft(19, '0').PadRight(20, ' ');
                                    strAlta += string.Format("{0}{1}{2}", c.ValorFinanciamento.Trim().PadLeft(18, '0'), c.CodigoContabil.Trim().Substring(1).PadRight(15, '0'), c.SeguroMIP.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", c.Reajuste.Trim().PadRight(10, ' '), c.DataGarantia.Trim().PadRight(18, ' '));
                                    strAlta += string.Format("{0}{1}{2}{3}", c.Agencia.Trim().PadLeft(8,'0'), c.SeguroDFI.Trim().PadLeft(10,'0'), c.Prazo.Trim().PadLeft(5,'0'), c.ValorGarantia.Trim().PadLeft(18,'0')).PadRight(49,' ');
                                    strAlta += string.Format("{0}{1}{2}", c.Empreendimento.Trim().PadRight(18, '0'), "0".PadRight(5, '0'), c.TaxaJuros.Trim());
                                    strAlta += string.Format("{0}{1}{2}{3}", c.DataPrimeiroVencimento.Trim().PadLeft(10, '0'), c.Apolice.PadRight(6, '0'), c.Cep.Trim(),"-"+ c.Bairro.Trim()).PadRight(50,' ');
                                    strAlta += string.Format("{0}{1}", "0".PadRight(11, '0'), c.Correcao.Trim().PadRight(10, ' '),c.Razao.Trim().PadLeft(20,'0'));
                                    strAlta += string.Format("{0}",c.Situacao.Trim().PadLeft(37, '0')).PadRight(82,' ');
                                    
                                    strLayoutContrato.AppendLine(strAlta);

                                    strAlta = string.Empty;

                                    q.Ocorrencias.ForEach(o => {

                                        if (new string[] { "004", "005", "010" }.Any(t => t == o.CodigoOcorrencia))
                                        {
                                            
                                            var cc = q.Cabecalhos[(countItem + 1)];
                                            strAlta = string.Format("{0}{1}", _contratoGT, o.Vencimento.Trim() + o.Pagamento.Trim() + o.Descricao.Trim().PadRight(36, ' '));
                                            strAlta += string.Format("{0}", o.CodigoOcorrencia.Trim() + "0".PadRight(18, '0') + (string.IsNullOrWhiteSpace(o.Mora) ? "0".PadRight(18, '0') : o.Mora.Trim().PadLeft(18, '0')) + "0".PadRight(15, '0'));
                                            strAlta += string.Format("{0}", o.Amortizacao.Trim().PadLeft(18, '0') + o.SaldoDevedor.Trim().PadLeft(18, '0') + Convert.ToDateTime(c.DataPrimeiroVencimento.Trim()).ToString("yyyyMMdd")).PadRight(66, ' ');
                                            strAlta += string.Format("{0}", o.Vencimento.Trim().PadLeft(20, ' ') + o.SaldoDevedor.Trim().PadLeft(18, '0') + Convert.ToDateTime(c.DataPrimeiroVencimento.Trim()).ToString("yyyyMMdd")).PadRight(66, ' ');
                                        }

                                        strAlta = string.Empty;
                                    });
                                });
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
                            using (StreamWriter sw = new StreamWriter(arquivoPdf.DirectoryName + @"\LogErroContratos.txt",true,Encoding.UTF8))
                            {
                                StringBuilder strErro = new StringBuilder();
                                strErro.AppendLine(string.Format("ARQUIVO: {0}", arquivoPdf.Name))
                                    .AppendLine(string.Format("PAGINA DO ERRO: {0}", numberPage))
                                    .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", ex.Message))
                                    .AppendLine(string.Format("DIRETORIO DO ARQUIVO: {0}", arquivoPdf.DirectoryName));
                                sw.Write(strErro);
                                sw.WriteLine("================================================================================================================================================");
                            }

                            lstParcelas = null;
                            lstCabecalho = null;
                            lstOcorrencia = null;
                            objCabecalho = null;
                            isParcelas = false;
                            padrao = 0;

                            contador++;
                            pagina = string.Empty;

                            its = null;
                          
                        }
                    });



                // ATUALIZA O AQUIVO DE SITUAÇÕES SE HOUVER ALGUMA SITUAÇÃO DE CONTRATO QUE NAO ESTEJA NO ARQUIVO SITU115A.TXT
                using (StreamWriter escrever = new StreamWriter(diretorioOrigemPdf + @"\SITU115A.TXT", true,Encoding.UTF8))
                {
                    lstSituacao.ForEach(f =>
                    {
                        if (!_situacoesAtual.Contains(f))
                            escrever.WriteLine(f);
                    });
                }

            if(isErro)
                MessageBox.Show("Finalizado COM erros!", "Erro de Converção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("Finalizado SEM erros!", "Sucesoo de Converção", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Environment.Exit(0);
        }
    }
}
