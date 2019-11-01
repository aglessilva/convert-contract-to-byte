using ConvetPdfToLayoutAlta.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConvetPdfToLayoutAlta.FluentApi
{
    public class FluentApiParcelas : EntityTypeConfiguration<Parcela>
    {
        public FluentApiParcelas()
        {
            ToTable("Parcelas");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasIndex(p => p.Contrato);

            Property(p => p.Agencia).HasMaxLength(10);
            Property(p => p.Carteira).HasMaxLength(5);
            Property(p => p.Contrato).HasMaxLength(20);
            Property(p => p.Vencimento).HasMaxLength(20);
            Property(p => p.VencimentoCorrecao).HasMaxLength(20);
            Property(p => p.DataBaseContrato).HasMaxLength(20);
            Property(p => p.Indice).HasMaxLength(10);
            Property(p => p.IndiceCorrecao).HasMaxLength(10);
            Property(p => p.Pagamento).HasMaxLength(20);
            Property(p => p.NumeroPrazo).HasMaxLength(10);
            Property(p => p.Prestacao).HasMaxLength(20);
            Property(p => p.Seguro).HasMaxLength(20);
            Property(p => p.TPG_EVE_HIS).HasMaxLength(5);
            Property(p => p.Taxa).HasMaxLength(20);
            Property(p => p.Fgts).HasMaxLength(20);
            Property(p => p.AmortizacaoCorrecao).HasMaxLength(20);
            Property(p => p.Banco).HasMaxLength(20);
            Property(p => p.SaldoDevedorCorrecao).HasMaxLength(20);
            Property(p => p.Encargo).HasMaxLength(20);
            Property(p => p.Pago).HasMaxLength(20);
            Property(p => p.Juros).HasMaxLength(20);
            Property(p => p.Mora).HasMaxLength(20);
            Property(p => p.Amortizacao).HasMaxLength(20);
            Property(p => p.Indicador).HasMaxLength(1);
            Property(p => p.SaldoDevedor).HasMaxLength(20);
            Property(p => p.Proc_Emi_Pag).HasMaxLength(25);
            Property(p => p.Iof).HasMaxLength(20);
        }
    }
}
