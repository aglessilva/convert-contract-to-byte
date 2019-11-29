using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{

    public class Tela18
    {
        public Tela18()
        {
            Contrato = "";
            Carteira = "";
            Damps = new List<Damp>();
        }
        public string Contrato { get; set; }
        public string Carteira { get; set; }
        public List<Damp> Damps { get; set; }
    }

    public class Damp
    {
        public Damp()
        {
            NumeroDamp = "";
            Inicio = "";
            Quantidade = "";
            ValorDamp = "";
            Percentual = "";
            SaldoInicial = "";
            Quota = "";
            ParcelaFgts = new List<ParcelaFgts>();
        }
        public string NumeroDamp { get; set; }
        public string Inicio { get; set; }
        public string Quantidade { get; set; }
        public string ValorDamp { get; set; }
        public string Percentual { get; set; }
        public string SaldoInicial { get; set; }
        public string Quota { get; set; }
        public List<ParcelaFgts> ParcelaFgts { get; set; }
    }


    public class ParcelaFgts
    {
        public ParcelaFgts()
        {
            TipoLinha = "";
            ParcelaQuota = "";
            QuotaNominal = "";
            SaldoFgtsJAM = "";
            SaldoFgtsQUO = "";
            SobraMes = "";
            SobraMesJAM = "0";
            SobraAcumulada = "";
            DataPagamento = "";
            ValorUtilizado = "";
            PercentualAbatimento = "";
        }
        public string TipoLinha { get; set; }
        public string ParcelaQuota { get; set; }
        public string DataVencimento { get; set; }
        public string QuotaNominal { get; set; }
        public string SaldoFgtsJAM { get; set; }
        public string SaldoFgtsQUO { get; set; }
        public string SobraMes { get; set; }
        public string SobraMesJAM { get; set; }
        public string SobraAcumulada { get; set; }
        public string SobraAcumuladaJAM { get; set; }
        public string DataPagamento { get; set; }
        public string ValorUtilizado { get; set; }
        public string PercentualAbatimento { get; set; }
    }
   

}
