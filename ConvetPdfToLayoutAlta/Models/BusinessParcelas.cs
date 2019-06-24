using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessParcelas
    {
        public Parcela TrataLinhaParcelas(Parcela _obj, string[] _linha, int sequencia, bool _hasTaxa)
        {
            string  _case =  "VAZIO";
            try
            {

                switch (sequencia)
                {
                    case 1: // PEGA A LINHA DE CORREÇÃO
                        {
                            _case = "Case 1 - Metodo: TrataLinhaParcelas -  PEGA A LINHA DE CORREÇÃO";

                            _obj.VencimentoCorrecao = _linha[0].Trim();
                            _obj.IndiceCorrecao = Regex.Replace(_linha[2], @"[^0-9$]", "");
                            _obj.AmortizacaoCorrecao = Regex.Replace(_linha[3], @"[^0-9$]", "");
                            _obj.SaldoDevedorCorrecao = Regex.Replace(_linha[4], @"[^0-9$]", "");

                            break;
                        }
                    case 2: // PEGA A LINHA DE PAGAMENTO
                        {
                            _case = "Case 2 - Metodo: TrataLinhaParcelas - PEGA A LINHA DE PAGAMENTO";
                            int count = 0;

                            _obj.Vencimento = Regex.Replace(_linha[count++], @"[^0-9\/$]", "");

                            if (_linha.Length >= 9)
                                _obj.Pagamento = Regex.IsMatch(_linha[1].ToString(), @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$") ? Regex.Replace(_linha[count++], @"[^0-9\/$]", "") : "0";

                            _obj.NumeroPrazo = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.Indice = _linha[count].StartsWith("1,00") ? Regex.Replace(_linha[count++], @"[^0-9$]", "") : "0"; 
                            _obj.Prestacao = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.Seguro = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.Taxa = _hasTaxa ? Regex.Replace(_linha[count++], @"[^0-9$]", "") : "0";
                            _obj.Encargo = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.Juros = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.Amortizacao = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linha[count++], @"[^0-9$]", "");

                            break;
                        }
                    case 3: // PEGA A LINHA DE BANCO E AGENCIA
                        {
                            _case = "Case 3 - Metodo: TrataLinhaParcelas - PEGA A LINHA DE BANCO E AGENCIA";

                            if (Regex.Replace(_linha[0], @"[^0-9\/$]", "") != "033" && _linha.Length == 7)
                            {
                                var x = _linha.ToList();
                                x.RemoveAt(3);
                                _linha = x.ToArray();
                            }

                           
                            _obj.Banco = _linha.Length > 0 ? Regex.Replace(_linha[0], @"[^0-9\/$]", "") : "0";
                            _obj.Agencia = _linha.Length > 1 ? Regex.Replace(_linha[1], @"[^0-9\/$]", "") : "0";
                            _obj.TPG_EVE_HIS = _linha.Length > 2 ? _linha[2].Trim() : "0";
                            _obj.Proc_Emi_Pag =  _linha.Length > 4 ? Regex.Replace(_linha[3], @"[^0-9\/$]", "") + Regex.Replace(_linha[4], @"[^0-9\/$]", "") : "99/99/9999";
                            _obj.Pago = _linha.Length >=5 ? Regex.Replace(_linha[5], @"[^0-9$]", "") : "0";
                            _obj.Mora = _linha.Length > 6 ? Regex.Replace(_linha[6], @"[^0-9$]", "") : "0";

                            break;
                        }
                    case 4: // PEGA A LINHA DE PROXIMO PAGAMENTO E MORA
                        {
                            _case = "Case 4 - Metodo: TrataLinhaParcelas - PEGA A LINHA DE PROXIMO PAGAMENTO E MORA";

                            _obj.Proc_Emi_Pag = Regex.Replace(_linha[0], @"[^0-9\/$]", "") + Regex.Replace(_linha[1], @"[^0-9\/$]", "");
                            if (_linha.Length > 2)
                            {
                                _obj.Mora = Regex.Replace(_linha[2], @"[^0-9$]", "");
                            }
                            break;
                        }
                    case 5: // DAMP
                        {
                            _case = "DAMP0 - Metodo: TrataOcorrencia -  Situação: DAMP0";
                            _obj.Dump = Regex.Replace(_linha[1].Trim(), @"[^0-9$]", "");
                            break;
                        }

                    default:
                        break;
                }
          
            }
            catch (Exception exOut)
            {
                throw new ArgumentOutOfRangeException("Ocorrencia do PDF - Arquivo: BusinessParcelas " + _case, exOut.InnerException); ;
            }
            return _obj;
        }

        public string[] TrataArray(string _linha)
        {
            _linha = Regex.Replace(_linha, @"[^A-Za-zÀ-ú0-9\/,.$]", " ");
            return _linha.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        public bool ValidaLinha (string _linha)
        {
            return _linha.Split('*').Any(f => f.Equals("( ) "));
        }

        public string[] TratArrayPadrao2(string _linhaAtual, string _pagina, int _countExption )
        {
            string _codigoOcorrencia = string.Empty;
            string[] arrayPagina = _pagina.Split('\n');
            List<string> x = null;
            string texto = _linhaAtual.Split('*').Single((f => f.Equals("( ) ")));
            string texto1 = Regex.Replace(_linhaAtual.Trim(), @"[^A-Za-zà-ú0-9$.,]", " ").Trim();
            int contador = 0;

            for (int i = 0; i < arrayPagina.Length; i++)
            {
                if(arrayPagina[i].StartsWith(texto))
                {
                    if (_countExption == contador)
                    {
                        _codigoOcorrencia = Regex.Replace(arrayPagina[i], @"[^0-9$]", "");
                         x = arrayPagina[(i - 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();

                        if (!Regex.IsMatch(x[0].ToString(), @"(\d{2}\/\d{2}\/\d{4})"))
                            x = arrayPagina[(i + 1)].Split(' ').Where(j => !string.IsNullOrWhiteSpace(j)).ToList();

                        x.Insert(2, _codigoOcorrencia);

                        for (int t = 0; t < x.Count; t++)
                        {
                            if(x[t].Contains("(") || x[t].Contains(")"))
                            {
                                x.RemoveAt(t);
                                t--;
                            }
                        }

                        
                        break;
                    }
                    contador++;
                }
            }

            return x.ToArray();
        }

        public Ocorrencia TrataOcorrencia(string[] _linhaOcorrencia )
        {

            Ocorrencia _obj = new Ocorrencia();
            string _case = string.Empty, _codigoOcorrencia = _linhaOcorrencia[2].Trim();
            try
            {
                bool hasMora = _linhaOcorrencia.Any(h => h.StartsWith("Tot."));
              //  bool hasLiquidacaoFGTS = _linhaOcorrencia.Any(h => h.StartsWith("Liquidação"));

                if (_linhaOcorrencia.Any(d => d.Equals("DAMP")))
                    _codigoOcorrencia = "DAMP";

                var x = _linhaOcorrencia.ToList();

                for (int i = 0; i < x.Count; i++)
                {
                    if(char.IsLetter(x[i],0))
                    {
                        x.RemoveAt(i);
                        i--;
                    }
                }

                _linhaOcorrencia = x.ToArray();
               
               
                switch (_codigoOcorrencia)
                {
                    case "004": // Mudança de Vencimento
                        {
                            _case = "004 - Metodo: TrataOcorrencia -  Situação: Mudança dia vencimento";
                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***004Mudança dia vencimento";
                            break;
                        }
                    case "005": //Alteração de Garantia
                        {
                            _case = "005 - Metodo: TrataOcorrencia -  Situação: Alteração de Garantia";


                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***010Alteração de Garantia";

                            break;
                        }
                    case "010": // Alteração Contratual
                        {
                            _case = "010 - Metodo: TrataOcorrencia -  Situação: Alteração Contratual";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***010Alteração Contratual";
                            break;
                        }
                    case "020": // Amortização Extra
                        {
                            _case = "020 - Metodo: TrataOcorrencia -  Situação: Amortização Extra";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[count++].Trim();
                            if (hasMora)
                            {
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[(count += 2)].Trim(), @"[^0-9$]", "");
                                count++;
                                // _obj.Mora = _linhaOcorrencia[(_linhaOcorrencia.Length == 7 ? (count+=2): count++)].Trim();
                            }
                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[(_linhaOcorrencia.Length - 2)].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[_linhaOcorrencia.Length - 1].Trim(),@"[^0-9$]", "");
                            _obj.Descricao = "***020Amortização extra";
                            break;
                        }
                    case "021": // Amortização Reg FGTS
                        {
                            _case = "021 - Metodo: TrataOcorrencia -  Situação: Amortização Reg FGTS";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[count++].Trim();
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***021Amortização rec. Fgts";
                            break;
                        }
                    case "022": // Sinistro Parcial
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = Regex.IsMatch(_linhaOcorrencia[count].Trim(), @"[0-9]{2}/[0-9]{2}/[0-9]{4}") ? _linhaOcorrencia[count++].Trim() : "00010101".PadLeft(10, ' ');
                            _obj.CodigoOcorrencia = _linhaOcorrencia[count++].Trim();
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[(_linhaOcorrencia.Length - 2)].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[_linhaOcorrencia.Length - 1].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***022Sinistro parcial";
                            break;
                        }
                    case "030": // Incorporação no Saldo
                        {
                            _case = "030 - Metodo: TrataOcorrencia -  Situação: Incorporação no Saldo";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = Regex.IsMatch(_linhaOcorrencia[1].Trim(), @"[0-9]{2}/[0-9]{2}/[0-9]{4}") ? _linhaOcorrencia[1].Trim() : "00010101".PadLeft(10,' ');
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***030Incorporação no Saldo";
                            break;
                        }
                    case "031": // Consolidação da dívida
                        {
                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***031Consolidação da divida";
                            break;
                        }
                    case "032": // Incorporação de Juro
                        {
                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***032Incorporação juros";
                            break;
                        }
                    case "040": // Transferencia
                        {
                            throw new ArgumentOutOfRangeException("Case: 040 - Metodo: TrataOcorrencia  - Descrição: Nova Ocorrencia não tratada -  tipo de Ocorrencia: Transferencia");
                        }
                    case "051": // Liquidação rec Fgts
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[count++].Trim();
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***051Liquidação Fgts";
                            break;
                        }
                    case "044": // Transferencia Parte Ideal
                        {
                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[count++].Trim();
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***044Transf.Parte ideal";
                            break;
                        }
                    case "050": // 050-Liquidação Antecipada
                        {
                            _case = "050 - Metodo: TrataOcorrencia -  Situação: -Liquidação Antecipada";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[count++].Trim();
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***050Liquidação Antecipada";
                            break;
                        }
                    case "058": // 058-Liquidação Interveniência
                        {
                            _case = "058 - Metodo: TrataOcorrencia -  Situação: Liquidação Interveniência";

                            int count = 0;
                            _obj.Vencimento = _linhaOcorrencia[count++].Trim();
                            _obj.Pagamento = _linhaOcorrencia[count++].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[count++].Trim();
                            if (hasMora)
                                _obj.Juros = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            else
                                _obj.Juros = "0";

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***058Liquidação Interveniência";
                            break;
                        }
                    default :
                        break;
                }
            }
            catch (Exception exOut)
            {
                throw new ArgumentOutOfRangeException("Ocorrencia do PDF - Arquivo: BusinessParcelas - case: " + _case, exOut.Message); ;
            }

            return _obj;
        }


        public Parcela PreencheParcela(Parcela obj)
        {
            return new Parcela() {
                Agencia = string.IsNullOrWhiteSpace(obj.Agencia) ? "0" : obj.Agencia,
                Amortizacao = string.IsNullOrWhiteSpace(obj.Amortizacao) ? "0" : obj.Amortizacao,
                AmortizacaoCorrecao = string.IsNullOrWhiteSpace(obj.AmortizacaoCorrecao) ? "0" : obj.AmortizacaoCorrecao,
                Banco = string.IsNullOrWhiteSpace(obj.Banco) ? "033" : obj.Banco,
                Encargo = string.IsNullOrWhiteSpace(obj.Encargo) ? "0" : obj.Encargo,
                Indice = string.IsNullOrWhiteSpace(obj.Indice) ? "0" : obj.Indice,
                IndiceCorrecao = string.IsNullOrWhiteSpace(obj.IndiceCorrecao) ? "0" : obj.IndiceCorrecao,
                Juros = string.IsNullOrWhiteSpace(obj.Juros) ? "0" : obj.Juros,
                Mora = string.IsNullOrWhiteSpace(obj.Mora) ? "0" : obj.Mora,
                NumeroPrazo = string.IsNullOrWhiteSpace(obj.NumeroPrazo) ? "0" : obj.NumeroPrazo,
                Pagamento = string.IsNullOrWhiteSpace(obj.Pagamento) ? "00010101" : obj.Pagamento,
                Pago = string.IsNullOrWhiteSpace(obj.Pago) ? "0" : obj.Pago,
                Prestacao = string.IsNullOrWhiteSpace(obj.Prestacao) ? "0" : obj.Prestacao,
                Proc_Emi_Pag = string.IsNullOrWhiteSpace(obj.Proc_Emi_Pag) ? "0" : obj.Proc_Emi_Pag,
                SaldoDevedor = string.IsNullOrWhiteSpace(obj.SaldoDevedor) ? "0" : obj.SaldoDevedor,
                SaldoDevedorCorrecao = string.IsNullOrWhiteSpace(obj.SaldoDevedorCorrecao) ? "0" : obj.SaldoDevedorCorrecao,
                Seguro = string.IsNullOrWhiteSpace(obj.Seguro) ? "0" : obj.Seguro,
                Id = obj.Id,
                IdCabecalho = obj.IdCabecalho,
                Taxa = string.IsNullOrWhiteSpace(obj.Taxa) ? "0" : obj.Taxa,
                TPG_EVE_HIS = string.IsNullOrWhiteSpace(obj.TPG_EVE_HIS) ? "0" : obj.TPG_EVE_HIS,
                Vencimento = string.IsNullOrWhiteSpace(obj.Vencimento) ? "00010101" : obj.Vencimento,
                VencimentoCorrecao = string.IsNullOrWhiteSpace(obj.VencimentoCorrecao) ? "0" : obj.VencimentoCorrecao,
                Dump = string.IsNullOrWhiteSpace(obj.Dump) ? "0" : obj.Dump,
            };
        }




    }
}
