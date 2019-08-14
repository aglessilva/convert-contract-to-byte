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
            List<string> lstArquiPoint = new List<string>();

            //var arr = Directory.GetFiles(@"U:\TOMBAMENTO_PF\Processamento\Processamento Agles\", "*_25.pdf", SearchOption.AllDirectories).ToList();

            string damp = string.Empty;
            using (StreamReader sw = new StreamReader(@"D:\PDFSTombamento\RELADAMP.txt",  Encoding.UTF8))
            {
                using (StreamWriter escr = new StreamWriter(@"D:\PDFSTombamento\COMPARE_RELADAMP.txt", true, Encoding.UTF8))
                {

                    while (!sw.EndOfStream)
                    {
                        damp = sw.ReadLine().Substring(0, 15);
                        if (!lstArquiPoint.Any(dmp => dmp.Equals(damp)))
                        {
                            lstArquiPoint.Add(damp);
                            escr.WriteLine(damp);
                        }
                        damp = string.Empty;

                    }
                }
            }

        }

        
    }
}
