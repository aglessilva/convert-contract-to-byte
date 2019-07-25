using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Path = System.IO.Path;

namespace ConvertToByte
{
    class Class1
    {


        static void Main(string[] args)
        {

            Console.ReadKey();
            List<string> lstArquiPoint = new List<string>();

            //  Environment.Exit(0);


            using (StreamReader sw = new StreamReader(string.Format(@"{0}\Config\ARQUPONT.txt", args[0])))
            {
                while (!sw.EndOfStream)
                    lstArquiPoint.Add(sw.ReadLine().Substring(1, 16).Trim());
            }


            var arr = Directory.GetFiles(args[0], "*.pdf", SearchOption.AllDirectories).ToList();


            for (int i = 0; i < arr.Count; i++)
            {
                FileInfo f = new FileInfo(arr[i]);

                if (!lstArquiPoint.Any(g => g.Equals(f.Name.Split('_')[0])))
                {
                    Console.WriteLine("NAO TEM >> " + f.Name.Split('_')[0]);
                    if (File.Exists(f.FullName))
                    {
                        if(File.Exists(string.Format(@"{0}\NaoTem\{1}", args[0], f.Name.Split('.')[0] + ".Err")))
                            File.Delete(string.Format(@"{0}\NaoTem\{1}", args[0], f.Name.Split('.')[0] + ".Err"));

                        File.Move(f.FullName, string.Format(@"{0}\NaoTem\{1}", args[0], f.Name.Split('.')[0]+".Err"));
                    }

                }

            }


            Console.WriteLine("\a\n\n\n\t\t\t\tFINALIZADO\n\n\n\n >>> ESC para sair <<<");
            Console.ReadKey();
            //            strlinhacont = _lerCont.ReadLine();
            //            if (strlinhacont.Substring(1, 14).Equals(f.Name.Split('_')[0]))
            //            {
            //                sw.WriteLine(strlinhacont);
            //            }

            //        }

            //    }
            //});








            //string pagina = string.Empty;
            //Stopwatch stopwatch = new Stopwatch();


            //IEnumerable<string> fileContract = Directory.GetFiles(@"C:\Blocado").AsEnumerable();

            //Console.WriteLine("Aguarde...");
            //StringBuilder str = new StringBuilder();
            //int contador = 0;
            //stopwatch.Reset();
            //stopwatch.Start();
            //fileContract.ToList().ForEach(w =>
            //        {
            //            FileInfo _contract = new FileInfo(w);
            //            ITextExtractionStrategy its;
            //            using (PdfReader reader = new PdfReader(w))
            //            {

            //                for (int i = 1; i <= reader.NumberOfPages; i++)
            //                {
            //                    its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
            //                    pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
            //                   // pagina = Regex.Replace(PdfTextExtractor.GetTextFromPage(reader, i, its).Trim(), @"[^áéíóúàèìòùâêîôûãõç:\\sA-Za-z0-9$]+", " ");
            //                    pagina = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));
            //                    str.Append(pagina);
            //                }

            //                its = null;

            //            }

            //            using (StreamWriter sr = new StreamWriter(_contract.DirectoryName+@"\txt\"+  System.IO.Path.ChangeExtension(_contract.Name,"txt")))
            //            {

            //                sr.Write(str.ToString());
            //                contador++;
            //            }


            //            str.Clear();


            //        });
            //stopwatch.Stop();
            //Console.Clear();

            //string tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            //Console.WriteLine(tmp);
            //Console.WriteLine("Total convertido:{0}", contador);
            //Console.ReadKey();


        }
    }
}
