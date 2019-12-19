using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using ErikEJ.SqlCe;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessParcelas
    {

        public BusinessParcelas(){}

        public Parcela TrataLinhaParcelas(Parcela _obj, string[] _linha, int sequencia, /*bool _hasTaxa, bool _hasIof,*/ Cabecalho cabecalho, Parcela  _parcela =  null, List<ItensDamp> lstDampFgts = null)
        {

            string _case = "VAZIO";
            string _contrato = Convert.ToInt32(cabecalho.Carteira.Trim()) + cabecalho.Contrato.Trim();

            try
            {
                switch (sequencia)
                {
                    case 1: // PEGA A LINHA DE CORREÇÃO
                        {
                            _case = "Case 1 - Metodo: TrataLinhaParcelas -  PEGA A LINHA DE CORREÇÃO";

                            _obj.VencimentoCorrecao = _linha[0].Trim();
                            _obj.IndiceCorrecao = Regex.Replace(_linha[2], @"[^0-9$]", "");
                            _obj.AmortizacaoCorrecao = Regex.Replace(_linha[3], @"[^0-9$]", "") ;
                            _obj.SaldoDevedorCorrecao = Regex.Replace(_linha[4], @"[^0-9$]", "");

                            break;
                        }
                    case 2: // PEGA A LINHA DE PAGAMENTO
                        {
                            List<string> _pagamento = _linha.ToList();
                            _case = "Case 2 - Metodo: TrataLinhaParcelas - PEGA A LINHA DE PAGAMENTO";
                            int count = (_pagamento.Count - 1);

                            _obj.SaldoDevedor = Regex.Replace(_pagamento.Last(), @"[^0-9$]", ""); _pagamento.RemoveAt(count--);
                            _obj.Amortizacao = Regex.Replace(_pagamento.Last(), @"[^0-9$\-]", ""); _pagamento.RemoveAt(count--);
                            _obj.Juros = Regex.Replace(_pagamento.Last(), @"[^0-9$]", ""); _pagamento.RemoveAt(count--);
                            _obj.Encargo = Regex.Replace(_pagamento.Last(), @"[^0-9.,$]", ""); _pagamento.RemoveAt(count--);

                            if (!_pagamento.Any(l => l.Contains("INCORP")))
                            {
                                if (!Regex.IsMatch(_pagamento[1].Trim(), @"(\d{2}\/\d{2}\/\d{4})"))
                                    _pagamento.Insert(1, "01/01/0001");
                            }

                            if(!_pagamento.Any(f => Regex.IsMatch(_pagamento[3], @"(^\d{1},\d{6}$)")))
                                _pagamento.Insert(3, "0");

                            _obj.Vencimento = Regex.Replace(_pagamento.FirstOrDefault(), @"[^0-9\/$]", ""); _pagamento.RemoveAt(0);
                            _obj.Pagamento = Regex.Replace(_pagamento.FirstOrDefault(), @"[^0-9A-Z\/$]", ""); _pagamento.RemoveAt(0); 
                            _obj.NumeroPrazo = Regex.Replace(_pagamento.FirstOrDefault(), @"[^0-9$]", ""); _pagamento.RemoveAt(0);
                            _obj.Indice =  Regex.Replace(_pagamento.FirstOrDefault(), @"[^0-9$]", ""); _pagamento.RemoveAt(0);
                            _obj.Prestacao = Regex.Replace(_pagamento.FirstOrDefault(), @"[^0-9$]", "");  _pagamento.RemoveAt(0);
                            _obj.Seguro = Regex.Replace(_pagamento.FirstOrDefault(), @"[^0-9$]", ""); _pagamento.RemoveAt(0);


                            if (_pagamento.Count == 1)
                            {
                                string valorTaxaOuIof = Regex.Replace(_pagamento[0], @"[^0-9$]", "").Trim();

                                if (cabecalho.Taxa.Equals(valorTaxaOuIof))
                                {
                                    _obj.Taxa = valorTaxaOuIof;
                                    break;
                                }

                                if (cabecalho.Iof.Equals(valorTaxaOuIof))
                                {
                                    _obj.Iof = valorTaxaOuIof;
                                    break;
                                }

                                if (Convert.ToInt32(cabecalho.Taxa).Equals(0) && Convert.ToInt32(cabecalho.Iof).Equals(0))
                                    _obj.Taxa = valorTaxaOuIof;
                                else
                                    _obj.Iof = valorTaxaOuIof;
                            }   
                            else
                            {
                                if (_pagamento.Count == 2)
                                {
                                    _obj.Taxa = Regex.Replace(_pagamento[0], @"[^0-9$]", "").Trim();
                                    _obj.Iof = Regex.Replace(_pagamento[1], @"[^0-9$]", "").Trim();
                                }
                            }

                            break;
                        }
                    case 3: // PEGA A LINHA DE BANCO E AGENCIA
                        {
                            _case = "Case 3 - Metodo: TrataLinhaParcelas - PEGA A LINHA DE BANCO E AGENCIA";
                            int count = 0;
                            List<string> item = _linha.ToList();


                            // if (!item.Any(i => i.Equals("033") || i.Equals("999")))
                            if(item[0].Length > 3)
                                item.Insert(0, "999");

                            _obj.Banco = item[count].Length == 3 ? Regex.Replace(item[count++], @"[^0-9\/$]", "") : "0";
                            _obj.Agencia = Regex.IsMatch(item[count].Trim(), @"(^\d{6}.\d{1}$)") ? Regex.Replace(item[count++], @"[^0-9\/$]", "") : "0";
                            _obj.TPG_EVE_HIS = item[count].Length == 3 ? item[count++].Trim() : "0";
                            _obj.Proc_Emi_Pag = item.FirstOrDefault(d => Regex.IsMatch(d.Trim(), @"(^\d{2}\/\d{2}\/\d{4}$)")) + item.LastOrDefault(d => Regex.IsMatch(d.Trim(), @"(^\d{2}\/\d{2}\/\d{4}$)"));

                            int _id = item.ToList().FindLastIndex(d => Regex.IsMatch(d.Trim(), @"(^\d{2}\/\d{2}\/\d{4}$)"));

                            var newItem = item.Skip((_id + 1)).ToList();

                            if (newItem.Count == 0)
                                break;

                            // TRATAMENTO FODA PARA FGTS, MORA, PAGAMENTO
                            if (newItem.Count == 1)
                                _obj.Pago = Regex.Replace(newItem[0].Trim(), @"[^0-9$]+", "");

                            else if (newItem.Count == 2)
                            {
                                if (lstDampFgts.Count > 0)
                                {
                                    if (lstDampFgts.Any(p => p.Contrato.Equals(_contrato) && p.DataVencimento.Equals(_obj.Vencimento.Trim())))
                                    {
                                        _obj.Fgts = Regex.Replace(newItem[0].Trim(), @"[^0-9$]+", "");
                                        _obj.Pago = Regex.Replace(newItem[1].Trim(), @"[^0-9$]+", "");
                                    }
                                    else
                                    {
                                        _obj.Pago = Regex.Replace(newItem[0].Trim(), @"[^0-9$]+", "");
                                        _obj.Mora = Regex.Replace(newItem[1].Trim(), @"[^0-9$]+", "");
                                    }
                                }
                                else
                                {
                                    _obj.Pago = Regex.Replace(newItem[0].Trim(), @"[^0-9$]+", "");
                                    _obj.Mora = Regex.Replace(newItem[1].Trim(), @"[^0-9$]+", "");
                                }

                            }

                            else
                            {
                                _obj.Fgts = Regex.Replace(newItem[0].Trim(), @"[^0-9$]+", "");
                                _obj.Pago = Regex.Replace(newItem[1].Trim(), @"[^0-9$]+", "");
                                _obj.Mora = Regex.Replace(newItem[2].Trim(), @"[^0-9$]+", "");
                            }

                            break;
                        }
                    case 4: // PEGA A LINHA DE PROXIMO PAGAMENTO E MORA
                        {
                            _case = "Case 4 - Metodo: TrataLinhaParcelas - PEGA A LINHA DE PROXIMO PAGAMENTO E MORA";

                            if (_linha.Count(u => Regex.IsMatch(u, @"(^\d{2}\/\d{2}\/\d{4}$)")) == 2)
                            {
                                _obj.Proc_Emi_Pag = Regex.Replace(_linha[0] + _linha[1], @"[^0-9\/$]", "");

                                if (_linha.Length == 3)
                                {
                                    // não foi definido se iremos utilizar p campo "DataBase" ou "DataEmissao"
                                    // optamos por utilizar "DataEmissao", até o que o Luis esclareça essa dúvida com a Andrea
                                    //Data de Correção: 22/11/2019
                                    if (lstDampFgts.Any(p => p.Contrato.Equals(_contrato) && p.DataVencimento.Equals(_obj.Vencimento.Trim())))
                                        _obj.Fgts = _obj.Fgts == "0" ? Regex.Replace(_linha[2], @"[^0-9$]", "") : "0";
                                        else
                                            if (Convert.ToDateTime(cabecalho.DataEmicao) <= Convert.ToDateTime(_obj.Vencimento))
                                                _obj.Mora = _obj.Mora = "0";
                                            else
                                                if (Convert.ToInt32((Convert.ToDecimal(_linha[2]) / Convert.ToDecimal(_obj.Encargo)) * 100) <= 80)
                                                    _obj.Mora = _obj.Mora == "0" ? Regex.Replace(_linha[2], @"[^0-9$]", "") : "0";

                                }

                                if (_linha.Length > 3)
                                {
                                    _obj.Fgts = _obj.Fgts == "0" ? Regex.Replace(_linha[2], @"[^0-9$]", "") : "0";
                                    _obj.Mora = _obj.Mora == "0" ? Regex.Replace(_linha[3], @"[^0-9$]", "") : "0";
                                }
                            }

                            if (_linha.Count(u => Regex.IsMatch(u, @"(^\d{2}\/\d{2}\/\d{4}$)")) > 2)
                                _obj.Proc_Emi_Pag = Regex.Replace(_linha[1] + _linha[2], @"[^0-9\/$]", "");

                            break;
                        }
                    default:
                        break;
                }

            }
            catch (Exception exOut)
            {
                throw new ArgumentOutOfRangeException("Ocorrencia do PDF - Arquivo: BusinessParcelas " + _case, exOut.InnerException); 
            }
            return _obj;
        }

        public string[] TrataArray(string _linha)
        {
            _linha = Regex.Replace(_linha, @"[^A-Za-zÀ-ú0-9\/,.\-$]", " ");
            return _linha.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        public bool ValidaLinha(string _linha)
        {
            return _linha.Split('*').Any(f => f.Equals("( ) "));
        }

        public string[] TrataArrayPadrao3(string _linhaAtual, string _pagina, int _numberLine = 0)
        {
            _numberLine = _numberLine - 3;
            string[] arrayPagina = _pagina.Split('\n');

            List<string> x, y;
            x = y = new List<string>();
            
            for (int i = _numberLine; i < arrayPagina.Length; i++)
            {
                if (arrayPagina[i].Trim() == _linhaAtual)
                {
                    if (_linhaAtual.Contains("COR"))
                    {
                        if (arrayPagina[(i + 1)].Split(' ').Length < 5)
                        {
                            y = arrayPagina[(i + 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();

                            if (y.Any(e => Regex.IsMatch(e.Trim(), @"(^\d{1,2}:\d{1,2}:\d{1,2}$)")))
                                y = arrayPagina[(i - 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();
                        }
                        else
                        {
                            y = arrayPagina[(i - 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();

                            if (y.Any(e => Regex.IsMatch(e.Trim(), @"(^\d{1,2}:\d{1,2}:\d{1,2}$)")))
                                y = arrayPagina[(i + 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();
                        }
                    }
                    string _ordenValor = y.Find(r => Regex.IsMatch(r.Trim(), @"(^\d{1},\d{6}$)"));
                    x = _linhaAtual.Split(' ').ToList();
                    x.Insert(2, _ordenValor);
                    y.Remove(_ordenValor);
                    x.Insert(3, y[0]);

                    break;
                }
            }

            return x.ToArray();
        }

        public string[] TratArrayPadrao2(string _linhaAtual, string _pagina, int _numberLine = 0)
        {
            _numberLine = _numberLine - 2;
            string _codigoOcorrencia = Regex.Replace(_linhaAtual.Trim(), @"[^A-Za-zà-ú*0-9$.,]", " ").Trim(); 
            string[] arrayPagina = _pagina.Split('\n');
            List<string> x = null;
            string texto = _linhaAtual.Split('*').Single((f => f.Equals("( ) ")));
            //int contador = 0;

            for (int i =_numberLine; i < arrayPagina.Length; i++)
            {
                if (arrayPagina[i].Equals(_linhaAtual))
                {
                    x = arrayPagina[(i - 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();

                    if (x.Any(l => l.Trim().Contains("PRO") || l.Trim().Contains("***") || l.Trim().Contains("00/00/0000")))
                    {
                        x = arrayPagina[(i + 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();
                    }
                    else
                    {
                        if (!Regex.IsMatch(x[0].ToString(), @"(\d{2}\/\d{2}\/\d{4})"))
                            x = arrayPagina[(i + 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();
                    }

                    x.Insert(2, _codigoOcorrencia);

                    for (int t = 0; t < x.Count; t++)
                    {
                        if (x[t].Contains("(") || x[t].Contains(")"))
                        {
                            x.RemoveAt(t);
                            t--;
                        }
                    }
                }

                if (x != null)
                    break;
            }

            return x.ToArray();
        }

        public Ocorrencia TrataOcorrencia(string[] _linhaOcorrencia, string _diretorioDestino, string _contrato = null)
        {
            Ocorrencia _obj = new Ocorrencia();
            _obj.Contrato = _contrato;

            string _case = string.Empty ,_codigoOcorrencia = string.Empty;
            try
            {
                if (_linhaOcorrencia.Length < 2)
                    return _obj;

                _codigoOcorrencia = _linhaOcorrencia.Any(u => u.Trim().Equals("DAMP")) ? _linhaOcorrencia[0] : Regex.Replace(_linhaOcorrencia[2], @"[^0-9$]", "").Substring(0, 3);
                bool hasMora = _linhaOcorrencia.Any(t => t.Contains("Tot"));

                    if (_linhaOcorrencia.Any(d => d.Equals("DAMP")))
                        _codigoOcorrencia = "DAMP";

                if (_codigoOcorrencia != "DAMP")
                {
                    var x = _linhaOcorrencia.ToList();

                    x[1] = Regex.IsMatch(_linhaOcorrencia[1], @"(^\d{2}\/\d{2}\/\d{4}$)") ? _linhaOcorrencia[1] : "00/00/0000";

                    // remove da lista todos os indices que não são numeros
                    for (int i = 0; i < x.Count; i++)
                    {
                        if (char.IsLetter(x[i], 0))
                        {
                            x.RemoveAt(i);
                            i--;
                        }
                    }

                    _linhaOcorrencia = x.ToArray();

                }
 
                switch (_codigoOcorrencia)
                {
                    
                    case "DAMP": // DAMP3
                        {
                            _case = "DAMP0 - Metodo: TrataOcorrencia -  Situação: DAMP";
                            _obj.Damp = Regex.Replace(_linhaOcorrencia[1].Trim(), @"[^0-9$]", "");
                            break;
                        }
                    case "004": // Mudança de Vencimento
                        {
                           
                            _case = "004 - Metodo: TrataOcorrencia -  Situação: Mudança dia vencimento";
                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; 
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***004Mudança dia vencimento";
                            break;
                        }
                    case "005": //Alteração de Garantia
                        {
                            _case = "005 - Metodo: TrataOcorrencia -  Situação: Alteração de Garantia";


                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***005Alteração de Garantia";

                            break;
                        }
                    case "010": // Alteração Contratual
                        {
                            _case = "010 - Metodo: TrataOcorrencia -  Situação: Alteração Contratual";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***010Alteração Contratual";
                            break;
                        }
                    case "012": // Crescimento tx.Juros
                        {
                            _case = "012 - Metodo: TrataOcorrencia -  Situação: Crescimento tx.Juros";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***012Crescimento tx.Juros";
                            break;
                        }
                    case "020": // Amortização Extra
                        {
                            _case = "020 - Metodo: TrataOcorrencia -  Situação: Amortização Extra";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (_linhaOcorrencia.Length > 5)
                            {
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            }

                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[(_linhaOcorrencia.Length - 2)].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[_linhaOcorrencia.Length - 1].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***020Amortização extra";
                            break;
                        }
                    case "021": // Amortização Reg FGTS
                        {
                            _case = "021 - Metodo: TrataOcorrencia -  Situação: Amortização Reg FGTS";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***021Amortização rec. Fgts";
                            break;
                        }
                    case "022": // Sinistro Parcial
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = Regex.IsMatch(_linhaOcorrencia[count].Trim(), @"[0-9]{2}/[0-9]{2}/[0-9]{4}") ? _linhaOcorrencia[count++].Trim() : "01/01/0001".PadLeft(10, ' ');
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[(_linhaOcorrencia.Length - 2)].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[_linhaOcorrencia.Length - 1].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***022Sinistro parcial";
                            break;
                        }
                    case "028": // Amortização s/recalculo 
                        {

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = Regex.IsMatch(_linhaOcorrencia[count].Trim(), @"[0-9]{2}/[0-9]{2}/[0-9]{4}") ? _linhaOcorrencia[count++].Trim() : "01/01/0001".PadLeft(10, ' ');
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[(_linhaOcorrencia.Length - 2)].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[_linhaOcorrencia.Length - 1].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***028Amortização s/recalculo";
                            break;
                        }
                    case "030": // Incorporação no Saldo
                        {
                            _case = "030 - Metodo: TrataOcorrencia -  Situação: Incorporação no Saldo";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = Regex.IsMatch(_linhaOcorrencia[1].Trim(), @"[0-9]{2}/[0-9]{2}/[0-9]{4}") ? _linhaOcorrencia[1].Trim() : "01/01/0001".PadLeft(10, ' ');
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***030Incorporação no Saldo";
                            break;
                        }
                    case "031": // Consolidação da dívida
                        {
                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***031Consolidação da divida";
                            break;
                        }
                    case "032": // Incorporação de Juro
                        {
                            _case = "032 - Metodo: TrataOcorrencia -  Situação: Incorporação de Juro";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***032Incorporação juros";
                            break;
                        }
                    case "040": // Transferencia
                        {
                            _case = "040 - Metodo: TrataOcorrencia -  Situação: Transferencia";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = Regex.IsMatch(_linhaOcorrencia[1].Trim(), @"[0-9]{2}/[0-9]{2}/[0-9]{4}") ? _linhaOcorrencia[1].Trim() : "01/01/0001".PadLeft(10, ' ');
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***040Transferência";
                            break;
                        }
                    case "044": // Transferencia Parte Ideal
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***044Transf.Parte ideal";
                            break;
                        }
                    case "046": // Sinistro Parcial c/mudanca devedor
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***046Sinistro Parcial Mud. Devedor";
                            break;
                        }
                    case "050": // 050-Liquidação Antecipada
                        {
                            _case = "050 - Metodo: TrataOcorrencia -  Situação: -Liquidação Antecipada";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***050Liquidação Antecipada";
                            break;
                        }
                    case "051": // Liquidação rec Fgts
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***051Liquidação Fgts";
                            break;
                        }
                    case "052": // Sinistro Total
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***052Sinistro Total";
                            break;
                        }
                    case "054": // 054-Liquidação Coobrigado
                        {
                            _case = "054 - Metodo: TrataOcorrencia -  Situação: -Liquidação Coobrigado";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***054Liquidação Coobrigado";
                            break;
                        }
                    case "058": // 058-Liquidação Interveniência
                        {
                            _case = "058 - Metodo: TrataOcorrencia -  Situação: Liquidação Interveniência";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***058Liquidação Interveniência";
                            break;
                        }
                    case "059": // 059-Liquidação por portabilidade
                        {
                            _case = "059 - Metodo: TrataOcorrencia -  Situação: Liquidação por portabilidade";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia; count++;
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***059Liquidação Portabilidade";
                            break;
                        }
                    case "060": // Incorporação de Juro
                        {
                            _case = "060 - Metodo: TrataOcorrencia -  Situação: Contrato Encerrado";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _codigoOcorrencia;
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$\/-]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***060TERMINO PRAZO";
                            break;
                        }
                    default:
                        using (StreamWriter escreverNovaOcorrencia = new StreamWriter(_diretorioDestino + @"\NOVAS_OCORRENCIAS.txt", true, Encoding.Default))
                        {
                            string _novaOocorrencia = "NOVO CODIGO DE CONTRADO ENCONTRADO: " + _codigoOcorrencia +" - CONTRATO: " + _obj.Contrato;
                            escreverNovaOcorrencia.WriteLine(_novaOocorrencia);
                        }
                        break;
                }

            }
            catch (Exception exOut)
            {
                throw new ArgumentOutOfRangeException("Ocorrencia do PDF - Arquivo: BusinessParcelas - case: " + _case, exOut.Message);
            }

            return _obj;
        }

        public Parcela PreencheParcela(Parcela obj)
        {
            return new Parcela()
            {
                Agencia = string.IsNullOrWhiteSpace(obj.Agencia) ? "0" : obj.Agencia,
                Amortizacao = string.IsNullOrWhiteSpace(obj.Amortizacao) ? "0" : obj.Amortizacao,
                AmortizacaoCorrecao = string.IsNullOrWhiteSpace(obj.AmortizacaoCorrecao) ? "0" : obj.AmortizacaoCorrecao,
                Banco = string.IsNullOrWhiteSpace(obj.Banco) ? "" : obj.Banco,
                Encargo = string.IsNullOrWhiteSpace(obj.Encargo) ? "0" : obj.Encargo,
                Indice = string.IsNullOrWhiteSpace(obj.Indice) ? "0" : obj.Indice,
                IndiceCorrecao = string.IsNullOrWhiteSpace(obj.IndiceCorrecao) ? "0" : obj.IndiceCorrecao,
                Juros = string.IsNullOrWhiteSpace(obj.Juros) ? "0" : obj.Juros,
                Mora = string.IsNullOrWhiteSpace(obj.Mora) ? "0" : obj.Mora,
                NumeroPrazo = string.IsNullOrWhiteSpace(obj.NumeroPrazo) ? "0" : obj.NumeroPrazo,
                Pagamento = string.IsNullOrWhiteSpace(obj.Pagamento) ? "01/01/0001" : obj.Pagamento,
                Pago = string.IsNullOrWhiteSpace(obj.Pago) ? "0" : obj.Pago,
                Prestacao = string.IsNullOrWhiteSpace(obj.Prestacao) ? "0" : obj.Prestacao,
                Proc_Emi_Pag = string.IsNullOrWhiteSpace(obj.Proc_Emi_Pag) ? "0/00/0000" : obj.Proc_Emi_Pag,
                SaldoDevedor = string.IsNullOrWhiteSpace(obj.SaldoDevedor) ? "0" : obj.SaldoDevedor,
                SaldoDevedorCorrecao = string.IsNullOrWhiteSpace(obj.SaldoDevedorCorrecao) ? "0" : obj.SaldoDevedorCorrecao,
                Seguro = string.IsNullOrWhiteSpace(obj.Seguro) ? "0" : obj.Seguro,
                Id = obj.Id,
                IdCabecalho = obj.IdCabecalho,
                Taxa = string.IsNullOrWhiteSpace(obj.Taxa) ? "0" : obj.Taxa,
                Fgts = string.IsNullOrWhiteSpace(obj.Fgts) ? "0" : obj.Fgts,
                TPG_EVE_HIS = string.IsNullOrWhiteSpace(obj.TPG_EVE_HIS) ? "0" : obj.TPG_EVE_HIS,
                Vencimento = string.IsNullOrWhiteSpace(obj.Vencimento) ? "" : obj.Vencimento,
                VencimentoCorrecao = string.IsNullOrWhiteSpace(obj.VencimentoCorrecao) ? "0" : obj.VencimentoCorrecao,
                DataVencimentoAnterior = string.IsNullOrWhiteSpace(obj.DataVencimentoAnterior) ? "01/01/0001" : obj.DataVencimentoAnterior,
                Iof = string.IsNullOrWhiteSpace(obj.Iof) ? "0" : obj.Iof,
                Contrato = string.IsNullOrWhiteSpace(obj.Contrato) ? "0" : obj.Contrato,
                
            };
        }

        public string[] TrataParcela2(string _linhaAtual, string _pagina, int _numberLine = 0)
        {
            _numberLine = _numberLine - 3;

            string[] arrayPagina = _pagina.Split('\n');

            List<string> x, y;
            x = y = new List<string>();

            for (int i = _numberLine; i < arrayPagina.Length; i++)
            {
                if (arrayPagina[i].Trim() == _linhaAtual)
                {
                    //if ((arrayPagina[(i + 1)].Trim().Split(' ').Length < 5) && (!arrayPagina[(i + 1)].Trim().Contains("ANT") || !arrayPagina[(i + 1)].Trim().Contains("COR")))
                    if (arrayPagina[(i + 1)].Split(' ').Any(t => Regex.IsMatch(t, @"(^\d{6}.\d{1}$)")) || arrayPagina[(i + 1)].Split(' ').Any(t => Regex.IsMatch(t, @"(^\d{1},\d{6}$)")))
                        y = arrayPagina[(i + 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();
                   else
                    {
                        y = arrayPagina[(i - 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();
                    }

                    x = _linhaAtual.Split(' ').ToList();

                    if(y.Any(t => t.Equals("COR")))
                        return x.ToArray();

                    if (y.Any(h => h.Contains("*")))
                        return x.ToArray();

                    if(y.Any(h => Regex.IsMatch(h, @"(^\d{1,2}:\d{1,2}:\d{1,2}$)")))
                        return x.ToArray();

                    if (string.Join(" ", y).Contains("00/00/0000"))
                        return x.ToArray();

                    if(y.Any(r => Regex.IsMatch(r.Trim(), @"(^\d{6}.\d{1}$)")))
                        return x.ToArray();

                    if(y.Any(r => Regex.IsMatch(r.Trim(), @"(^\d{1,2}:\d{1,2}:\d{1,2}$)")))
                        return x.ToArray();

                    string _ordenValor = y.Find(r => Regex.IsMatch(r.Trim(), @"(^\d{1},\d{6}$)"));

                    if (!string.IsNullOrWhiteSpace(_ordenValor))
                    {
                        int sequencia = x.FindIndex(u => Regex.IsMatch(u, @"(^\d{3}\/\d{3}$)"));

                        x.Insert((sequencia + 1), _ordenValor);
                        y.Remove(_ordenValor);
                        _ordenValor = y.Find(r => Regex.IsMatch(r.Trim(), @"(^\d{2}\/\d{2}\/\d{4}$)"));

                        if (string.IsNullOrWhiteSpace(_ordenValor))
                        {
                            x.Insert(1, "INCORP");
                            y.RemoveAll(r => r.Contains("INCORP"));
                        }
                        else
                        {
                            x.Insert(1, _ordenValor);
                            y.Remove(_ordenValor);
                        }
                        x.Insert(5, y[0]);
                    }

                    break;
                }
            }

            x.Remove("COR");
            x.RemoveAll(rem => string.IsNullOrWhiteSpace(rem));

            return x.ToArray();
        }

        public DataTable CriaTabelaParcelas()
        {
            var table = new DataTable();
            table.Columns.Add("Carteira", typeof(string));
            table.Columns.Add("Contrato", typeof(string));
            table.Columns.Add("Agencia", typeof(string));
            table.Columns.Add("Vencimento", typeof(string));
            table.Columns.Add("DataBaseContrato", typeof(string));
            table.Columns.Add("Indice", typeof(string));
            table.Columns.Add("IndiceCorrecao", typeof(string));
            table.Columns.Add("Pagamento", typeof(string));
            table.Columns.Add("NumeroPrazo", typeof(string));
            table.Columns.Add("Prestacao", typeof(string));
            table.Columns.Add("Seguro", typeof(string));
            table.Columns.Add("TPG_EVE_HIS", typeof(string));
            table.Columns.Add("Taxa", typeof(string));
            table.Columns.Add("Fgts", typeof(string));
            table.Columns.Add("AmortizacaoCorrecao", typeof(string));
            table.Columns.Add("Banco", typeof(string));
            table.Columns.Add("SaldoDevedorCorrecao", typeof(string));
            table.Columns.Add("Encargo", typeof(string));
            table.Columns.Add("Pago", typeof(string));
            table.Columns.Add("Juros", typeof(string));
            table.Columns.Add("Mora", typeof(string));
            table.Columns.Add("Amortizacao", typeof(string));
            table.Columns.Add("Indicador", typeof(char));
            table.Columns.Add("SaldoDevedor", typeof(string));
            table.Columns.Add("Proc_Emi_Pag", typeof(string));
            table.Columns.Add("Iof", typeof(string));

            return table;
        }

        public DataTable CriaTabelaOcorrencia()
        {
            var table = new DataTable();
            table.Columns.Add("Contrato", typeof(string));
            table.Columns.Add("DataVencimento", typeof(string));
            table.Columns.Add("DataPagamento", typeof(string));
            table.Columns.Add("Simbulo", typeof(string));
            table.Columns.Add("CodigoOcorrencia", typeof(string));
            table.Columns.Add("Descricao", typeof(string));
            table.Columns.Add("Enc_Pago", typeof(string));
            table.Columns.Add("Juros", typeof(string));
            table.Columns.Add("Mora", typeof(string));
            table.Columns.Add("ValorAmortizado", typeof(string));
            table.Columns.Add("Sinal", typeof(char));
            table.Columns.Add("SaldoDevedor", typeof(string));
            table.Columns.Add("Alterado", typeof(string));
            table.Columns.Add("Sit_Anterior", typeof(string));
            table.Columns.Add("Sit_Atual", typeof(string));
            table.Columns.Add("Sit_Aux", typeof(string));

            return table;
        }

        public void AddBulkItens(object _dataTable)
        {
            DataTable dataTable = (DataTable)_dataTable.GetType().GetProperty("item1").GetValue(_dataTable, null);
            string _nameTable = (string)_dataTable.GetType().GetProperty("item2").GetValue(_dataTable, null);

            SqlCeBulkCopyOptions options = new SqlCeBulkCopyOptions();

            if (true)
            {
                options = options |= SqlCeBulkCopyOptions.KeepNulls;
            }

            try
            {
                using (DbConnEntity connEntity = new DbConnEntity())
                {
                    using (SqlCeBulkCopy bc = new SqlCeBulkCopy(connEntity.Database.Connection.ConnectionString.ToString(), options))
                    {
                        bc.DestinationTableName = _nameTable;
                        bc.WriteToServer(dataTable);
                    }
                }

                dataTable = null;
            }
            catch (Exception exdb)
            {
                throw new Exception("Erro ao tentar converter objeto na função AddHistoricoParcelas: " + exdb.Message);
            }
        }

        public void DoBulkCopy(bool keepNulls, List<Parcela> _parcela)
        {
            DataTable dataTable = CriaTabelaParcelas();
            DataRow dataRow = null;

            foreach (var item in _parcela)
            {
                dataRow = dataTable.NewRow();

                dataRow["Carteira"] = item.Carteira.Trim();
                dataRow["Contrato"] = item.Contrato.Trim();
                dataRow["Agencia"] = item.Agencia.Trim();
                dataRow["Vencimento"] = item.Vencimento.Trim();
                dataRow["DataBaseContrato"] = item.DataBaseContrato.Trim();
                dataRow["Indice"] = item.Indice.Trim();
                dataRow["IndiceCorrecao"] = item.IndiceCorrecao.Trim();
                dataRow["Pagamento"] = item.Pagamento.Trim();
                dataRow["NumeroPrazo"] = item.NumeroPrazo.Trim();
                dataRow["Prestacao"] = item.Prestacao.Trim();
                dataRow["Seguro"] = item.Seguro.Trim();
                dataRow["TPG_EVE_HIS"] = item.TPG_EVE_HIS.Trim();
                dataRow["Taxa"] = item.Taxa.Trim();
                dataRow["Fgts"] = item.Fgts.Trim();
                dataRow["AmortizacaoCorrecao"] = item.AmortizacaoCorrecao.Trim();
                dataRow["Banco"] = item.Banco.Trim();
                dataRow["SaldoDevedorCorrecao"] = item.SaldoDevedorCorrecao.Trim();
                dataRow["Encargo"] = item.Encargo.Trim();
                dataRow["Pago"] = item.Pago.Trim();
                dataRow["Juros"] = item.Juros.Trim();
                dataRow["Mora"] = item.Mora.Trim();
                dataRow["Amortizacao"] = item.Amortizacao.Trim();
                dataRow["Indicador"] = item.Indicador.Trim();
                dataRow["SaldoDevedor"] = item.SaldoDevedor.Trim();
                dataRow["Proc_Emi_Pag"] = item.Proc_Emi_Pag.Trim();
                dataRow["Iof"] = item.Iof.Trim();

                dataTable.Rows.Add(dataRow);
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
                        connEntity.Parcelas.Create();

                    using (SqlCeBulkCopy bc = new SqlCeBulkCopy(connEntity.Database.Connection.ConnectionString.ToString(), options))
                    {
                        bc.DestinationTableName = "Parcelas";
                        bc.WriteToServer(dataTable);
                    }
                }
            }
            catch (Exception exEntity)
            {
                string erro = exEntity.Message;
                throw;
            }
        }

        public List<Parcela> GetParcelas(string _contrato)
        {
            try
            {
                using (DbConnEntity dbConnEntity = new DbConnEntity())
                {
                    return dbConnEntity.Parcelas.Where(p => p.Contrato.Equals(_contrato.Trim())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<ItensDamp> GetParcelaFgts()
        {
            List<ItensDamp> listaParcelasFgts = null;
            try
            {
                using (DbConnEntity dbConn = new DbConnEntity())
                {
                    listaParcelasFgts = dbConn.DampFgts.Select(p => new ItensDamp(){ Contrato = p.Contrato, DataVencimento = p.DataVencimento }).ToList();
                }
            }
            catch (Exception exefgts)
            {
                throw exefgts;
            }

            return listaParcelasFgts;
        }

        public List<DampFgts> GetDampFgts(int numeroPagina, string pathDoc)
        {
            LocationTextExtractionStrategy its = null;

            List<DampFgts> lstFgts = new List<DampFgts>();  
            try
            {
                using (PdfReader reader = new PdfReader(pathDoc))
                {
                    its = new LocationTextExtractionStrategy();
                    string pagina = PdfTextExtractor.GetTextFromPage(reader, numeroPagina, its).Trim();
                    pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));
                    string _contrato = string.Empty;
                    using (StringReader strReader = new StringReader(pagina))
                    {
                        string line;
                        string[] arrayLinha;

                        while ((line = strReader.ReadLine()) != null)
                        {
                            arrayLinha = line.Split(' ');
                            if (arrayLinha.Any(f => Regex.IsMatch(f, @"(^\d{2}\/\d{4}$)")))
                            {
                                _contrato = new FileInfo(pathDoc).Name.Split('_')[0];
                                lstFgts.Add(new DampFgts() { Contrato = _contrato, ValidadeInicial = arrayLinha.FirstOrDefault(v => Regex.IsMatch(v, @"(^\d{2}\/\d{4}$)")), ValidadeFinal = arrayLinha.LastOrDefault(v => Regex.IsMatch(v, @"(^\d{2}\/\d{4}$)")) });
                            }
                        }
                    }
                }
            }
            catch (Exception exDamp)
            {
                throw exDamp;
            }

            return lstFgts;
        }
    }
}
