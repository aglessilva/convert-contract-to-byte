using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public class ContratoPdf
    {
        public ContratoPdf()
        {
            this.Cabecalhos = new List<Cabecalho>();
            this.Parcelas = new List<Parcela>();
            this.Ocorrencias = new List<Ocorrencia>();
        }
        public string Contrato { get; set; }
        public List<Cabecalho> Cabecalhos { get; set; }
        public List<Parcela> Parcelas { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }
    }
}
