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


            int countItem = 0;

            using (StreamWriter sw = new StreamWriter(@"D:\PDFSTombamento\txt\LayContrato.txt"))
            {
                var arr = Directory.GetFiles(@"D:\PDFSTombamento", "*.pdf", SearchOption.TopDirectoryOnly);
                string strlinhacont = "";
                arr.ToList().ForEach(q =>
                {
                    FileInfo f = new FileInfo(arr[countItem++]);
                    using (StreamReader _lerCont = new StreamReader(@"D:\ALTA\TL16CONT.TXT"))
                    {
                        while (!_lerCont.EndOfStream)
                        {
                            strlinhacont = _lerCont.ReadLine();
                            if (strlinhacont.Substring(1, 14).Equals(f.Name.Split('_')[0]))
                            {
                                sw.WriteLine(strlinhacont);
                            }

                        }

                    }
                });
            }


            Environment.Exit(0);





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
