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
            Console.WriteLine("Lendo contratos...");
            string PathFileCompany = @"C:\Temp1";
            try
            {
                string newNameContract = string.Empty;
                int contador = 0, totalContratos = 0, numLote = 1;
                List<FileCompress> lstFile = new List<FileCompress>();
                string pagina, _cpf;
                IEnumerable<string> fileContract = Directory.EnumerateFiles(PathFileCompany,"*_16.pdf", SearchOption.AllDirectories);

                   fileContract.ToList().ForEach(w =>
                   {
                       try
                       {
                           FileInfo _contract = new FileInfo(w);
                           using (PdfReader reader = new PdfReader(w))
                           {
                               ITextExtractionStrategy its;
                               pagina = _cpf = string.Empty;

                               for (int i = 1; i <= reader.NumberOfPages; i++)
                               {
                                   its = new LocationTextExtractionStrategy();
                                   pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();

                                   using (StringReader strReader = new StringReader(pagina))
                                   {
                                       string line = string.Empty;

                                       while ((line = strReader.ReadLine()) != null)
                                       {
                                           if (!line.Contains("C.P.F")) continue;

                                           _cpf = line.Split(' ').FirstOrDefault(c => Regex.IsMatch(c, @"(^\d{3}.\d{3}.\d{3}\-\d{2}$)"));
                                           _cpf = Regex.Replace(_cpf, "[^0-9$]", string.Empty);

                                           break;
                                       }
                                   }

                                   if (!string.IsNullOrWhiteSpace(_cpf))
                                       break;
                               }

                               byte[] ArqPdf = File.ReadAllBytes(_contract.FullName);
                               newNameContract = _contract.Name.Split('_')[0];

                               if (!lstFile.Any(n => n.DocumentCpf.Equals(_cpf)))
                                   lstFile.Add(new FileCompress() { FileEncryption = ArqPdf, ContractName = newNameContract, DocumentCpf = _cpf });
                           }
                           contador++;
                           _contract = null;

                           if (contador == 1000)
                           {
                               totalContratos += contador;

                               Console.WriteLine($"Aguarde...\n\nArmazenando o {numLote++}º lote de contratos\n");
                               int total = Cnn.FileStores(lstFile);
                               Console.WriteLine($"De {contador} contratos {total} foram armazenados - total lidos {totalContratos}\n\n Realizando leitura de contratos");
                               lstFile.Clear();
                               contador = 0;
                               total = 0;
                           }


                       }
                       catch (iTextSharp.text.exceptions.InvalidPdfException exload)
                       {
                           Console.WriteLine(exload.Message);
                       }

                       catch (PdfException exload)
                       {
                           Console.WriteLine(exload.Message);
                       }

                   });

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

    }
}
