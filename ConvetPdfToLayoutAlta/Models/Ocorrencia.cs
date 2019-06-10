using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public class Ocorrencia
    {
        public string Contrato { get; set; }
        public string Vencimento { get; set; }
        public string Pagamento { get; set; }
        public string Amortizacao { get; set; }
        public string Mora { get; set; }
        public string SaldoDevedor { get; set; }
        public string FGTS { get; set; }
        public string Dump { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string Descricao { get; set; }
    }
}
