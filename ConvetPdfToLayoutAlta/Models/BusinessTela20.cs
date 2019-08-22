using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessTela20
    {
        public string[] GetArrayLine(string _line)
        {
            _line = Regex.Replace(_line, @"[^\wÀ-úa-zA-Z0-9.,\/\-$]+", " ");
            return _line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }


        public Tela20 GetValores(Tela20 obj, string _linha)
        {
            try
            {
                string[] _arrayLinha = { };

                if (_linha.Contains("ValorPago"))
                    _arrayLinha = _linha.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                else
                    _arrayLinha = _linha.Split(':');


                if (_arrayLinha.Any(l => l.Equals("Líquido")))
                {
                    obj.Liquido = Regex.Replace(_arrayLinha[1], @"[^0-9$]", "");
                    obj.ValorAtualizado = Regex.Replace(_arrayLinha[2], @"[^0-9\-$]", "");
                }

                if (_arrayLinha.Any(l => Regex.IsMatch(l.Trim(), @"(^\d{4}.\d{5}.\d{3}\-\d{1}$)")))
                {
                    obj.Contrato = _arrayLinha.FirstOrDefault(c => Regex.IsMatch(c.Trim(), @"(^\d{4}.\d{5}.\d{3}\-\d{1}$)"));
                    obj.Contrato = Regex.Replace(obj.Contrato, @"[^0-9$]", "");
                    obj.Carteira = Regex.Replace(_arrayLinha[1], @"[^0-9$]", "");
                }

                if (_arrayLinha.Any(l => l.Equals("ValorPago")))
                {
                    obj.ValorPago = Regex.Replace(_arrayLinha[0], @"[^0-9$]", "");
                }

                if (_linha.Contains("Motivo"))
                {
                    string[] item = _arrayLinha[1].Split('-');
                    obj.MotivoRejeicao = Regex.Replace(item[0].Trim() + item[1].Trim(), @"[^0-9a-zà-úA-ZÀ-Ú\.$]", " ").Replace("Observação","").Trim();
                    obj.IsOk = true;
                }

            }
            catch (Exception exArgument)
            {
                string err = "Erro na Tela 18 - Metodo: [GetValores] - Arquivo: [BusinessTela20]";
                throw new ArgumentOutOfRangeException(exArgument.Message, err);
            }

            return obj;
        }

        public void Populatela20(object parametro)
        {
            List<Tela20> lstContratosPdf = (List<Tela20>)parametro.GetType().GetProperty("item1").GetValue(parametro, null);
            string _diretorioDestino = (string)parametro.GetType().GetProperty("item2").GetValue(parametro, null);

            string strAlta = string.Empty;
            using (StreamWriter escreverPendencia = new StreamWriter(_diretorioDestino + @"\TL20PEND.txt", true, Encoding.Default))
            {
                string _valorAtualizado = string.Empty;
                lstContratosPdf.ForEach(t20 =>
                {
                    if (t20.ValorAtualizado.Contains("-"))
                        _valorAtualizado = "-" + t20.ValorAtualizado.PadLeft(17, '0').Replace("-", "0");
                    else
                        _valorAtualizado = t20.ValorAtualizado.PadLeft(18, '0');

                    strAlta = string.Empty;
                    t20.MotivoRejeicao = t20.MotivoRejeicao.Trim().Length > 49 ? t20.MotivoRejeicao.Trim().Substring(0, 50).Trim() : t20.MotivoRejeicao.Trim();
                    strAlta += string.Format("{0}{1}{2}{3}", (t20.Carteira.Substring(2)+ t20.Contrato).PadLeft(15,'0'), _valorAtualizado , t20.Liquido.PadLeft(18, '0'), t20.Acrescimo.PadLeft(18, '0'));
                    strAlta += string.Format("{0}{1}",t20.ValorPago.PadLeft(18,'0') ,t20.MotivoRejeicao.Trim().Replace("Observaçã", "").PadRight(51, ' '));
                    strAlta = strAlta.PadRight(139, ' ');
                    escreverPendencia.WriteLine(strAlta);
                    _valorAtualizado = string.Empty;
                });
            }
        }
    }

}
