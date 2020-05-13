using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmHistoricoParcelas : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        BusinessHistoricoParcelas businessHistoricoParcelas = null;
        Thread t = null;

        string _diretorioArquivoHistoricoParcelas = string.Empty, tmp = string.Empty;
        int contador = 0 , countPercent = 0;

        List<string> lstLinha = new List<string>();

        public FrmHistoricoParcelas(string _diretorioArquivo)
        {
            InitializeComponent();
            _diretorioArquivoHistoricoParcelas = _diretorioArquivo;
        }

        private void backgroundWorkerHistParcelas_DoWork(object sender, DoWorkEventArgs e)
        {
            if (t.ThreadState == System.Threading.ThreadState.Running)
            {
                t.Join();
                stopwatch.Restart();
            }
            try
            {
                businessHistoricoParcelas  = new BusinessHistoricoParcelas();
                DataTable dataTable = businessHistoricoParcelas.CriaTabelaBulkHistoricoParcelas();
                DataRow dataRow = null;

                lstLinha.ForEach(linha =>
                 {
                     if (linha.Length >= 699)
                     {
                         dataRow = dataTable.NewRow();

                         dataRow["TipoArquivo"] = linha.Substring(0, 1).Trim();
                         dataRow["DataReferenciaExtracao"] = linha.Substring(1, 8).Trim();
                         dataRow["IdentificacaoContrato"] = linha.Substring(10, 15).Trim();
                         dataRow["TipoRegistroContrato"] = linha.Substring(29, 2).Trim();
                         dataRow["DataAmortizacaoParcela"] = linha.Substring(50, 8).Trim();
                         dataRow["NumeroParcelaContrato"] = linha.Substring(58, 3).Trim();
                         dataRow["Sinal0"] = linha.Substring(61, 1).Trim();
                         dataRow["ValorAmortizacaoParcela"] = linha.Substring(62, 13).Trim();
                         dataRow["Sinal1"] = linha.Substring(75, 1).Trim();
                         dataRow["ValorJurosParcela"] = linha.Substring(76, 13).Trim();
                         dataRow["Sinal2"] = linha.Substring(89, 1).Trim();
                         dataRow["ValorSeguroMIPParcela"] = linha.Substring(90, 13).Trim();
                         dataRow["Sinal3"] = linha.Substring(103, 1).Trim();
                         dataRow["ValorSeguroDFIParcela"] = linha.Substring(104, 13).Trim();
                         dataRow["Sinal4"] = linha.Substring(117, 1).Trim();
                         dataRow["ValorTarifaParcela"] = linha.Substring(118, 13).Trim();
                         dataRow["CodigoEstipulanteMIP"] = linha.Substring(131, 4).Trim();
                         dataRow["RegiaoApoliceMIP"] = linha.Substring(135, 2).Trim();
                         dataRow["MatriculaMIP"] = linha.Substring(137, 6).Trim();
                         dataRow["AgenciaMIP"] = linha.Substring(143, 2).Trim();
                         dataRow["EmpreendimentoMIP"] = linha.Substring(145, 9).Trim();
                         dataRow["CodigoApoliceAtualContratoMIP"] = linha.Substring(154, 20).Trim();
                         dataRow["CodigoEstipulanteDFI"] = linha.Substring(174, 4).Trim();
                         dataRow["RegiaoApoliceDFI"] = linha.Substring(178, 2).Trim();
                         dataRow["MatriculaDFI"] = linha.Substring(180, 6).Trim();
                         dataRow["AgenciaDFI"] = linha.Substring(186, 2).Trim();
                         dataRow["EmpreendimentoDFI"] = linha.Substring(188, 9).Trim();
                         dataRow["CodigoApoliceAtualContratoDFI"] = linha.Substring(197, 20).Trim();
                         dataRow["Sinal5"] = linha.Substring(217, 1).Trim();
                         dataRow["ValorAmortizacaoParcelaCorrigida"] = linha.Substring(218, 13).Trim();
                         dataRow["Sinal6"] = linha.Substring(231, 1).Trim();
                         dataRow["ValorJurosParcelaCorrigida"] = linha.Substring(232, 13).Trim();
                         dataRow["Sinal7"] = linha.Substring(245, 1).Trim();
                         dataRow["ValorSeguroMIPParcelaCorrigida"] = linha.Substring(246, 13).Trim();
                         dataRow["Sinal8"] = linha.Substring(259, 1).Trim();
                         dataRow["ValorSeguroDFIParcelaCorrigida"] = linha.Substring(260, 13).Trim();
                         dataRow["Sinal9"] = linha.Substring(273, 1).Trim();
                         dataRow["ValorIOFSeguroMIP"] = linha.Substring(274, 13).Trim();
                         dataRow["Sinal10"] = linha.Substring(287, 1).Trim();
                         dataRow["ValorIOFSeguroDFI"] = linha.Substring(288, 13).Trim();
                         dataRow["Sinal11"] = linha.Substring(301, 1).Trim();
                         dataRow["ValorTarifaParcelaCorrigidaTSA"] = linha.Substring(302, 13).Trim();
                         dataRow["Sinal12"] = linha.Substring(315, 1).Trim();
                         dataRow["ValorAbatimentoFGTSDAMP3"] = linha.Substring(316, 13).Trim();
                         dataRow["FatorCorrecaoMonetariaAplicada"] = linha.Substring(329, 12).Trim();
                         dataRow["Sinal13"] = linha.Substring(341, 1).Trim();
                         dataRow["ValorCorrecaoMonetariaEncargos"] = linha.Substring(342, 13).Trim();
                         dataRow["Sinal14"] = linha.Substring(355, 1).Trim();
                         dataRow["ValorJurosRemuneratorios"] = linha.Substring(356, 13).Trim();
                         dataRow["Sinal15"] = linha.Substring(369, 1).Trim();
                         dataRow["ValorJurosMoratorios"] = linha.Substring(370, 13).Trim();
                         dataRow["Sinal16"] = linha.Substring(383, 1).Trim();
                         dataRow["ValorCorrecaoMonetariaAtraso"] = linha.Substring(385, 13).Trim();
                         dataRow["Sinal117"] = linha.Substring(397, 1).Trim();
                         dataRow["ValorIncrementoDescontoOriginal"] = linha.Substring(398, 13).Trim();
                         dataRow["Sinal18"] = linha.Substring(411, 1).Trim();
                         dataRow["ValorIncrementoDescontoCorrigido"] = linha.Substring(412, 13).Trim();
                         dataRow["Sinal19"] = linha.Substring(425, 1).Trim();
                         dataRow["ValorRDM"] = linha.Substring(426, 13).Trim();
                         dataRow["Sinal20"] = linha.Substring(439, 1).Trim();
                         dataRow["SaldoAmortizado"] = linha.Substring(440, 13).Trim();
                         dataRow["NumeroBoleto"] = linha.Substring(453, 25).Trim();
                         dataRow["IndicativoParcelaPaga"] = linha.Substring(478, 1).Trim();
                         dataRow["DataMovimentoPagamento"] = linha.Substring(479, 8).Trim();
                         dataRow["DataProcessamentoPagamento"] = linha.Substring(487, 8).Trim();
                         dataRow["FormaLiquidacao"] = linha.Substring(495, 10).Trim();
                         dataRow["Sinal21"] = linha.Substring(505, 1).Trim();
                         dataRow["ValorPago"] = linha.Substring(506, 13).Trim();
                         dataRow["Sinal22"] = linha.Substring(519, 1).Trim();
                         dataRow["ValorDescontoConcedido"] = linha.Substring(520, 13).Trim();
                         dataRow["ValorDiferencaProximaParcela"] = linha.Substring(533, 13).Trim();
                         dataRow["Sinal23"] = linha.Substring(546, 1).Trim();
                         dataRow["ValorPrincipalSaldoDevedor"] = linha.Substring(547, 13).Trim();
                         dataRow["ValorJurosSaldoDevedor"] = linha.Substring(560, 13).Trim();
                         dataRow["Sinal24"] = linha.Substring(573, 1).Trim();
                         dataRow["ValorCMSaldoDevedor"] = linha.Substring(574, 13).Trim();
                         dataRow["CodigoTipoOcorrência"] = linha.Substring(587, 2).Trim();
                         dataRow["Sinal25"] = linha.Substring(589, 1).Trim();
                         dataRow["ValorOcorrencia"] = linha.Substring(590, 13).Trim();
                         dataRow["IndicadorParcelaEmitida"] = linha.Substring(603, 1).Trim();
                         dataRow["SistemaAmortizacao"] = linha.Substring(604, 1).Trim();
                         dataRow["TaxaJurosEfetivoContrato"] = linha.Substring(605, 12).Trim();
                         dataRow["TaxaJurosNominalContrato"] = linha.Substring(617, 12).Trim();
                         dataRow["To08_Ind_Incorp"] = linha.Substring(629, 1).Trim();
                         dataRow["To08_Vl_Quot_S"] = linha.Substring(630, 1).Trim();
                         dataRow["To08_Vl_Quot"] = linha.Substring(631, 12).Trim();
                         dataRow["To08_Sld_Fgts_Ant_S"] = linha.Substring(643, 1).Trim();
                         dataRow["To08_Sld_Fgts_Ant"] = linha.Substring(644, 13).Trim();
                         dataRow["To08_Sld_Fgts_Atu_S"] = linha.Substring(657, 1).Trim();
                         dataRow["To08_Sld_Fgts_Atu"] = linha.Substring(658, 13).Trim();
                         dataRow["To08_Sld_Sob_Acu_S"] = linha.Substring(671, 1).Trim();
                         dataRow["To08_Sld_Sob_Acu"] = linha.Substring(672, 13).Trim();
                         dataRow["To08_Sld_Soma_Sob_S"] = linha.Substring(685, 1).Trim();
                         dataRow["To08_Sld_Soma_Sob"] = linha.Substring(686, 13).Trim();
                         dataRow["Filler"] = linha.Substring(699).Trim();

                         dataTable.Rows.Add(dataRow);
                     }

                     contador++;
                     countPercent++;

                     obj = new UserObject() { Contrato = dataRow[2].ToString().Substring(1) };

                    backgroundWorkerHistParcelas.ReportProgress(countPercent, obj);
                    if (contador == 40000)
                    {
                        var tab = new
                        {
                            item1 = dataTable,
                        };

                        if (_thread != null)
                            if (_thread.ThreadState == System.Threading.ThreadState.Running)
                                _thread.Join();

                        _thread = new Thread(new ParameterizedThreadStart(businessHistoricoParcelas.AddHistoricoParcelas));
                        _thread.Start(tab);

                         contador = 0;
                         dataTable = businessHistoricoParcelas.CriaTabelaBulkHistoricoParcelas();
                     }
                 });


                lstLinha = null;

                if (contador > 0)
                {
                    var tab = new
                    {
                        item1 = dataTable,
                    };

                    if (_thread != null)
                        if (_thread.ThreadState == System.Threading.ThreadState.Running)
                            _thread.Join();

                    _thread = new Thread(new ParameterizedThreadStart(businessHistoricoParcelas.AddHistoricoParcelas));
                    _thread.Start(tab);

                    dataTable = businessHistoricoParcelas.CriaTabelaBulkHistoricoParcelas();
                }
            }
            catch (Exception exErr)
            {
                MessageBox.Show("Erro ao tentar preencher os objeto HistoricoParcela\nDescrição: " + exErr.Message);
            }
        }

        private void backgroundWorkerHistParcelas_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            obj = (UserObject)e.UserState;

            label1.Text = "Armazenando histórico de parcelas...";

            progressBarReaderPdf.Value = e.ProgressPercentage;

            if (obj != null)
            {
                lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
            }
        }

        private void FrmHistoricoParcelas_Load(object sender, EventArgs e)
        {
            int total = 0;
            try
            {
                using (StreamReader sr = new StreamReader(_diretorioArquivoHistoricoParcelas, Encoding.Default))
                {
                    sr.ReadLine(); sr.ReadLine();

                    string linha = string.Empty;
                    while (!sr.EndOfStream)
                    {
                        linha = sr.ReadLine();
                        lstLinha.Add(linha);
                        total++;
                    }
                }

                progressBarReaderPdf.Maximum = total;
                lblQtd.Text = $"Total de Parcelas: {total}";
                label1.Text = "Aguarde.. excluindo registros antigos";
                t = new Thread(() => RecreatingTable());
                t.Start();

                backgroundWorkerHistParcelas.RunWorkerAsync();
            }
            catch(Exception exsql)
            {
                MessageBox.Show("Erro ao tentar ler o arquivo:" + exsql.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
           
        }


        private int  RecreatingTable()
        {
            int i = 0;
            using (DbConnEntity dbConn = new DbConnEntity())
            {
                if (dbConn.Database.Exists())
                {
                    i  =  dbConn.Database.ExecuteSqlCommand("DELETE FROM [HistoricoParcelas];");
                    dbConn.Database.ExecuteSqlCommand("ALTER TABLE [HistoricoParcelas] ALTER COLUMN [Id] IDENTITY (1, 1);");
                }
                else
                {
                    dbConn.Database.Create();
                }
            }
            return i ;
        }

        private void backgroundWorkerHistParcelas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            MessageBox.Show("Armazenamento conlcuído\n"+tmp, "Historico de Parcelas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }


    }
}
