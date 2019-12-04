using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            Id = 0;
            Contrato = "";
            TipoLinha = "";
            ParcelaQuota = "0";
            QuotaNominal = "0";
            SaldoFgtsJAM = "0";
            SaldoFgtsQUO = "0";
            SobraMes = "0";
            SobraMesJAM = "0";
            SobraAcumulada = "0";
            DataVencimento = "0";
            ValorUtilizado = "0";
        }

        public int Id { get; set; }
        public string Contrato { get; set; }
        public string TipoLinha { get; set; }
        public string ParcelaQuota { get; set; }
        public string DataVencimento { get; set; }
        public string QuotaNominal { get; set; }
        public string SaldoFgtsJAM { get; set; }
        public string SaldoFgtsQUO { get; set; }
        public string SobraMes { get; set; }
        public string SobraMesJAM { get; set; }
        public string SobraAcumulada { get; set; }
        [NotMapped]
        public string SobraAcumuladaJAM { get; set; }
        public string ValorUtilizado { get; set; }
    }
   
    public class ItensDamp
    {
        public string Contrato { get; set; }
        public string DataVencimento { get; set; }
    }

}
