﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessTela18cs
    {
        public string[] GetArrayLine(string _line)
        {
            _line = Regex.Replace(_line, @"[^\wÀ-úa-zA-Z0-9.,\/\-$]+", " ");
            return _line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        public Damp GetDamp(string[] _ArrayLine)
        {
            return new Damp
            {
                NumeroDamp = _ArrayLine[0],
                Inicio = _ArrayLine[1],
                Quantidade = _ArrayLine[2],
                ValorDamp = Regex.Replace(_ArrayLine[3], @"[^0-9$]", ""),
                Percentual = Regex.Replace(_ArrayLine[4], @"[^0-9$]", ""),
                SaldoInicial = Regex.Replace(_ArrayLine[5], @"[^0-9$]", ""),
                Quota = Regex.Replace(_ArrayLine[6], @"[^0-9$]", ""),
            };
        }

        public ParcelaFgts GetParcelaFgts(string[] _ArrayLine, ParcelaFgts parcelaFgts)
        {
            try
            {
                if (_ArrayLine[0].Trim().Equals("JAM"))
                {
                    int count = 1;
                    parcelaFgts.DataVencimento = Regex.Replace(_ArrayLine[count++].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.QuotaNominal = Regex.Replace(_ArrayLine[count++].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.SaldoFgtsJAM = Regex.Replace(_ArrayLine[count++].Trim(), @"[^0-9\/$]", "");
                }
                else
                {
                    parcelaFgts.ParcelaQuota = _ArrayLine[1].Trim().Length == 5 ? Regex.Replace(_ArrayLine[1].Trim(), @"[^0-9$]", "") : "0";
                    parcelaFgts.DataVencimento = Regex.Replace(_ArrayLine[2].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.SaldoFgtsQUO = Regex.Replace(_ArrayLine[4].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.SobraMes = Regex.Replace(_ArrayLine[5].Trim(), @"[^0-9\-$]", "");
                    parcelaFgts.SobraAcumulada = Regex.Replace(_ArrayLine[6].Trim(), @"[^0-9$]", "");
                    //parcelaFgts.DataPagamento = Regex.Replace(_ArrayLine[7].Trim(), @"[^0-9\/$]", "");
                    //parcelaFgts.ValorUtilizado = Regex.Replace(_ArrayLine[8].Trim(), @"[^0-9\/$]", "");
                }
                
            }
            catch (System.Exception)
            {

                throw;
            }

            return parcelaFgts;
        }


        public void PopulaTela18(object parametro)
        {
            List<Tela18> lstTela18 = (List<Tela18>)parametro.GetType().GetProperty("item1").GetValue(parametro, null);
            string _diretorioDestino = (string)parametro.GetType().GetProperty("item3").GetValue(parametro, null);
            string _diretorioOrigem = (string)parametro.GetType().GetProperty("item4").GetValue(parametro, null);
            string strAlta, strAltaFgts;
            strAlta = strAltaFgts = string.Empty;

#if DEBUG
            _diretorioDestino = @"D:\PDFSTombamento\txt";
            _diretorioOrigem = @"D:\PDFSTombamento\";

#endif
            using (StreamWriter escreverTela18 = new StreamWriter(_diretorioDestino + @"\TL18FGTS.txt", true, Encoding.UTF8))
            {
               // int x = 0, y = 0;

                lstTela18.ForEach(t18 =>
                {
                    try
                    {
                        t18.Damps.ForEach(dmp => {

                            strAlta = string.Format("{0}{1}", t18.Carteira.Substring(2), t18.Contrato);
                            strAlta += string.Format("{0}{1}", dmp.NumeroDamp, dmp.ValorDamp.PadLeft(12, '0')).PadRight(39, ' ');
                            strAlta += string.Format("{0}{1}", dmp.Inicio, dmp.Quantidade.PadLeft(3,'0')).PadRight(22, ' ');

                            dmp.ParcelaFgts.ForEach(fgts => {
                                strAltaFgts = strAlta;
                               
                                strAltaFgts += string.Format("{0}{1}", fgts.SobraMes.Replace("-","").PadLeft(11, '0') , fgts.SobraMes.Contains("-") ? "-" : "+");
                                strAltaFgts += string.Format("{0}{1}", fgts.DataVencimento.Trim(), fgts.QuotaNominal.PadLeft(12, '0'));
                                strAltaFgts += string.Format("{0}{1}", fgts.SaldoFgtsJAM.Trim().PadLeft(12,'0'),fgts.ParcelaQuota.Trim().PadLeft(5,'0'));
                                strAltaFgts += string.Format("{0}{1}+", fgts.SaldoFgtsQUO.Trim().PadLeft(12, '0'), fgts.SobraAcumulada.Trim().PadLeft(11,'0'));
                                escreverTela18.WriteLine(strAltaFgts);
                                strAltaFgts = string.Empty;
                            });

                        });
                    }
                    catch (System.Exception ex)
                    {
                        //string erro = ex.Message + x + " - " + y ;
                        throw;
                    }
                });
            }
        }
    }
}
