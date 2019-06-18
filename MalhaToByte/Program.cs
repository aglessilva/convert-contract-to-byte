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

                       byte[] ArqPdf = File.ReadAllBytes( @"ZipFiles\" + newNameContract + "\\" +_contract.Name);

                       ZipFile.CreateFromDirectory( @"ZipFiles\" + newNameContract, @"ZipFiles\" + newNameContract + ".zip");
                       byte[] arq = File.ReadAllBytes(@"ZipFiles\" + newNameContract + ".zip");
                       Directory.Delete(@"ZipFiles\" + newNameContract, true);
                       File.Delete(@"ZipFiles\" + newNameContract + ".zip");

                       lstFile.Add(new FileCompress() { FileEncryption = arq, FileEncryptionPdf = ArqPdf, IdCompany = x.IdCompany, ContractName = newNameContract, CpfName = _cpf, DateInput = DateTime.Now.Date });

                       _contract = null;
                       its = null;
                       
                       pagina = _cpf = string.Empty;
                   });

                    int total = Cnn.FileStores(lstFile);
                    //Console.WriteLine("Total de registros {0}", total.ToString());
                    lstFile.Clear();
               });


                Directory.Delete("ZipFiles", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }

 

        }

    }
}
