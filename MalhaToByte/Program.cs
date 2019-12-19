using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MalhaToByte.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MalhaToByte
{
    class Program
    {
       

        static void Main(string[] args)
        {
            DataTable table = null;
            DataRow dataRow = null;

            Console.WriteLine("Lendo contratos...");
            string PathFileCompany = @"C:\TombamentoV1_01\TOMBAMENTO2019-10-25";
            try
            {
                table = CriaTabelaPdf();

                string newNameContract = string.Empty;
                int contador = 0, totalContratos = 0;
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
                                           if (!line.Split(' ').Any(c => Regex.IsMatch(c, @"(^\d{3}.\d{3}.\d{3}\-\d{2}$)") || Regex.IsMatch(c, @"(^\d{3}.\d{3}.\d{3}\/\d{4}\-\d{2}$)"))) continue;

                                           _cpf = line.Split(' ').FirstOrDefault(c => Regex.IsMatch(c, @"(^\d{3}.\d{3}.\d{3}\-\d{2}$)") || Regex.IsMatch(c, @"(^\d{3}.\d{3}.\d{3}\/\d{4}\-\d{2}$)"));
                                           _cpf = Regex.Replace(_cpf, "[^0-9$]", string.Empty);

                                           break;
                                       }
                                   }

                                   if (!string.IsNullOrWhiteSpace(_cpf))
                                       break;
                               }


                               dataRow = table.NewRow();

                               dataRow["ContractName"] = _contract.Name.Split('_')[0];
                               dataRow["DocumentCpf"] = _cpf;
                               dataRow["FileEncryption"] = File.ReadAllBytes(_contract.FullName);
                               dataRow["DateInput"] = DateTime.Now;

                               table.Rows.Add(dataRow);

                           }
                           contador++;
                           _contract = null;

                           if (contador == 5000)
                           {
                               totalContratos += contador;

                               Cnn.FileStores(table);
                               Console.WriteLine($"Aguarde...\n\nTotal Arnazenado  {totalContratos} \n");
                               contador = 0;
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


                if (contador > 0)
                {
                    totalContratos += contador;

                    Cnn.FileStores(table);
                    contador = 0;
                    table = null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            Console.WriteLine("\n\n\n CONCLUIDO");
            Console.ReadKey();

        }


        private static DataTable CriaTabelaPdf()
        {
            var table = new DataTable();
            table.Columns.Add("ContractName", typeof(string));
            table.Columns.Add("DocumentCpf", typeof(string));
            table.Columns.Add("FileEncryption", typeof(byte[]));
            table.Columns.Add("DateInput", typeof(DateTime));

            return table;
        }

    }
}
