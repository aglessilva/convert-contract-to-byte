using ErikEJ.SqlCe;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessTela18
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
                    parcelaFgts.TipoLinha = _ArrayLine[0].Trim();
                    parcelaFgts.DataVencimento = Regex.Replace(_ArrayLine[count++].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.QuotaNominal = Regex.Replace(_ArrayLine[count++].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.SaldoFgtsJAM = Regex.Replace(_ArrayLine[count++].Trim(), @"[^0-9\/$]", "");

                    if (_ArrayLine.Length > 5)
                        parcelaFgts.SobraMesJAM = Regex.Replace(_ArrayLine[(count)] , @"[^0-9$]", "");

                    parcelaFgts.SobraAcumuladaJAM = Regex.Replace(_ArrayLine[(_ArrayLine.Length > 5 ? (count+1) : count)].Trim(), @"[^0-9$]", "");
                }
                else
                {
                    parcelaFgts.TipoLinha = _ArrayLine[0].Trim();
                    //parcelaFgts.ParcelaQuota = _ArrayLine[1].Trim().Length == 5 ? Regex.Replace(_ArrayLine[1].Trim(), @"[^0-9$]", "") : "0";
                    parcelaFgts.ParcelaQuota = Regex.Replace(_ArrayLine[1].Trim(), @"[^0-9$]", "");
                    parcelaFgts.DataVencimento = Regex.Replace(_ArrayLine[2].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.SaldoFgtsQUO = Regex.Replace(_ArrayLine[4].Trim(), @"[^0-9\/$]", "");
                    parcelaFgts.SobraMes = Regex.Replace(_ArrayLine[5].Trim(), @"[^0-9\-$]", "");
                    parcelaFgts.SobraAcumulada = Regex.Replace(_ArrayLine[6].Trim(), @"[^0-9$]", "");
                    parcelaFgts.ValorUtilizado = _ArrayLine.Length <= 8 ? "0" :   Regex.Replace(_ArrayLine[8].Trim(), @"[^0-9$]", "");
                }
                
            }
            catch (Exception exOut)
            {

                throw new ArgumentOutOfRangeException("Leitura das parcelas do FGTS da TELA18- Arquivo: BusinessTela18 - Metodo: [GetParcelaFgts]", exOut.Message);
            }

            return parcelaFgts;
        }

        public void PopulaTela18(object parametro)
        {
            List<Tela18> lstTela18 = (List<Tela18>)parametro.GetType().GetProperty("item1").GetValue(parametro, null);
            string _diretorioDestino = (string)parametro.GetType().GetProperty("item2").GetValue(parametro, null);
            string strAlta, strAltaFgts;
            strAlta = strAltaFgts = string.Empty;

            using (StreamWriter escreverTela18 = new StreamWriter(_diretorioDestino + @"\TL18FGTS.txt", true, Encoding.Default))
            {
                string _contract ="", _sobraAcumulada = "", _sobraMesJam = "";

                List<ParcelaFgts> parcelaFgts = new List<ParcelaFgts>();

                lstTela18.ForEach(t18 =>
                {
                    _contract = t18.Contrato;
                    try
                    {
                        t18.Damps.ForEach(dmp => {

                            strAlta = string.Format("{0}{1}", t18.Carteira.Substring(2), t18.Contrato);
                            strAlta += string.Format("{0}{1}", dmp.NumeroDamp, dmp.ValorDamp.PadLeft(12, '0')).PadRight(27, ' ');

                            dmp.ParcelaFgts.ForEach(fgts => {
                                strAltaFgts = strAlta;

                                _sobraAcumulada = string.Format("{0}{1}", fgts.SobraAcumuladaJAM, (Convert.ToInt32(fgts.SobraAcumuladaJAM) >= 0 ? "+": "-"));

                                _sobraMesJam = string.Format("{0}{1}", fgts.SobraMesJAM, (Convert.ToInt32(fgts.SobraMesJAM) >= 0 ? "+" : "-")).PadLeft(12,'0');

                                strAltaFgts += string.Format("{0}{1}{2}{3}", _sobraMesJam, dmp.Inicio,dmp.Quantidade.PadLeft(3,'0'), _sobraAcumulada.PadLeft(12, '0')).PadRight(22, ' ');
                                strAltaFgts += string.Format("{0}{1}", fgts.SobraMes.Replace("-", "").PadLeft(11, '0'), fgts.SobraMes.Contains("-") ? "-" : "+");
                                strAltaFgts += string.Format("{0}{1}", fgts.DataVencimento.Trim(), fgts.QuotaNominal.PadLeft(12, '0'));
                                strAltaFgts += string.Format("{0}{1}", fgts.SaldoFgtsJAM.Trim().PadLeft(12,'0'),fgts.ParcelaQuota.Trim().PadLeft(5,'0'));
                                strAltaFgts += string.Format("{0}{1}+", fgts.SaldoFgtsQUO.Trim().PadLeft(12, '0'), fgts.SobraAcumulada.Trim().PadLeft(11,'0'));

                                fgts.SobraAcumuladaJAM = Convert.ToDecimal(fgts.SobraAcumuladaJAM) == 0 ? "0" : _sobraAcumulada;
                                fgts.SobraMesJAM = Convert.ToDecimal(fgts.SobraMesJAM) == 0 ? "0" : _sobraMesJam;
                                fgts.Contrato = Convert.ToInt32(t18.Carteira) + _contract;

                                escreverTela18.WriteLine(strAltaFgts);
                                strAltaFgts = _sobraAcumulada = _sobraMesJam = string.Empty;

                                parcelaFgts.Add(fgts);

                            });

                        });
                    }
                    catch (Exception exTela18)
                    {
                        string _err0 = string.Format(" Leitura das parcelas do FGTS da TELA18- Arquivo: BusinessTela18 - Metodo: [GetParcelaFgts] - Detalhes: {0}", exTela18.Message);
                        ExceptionError.TrataErros(exTela18, _contract, _err0, _diretorioDestino);
                    }
                });

                // Executa do BookInsert
                DoBulkCopy(true, parcelaFgts);
                parcelaFgts = null;
            }
        }

        public void DoBulkCopy(bool keepNulls, List<ParcelaFgts> _parcela)
        {
            DataTable dataTable = CriaTabelaFgts();
            DataRow dataRow = null;

            foreach (var item in _parcela)
            {
                if (!string.IsNullOrWhiteSpace(item.DataVencimento))
                {
                    dataRow = dataTable.NewRow();

                    dataRow["Contrato"] = item.Contrato.Trim();
                    dataRow["TipoLinha"] = item.TipoLinha.Trim();
                    dataRow["DataVencimento"] = item.DataVencimento.Trim();
                    dataRow["ParcelaQuota"] = item.ParcelaQuota.Trim();
                    dataRow["QuotaNominal"] = Convert.ToDecimal(item.QuotaNominal.Trim()) == 0 ? "0" : item.QuotaNominal.Trim();
                    dataRow["SaldoFgtsJAM"] = Convert.ToDecimal(item.SaldoFgtsJAM.Trim()) == 0 ? "0" : item.SaldoFgtsJAM.Trim();
                    dataRow["SaldoFgtsQUO"] = Convert.ToDecimal(item.SaldoFgtsQUO.Trim()) == 0 ? "0" : item.SaldoFgtsQUO.Trim();
                    dataRow["SobraMes"] = Convert.ToDecimal(item.SobraMes.Trim()) == 0 ? "0" : item.SobraMes.Trim();
                    dataRow["SobraMesJAM"] = Convert.ToDecimal(item.SobraMesJAM.Trim()) == 0 ? "0" : item.SobraMesJAM.Trim();
                    dataRow["SobraAcumulada"] = Convert.ToDecimal(item.SobraAcumulada.Trim()) == 0 ? "0" : item.SobraAcumulada.Trim();
                    dataRow["ValorUtilizado"] = Convert.ToDecimal(item.ValorUtilizado.Trim()) == 0 ? "0" : item.ValorUtilizado.Trim();

                    dataTable.Rows.Add(dataRow);
                    dataRow = null;
                }
            }

            SqlCeBulkCopyOptions options = new SqlCeBulkCopyOptions();
            if (keepNulls)
            {
                options = options |= SqlCeBulkCopyOptions.KeepNulls;
            }

            try
            {
                using (DbConnEntity connEntity = new DbConnEntity())
                {
                    if (!connEntity.Database.Exists())
                        connEntity.DampFgts.Create();

                    using (SqlCeBulkCopy bc = new SqlCeBulkCopy(connEntity.Database.Connection.ConnectionString.ToString(), options))
                    {
                        bc.DestinationTableName = "DampFgts";
                        bc.WriteToServer(dataTable);
                    }
                }
            }
            catch (Exception exEntity)
            {
                string erro = exEntity.Message;
            }
        }

        public DataTable CriaTabelaFgts()
        {
            var table = new DataTable();
            table.Columns.Add("Contrato", typeof(string));
            table.Columns.Add("TipoLinha", typeof(string));
            table.Columns.Add("DataVencimento", typeof(string));
            table.Columns.Add("ParcelaQuota", typeof(string));
            table.Columns.Add("QuotaNominal", typeof(string));
            table.Columns.Add("SaldoFgtsJAM", typeof(string));
            table.Columns.Add("SaldoFgtsQUO", typeof(string));
            table.Columns.Add("SobraMes", typeof(string));
            table.Columns.Add("SobraMesJAM", typeof(string));
            table.Columns.Add("SobraAcumulada", typeof(string));
            table.Columns.Add("ValorUtilizado", typeof(string));

            return table;
        }

        public List<ParcelaFgts> GetParcelaFgts(string _contrato)
        {
            try
            {
                using (DbConnEntity dbConn = new DbConnEntity())
                {
                    return dbConn.DampFgts.Where(c => c.Contrato.Equals(_contrato.Trim())).ToList();
                }
            }
            catch (Exception exFgts)
            {
                throw exFgts;
            }
        }
    }
}
