using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Configuration;
using System.Text.RegularExpressions;
using MalhaToByte.DAL;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;

namespace MalhaToByte
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //int _totalLinhas = 0 , index = 0;

                //string arquivo = @"C:\Users\x194262\Desktop\DTI Financeiro\DTI13_PARCELAS_UNIFICADO.TXT";

                //List<string> lstArquivo = new List<string>() { "Part01.txt", "Part02.txt", "Part03.txt", "Part04.txt", "Part05.txt", "Part06.txt", "Part07.txt", "Part08.txt", "Part09.txt", "Part10.txt" };

                //using (StreamReader textoCount = new StreamReader(arquivo))
                //{
                //    while (!textoCount.EndOfStream)
                //    {
                //        textoCount.ReadLine();
                //        _totalLinhas++;
                //    }

                //    if (_totalLinhas++ > 99999)
                //    {

                //        lstArquivo.ForEach(x =>
                //        {
                //            FileStream fs = File.Create(@"C:\FileZip\" + x);
                //            fs.Dispose();
                //        });
                //    }

                    
                //}

                //using (StreamReader texto = new StreamReader(arquivo))
                //{
                //     StreamWriter escrever =  null;
                //    _totalLinhas = 0;

                //    while (!texto.EndOfStream)
                //    {
                //        if(_totalLinhas == 0)
                //        { 
                //            escrever= new StreamWriter(@"C:\FileZip\" + lstArquivo[index], true);
                //            }

                //        if (_totalLinhas++ <= 100000)
                //        {
                //            escrever.WriteLine(texto.ReadLine());
                //        }
                //        else
                //        {
                //            index++;
                //            _totalLinhas = 0;
                //            escrever.Dispose();
                //        }
                               
                //    }
                //}

                //List<string> filesDiv = new List<string>();

                //Directory.GetFiles(@"C:\FileZip\").ToList().ForEach(g => {
                //    if (new FileInfo(g).Length > 0)
                //    {
                //        filesDiv.Add(g);
                //        Console.WriteLine("Arquivos: {0}", g);
                //    }
                        
                //});

            //string _fileZip = ConfigurationManager.AppSettings["pathFileCofre"].ToString();
             // @"(?<!\d)0+(?=\d)" remove todos os zeeros a esquerda


                Console.WriteLine(DateTime.Now.ToShortTimeString());
              

                if (!Directory.Exists("ZipFiles"))
                    Directory.CreateDirectory("ZipFiles");

                List<PathFiles> _path = Cnn.GetPathFileCompany();
                List<FileCompress> lstFile = new List<FileCompress>();

                string newNameContract = string.Empty;
                string _cpf = string.Empty;
                string pagina = string.Empty;
                ;
                   
                _path.ForEach(x =>
               {
                   IEnumerable<string> fileContract = Directory.GetFiles(x.PathFileCompany).AsEnumerable();

                   
                   fileContract.ToList().ForEach(w =>
                   {
                       FileInfo _contract = new FileInfo(w);
                       ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                       using (PdfReader reader = new PdfReader(w))
                       {
                            pagina  = PdfTextExtractor.GetTextFromPage(reader, 1, its);

                           IEnumerable<string> strArray = pagina.Split('\n').Take(15).Where(g => g.Contains("C.P.F"));
                            _cpf = Regex.Replace(strArray.First().Trim(), @"[^0-9$]+", "").Substring(0, 11);
                       }
                       
                       newNameContract = _contract.Name.Split('_')[0].Trim();

                       if (Directory.Exists(@"ZipFiles\" + newNameContract))
                           Directory.Delete(@"ZipFiles\" + newNameContract, true);
                       
                       Directory.CreateDirectory(@"ZipFiles\" + newNameContract);

                       File.Copy(_contract.FullName, @"ZipFiles\" + newNameContract + "\\" +_contract.Name);

                       ZipFile.CreateFromDirectory( @"ZipFiles\" + newNameContract, @"ZipFiles\" + newNameContract + ".zip");
                       byte[] arq = File.ReadAllBytes(@"ZipFiles\" + newNameContract + ".zip");

                       Directory.Delete(@"ZipFiles\" + newNameContract, true);
                       File.Delete(@"ZipFiles\" + newNameContract + ".zip");

                       lstFile.Add(new FileCompress() { FileEncryption = arq, IdCompany = x.IdCompany, ContractName = newNameContract, CpfName = _cpf, DateInput = DateTime.Now.Date });

                       _contract = null;
                       its = null;
                       
                       pagina = _cpf = string.Empty;
                   });

                    int total = Cnn.FileStores(lstFile);
                    Console.WriteLine("Total de registros {0}", total.ToString());
                    lstFile.Clear();
               });


                Directory.Delete("ZipFiles", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }

          

            Console.WriteLine("Arquivos dvididos com sucesso!");
            Console.WriteLine(DateTime.Now.ToShortTimeString());
            Console.ReadKey();

        }

    }
}
