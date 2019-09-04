using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public class Cabecalho
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string DataBase { get; set; }
        /// <summary>
        /// Posicionamento: 000,2
        /// </summary>
        public string Carteira { get; set; }
        public string DataEmicao { get; set; }
        /// <summary>
        /// Carteira + Contrato
        /// Posicionamento: 001,15
        /// </summary>
        public string Contrato { get; set; }
        /// <summary>
        /// Nome
        /// Posicionamento: 025,40  - Complementar com espaço a direita
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Cpf
        /// Posicionamento: 169,17  - Complementar com espaço a direita
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// DataNascimento
        /// Posicionamento: 065,24  - Complementar com espaço a direita
        /// </summary>
        public string DataNascimento { get; set; }

        /// <summary>
        /// EnderecoImovel
        /// Posicionamento: 089,80  - Complementar com espaço a direita
        /// </summary>                                                        
        public string EnderecoImovel { get; set; }

        public string BairroImovel { get; set; }
        public string CepImovel { get; set; }
        public string CidadeImovel { get; set; }
        public string UfImovel { get; set; }
        public string Cliente { get; set; }
        public string TelefoneResidencia { get; set; }
        public string TelefoneComercial { get; set; }
        public string Categoria { get; set; }
        public string Modalidade { get; set; }
        public string ContaDeposito { get; set; }
        public string TxCETAno { get; set; }
        public string TxCEMes { get; set; }
        public string Cartorio { get; set; }
        public string Iof { get; set; }
        public string Pis { get; set; }
        public string DataCaDoc { get; set; }
        public string CAC { get; set; }
        public string Plano { get; set; }
        public string DataContrato { get; set; }
        public string OrigemRecurso { get; set; }
        public string FgtsUtilizado { get; set; }
        public string DescontoAquisicao { get; set; }
        public string Remuneracao { get; set; }
        public string Comissao { get; set; }
        public string Prestacao { get; set; }
        public string Sistema { get; set; }
        public string ValorFinanciamento { get; set; }
        public string ValorGarantia { get; set; }
        /// <summary>
        /// Agencia
        /// Posicionamento: 018,07  - Complementar com espaço a direita
        /// </summary>
        public string Agencia { get; set; }
        public string CodigoContabil { get; set; }
        public string SeguroMIP { get; set; }
        public string SeguroDFI { get; set; }
        public string RemunDifJuros { get; set; }
        public string Reajuste { get; set; }
        public string DataGarantia { get; set; }
        public string Empreendimento { get; set; }
        public string Taxa { get; set; }
        public string TaxaJuros { get; set; }
        /// <summary>
        /// DataPrimeiroVencimento
        /// Posicionamento: 016,02
        /// </summary>
        public string DataPrimeiroVencimento { get; set; }
        public string Apolice { get; set; }
        public string Razao { get; set; }
        public string Correcao { get; set; }
        public string DataInclusao { get; set; }
        public string DataReinclusao { get; set; }
        public string TipoFinanciamento { get; set; }
        public string DataUltimaAlteracao { get; set; }
        public string Repactuacao { get; set; }
        public string Carencia { get; set; }
        public string Prazo { get; set; }
        public string TipoOrigem { get; set; }
        public string Situacao { get; set; }
        public string TaxaServico { get; set; }
        public string Lastro { get; set; }
        public string Custas { get; set; }
        public string Agio { get; set; }
        public string Prorrogacao { get; set; }
        public string SeguroVista { get; set; }






    }
}
