using System;
using System.Collections.Generic;
using System.Data;
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
                        _linha = _linha = Regex.Replace(_linha.Replace("Nº", ":").Replace("Data Base", ""), @"[^\wÀ-úa-zA-Z0-9:\/$]+", " ");
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

                        _arrayLinha = _linha.Split(' ');
                        var x = TrataArray(_linha).ToList();

                        _cpf = x.Find(y => Regex.IsMatch(y, @"(\d{3}.\d{3}.\d{3}\-\d{2})"));
                        x.Remove(_cpf);
                        _dataNascimento = x.Last();
                        x.Remove(_dataNascimento);

                        x.RemoveRange((x.Count - 3), 3);

                        _nome = string.Join(" ", x.Where(f => !string.IsNullOrWhiteSpace(f)));
                        _arrayLinha = new string[] { _nome, _cpf, _dataNascimento };

                        break;
                    }
                case 90:
                    {
                        string _cnpj = string.Empty, _nome = string.Empty ;

                        _arrayLinha = _linha.Split(' ');
                        var x = TrataArray(_linha).ToList();

                        _cnpj = x.FirstOrDefault(y => Regex.IsMatch(y, @"(^\d{3}.\d{3}.\d{3}\/\d{4}\-\d{2}$)"));
                        x.Remove(_cnpj);

                        _nome = string.Join(" ", x.Where(f => !string.IsNullOrWhiteSpace(f)));
                        _arrayLinha = new string[] { _nome, _cnpj, "01/01/0001" };

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
                        var lstItens = _arrayLinha.ToList();

                        lstItens.ForEach(k => { k =  Regex.Replace(k, @"[^A-ZÀ-Ú0-9.\-$]", " "); });

                        _arrayLinha = lstItens.ToArray();
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

                case 100:
                    {
                        _linha = Regex.Replace(_linha, @"[^0-9a-zà-úÀ-ÙA-Z\/.,\-]+", " ");
                        _arrayLinha = _linha
                                            .Replace("Plano", ":Plano").Trim()
                                            .Replace("Data Contrato", ":DtContrato").Trim()
                                            .Replace("COMISSÃO", ":Comisao").Trim()
                                            .Replace("Taxa Servico", ":TaxaServico").Trim()
                                            .Replace("Lastro", ":Lastro").Trim()
                                            .Replace("FGTS.UTILIZADO", ":FgtsUtil").Trim()
                                            .Replace("DESC.AQUISIÇÃO", ":DESCARQUISICAO")
                                            .Replace("Origem de Recursos", ":OriRec").Trim()
                                            .Replace("Prestação", ":Prestacao").Trim()
                                            .Replace("Seguro a vista", ":SegVis").Trim()
                                            .Replace("CUSTAS", ":Custas").Trim()
                                            .Replace("ÁGIO", ":Agio").Trim()
                                            .Replace("Repactuação", ":Repac").Replace("REP", "00").Trim()
                                            .Replace("Valor Garanta", ":VlrGarantia").Trim()
                                            .Replace("Agência", ":Agencia").Trim()
                                            .Replace("TAXA", ":TAXA").Trim()
                                            .Replace("Razão", ":Razao").Trim()
                                            .Replace("IOF.SEG", ":SEG").Trim()
                                            .Replace("Tipo de Origem", ":TipoOrig").Trim()
                                            .Replace("Data Ult. Alteração", ":DtUltAlter").Trim()
                                            .Replace("Tipo Financiamento", ":TipoFinanc").Trim()
                                            .Replace("Data Re-Inclusão", ":DtReInc").Trim()
                                            .Replace("Correção", ":Correcao").Trim()
                                            .Replace("Taxa Juros", ":TaxaJuros").Trim()
                                            .Replace("Data 1 Vencimento", ":Data1v").Trim()
                                            .Replace("Código Contábil", ":Contabil").Trim()
                                            .Replace("Empreendimento", ":Emp").Trim()
                                            .Replace("Apólice", ":aplc").Trim()
                                            .Replace("Data Inclusao", ":DtInc").Trim()
                                            .Replace("Prorrogação", "Prorrogacao").Trim()
                                            .Replace("Prazo", ":Prazo").Trim()
                                            .Replace("Carência", ":Carencia").Trim()
                                            .Replace("REMUN.DIF.JUROS", ":RemunDifJuros").Trim()
                                            .Replace("Data Garanta", ":DtGarantia")
                                            .Replace("Seguro DFI", ":SeguroDFi").Trim()
                                            .Replace("Sistema", ":Sistema").Trim()
                                            .Replace("Valor Financiamento", ":VlrFinan").Trim()
                                            .Replace("REMUNERAÇÃO", ":Remuneracao").Trim()
                                            .Replace("Seguro MIP", ":SeguroMIP").Trim()
                                            .Replace("Reajuste",":Reaj")

                                            .Trim().Split(':')
                                            .Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();

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
                            obj.DataBase = _arrayLinha[1].Trim().Substring(0, 10);
                            break;
                        }
                    case 7:
                        {
                            _case = "7 - Metodo: TrataCabecalho -  campo: DataEmicao";

                            if(!Regex.IsMatch(obj.DataEmicao, @"(^\d{2}\/\d{2}\/\d{4}$)"))
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
                    case 90:
                        {
                            _case = "9 - Metodo: TrataCabecalho -  campo: Nome, CPF, DataNascimento";

                            obj.Nome = Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Z$]+", " ");
                            obj.Cpf = Regex.Replace(_arrayLinha[1].Trim(), @"[^0-9$]+", "");
                            obj.DataNascimento = "01/01/0001";
                            break;
                        }
                    case 10:
                        {
                            _case = "10 - Metodo: TrataCabecalho -  campo: EncdrecoImovel, CEP, Bairro...";

                            string _bairroImovel = Regex.Replace(_arrayLinha[1].Trim(), @"[^A-Z0-9$]+", " ");

                            obj.EnderecoImovel = _arrayLinha.Length > 0 ? Regex.Replace(_arrayLinha[0].Trim(), @"[^A-Za-z0-9\-.$]+", " ") : "";
                            obj.BairroImovel = _arrayLinha.Length > 1 ? _bairroImovel : "";
                            obj.CepImovel = _arrayLinha.Length > 2 ? Regex.Replace(_arrayLinha[2].Trim(), @"[^0-9\-$]+", "") : "";
                            obj.CidadeImovel = _arrayLinha.Length > 3 ? Regex.Replace(_arrayLinha[3].Trim(), @"[^A-Z0-9$]+", " ") + "-" + Regex.Replace(_arrayLinha[4].Trim(), @"[^A-Z$]+", "") : "";
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
                    case 100:
                        {
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

                            if (_arrayLinha.Any(n => n.Contains("TaxaServico")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TaxaServico"));
                                obj.TaxaServico = Regex.Replace(_arrayLinha[_id].Replace("TaxaServico", "").Trim(), @"[^0-9$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Lastro")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Lastro"));
                                obj.Lastro = Regex.Replace(_arrayLinha[_id].Replace("Lastro", "").Trim(), @"[^0-9$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("DESCARQUISICAO")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DESCARQUISICAO"));
                                obj.DescontoAquisicao = Regex.Replace(_arrayLinha[_id].Replace("DESCARQUISICAO", "").Trim(), @"[^0-9$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Remuneracao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Remuneracao"));
                                obj.Remuneracao = Regex.Replace(_arrayLinha[_id].Replace("Remuneracao", "").Trim(), @"[^0-9$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("OriRec")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("OriRec"));
                                obj.OrigemRecurso = Regex.Replace(_arrayLinha[_id].Replace("OriRec", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Prestacao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Prestacao"));
                                obj.Prestacao = (obj.Prestacao == "" || obj.Prestacao == "0") ? Regex.Replace(_arrayLinha[_id].Replace("Prestacao", "").Trim(), @"[^0-9$]+", "") : "0";
                            }

                            if (_arrayLinha.Any(n => n.Contains("Custas")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Custas"));
                                obj.Custas = Regex.Replace(_arrayLinha[_id].Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Agio")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Agio"));
                                obj.Agio = Regex.Replace(_arrayLinha[_id].Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Sistema")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Sistema"));
                                string _sistema = Regex.Replace(_arrayLinha[_id].Replace("Sistema", "").Trim(), @"[^A-Za-z0-9$]", "");
                                obj.Sistema = _sistema.ToUpper().Contains("SAC") ? "S" : _sistema.ToUpper().Contains("PRICE") ? "P" : _sistema.ToUpper().Contains("LIVRE") ? "L" : _sistema.ToUpper().Contains("SACRE") ? "R" : "O";
                            }

                            if (_arrayLinha.Any(n => n.Contains("VlrFinan")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("VlrFinan"));
                                obj.ValorFinanciamento = (obj.ValorFinanciamento == "" || obj.ValorFinanciamento == "0") ? Regex.Replace(_arrayLinha[_id].Replace("VlrFinan", "").Trim(), @"[^0-9$]+", "") : "0";
                            }

                            if (_arrayLinha.Any(n => n.Contains("Contabil")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Contabil"));
                                obj.CodigoContabil = (obj.CodigoContabil == "" || obj.CodigoContabil == "0") ? Regex.Replace(_arrayLinha[_id].Replace("Contabil", "").Trim(), @"[^0-9$]+", "") : "0";
                            }

                            if (_arrayLinha.Any(n => n.Contains("SeguroDFi")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SeguroDFi"));
                                obj.SeguroDFI = (obj.SeguroDFI == "" || obj.SeguroDFI == "0") ? Regex.Replace(_arrayLinha[_id].Replace("SeguroDFi", "").Trim(), @"[^0-9$]+", "") : "0";
                            }

                            if (_arrayLinha.Any(n => n.Contains("SeguroMIP")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SeguroMIP"));
                                obj.SeguroMIP = (obj.SeguroMIP == "" || obj.SeguroMIP == "0") ? Regex.Replace(_arrayLinha[_id].Replace("SeguroMIP", "").Trim(), @"[^0-9$]+", "") : "0";
                            }

                            if (_arrayLinha.Any(n => n.Contains("Razao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Razao"));
                                obj.Razao = (obj.Razao == "" || obj.Razao == "0") ? Regex.Replace(_arrayLinha[_id].Replace("Razao", "").Trim(), @"[^0-9\/$]+", "") : "0";
                            }

                            if (_arrayLinha.Any(n => n.Contains("TAXA")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TAXA"));
                                obj.Taxa = Regex.Replace(_arrayLinha[_id].Replace("TAXA", "").Trim(), @"[^0-9\/$]+", "");
                            }

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

                            if (_arrayLinha.Any(n => n.Contains("RemunDifJuros")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("RemunDifJuros"));
                                obj.RemunDifJuros = Regex.Replace(_arrayLinha[_id].Replace("RemunDifJuros", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Prazo")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Prazo"));
                                obj.Prazo = Regex.Replace(_arrayLinha[_id].Replace("Prazo", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Emp")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Emp"));
                                obj.Empreendimento = Regex.Replace(_arrayLinha[_id].Replace("Emp", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("SEG")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SEG"));
                                obj.Iof = Regex.Replace(_arrayLinha[_id].Replace("SEG", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("aplc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => Regex.IsMatch(f.Trim(), @"(^[\w]{4}\s\d{6}$)"));
                                if (_id >= 0)
                                    obj.Apolice = string.IsNullOrWhiteSpace(obj.Apolice) ? Regex.Replace(_arrayLinha[_id].Replace("aplc", "").Trim(), @"[^0-9$]+", "") : obj.Apolice;
                            }

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

                            if (_arrayLinha.Any(n => n.Contains("DtInc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("DtInc"));
                                obj.DataInclusao = Regex.Replace(_arrayLinha[_id].Replace("DtInc", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Correcao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Correcao"));
                                obj.Correcao = _arrayLinha[_id].Replace("Correcao", "").Trim();
                            }

                            if (_arrayLinha.Any(n => n.Contains("Carencia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Carencia"));
                                obj.Carencia = Regex.Replace(_arrayLinha[_id].Replace("Carencia", "").Trim(), @"[^0-9\/$]+", "");
                            }
                            if (_arrayLinha.Any(n => n.Contains("Prorrogacao")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Prorrogacao"));
                                obj.Prorrogacao = Regex.Replace(_arrayLinha[_id].Replace("Prorrogacao", "").Trim(), @"[^0-9\/$]+", "");
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

                            if (_arrayLinha.Any(n => n.Contains("TipoFinanc")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TipoFinanc"));
                                obj.TipoFinanciamento = _arrayLinha[_id].Replace("TipoFinanc", "").Trim();
                            }

                            if (_arrayLinha.Any(n => n.Contains("TipoOrig")))
                            {
                                if (_arrayLinha.Length == 2)
                                {
                                    obj.TipoOrigem = string.Join("", _arrayLinha).Replace("TipoOrig", string.Empty).Trim();
                                }
                                else
                                {
                                    _id = _arrayLinha.ToList().FindIndex(f => f.Contains("TipoOrig"));
                                    obj.TipoOrigem = _arrayLinha[_id].Replace("TipoOrig", "");
                                }
                            }

                            if (_arrayLinha.Any(n => n.Contains("Repac")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Repac"));
                                obj.Repactuacao = Regex.Replace(_arrayLinha[_id].Replace("Repac", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("VlrGarantia")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("VlrGarantia"));
                                obj.ValorGarantia = Regex.Replace(_arrayLinha[_id].Replace("VlrGarantia", "").Trim(), @"[^0-9\/$]+", "");
                            }

                            if (_arrayLinha.Any(n => n.Contains("Reaj")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("Reaj"));
                                obj.Reajuste = _arrayLinha[_id].Replace("Reaj", "").Trim() ;
                            }

                            if (_arrayLinha.Any(n => n.Contains("SegVis")))
                            {
                                _id = _arrayLinha.ToList().FindIndex(f => f.Contains("SegVis"));
                                obj.SeguroVista = _arrayLinha[_id].Replace("SegVis", "").Trim();
                            }

                            break;
                        }
                    default:
                        return obj;

                }
            }
            catch (Exception exOut)
            {
                throw new ArgumentOutOfRangeException("Cabeçaho do PDF - Arquivo: BusinessCabecalho - case: " + _case, exOut.Message);
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

                            if (_linha.Contains("C.P.F"))
                            {
                                _arrayLinha = _linha.Replace("C.P.F.", ":").Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                                var arr = _arrayLinha[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList(); 
                                arr.Add(_arrayLinha[0]);
                                _arrayLinha = arr.ToArray();
                            }
                            else
                            {
                                List<string> lstArray = _linha.Split(' ').ToList();
                                List<string> arr = new List<string>(3)
                                {
                                    lstArray.FirstOrDefault(x => Regex.IsMatch(x, @"(^\d{3}.\d{3}.\d{3}-\d{2}$)")),
                                    lstArray.FirstOrDefault(x => Regex.IsMatch(x, @"(^\d{2}\/\d{2}\/\d{4}$)"))
                                };

                                lstArray.Remove(arr[0]);
                                lstArray.Remove(arr[1]);
                                lstArray.RemoveAt((lstArray.Count - 1));

                                arr.Add(string.Join(" ", lstArray));
                                _arrayLinha = arr.ToArray();
                            }
                            break;
                        }
                    case 3:
                        {
                            _case = "3 - Metodo: TrataLinhaPDFPadrao2 -  campo: endereço de imovel";

                            string _bairro = string.Empty, _cep = string.Empty, _cidade = string.Empty, _endereco = string.Empty, _uf = string.Empty;
                            var arr = _linha.Trim().Split(' ').ToList();

                            int p = arr.FindIndex(u => Regex.IsMatch(u, ("[0-9]{2,3}.[0-9]{3}-[0-9]{3}")));

                            List<string> lstBairo = new List<string>();
                            var j = arr.GetRange(0, p);
                            var m = arr.Skip(p).ToList();

                            _cep = m[0];
                            m.RemoveAt(0);
                            _uf = m.Last();

                            m.Remove(_uf);


                            _cidade = string.Join(" ", m.ToArray()) + "-" + _uf;

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

                            if (lstBairo.Count == 0 && j.Any(n => string.IsNullOrWhiteSpace(n)))
                            {
                                for (int i = (j.Count-1) ; i > 0 ; i--)
                                {
                                    if (!string.IsNullOrWhiteSpace(j[i]))
                                    {
                                        lstBairo.Add(j[i]);
                                        j.Remove(j[i]);
                                    }
                                    else break;
                                }
                            }

                            if (j.Any(x => x.Equals("CENTRO")))
                            {
                                j.Remove("CENTRO");
                                _bairro = "CENTRO";
                            }

                            _endereco = string.Join(" ", j.ToArray()).Trim();
                            lstBairo.Reverse();
                            _bairro = string.IsNullOrWhiteSpace(_bairro) ? string.Join(" ", lstBairo.ToArray()) : _bairro;

                            _bairro = string.IsNullOrWhiteSpace(_bairro) ? _cidade : _bairro.Trim();
                            _endereco = _endereco.Length > 80 ? _endereco.Substring(0, 79) : _endereco.Trim();
                            var lstItens = new List<string>() { _endereco.Trim(), _bairro.Trim(), _cidade.Trim(), _uf.Trim(), _cep.Trim() }.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();

                            for (int i = 0; i < lstItens.Count; i++)
                            {
                                lstItens[i] = Regex.Replace(lstItens[i], @"\s+", " ");
                            }
                           

                            _arrayLinha = lstItens.ToArray();

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
                DataTransferencia = string.IsNullOrWhiteSpace(obj.DataTransferencia) ? "" : obj.DataTransferencia,
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
                Taxa = string.IsNullOrWhiteSpace(obj.Taxa) ? "0" : obj.Taxa,
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
                TaxaServico = string.IsNullOrWhiteSpace(obj.TaxaServico) ? "0" : obj.TaxaServico,
                DescontoAquisicao = string.IsNullOrWhiteSpace(obj.DescontoAquisicao) ? "0" : obj.DescontoAquisicao,
                RemunDifJuros = string.IsNullOrWhiteSpace(obj.RemunDifJuros) ? "0" : obj.RemunDifJuros,
                Remuneracao = string.IsNullOrWhiteSpace(obj.Remuneracao) ? "0" : obj.Remuneracao,
                Custas = string.IsNullOrWhiteSpace(obj.Custas) ? "0" : obj.Custas,
                Lastro = string.IsNullOrWhiteSpace(obj.Lastro) ? "0" : obj.Lastro,
                Agio = string.IsNullOrWhiteSpace(obj.Agio) ? "0" : obj.Agio,
                Prorrogacao = string.IsNullOrWhiteSpace(obj.Prorrogacao) ? "0" : obj.Prorrogacao,
            };
        }

        public void PopulaContrato(object parametro)
        {

            List<ContratoPdf> lstContratosPdf = (List<ContratoPdf>)parametro.GetType().GetProperty("item1").GetValue(parametro, null);
            string _diretorioDestino = (string)parametro.GetType().GetProperty("item3").GetValue(parametro, null);
            string _diretorioOrigem = (string)parametro.GetType().GetProperty("item4").GetValue(parametro, null);
            List<string> lstContratosNovos = new List<string>();

            string strAlta = string.Empty;
            string _contratoGT = string.Empty;
            string  _dataPrimeiroVencimento, _taxaJurosContrato, _valorgarantia, _reajuste, _apolice, _repactuacao, _diaVencimento;

            _dataPrimeiroVencimento = _taxaJurosContrato = _valorgarantia = _reajuste = _apolice = _repactuacao = _diaVencimento = string.Empty;

            #region BLOCO QUE GERA O ARQUIVO DE CONTRATO
            //======================= BLOCO QUE GERA O ARQUIVO DE CONTRATO================================================
            using (StreamWriter escreverContrato = new StreamWriter(_diretorioDestino + @"\TL16CONT.txt", true, Encoding.Default))
            {
                Cabecalho c = null;
               
                //foreach (ContratoPdf item in lstContratosPdf)
                for (int i = 0; i < lstContratosPdf.Count; i++)
                {
                    ContratoPdf item = lstContratosPdf[i];

                    try
                    {
                        strAlta = string.Empty;

                        // Pegar a Data do 1º vencimento sempre do primero Cabeçalho, definido pela Andrea no Call do dia 07/10/2019
                        // Data de Alteração: 07/10/2019
                        _dataPrimeiroVencimento = item.Cabecalhos[0].DataPrimeiroVencimento;

                        item.Cabecalhos.ForEach(l =>
                        {
                            //Solicitação da Camila para pegar somente a Apolice do ultimo cabeçalho de cada contrato
                            //Data: 10/10/2019
                            _apolice = l.Apolice;
                           // _repactuacao = l.Repactuacao;

                            if (!l.TaxaJuros.Trim().Equals(_taxaJurosContrato))
                                _taxaJurosContrato = l.TaxaJuros.Trim();
                            //if (!l.Apolice.Trim().Equals(_apolice))
                            //    _apolice = l.Apolice.Trim();
                           
                            //if (!l.Prazo.Trim().Equals(_prazo))
                            //    _prazo = l.Prazo.Trim();
                            if (!l.Reajuste.Trim().Equals(_reajuste))
                                _reajuste = l.Reajuste.Trim();
                            if (!l.ValorGarantia.Trim().Equals(_valorgarantia))
                                _valorgarantia = l.ValorGarantia.Trim();

                            // na reunião onde o Luis quebrou o pau com a Andreia, foi definido que a 'Data 1º Vencimento, deve ser sempre a data do primero Cabeçalho
                            // Data de Alteração: 07/10/2019
                            //if (!l.DataPrimeiroVencimento.Trim().Equals(_dataPrimeiroVencimento))
                            //    _dataPrimeiroVencimento = l.DataPrimeiroVencimento.Trim();


                            // Definição pela Andrea, pergar o Dia do Vencimento sempre do ultimo cabeçado
                            // Data: 09/10/2019
                            _diaVencimento = l.DataPrimeiroVencimento.Trim().Substring(0,2);
                        });

                        c = item.Cabecalhos.FirstOrDefault();

                        // Melhoria solicitada na Versão 1.2 da Ferramenta, Melhoria com orientação do Luis 
                        // Se a data do primeiro vencimento for == 29/02, então pega a data do vencimwento da primeira parcela
                        // Data de Alteração: 27/08/2019
                        int[] _dataIs2902 = {
                                    Convert.ToInt32(c.DataPrimeiroVencimento.Split('/')[0]),
                                    Convert.ToInt32(c.DataPrimeiroVencimento.Split('/')[1]),
                                    Convert.ToInt32(c.DataPrimeiroVencimento.Split('/')[2].Substring(0,4))
                                };
                        if (_dataIs2902[0] > DateTime.DaysInMonth(_dataIs2902[2], _dataIs2902[1]))
                        {
                            _dataPrimeiroVencimento = item.Parcelas.FirstOrDefault().Vencimento;
                        }

                        _contratoGT = Ambiente.listGTBem.Find(p => p.Key.Equals(c.Carteira.Substring(2, 2) + c.Contrato.Trim())).Key;

                        if (!string.IsNullOrWhiteSpace(_contratoGT))
                        {
                            // Ajuste com orientação do Luis, 
                            // Alteração: Adicionamos o campo Agencia na posição(18,4)
                            // DATA: 29/08/2019 as 11:30

                            // strAlta = string.Format("{0}{1}{2}{3}", c.Carteira.Trim().Substring(2), c.Contrato.Trim(), (string.IsNullOrWhiteSpace(_dataPrimeiroVencimento) ? c.DataPrimeiroVencimento.Trim() : _dataPrimeiroVencimento).Substring(0, 2), c.Agencia.Substring(2, 4)).PadRight(24, ' ');
                            // COMENTAMOS A LINHA ACIMA, PORQUE FOI DEFINIDO PELA ANDREA QUE O DIA DE VENCIMENTO SERÁ SEMPRE O DIA DO ULTIMO CABEÇALHO

                            strAlta = string.Format("{0}{1}{2}{3}", c.Carteira.Trim().Substring(2), c.Contrato.Trim(), _diaVencimento, c.Agencia.Substring(2, 4)).PadRight(24, ' ');
                            strAlta += string.Format("{0}{1}{2}{3}", (c.Nome.Trim().Length > 40 ? c.Nome.Trim().Substring(0, 40) : c.Nome.Trim()).PadRight(40, ' '), c.DataNascimento.Trim().PadRight(10, ' '), c.DataTransferencia.PadRight(14, ' '), c.EnderecoImovel.Trim().PadRight(80, ' '));
                            strAlta += string.Format("{0}{1}{2}{3}", (c.Cpf.Trim().Length < 15 ? c.Cpf : c.Cpf.Substring(1)).PadRight(14, ' '), "".PadRight(3, ' '), _contratoGT.Trim().PadRight(20, ' '), item.Bem.PadRight(20, ' '));
                            strAlta += string.Format("{0}{1}", c.Modalidade.Trim().PadRight(40, ' '), (c.CidadeImovel.Trim().Length <= 31 ? c.CidadeImovel.Trim() : c.CidadeImovel.Trim().Substring(0, 31)).PadRight(31, ' '));
                            strAlta += string.Format("{0}{1}{2}", c.Plano.Trim().PadRight(10, ' '), c.DataContrato.Trim().PadRight(10, ' '), "".PadRight(2, ' '));
                            strAlta += string.Format("{0}{1}", c.Prestacao.Trim().PadLeft(18, '0'), c.Sistema.Trim().PadRight(2, ' '));
                            strAlta += string.Format("{0}{1}{2}", c.ValorFinanciamento.Trim().PadLeft(18, '0'), string.IsNullOrWhiteSpace(c.CodigoContabil.Trim()) ? "".PadRight(15, '0') : c.CodigoContabil.Trim().Substring(1).PadRight(15, '0'), c.SeguroMIP.Trim().PadLeft(18, '0'));

                            // O Luis disse que na construção do contrato, "REAJUSTE"  é sempre o original
                            //strAlta += string.Format("{0}{1}", c.Reajuste.Trim().PadRight(10, ' '), c.DataGarantia.Trim().PadRight(18, ' '));
                            strAlta += string.Format("{0}{1}", (string.IsNullOrWhiteSpace(_reajuste) ? c.Reajuste.Trim() : _reajuste).PadRight(10, ' '), c.DataGarantia.Trim().PadRight(18, ' '));
                            strAlta += string.Format("{0}", c.SeguroDFI.Trim().PadLeft(18, '0'));

                            // O Luis disse que na construção do contrato, "PRAZO e VALOR GARANTIA"  é sempre o original
                            // strAlta += string.Format("{0}{1}{2}", (string.IsNullOrWhiteSpace(_prazo) ? c.Prazo.Trim() : _prazo).PadLeft(5, '0'), _valorgarantia.Trim().PadLeft(18, '0'), "".PadRight(8, ' '));
                            strAlta += string.Format("{0}{1}", c.Prazo.Trim().PadLeft(5, '0'), _valorgarantia.Trim().PadLeft(18, '0')).PadRight(31, ' ');


                            // O Luis disse que na construção do contrato, "TAXA DE JUROS"  é sempre o original, Mas o Luis Falou que tem que percorrer os cabeçaos
                            // strAlta += string.Format("{0}{1}", "0".PadRight(18, '0'), c.TaxaJuros.Trim().PadLeft(11, '0'));
                            strAlta += string.Format("{0}{1}", "0".PadRight(18, '0'), (string.IsNullOrWhiteSpace(_taxaJurosContrato) ? c.TaxaJuros.Trim() : _taxaJurosContrato).Trim().PadLeft(11, '0'));

                            // O Luis disse que na construção do contrato, "APOLICE"  é sempre o original
                            //strAlta += string.Format("{0}{1}{2}", (string.IsNullOrWhiteSpace(_dataPrimeiroVencimento) ? c.DataPrimeiroVencimento.Trim() : _dataPrimeiroVencimento).Substring(0, 10).PadLeft(10, '0'), (string.IsNullOrWhiteSpace(_apolice) ? c.Apolice.Trim() : _apolice).PadLeft(6, '0'), (c.CepImovel.Trim() + "-" + (c.BairroImovel.Trim().Length > 24 ? c.BairroImovel.Trim().Substring(0, 24) : c.BairroImovel.Trim())).PadRight(34, ' '));
                            strAlta += string.Format("{0}{1}{2}", (string.IsNullOrWhiteSpace(_dataPrimeiroVencimento) ? c.DataPrimeiroVencimento.Trim() : _dataPrimeiroVencimento).Substring(0, 10).PadLeft(10, '0'), _apolice.Trim().PadLeft(6, '0'), (c.CepImovel.Trim() + "-" + (c.BairroImovel.Trim().Length > 24 ? c.BairroImovel.Trim().Substring(0, 24) : c.BairroImovel.Trim())).PadRight(34, ' '));
                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadRight(11, '0'), c.Correcao.Trim().PadRight(10, ' '), "0".PadLeft(2, '0'), c.Razao.Trim().PadLeft(18, '0'));
                            strAlta += string.Format("{0}{1}", "0".PadLeft(2, '0'), c.Situacao.Trim()).PadRight(60, ' ');
                            c = null;

                            _contratoGT = string.Empty;

                            strAlta = strAlta.PadRight(648, ' ');
                            escreverContrato.WriteLine(strAlta);
                            _dataPrimeiroVencimento = string.Empty;
                            strAlta = string.Empty;
                        }
                        else
                        {
                            lstContratosNovos.Add(c.Contrato.Trim());
                            ExceptionError.NovoContratoGT(c.Contrato, _diretorioDestino);
                        }
                    }
                    catch (ArgumentOutOfRangeException exOut)
                    {
                        lstContratosPdf.Remove(item);
                        string _detalhes = "Metodo: PopulaContrato - Classe: BusinessCabecalho - 001 Ação: tentativa de gerear o registro no arquivo TL16CONT.txt";
                        ExceptionError.TrataErros(exOut, c.Contrato, _detalhes, _diretorioDestino);
                        
                    }
                    catch (Exception ex)
                    {
                        lstContratosPdf.Remove(item);
                        string _detalhes = "Metodo: PopulaContrato - Classe: BusinessCabecalho - 002 Ação: tentativa de gerear o registro no arquivo TL16CONT.txt";
                        ExceptionError.TrataErros(ex, c.Contrato, _detalhes, _diretorioDestino);
                       
                    }
                    
                }
            }
            //=============================================================================================================
            #endregion

            #region BLOCO QUE GERA O ARQUIVO DE OCORRENCIA E PARCELAS
            //======================= BLOCO QUE GERA O ARQUIVO DE OCORRENCIA================================================

            using (StreamWriter escreverOcorrencia = new StreamWriter(_diretorioDestino + @"\TL16OCOR.txt", true, Encoding.Default))
            {
                string _novaOcorrencia, altaRepac,strCabecalhoOcorrencia;
                strAlta = strCabecalhoOcorrencia = altaRepac = string.Empty;

                bool isOcorrenciaWrite =  false;
                Parcela _parcela = null;
                Cabecalho _cabecalho = null, _cabecalhoAnterior = null;
                List<string> lstTipoOcorrencia = new List<string>() { "004", "005", "010" };

                lstContratosPdf.ForEach(q =>
                {
                    string _datavencimentoAnterior = string.Empty;
                    strAlta = string.Empty;

                    q.Ocorrencias.ForEach(o =>
                    {
                        if (!lstContratosNovos.Any(k => k.Equals(o.Contrato.Trim())))
                        { 
                            try
                            {
                                _parcela = q.Parcelas.SingleOrDefault(m => m.Id == o.IdParcela);

                                strAlta = string.Empty;

                                if (_parcela == null)
                                    _cabecalho = q.Cabecalhos.Find(k => k.Id == o.IdCabecalho);
                                else
                                    _cabecalho = q.Cabecalhos.Find(k => k.Id == _parcela.IdCabecalho);

                                _datavencimentoAnterior = _cabecalho.DataPrimeiroVencimento;

                                if (q.Cabecalhos.Count > 1)
                                {
                                    if (q.Cabecalhos.Any(a => a.Id == (o.IdCabecalho + 1)))
                                        _cabecalhoAnterior = q.Cabecalhos.Find(xx => xx.Id == (o.IdCabecalho + 1));
                                    else
                                        _cabecalhoAnterior = q.Cabecalhos.Find(xx => xx.Id == (o.IdCabecalho - 1));
                                }
                                else
                                    _cabecalho = _cabecalhoAnterior = q.Cabecalhos.Find(k => k.Id == o.IdCabecalho);

                                strAlta = string.Format("{0}", (q.Carteira.Substring(2, 2) + o.Contrato).PadRight(15, '0'));
                                strAlta += string.Format("{0}{1}", o.Vencimento.Trim().Trim().PadRight(10, '0'), o.Pagamento.Trim().Trim().PadRight(10, '0'));
                                strAlta += string.Format("{0}", o.Descricao.Trim().PadRight(36, ' '));

                                altaRepac = _novaOcorrencia = strCabecalhoOcorrencia = strAlta;

                                if (o.CodigoOcorrencia.Equals("020")) //Amortização extra
                                {
                                    isOcorrenciaWrite = true;
                                    string strAltaNovoPrazo = strAlta;

                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";

                                    strAlta += string.Format("{0}{1}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    
                                    //alteração feita na data: 29/07/2019 as 21:30 pedido pelo Emerson e Luis 
                                    //Alteração feita na data: 30/01/2019 14: 47 (quando não houver parcelas para vencimento da ocorrencia, então; deixar a data de vencimento em 'Branco')
                                    strAlta += string.Format("{0}", (o.NaoTemParcela ? "" : Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd")).Trim().PadRight(30, ' '), "".PadRight(90, ' '));

                                    strAlta = strAlta.PadRight(281, ' ');
                                    escreverOcorrencia.WriteLine(strAlta);
                                    strAlta = string.Empty;
                                    // O Luis falou que, sempre que encontrar uma ocorrencia de 'Novo Prazo' após uma ocorencia do tipo 'Amortização extra' de codigo '020'
                                    // gerar uma nova ocorrencia com os dados zerados exceto o Saldo devedor
                                    //Data: 31/07/2019 as 10:00hs
                                    if (o.IsNovoPrazo)
                                    {
                                        // Solicitação da Camila, quando houver 020Amortização extra, seguido de alteração de (Novo Prazo)
                                        // Enviar a segunda ocorrencia com o codigo 010 exemplo: ***010Alteração Contratual
                                        // Data: 03/10/2019
                                        strAltaNovoPrazo = strAltaNovoPrazo.Replace("020Amortização extra", "010Alteração Contratual").Trim().PadRight(71, ' ');

                                        strAltaNovoPrazo += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "PRAZO".PadRight(30, ' '));
                                        strAltaNovoPrazo += string.Format("{0}{1}", _parcela.NumeroPrazo.Substring(3).Trim().PadRight(30, ' '), o.NovoNumeroPrazo.PadLeft(3, '0').Trim().PadRight(30, ' '));

                                        if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                            strAltaNovoPrazo += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                        else
                                            //**************************************VOLTA ESSA LINHA SE DER ALGO DE ERRADO NO VENCIMENTO DO PRAZO ************************************** DATA:01/08/2019
                                            //strAltaNovoPrazo += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _parcela.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                            strAltaNovoPrazo += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));

                                        strAltaNovoPrazo = strAltaNovoPrazo.PadRight(281, ' ');
                                        escreverOcorrencia.WriteLine(strAltaNovoPrazo);

                                        strAlta = strAltaNovoPrazo = string.Empty;
                                    }
                                }

                                if (o.CodigoOcorrencia.Equals("021")) //Amortização rec. Fgts
                                {
                                    string strAltaNovoPrazo = strAlta;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _parcela.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                    strAlta += string.Format("{0}", (string.IsNullOrWhiteSpace(o.Damp) ? string.Empty : o.Damp).Trim().PadRight(30, ' '));

                                    strAlta = strAlta.PadRight(281, ' ');
                                    escreverOcorrencia.WriteLine(strAlta);
                                    strAlta = string.Empty;

                                    // O Luis falou que, sempre que encontrar uma ocorrencia de 'Novo Prazo' após uma ocorencia do tipo 'Amortização extra' de codigo '021'
                                    // gerar uma nova ocorrencia com os dados zerados exceto o Saldo devedor
                                    //Data: 31/07/2019 as 10:30hs
                                    if (o.IsNovoPrazo)
                                    {
                                        // Solicitação da Camila, quando houver amortização fgts(Damp), seguido de alteração de (Novo Prazo)
                                        // Enviar a segunda ocorrencia com o codigo 010 exemplo: ***010Alteração Contratual
                                        strAltaNovoPrazo = strAltaNovoPrazo.Replace("021Amortização rec. Fgts", "010Alteração Contratual").PadRight(71, ' ');

                                        strAltaNovoPrazo += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "PRAZO".PadRight(30, ' '));
                                        strAltaNovoPrazo += string.Format("{0}{1}", _parcela.NumeroPrazo.Substring(3).Trim().PadRight(30, ' '), o.NovoNumeroPrazo.PadLeft(3, '0').Trim().PadRight(30, ' '));

                                        if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                            strAltaNovoPrazo += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                        else
                                            strAltaNovoPrazo += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _parcela.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));

                                        strAltaNovoPrazo = strAltaNovoPrazo.PadRight(281, ' ');
                                        escreverOcorrencia.WriteLine(strAltaNovoPrazo);

                                        strAltaNovoPrazo = string.Empty;
                                    }
                                }

                                // ALTERAÇÃO CONTRATUAL COM OS TIPOS DE OCORRENCIAS
                                // TIPO DE OCORRENCIA: 004", "005", "010"
                                if (lstTipoOcorrencia.Any(t => t.Equals(o.CodigoOcorrencia.Trim()))) // Alteração Contratual, Alteração de garantia, Mudança dia vencimento
                                {
                                    bool hasTypeOccurrence = false;

                                    if (o.CodigoOcorrencia.Equals("010"))
                                    {
                                        #region TAXA DE JUROS
                                        if (!_cabecalhoAnterior.TaxaJuros.Equals(_cabecalho.TaxaJuros)) 
                                        {
                                            hasTypeOccurrence = true;
                                            //  hasDiferenca = true;
                                            if (o.NaoTemParcela)
                                            {
                                                string altaAnterior = strAlta;

                                                strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "PRAZO".PadRight(30, ' '));
                                                strAlta += string.Format("{0}{1}{2}", "".PadLeft(30, ' '), _parcela.NumeroPrazo.Substring(3).Trim().PadRight(30, ' '), "00010101".PadRight(30, ' '));
                                                strAlta = strAlta.PadRight(281, ' ');
                                                escreverOcorrencia.WriteLine(strAlta);
                                                strAlta = altaAnterior;
                                            }

                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "TAXA JUROS".PadRight(30, ' '));                                                 
                                            strAlta += string.Format("{0}{1}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '), _cabecalhoAnterior.TaxaJuros.Trim().PadRight(30, ' '));

                                            if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                                strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                            else
                                                // Adicionado o valor '00010101' se houver uma ocorrencia de  TAXA DE JUROS sem parcelas
                                                // Data: 30/07/2019 as 17:5
                                                
                                                // O Luis e a Camila pediu para pegar o REAJUSTE qunado for ocorrencia do tipo 10 e descrição 'TAXA DE JUTOS'e subistituir o campo 
                                                //Data: 19/11/2019
                                                //strAlta += string.Format("{0}", (o.NaoTemParcela ? "00010101" : Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd")).Trim().PadRight(30, ' '));

                                                strAlta += string.Format("{0}", (o.NaoTemParcela ? "00010101" : _cabecalhoAnterior.Reajuste.Trim().PadRight(30, ' ')));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        Cabecalho item = q.Cabecalhos.FirstOrDefault(k => k.Contrato.Equals(o.Contrato)); 
                                        Cabecalho item1 = q.Cabecalhos.LastOrDefault(k => k.Contrato.Equals(o.Contrato));

                                        #region REPACTUAÇÃO
                                        if (!item.Repactuacao.Equals(item1.Repactuacao)) 
                                        {
                                            hasTypeOccurrence = true;
                                            _repactuacao = item1.Repactuacao;

                                            if (!_repactuacao.Equals("0"))
                                            {
                                                strAlta = altaRepac;
                                                strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "REPACTUACAO".PadRight(30, ' '));
                                                strAlta += string.Format("{0}{1}", _repactuacao.Split('/')[1].Trim().PadRight(30, ' '), "".PadRight(30, ' '));

                                                if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                                    strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                                else
                                                    // Adicionado o valor '00010101' se houver uma ocorrencia de REPACTUAÇÃO sem parcelas
                                                    // Data: 30/07/2019 as 17:5
                                                    strAlta += string.Format("{0}", (o.NaoTemParcela ? "00010101" : Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd")).Trim().PadRight(30, ' '));

                                                strAlta = strAlta.PadRight(281, ' ');
                                                escreverOcorrencia.WriteLine(strAlta);
                                                strAlta = _repactuacao = string.Empty;
                                            }
                                        }
                                        #endregion

                                        #region APOLICE
                                        if (!_cabecalhoAnterior.Apolice.Equals(_cabecalho.Apolice)) 
                                        {
                                            hasTypeOccurrence = true;
                                            strAlta = _novaOcorrencia;

                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "APOLICE".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.Apolice.Trim().PadRight(30, ' '), _cabecalhoAnterior.Apolice.Trim().PadRight(30, ' '));

                                            if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                                strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                            else
                                                strAlta += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _parcela.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        #region PRAZO
                                        if (!_cabecalhoAnterior.Prazo.Equals(_cabecalho.Prazo)) // PRAZO
                                        {
                                            
                                            // se o prazo da parcela da ocorrencia '010' for igual ao prazo do cabeçalho anterior, então, não imprime a ocorrencia
                                            // Logica alinhada com o Luis para não gerar a linha de prazo se caso a ocorrencia anterior for um 'Novo Prazo'
                                            // Data: 31/07/2019  as 13:10hs
                                            if (_parcela.NumeroPrazo.Substring(3).Trim().Equals(_cabecalhoAnterior.Prazo.PadLeft(3, '0').Trim())) return;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;

                                           // hasDiferenca = true;
                                           // NUNCA IRIA ENTRAR NESSE BLOCO, MAS VAMOS DEIXAR AQUI
                                            //if (_consistencia)
                                            //{
                                            //    strAlta = string.Format("{0}", (q.Carteira.Substring(2, 2) + o.Contrato).PadRight(15, '0'));
                                            //    strAlta += string.Format("{0}{1}", o.Vencimento.Trim().Trim().PadRight(10, '0'), o.Pagamento.Trim().Trim().PadRight(10, '0'));
                                            //    strAlta += string.Format("{0}", o.Descricao.Trim().PadRight(36, ' '));
                                            //}

                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "PRAZO".PadRight(30, ' '));

                                            // Alterado, ocorrencia  igual a '010' por via de regra, deve comparar cabeçalho atual com Anterior
                                            // seguido pelos campos 'Taxa de Juros', 'Apolice', 'Prazo', 'Data do Primeiro Vencimento'
                                            // Data: 30/07/2019 as 17:48hs
                                            // linha de codigo alterada
                                            // ====> strAlta += string.Format("{0}{1}", _parcela.NumeroPrazo.Substring(3).Trim().PadRight(30, ' '), _cabecalhoAnterior.Prazo.PadLeft(3,'0').Trim().PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.Prazo.PadLeft(3, '0').Trim().PadRight(30, ' '), _cabecalhoAnterior.Prazo.PadLeft(3,'0').Trim().PadRight(30, ' '));

                                            if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                                strAlta += string.Format("{0}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                            else
                                                strAlta += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _parcela.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        #region PLANO
                                        if (!_cabecalhoAnterior.Plano.Equals(_cabecalho.Plano))
                                        {
                                            hasTypeOccurrence = true;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;
                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "PLANO".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.Plano.Trim().PadRight(30, ' '), _cabecalhoAnterior.Plano.Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        #region SISTEMA
                                        if (!_cabecalhoAnterior.Sistema.Equals(_cabecalho.Sistema))
                                        {
                                            hasTypeOccurrence = true;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;
                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "SISTEMA".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.Sistema.Trim().PadRight(30, ' '), _cabecalhoAnterior.Sistema.Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        #region REAJUSTE
                                        if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                        {
                                            hasTypeOccurrence = true;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;
                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "REAJUSTE".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.Reajuste.Trim().PadRight(30, ' '), _cabecalhoAnterior.Reajuste.Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        #region DATA DE CONTRATO
                                        if (!_cabecalhoAnterior.DataContrato.Equals(_cabecalho.DataContrato))
                                        {
                                            hasTypeOccurrence = true;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;
                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "DTCONTRATO".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.DataContrato.Trim().PadRight(30, ' '), _cabecalhoAnterior.DataContrato.Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        #region VALOR FINANCIADO
                                        if (!_cabecalhoAnterior.ValorFinanciamento.Equals(_cabecalho.ValorFinanciamento))
                                        {
                                            hasTypeOccurrence = true;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;
                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "VRFINANCIAMENTO".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.ValorFinanciamento.Trim().PadRight(30, ' '), _cabecalhoAnterior.ValorFinanciamento.Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion

                                        #region VALOR PRESTAÇÃO
                                        if (!_cabecalhoAnterior.Prestacao.Equals(_cabecalho.Prestacao))
                                        {
                                            hasTypeOccurrence = true;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;
                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "VRPRESTACAO".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.Prestacao.Trim().PadRight(30, ' '), _cabecalhoAnterior.Prestacao.Trim().PadRight(30, ' '));

                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }
                                        #endregion
                                       

                                        #region DATA 1º VENCIMENTO 

                                        // Usando o contrato 72189230001887, foi definido com o Agles e Luiz, que dentro da ocorrencia 010, não é necessario validar a Data de Primeiro Vencimento
                                        // DATA: 08/20/2019

                                        if (!_cabecalhoAnterior.DataPrimeiroVencimento.Equals(_cabecalho.DataPrimeiroVencimento)) // ALTERAÇÃO NA DATA DO 1º. VENCIMENTO DA PARCELA
                                        {
                                            hasTypeOccurrence = true;

                                            strAlta = string.IsNullOrWhiteSpace(_novaOcorrencia) ? strCabecalhoOcorrencia : _novaOcorrencia;

                                            //hasDiferenca = true;
                                            strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), "DTVENCIMENTO".PadRight(30, ' '));
                                            strAlta += string.Format("{0}", Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _parcela.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                            strAlta += string.Format("{0}", _cabecalho.DataPrimeiroVencimento.PadRight(30, ' '));
                                            strAlta += string.Format("{0}", _cabecalhoAnterior.DataPrimeiroVencimento.Trim().PadRight(60, ' '));
                                            strAlta = strAlta.PadRight(281, ' ');
                                            escreverOcorrencia.WriteLine(strAlta);
                                            strAlta = string.Empty;
                                        }

                                        //if (!hasDiferenca)
                                        //{
                                        //    // Alteramos para quando não houver diferença entre os cabeçalhos, zerar os demais campos e pegar somente o saldo devedor
                                        //    // e 0 campo PRAZo deve ser vazio mantendo o posicionamento do campo
                                        //    // Data: 30/07/2019 
                                        //    strAlta = strCabecalhoOcorrencia;
                                        //    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(72, '0'), o.SaldoDevedor.Trim().PadLeft(18, '0'), "PRAZO".PadRight(30, ' '));
                                        //    strAlta += string.Format("{0}{1}","".PadRight(30, ' '), _cabecalhoAnterior.Prazo.Trim().PadRight(30, ' '));
                                        //    strAlta += string.Format("{0}", "00010101".PadRight(30, ' '));
                                        //}

                                        #endregion
                                       
                                        // SE A OCORRENCIA 010 NÃO FOR DO TIPO [TAXA DE JUTOS, PRAZO, APOLICE, REPACTUAÇÃO], ESCREVE SOMENTE O CONTRATO E O TIPO DA OCORRENCIA
                                        if (!hasTypeOccurrence)
                                        {
                                            if (strAlta.Length > 15)
                                            {
                                                strAlta = strAlta.PadRight(281, ' ');
                                                escreverOcorrencia.WriteLine(strAlta);
                                            }
                                            strAlta = string.Empty;
                                        }
                                    }


                                    if (o.CodigoOcorrencia.Equals("005")) // Alteração de garantia
                                    {
                                        strAlta = strCabecalhoOcorrencia;

                                        if (!_cabecalhoAnterior.ValorGarantia.Equals(_cabecalho.ValorGarantia)) // GARATNTIA
                                        {
                                            isOcorrenciaWrite = true;
                                            strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "VRGARANTIA".PadRight(30, ' '));
                                            strAlta += string.Format("{0}{1}{2}", _cabecalho.ValorGarantia.Trim().PadLeft(18, '0'), "".PadRight(12, ' '), _cabecalhoAnterior.ValorGarantia.Trim().PadLeft(18, '0'));

                                            if (!_cabecalhoAnterior.Reajuste.Equals(_cabecalho.Reajuste))
                                                strAlta += string.Format("{0}{1}", "".PadRight(12, ' '), _cabecalho.TaxaJuros.Trim().PadRight(30, ' '));
                                            else
                                                strAlta += string.Format("{0}{1}", "".PadRight(12, ' '), Convert.ToDateTime((_parcela == null ? _cabecalho.DataGarantia : _parcela.Vencimento)).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                        }
                                        else
                                        {
                                            strAlta = string.Empty;
                                            _parcela = null;

                                        }
                                    }

                                    if (o.CodigoOcorrencia.Equals("004"))//Mudança dia vencimento")
                                    {
                                        strAlta = strCabecalhoOcorrencia;

                                        if (!_cabecalhoAnterior.DataPrimeiroVencimento.Equals(_cabecalho.DataPrimeiroVencimento))
                                        {
                                            string altaAnterior = strAlta;
                                            isOcorrenciaWrite = true;
                                            // Ajuste quando a ocorrencia nao tem parcela associada 
                                            // Reunião com Emerson e Luis no Radar [Data: 27/07/2019]
                                            if (o.NaoTemParcela)
                                            {
                                                strAlta += string.Format("{0}{1}{2}{3}", "0".PadLeft(54, '0'), "0".PadLeft(17, '0') + "+", o.SaldoDevedor.Trim().PadLeft(18, '0'), "PRAZO".PadRight(30, ' '));
                                                strAlta += string.Format("{0}{1}{2}","".PadLeft(30,' '), _cabecalho.Prazo.Trim().PadRight(30, ' '), "00010101".PadRight(30, ' '));
                                                strAlta = strAlta.PadRight(281, ' ');
                                                escreverOcorrencia.WriteLine(strAlta);
                                                strAlta = string.Empty;
                                            }

                                            strAlta = altaAnterior;

                                            // Alteramos o campo data de Vencimento, no trecho [o.NaoTemParcela ? "" : Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd")]
                                            //Quando não houver parcela de vencimento para ocorrencia, colocar a data de vencimento vazia 
                                            // Data: 30/06/2019 - ajustado em acordo com o Luis e Emerson
                                            strAlta += string.Format("{0}{1}{2}{3}{4}", "0".PadLeft(54, '0'),"0".PadLeft(17,'0')+"+", o.SaldoDevedor.Trim().PadLeft(18, '0'), o.NaoTemParcela ? "".PadLeft(8,' ') : Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd"),"".PadRight(22, ' '));
                                            strAlta += string.Format("{0}{1}", _cabecalho.DataPrimeiroVencimento.Trim().PadRight(30, ' '), _cabecalhoAnterior.DataPrimeiroVencimento.Trim().PadRight(30, ' '));
                                            strAlta += string.Format("{0}", "".PadLeft(30, ' '));

                                            altaAnterior = string.Empty;
                                        }
                                        else 
                                        {
                                            strAlta = string.Empty;
                                            _parcela = null;
                                        }
                                    }
                                }

                                if (o.CodigoOcorrencia.Equals("012")) // Crescimento tx.Juros
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim()+ "TAXA DE JUROS").PadRight(30, ' ');
                                    strAlta += string.Format("{0}{1}", _cabecalho.TaxaJuros.Trim().PadRight(30, ' '), _cabecalhoAnterior.TaxaJuros.Trim().PadRight(30, ' '));
                                    strAlta += string.Format("{0}", (o.NaoTemParcela ? "00010101" : _cabecalhoAnterior.Reajuste.Trim().PadRight(30, ' ')));
                                }

                                if (o.CodigoOcorrencia.Equals("022")) // Sinistro parcial
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("029")) // Amortização Residuo
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0') + _sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("028")) // Amortização s/recalculo
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("030")) // Incorporação no saldo
                                {
                                    isOcorrenciaWrite = true;
                                    string _reptc  = q.Cabecalhos.SingleOrDefault(repac => repac.Id.Equals(o.IdCabecalho + 1 > q.Cabecalhos.Count ? o.IdCabecalho : o.IdCabecalho + 1)).Repactuacao;

                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    //strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                    strAlta += string.Format("{0}", (o.NaoTemParcela ? "" : Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd")).Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                                    if (!_reptc.Equals("0"))
                                        strAlta += _reptc.Split('/')[1];
                                }

                                if (o.CodigoOcorrencia.Equals("031")) // Consolidação da divida
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("032")) // Incorporação juros
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-","").Trim().PadLeft(17, '0')+ _sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("040")) // Transferencia
                                {
                                    isOcorrenciaWrite = true;
                                    _cabecalho = q.Cabecalhos[0];

                                    _cabecalho.DataPrimeiroVencimento = ValidaData(_cabecalho.DataPrimeiroVencimento);
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("044")) // Transf.Parte ideal
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(90, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("046")) // Sinistro Parcial c/mudanca devedor
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("051")) //Liguidaçao rec. Fgts
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                    strAlta += string.Format("{0}", (string.IsNullOrWhiteSpace(o.Damp) ? string.Empty : o.Damp).Trim().PadRight(30, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("050")) // Liquidação Antecipada
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("052")) // Sinistro Total
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(18, '0'), Regex.Replace(o.Juros, "[^0-9$]", "").Trim().PadLeft(18, '0'), "0".PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '), "".PadRight(60, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("054")) // Liquidação Coobrigado
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("058")) // Liquidação Interveniencia
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("059")) //  Liquidação por portabilidade
                                {
                                    isOcorrenciaWrite = true;
                                    string _sinalOcorrencia = Convert.ToInt32(o.Amortizacao) < 0 ? "-" : "+";
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), o.Amortizacao.Replace("-", "").Trim().PadLeft(17, '0')+_sinalOcorrencia, o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                }

                                if (o.CodigoOcorrencia.Equals("060")) // Termino Prazo
                                {
                                    isOcorrenciaWrite = true;
                                    strAlta += string.Format("{0}{1}{2}", "0".PadLeft(54, '0'), "PRAZO".PadRight(18, ' '), o.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}", Convert.ToDateTime(_parcela.Vencimento).ToString("yyyyMMdd").Trim().PadRight(30, ' '));
                                }

                                if (!string.IsNullOrWhiteSpace(strAlta))
                                    strAlta = strAlta.PadRight(281, ' ');

                                if (!string.IsNullOrWhiteSpace(strAlta) && isOcorrenciaWrite)
                                    escreverOcorrencia.WriteLine(strAlta);

                                _parcela = null;
                                isOcorrenciaWrite = false;
                                strAlta = string.Empty;
                            }
                            catch (Exception exOc)
                            {
                                ExceptionError.TrataErros(exOc, q.Contrato, "Metodo: PopulaContrato - Classe: BusinessCabecalho - Ação: Erro gerado ao tentar escrever a Ocorrencia no arquivo TL16OCOR.txt", _diretorioDestino);
                            }
                        }
                    });

                });
                //=============================================================================================================


                #region BLOCO QUE GERA O ARQUIVO DE PARCELAS
                //======================= BLOCO QUE GERA O ARQUIVO DE PARCELAS ==================================================
                strAlta = string.Empty;
                using (StreamWriter escreverParcelas = new StreamWriter(_diretorioDestino + @"\TL16PARC.txt", true, Encoding.Default))
                {
                    BusinessParcelas businessParcelas = new BusinessParcelas();

                    List<Parcela> parcelas = new List<Parcela>();

                    string _sinal = string.Empty;
                    lstContratosPdf.ForEach(q =>
                    {
                        strAlta = string.Empty;

                        q.Parcelas.ForEach(p =>
                        {
                            if (!lstContratosNovos.Any(k => k.Equals(p.Contrato.Trim())))
                            {
                                try
                                {
                                    strAlta = string.Empty;

                                    _sinal = Convert.ToInt64(p.Amortizacao) < 0 ? "-" : "+";
                                    
                                    bool hasOcorrencia = q.Ocorrencias.Any(x => x.IdParcela == p.Id);
                                    strAlta = string.Format("{0}", (q.Carteira.Substring(2, 2) + q.Contrato).PadRight(15, '0'));
                                    strAlta += string.Format("{0}{1}", p.Vencimento.Trim().PadLeft(10, '0'), (p.Indice.Trim().Equals("") ? p.IndiceCorrecao.Trim() : p.Indice.Trim()).PadRight(7, '0'));
                                    strAlta += string.Format("{0}{1}", (p.Pagamento.Trim().Equals("01/01/0001") ? "" : p.Pagamento.Trim().Contains("INCORP") ? "INCORP" : p.Pagamento.Trim()).PadRight(10, ' '), p.NumeroPrazo.Trim().PadLeft(3, '0'));
                                    strAlta += string.Format("{0}{1}", p.Prestacao.Trim().PadLeft(18, '0'), p.Seguro.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.Taxa.Trim().PadLeft(18, '0'), Regex.Replace(p.Fgts.Trim(), @"[^0-9$]+", "").PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.AmortizacaoCorrecao.Trim().PadLeft(17, '0'), "+" + p.SaldoDevedorCorrecao.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Regex.Replace(p.Encargo.Trim(), @"[^0-9$]+", "").PadLeft(18, '0'), p.Pago.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", p.Juros.Trim().PadLeft(18, '0'), p.Mora.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", Regex.Replace(p.Amortizacao, @"[^0-9$]+", "").PadLeft(17, '0')+ _sinal,  p.SaldoDevedor.Trim().PadLeft(18, '0'));
                                    strAlta += string.Format("{0}{1}", "0".PadLeft(4, '0'), "0".PadLeft(10, '0'));
                                    strAlta += string.Format("{0}{1}{2}", p.Proc_Emi_Pag.Trim().PadLeft(20, '0'), (hasOcorrencia ? p.NumeroPrazo.Substring(0, 3) : "0".PadLeft(3, '0')), p.Iof.PadLeft(12, '0'));

                                    escreverParcelas.WriteLine(strAlta);

                                    p.Carteira = q.Carteira;
                                    p.Indicador = _sinal;
                                    p.Contrato = Convert.ToInt32(q.Carteira) + q.Contrato;
                                    p.DataBaseContrato = q.Cabecalhos[0].DataBase;

                                   
                                    parcelas.Add(p);

                                    strAlta = string.Empty;
                                }
                                catch (Exception exp)
                                {
                                    ExceptionError.TrataErros(exp, q.Contrato, "Metodo: PopulaContrato - Classe: BusinessCabecalho - Ação: Erro gerado ao tentar escrever a parcela no arquivo TL16PARC.txt", _diretorioDestino);
                                    throw exp;
                                }
                            }
                        });
                        strAlta = string.Empty;
                    });

                    //cria tabela para adicionar os valore e fazer o BulkInsert
                    businessParcelas.DoBulkCopy(true, parcelas);
                    parcelas = null;

                }
                //===============================================================================================================
                #endregion
                strAlta = string.Empty;
            }
            #endregion

            strAlta = string.Empty;

            #region BLOCO QUE GERA O ARQUIVO DE CRONOGRAMA
            //======================= BLOCO QUE GERA O ARQUIVO DE CRONOGRAMA ===================================================

            if (lstContratosPdf.Any(xp => xp.Cronogramas.Count > 0))
            {
                using (StreamWriter escreveCronograma = new StreamWriter(_diretorioDestino + @"\TL16CRON.txt", true, Encoding.Default))
                {
                    lstContratosPdf.ForEach(p =>
                    {
                        p.Cronogramas.ForEach(cron =>
                        {
                            escreveCronograma.WriteLine(string.Format("{0}CRONOGRAMA", cron).PadRight(84, ' '));
                        });
                    });
                }
            }
            #endregion

            lstContratosPdf = null;
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
