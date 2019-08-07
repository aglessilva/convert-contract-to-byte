namespace ConvetPdfToLayoutAlta.Models
{
    public class Ocorrencia
    {
        public Ocorrencia(){}

        public Ocorrencia(Ocorrencia _ocorrencia)
        {
            IdCabecalho = _ocorrencia.IdCabecalho;
            IdParcela = _ocorrencia.IdParcela;
            Contrato = string.IsNullOrWhiteSpace(_ocorrencia.Contrato) ? "" : _ocorrencia.Contrato;
            Vencimento = string.IsNullOrWhiteSpace(_ocorrencia.Vencimento) ? "" : _ocorrencia.Vencimento;
            Pagamento = string.IsNullOrWhiteSpace(_ocorrencia.Pagamento) ? "" : _ocorrencia.Pagamento;
            Amortizacao = string.IsNullOrWhiteSpace(_ocorrencia.Amortizacao) ? "" : _ocorrencia.Amortizacao;
            Juros = string.IsNullOrWhiteSpace(_ocorrencia.Juros) ? "" : _ocorrencia.Juros;
            SaldoDevedor = string.IsNullOrWhiteSpace(_ocorrencia.SaldoDevedor) ? "" : _ocorrencia.SaldoDevedor;
            FGTS = string.IsNullOrWhiteSpace(_ocorrencia.FGTS) ? "" : _ocorrencia.FGTS;
            Damp = string.IsNullOrWhiteSpace(_ocorrencia.Damp) ? "" : _ocorrencia.Damp;
            CodigoOcorrencia = string.IsNullOrWhiteSpace(_ocorrencia.CodigoOcorrencia) ? "" : _ocorrencia.CodigoOcorrencia;
            Descricao = string.IsNullOrWhiteSpace(_ocorrencia.Descricao) ? "" : _ocorrencia.Descricao;
        }

        public int IdCabecalho { get; set; }
        public int IdParcela { get; set; }
        public string Contrato { get; set; }
        public string Vencimento { get; set; }
        public string Pagamento { get; set; }
        public string Amortizacao { get; set; }
        public string Juros { get; set; }
        public string SaldoDevedor { get; set; }
        public string FGTS { get; set; }
        public string Damp { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string Descricao { get; set; }
        public bool NaoTemParcela { get; set;}
        public bool IsNovoPrazo { get; set; }
        public string NovoNumeroPrazo { get; set; }
    }
}
