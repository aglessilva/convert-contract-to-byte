using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmHistoricoParcelas : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        SqlCommand command = null;
        BusinessHistoricoParcelas businessHistoricoParcelas = null;
        List<HistoricoParcela> historicoParcelas = null;
        HistoricoParcela historicoParcela = null;

        string _diretorioArquivoHistoricoParcelas = string.Empty, tmp = string.Empty;
        int contador = 0 , countPercent = 0;

        Conn GetConn = new Conn();

        List<string> lstLinha = new List<string>();

        public FrmHistoricoParcelas(string _diretorioArquivo)
        {
            InitializeComponent();
            _diretorioArquivoHistoricoParcelas = _diretorioArquivo;
        }

        private void backgroundWorkerHistParcelas_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                businessHistoricoParcelas  = new BusinessHistoricoParcelas();
                historicoParcelas = new List<HistoricoParcela>();
              
                lstLinha.ForEach(linha =>
                 {
                     if (linha.Length >= 629)
                     {
                         historicoParcela = new HistoricoParcela()
                         {
                             TipoArquivo = linha.Substring(0, 1).Trim(),
                             DataReferenciaExtracao = linha.Substring(1, 8).Trim(),
                             IdentificacaoContrato = linha.Substring(9, 20).Trim(),
                             TipoRegistroContrato = linha.Substring(29, 2).Trim(),
                             DataAmortizacaoParcela = linha.Substring(50, 8).Trim(),
                             NumeroParcelaContrato = linha.Substring(58, 3).Trim(),
                             Sinal0 = linha.Substring(61, 1).Trim(),
                             ValorAmortizacaoParcela = linha.Substring(62, 13).Trim(),
                             Sinal1 = linha.Substring(75, 1).Trim(),
                             ValorJurosParcela = linha.Substring(76, 13).Trim(),
                             Sinal2 = linha.Substring(89, 1).Trim(),
                             ValorSeguroMIPParcela = linha.Substring(90, 13).Trim(),
                             Sinal3 = linha.Substring(103, 1).Trim(),
                             ValorSeguroDFIParcela = linha.Substring(104, 13).Trim(),
                             Sinal4 = linha.Substring(117, 1).Trim(),
                             ValorTarifaParcela = linha.Substring(118, 13).Trim(),
                             CodigoEstipulanteMIP = linha.Substring(131, 4).Trim(),
                             RegiaoApoliceMIP = linha.Substring(135, 2).Trim(),
                             MatriculaMIP = linha.Substring(137, 6).Trim(),
                             AgenciaMIP = linha.Substring(143, 2).Trim(),
                             EmpreendimentoMIP = linha.Substring(145, 9).Trim(),
                             CodigoApoliceAtualContratoMIP = linha.Substring(154, 20).Trim(),
                             CodigoEstipulanteDFI = linha.Substring(174, 4).Trim(),
                             RegiaoApoliceDFI = linha.Substring(178, 2).Trim(),
                             MatriculaDFI = linha.Substring(180, 6).Trim(),
                             AgenciaDFI = linha.Substring(186, 2).Trim(),
                             EmpreendimentoDFI = linha.Substring(188, 9).Trim(),
                             CodigoApoliceAtualContratoDFI = linha.Substring(197, 20).Trim(),
                             Sinal5 = linha.Substring(217, 1).Trim(),
                             ValorAmortizacaoParcelaCorrigida = linha.Substring(218, 13).Trim(),
                             Sinal6 = linha.Substring(231, 1).Trim(),
                             ValorJurosParcelaCorrigida = linha.Substring(232, 13).Trim(),
                             Sinal7 = linha.Substring(245, 1).Trim(),
                             ValorSeguroMIPParcelaCorrigida = linha.Substring(246, 13).Trim(),
                             Sinal8 = linha.Substring(259, 1).Trim(),
                             ValorSeguroDFIParcelaCorrigida = linha.Substring(260, 13).Trim(),
                             Sinal9 = linha.Substring(273, 1).Trim(),
                             ValorIOFSeguroMIP = linha.Substring(274, 13).Trim(),
                             Sinal10 = linha.Substring(287, 1).Trim(),
                             ValorIOFSeguroDFI = linha.Substring(288, 13).Trim(),
                             Sinal11 = linha.Substring(301, 1).Trim(),
                             ValorTarifaParcelaCorrigidaTSA = linha.Substring(302, 13).Trim(),
                             Sinal12 = linha.Substring(315, 1).Trim(),
                             ValorAbatimentoFGTSDAMP3 = linha.Substring(316, 13).Trim(),
                             FatorCorrecaoMonetariaAplicada = linha.Substring(329, 12).Trim(),
                             Sinal13 = linha.Substring(341, 1).Trim(),
                             ValorCorrecaoMonetariaEncargos = linha.Substring(342, 13).Trim(),
                             Sinal14 = linha.Substring(355, 1).Trim(),
                             ValorJurosRemuneratorios = linha.Substring(356, 13).Trim(),
                             Sinal15 = linha.Substring(369, 1).Trim(),
                             ValorJurosMoratorios = linha.Substring(370, 13).Trim(),
                             Sinal16 = linha.Substring(383, 1).Trim(),
                             ValorCorrecaoMonetariaAtraso = linha.Substring(385, 13).Trim(),
                             Sinal117 = linha.Substring(397, 1).Trim(),
                             ValorIncrementoDescontoOriginal = linha.Substring(398, 13).Trim(),
                             Sinal18 = linha.Substring(411, 1).Trim(),
                             ValorIncrementoDescontoCorrigido = linha.Substring(412, 13).Trim(),
                             Sinal19 = linha.Substring(425, 1).Trim(),
                             ValorRDM = linha.Substring(426, 13).Trim(),
                             Sinal20 = linha.Substring(439, 1).Trim(),
                             SaldoAmortizado = linha.Substring(440, 13).Trim(),
                             NumeroBoleto = linha.Substring(453, 25).Trim(),
                             IndicativoParcelaPaga = linha.Substring(478, 1).Trim(),
                             DataMovimentoPagamento = linha.Substring(479, 8).Trim(),
                             DataProcessamentoPagamento = linha.Substring(487, 8).Trim(),
                             FormaLiquidacao = linha.Substring(495, 10).Trim(),
                             Sinal21 = linha.Substring(505, 1).Trim(),
                             ValorPago = linha.Substring(506, 13).Trim(),
                             Sinal22 = linha.Substring(519, 1).Trim(),
                             ValorDescontoConcedido = linha.Substring(520, 13).Trim(),
                             ValorDiferencaProximaParcela = linha.Substring(533, 13).Trim(),
                             Sinal23 = linha.Substring(546, 1).Trim(),
                             ValorPrincipalSaldoDevedor = linha.Substring(547, 13).Trim(),
                             ValorJurosSaldoDevedor = linha.Substring(560, 13).Trim(),
                             Sinal24 = linha.Substring(573, 1).Trim(),
                             ValorCMSaldoDevedor = linha.Substring(574, 13).Trim(),
                             CodigoTipoOcorrência = linha.Substring(587, 2).Trim(),
                             Sinal25 = linha.Substring(589, 1).Trim(),
                             ValorOcorrencia = linha.Substring(590, 13).Trim(),
                             IndicadorParcelaEmitida = linha.Substring(603, 1).Trim(),
                             SistemaAmortizacao = linha.Substring(604, 1).Trim(),
                             TaxaJurosEfetivoContrato = linha.Substring(605, 12).Trim(),
                             TaxaJurosNominalContrato = linha.Substring(617, 12).Trim(),
                             To08_Ind_Incorp = linha.Substring(629, 1).Trim(),
                             To08_Vl_Quot_S = linha.Substring(630, 1).Trim(),
                             To08_Vl_Quot = linha.Substring(631, 12).Trim(),
                             To08_Sld_Fgts_Ant_S = linha.Substring(643, 1).Trim(),
                             To08_Sld_Fgts_Ant = linha.Substring(644, 13).Trim(),
                             To08_Sld_Fgts_Atu_S = linha.Substring(657, 1).Trim(),
                             To08_Sld_Fgts_Atu = linha.Substring(658, 13).Trim(),
                             To08_Sld_Sob_Acu_S = linha.Substring(671, 1).Trim(),
                             To08_Sld_Sob_Acu = linha.Substring(672, 13).Trim(),
                             To08_Sld_Soma_Sob_S = linha.Substring(685, 1).Trim(),
                             To08_Sld_Soma_Sob = linha.Substring(686, 13).Trim(),
                         };


                         historicoParcelas.Add(historicoParcela);

                         obj = new UserObject() { Contrato = historicoParcela.IdentificacaoContrato.Substring(1) };
                         contador++;
                         countPercent++;

                         backgroundWorkerHistParcelas.ReportProgress(countPercent, obj);
                         if (contador == 100000)
                         {
                             var tab = new
                             {
                                 item1 = historicoParcelas,
                                 item2 = command
                             };

                             if (_thread != null)
                                 if (_thread.ThreadState == System.Threading.ThreadState.Running)
                                     _thread.Join();

                             _thread = new Thread(new ParameterizedThreadStart(businessHistoricoParcelas.AddHistoricoParcelas));
                             _thread.Start(tab);

                             contador = 0;
                             historicoParcelas = new List<HistoricoParcela>();
                         }
                     }
                 });


                lstLinha = null;

                if (contador > 0)
                {
                    var tab = new
                    {
                        item1 = historicoParcelas,
                        item2 = command
                    };

                    if (_thread != null)
                        if (_thread.ThreadState == System.Threading.ThreadState.Running)
                            _thread.Join();

                    _thread = new Thread(new ParameterizedThreadStart(businessHistoricoParcelas.AddHistoricoParcelas));
                    _thread.Start(tab);

                    historicoParcelas = new List<HistoricoParcela>();
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

                businessHistoricoParcelas = new BusinessHistoricoParcelas();
                businessHistoricoParcelas.TruncaTabelas();


                progressBarReaderPdf.Maximum = total;
                lblQtd.Text = $"Total de Parcelas: {total}";
                command =  GetConn.Parametriza("SP_ADD_ARQ_08_HISTORICO_PARCRELAS");


                stopwatch.Restart();
                backgroundWorkerHistParcelas.RunWorkerAsync();
                
            }
            catch(SqlException exsql)
            {
                command.Connection.Close();
                MessageBox.Show("Erro ao tentar ler o arquivo:" + exsql.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception exLeitura)
            {
                MessageBox.Show ("Erro ao tentar ler o arquivo:" + exLeitura.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void backgroundWorkerHistParcelas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            if (command.Connection.State == System.Data.ConnectionState.Open)
                command.Connection.Close();
            string tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            MessageBox.Show("Armazenamento conlcuído\n"+tmp, "Historico de Parcelas", MessageBoxButtons.OK);
            Close();
        }


    }
}
