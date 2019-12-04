using ConvetPdfToLayoutAlta.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConvetPdfToLayoutAlta.FluentApi
{
    public class FluentApiFgts : EntityTypeConfiguration<ParcelaFgts>
    {
        public FluentApiFgts()
        {
            ToTable("DampFgts");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasIndex(p => p.Contrato);


            Property(p => p.TipoLinha).HasMaxLength(3);
            Property(p => p.ParcelaQuota).HasMaxLength(8);
            Property(p => p.DataVencimento).HasMaxLength(18);
            Property(p => p.QuotaNominal).HasMaxLength(18);
            Property(p => p.SaldoFgtsJAM).HasMaxLength(18);
            Property(p => p.SaldoFgtsQUO).HasMaxLength(18);
            Property(p => p.SobraMes).HasMaxLength(18);
            Property(p => p.SobraMesJAM).HasMaxLength(18);
            Property(p => p.SobraAcumulada).HasMaxLength(18);
        }
    }
}
