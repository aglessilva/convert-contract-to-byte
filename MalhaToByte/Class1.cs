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

            IEnumerable<string> fileContract = Directory.EnumerateFiles(@"C:\TombamentoV1_01\SIMULADO2019-08-30", "*_16.pdf", SearchOption.AllDirectories);

            using (StreamWriter sw = new StreamWriter(@"C:\TombamentoV1_01\ALTA\ARQUPONT.txt", true, Encoding.ASCII))
            {
                using (StreamReader sr = new StreamReader(@"C:\TombamentoV1_01\ALTA\TL16CONT.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string texto = "01"+ sr.ReadLine().Substring(0,15)+"1";
                        sw.WriteLine(texto);

                    }
                }
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
