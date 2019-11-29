using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
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

            // GetTableExcel();

            IEnumerable<string> fileContract = Directory.EnumerateFiles(@"C:\@TombamentoV1_01\TOMBAMENTO2019-10-25", "*_16.pdf", SearchOption.AllDirectories);

            FileInfo f = null;
            using (StreamWriter sw = new StreamWriter(@"C:\@TombamentoV1_01\config\ARQUPONT.txt", true, Encoding.ASCII))
            {
                //using (StreamReader sr = new StreamReader(@"C:\TombamentoV1_01\ALTA\TL16CONT.txt"))
                //{
                //    while (!sr.EndOfStream)
                //    {
                fileContract.ToList().ForEach(w =>
                {
                    f = new FileInfo(w);

                    sw.WriteLine( f.Name.Split('_')[0]);
                });

                //    }
                //}
            }
        }



        public static string[] GetTableExcel()
        {
            string con =
   @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\!ZONA\Pasta1.xlsx;" +
   @"Extended Properties='Excel 8.0;HDR=Yes;'";
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select CODIGO, NOMES from  [Planilha1$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //var row1Col0 = dr[0];
                        Console.WriteLine($"{dr["CODIGO"].ToString()} - {dr["NOMES"].ToString()}");
                    }
                }
            }

            Console.ReadLine();
            return null;
        }

    }
}
