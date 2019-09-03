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
            string PathFileCompany = @"D:\PDFSTombamento\Massa_de_Desenvolvimento\2019-06-03\T004Z20\TELA16";
            try
            {
                string newNameContract = string.Empty;
                List<FileCompress> lstFile = new List<FileCompress>();

                IEnumerable<string> fileContract = Directory.EnumerateFiles(PathFileCompany);

                   fileContract.ToList().ForEach(w =>
                   {
                       FileInfo _contract = new FileInfo(w);

                       byte[] ArqPdf = File.ReadAllBytes(_contract.FullName);
                       newNameContract = _contract.Name.Split('_')[0];
                       lstFile.Add(new FileCompress() { FileEncryption =  ArqPdf, ContractName = newNameContract });

                       _contract = null;

                   });

                   int total = Cnn.FileStores(lstFile);


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

    }
}
