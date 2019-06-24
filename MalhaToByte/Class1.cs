using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvertToByte
{
    class Class1
    {


        static void Main(string[] args)
        {

            string texto = @"16.642 01/06/2019\nNº Data Base: 1/4\nDemonstrativo de Evolução do Financiamento\nv19126/1035 04/06/2019 10:18:27\nData de Emissão: CTFIN/O016A\n01-BANCO SANTANDER SA 0007 BANESPA 2154.23001.111-1\nCarteira : Contrato: \nSEVERINO PAULO DA SILVA C.P.F. 106.619.778-46 18/06/1967\nData Nascimento:\nNome:\nRUA HERCULANO DE FREITAS 75     CERQUEIRA CESAR 01.308-020 SAO PAULO SP\nEnd.Imóvel: Bairro: CEP: Cidade: UF:\nEnd.Correspondência: SP\nR COSTA 41 AP 54    Bairro: CONSOLACAO CEP: 01.304-010 Cidade: SAO PAULO UF:\nCliente: 0 4951359 (10) 3237-3682 (00) 0000-0000\nTelefone Residencial: Telefone Comercial:\nCategoria:\n 32 CANAL EXTERNO SUL (SUPERRANKIN 91 SC PARC ATUAL C/ REDUCAO TAXA 0332154010155893\nModalidade: Conta Depósito:\nTx CET Ano:11,92 Tx CET Mes:0,93 Cartorio:1                    CAC:0000000000 PIS:000.00000.00-0 DATA CADOC:20150520\nPlano          PCM/SAC Data Contrato        20/05/2015 Origem de Recursos                11 Prestação          2.470,21\nSistema               SAC Valor Financiamento        224.000,00 Código Contábil  0107111100000000 Seguro MIP            122,88\nData Garanta        20/05/2015 Agência          002154.7 Seguro DFI             27,99\nReajuste           PCM/POU\nPrazo               384 Valor Garanta        280.000,00 Empreendimento          000000.0 TAXA             25,00\nTaxa Juros           10,1083 Data 1º Vencimento        20/06/2015 Apólice            000131 Razão              4,91\nCorreção           PCM/POU Data Inclusao        19/05/2015\nTipo Financiamento Contrato Normal Data Ult. Alteração        20/05/2019\nTipo de Origem Habitac.c/ Apólice Seguro Esp.\nSituações 162 171 221 232\nSeguro TAXA Juros\nVencimento Pagamento Núm. Prazo Índice Prestação Encargo Amortizações Saldo Devedor\nNovo Vencimento Banco Agência TPG EVE HIS Proc.Emi/Pag. FGTS Aj. Util. Aj. Gerado Pago Mora\n20/05/2015 ANT 224.000,00\n20/06/2015 COR 1,001827 409,25 224.409,25\n20/06/2015 22/06/2015 001/384 1,001827 2.474,72 151,15 25,00 2.650,87 1.890,33 584,39 223.824,85\n033 002154.7 002 (01/06/2015 22/06/2015) 2.650,87\n20/07/2015 COR 1,001471 329,25 224.154,10\n20/07/2015 20/07/2015 002/384 1,001471 2.473,42 151,06 25,00 2.649,48 1.888,17 585,25 223.568,85\n033 002154.7 002 (01/07/2015 20/07/2015) 2.649,48\n20/08/2015 COR 1,002359 527,40 224.096,25\n20/08/2015 20/08/2015 003/384 1,002359 2.474,32 151,09 25,00 2.650,41 1.887,69 586,63 223.509,61\n033 002154.7 002 (01/08/2015 20/08/2015) 2.650,41\n20/09/2015 COR 1,001542 344,65 223.854,26\n20/09/2015 21/09/2015 004/384 1,001542 2.473,19 151,00 25,00 2.649,19 1.885,65 587,54 223.266,73\n033 002154.7 002 (01/09/2015 21/09/2015) 2.649,19\n20/10/2015 COR 223.542,91\n 1,001237 276,18";

            int total = texto.Count(f => f.Equals("COR"));

        
                int posicao = texto.IndexOf("COR", 1880,30);

            string[] txt = texto.Replace("COR", "*").Split('*');
            



            Environment.Exit(0);
                //int countItem = 0;

                //using (StreamWriter sw = new StreamWriter(@"D:\PDFSTombamento\txt\LayContrato.txt"))
                //{
                //    var arr = Directory.GetFiles(@"D:\PDFSTombamento", "*.pdf", SearchOption.TopDirectoryOnly);
                //    string strlinhacont = "";
                //    arr.ToList().ForEach(q =>
                //    {
                //        FileInfo f = new FileInfo(arr[countItem++]);
                //        using (StreamReader _lerCont = new StreamReader(@"D:\ALTA\TL16CONT.TXT"))
                //        {
                //            while (!_lerCont.EndOfStream)
                //            {
                //                strlinhacont = _lerCont.ReadLine();
                //                if (strlinhacont.Substring(1, 14).Equals(f.Name.Split('_')[0]))
                //                {
                //                    sw.WriteLine(strlinhacont);
                //                }

                //            }

                //        }
                //    });
                //}







            string pagina = string.Empty;
            Stopwatch stopwatch = new Stopwatch();


            IEnumerable<string> fileContract = Directory.GetFiles(@"C:\Blocado").AsEnumerable();
            
            Console.WriteLine("Aguarde...");
            StringBuilder str = new StringBuilder();
            int contador = 0;
            stopwatch.Reset();
            stopwatch.Start();
            fileContract.ToList().ForEach(w =>
                    {
                        FileInfo _contract = new FileInfo(w);
                        ITextExtractionStrategy its;
                        using (PdfReader reader = new PdfReader(w))
                        {
                            
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                                pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                               // pagina = Regex.Replace(PdfTextExtractor.GetTextFromPage(reader, i, its).Trim(), @"[^áéíóúàèìòùâêîôûãõç:\\sA-Za-z0-9$]+", " ");
                                pagina = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));
                                str.Append(pagina);
                            }
                           
                            its = null;
                           
                        }
                      
                        using (StreamWriter sr = new StreamWriter(_contract.DirectoryName+@"\txt\"+  System.IO.Path.ChangeExtension(_contract.Name,"txt")))
                        {
                            
                            sr.Write(str.ToString());
                            contador++;
                        }

                       
                        str.Clear();
                    
                       
                    });
            stopwatch.Stop();
            Console.Clear();

            string tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            Console.WriteLine(tmp);
            Console.WriteLine("Total convertido:{0}", contador);
            Console.ReadKey();
        }

    }
}
