using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessTela25
    {

        public string[] GetArrayLine(string _line)
        {
            _line = Regex.Replace(_line, @"[^\wÀ-úa-zA-Z0-9.,\/\-$]+", " ");
            return _line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        public Tela25 TrataBoletim(Tela25 obj, string[] _arrayLinha)
        {
            try
            {
                if (_arrayLinha.Any(s => s.Trim().Equals("SANTANDER")))
                {
                    var x = new List<string>();

                    _arrayLinha.ToList().ForEach(i => { x.Add(Regex.Replace(i, @"[^0-9$]", "")); });
                    _arrayLinha = x.Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();

                    obj.Carteira = _arrayLinha.FirstOrDefault(f => Regex.IsMatch(f, @"(^\d{4}$)"));
                    obj.Contrato = Regex.Replace(_arrayLinha.LastOrDefault(), @"[^0-9$]", "");
                }

                if (_arrayLinha.Any(s => s.Trim().Equals("Empreendimento")))
                {
                    obj.Empreendimento = obj.Empreendimento == null ? _arrayLinha.LastOrDefault(e => Regex.IsMatch(e, @"(^\d{6}.\d{1}$)")) : obj.Empreendimento;
                }


                if (_arrayLinha.Any(cpf => Regex.IsMatch(cpf, @"(^\d{3}.\d{3}.\d{3}\-\d{2}$)")))
                {
                    obj.Cpf = _arrayLinha.First(cpf => Regex.IsMatch(cpf, @"(^\d{3}.\d{3}.\d{3}\-\d{2}$)")).Replace("-", ".");
                    obj.Agencia = _arrayLinha.First(cpf => Regex.IsMatch(cpf, @"(^\d{6}.\d{1}$)")).Replace(".", "");
                    if (_arrayLinha.Any(t => Regex.IsMatch(t, @"(^\d{6}.\d{1}$)")))
                        obj.Empreendimento = _arrayLinha.LastOrDefault(e => Regex.IsMatch(e, @"(^\d{6}.\d{1}$)"));
                   
                }

                if (_arrayLinha.Any(v => v.Trim().Equals("Movimento")))
                {
                    obj.CD = _arrayLinha[0];
                    obj.DataMovimentacao = _arrayLinha.First(d => Regex.IsMatch(d, @"(^\d{2}\/\d{2}\/\d{4}$)"));
                    obj.Tp = _arrayLinha[2];
                }

                if (_arrayLinha.Any(v => v.Trim().Equals("Vencimento")))
                {
                    obj.Vencimento = _arrayLinha[0];
                    obj.Prestacao = _arrayLinha[1];
                    obj.Empresa = _arrayLinha[2];
                    obj.Cart = _arrayLinha[3];
                    obj.NCPD = _arrayLinha[5];
                    obj.ValorSemAcrecimo = Regex.Replace(_arrayLinha[6], @"[^0-9$]", "");
                    obj.ValorCorrecao = Regex.Replace(_arrayLinha[7], @"[^0-9$]", "");
                    obj.ValorJuros = Regex.Replace(_arrayLinha[8], @"[^0-9$]", "");
                    obj.ValorMora = Regex.Replace(_arrayLinha[9], @"[^0-9$]", "");
                    obj.ValorMulta = Regex.Replace(_arrayLinha[10], @"[^0-9$]", "");
                    obj.ValorAcrecimo = Regex.Replace(_arrayLinha[11], @"[^0-9$]", "");
                    obj.ValorDevido = Regex.Replace(_arrayLinha[12], @"[^0-9$]", "");
                    obj.IsFinal = true;
                }

            }
            catch (Exception exArgument)
            {
                string err = "Erro na Tela 15 - Metodo: [TrataBoletim] - Arquivo: [BusinessTela25]";
                throw new ArgumentOutOfRangeException(exArgument.Message, err);
            }
            return obj;
        }

        public void PopulaTela25(object paramentro)
        {
            string strAlta = string.Empty;
            List<Tela25> lstContratosPdf = (List<Tela25>)paramentro.GetType().GetProperty("item1").GetValue(paramentro, null);
            string _diretorioDestino = (string)paramentro.GetType().GetProperty("item2").GetValue(paramentro, null);

            using (StreamWriter escreverBoletim = new StreamWriter(_diretorioDestino + @"\TL25BOLE.txt", true, Encoding.Default))
            {
                lstContratosPdf.ForEach(t25 => {

                    try
                    {

                        strAlta = string.Empty;
                        strAlta += string.Format("{0}{1}", t25.Carteira.Substring(2), t25.Contrato).PadRight(65, ' ');
                        strAlta += string.Format("{0}{1}", t25.Cpf.Trim(), t25.Agencia.Trim().PadRight(14, '0')).PadRight(142, ' ');
                        strAlta += string.Format("{0}{1}", t25.CD.Trim().PadLeft(11, '_'), t25.DataMovimentacao.Trim().PadRight(15, '_'));
                        strAlta += string.Format("{0}{1}", t25.Tp.Trim().PadLeft(5, '0'), t25.ValorDevido.Trim().PadLeft(18, '0'));
                        strAlta += string.Format("{0}{1}", t25.ValorAcrecimo.Trim().PadLeft(18, '0'), t25.Vencimento.Trim());
                        strAlta += string.Format("{0}{1}", t25.Prestacao.Trim().PadLeft(3, '0'), t25.Empresa.Trim().PadLeft(2, '0'));
                        strAlta += string.Format("{0}{1}", t25.Cart.Trim().PadLeft(4, '0'), t25.Contrato.Trim());
                        strAlta += string.Format("{0}{1}", t25.NCPD.Trim(), t25.ValorSemAcrecimo.Trim().PadLeft(18, '0'));
                        strAlta += string.Format("{0}{1}", t25.ValorCorrecao.Trim().PadLeft(18, '0'), t25.ValorJuros.Trim().PadLeft(18, '0'));
                        strAlta += string.Format("{0}{1}", t25.ValorMora.Trim().PadLeft(18, '0'), t25.ValorMulta.Trim().PadLeft(18, '0'));
                        strAlta += string.Format("{0}", t25.ValorAcrecimo.PadLeft(18, '0'));

                        //strAlta = strAlta.PadRight(423, ' ');
                        escreverBoletim.WriteLine(strAlta);
                        strAlta = string.Empty;
                    }
                    catch (Exception execp25)
                    {
                        string _err0 = string.Format("Erro na tentantiva de escrever o arquivo TL25BOLE Arquivo: BusinessTela25 - Metodo: [PopulaTela25] - Detalhes: {0}", execp25.Message);
                        ExceptionError.TrataErros(execp25, t25.Contrato, _err0, _diretorioDestino);
                    }
                });
            }

        }

    }
}
