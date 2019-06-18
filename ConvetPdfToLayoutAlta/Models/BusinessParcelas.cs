using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessParcelas
    {
        public Parcela TrataLinhaParcelas(Parcela _obj, string[] _linha, int sequencia)
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
                            _obj.Pagamento = Regex.IsMatch(_linha[1].ToString(), @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$") ? Regex.Replace(_linha[count++], @"[^0-9\/$]", "") : "0"; ;
                            _obj.NumeroPrazo = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.Indice = _linha[count].StartsWith("1,00") ? Regex.Replace(_linha[count++], @"[^0-9$]", "") : "0";
                            _obj.Prestacao = Regex.Replace(_linha[count++], @"[^0-9$]", "");
                            _obj.Seguro = Regex.Replace(_linha[count++], @"[^0-9$]", "");
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

                           
                            _obj.Banco = _linha.Length > 0 ? _linha[0].Trim() : "0";
                            _obj.Agencia = _linha.Length > 1 ? _linha[1].Trim() : "0";
                            _obj.TPG_EVE_HIS = _linha.Length > 2 ? _linha[2].Trim() : "0";
                            _obj.Proc_Emi_Pag =  _linha.Length > 4 ? Regex.Replace(_linha[3], @"[^0-9\/$]", "") + Regex.Replace(_linha[4], @"[^0-9\/$]", "") : "01/01/1900";
                            _obj.Pago = _linha.Length >=5 ? Regex.Replace(_linha[5], @"[^0-9$]", "") : "0";
                            _obj.Mora = _linha.Length > 6 ? Regex.Replace(_linha[6], @"[^0-9$]", "") : "0";

                            //if (_linha.Length > 6)
                            //    _obj.Mora = Regex.Replace(_linha[6], @"[^0-9$]", "");

                            break;
                        }
                    case 4:
                        {
                            _case = "Case 4 - Metodo: TrataLinhaParcelas - PEGA A LINHA DE PROXIMO PAGAMENTO E MORA";

                            _obj.Proc_Emi_Pag = Regex.Replace(_linha[0], @"[^0-9\/$]", "") + Regex.Replace(_linha[1], @"[^0-9\/$]", "");
                            if (_linha.Length > 2)
                            {
                                _obj.Mora = Regex.Replace(_linha[2], @"[^0-9$]", "");
                            }
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


        public Ocorrencia TrataOcorrencia(string[] _linhaOcorrencia, string _codigoOcorrencia )
        {
            Ocorrencia _obj = new Ocorrencia();
            string _case = string.Empty;
            try
            {
                int index = 0;
                bool hasMora = _linhaOcorrencia.Any(h => h.StartsWith("Tot."));
                bool hasFGTS = _linhaOcorrencia.Any(h => h.StartsWith("rec."));

                if (hasFGTS)
                {
                }

                if (hasMora)
                {
                   index = _linhaOcorrencia.ToList().FindIndex(f => f.StartsWith("Tot."));
                    var x = _linhaOcorrencia.ToList();
                    x.RemoveAt(index);
                    _linhaOcorrencia = x.ToArray();
                }

               // string _tiraLetra = String.Join(" ", _linhaOcorrencia).Replace("Tot.", "").Replace("rec. Fgts", "");
               // _tiraLetra = Regex.Replace(_tiraLetra, @"[^0-9\/,.\-$]", " ");
               // _linhaOcorrencia = _tiraLetra.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x) && !x.Equals(".")).ToArray();

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
                            throw new ArgumentOutOfRangeException("Case: 005 - Metodo: TrataOcorrencia  - Descrição: Nova Ocorrencia não tratada -  tipo de Ocorrencia: Alteração de Garantia");                           
                        }
                    case "010": // Alteração Contratual
                        {
                            _case = "010 - Metodo: TrataOcorrencia -  Situação: Alteração Contratual";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[5].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[6].Trim(), @"[^0-9$]", "");
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
                            if (_linhaOcorrencia.Length < 6)
                            {
                                if (_linhaOcorrencia[count].Length <= 6)
                                {
                                    int m = (count + 1);
                                    if (Convert.ToInt32(_linhaOcorrencia[count]) < Convert.ToInt32(_linhaOcorrencia[m]))
                                    _obj.Juros = _linhaOcorrencia[count++].Trim();
                                }
                            }
                            else
                            {
                                _obj.Juros = _linhaOcorrencia[(count++)].Trim();
                                ;
                            }

                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[count++].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***021Amortização Rec Fgts";
                            break;
                        }
                    case "022": // Sinistro Parcial
                        {
                            throw new ArgumentOutOfRangeException("Case: 022 - Metodo: TrataOcorrencia  - Descrição: Nova Ocorrencia não tratada -  tipo de Ocorrencia: Sinistro Parcial");                           
                        }
                    case "030": // Incorporação no Saldo
                        {
                            _case = "030 - Metodo: TrataOcorrencia -  Situação: Incorporação no Saldo";

                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***030Incorporação no Saldo";
                            break;
                        }
                    case "031": // Consolidação da dívida
                        {
                            throw new InvalidOperationException("Case: 031 - Metodo: TrataOcorrencia  - Descrição: Nova Ocorrencia não tratada -  tipo de Ocorrencia: Consolidação da dívida");                           
                        }
                    case "032": // Incorporação de Juro
                        {
                            _obj.Vencimento = _linhaOcorrencia[0].Trim();
                            _obj.Pagamento = _linhaOcorrencia[1].Trim();
                            _obj.CodigoOcorrencia = _linhaOcorrencia[2].Trim();
                            _obj.Amortizacao = Regex.Replace(_linhaOcorrencia[3].Trim(), @"[^0-9$]", "");
                            _obj.SaldoDevedor = Regex.Replace(_linhaOcorrencia[4].Trim(), @"[^0-9$]", "");
                            _obj.Descricao = "***032Incorporação de Juro";
                            break;
                        }
                    case "040": // Transferencia
                        {
                            throw new ArgumentOutOfRangeException("Case: 040 - Metodo: TrataOcorrencia  - Descrição: Nova Ocorrencia não tratada -  tipo de Ocorrencia: Transferencia");
                        }
                    case "044": // Transferencia Parte Ideal
                        {
                            throw new ArgumentOutOfRangeException("Case: 044 - Metodo: TrataOcorrencia  - Descrição: Nova Ocorrencia não tratada -  tipo de Ocorrencia: Transferencia Parte Ideal");
                            
                        }
                    case "DAMP": // DAMP
                        {
                            throw new ArgumentOutOfRangeException("Nova Ocorrencia não tratada -  tipo de Ocorrencia: DAMP"); 
                        }
                    //case "DAMP0": // DAMP0
                    //    {
                    //        _case = "DAMP0 - Metodo: TrataOcorrencia -  Situação: DAMP0";

                    //        _obj.Vencimento = _linhaOcorrencia[0].Trim() + " : " + _linhaOcorrencia[1].Trim();
                    //        _obj.Pagamento = _linhaOcorrencia[3].Trim() + ": QuotaUnica:" + _linhaOcorrencia[4].Trim();
                    //        _obj.Dump = _linhaOcorrencia[2].Trim();
                    //        _obj.CodigoOcorrencia = "DAMP ou DAMP0";
                    //        _obj.Descricao = "***DAMP0";
                    //        break;
                    //    }
                    default:
                        break;
                }
            }
            catch (Exception exOut)
            {
                throw new ArgumentOutOfRangeException("Ocorrencia do PDF - Arquivo: BusinessParcelas - case: " + _case, exOut.Message); ;
            }

            return _obj;
        }

    }
}
