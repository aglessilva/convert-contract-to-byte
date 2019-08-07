using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public static class ExceptionError
    {
        public static void TrataErros(Exception exception, string _contrato, string _detalhes, string _path)
        {
            using (StreamWriter sw = new StreamWriter(_path + @"\LogErroContratos.txt", true, Encoding.UTF8))
            {
                string _descricaoErro = exception == null ? "NÃO INFORMADO" : exception.Message;

                StringBuilder strErro = new StringBuilder();
                strErro.AppendLine(string.Format("CONTRATO: {0}",_contrato))
                        .AppendLine(string.Format("DETALHES: {0}", _detalhes))
                        .AppendLine(string.Format("DESCRIÇÃO DO ERRO: {0}", _descricaoErro));
                sw.Write(strErro);
                sw.WriteLine("================================================================================================================================================");
            }

        }


        public static void TrataErros(string _contrato, string _detalhes, string _path)
        {
            using (StreamWriter sw = new StreamWriter(_path + @"\LogErroContratos.txt", true, Encoding.UTF8))
            {

                StringBuilder strErro = new StringBuilder();
                strErro.AppendLine(string.Format("CONTRATO: {0}", _contrato))
                        .AppendLine(string.Format("DETALHES: {0}", _detalhes));
                sw.Write(strErro);
                sw.WriteLine("================================================================================================================================================");
            }

        }

        public static void NovoContratoGT(string _contrato, string _path)
        {
            using (StreamWriter sw = new StreamWriter(_path + @"\NOVOS_CONTRATOS.txt", true, Encoding.UTF8))
            {
                if (_contrato != "0")
                    sw.WriteLine(_contrato);
            }

        }
    }
}
