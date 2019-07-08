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
        /// <summary>
        /// Função que trata e extrais cada linhas de registro do PDF
        /// </summary>
        /// <param name="_linha">Linha do Pdf</param>
        /// <param name="_campo"> numero da linha  do pdf</param>
        /// <returns></returns>
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
                        _arrayLinha = _linha.Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
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
                        string _nome = string.Empty, _cpf = string.Empty, _dataNascimento = string.Empty;

                        _arrayLinha = _linha.Trim().Replace("C.P.F.", ":").Replace("Data Nascimento", "").Split(':');
                        if (_arrayLinha.Length < 3)
                        {
                            var x = TrataArray(_linha).ToList();

                            _dataNascimento = x.Last();

                            for (int i = 0; i < x.Count; i++)
                            {
                                if (!x[i].Contains("."))
                                {
                                    _nome += x[i] + " ";
                                    x.Remove(x[i]);
                                    i--;
                                }
                                else break;
                            }
                            for (int i = 0; i < x.Count; i++)
                            {
                                if (Regex.IsMatch(x[i], @"\d{3}.\d{3}.\d{3}-\d{2}"))
                                {
                                    _cpf = x[i];
                                }
                            }

                            _arrayLinha = new string[] { _nome, _cpf, _dataNascimento };
                        }

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
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úÀ-ÙA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Plano", ":Plano")
                                            .Replace("Data Contrato", ":DtContrato")
                                            .Replace("COMISSÃO", ":Comisao")
                                            .Replace("FGTS.UTILIZADO", ":FgtsUtil")
                                            .Replace("Origem de Recursos", ":OriRec")
                                            .Replace("Prestação", ":Prestacao")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        break;
                    }
                case 15:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Sistema", ":Sistema")
                                            .Replace("Valor Financiamento", ":VlrFinan")
                                            .Replace("Origem de Recursos", ":OrigRec")
                                            .Replace("Código Contábil", ":Contabil")
                                            .Replace("Seguro MIP", ":SeguroMIP")
                                            .Replace("TAXA", ":TAXA")
                                            .Replace("Razão", ":Razao")
                                            .Replace("FGTS.UTILIZADO", ":FgtsUtil")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        break;
                    }
                case 16:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Data Garanta", ":DtGarantia")
                                            .Replace("Agência", ":Agencia")
                                            .Replace("Código Contábil", ":Contabil")
                                            .Replace("Seguro DFI", ":SeguroDFi")
                                            .Replace("Origem de Recursos", ":OrigRec")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        break;
                    }
                case 17:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Prazo", ":Prazo")
                                            .Replace("Valor Garanta", ":vlrGrantia")
                                            .Replace("Empreendimento", ":Emp")
                                            .Replace("Data 1 Vencimento", ":Dt1Venc")
                                            .Replace("Agência", ":Agencia")
                                            .Replace("TAXA", ":TAXA")
                                            .Replace("Razão", ":Razao")
                                            .Replace("IOF.SEG", ":SEG")
                                            .Replace("Carência", ":Carencia")
                                            .Replace("Código Contábil", ":Contabil")
                                           .Trim().Split(':')
                                           .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        break;
                    }
                case 18:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                           .Replace("Taxa Juros", ":TaxaJuros").Trim()
                                           .Replace("Data 1 Vencimento", ":Data1v").Trim()
                                           .Replace("Empreendimento", ":Emp").Trim()
                                           .Replace("Apólice", ":apolice").Trim()
                                           .Replace("Razão", ":Razao").Trim()
                                           .Replace("IOF.SEG", ":SEG").Trim()
                                           .Replace("Correção", ":Correcao").Trim()
                                           .Replace("Data Inclusao", ":DtInc").Trim()
                                           .Replace("Agência", ":Agencia").Trim()
                                           .Trim().Split(':')
                                           .Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();
                        break;
                    }
                case 19:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Correção", ":Correcao")
                                            .Replace("Data Inclusao", ":DtInc")
                                            .Replace("Data Re-Inclusão", ":DtReInc")
                                            .Replace("Data Ult. Alteração", ":DtUltAlter")
                                            .Replace("Apólice", ":Apolice")
                                           .Trim().Split(':')
                                           .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        break;
                    }
                case 20:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Tipo Financiamento", ":TipoFinanc")
                                            .Replace("Data Ult. Alteração", ":DtUltAlter")
                                            .Replace("Data Re-Inclusão", ":DtReInc")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        break;
                    }
                case 21:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                           .Replace("Tipo de Origem", ":TipoOrig")
                                           .Replace("Data Ult. Alteração", ":DtUltAlter")
                                           .Trim().Split(':')
                                           .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        break;
                    }
                case 22:
                    {
                        List<string> g = new List<string>();
                        var item = Regex.Replace(_linha.Trim(), @"[^0-9$]", " ").Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                        g.Add(item.First());
                        g.Add(item[1]);
                        g.Add(item.LastOrDefault());

                        _arrayLinha = g.ToArray();
                        break;
                    }
                case 23:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Repactuação", ":Repac").Replace("REP", "01")
                                            .Replace("Empreendimento", ":Emp")
                                            .Replace("Valor Garanta", ":VlrGarantia")
                                            .Replace("Agência", ":Agencia")
                                            .Replace("TAXA", ":TAXA")
                                            .Replace("Razão", ":Razao")
                                            .Replace("IOF.SEG", ":SEG")
                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
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
                int _id = 0;

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

                            obj.DataEmicao = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9\/$]+", "").Substring(0, 10);
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

                            string _bairroImovel = Regex.Replace(_arrayLinha[1].Trim(), @"[^A-Z0-9$]+", " ");

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

                            if (!string.IsNullOrWhiteSpace(_campos))
                            {
                                if (_campos.Contains("Ano"))
                                    obj.TxCETAno = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                else
                                    obj.TxCETAno = "0";

                                if (_campos.Contains("Mes"))
                                    obj.TxCEMes = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                else
                                    obj.TxCEMes = "0";

                                if (_campos.Contains("CAC"))
                                    obj.CAC = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                else
                                    obj.CAC = "0";

                                if (_campos.Contains("PIS"))
                                    obj.Pis = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                else
                                    obj.Pis = "0";

                                if (_campos.Contains("CADOC"))
                                    obj.DataCaDoc = Regex.Replace(_arrayLinha[count++].Trim(), @"[^0-9$]+", "");
                                else
                                    obj.DataCaDoc = "0";
                            }

                            break;
                        }
                    case 14:
                        {
                            _case = "14 - Metodo: TrataCabecalho -  campo: Plano, DataContrato, Origem...";

                            if (_arrayLinha.Any(n => n.Contains("Plano")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Plano"));
                                obj.Plano = _arrayLinha[_id].Replace("Plano", "").Trim();
                            }

                            if (_arrayLinha.Any(n => n.Contains("DtContrato")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtContrato"));
                                obj.DataContrato = Regex.Replace(_arrayLinha[_id].Replace("DtContrato", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Comisao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Comisao"));
                                obj.Comissao = Regex.Replace(_arrayLinha[_id].Replace("Comisao", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("FgtsUtil")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("FgtsUtil"));
                                obj.FgtsUtilizado = Regex.Replace(_arrayLinha[_id].Replace("FgtsUtil", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("OriRec")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("OriRec"));
                                obj.OrigemRecurso = Regex.Replace(_arrayLinha[_id].Replace("OriRec", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Prestacao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Prestacao"));
                                obj.Prestacao = Regex.Replace(_arrayLinha[_id].Replace("Prestacao", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            break;
                        }
                    case 15:
                        {
                            _case = "15 - Metodo: TrataCabecalho -  campo: Sistema, CodigoContabel...";

                            if (_arrayLinha.Any(n => n.Contains("Sistema")))
                            {
                                string _sistema = Regex.Replace(_arrayLinha[_id].Replace("Sistema", "").Trim(), @"[^A-Z0-9\/$]+", "");
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Sistema"));
                                obj.Sistema = _sistema.Equals("SAC") ? "S" : _sistema.Equals("PRICE") ? "P" : _sistema.Equals("LIVRE") ? "L" : _sistema.Equals("SACRE") ? "R" : "O";
                            }

                            if (_arrayLinha.Any(n => n.Contains("VlrFinan")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("VlrFinan"));
                                obj.ValorFinanciamento = Regex.Replace(_arrayLinha[_id].Replace("VlrFinan", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("OrigRec")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("OrigRec"));
                                obj.OrigemRecurso = _arrayLinha[_id].Replace("OriRec", "").Trim();
                            }

                            if (_arrayLinha.Any(n => n.Contains("Contabil")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Contabil"));
                                obj.CodigoContabil = Regex.Replace(_arrayLinha[_id].Replace("Contabil", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("SeguroMIP")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SeguroMIP"));
                                obj.SeguroMIP = Regex.Replace(_arrayLinha[_id].Replace("SeguroMIP", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Razao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Razao"));
                                obj.Razao = Regex.Replace(_arrayLinha[_id].Replace("Razao", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("TAXA")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TAXA"));
                                obj.Taxa = Regex.Replace(_arrayLinha[_id].Replace("TAXA", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("FgtsUtil")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("FgtsUtil"));
                                obj.FgtsUtilizado = Regex.Replace(_arrayLinha[_id].Replace("FgtsUtil", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            break;
                        }
                    case 16:
                        {
                            _case = "16 - Metodo: TrataCabecalho -  campo: DataGarantia, agencia...";

                            if (_arrayLinha.Any(n => n.Contains("DtGarantia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtGarantia"));
                                obj.DataGarantia = _arrayLinha[_id].Replace("DtGarantia", "").Trim();
                            }
                            if (_arrayLinha.Any(n => n.Contains("Agencia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Agencia"));
                                obj.Agencia = Regex.Replace(_arrayLinha[_id].Replace("Agencia", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Contabil")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Contabil"));
                                obj.CodigoContabil = Regex.Replace(_arrayLinha[_id].Replace("Contabil", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("SeguroDFi")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SeguroDFi"));
                                obj.SeguroDFI = Regex.Replace(_arrayLinha[_id].Replace("SeguroDFi", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("OrigRec")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("OrigRec"));
                                obj.OrigemRecurso = Regex.Replace(_arrayLinha[_id].Replace("OrigRec", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            break;
                        }
                    case 17:
                        {
                            _case = "17 - Metodo: TrataCabecalho -  campo: ValorGarantia, Prazo, Taxa...";

                            if (_arrayLinha.Any(n => n.Contains("Prazo")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Prazo"));
                                obj.Prazo = Regex.Replace(_arrayLinha[_id].Replace("Prazo", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("vlrGrantia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("vlrGrantia"));
                                obj.ValorGarantia = Regex.Replace(_arrayLinha[_id].Replace("vlrGrantia", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Emp")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Emp"));
                                obj.Empreendimento = Regex.Replace(_arrayLinha[_id].Replace("Emp", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Dt1Venc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Dt1Venc"));
                                obj.DataPrimeiroVencimento = Regex.Replace(_arrayLinha[_id].Replace("Dt1Venc", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Agencia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Agencia"));
                                obj.Agencia = Regex.Replace(_arrayLinha[_id].Replace("Agencia", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("TAXA")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TAXA"));
                                obj.Taxa = Regex.Replace(_arrayLinha[_id].Replace("TAXA", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Razao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Razao"));
                                obj.Razao = Regex.Replace(_arrayLinha[_id].Replace("Razao", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("SEG")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SEG"));
                                obj.Iof = Regex.Replace(_arrayLinha[_id].Replace("SEG", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Carencia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Carencia"));
                                obj.Carencia = Regex.Replace(_arrayLinha[_id].Replace("Carencia", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Contabil")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Contabil"));
                                obj.CodigoContabil = Regex.Replace(_arrayLinha[_id].Replace("Contabil", "").Trim(), @"[^0-9\/$]+", "");
                            }


                            break;
                        }

                    case 18:
                        {
                            _case = "18 - Metodo: TrataCabecalho -  campo: TaxaJuros, PrimeiroVencimento, Apolice...";

                            if (_arrayLinha.Any(n => n.Contains("TaxaJuros")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TaxaJuros"));
                                obj.TaxaJuros = Regex.Replace(_arrayLinha[_id].Replace("TaxaJuros", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Data1v")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Data1v"));
                                obj.DataPrimeiroVencimento = Regex.Replace(_arrayLinha[_id].Replace("Data1v", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("apolice")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("apolice"));
                                obj.Apolice = Regex.Replace(_arrayLinha[_id].Replace("apolice", "").Trim(), @"[^0-9$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Razao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Razao"));
                                obj.Razao = Regex.Replace(_arrayLinha[_id].Replace("Razao", "").Trim(), @"[^0-9$]+", "");
                            }
                            else
                                obj.Razao = !string.IsNullOrWhiteSpace(obj.Razao) && obj.Razao != "0" ? obj.Razao : "0";

                            if (_arrayLinha.Any(n => n.Contains("SEG")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SEG"));
                                obj.Iof = Regex.Replace(_arrayLinha[_id].Replace("SEG", "").Trim(), @"[^0-9$]+", "");
                            }
                            else
                                obj.Iof = !string.IsNullOrWhiteSpace(obj.Iof) && obj.Iof != "0" ? obj.Iof : "0";

                            if (_arrayLinha.Any(n => n.Contains("dtInc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("dtInc"));
                                obj.DataInclusao = Regex.Replace(_arrayLinha[_id].Replace("dtInc", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Emp")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Emp"));
                                obj.Empreendimento = Regex.Replace(_arrayLinha[_id].Replace("Emp", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Agencia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Agencia"));
                                obj.Agencia = Regex.Replace(_arrayLinha[_id].Replace("Agencia", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            break;
                        }
                    case 19:
                        {
                            _case = "19 - Metodo: TrataCabecalho -  campo: Correção, DataInclusão...";

                            if (_arrayLinha.Any(n => n.Contains("Correcao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Correcao"));
                                obj.Correcao = _arrayLinha[_id].Replace("Correcao", "").Trim();
                            }
                            if (_arrayLinha.Any(n => n.Contains("DtInc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtInc"));
                                obj.DataInclusao = Regex.Replace(_arrayLinha[_id].Replace("DtInc", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("DtReInc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtReInc"));
                                obj.DataReinclusao = Regex.Replace(_arrayLinha[_id].Replace("DtReInc", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("DtUltAlter")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtUltAlter"));
                                obj.DataUltimaAlteracao = Regex.Replace(_arrayLinha[_id].Replace("DtUltAlter", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Apolice")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Apolice"));
                                obj.Apolice = Regex.Replace(_arrayLinha[_id].Replace("Apolice", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            break;
                        }
                    case 20:
                        {
                            _case = "20 - Metodo: TrataCabecalho -  campo: TipoFinancimento, DataAlteracao...";

                            if (_arrayLinha.Any(n => n.Contains("TipoFinanc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TipoFinanc"));
                                obj.TipoFinanciamento = _arrayLinha[_id].Replace("TipoFinanc", "").Trim();
                            }
                            if (_arrayLinha.Any(n => n.Contains("DtUltAlter")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtUltAlter"));
                                obj.DataUltimaAlteracao = Regex.Replace(_arrayLinha[_id].Replace("DtUltAlter", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("DtReInc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtReInc"));
                                obj.DataReinclusao = Regex.Replace(_arrayLinha[_id].Replace("DtReInc", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            break;
                        }

                    case 21:
                        {
                            _case = "21 - Metodo: TrataCabecalho -  campo: TipoOrigem, DataUltimaAlteracao...";

                            if (_arrayLinha.Any(n => n.Contains("TipoOrig")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TipoOrig"));
                                obj.TipoOrigem = _arrayLinha[_id].Replace("TipoOrig", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("DtUltAlter")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtUltAlter"));
                                obj.DataUltimaAlteracao = Regex.Replace(_arrayLinha[_id].Replace("DtUltAlter", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            break;
                        }
                    case 23:
                        {
                            _case = "23 - Metodo: TrataCabecalho -  campo: Repactuação...";

                            if (_arrayLinha.Any(n => n.Contains("Repac")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Repac"));
                                obj.Repactuacao = Regex.Replace(_arrayLinha[_id].Replace("Repac", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Emp")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Emp"));
                                obj.Empreendimento = Regex.Replace(_arrayLinha[_id].Replace("Emp", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("VlrGarantia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("VlrGarantia"));
                                obj.ValorGarantia = Regex.Replace(_arrayLinha[_id].Replace("VlrGarantia", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Agencia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Agencia"));
                                obj.Agencia = Regex.Replace(_arrayLinha[_id].Replace("Agencia", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("TAXA")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TAXA"));
                                obj.Taxa = Regex.Replace(_arrayLinha[_id].Replace("TAXA", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Razao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Razao"));
                                obj.Razao = Regex.Replace(_arrayLinha[_id].Replace("Razao", "").Trim(), @"[^0-9\/$]+", "");
                            }

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
            string _case = string.Empty;
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
                            _case = "3 - Metodo: TrataLinhaPDFPadrao2 -  campo: endereço de imovel";

                            string _bairro = string.Empty, _cep = string.Empty, _cidade = string.Empty, _endereco = string.Empty, _uf = string.Empty;
                            var arr = _linha.Trim().Split(' ').ToList();

                            int p = arr.FindIndex(u => Regex.IsMatch(u, ("[0-9]{2}.[0-9]{3}-[0-9]{3}")));

                            List<string> lstBairo = new List<string>();
                            var j = arr.GetRange(0, p);
                            var m = arr.Skip(p).ToList();

                            _cep = m[0];
                            m.RemoveAt(0);
                            _uf = m.Last();
                            _cidade = string.Join(" ", m.ToArray());

                            for (int k = (j.Count - 1); k > 0; k--)
                            {
                                if (!string.IsNullOrWhiteSpace(j[k]))
                                    if (char.IsLetter(j[k], 0) && j[k].Length > 1)
                                    {
                                        lstBairo.Add(j[k]);
                                        j.RemoveAt(k);
                                    }
                                    else { break; }
                            }

                            _endereco = string.Join(" ", j.ToArray());
                            lstBairo.Reverse();
                            _bairro = string.Join(" ", lstBairo.ToArray());

                            _bairro = string.IsNullOrWhiteSpace(_bairro) ? _cidade : _bairro;
                            _endereco = _endereco.Length > 80 ? _endereco.Substring(0, 79) : _endereco;
                            _arrayLinha = new List<string>() { _endereco, _bairro, _cidade, _uf, _cep }.ToArray();

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

                            string _contrato = _arrayLinha.First(u => Regex.IsMatch(u, @"(^\d{4}.\d{5}.\d{3}-\d{1}$)")) + "*" + _arrayLinha.First(u => Regex.IsMatch(u, @"(^\d{4}$)"));

                            obj.Contrato = Regex.Replace(_contrato.Split('*')[0], @"[^0-9$]+", "");
                            obj.Carteira = Regex.Replace(_contrato.Split('*')[1], @"[^0-9$]+", "");
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
                            obj.BairroImovel = _endImovel.Length > 24 ? _endImovel.Substring(0, 24) : _endImovel;
                            obj.CidadeImovel = _arrayLinha[2].Trim();
                            obj.UfImovel = _arrayLinha[3].Trim();
                            obj.CepImovel = Regex.Replace(_arrayLinha[4].Trim(), @"[^0-9\-$]", "");

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
                            obj.CodigoContabil = Regex.Replace(_arrayLinha[(_arrayLinha.Length == 3 ? 2 : 3)].Trim(), @"[^0-9$]+", "");
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

        public Cabecalho PreencheCabecalho(Cabecalho obj)
        {
            return new Cabecalho()
            {
                Agencia = string.IsNullOrWhiteSpace(obj.Agencia) ? "" : obj.Agencia,
                Apolice = string.IsNullOrWhiteSpace(obj.Apolice) ? "" : obj.Apolice,
                BairroImovel = string.IsNullOrWhiteSpace(obj.BairroImovel) ? "" : obj.BairroImovel,
                CAC = string.IsNullOrWhiteSpace(obj.CAC) ? "" : obj.CAC,
                Carencia = string.IsNullOrWhiteSpace(obj.Carencia) ? "" : obj.Carencia,
                Carteira = string.IsNullOrWhiteSpace(obj.Carteira) ? "" : obj.Carteira,
                Cartorio = string.IsNullOrWhiteSpace(obj.Cartorio) ? "" : obj.Cartorio,
                Categoria = string.IsNullOrWhiteSpace(obj.Categoria) ? "" : obj.Categoria,
                CepImovel = string.IsNullOrWhiteSpace(obj.CepImovel) ? "" : obj.CepImovel,
                CidadeImovel = string.IsNullOrWhiteSpace(obj.CidadeImovel) ? "" : obj.CidadeImovel,
                Cliente = string.IsNullOrWhiteSpace(obj.Cliente) ? "" : obj.Cliente,
                CodigoContabil = string.IsNullOrWhiteSpace(obj.CodigoContabil) ? "" : obj.CodigoContabil,
                Comissao = string.IsNullOrWhiteSpace(obj.Comissao) ? "" : obj.Comissao,
                ContaDeposito = string.IsNullOrWhiteSpace(obj.ContaDeposito) ? "" : obj.ContaDeposito,
                Contrato = string.IsNullOrWhiteSpace(obj.Contrato) ? "" : obj.Contrato,
                Correcao = string.IsNullOrWhiteSpace(obj.Correcao) ? "" : obj.Correcao,
                Cpf = string.IsNullOrWhiteSpace(obj.Cpf) ? "" : obj.Cpf,
                DataBase = string.IsNullOrWhiteSpace(obj.DataBase) ? "" : obj.DataBase,
                DataCaDoc = string.IsNullOrWhiteSpace(obj.DataCaDoc) ? "" : obj.DataCaDoc,
                DataContrato = string.IsNullOrWhiteSpace(obj.DataContrato) ? "" : obj.DataContrato,
                DataEmicao = string.IsNullOrWhiteSpace(obj.DataEmicao) ? "" : obj.DataEmicao,
                DataGarantia = string.IsNullOrWhiteSpace(obj.DataGarantia) ? "" : obj.DataGarantia,
                DataInclusao = string.IsNullOrWhiteSpace(obj.DataInclusao) ? "" : obj.DataInclusao,
                DataNascimento = string.IsNullOrWhiteSpace(obj.DataNascimento) ? "" : obj.DataNascimento,
                DataPrimeiroVencimento = string.IsNullOrWhiteSpace(obj.DataPrimeiroVencimento) ? "" : obj.DataPrimeiroVencimento,
                DataReinclusao = string.IsNullOrWhiteSpace(obj.DataReinclusao) ? "" : obj.DataReinclusao,
                DataUltimaAlteracao = string.IsNullOrWhiteSpace(obj.DataUltimaAlteracao) ? "" : obj.DataUltimaAlteracao,
                Empreendimento = string.IsNullOrWhiteSpace(obj.Empreendimento) ? "" : obj.Empreendimento,
                EnderecoImovel = string.IsNullOrWhiteSpace(obj.EnderecoImovel) ? "" : obj.EnderecoImovel,
                FgtsUtilizado = string.IsNullOrWhiteSpace(obj.FgtsUtilizado) ? "" : obj.FgtsUtilizado,
                Id = obj.Id,
                Modalidade = string.IsNullOrWhiteSpace(obj.Modalidade) ? "" : obj.Modalidade,
                Nome = string.IsNullOrWhiteSpace(obj.Nome) ? "" : obj.Nome,
                Numero = string.IsNullOrWhiteSpace(obj.Numero) ? "" : obj.Numero,
                OrigemRecurso = string.IsNullOrWhiteSpace(obj.OrigemRecurso) ? "" : obj.OrigemRecurso,
                Pis = string.IsNullOrWhiteSpace(obj.Pis) ? "0" : obj.Pis,
                Plano = string.IsNullOrWhiteSpace(obj.Plano) ? "" : obj.Plano,
                Prazo = string.IsNullOrWhiteSpace(obj.Prazo) ? "" : obj.Prazo,
                Prestacao = string.IsNullOrWhiteSpace(obj.Prestacao) ? "" : obj.Prestacao,
                Razao = string.IsNullOrWhiteSpace(obj.Razao) ? "" : obj.Razao,
                Reajuste = string.IsNullOrWhiteSpace(obj.Reajuste) ? "" : obj.Reajuste,
                Repactuacao = string.IsNullOrWhiteSpace(obj.Repactuacao) ? "0" : obj.Repactuacao,
                SeguroDFI = string.IsNullOrWhiteSpace(obj.SeguroDFI) ? "" : obj.SeguroDFI,
                SeguroMIP = string.IsNullOrWhiteSpace(obj.SeguroMIP) ? "" : obj.SeguroMIP,
                Sistema = string.IsNullOrWhiteSpace(obj.Sistema) ? "" : obj.Sistema,
                Situacao = string.IsNullOrWhiteSpace(obj.Situacao) ? "" : obj.Situacao,
                Taxa = string.IsNullOrWhiteSpace(obj.Taxa) ? "" : obj.Taxa,
                TaxaJuros = string.IsNullOrWhiteSpace(obj.TaxaJuros) ? "" : obj.TaxaJuros,
                TelefoneComercial = string.IsNullOrWhiteSpace(obj.TelefoneComercial) ? "0" : obj.TelefoneComercial,
                TelefoneResidencia = string.IsNullOrWhiteSpace(obj.TelefoneResidencia) ? "0" : obj.TelefoneResidencia,
                TipoFinanciamento = string.IsNullOrWhiteSpace(obj.TipoFinanciamento) ? "Contrato Normal" : obj.TipoFinanciamento,
                TipoOrigem = string.IsNullOrWhiteSpace(obj.TipoOrigem) ? "" : obj.TipoOrigem,
                TxCEMes = string.IsNullOrWhiteSpace(obj.TxCEMes) ? "" : obj.TxCEMes,
                TxCETAno = string.IsNullOrWhiteSpace(obj.TxCETAno) ? "" : obj.TxCETAno,
                UfImovel = string.IsNullOrWhiteSpace(obj.UfImovel) ? "" : obj.UfImovel,
                ValorFinanciamento = string.IsNullOrWhiteSpace(obj.ValorFinanciamento) ? "" : obj.ValorFinanciamento,
                ValorGarantia = string.IsNullOrWhiteSpace(obj.ValorGarantia) ? "" : obj.ValorGarantia,
                Iof = string.IsNullOrWhiteSpace(obj.Iof) ? "0" : obj.Iof,
            };
        }

        public void PopulaContrato(List<ContratoPdf> lstContratosPdf, List<string> lstGT, string _diretorioDestino, string _diretorioOrigem)
        {

#if DEBUG
            _diretorioDestino = @"D:\PDFSTombamento\txt";
            _diretorioOrigem = @"D:\PDFSTombamento\";

#endif


            string strAlta = string.Empty;
            string _contratoGT = string.Empty;
            //  string _apolice = string.Empty, _taxaJuros = string.Empty, _prazo = string.Empty, _reajuste = string.Empty, _valorgarantia = string.Empty;

            //   _apolice = _taxaJuros = _prazo = _reajuste = _valorgarantia = string.Empty;


            #region BLOCO QUE GERA O ARQUIVO DE CONTRATO
            //======================= BLOCO QUE GERA O ARQUIVO DE CONTRATO================================================
            using (StreamWriter escreverContrato = new StreamWriter(_diretorioDestino + @"\TL16CONT.txt", true, Encoding.UTF8))
            {
                Cabecalho c = null;
                foreach (ContratoPdf item in lstContratosPdf)
                {
                    try
                    {
                        strAlta = string.Empty;

                    //q.Cabecalhos.ForEach(l =>
                    //{
                    //    if (!l.TaxaJuros.Trim().Equals(_taxaJuros))
                    //        _taxaJuros = l.TaxaJuros;
                    //    if (!l.Apolice.Trim().Equals(_apolice))
                    //        _apolice = l.Apolice;
                    //    if (!l.Prazo.Trim().Equals(_prazo))
                    //        _prazo = l.Prazo;
                    //    if (!l.Reajuste.Trim().Equals(_reajuste))
                    //        _reajuste = l.Reajuste;
                    //});

                        c = item.Cabecalhos.LastOrDefault();

                        _contratoGT = lstGT.Find(p => p.Equals(c.Carteira.Substring(2, 2) + c.Contrato.Trim()));

                        if (!string.IsNullOrWhiteSpace(_contratoGT))
                        {
                            strAlta = string.Format("{0}{1}{2}", c.Carteira.Trim().Substring(2), c.Contrato.Trim(), c.DataPrimeiroVencimento.Trim().Substring(0, 2)).PadRight(24, ' ');
                            strAlta += string.Format("{0}{1}{2}{3}", (c.Nome.Trim().Length > 40 ? c.Nome.Trim().Substring(0, 40) : c.Nome.Trim()).PadRight(40, ' '), c.DataNascimento.Trim().PadRight(10, ' '), "".PadRight(14, ' '), c.EnderecoImovel.Trim().PadRight(80, ' '));
                            strAlta += string.Format("{0}{1}{2}{3}", c.Cpf.Trim().PadRight(14, ' '), "".PadRight(3, ' '), _contratoGT.Trim().PadRight(20, ' '), "".PadRight(20, ' '));
                            strAlta += string.Format("{0}{1}", c.Modalidade.Trim().PadRight(40, ' '), (c.CidadeImovel.Trim().Length <= 31 ? c.CidadeImovel.Trim() : c.CidadeImovel.Trim().Substring(0, 31)).PadRight(31, ' '));
                            strAlta += string.Format("{0}{1}{2}", c.Plano.Trim().PadRight(10, ' '), c.DataContrato.Trim().PadRight(10, ' '), "".PadRight(2, ' '));
                            strAlta += string.Format("{0}{1}", c.Prestacao.Trim().PadLeft(18, '0'), c.Sistema.Trim().PadRight(2, ' '));
                            strAlta += string.Format("{0}{1}{2}", c.ValorFinanciamento.Trim().PadLeft(18, '0'), c.CodigoContabil.Trim().Substring(1).PadRight(15, '0'), c.SeguroMIP.Trim().PadLeft(18, '0'));
                        //strAlta += string.Format("{0}{1}", (string.IsNullOrWhiteSpace(_reajuste) ? c.Reajuste.Trim() : _reajuste).PadRight(10, ' '), c.DataGarantia.Trim().PadRight(18, ' '));
                        strAlta += string.Format("{0}{1}", c.Reajuste.Trim().PadRight(10, ' '), c.DataGarantia.Trim().PadRight(18, ' '));
                            strAlta += string.Format("{0}", c.SeguroDFI.Trim().PadLeft(18, '0'));
                        //strAlta += string.Format("{0}{1}{2}", (string.IsNullOrWhiteSpace(_prazo) ? c.Prazo.Trim() : _prazo).PadLeft(5, '0'), c.ValorGarantia.Trim().PadLeft(18, '0'), "".PadRight(8, ' '));
                        strAlta += string.Format("{0}{1}{2}", c.Prazo.Trim().PadLeft(5, '0'), c.ValorGarantia.Trim().PadLeft(18, '0'), "".PadRight(8, ' '));
                        //strAlta += string.Format("{0}{1}", "0".PadRight(18, '0'), (string.IsNullOrWhiteSpace(_taxaJuros) ? c.TaxaJuros.Trim() : _taxaJuros).Trim().PadLeft(11, '0'));
                        strAlta += string.Format("{0}{1}", "0".PadRight(18, '0'), c.TaxaJuros.Trim().PadLeft(11, '0'));
                        //strAlta += string.Format("{0}{1}{2}", c.DataPrimeiroVencimento.Trim().Substring(0, 10).PadLeft(10, '0'), (string.IsNullOrWhiteSpace(_apolice) ? c.Apolice.Trim() : _apolice).PadLeft(6, '0'), (c.CepImovel.Trim() + "-" + (c.BairroImovel.Trim().Length > 24 ? c.BairroImovel.Trim().Substring(0, 24) : c.BairroImovel.Trim())).PadRight(34, ' '));
                        strAlta += string.Format("{0}{1}{2}", c.DataPrimeiroVencimento.Trim().Substring(0, 10).PadLeft(10, '0'), c.Apolice.Trim().PadLeft(6, '0'), (c.CepImovel.Trim() + "-" + (c.BairroImovel.Trim().Length > 24 ? c.BairroImovel.Trim().Substring(0, 24) : c.BairroImovel.Trim())).PadRight(34, ' '));
                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadRight(11, '0'), c.Correcao.Trim().PadRight(10, ' '), "0".PadLeft(2, '0'), c.Razao.Trim().PadLeft(18, '0'));
                            strAlta += string.Format("{0}{1}", "0".PadLeft(2, '0'), c.Situacao.Trim()).PadRight(60, ' ');
                            c = null;
                            escreverContrato.WriteLine(strAlta);

                            strAlta = string.Empty;
                        }
                        else
                        {
                            ExceptionError.NovoContratoGT(c.Contrato, _diretorioOrigem);
                        }
                    }
                    catch (Exception ex)
                    {
                        string _detalhes = "Metodo: PopulaContrato - Classe: BusinessCabecalho - Ação: tentativa de gerear o registro no arquivo TL16CONT.txt";
                        ExceptionError.TrataErros(ex, c.Contrato, _detalhes, _diretorioOrigem);
                    }
                    
                }
            }
            //=============================================================================================================
            #endregion

            #region BLOCO QUE GERA O ARQUIVO DE OCORRENCIA E PARCELAS
            //======================= BLOCO QUE GERA O ARQUIVO DE OCORRENCIA================================================

            using (StreamWriter escreverOcorrencia = new StreamWriter(_diretorioDestino + @"\TL16OCOR.txt", true, Encoding.UTF8))
            {
                string _novaOcorrencia = string.Empty;
                strAlta = string.Empty;
                bool _consistencia = false;
                Parcela _parcela = null;
                Cabecalho _cabecalho = null, _cabecalhoAnterior = null;
                List<string> lstTipoOcorrencia = new List<string>() { "004", "005", "010" };

                lstContratosPdf.ForEach(q =>
                {
                    string _datavencimentoAnterior = string.Empty;
                    strAlta = string.Empty;

                    q.Ocorrencias.ForEach(o =>
                    {
                        try
                        {
                            _parcela = q.Parcelas.Find(m => m.Id == o.IdParcela);

                            _datavencimentoAnterior = _parcela.Vencimento;

                            strAlta = string.Empty;

                            if (_parcela == null)
                                _cabecalho = q.Cabecalhos.Find(k => k.Id == o.IdCabecalho);
                            else
                                _cabecalho = q.Cabecalhos.Find(k => k.Id == _parcela.IdCabecalho);

                            if (q.Cabecalhos.Count > 1)
                            {
                                if (q.Cabecalhos.Any(a => a.Id == (_parcela == null ? (o.IdCabecalho + 1) : (_parcela.IdCabecalho + 1))))
                                    _cabecalhoAnterior = q.Cabecalhos.Find(xx => xx.Id == (_parcela == null ? (o.IdCabecalho + 1) : (_parcela.IdCabecalho + 1)));
                                else
                                    _cabecalhoAnterior = q.Cabecalhos.Find(xx => xx.Id == (_parcela == null ? (o.IdCabecalho - 1) : (_parcela.IdCabecalho - 1)));
                            }
                            else
                                _cabecalho = _cabecalhoAnterior = q.Cabecalhos.Find(k => k.Id == (_parcela == null ? (o.IdCabecalho) : (_parcela.IdCabecalho)));

                            strAlta = string.Format("{0}", (q.Carteira.Substring(2, 2) + o.Contrato).PadRight(15, '0'));
                            strAlta += string.Format("{0}{1}", o.Vencimento.Trim().Trim().PadRight(10, '0'), o.Pagamento.Trim().Trim().PadRight(10, '0'));
                            strAlta += string.Format("{0}", o.Descricao.Trim().PadRight(36, ' '));

                            _novaOcorrencia = strAlta;

                            if (o.CodigoOcorrencia.Equals("020")) //Amortização extra
                            {
                                strAlta += string.Format("{0}{1}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_cabecalho.DataGarantia).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("021")) //Amortização rec. Fgts
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                strAlta += string.Format("{0}{1}", o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}{1}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _datavencimentoAnterior)).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                strAlta += string.Format("{0}", _parcela.Dump.Trim().PadRight(30, ' '));
                            }

                            if (lstTipoOcorrencia.Any(t => t.Equals(o.CodigoOcorrencia.Trim()))) // Alteração Contratual, Alteração de garantia, Mudança dia vencimento
                            {
                                if (o.CodigoOcorrencia.Equals("010"))
                                {
                                    bool hasDiferenca = false; //  variavel que setada como true caso entre nos blocos de IF de taxa de juros, apolice e prazo

                                    if (!_cabecalhoAnterior.TaxaJuros.Equals(_cabecalho.TaxaJuros)) // TAXA DE JUROS")
                                    {
                                        hasDiferenca = true;
                                        _consistencia = true;

                                        strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), "TAXA JUROS".PadRight(30, ' '));
                                        strAlta += string.Format("{0}{1}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '), _cabecalhoAnterior.TaxaJuros.Trim().PadRight(30, ' '));

                                        if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                            strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                        else
                                            strAlta += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : o.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));

                                        escreverOcorrencia.WriteLine(strAlta);
                                        strAlta = string.Empty;
                                    }


                                    if (!_cabecalhoAnterior.Apolice.Equals(_cabecalho.Apolice)) // APOLICE
                                    {
                                        hasDiferenca = true;
                                        strAlta = _novaOcorrencia;

                                        strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), "APOLICE".PadRight(30, ' '));
                                        strAlta += string.Format("{0}{1}", _cabecalho.Apolice.Trim().PadRight(30, ' '), _cabecalhoAnterior.Apolice.Trim().PadRight(30, ' '));

                                        if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                            strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                        else
                                            strAlta += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _datavencimentoAnterior)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));

                                        escreverOcorrencia.WriteLine(strAlta);
                                        strAlta = string.Empty;
                                    }

                                    if (!_cabecalhoAnterior.Prazo.Equals(_cabecalho.Prazo)) // PRAZO
                                    {
                                        strAlta = _novaOcorrencia;

                                        hasDiferenca = true;
                                        if (_consistencia)
                                        {
                                            strAlta = string.Format("{0}", (q.Carteira.Substring(2, 2) + o.Contrato).PadRight(15, '0'));
                                            strAlta += string.Format("{0}{1}", o.Vencimento.Trim().Trim().PadRight(10, '0'), o.Pagamento.Trim().Trim().PadRight(10, '0'));
                                            strAlta += string.Format("{0}", o.Descricao.Trim().PadRight(36, ' '));
                                        }

                                        strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), "PRAZO".PadRight(30, ' '));
                                        strAlta += string.Format("{0}{1}", _cabecalho.Prazo.Trim().PadRight(30, ' '), _cabecalhoAnterior.Prazo.Trim().PadRight(30, ' '));

                                        if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                            strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                        else
                                            strAlta += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _datavencimentoAnterior)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));

                                        escreverOcorrencia.WriteLine(strAlta);
                                        strAlta = string.Empty;
                                    }

                                    if (!_cabecalhoAnterior.DataPrimeiroVencimento.Equals(_cabecalho.DataPrimeiroVencimento)) // ALTERAÇÃO NA DATA DO 1º. VENCIMENTO DA PARCELA
                                    {
                                        strAlta = _novaOcorrencia;
                                        hasDiferenca = true;
                                        strAlta += string.Format("{0}{1}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                        strAlta += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _datavencimentoAnterior)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                        strAlta += string.Format("{0}", _cabecalho.DataPrimeiroVencimento.PadRight(30, ' '));
                                        strAlta += string.Format("{0}", _cabecalhoAnterior.DataPrimeiroVencimento.Trim().PadRight(30, ' '));

                                        escreverOcorrencia.WriteLine(strAlta);
                                        strAlta = string.Empty;
                                    }

                                    if (!hasDiferenca)
                                    {
                                        strAlta = string.Empty;
                                        return;
                                    }
                                }

                                if (o.CodigoOcorrencia.Equals("005")) // Alteração de garantia
                                {
                                    if (!_cabecalhoAnterior.ValorGarantia.Equals(_cabecalho.ValorGarantia)) // GARATNTIA
                                    {
                                        strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), "VRGARANTIA".PadRight(30, ' '));
                                        strAlta += string.Format("{0}{1}{2}", _cabecalho.ValorGarantia.Trim().PadLeft(18, '0'), "".PadRight(12, ' '), _cabecalhoAnterior.ValorGarantia.Trim().PadLeft(18, '0'));

                                        if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                            strAlta += string.Format("{0}{1}", "".PadRight(12, ' '), _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                        else
                                            strAlta += string.Format("{0}{1}", "".PadRight(12, ' '), Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _datavencimentoAnterior)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                    }
                                    else
                                    {
                                        strAlta = string.Empty;
                                        _parcela = null;

                                    }
                                }

                                if (o.CodigoOcorrencia.Equals("004"))//Mudança dia vencimento")
                                {
                                    if (!_cabecalhoAnterior.DataPrimeiroVencimento.Equals(_cabecalho.DataPrimeiroVencimento))
                                    {
                                        strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                        strAlta += string.Format("{0}{1}", _cabecalho.DataPrimeiroVencimento.Trim().PadRight(30, ' '), _cabecalhoAnterior.DataPrimeiroVencimento.Trim().PadRight(30, ' '));
                                        strAlta += string.Format("{0}", "".PadLeft(30, ' '));
                                    }
                                    else
                                    {
                                        strAlta = string.Empty;
                                        _parcela = null;
                                    }
                                }
                            }

                            if (o.CodigoOcorrencia.Equals("022")) // Sinistro parcial
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("030")) // Incorporação no saldo
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("031")) // Consolidação da divida
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("032")) // Incorporação juros
                            {
                                _cabecalho = q.Cabecalhos[0];
                                _cabecalho.DataPrimeiroVencimento = ValidaData(_cabecalho.DataPrimeiroVencimento);

                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_cabecalho.DataPrimeiroVencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("040")) // Transferencia
                            {
                                _cabecalho = q.Cabecalhos[0];

                                _cabecalho.DataPrimeiroVencimento = ValidaData(_cabecalho.DataPrimeiroVencimento);

                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_cabecalho.DataPrimeiroVencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("044")) // Transf.Parte ideal
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(o.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("051")) //Liguidaçao rec. Fgts
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                strAlta += string.Format("{0}{1}", o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}{1}", Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                strAlta += string.Format("{0}", _parcela.Dump.Trim().PadRight(30, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("050")) // Liquidação Antecipada
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("054")) // Liquidação Coobrigado
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                            }

                            if (o.CodigoOcorrencia.Equals("058")) // Liquidação Interveniencia
                            {
                                strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Trim().PadLeft(18, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                strAlta += string.Format("{0}", Convert.ToDateTime(_datavencimentoAnterior).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                            }

                            strAlta = strAlta.PadRight(281, ' ');

                            if (!string.IsNullOrWhiteSpace(strAlta))
                                escreverOcorrencia.WriteLine(strAlta);

                            _parcela = null;
                            _consistencia = false;
                            strAlta = string.Empty;
                        }
                        catch (Exception exOc)
                        {
                            ExceptionError.TrataErros(exOc, q.Contrato, "Metodo: PopulaContrato - Classe: BusinessCabecalho - Ação: Erro gerado ao tentar escrever a Ocorrencia no arquivo TL16OCOR.txt", _diretorioOrigem);
                        }
                    });

                });
                //=============================================================================================================



                #region BLOCO QUE GERA O ARQUIVO DE PARCELAS
                //======================= BLOCO QUE GERA O ARQUIVO DE PARCELAS ==================================================
                strAlta = string.Empty;
                using (StreamWriter escreverParcelas = new StreamWriter(_diretorioDestino + @"\TL16PARC.txt", true, Encoding.UTF8))
                {
                    lstContratosPdf.ForEach(q =>
                    {
                        strAlta = string.Empty;

                        q.Parcelas.ForEach(p =>
                        {
                            try
                            {
                                strAlta = string.Empty;
                                if (!p.IsAnt)
                                {
                                    strAlta = string.Format("{0}", (q.Carteira.Substring(2, 2) + q.Contrato).PadRight(15, '0'));
                                    strAlta += string.Format("{0}{1}", p.Vencimento.Trim().PadLeft(10, '0'), p.Indice.Trim().PadRight(7, '0'));
                                    strAlta += string.Format("{0}{1}", (p.Pagamento.Trim().Equals("01/01/0001") ? "" : p.Pagamento.Trim()).PadLeft(10, ' '), p.NumeroPrazo.Trim().PadLeft(3, '0'));
                                    strAlta += string.Format("{0}{1}", p.Prestacao.Trim().PadLeft(18, '0'), p.Seguro.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.Taxa.Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.AmortizacaoCorrecao.Trim().PadLeft(17, '0'), "+" + p.SaldoDevedorCorrecao.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.Encargo.Trim().PadLeft(18, '0'), p.Pago.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.Juros.Trim().PadLeft(18, '0'), p.Mora.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.Amortizacao.Trim().PadLeft(17, '0'), "+" + p.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(4, '0'), "0".PadLeft(7, '0'), "0".PadLeft(3, '0'));
                                    strAlta += string.Format("{0}{1}", p.Proc_Emi_Pag.Trim().PadLeft(20, '0'), "0".PadLeft(15, '0'));

                                    escreverParcelas.WriteLine(strAlta);
                                    strAlta = string.Empty;
                                }
                            }
                            catch (Exception exp)
                            {
                                ExceptionError.TrataErros(exp, q.Contrato, "Metodo: PopulaContrato - Classe: BusinessCabecalho - Ação: Erro gerado ao tentar escrever a parcela no arquivo TL16PARC.txt", _diretorioOrigem);
                            }
                        });
                        strAlta = string.Empty;
                    });

                }
                //===============================================================================================================
                #endregion
                strAlta = string.Empty;
            }
            #endregion

            #region BLOCO QUE GERA O ARQUIVO DE PONTEIRO
            strAlta = string.Empty;
            //======================= BLOCO QUE GERA O ARQUIVO DE PARCELAS ===================================================
            using (StreamWriter escreveArquiPont = new StreamWriter(_diretorioDestino + @"\ARQUPONT.txt", true, Encoding.UTF8))
            {
                lstContratosPdf.ForEach(p =>
                {
                    escreveArquiPont.WriteLine(string.Format("01{0}1", p.Contrato));
                });
            }
            #endregion

        }


        public string ValidaData(string _data)
        {
            string[] _arrayData = _data.Split('/');
            string _novaData = string.Empty;

            int _dia = Convert.ToInt32(_arrayData[0]);
            int _mes = Convert.ToInt32(_arrayData[1]);
            int _ano = Convert.ToInt32(_arrayData[2]);

            if (_dia > DateTime.DaysInMonth(_ano, _mes))
            {
                _novaData = string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(_ano, _mes).ToString(), _arrayData[1], _arrayData[2]);
            }
            return string.IsNullOrWhiteSpace(_novaData) ? _data : _novaData;
        }

    }

}
