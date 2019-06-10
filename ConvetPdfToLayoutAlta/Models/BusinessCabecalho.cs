using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                            break;
                        }
                    case 19:
                        {
                            _arrayLinha = _linha
                                               .Replace("Correção", "")
                                               .Replace("Data Inclusao", ":")
                                               .Replace("Apólice", ":")
                                               .Trim().Split(':')
                                               .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
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
                            if (_arrayLinha.Length != 2)
                            {
                            }
                            obj.Numero = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.DataBase = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "").Substring(0, 8);
                            break;
                        }
                    case 7:
                        {
                            _case = "7 - Metodo: TrataCabecalho -  campo: DataEmicao";
                            if (_arrayLinha.Length != 1)
                            {
                            }
                            obj.DataEmicao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "").Substring(0, 8);
                            break;
                        }
                    case 8:
                        {
                            _case = "8 - Metodo: TrataCabecalho -  campo: Carteira, Contrato";
                            if (_arrayLinha.Length != 2)
                            {
                            }
                            obj.Carteira = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.Contrato = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            break;
                        }
                    case 9:
                        {
                            _case = "9 - Metodo: TrataCabecalho -  campo: Nome, CPF, DataNascimento";
                            if (_arrayLinha.Length != 3)
                            {
                            }
                            obj.Nome = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Z$]+", " ");
                            obj.Cpf = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.DataNascimento = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9\/$]+", "");
                            break;
                        }
                    case 10:
                        {
                            _case = "10 - Metodo: TrataCabecalho -  campo: EncdrecoImovel, CEP, Bairro...";
                            if (_arrayLinha.Length != 5)
                            {
                            }
                            obj.EnderecoImovel = _arrayLinha[0];
                            obj.Bairro = Regex.Replace(_arrayLinha[1].Trim(), @"[^A-Z$]+", " ");
                            obj.Cep = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9\-$]+", "");
                            obj.Cidade = Regex.Replace(_arrayLinha[3].Trim(), @"[^A-Z$]+", " ") + "-" + Regex.Replace(_arrayLinha[4].Trim(), @"[^A-Z$]+", "");
                            obj.ImovelUF = Regex.Replace(_arrayLinha[4].Trim(), @"[^A-Z$]+", "");
                            break;
                        }
                    case 11:
                        {
                            _case = "11 - Metodo: TrataCabecalho -  campo: Correspondencia...";
                            if (_arrayLinha.Length != 4)
                            {
                            }
                            obj.CorrespondenciaEndereco = _arrayLinha[0].Trim();
                            obj.CorrespondenciaBairro = _arrayLinha[1].Trim();
                            obj.CorrespondenciaCep = Regex.Replace(_arrayLinha[2], @"[^0-9\-$]+", "");
                            obj.CorrespondenciaCidade = _arrayLinha[3].Trim();
                            break;
                        }
                    case 12:
                        {
                            _case = "12 - Metodo: TrataCabecalho -  campo: Cliente, Telefone...";
                            if (_arrayLinha.Length != 3)
                            {
                            }
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
                                obj.Cartorio = _arrayLinha.Length > 5 ? Regex.Replace(_arrayLinha[count++].Trim(), @"[^A-Z$]+", "") : "";
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
                            if (_arrayLinha.Length != 4)
                            {
                            }
                            obj.Sistema = _arrayLinha[0].Trim() == "SAC" ? "S" : _arrayLinha[0].Trim() == "PRICE" ? "P" : _arrayLinha[0].Trim() == "LIVRE" ? "L" : _arrayLinha[0].Trim() == "SACRE" ? "R" : "O";
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
                            if (_arrayLinha.Length != 3)
                            {
                            }
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
                            obj.ValorGarantia = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            

                            if (!string.IsNullOrWhiteSpace(obj.FgtsUtilizado))
                                obj.Agencia = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                            else
                                obj.Empreendimento = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            if (_arrayLinha.Any(n => n.Equals("Razao")))
                                obj.Razao = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            else
                                obj.Taxa = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "");
                            break;
                        }

                    case 18:
                        {
                            _case = "18 - Metodo: TrataCabecalho -  campo: TaxaJuros, PrimeiroVencimento, Apolice...";
                           
                            obj.TaxaJuros = Regex.Replace(_arrayLinha[0].Trim(), @"[^0-9$]+", "");
                            obj.DataPrimeiroVencimento = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");

                            if (!string.IsNullOrWhiteSpace(obj.FgtsUtilizado))
                                obj.Empreendimento = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");
                            else
                                obj.Apolice = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            obj.Razao = string.IsNullOrWhiteSpace(obj.Razao) ? Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9$]+", "") : obj.Razao;
                            break;
                        }
                    case 19:
                        {
                            _case = "19 - Metodo: TrataCabecalho -  campo: Correção, DataInclusão...";
                            if (_arrayLinha.Length != 2 && _arrayLinha.Length != 3)
                            {
                            }
                            obj.Correcao = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9\/$]+", "");
                            obj.DataInclusao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");

                            if (_arrayLinha.Length > 2)
                                obj.Apolice = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9$]+", "");

                            break;
                        }
                    case 20:
                        {
                            _case = "20 - Metodo: TrataCabecalho -  campo: TipoFinancimento, DataAlteracao...";
                            if (_arrayLinha.Length != 2)
                            {
                            }
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
                            if (_arrayLinha.Length != 1 && _arrayLinha.Length != 2)
                            {
                            }
                            obj.TipoOrigem = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9$]+", " ");
                            if (_arrayLinha.Any(x => x.Equals("Ult")))
                                obj.DataUltimaAlteracao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "");

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

                            var arr = Regex.Replace(_linha, @"[^A-Z $]+", ":").Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                            _arrayLinha = Regex.Replace(_linha, @"[^0-9 .\-$]+", " ").Trim().Split().Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            arr.Add(_arrayLinha[1].Trim());
                            _arrayLinha = arr.ToArray();
                            break;
                        }
                    case 4:
                        {
                             _case = "4 - Metodo: TrataLinhaPDFPadrao2 -  campo: Bairro, Cep, Cidade... ";
                            _arrayLinha = _linha.Replace("Bairro", "")
                                                .Replace("CEP", "")
                                                .Replace("Cidade", "")
                                                .Replace("UF", "").Split(':')
                                                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();


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

                            obj.EnderecoImovel = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Z$]+", " ");
                            obj.Bairro = Regex.Replace(_arrayLinha[1].Trim(), @"[^A-z$]+", " ");
                            obj.Cidade = Regex.Replace(_arrayLinha[2].Trim().Substring(0, (_arrayLinha[2].Trim().Length - 2)), @"[^A-Z$]+", " ") +"-"+Regex.Replace(_arrayLinha[2].Trim().Substring((_arrayLinha[2].Trim().Length - 2)), @"[^A-Z$]+", " ");
                            obj.ImovelUF = Regex.Replace(_arrayLinha[2].Trim().Substring((_arrayLinha[2].Trim().Length - 2)), @"[^A-Z$]+", " ");
                            obj.Cep = Regex.Replace(_arrayLinha[3].Trim(), @"[^0-9\-$]+", "");
                            break;
                        }
                    case 4:
                        {
                            _case = "4 - Metodo: TrataCabecalhoPadrao2 -  campo: CorrespondenciaEndereco, CorrespondenciaBairro...";

                            obj.CorrespondenciaEndereco = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Z0-9$]+", " ");
                            obj.CorrespondenciaBairro = Regex.Replace(_arrayLinha[1].Trim(), @"[^A-z$]+", " ");
                            obj.CorrespondenciaCidade = Regex.Replace(_arrayLinha[3].Trim(), @"[^A-Z$]+", " ");
                            obj.Cep = Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9\-$]+", "");
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
    }
    
}
