using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public static class ExceptionError
    {
        public static int countError;
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


        public static void RemoverTela(FileInfo fileInfoPdf, string _diretorioOrigemPdf)
        {
            string _pathContratoError = string.Empty;
            List<string> tela = null;
            string[] _arraTelas = { "TELA16", "TELA18", "TELA20", "TELA25" };

            // Renomeia o contrato da tela atual para extensão .err
            if (fileInfoPdf.Exists)
                File.Move(fileInfoPdf.FullName, System.IO.Path.ChangeExtension(fileInfoPdf.FullName, ".err"));


            foreach (string itemTela in _arraTelas)
            {
                string filtro = string.Format("*_{0}.pdf", Regex.Replace(itemTela, @"[^0-9$]", ""));
                tela = Directory.EnumerateFiles(_diretorioOrigemPdf, filtro, SearchOption.AllDirectories).ToList();
                string _contratoPedf = string.Format("{0}{1}", fileInfoPdf.Name.Split('_')[0], filtro).Replace("*", string.Empty);
                _pathContratoError = tela.FirstOrDefault(p => p.Contains(_contratoPedf.ToUpper()));

                if (!string.IsNullOrWhiteSpace(_pathContratoError))
                {
                    fileInfoPdf = new FileInfo(_pathContratoError);
                    if (fileInfoPdf.Exists)
                        File.Move(fileInfoPdf.FullName, System.IO.Path.ChangeExtension(fileInfoPdf.FullName, ".err"));
                }
            }

        }
    }
}
