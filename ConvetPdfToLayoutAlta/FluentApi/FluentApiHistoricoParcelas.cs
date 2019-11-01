using ConvetPdfToLayoutAlta.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConvetPdfToLayoutAlta.FluentApi
{
    public class FluentApiHistoricoParcelas : EntityTypeConfiguration<HistoricoParcela>
    {
        public FluentApiHistoricoParcelas()
        {
            ToTable("HistoricoParcelas");
            HasKey(p => p.Id);
            HasIndex(p => p.IdentificacaoContrato);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.TipoArquivo).HasMaxLength(15);
            Property(p => p.DataReferenciaExtracao).HasMaxLength(15);
            Property(p => p.IdentificacaoContrato).HasMaxLength(15);
            Property(p => p.TipoRegistroContrato).HasMaxLength(15);
            Property(p => p.DataAmortizacaoParcela).HasMaxLength(15);
            Property(p => p.NumeroParcelaContrato).HasMaxLength(15);
            Property(p => p.Sinal0).HasMaxLength(1);
            Property(p => p.ValorAmortizacaoParcela).HasMaxLength(15);
            Property(p => p.Sinal1).HasMaxLength(1);
            Property(p => p.ValorJurosParcela).HasMaxLength(15);
            Property(p => p.Sinal2).HasMaxLength(1);
            Property(p => p.ValorSeguroMIPParcela).HasMaxLength(15);
            Property(p => p.Sinal3).HasMaxLength(1);
            Property(p => p.ValorSeguroDFIParcela).HasMaxLength(15);
            Property(p => p.Sinal4).HasMaxLength(1);
            Property(p => p.ValorTarifaParcela).HasMaxLength(15);
            Property(p => p.CodigoEstipulanteMIP).HasMaxLength(15);
            Property(p => p.RegiaoApoliceMIP).HasMaxLength(15);
            Property(p => p.MatriculaMIP).HasMaxLength(15);
            Property(p => p.AgenciaMIP).HasMaxLength(15);
            Property(p => p.EmpreendimentoMIP).HasMaxLength(15);
            Property(p => p.CodigoApoliceAtualContratoMIP).HasMaxLength(15);
            Property(p => p.CodigoEstipulanteDFI).HasMaxLength(15);
            Property(p => p.RegiaoApoliceDFI).HasMaxLength(15);
            Property(p => p.MatriculaDFI).HasMaxLength(15);
            Property(p => p.AgenciaDFI).HasMaxLength(15);
            Property(p => p.EmpreendimentoDFI).HasMaxLength(15);
            Property(p => p.CodigoApoliceAtualContratoDFI).HasMaxLength(15);
            Property(p => p.Sinal5).HasMaxLength(1);
            Property(p => p.ValorAmortizacaoParcelaCorrigida).HasMaxLength(15);
            Property(p => p.Sinal6).HasMaxLength(1);
            Property(p => p.ValorJurosParcelaCorrigida).HasMaxLength(15);
            Property(p => p.Sinal7).HasMaxLength(1);
            Property(p => p.ValorSeguroMIPParcelaCorrigida).HasMaxLength(15);
            Property(p => p.Sinal8).HasMaxLength(1);
            Property(p => p.ValorSeguroDFIParcelaCorrigida).HasMaxLength(15);
            Property(p => p.Sinal9).HasMaxLength(1);
            Property(p => p.ValorIOFSeguroMIP).HasMaxLength(15);
            Property(p => p.Sinal10).HasMaxLength(1);
            Property(p => p.ValorIOFSeguroDFI).HasMaxLength(15);
            Property(p => p.Sinal11).HasMaxLength(1);
            Property(p => p.ValorTarifaParcelaCorrigidaTSA).HasMaxLength(15);
            Property(p => p.Sinal12).HasMaxLength(1);
            Property(p => p.ValorAbatimentoFGTSDAMP3).HasMaxLength(15);
            Property(p => p.FatorCorrecaoMonetariaAplicada).HasMaxLength(15);
            Property(p => p.Sinal13).HasMaxLength(1);
            Property(p => p.ValorCorrecaoMonetariaEncargos).HasMaxLength(15);
            Property(p => p.Sinal14).HasMaxLength(1);
            Property(p => p.ValorJurosRemuneratorios).HasMaxLength(15);
            Property(p => p.Sinal15).HasMaxLength(1);
            Property(p => p.ValorJurosMoratorios).HasMaxLength(15);
            Property(p => p.Sinal16).HasMaxLength(1);
            Property(p => p.ValorCorrecaoMonetariaAtraso).HasMaxLength(15);
            Property(p => p.Sinal117).HasMaxLength(1);
            Property(p => p.ValorIncrementoDescontoOriginal).HasMaxLength(15);
            Property(p => p.Sinal18).HasMaxLength(1);
            Property(p => p.ValorIncrementoDescontoCorrigido).HasMaxLength(15);
            Property(p => p.Sinal19).HasMaxLength(1);
            Property(p => p.ValorRDM).HasMaxLength(15);
            Property(p => p.Sinal20).HasMaxLength(1);
            Property(p => p.SaldoAmortizado).HasMaxLength(15);
            Property(p => p.NumeroBoleto).HasMaxLength(15);
            Property(p => p.IndicativoParcelaPaga).HasMaxLength(15);
            Property(p => p.DataMovimentoPagamento).HasMaxLength(15);
            Property(p => p.DataProcessamentoPagamento).HasMaxLength(15);
            Property(p => p.FormaLiquidacao).HasMaxLength(15);
            Property(p => p.Sinal21).HasMaxLength(1);
            Property(p => p.ValorPago).HasMaxLength(15);
            Property(p => p.Sinal22).HasMaxLength(1);
            Property(p => p.ValorDescontoConcedido).HasMaxLength(15);
            Property(p => p.ValorDiferencaProximaParcela).HasMaxLength(15);
            Property(p => p.Sinal23).HasMaxLength(1);
            Property(p => p.ValorPrincipalSaldoDevedor).HasMaxLength(15);
            Property(p => p.ValorJurosSaldoDevedor).HasMaxLength(15);
            Property(p => p.Sinal24).HasMaxLength(1);
            Property(p => p.ValorCMSaldoDevedor).HasMaxLength(15);
            Property(p => p.CodigoTipoOcorrência).HasMaxLength(15);
            Property(p => p.Sinal25).HasMaxLength(1);
            Property(p => p.ValorOcorrencia).HasMaxLength(15);
            Property(p => p.IndicadorParcelaEmitida).HasMaxLength(15);
            Property(p => p.SistemaAmortizacao).HasMaxLength(15);
            Property(p => p.TaxaJurosEfetivoContrato).HasMaxLength(15);
            Property(p => p.TaxaJurosNominalContrato).HasMaxLength(15);
            Property(p => p.To08_Ind_Incorp).HasMaxLength(15);
            Property(p => p.To08_Vl_Quot_S).HasMaxLength(1);
            Property(p => p.To08_Vl_Quot).HasMaxLength(15);
            Property(p => p.To08_Sld_Fgts_Ant_S).HasMaxLength(1);
            Property(p => p.To08_Sld_Fgts_Ant).HasMaxLength(15);
            Property(p => p.To08_Sld_Fgts_Atu_S).HasMaxLength(1);
            Property(p => p.To08_Sld_Fgts_Atu).HasMaxLength(15);
            Property(p => p.To08_Sld_Sob_Acu_S).HasMaxLength(1);
            Property(p => p.To08_Sld_Sob_Acu).HasMaxLength(15);
            Property(p => p.To08_Sld_Soma_Sob_S).HasMaxLength(1);
            Property(p => p.To08_Sld_Soma_Sob).HasMaxLength(15);
            Property(p => p.Filler).HasMaxLength(100);
        }
    }
}
