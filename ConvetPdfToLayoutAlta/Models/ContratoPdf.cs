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
            Cabecalhos = new List<Cabecalho>();
            Parcelas = new List<Parcela>();
            Ocorrencias = new List<Ocorrencia>();
          
        }
        public string Carteira { get; set; }
        public string Contrato { get; set; }
        public List<Cabecalho> Cabecalhos { get; set; }
        public List<Parcela> Parcelas { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }
       
    }
}
