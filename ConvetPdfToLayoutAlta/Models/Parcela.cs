﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public class Parcela
    {
        public Parcela()
        {
           
        }
        public bool IsCorrecao { get; set; }
        public string VencimentoCorrecao { get; set; }
        public string VencimentoAnterior { get; set; }
        public string Vencimento { get; set; }
        public string Pagamento { get; set; }
        public string NumeroPrazo { get; set; }
        public string Indice { get; set; }
        public string IndiceCorrecao { get; set; }
        public string Prestacao { get; set; }
        public string Seguro { get; set; }
        public string Taxa { get; set; }
        public string Encargo { get; set; }
        public string Juros { get; set; }
        public string Amortizacao { get; set; }
        public string AmortizacaoCorrecao { get; set; }
        public string SaldoDevedor { get; set; }
        public string SaldoDevedorAntes { get; set; }
        public string SaldoDevedorCorrecao { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string TPG_EVE_HIS { get; set; }
        public string Proc_Emi_Pag { get; set; }
        public string Pago { get; set; }
        public string Mora { get; set; }

    }
}
