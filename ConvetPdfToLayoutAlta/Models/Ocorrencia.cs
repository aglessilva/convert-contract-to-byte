namespace ConvetPdfToLayoutAlta.Models
{
    public class Ocorrencia
    {
        public int IdCabecalho { get; set; }
        public int IdParcela { get; set; }
        public string Contrato { get; set; }
        public string Vencimento { get; set; }
        public string Pagamento { get; set; }
        public string Amortizacao { get; set; }
        public string Juros { get; set; }
        public string SaldoDevedor { get; set; }
        public string FGTS { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string Descricao { get; set; }
    }
}
