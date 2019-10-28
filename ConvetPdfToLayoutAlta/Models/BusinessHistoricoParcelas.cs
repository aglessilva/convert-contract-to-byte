using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConvetPdfToLayoutAlta.Models
{
    public class BusinessHistoricoParcelas
    {
        Conn conn = null;
        SqlCommand command = null;
        List<HistoricoParcela> historicoParcelas = null;

        public void AddHistoricoParcelas(object _historicoParcelas)
        {
            int ret = 0;
            List<HistoricoParcela> historicoParcelas = (List<HistoricoParcela>)_historicoParcelas.GetType().GetProperty("item1").GetValue(_historicoParcelas, null);
            SqlCommand command = (SqlCommand)_historicoParcelas.GetType().GetProperty("item2").GetValue(_historicoParcelas, null);

            DataTable dataTable = CriaTabelaBulkHistoricoParcelas();
            DataRow dataRow = null;

            try
            {

                SqlBulkCopy bulkCopy = new SqlBulkCopy(command.Connection,SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction,null);
                bulkCopy.DestinationTableName = "dbo.EXT_08_HIST_PARCELAS";

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                foreach (HistoricoParcela item in historicoParcelas)
                {
                    try
                    {
                        dataRow = dataTable.NewRow();

                        dataRow["TipoArquivo"] = item.TipoArquivo.Trim();
                        dataRow["DataReferenciaExtracao"] = item.DataReferenciaExtracao.Trim(); 
                        dataRow["IdentificacaoContrato"] = item.IdentificacaoContrato.Trim();
                        dataRow["TipoRegistroContrato"] = item.TipoRegistroContrato.Trim();
                        dataRow["DataAmortizacaoParcela"] = item.DataAmortizacaoParcela.Trim();
                        dataRow["NumeroParcelaContrato"] = item.NumeroParcelaContrato.Trim();
                        dataRow["Sinal0"] = item.Sinal0.Trim();
                        dataRow["ValorAmortizacaoParcela"] = item.ValorAmortizacaoParcela.Trim();
                        dataRow["Sinal1"] = item.Sinal1.Trim();
                        dataRow["ValorJurosParcela"] = item.ValorJurosParcela.Trim();
                        dataRow["Sinal2"] = item.Sinal2.Trim();
                        dataRow["ValorSeguroMIPParcela"] = item.ValorSeguroMIPParcela.Trim();
                        dataRow["Sinal3"] = item.Sinal3.Trim();
                        dataRow["ValorSeguroDFIParcela"] = item.ValorSeguroDFIParcela.Trim();
                        dataRow["Sinal4"] = item.Sinal4.Trim();
                        dataRow["ValorTarifaParcela"] = item.ValorTarifaParcela.Trim();
                        dataRow["CodigoEstipulanteMIP"] = item.CodigoEstipulanteMIP.Trim();
                        dataRow["RegiaoApoliceMIP"] = item.RegiaoApoliceMIP.Trim();
                        dataRow["MatriculaMIP"] = item.MatriculaMIP.Trim();
                        dataRow["AgenciaMIP"] = item.AgenciaMIP.Trim();
                        dataRow["EmpreendimentoMIP"] = item.EmpreendimentoMIP.Trim();
                        dataRow["CodigoApoliceAtualContratoMIP"] = item.CodigoApoliceAtualContratoMIP.Trim();
                        dataRow["CodigoEstipulanteDFI"] = item.CodigoEstipulanteDFI.Trim();
                        dataRow["RegiaoApoliceDFI"] = item.RegiaoApoliceDFI.Trim();
                        dataRow["MatriculaDFI"] = item.MatriculaDFI.Trim();
                        dataRow["AgenciaDFI"] = item.AgenciaDFI.Trim();
                        dataRow["EmpreendimentoDFI"] = item.EmpreendimentoDFI.Trim();
                        dataRow["CodigoApoliceAtualContratoDFI"] = item.CodigoApoliceAtualContratoDFI.Trim();
                        dataRow["Sinal5"] = item.Sinal5.Trim();
                        dataRow["ValorAmortizacaoParcelaCorrigida"] = item.ValorAmortizacaoParcelaCorrigida.Trim();
                        dataRow["Sinal6"] = item.Sinal6.Trim();
                        dataRow["ValorJurosParcelaCorrigida"] = item.ValorJurosParcelaCorrigida.Trim();
                        dataRow["Sinal7"] = item.Sinal7.Trim();
                        dataRow["ValorSeguroMIPParcelaCorrigida"] = item.ValorSeguroMIPParcelaCorrigida.Trim();
                        dataRow["Sinal8"] = item.Sinal8.Trim();
                        dataRow["ValorSeguroDFIParcelaCorrigida"] = item.ValorSeguroDFIParcelaCorrigida.Trim();
                        dataRow["Sinal9"] = item.Sinal9.Trim();
                        dataRow["ValorIOFSeguroMIP"] = item.ValorIOFSeguroMIP.Trim();
                        dataRow["Sinal10"] = item.Sinal10.Trim();
                        dataRow["ValorIOFSeguroDFI"] = item.ValorIOFSeguroDFI.Trim();
                        dataRow["Sinal11"] = item.Sinal11.Trim();
                        dataRow["ValorTarifaParcelaCorrigidaTSA"] = item.ValorTarifaParcelaCorrigidaTSA.Trim();
                        dataRow["Sinal12"] = item.Sinal12.Trim();
                        dataRow["ValorAbatimentoFGTSDAMP3"] = item.ValorAbatimentoFGTSDAMP3.Trim();
                        dataRow["FatorCorrecaoMonetariaAplicada"] = item.FatorCorrecaoMonetariaAplicada.Trim();
                        dataRow["Sinal13"] = item.Sinal13.Trim();
                        dataRow["ValorCorrecaoMonetariaEncargos"] = item.ValorCorrecaoMonetariaEncargos.Trim();
                        dataRow["Sinal14"] = item.Sinal14.Trim();
                        dataRow["ValorJurosRemuneratorios"] = item.ValorJurosRemuneratorios.Trim();
                        dataRow["Sinal15"] = item.Sinal15.Trim();
                        dataRow["ValorJurosMoratorios"] = item.ValorJurosMoratorios.Trim();
                        dataRow["Sinal16"] = item.Sinal16.Trim();
                        dataRow["ValorCorrecaoMonetariaAtraso"] = item.ValorCorrecaoMonetariaAtraso.Trim();
                        dataRow["Sinal117"] = item.Sinal117.Trim();
                        dataRow["ValorIncrementoDescontoOriginal"] = item.ValorIncrementoDescontoOriginal.Trim();
                        dataRow["Sinal18"] = item.Sinal18.Trim();
                        dataRow["ValorIncrementoDescontoCorrigido"] = item.ValorIncrementoDescontoCorrigido.Trim();
                        dataRow["Sinal19"] = item.Sinal19.Trim();
                        dataRow["ValorRDM"] = item.ValorRDM.Trim();
                        dataRow["Sinal20"] = item.Sinal20.Trim();
                        dataRow["SaldoAmortizado"] = item.SaldoAmortizado.Trim();
                        dataRow["NumeroBoleto"] = item.NumeroBoleto.Trim();
                        dataRow["IndicativoParcelaPaga"] = item.IndicativoParcelaPaga.Trim();
                        dataRow["DataMovimentoPagamento"] = item.DataMovimentoPagamento.Trim();
                        dataRow["DataProcessamentoPagamento"] = item.DataProcessamentoPagamento.Trim();
                        dataRow["FormaLiquidacao"] = item.FormaLiquidacao.Trim();
                        dataRow["Sinal21"] = item.Sinal21.Trim();
                        dataRow["ValorPago"] = item.ValorPago.Trim();
                        dataRow["Sinal22"] = item.Sinal22.Trim();
                        dataRow["ValorDescontoConcedido"] = item.ValorDescontoConcedido.Trim();
                        dataRow["ValorDiferencaProximaParcela"] = item.ValorDiferencaProximaParcela.Trim();
                        dataRow["Sinal23"] = item.Sinal23.Trim();
                        dataRow["ValorPrincipalSaldoDevedor"] = item.ValorPrincipalSaldoDevedor.Trim();
                        dataRow["ValorJurosSaldoDevedor"] = item.ValorJurosSaldoDevedor.Trim();
                        dataRow["Sinal24"] = item.Sinal24.Trim();
                        dataRow["ValorCMSaldoDevedor"] = item.ValorCMSaldoDevedor.Trim();
                        dataRow["CodigoTipoOcorrência"] = item.CodigoTipoOcorrência.Trim();
                        dataRow["Sinal25"] = item.Sinal25.Trim();
                        dataRow["ValorOcorrencia"] = item.ValorOcorrencia.Trim();
                        dataRow["IndicadorParcelaEmitida"] = item.IndicadorParcelaEmitida.Trim();
                        dataRow["SistemaAmortizacao"] = item.SistemaAmortizacao.Trim();
                        dataRow["TaxaJurosEfetivoContrato"] = item.TaxaJurosEfetivoContrato.Trim();
                        dataRow["TaxaJurosNominalContrato"] = item.TaxaJurosNominalContrato.Trim();
                        dataRow["To08_Ind_Incorp"] = item.To08_Ind_Incorp.Trim();
                        dataRow["To08_Vl_Quot_S"] = item.To08_Vl_Quot_S.Trim();
                        dataRow["To08_Vl_Quot"] = item.To08_Vl_Quot.Trim();
                        dataRow["To08_Sld_Fgts_Ant_S"] = item.To08_Sld_Fgts_Ant_S.Trim();
                        dataRow["To08_Sld_Fgts_Ant"] = item.To08_Sld_Fgts_Ant.Trim();
                        dataRow["To08_Sld_Fgts_Atu_S"] = item.To08_Sld_Fgts_Atu_S.Trim();
                        dataRow["To08_Sld_Fgts_Atu"] = item.To08_Sld_Fgts_Atu.Trim();
                        dataRow["To08_Sld_Sob_Acu_S"] = item.To08_Sld_Sob_Acu_S.Trim();
                        dataRow["To08_Sld_Sob_Acu"] = item.To08_Sld_Sob_Acu.Trim();
                        dataRow["To08_Sld_Soma_Sob_S"] = item.To08_Sld_Soma_Sob_S.Trim();
                        dataRow["To08_Sld_Soma_Sob"] = item.To08_Sld_Soma_Sob.Trim();
                        dataRow["Filler"] = string.IsNullOrWhiteSpace(item.Filler) ? "" : item.Filler.Trim();

                        dataTable.Rows.Add(dataRow);
                        ret++;

                        if(ret > 15000)
                        {
                            ret = 0;
                            bulkCopy.WriteToServer(dataTable);

                            dataTable.Rows.Clear();
                        }

                    }
                    catch (Exception exErroAdd)
                    {
                        command.Connection.Close();
                        throw new Exception("Erro ao tentar inserir registros na base de dados: " + exErroAdd.Message);
                    }
                }

                if (ret > 0)
                {
                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();
                    ret = 0;
                    bulkCopy.WriteToServer(dataTable);

                    dataTable.Rows.Clear();
                }
                command.Connection.Close();
                historicoParcelas = null;
            }
            catch (Exception exdb)
            {
                
                command.Connection.Close();
                throw new Exception("Erro ao tentar converter objeto na função AddHistoricoParcelas: " + exdb.Message);
            }
        }

        public List<HistoricoParcela> GetHistoricoParcelas(string _numeroContrato)
        {
            HistoricoParcela historicoParcela = null;
            historicoParcelas = new List<HistoricoParcela>();
            conn = new Conn();
            try
            {
                command = conn.Parametriza("SP_GET_ARQ_08_HISTORICO_PARCRELAS");
                command.Parameters.Add(new SqlParameter("@IdentificacaoContrato", _numeroContrato.Trim()));

                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    historicoParcela = new HistoricoParcela()
                    {
                        TipoArquivo = dataReader["TipoArquivo"].ToString(),
                        DataReferenciaExtracao = dataReader["DataReferenciaExtracao"].ToString(),
                        IdentificacaoContrato = dataReader["IdentificacaoContrato"].ToString(),
                        TipoRegistroContrato = dataReader["TipoRegistroContrato"].ToString(),
                        DataAmortizacaoParcela = dataReader["DataAmortizacaoParcela"].ToString(),
                        NumeroParcelaContrato = dataReader["NumeroParcelaContrato"].ToString(),
                        Sinal0 = dataReader["Sinal0"].ToString(),
                        ValorAmortizacaoParcela = dataReader["ValorAmortizacaoParcela"].ToString(),
                        Sinal1 = dataReader["Sinal1"].ToString(),
                        ValorJurosParcela = dataReader["ValorJurosParcela"].ToString(),
                        Sinal2 = dataReader["Sinal2"].ToString(),
                        ValorSeguroMIPParcela = dataReader["ValorSeguroMIPParcela"].ToString(),
                        Sinal3 = dataReader["Sinal3"].ToString(),
                        ValorSeguroDFIParcela = dataReader["ValorSeguroDFIParcela"].ToString(),
                        Sinal4 = dataReader["Sinal4"].ToString(),
                        ValorTarifaParcela = dataReader["ValorTarifaParcela"].ToString(),
                        CodigoEstipulanteMIP = dataReader["CodigoEstipulanteMIP"].ToString(),
                        RegiaoApoliceMIP = dataReader["RegiaoApoliceMIP"].ToString(),
                        MatriculaMIP = dataReader["MatriculaMIP"].ToString(),
                        AgenciaMIP = dataReader["AgenciaMIP"].ToString(),
                        EmpreendimentoMIP = dataReader["EmpreendimentoMIP"].ToString(),
                        CodigoApoliceAtualContratoMIP = dataReader["CodigoApoliceAtualContratoMIP"].ToString(),
                        CodigoEstipulanteDFI = dataReader["CodigoEstipulanteDFI"].ToString(),
                        RegiaoApoliceDFI = dataReader["RegiaoApoliceDFI"].ToString(),
                        MatriculaDFI = dataReader["MatriculaDFI"].ToString(),
                        AgenciaDFI = dataReader["AgenciaDFI"].ToString(),
                        EmpreendimentoDFI = dataReader["EmpreendimentoDFI"].ToString(),
                        CodigoApoliceAtualContratoDFI = dataReader["CodigoApoliceAtualContratoDFI"].ToString(),
                        Sinal5 = dataReader["Sinal5"].ToString(),
                        ValorAmortizacaoParcelaCorrigida = dataReader["ValorAmortizacaoParcelaCorrigida"].ToString(),
                        Sinal6 = dataReader["Sinal6"].ToString(),
                        ValorJurosParcelaCorrigida = dataReader["ValorJurosParcelaCorrigida"].ToString(),
                        Sinal7 = dataReader["Sinal7"].ToString(),
                        ValorSeguroMIPParcelaCorrigida = dataReader["ValorSeguroMIPParcelaCorrigida"].ToString(),
                        Sinal8 = dataReader["Sinal8"].ToString(),
                        ValorSeguroDFIParcelaCorrigida = dataReader["ValorSeguroDFIParcelaCorrigida"].ToString(),
                        Sinal9 = dataReader["Sinal9"].ToString(),
                        ValorIOFSeguroMIP = dataReader["ValorIOFSeguroMIP"].ToString(),
                        Sinal10 = dataReader["Sinal10"].ToString(),
                        ValorIOFSeguroDFI = dataReader["ValorIOFSeguroDFI"].ToString(),
                        Sinal11 = dataReader["Sinal11"].ToString(),
                        ValorTarifaParcelaCorrigidaTSA = dataReader["ValorTarifaParcelaCorrigidaTSA"].ToString(),
                        Sinal12 = dataReader["Sinal12"].ToString(),
                        ValorAbatimentoFGTSDAMP3 = dataReader["ValorAbatimentoFGTSDAMP3"].ToString(),
                        FatorCorrecaoMonetariaAplicada = dataReader["FatorCorrecaoMonetariaAplicada"].ToString(),
                        Sinal13 = dataReader["Sinal13"].ToString(),
                        ValorCorrecaoMonetariaEncargos = dataReader["ValorCorrecaoMonetariaEncargos"].ToString(),
                        Sinal14 = dataReader["Sinal14"].ToString(),
                        ValorJurosRemuneratorios = dataReader["ValorJurosRemuneratorios"].ToString(),
                        Sinal15 = dataReader["Sinal15"].ToString(),
                        ValorJurosMoratorios = dataReader["ValorJurosMoratorios"].ToString(),
                        Sinal16 = dataReader["Sinal16"].ToString(),
                        ValorCorrecaoMonetariaAtraso = dataReader["ValorCorrecaoMonetariaAtraso"].ToString(),
                        Sinal117 = dataReader["Sinal117"].ToString(),
                        ValorIncrementoDescontoOriginal = dataReader["ValorIncrementoDescontoOriginal"].ToString(),
                        Sinal18 = dataReader["Sinal18"].ToString(),
                        ValorIncrementoDescontoCorrigido = dataReader["ValorIncrementoDescontoCorrigido"].ToString(),
                        Sinal19 = dataReader["Sinal19"].ToString(),
                        ValorRDM = dataReader["ValorRDM"].ToString(),
                        Sinal20 = dataReader["Sinal20"].ToString(),
                        SaldoAmortizado = dataReader["SaldoAmortizado"].ToString(),
                        NumeroBoleto = dataReader["NumeroBoleto"].ToString(),
                        IndicativoParcelaPaga = dataReader["IndicativoParcelaPaga"].ToString(),
                        DataMovimentoPagamento = dataReader["DataMovimentoPagamento"].ToString(),
                        DataProcessamentoPagamento = dataReader["DataProcessamentoPagamento"].ToString(),
                        FormaLiquidacao = dataReader["FormaLiquidacao"].ToString(),
                        Sinal21 = dataReader["Sinal21"].ToString(),
                        ValorPago = dataReader["ValorPago"].ToString(),
                        Sinal22 = dataReader["Sinal22"].ToString(),
                        ValorDescontoConcedido = dataReader["ValorDescontoConcedido"].ToString(),
                        ValorDiferencaProximaParcela = dataReader["ValorDiferencaProximaParcela"].ToString(),
                        Sinal23 = dataReader["Sinal23"].ToString(),
                        ValorPrincipalSaldoDevedor = dataReader["ValorPrincipalSaldoDevedor"].ToString(),
                        ValorJurosSaldoDevedor = dataReader["ValorJurosSaldoDevedor"].ToString(),
                        Sinal24 = dataReader["Sinal24"].ToString(),
                        ValorCMSaldoDevedor = dataReader["ValorCMSaldoDevedor"].ToString(),
                        CodigoTipoOcorrência = dataReader["CodigoTipoOcorrência"].ToString(),
                        Sinal25 = dataReader["Sinal25"].ToString(),
                        ValorOcorrencia = dataReader["ValorOcorrencia"].ToString(),
                        IndicadorParcelaEmitida = dataReader["IndicadorParcelaEmitida"].ToString(),
                        SistemaAmortizacao = dataReader["SistemaAmortizacao"].ToString(),
                        TaxaJurosEfetivoContrato = dataReader["TaxaJurosEfetivoContrato"].ToString(),
                        TaxaJurosNominalContrato = dataReader["TaxaJurosNominalContrato"].ToString(),
                        To08_Ind_Incorp = dataReader["To08_Ind_Incorp"].ToString(),
                        To08_Vl_Quot_S = dataReader["To08_Vl_Quot_S"].ToString(),
                        To08_Vl_Quot = dataReader["To08_Vl_Quot"].ToString(),
                        To08_Sld_Fgts_Ant_S = dataReader["To08_Sld_Fgts_Ant_S"].ToString(),
                        To08_Sld_Fgts_Ant = dataReader["To08_Sld_Fgts_Ant"].ToString(),
                        To08_Sld_Fgts_Atu_S = dataReader["To08_Sld_Fgts_Atu_S"].ToString(),
                        To08_Sld_Fgts_Atu = dataReader["To08_Sld_Fgts_Atu"].ToString(),
                        To08_Sld_Sob_Acu_S = dataReader["To08_Sld_Sob_Acu_S"].ToString(),
                        To08_Sld_Sob_Acu = dataReader["To08_Sld_Sob_Acu"].ToString(),
                        To08_Sld_Soma_Sob_S = dataReader["To08_Sld_Soma_Sob_S"].ToString(),
                        To08_Sld_Soma_Sob = dataReader["To08_Sld_Soma_Sob"].ToString(),
                        Filler = dataReader["Filler"].ToString(),
                    };

                    historicoParcelas.Add(historicoParcela);
                }

            }
            catch (SqlException sqlExe)
            {
                command.Connection.Close();
                throw sqlExe;
            }
            finally
            {
                command.Connection.Close();
            }

            return historicoParcelas;
        }

        private DataTable CriaTabelaBulkHistoricoParcelas()
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


        public void TruncaTabelas()
        {
            try
            {
                conn = new Conn();
                command = conn.Parametriza("SP_TRUNCATE_ARQ_08_HISTORICO_PARCRELAS");

                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();

                command.ExecuteNonQuery();
            }
            catch (SqlException sqlex)
            {
                command.Connection.Close();
                throw sqlex;
            }
        }
    }
}
