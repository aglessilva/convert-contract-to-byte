using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvetPdfToLayoutAlta.Models
{
    public class Parcela
    {
        public Parcela()
        {
        }

        [NotMapped]
        public bool IsAnt { get; set; }

        public int Id { get; set; }

        [NotMapped]
        public int IdCabecalho { get; set; }
        public string Agencia { get; set; }
        public string Amortizacao { get; set; }
        public string AmortizacaoCorrecao { get; set; }
        public string Banco { get; set; }
        public string Carteira { get; set; }
        public string Contrato { get; set; }
        public string DataBaseContrato { get; set; }

        [NotMapped]
        public string DataVencimentoAnterior { get; set; }
        public string Encargo { get; set; }
        public string Fgts { get; set; }
        public string Indicador { get; set; }
        public string Indice { get; set; }
        public string IndiceCorrecao { get; set; }
        public string Iof { get; set; }
        public string Juros { get; set; }
        public string Mora { get; set; }
        public string NumeroPrazo { get; set; }
        public string Pagamento { get; set; }
        public string Pago { get; set; }
        public string Prestacao { get; set; }
        public string Proc_Emi_Pag { get; set; }
        public string SaldoDevedor { get; set; }
        public string SaldoDevedorCorrecao { get; set; }
        public string Seguro { get; set; }
        public string TPG_EVE_HIS { get; set; }
        public string Taxa { get; set; }
        public string Vencimento { get; set; }
        public string VencimentoCorrecao { get; set; }
    }
}
