using ConvetPdfToLayoutAlta.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConvetPdfToLayoutAlta.FluentApi
{
    public class FluentApiOcorrecias: EntityTypeConfiguration<OcorrenciaBulk>
    {
        public FluentApiOcorrecias()
        {
            ToTable("Ocorrencias");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasIndex(p => p.Contrato);

            Property(p => p.DataVencimento).HasMaxLength(10);
            Property(p => p.DataPagamento).HasMaxLength(10);
            Property(p => p.Simbulo).HasMaxLength(3);
            Property(p => p.CodigoOcorrencia).HasMaxLength(3);
            Property(p => p.Descricao).HasMaxLength(30);
            Property(p => p.Enc_Pago).HasMaxLength(18);
            Property(p => p.Juros).HasMaxLength(18);
            Property(p => p.Mora).HasMaxLength(18);
            Property(p => p.ValorAmortizado).HasMaxLength(18);
            Property(p => p.Sinal).HasMaxLength(1);
            Property(p => p.SaldoDevedor).HasMaxLength(18);
            Property(p => p.Alterado).HasMaxLength(30);
            Property(p => p.Sit_Anterior).HasMaxLength(30);
            Property(p => p.Sit_Atual).HasMaxLength(30);
            Property(p => p.Sit_Aux).HasMaxLength(30);
           
        }
    }


    public class OcorrenciaBulk
    {
        public int Id { get; set; }
        public string Contrato { get; set; }
        public string DataVencimento { get; set; }
        public string DataPagamento { get; set; }
        public string Simbulo { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string Descricao { get; set; }
        public string Enc_Pago { get; set; }
        public string Juros { get; set; }
        public string Mora { get; set; }
        public string ValorAmortizado { get; set; }
        public string Sinal { get; set; }
        public string SaldoDevedor { get; set; }
        public string Alterado { get; set; }
        public string Sit_Anterior { get; set; }
        public string Sit_Atual { get; set; }
        public string Sit_Aux { get; set; }
    }
}
