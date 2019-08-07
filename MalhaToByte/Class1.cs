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

            var arr = Directory.GetFiles(@"D:\PDFSTombamento\2019-06-27", "*.sel", SearchOption.AllDirectories).ToList();


            for (int i = 0; i < arr.Count; i++)
            {
                FileInfo f = new FileInfo(arr[i]);

                string[] lst = Directory.EnumerateFiles(@"D:\PDFSTombamento\2019-06-27", string.Format("*{0}_16.pdf", f.Name.Split('_')[0]), SearchOption.AllDirectories).ToArray();

                if (lst.Length > 0)
                    if (File.Exists(lst[0]))
                    {
                        if (File.Exists(@"D:\PDFSTombamento\filtro\" + f.Name.Split('.')[0] + ".Err"))
                            File.Delete(@"D:\PDFSTombamento\filtro\" + f.Name.Split('.')[0] + ".Err");

                        File.Move(lst[0], @"D:\PDFSTombamento\filtro\" + f.Name.Split('.')[0] + ".Err");
                    }
            }

        }

        
    }
}
