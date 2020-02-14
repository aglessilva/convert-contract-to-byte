using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public class ContratoPdf
    {
        public ContratoPdf()
        {
            Cabecalhos = new List<Cabecalho>();
            Parcelas = new List<Parcela>();
            Ocorrencias = new List<Ocorrencia>();
            Cronogramas = new List<string>();
           
        }
        public string Carteira { get; set; }
        public string Contrato { get; set; }
        public string Bem { get; set; }
        public List<Cabecalho> Cabecalhos { get; set; }
        public List<Parcela> Parcelas { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }
        public List<ContratoPdf> ContratoPdfs { get; set; }
        public List<string> Cronogramas { get; set; }
    }

    public class UserObject
    {
        public string Contrato { get; set; }
        public FileInfo PdfInfo { get; set; }
        public int TotalArquivoPorPasta { get; set; }
        public string  DescricaoPercentural { get; set; }
    }
       
}
