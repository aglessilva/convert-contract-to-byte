using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Data;
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
            List<string> lstErr = Directory.EnumerateFiles(@"D:\Testes\", "*.err", SearchOption.AllDirectories).ToList();
            DataRow[] _dataRows = null;
            DataTable tbl16Cont = new DataTable("TL16CONT");
            tbl16Cont.Columns.Add("Contrato");
            tbl16Cont.Columns.Add("Registro");

            string linha = string.Empty;
            //using (StreamReader sw = new StreamReader(@"D:\Testes\ALTA\TL16CONT.txt", Encoding.UTF8))
            //{
            //    while (!sw.EndOfStream)
            //    {
            //        linha = sw.ReadLine();
            //        tbl16Cont.Rows.Add(linha.Substring(0, 15), linha);
            //    }
            //}


            //DataTable tbl16Ocor = new DataTable("TL16OCOR");
            //tbl16Ocor.Columns.Add("Contrato");
            //tbl16Ocor.Columns.Add("Registro");

            //using (StreamReader sw = new StreamReader(@"D:\Testes\ALTA\TL16OCOR.txt", Encoding.UTF8))
            //{
            //    linha = string.Empty;
            //    while (!sw.EndOfStream)
            //    {
            //        linha = sw.ReadLine();
            //        tbl16Ocor.Rows.Add(linha.Substring(0, 15), linha);
            //    }
            //}


            //DataTable tbl16Parc = new DataTable("TL16PARC");
            //tbl16Parc.Columns.Add("Contrato");
            //tbl16Parc.Columns.Add("Registro");

            //using (StreamReader sw = new StreamReader(@"D:\Testes\ALTA\TL16PARC.txt", Encoding.UTF8))
            //{
            //    linha = string.Empty;
            //    while (!sw.EndOfStream)
            //    {
            //        linha = sw.ReadLine();
            //        tbl16Parc.Rows.Add(linha.Substring(0, 15), linha);
            //    }
            //}


            //DataTable tbl18 = new DataTable("Tela18");
            //tbl18.Columns.Add("Contrato");
            //tbl18.Columns.Add("Registro");
            //using (StreamReader sw = new StreamReader(@"D:\Testes\ALTA\TL18FGTS.txt", Encoding.UTF8))
            //{
            //    linha = string.Empty;
            //    while (!sw.EndOfStream)
            //    {
            //        linha = sw.ReadLine();
            //        tbl18.Rows.Add(linha.Substring(0, 15), linha);
            //    }
            //}


            DataTable tbl20 = new DataTable("Tela20");
            tbl20.Columns.Add("Contrato");
            tbl20.Columns.Add("Registro");

            using (StreamReader sw = new StreamReader(@"D:\ALTA\TL20PEND.txt",Encoding.Default))
            {
               

                using (StreamWriter ttt = new StreamWriter(@"D:\Testes\ALTA\teste.txt",true,Encoding.Default))
                {
                    linha = string.Empty;
                    while (!sw.EndOfStream)
                    {
                        linha = sw.ReadToEnd();
                        ttt.Write(linha);
                        //  tbl20.Rows.Add(linha.Substring(0, 15), linha);
                    }
                }
            }


          

            //DataTable tbl25 = new DataTable("Tela25");
            //tbl25.Columns.Add("Contrato");
            //tbl25.Columns.Add("Registro");

            //using (StreamReader sw = new StreamReader(@"D:\Testes\ALTA\TL25BOLE.txt", Encoding.UTF8))
            //{
            //    linha = string.Empty;
            //    while (!sw.EndOfStream)
            //    {
            //        linha = sw.ReadLine();
            //        tbl25.Rows.Add(linha.Substring(0, 15), linha);
            //    }
            //}


            //FileInfo fileInfo = null;
            //lstErr.ForEach(k => {

            //    fileInfo = new FileInfo(k);

            //    string filtro = string.Format("Contrato = {0}", fileInfo.Name.Split('_')[0]);

            //    _dataRows = tbl16Cont.Select(filtro);
            //    if (_dataRows.Count() > 0)
            //    {
            //        foreach (DataRow item in _dataRows)
            //        {
            //            tbl16Cont.Rows.Remove(item);
            //        }
            //        _dataRows = null;
            //    }

            //    _dataRows = tbl16Parc.Select(filtro);
            //    if (_dataRows.Count() > 0)
            //    {
            //        foreach (DataRow item in _dataRows)
            //        {
            //            tbl16Parc.Rows.Remove(item);
            //        }
            //        _dataRows = null;
            //    }

            //    _dataRows = tbl16Ocor.Select(filtro);
            //    if (_dataRows.Count() > 0)
            //    {
            //        foreach (DataRow item in _dataRows)
            //        {
            //            tbl16Ocor.Rows.Remove(item);
            //        }
            //        _dataRows = null;
            //    }


            //    _dataRows = tbl18.Select(filtro);
            //    if (_dataRows.Count() > 0)
            //    {
            //        foreach (DataRow item in _dataRows)
            //        {
            //            tbl18.Rows.Remove(item);
            //        }
            //        _dataRows = null;
            //    }

            //    _dataRows = tbl20.Select(filtro);
            //    if (_dataRows.Count() > 0)
            //    {
            //        foreach (DataRow item in _dataRows)
            //        {
            //            tbl20.Rows.Remove(item);
            //        }
            //        _dataRows = null;
            //    }

            //    _dataRows = tbl25.Select(filtro);
            //    if (_dataRows.Count() > 0)
            //    {
            //        foreach (DataRow item in _dataRows)
            //        {
            //            tbl25.Rows.Remove(item);
            //        }
            //        _dataRows = null;
            //    }
            //});

            //string lineValue = string.Empty;
            //using (StreamWriter escreverContrato = new StreamWriter(@"D:\Testes\ALTA\NEW_TL16CONT.txt", true, Encoding.UTF8))
            //{
            //    foreach (DataRow item in tbl16Cont.Rows)
            //    {
            //        lineValue = string.Format("{0}",item[1].ToString());
            //        escreverContrato.WriteLine(lineValue);
            //    }
            //}


            //using (StreamWriter escreverOcorrencia = new StreamWriter(@"D:\Testes\ALTA\NEW_TL16OCOR.txt", true, Encoding.UTF8))
            //{
            //    lineValue = string.Empty;
            //    foreach (DataRow item in tbl16Ocor.Rows)
            //    {
            //        lineValue = string.Format("{0}", item[1].ToString());
            //        escreverOcorrencia.WriteLine(lineValue);
            //    }
            //}


            //using (StreamWriter escreverParcelas = new StreamWriter(@"D:\Testes\ALTA\NEW_ TL16PARC.txt", true, Encoding.UTF8))
            //{
            //    lineValue = string.Empty;
            //    foreach (DataRow item in tbl16Parc.Rows)
            //    {
            //        lineValue = string.Format("{0}", item[1].ToString());
            //        escreverParcelas.WriteLine(lineValue);
            //    }
            //}

            //using (StreamWriter escreverTela18 = new StreamWriter(@"D:\Testes\ALTA\NEW_TL18FGTS.txt", true, Encoding.UTF8))
            //{
            //    lineValue = string.Empty;
            //    foreach (DataRow item in tbl18.Rows)
            //    {
            //        lineValue = string.Format("{0}", item[1].ToString());
            //        escreverTela18.WriteLine(lineValue);
            //    }
            //}

            //using (StreamWriter escreverPendencia = new StreamWriter(@"D:\Testes\ALTA\NEW_TL20PEND.txt", true, Encoding.UTF8))
            //{
            //    lineValue = string.Empty;
            //    foreach (DataRow item in tbl20.Rows)
            //    {
            //        lineValue = string.Format("{0}", item[1].ToString());
            //        escreverPendencia.WriteLine(lineValue);
            //    }
            //}


            //using (StreamWriter escreverBoletim = new StreamWriter(@"D:\Testes\ALTA\NEW_TL25BOLE.txt", true, Encoding.UTF8))
            //{
            //    lineValue = string.Empty;
            //    foreach (DataRow item in tbl25.Rows)
            //    {
            //        lineValue = string.Format("{0}", item[1].ToString());
            //        escreverBoletim.WriteLine(lineValue);
            //    }
            //}
        }

        
    }
}
