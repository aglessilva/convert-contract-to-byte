using ErikEJ.SqlCe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ConvetPdfToLayoutAlta.FluentApi;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessHistoricoParcelas
    {

        public void AddHistoricoParcelas(object _dataTable)
        {
            DataTable dataTable = (DataTable)_dataTable.GetType().GetProperty("item1").GetValue(_dataTable, null);
            SqlCeBulkCopyOptions options = new SqlCeBulkCopyOptions();

            if (true)
            {
                options = options |= SqlCeBulkCopyOptions.KeepNulls;
            }

            try
            {
                using (DbConnEntity connEntity = new DbConnEntity())
                {
                    using (SqlCeBulkCopy bc = new SqlCeBulkCopy(connEntity.Database.Connection.ConnectionString.ToString(), options))
                    {
                        bc.DestinationTableName = "HistoricoParcelas";
                        bc.WriteToServer(dataTable);
                    }
                }

                dataTable = null;
            }
            catch (Exception exdb)
            {
                throw new Exception("Erro ao tentar converter objeto na função AddHistoricoParcelas: " + exdb.Message);
            }
        }

        public List<HistoricoParcela> GetHistoricoParcelas(string _numeroContrato)
        {
            try
            {
                using (DbConnEntity dbConnEntity = new DbConnEntity())
                {
                    return dbConnEntity.HistoricoParcelas.Where(hp => hp.IdentificacaoContrato.Equals(_numeroContrato.Trim())).ToList();
                }
            }

            catch (Exception sqlExe)
            {
                throw sqlExe;
            }

        }

        public List<OcorrenciaBulk> GetOcorrenciaBulks(string _numeroContrato)
        {
            try
            {
                List<OcorrenciaBulk> lst = null; 
                using (DbConnEntity dbConnEntity = new DbConnEntity())
                {
                    lst = dbConnEntity.Ocorrencias.Where(hp => hp.Contrato.Equals(_numeroContrato.Trim())).ToList();
                }

                return lst;
            }

            catch (Exception sqlExe)
            {
                throw sqlExe;
            }

        }

        public DataTable CriaTabelaBulkHistoricoParcelas()
        {
            var table = new DataTable();
            table.Columns.Add("TipoArquivo", typeof(char));
            table.Columns.Add("DataReferenciaExtracao", typeof(string));
            table.Columns.Add("IdentificacaoContrato", typeof(string));
            table.Columns.Add("TipoRegistroContrato", typeof(string));
            table.Columns.Add("DataAmortizacaoParcela", typeof(string));
            table.Columns.Add("NumeroParcelaContrato", typeof(string));
            table.Columns.Add("Sinal0", typeof(char));
            table.Columns.Add("ValorAmortizacaoParcela", typeof(string));
            table.Columns.Add("Sinal1", typeof(char));
            table.Columns.Add("ValorJurosParcela", typeof(string));
            table.Columns.Add("Sinal2", typeof(char));
            table.Columns.Add("ValorSeguroMIPParcela", typeof(string));
            table.Columns.Add("Sinal3", typeof(char));
            table.Columns.Add("ValorSeguroDFIParcela", typeof(string));
            table.Columns.Add("Sinal4", typeof(char));
            table.Columns.Add("ValorTarifaParcela", typeof(string));
            table.Columns.Add("CodigoEstipulanteMIP", typeof(string));
            table.Columns.Add("RegiaoApoliceMIP", typeof(string));
            table.Columns.Add("MatriculaMIP", typeof(string));
            table.Columns.Add("AgenciaMIP", typeof(string));
            table.Columns.Add("EmpreendimentoMIP", typeof(string));
            table.Columns.Add("CodigoApoliceAtualContratoMIP", typeof(string));
            table.Columns.Add("CodigoEstipulanteDFI", typeof(string));
            table.Columns.Add("RegiaoApoliceDFI", typeof(string));
            table.Columns.Add("MatriculaDFI", typeof(string));
            table.Columns.Add("AgenciaDFI", typeof(string));
            table.Columns.Add("EmpreendimentoDFI", typeof(string));
            table.Columns.Add("CodigoApoliceAtualContratoDFI", typeof(string));
            table.Columns.Add("Sinal5", typeof(char));
            table.Columns.Add("ValorAmortizacaoParcelaCorrigida", typeof(string));
            table.Columns.Add("Sinal6", typeof(char));
            table.Columns.Add("ValorJurosParcelaCorrigida", typeof(string));
            table.Columns.Add("Sinal7", typeof(char));
            table.Columns.Add("ValorSeguroMIPParcelaCorrigida", typeof(string));
            table.Columns.Add("Sinal8", typeof(char));
            table.Columns.Add("ValorSeguroDFIParcelaCorrigida", typeof(string));
            table.Columns.Add("Sinal9", typeof(char));
            table.Columns.Add("ValorIOFSeguroMIP", typeof(string));
            table.Columns.Add("Sinal10", typeof(char));
            table.Columns.Add("ValorIOFSeguroDFI", typeof(string));
            table.Columns.Add("Sinal11", typeof(char));
            table.Columns.Add("ValorTarifaParcelaCorrigidaTSA", typeof(string));
            table.Columns.Add("Sinal12", typeof(char));
            table.Columns.Add("ValorAbatimentoFGTSDAMP3", typeof(string));
            table.Columns.Add("FatorCorrecaoMonetariaAplicada", typeof(string));
            table.Columns.Add("Sinal13", typeof(char));
            table.Columns.Add("ValorCorrecaoMonetariaEncargos", typeof(string));
            table.Columns.Add("Sinal14", typeof(char));
            table.Columns.Add("ValorJurosRemuneratorios", typeof(string));
            table.Columns.Add("Sinal15", typeof(char));
            table.Columns.Add("ValorJurosMoratorios", typeof(string));
            table.Columns.Add("Sinal16", typeof(char));
            table.Columns.Add("ValorCorrecaoMonetariaAtraso", typeof(string));
            table.Columns.Add("Sinal117", typeof(char));
            table.Columns.Add("ValorIncrementoDescontoOriginal", typeof(string));
            table.Columns.Add("Sinal18", typeof(char));
            table.Columns.Add("ValorIncrementoDescontoCorrigido", typeof(string));
            table.Columns.Add("Sinal19", typeof(char));
            table.Columns.Add("ValorRDM", typeof(string));
            table.Columns.Add("Sinal20", typeof(char));
            table.Columns.Add("SaldoAmortizado", typeof(string));
            table.Columns.Add("NumeroBoleto", typeof(string));
            table.Columns.Add("IndicativoParcelaPaga", typeof(string));
            table.Columns.Add("DataMovimentoPagamento", typeof(string));
            table.Columns.Add("DataProcessamentoPagamento", typeof(string));
            table.Columns.Add("FormaLiquidacao", typeof(string));
            table.Columns.Add("Sinal21", typeof(char));
            table.Columns.Add("ValorPago", typeof(string));
            table.Columns.Add("Sinal22", typeof(char));
            table.Columns.Add("ValorDescontoConcedido", typeof(string));
            table.Columns.Add("ValorDiferencaProximaParcela", typeof(string));
            table.Columns.Add("Sinal23", typeof(char));
            table.Columns.Add("ValorPrincipalSaldoDevedor", typeof(string));
            table.Columns.Add("ValorJurosSaldoDevedor", typeof(string));
            table.Columns.Add("Sinal24", typeof(char));
            table.Columns.Add("ValorCMSaldoDevedor", typeof(string));
            table.Columns.Add("CodigoTipoOcorrência", typeof(string));
            table.Columns.Add("Sinal25", typeof(char));
            table.Columns.Add("ValorOcorrencia", typeof(string));
            table.Columns.Add("IndicadorParcelaEmitida", typeof(string));
            table.Columns.Add("SistemaAmortizacao", typeof(string));
            table.Columns.Add("TaxaJurosEfetivoContrato", typeof(string));
            table.Columns.Add("TaxaJurosNominalContrato", typeof(string));
            table.Columns.Add("To08_Ind_Incorp", typeof(string));
            table.Columns.Add("To08_Vl_Quot_S", typeof(char));
            table.Columns.Add("To08_Vl_Quot", typeof(string));
            table.Columns.Add("To08_Sld_Fgts_Ant_S", typeof(char));
            table.Columns.Add("To08_Sld_Fgts_Ant", typeof(string));
            table.Columns.Add("To08_Sld_Fgts_Atu_S", typeof(char));
            table.Columns.Add("To08_Sld_Fgts_Atu", typeof(string));
            table.Columns.Add("To08_Sld_Sob_Acu_S", typeof(char));
            table.Columns.Add("To08_Sld_Sob_Acu", typeof(string));
            table.Columns.Add("To08_Sld_Soma_Sob_S", typeof(char));
            table.Columns.Add("To08_Sld_Soma_Sob", typeof(string));
            table.Columns.Add("Filler", typeof(string));

            return table;
        }

    }
}
