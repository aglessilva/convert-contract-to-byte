using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessCabecalho
    {
        public string[] TrataLinhaPDF(string _linha, int _campo)
        {
            string[] _arrayLinha = null;

                switch (_campo)
                {
                    case 6:
                        {
                            _linha = _linha = Regex.Replace(_linha.Replace("Nº", ":").Replace("Data Base", ""), @"[^\wÀ-úa-zA-Z0-9:$]+", " ");
                            _arrayLinha = _linha.Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); 
                            break;
                        }
                    case 7:
                        {
                            _arrayLinha = _linha.Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); ;
                            break;
                        }
                    case 8:
                        {
                            _linha = _linha = Regex.Replace(_linha, @"[^0-9:$]+", "");
                            _arrayLinha = _linha.Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); ;
                            break;
                        }
                    case 9:
                        {
                            _arrayLinha = _linha.Trim().Replace("C.P.F.", ":").Replace("Data Nascimento", "").Split(':');
                            break;
                        }
                    case 10:
                        {
                        // PADRÃO FACIL PARA PEGAR O ENDEREÇO
                        _arrayLinha = _linha.Replace("End.Imóvel", "")
                                                  .Replace("Bairro", "")
                                                  .Replace("CEP", "")
                                                  .Replace("Cidade", "")
                                                  .Replace("UF", "")
                                                  .Trim().Split(':')
                                                  .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); 

                            break;
                        }
                    case 11:
                        {
                            _arrayLinha = _linha.Replace("Bairro", "")
                                                .Replace("CEP", "")
                                                .Replace("Cidade", "")
                                                .Replace("UF", "")
                                                .Trim().Split(':')
                                                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); 
                            break;
                        }
                    case 12:
                        {
                            _arrayLinha = _linha.Trim().Split('(');
                            break;
                        }
                    case 13:
                        {
                            _arrayLinha = _linha.Replace("Tx CET Ano", "")
                                                 .Replace("Tx CET Mes", "")
                                                 .Replace("Cartorio", "")
                                                 .Replace("CAC", "")
                                                 .Replace("PIS", "")
                                                 .Replace("DATA CADOC", "")
                                                 .Trim().Split(':')
                                                 .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); 
                            break;
                        }
                    case 14:
                        {
                            _arrayLinha = _linha.Replace("Plano", "")
                                                .Replace("Data Contrato", ":")
                                                .Replace("FGTS.UTILIZADO", ":")
                                                .Replace("Origem de Recursos", ":")
                                                .Replace("Prestação", ":")
                                                .Trim().Split(':')
                                                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                            if (_linha.Contains("FGTS.UTILIZADO"))
                            { 
                                var  newItem =_arrayLinha.ToList(); 
                                newItem.Add("FGTS");
                                _arrayLinha = newItem.ToArray();
                            }

                            break;
                        }
                    case 15:
                        {
                            
                            _arrayLinha = _linha
                                                .Replace("Sistema", "")
                                                .Replace("Valor Financiamento", ":") 
                                                .Replace("Origem de Recursos", ":")
                                                .Replace("Código Contábil",":")
                                                .Replace("Seguro MIP", ":") 
                                                .Trim().Split(':')
                                                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            break; 
                        }
                    case 16:
                        {
                            _arrayLinha = _linha
                                               .Replace("Data Garanta", "")
                                               .Replace("Agência", ":")
                                               .Replace("Código Contábil", ":")
                                               .Replace("Seguro DFI", ":")
                                               .Trim().Split(':')
                                               .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            break;
                        }
                    case 17:
                        {
                            _arrayLinha = _linha
                                               .Replace("Prazo", "")
                                               .Replace("Valor Garanta", ":")
                                               .Replace("Empreendimento", ":")
                                               .Replace("Data 1º Vencimento", ":")
                                               .Replace("Agência", ":")
                                               .Replace("TAXA",":")
                                               .Replace("Razão",":")
                                               .Trim().Split(':')
                                               .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                            if (_linha.Contains("Razão"))
                            {
                                var x = _arrayLinha.ToList();
                                x.Add("Razao");
                                _arrayLinha = x.ToArray();
                            }
                            if (_linha.Contains("TAXA"))
                            {
                                var x = _arrayLinha.ToList();
                                x.Add("TAXA");
                                _arrayLinha = x.ToArray();
                            }
                        if (_linha.Contains("1º"))
                        {
                            var x = _arrayLinha.ToList();
                            x.Add("1ºVENCIMENTO");
                            _arrayLinha = x.ToArray();
                        }
                        break;
                        }
                    case 18:
                        {
                            _arrayLinha = _linha
                                               .Replace("Taxa Juros", "")
                                               .Replace("Data 1º Vencimento", ":")
                                               .Replace("Empreendimento", ":")
                                               .Replace("Apólice", ":")
                                               .Replace("Razão", ":")
                                               .Trim().Split(':')
                                               .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            if (_linha.Contains("Razão"))
                            {
                                var x = _arrayLinha.ToList();
                                x.Add("Razao");
                                _arrayLinha = x.ToArray();
                            }
                        break;
                        }
                    case 19:
                        {
                            _arrayLinha = _linha
                                               .Replace("Correção", "")
                                               .Replace("Data Inclusao", ":")
                                               .Replace("Data Ult. Alteração", ":")
                                               .Replace("Apólice", ":")
                                               .Trim().Split(':')
                                                   .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            if (_linha.Contains("Data Ult. Alteração"))
                            {
                                var newItem = _arrayLinha.ToList();
                                newItem.Add("ALTER");
                                _arrayLinha = newItem.ToArray();
                            }
                        break;
                        }
                    case 20:
                        {
                            _arrayLinha = _linha
                                               .Replace("Tipo Financiamento", "")
                                               .Replace("Data Ult. Alteração", ":")
                                               .Replace("Data Re-Inclusão", ":")
                                               .Trim().Split(':')
                                               .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                            if (_linha.Contains("Re-Inclusão"))
                            {
                                var newItem = _arrayLinha.ToList();
                                newItem.Add("REINC");
                                _arrayLinha = newItem.ToArray();
                            }
                            break;
                        }
                    case 21:
                        {
                            _arrayLinha = _linha
                                               .Replace("Tipo de Origem", "")
                                               .Replace("Data Ult. Alteração", ":")
                                               .Trim().Split(':')
                                               .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                            if (_linha.Contains("Ult."))
                            {
                                var newItem = _arrayLinha.ToList();
                                newItem.Add("Ult");
                                _arrayLinha = newItem.ToArray();
                            }
                            break;
                        }
                case 22:
                    {
                        List<string> g = new List<string>();
                        var item  = Regex.Replace(_linha.Trim(), @"[^0-9$]"," ").Split(' ') .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                        g.Add(item.First());
                        g.Add(item[1]);
                        g.Add(item.LastOrDefault());

                        _arrayLinha = g.ToArray();
                        break;
                    }
                case 23:
                    {
                        _arrayLinha = _linha
                                            .Replace("Repactuação", "")
                                            .Replace("Empreendimento", ":")
                                            .Replace("Valor Garanta", ":")
                                            .Replace("Agência", ":")
                                            .Replace("TAXA", ":")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        if (_linha.Contains("Empreendimento"))
                        {
                            var newItem = _arrayLinha.ToList();
                            newItem.Add("Empreendimento");
                            _arrayLinha = newItem.ToArray();
                        }

                        break;
                    }
                case 24:
                    {
                        _arrayLinha = _linha
                                            .Replace("Taxa Juros", "")
                                            .Replace("Data Inclusao", ":")
                                            .Replace("Carência", ":")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        break;
                    }
                case 25:
                    {
                        _arrayLinha = _linha
                                            .Replace("Taxa Juros", "")
                                            .Replace("Data Inclusao", ":")
                                            .Replace("Apólice", ":")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        break;
                    }
                default:
                        break;
                }
            

            return _arrayLinha;
        }

        public Cabecalho TrataCabecalho(Cabecalho obj, string[] _arrayLinha, int linha, string _campos = null)
        {
            string _case = string.Empty;
            try
            {
                switch (linha)
                {
                    case 6:
                        {
                            _case = "6 - Metodo: TrataCabecalho -  campo: Numero, DataBase";
                            
                            obj.Numero = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.DataBase = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "").Substring(0, 8);
                            break;
                        }
                    case 7:
                        {
                            _case = "7 - Metodo: TrataCabecalho -  campo: DataEmicao";
                           
                            obj.DataEmicao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "").Substring(0, 8);
                            break;
                        }
                    case 8:
                        {
                            _case = "8 - Metodo: TrataCabecalho -  campo: Carteira, Contrato";
                           
                            obj.Carteira = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.Contrato = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            break;
                        }
                    case 9:
                        {
                            _case = "9 - Metodo: TrataCabecalho -  campo: Nome, CPF, DataNascimento";
                           
                            obj.Nome = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Z$]+", " ");
                            obj.Cpf = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.DataNascimento = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9\/$]+", "");
                            break;
                        }
                    case 10:
                        {
                            _case = "10 - Metodo: TrataCabecalho -  campo: EncdrecoImovel, CEP, Bairro...";

                            string _bairroImovel = Regex.Replace(_arrayLinha[1].Trim(), @"[^A-Z$]+", " ");

                            obj.EnderecoImovel = _arrayLinha.Length > 0 ? Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9\-.$]+", " ") : "";
                            obj.BairroImovel = _arrayLinha.Length > 1 ? _bairroImovel : "";
                            obj.CepImovel = _arrayLinha.Length > 2 ? Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9\-$]+", "") : "";
                            obj.CidadeImovel = _arrayLinha.Length > 3 ? Regex.Replace(_arrayLinha[3].Trim(), @"[^A-Z$]+", " ") + "-" + Regex.Replace(_arrayLinha[4].Trim(), @"[^A-Z$]+", "") : "";
                            obj.UfImovel = _arrayLinha.Length > 4 ? Regex.Replace(_arrayLinha[4].Trim(), @"[^A-Z$]+", "") : "";

                            obj.EnderecoImovel = string.IsNullOrWhiteSpace(obj.EnderecoImovel.Trim()) ? "***NAO INFORMADO***" : obj.EnderecoImovel;
                            obj.BairroImovel = string.IsNullOrWhiteSpace(obj.BairroImovel.Trim()) ? "***NAO INFORMADO***" : obj.BairroImovel;
                            obj.CepImovel = string.IsNullOrWhiteSpace(obj.CepImovel.Trim()) ? "00000000" : obj.CepImovel;
                            obj.CidadeImovel = string.IsNullOrWhiteSpace(obj.CidadeImovel.Trim()) ? "***NAO INFORMADO***" : obj.CidadeImovel;
                            obj.UfImovel = string.IsNullOrWhiteSpace(obj.UfImovel.Trim()) ? "XX" : obj.UfImovel;


                            break;
                        }
                    case 12:
                        {
                            _case = "12 - Metodo: TrataCabecalho -  campo: Cliente, Telefone...";
                           
                            obj.Cliente = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.TelefoneResidencia = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.TelefoneComercial = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                            break;
                        }
                    case 13:
                        {
                            int count = 0;
                            _case = "13 - Metodo: TrataCabecalho -  campo: PIS,CAC TxCET...";
                            if (_arrayLinha.Length == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(_campos))
                                {
                                    if (_campos.Contains("CET"))
                                        obj.TxCETAno = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                    if (_campos.Contains("CADOC"))
                                        obj.DataCaDoc = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                }
                            }
                            else
                            {
                                obj.TxCETAno = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                obj.TxCEMes = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                obj.Cartorio = _arrayLinha.Length > 5 ? Regex.Replace(_arrayLinha[count++].Trim(), @"[^A-Za-z$]+", "") : "";
                                obj.CAC = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                obj.Pis = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                obj.DataCaDoc = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                            }

                            break;
                        }
                    case 14:
                        {
                            _case = "14 - Metodo: TrataCabecalho -  campo: Plano, DataContrato, Origem...";
                            if (_arrayLinha.Length != 4)
                            {
                            }
                            obj.Plano = _arrayLinha[0].Trim();
                            obj.DataContrato = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");
                            if (_arrayLinha.Any(x => x.Equals("FGTS")))
                                obj.FgtsUtilizado = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                            else
                                obj.OrigemRecurso = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            obj.Prestacao = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            break;
                        }
                    case 15:
                        {
                            _case = "15 - Metodo: TrataCabecalho -  campo: Sistema, CodigoContabel...";
                           
                            obj.Sistema = _arrayLinha[0].Trim().ToUpper().Contains("SAC") ? "S" : _arrayLinha[0].Trim().ToUpper().Contains("PRICE") ? "P" : _arrayLinha[0].Trim().ToUpper().ToUpper().Contains("LIVRE") ? "L" : _arrayLinha[0].Trim().ToUpper().Contains("SACRE") ? "R" : "O";
                            obj.ValorFinanciamento = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            if (string.IsNullOrWhiteSpace(obj.FgtsUtilizado))
                                obj.CodigoContabil = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                            else
                                obj.OrigemRecurso = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            obj.SeguroMIP = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            break;
                        }
                    case 16:
                        {
                            _case = "16 - Metodo: TrataCabecalho -  campo: DataGarantia, agencia...";
                           
                            obj.DataGarantia = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9\/$]+", "");
                            if (string.IsNullOrWhiteSpace(obj.FgtsUtilizado))
                                obj.Agencia = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            else
                                obj.CodigoContabil = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");

                            obj.SeguroDFI = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                            break;
                        }
                    case 17:
                        {
                            _case = "17 - Metodo: TrataCabecalho -  campo: ValorGarantia, Prazo, Taxa...";
                            obj.Prazo = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");

                            if (_arrayLinha.Any(n => n.Equals("1ºVENCIMENTO"))) 
                                obj.DataPrimeiroVencimento = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$\/]", "");
                            else
                                obj.ValorGarantia = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");

                            if (!string.IsNullOrWhiteSpace(obj.FgtsUtilizado))
                                obj.Agencia = string.IsNullOrWhiteSpace(obj.Agencia) ? Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "") : obj.Agencia;
                            else
                                obj.Empreendimento = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            if (_arrayLinha.Any(n => n.Equals("Razao")))
                                obj.Razao = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            else
                                obj.Razao = "0";//string.IsNullOrWhiteSpace(obj.Razao) ? Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "") : obj.Razao;

                            if (_arrayLinha.Any(n => n.Equals("TAXA")))
                                obj.Taxa = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            else
                                obj.Taxa = "0"; // string.IsNullOrWhiteSpace(obj.Taxa) ? Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "") : obj.Taxa;
                            break;
                        }

                    case 18:
                        {
                            _case = "18 - Metodo: TrataCabecalho -  campo: TaxaJuros, PrimeiroVencimento, Apolice...";
                           
                            obj.TaxaJuros = string.IsNullOrWhiteSpace(obj.TaxaJuros) ? Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "") : obj.TaxaJuros;
                            obj.DataPrimeiroVencimento  = string.IsNullOrWhiteSpace(obj.DataPrimeiroVencimento) ? Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "") : obj.DataPrimeiroVencimento;

                            if (!string.IsNullOrWhiteSpace(obj.FgtsUtilizado))
                                obj.Empreendimento = string.IsNullOrWhiteSpace(obj.Empreendimento) ? Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "") : obj.Empreendimento;
                            else
                                obj.Apolice = string.IsNullOrWhiteSpace(obj.Apolice) ? Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "") : obj.Apolice;

                            if (_arrayLinha.Any(n => n.Equals("Razao")))
                                obj.Razao = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            else
                                obj.Razao = !string.IsNullOrWhiteSpace(obj.Razao) && obj.Razao != "0" ? obj.Razao : "0";
                            break;
                        }
                    case 19:
                        {
                            _case = "19 - Metodo: TrataCabecalho -  campo: Correção, DataInclusão...";
                           
                            obj.Correcao = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9\/$]+", "");
                            obj.DataInclusao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");

                            if (_arrayLinha.Length > 2)
                                if (!_arrayLinha[2].Equals("ALTER"))
                                    obj.Apolice = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                                else
                                    obj.DataUltimaAlteracao = _arrayLinha[2];

                            break;
                        }
                    case 20:
                        {
                            _case = "20 - Metodo: TrataCabecalho -  campo: TipoFinancimento, DataAlteracao...";
                           
                            obj.TipoFinanciamento = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9$]+", " ");

                            if (_arrayLinha.Any(x => x.Equals("REINC")))
                                obj.DataReinclusao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");
                            else
                                obj.DataUltimaAlteracao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");
                            break;
                        }

                    case 21:
                        {
                            _case = "21 - Metodo: TrataCabecalho -  campo: TipoOrigem, DataUltimaAlteracao...";
                           
                            obj.TipoOrigem = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9$]+", " ");
                            if (_arrayLinha.Any(x => x.Equals("Ult")))
                                obj.DataUltimaAlteracao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");

                            break;
                        }
                    case 23:
                        {
                            _case = "23 - Metodo: TrataCabecalho -  campo: Repactuação...";

                            obj.Repactuacao = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9\/$]+", "");
                            obj.ValorGarantia = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]", "");
                            if (_arrayLinha.Any(o => o.Trim().Equals("Empreendimento")))
                                obj.Empreendimento = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]", "");
                            else
                                obj.Agencia = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]", "");

                            obj.Taxa = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]", "");

                            break;
                        }
                    case 24:
                        {
                            _case = "24 - Metodo: TrataCabecalho -  campo: Carência...";

                            obj.TaxaJuros = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.DataInclusao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]", "");
                            obj.Carencia = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]", "");

                            break;
                        }

                    case 25:
                        {
                            _case = "24 - Metodo: TrataCabecalho -  campo: Apolice, Data Inclusao, Taxa Juros...";

                            if (_arrayLinha.Length > 2)
                            {
                                obj.TaxaJuros = string.IsNullOrWhiteSpace(obj.TaxaJuros) ? Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "") : obj.TaxaJuros;
                                obj.DataInclusao = string.IsNullOrWhiteSpace(obj.DataInclusao) ? Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$\/]+", "") : obj.DataInclusao;
                                obj.Apolice = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]", "");
                            }
                            else
                                obj.Apolice = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]", "");

                            break;
                        }

                    default:
                        return obj;

                }
            }
            catch (Exception exOut)
            {
                throw new ArgumentOutOfRangeException("Cabeçaho do PDF - Arquivo: BusinessCabecalho - case: " + _case, exOut.InnerException); ;
            }

            return obj;
        }



        public string[] TrataLinhaPDFPadrao2(string _linha, int _campo)
        {
            string[] _arrayLinha = null;
            string _case =  string.Empty;
            try
            {

                switch (_campo)
                {
                    case 2:
                        {
                            _case = "2 - Metodo: TrataLinhaPDFPadrao2 -  campo: Nome, CPF...";

                            _arrayLinha = _linha.Replace("C.P.F.", ":").Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            var arr = _arrayLinha[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList(); ;
                            arr.Add(_arrayLinha[0]);
                            _arrayLinha = arr.ToArray();
                            break;
                        }
                    case 3:
                        {
                            _case = "3 - Metodo: TrataLinhaPDFPadrao2 -  campo: *";

                            var arr = _linha.Split(' ').ToList();
                            string _endereco = string.Join(" ", arr.GetRange(0, arr.FindIndex(f => string.IsNullOrWhiteSpace(f))));
                            arr.RemoveRange(0, arr.FindIndex(g => string.IsNullOrWhiteSpace(g)));

                            var y = arr.Where(o => !string.IsNullOrWhiteSpace(o)).ToList();

                            int sequencia = y.FindIndex(u => Regex.IsMatch(u, ("[0-9]{2}.[0-9]{3}-[0-9]{3}")));
                           string _cep = y[sequencia]; y.RemoveAt(sequencia);

                            string _bairro= string.Join(" ", y.GetRange(0, sequencia));
                            string _cidade = string.Join(" ", y.GetRange(sequencia, (y.Count - sequencia)));

                           _arrayLinha = new List<string>() { _endereco, _bairro , _cidade,  y.Last(), _cep}.ToArray();

                            break;
                        }
                    case 5:
                        {
                             _case = "5 - Metodo: TrataLinhaPDFPadrao2 -  campo: *";

                            _arrayLinha = Regex.Replace(_linha, @"[^0-9($]", " ").Trim().Split('(');
                            break;
                        }
                    case 6:
                        {
                            // Alteração padrão 2 Categoria
                             _case = "6 - Metodo: TrataLinhaPDFPadrao2 -  campo: Categoria";

                            _arrayLinha = Regex.Replace(_linha, @"[^0-9$]", " ").Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            break;
                        }
                    default:

                        break;
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Cabeçaho do PDF - Arquivo: BusinessCabecalho - case: " + _case, ex.Message);
            }
            return _arrayLinha;
        }


        public Cabecalho TrataCabecalhoPadrao2(Cabecalho obj, string[] _arrayLinha, int linha)
        {
            string _case = string.Empty;
            try
            {

                switch (linha)
                {
                    case 1:
                        {
                            _case = "1 - Metodo: TrataCabecalhoPadrao2 -  campo: Carteira, Contrato";

                            obj.Carteira = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            obj.Contrato = Regex.Replace(_arrayLinha[5].Trim(), @"[^0-9$]+", "");
                            break;
                        }
                    case 2:
                        {
                            _case = "2 - Metodo: TrataCabecalhoPadrao2 -  campo: Cpf, DataNascimento";

                            obj.Nome = Regex.Replace(_arrayLinha[2].Trim(), @"[^A-Z$]+", " ");
                            obj.Cpf = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.DataNascimento = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");
                            break;
                        }
                    case 3:
                        {
                            _case = "3 - Metodo: TrataCabecalhoPadrao2 -  campo: Bairro, ImovelUF...";

                            string _endImovel = _arrayLinha[1].Trim();

                            obj.EnderecoImovel = _arrayLinha[0].Trim();
                            obj.BairroImovel = _endImovel.Length > 24 ? _endImovel.Substring(0,24) : _endImovel;
                            obj.CidadeImovel = _arrayLinha[2].Trim();
                            obj.UfImovel = _arrayLinha[3].Trim();
                            obj.CepImovel = Regex.Replace(_arrayLinha[4].Trim(), @"[^0-9\-$]","");
                            
                            break;
                        }
                    case 5:
                        {
                            _case = "5 - Metodo: TrataCabecalhoPadrao2 -  campo: Cliente, TelefoneResidencia";

                            obj.Cliente = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.TelefoneResidencia = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.TelefoneComercial = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                            break;
                        }
                    case 6:
                        {
                            _case = "6 - Metodo: TrataCabecalhoPadrao2 -  campo: Modalidade, CodigoContabil";

                            obj.Categoria = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.Modalidade = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.CodigoContabil = Regex.Replace(_arrayLinha[(_arrayLinha.Length == 3 ? 2: 3)].Trim(), @"[^0-9$]+", "");
                            break;
                        }


                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Cabeçaho do PDF - Arquivo: BusinessCabecalho - case: " + _case, ex.Message);
            }
            return obj;
        }

        public string[] TrataArray(string _linha)
        {
            string[] arrayLinha = _linha.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            return arrayLinha;
        }

        public void PopulaContrato(List<ContratoPdf> lstContratosPdf, List<string> lstGT)
        {

            string strAlta = string.Empty;
            string _contratoGT = string.Empty;
            string _apolice = string.Empty, _taxaJuros = string.Empty, _prazo = string.Empty, _reajuste = string.Empty, _valorgarantia = string.Empty;

            _apolice = _taxaJuros = _prazo = _reajuste = _valorgarantia = string.Empty;
            //using (StreamWriter escreverContrato = new StreamWriter(@"D:\PDFSTombamento\txt\AltaContrato.txt", true, Encoding.UTF8))
            //{
            //    lstContratosPdf.ForEach(q =>
            //    {

            //        q.Cabecalhos.ForEach(l =>
            //        {
            //            if (!l.TaxaJuros.Trim().Equals(_taxaJuros))
            //                _taxaJuros = l.TaxaJuros;
            //            if (!l.Apolice.Trim().Equals(_apolice))
            //                _apolice = l.Apolice;
            //            if (!l.Prazo.Trim().Equals(_prazo))
            //                _prazo = l.Prazo;
            //            if (!l.Reajuste.Trim().Equals(_reajuste))
            //                _reajuste = l.Reajuste;
            //        });

            //        Cabecalho c = q.Cabecalhos.FirstOrDefault();

            //        _contratoGT = lstGT.Find(p => p.Equals(c.Carteira.Substring(2, 2) + c.Contrato.Trim()));
            //        strAlta = string.Format("{0}{1}{2}", c.Carteira.Trim().Substring(2), c.Contrato.Trim(), c.DataPrimeiroVencimento.Trim().Substring(0, 2)).PadRight(24, ' ');
            //        strAlta += string.Format("{0}{1}{2}{3}", c.Nome.Trim().PadRight(40, ' '), c.DataNascimento.Trim().PadRight(10, ' '), "".PadRight(14, ' '), c.EnderecoImovel.Trim().PadRight(80, ' '));
            //        strAlta += string.Format("{0}{1}{2}{3}", c.Cpf.Trim().PadRight(14, ' '), "".PadRight(3, ' '), _contratoGT.Trim().PadRight(20, ' '), "".PadRight(20, ' '));
            //        strAlta += string.Format("{0}{1}", c.Modalidade.Trim().PadRight(40, ' '), (c.CidadeImovel.Trim().Length <= 31 ? c.CidadeImovel.Trim() : c.CidadeImovel.Trim().Substring(0, 31)).PadRight(31, ' '));
            //        strAlta += string.Format("{0}{1}{2}", c.Plano.Trim().PadRight(10, ' '), c.DataContrato.Trim().PadRight(10, ' '), "".PadRight(2, ' '));
            //        strAlta += string.Format("{0}{1}", c.Prestacao.Trim().PadLeft(18, '0'), c.Sistema.Trim().PadRight(2, ' '));
            //        strAlta += string.Format("{0}{1}{2}", c.ValorFinanciamento.Trim().PadLeft(18, '0'), c.CodigoContabil.Trim().Substring(1).PadRight(15, '0'), c.SeguroMIP.Trim().PadLeft(18, '0'));
            //        strAlta += string.Format("{0}{1}", (string.IsNullOrWhiteSpace(_reajuste) ? c.Reajuste.Trim() : _reajuste).PadRight(10, ' '), c.DataGarantia.Trim().PadRight(18, ' '));
            //        strAlta += string.Format("{0}", c.SeguroDFI.Trim().PadLeft(18, '0'));
            //        strAlta += string.Format("{0}{1}{2}", (string.IsNullOrWhiteSpace(_prazo) ? c.Prazo.Trim() : _prazo).PadLeft(5, '0'), c.ValorGarantia.Trim().PadLeft(18, '0'), "".PadRight(8, ' '));
            //        strAlta += string.Format("{0}{1}", "0".PadRight(18, '0'), (string.IsNullOrWhiteSpace(_taxaJuros) ? c.TaxaJuros.Trim() : _taxaJuros).Trim().PadLeft(11, '0'));
            //        strAlta += string.Format("{0}{1}{2}", c.DataPrimeiroVencimento.Trim().PadLeft(10, '0'), (string.IsNullOrWhiteSpace(_apolice) ? c.Apolice.Trim() : _apolice).PadLeft(6, '0'), (c.CepImovel.Trim() + "-" + c.BairroImovel.Trim()).PadRight(34, ' '));
            //        strAlta += string.Format("{0}{1}{2}{3}", "0".PadRight(11, '0'), c.Correcao.Trim().PadRight(10, ' '), "0".PadLeft(2, '0'), c.Razao.Trim().PadLeft(18, '0'));
            //        strAlta += string.Format("{0}{1}", "0".PadLeft(2, '0'), c.Situacao.Trim()).PadRight(60, ' ');
            //        c = null;
            //        escreverContrato.WriteLine(strAlta);

            //        strAlta = string.Empty;
            //    });
            //}


            using (StreamWriter escreverOcorrencia = new StreamWriter(@"D:\PDFSTombamento\txt\AltaOcorrencia.txt", true, Encoding.UTF8))
            {
                strAlta = string.Empty;
                Parcela _parcela = null;
                Cabecalho _cabecalho = null,  _cabecalhoAnterior = null;

                lstContratosPdf.ForEach(q => {


                    q.Ocorrencias.ForEach(o => {

                        _parcela = q.Parcelas.Find(m => m.Sequencia == o.ReferenciaParcela);
                        _cabecalho = q.Cabecalhos.Last();
                        _cabecalhoAnterior = q.Cabecalhos[(q.Cabecalhos.Count - 2)];

                        strAlta = string.Format("{0}", (q.Carteira.Substring(2,2) + o.Contrato).PadRight(15,'0'));
                        strAlta += string.Format("{0}{1}", o.Vencimento.Trim().Trim().PadRight(10, '0'), o.Pagamento.Trim().Trim().PadRight(10, '0'));
                        strAlta += string.Format("{0}", o.Descricao.Trim().PadRight(36,' '));

                        if (o.CodigoOcorrencia.Equals("020")) //Amortização extra
                        {
                            strAlta += string.Format("{0}{1}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'));
                            strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                            strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                        }
                        if (o.CodigoOcorrencia.Equals("010")) // Alteração Contratual
                        {
                            if (!_cabecalhoAnterior.TaxaJuros.Equals(_cabecalho.TaxaJuros))
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), "TAXA JUROS".PadRight(30, ' '));
                                strAlta += string.Format("{0}{1}" ,_cabecalhoAnterior.TaxaJuros.Trim().PadRight(30, ' '), _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                            }

                            if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                            else
                                strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                        }

                        escreverOcorrencia.WriteLine(strAlta);
                        _parcela = null;
                        strAlta = string.Empty;
                    });
                    
                });
            }

        }

    }
    
}
