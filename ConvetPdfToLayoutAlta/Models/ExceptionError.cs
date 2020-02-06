using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public static void SemNumeroDamp(FileInfo fileInfo, string _path, string _diretorioOrigemPdf)
        {
            using (StreamWriter sw = new StreamWriter(_path + @"\LogErroContratos.txt", true, Encoding.UTF8))
            {

                StringBuilder strErro = new StringBuilder();
                strErro.AppendLine(string.Format("CONTRATO: {0}", fileInfo.Name.Split('_')[0]))
                        .AppendLine("DETALHES: Tela 18 não possui número de DAMP, favor informar ao gestor");
                sw.Write(strErro);
                sw.WriteLine("================================================================================================================================================");
            }

            List<string> tela = null;

            string filtro = $"*{fileInfo.Name.Split('_')[0]}*.pdf";
            tela = Directory.EnumerateFiles(_diretorioOrigemPdf, filtro, SearchOption.AllDirectories).ToList();

            foreach (string itemTela in tela)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(itemTela))
                    {
                        fileInfo = new FileInfo(itemTela);
                        if (fileInfo.Exists)
                            File.Move(fileInfo.FullName, Path.ChangeExtension(fileInfo.FullName, ".damp"));
                    }
                }
                catch (Exception exeErr)
                {
                    string t = exeErr.Message;
                }
            }
        }

        public static void NovoContratoGT(string _contrato, string _path)
        {
            using (StreamWriter sw = new StreamWriter(_path + @"\ARQ_GARANTIA.arq", true, Encoding.UTF8))
            {
                if (_contrato != "0")
                    sw.WriteLine(_contrato);
            }
        }


        public static void RemoverTela(FileInfo fileInfoPdf, string _diretorioOrigemPdf)
        {
            string _pathContratoError = string.Empty;
            List<string> tela = null;

            string filtro = $"*{fileInfoPdf.Name.Split('_')[0]}*.pdf";
            tela = Directory.EnumerateFiles(_diretorioOrigemPdf, filtro, SearchOption.AllDirectories).ToList();

            foreach (string itemTela in tela)
            {
                if (!string.IsNullOrWhiteSpace(itemTela))
                {
                    fileInfoPdf = new FileInfo(itemTela);
                    if (fileInfoPdf.Exists)
                        File.Move(fileInfoPdf.FullName, System.IO.Path.ChangeExtension(fileInfoPdf.FullName, ".err"));
                }
            }
        }



        public static void GerarPonteiro(string _diretorioPdf)
        {
            IEnumerable<string> fileContract = Directory.EnumerateFiles(_diretorioPdf, "*_16.*", SearchOption.AllDirectories);

            FileInfo f = null;
            using (StreamWriter sw = new StreamWriter($@"{Directory.GetCurrentDirectory()}\config\ARQUPONT.txt", true, Encoding.ASCII))
            {
                fileContract.ToList().ForEach(w =>
                {
                    f = new FileInfo(w);
                    sw.WriteLine(f.Name.Split('_')[0]);
                });
            }
        }
    }
}
