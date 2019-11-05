using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmCarregaParcelas : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        BusinessParcelas businessParcelas = null;

        string _diretorioArquivoHistoricoParcelas = string.Empty, tmp = string.Empty;
        int contador = 0, countPercent = 0;

        public FrmCarregaParcelas(string _diretorioOrigemPdf)
        {
            _diretorioArquivoHistoricoParcelas = _diretorioOrigemPdf;
            InitializeComponent();
        }

        private void backgroundWorkerParcelasPdf_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_thread.ThreadState == System.Threading.ThreadState.Running)
            {
                _thread.Join();
                stopwatch.Restart();
            }
            try
            {
                businessParcelas = new BusinessParcelas();
                DataTable dataTable = businessParcelas.CriaTabelaParcelas();
                DataRow dataRow = null;

                using (StreamReader sr = new StreamReader(_diretorioArquivoHistoricoParcelas, Encoding.Default))
                {
                    string linha = string.Empty;

                    while (!sr.EndOfStream)
                    {
                        linha = sr.ReadLine();
                        if (linha.Length >= 313)
                        {
                            dataRow = dataTable.NewRow();
                            dataRow["Carteira"] = linha.Substring(0,2).Trim();
                            dataRow["Contrato"] = linha.Substring(1,14).Trim();
                            dataRow["Vencimento"] = linha.Substring(15,10).Trim();
                            dataRow["IndiceCorrecao"] = linha.Substring(25,7).Trim();
                            dataRow["Indice"] = linha.Substring(25,7).Trim();
                            dataRow["Pagamento"] = linha.Substring(32,10).Trim();
                            dataRow["NumeroPrazo"] = linha.Substring(42,6).Trim();
                            dataRow["Prestacao"] = linha.Substring(48,18).Trim();
                            dataRow["Seguro"] = linha.Substring(66,18).Trim();
                            dataRow["Taxa"] = linha.Substring(84,18).Trim();
                            dataRow["Fgts"] = linha.Substring(102,18).Trim();
                            dataRow["AmortizacaoCorrecao"] = linha.Substring(120,17).Trim();
                            dataRow["Indicador"] = linha.Substring(137, 1).Trim();
                            dataRow["SaldoDevedorCorrecao"] = linha.Substring(138,18).Trim();
                            dataRow["Encargo"] = linha.Substring(156,18).Trim();
                            dataRow["Pago"] = linha.Substring(174,18).Trim();
                            dataRow["Juros"] = linha.Substring(192,18).Trim();
                            dataRow["Mora"] = linha.Substring(210,18).Trim();
                            dataRow["Amortizacao"] = linha.Substring(228,17).Trim();
                            dataRow["SaldoDevedor"] = linha.Substring(246, 18).Trim();
                            dataRow["Banco"] = linha.Substring(264,4).Trim().Trim();
                            dataRow["Agencia"] = linha.Substring(268,7).Trim();
                            dataRow["TPG_EVE_HIS"] = linha.Substring(275,3).Trim();
                            dataRow["Proc_Emi_Pag"] = linha.Substring(278,20).Trim();
                            dataRow["Iof"] = linha.Substring(301,12).Trim();
                            dataRow["DataBaseContrato"] = "0";

                            dataTable.Rows.Add(dataRow);
                        }

                        contador++;
                        countPercent++;

                        obj = new UserObject() { Contrato = dataRow[2].ToString().Substring(1) };

                        backgroundWorkerParcelasPdf.ReportProgress(countPercent, obj);
                        if (contador == 40000)
                        {
                            var tab = new
                            {
                                item1 = dataTable,
                                item2 = "Parcelas"
                            };

                            if (_thread != null)
                                if (_thread.ThreadState == System.Threading.ThreadState.Running)
                                    _thread.Join();

                            _thread = new Thread(new ParameterizedThreadStart(businessParcelas.AddBulkItens));
                            _thread.Start(tab);

                            contador = 0;
                            dataTable = businessParcelas.CriaTabelaParcelas();
                        }
                    }

                }

                if (contador > 0)
                {
                    var tab = new
                    {
                        item1 = dataTable,
                        item2 = "Parcelas"
                    };

                    if (_thread != null)
                        if (_thread.ThreadState == System.Threading.ThreadState.Running)
                            _thread.Join();

                    _thread = new Thread(new ParameterizedThreadStart(businessParcelas.AddBulkItens));
                    _thread.Start(tab);

                    dataTable = businessParcelas.CriaTabelaParcelas();
                }
            }
            catch (Exception exErr)
            {
                MessageBox.Show("Erro ao tentar adicionar itens na tabela\nDescrição: " + exErr.Message);
            }
        }


        private void backgroundWorkerParcelasPdf_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            obj = (UserObject)e.UserState;

            label1.Text = "Armazenando parcelas de contratos pdf...";

            progressBarReaderPdf.Value = e.ProgressPercentage;

            if (obj != null)
            {
                lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
            }
        }

        private void backgroundWorkerParcelasPdf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            MessageBox.Show("Armazenamento conlcuído\n" + tmp, "Parcelas pdf", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void FrmCarregaParcelas_Load(object sender, EventArgs e)
        {
            int total = 0;
            try
            {
                using (StreamReader sr = new StreamReader(_diretorioArquivoHistoricoParcelas, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        sr.ReadLine();
                        total++;
                    }
                }

                progressBarReaderPdf.Maximum = total;
                lblQtd.Text = $"Total de Parcelas: {total}";
                _thread = new Thread(() => RecreatingTable());
                _thread.Start();

                backgroundWorkerParcelasPdf.RunWorkerAsync();
            }
            catch (Exception exsql)
            {
                MessageBox.Show("Erro ao tentar ler o arquivo:" + exsql.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

           
        }

        private int RecreatingTable()
        {
            int i = 0;
            using (DbConnEntity dbConn = new DbConnEntity())
            {
                if (dbConn.Database.Exists())
                {
                    i = dbConn.Database.ExecuteSqlCommand("DELETE FROM [Parcelas];");
                    dbConn.Database.ExecuteSqlCommand("ALTER TABLE [Parcelas] ALTER COLUMN [Id] IDENTITY (1, 1);");
                }
                else
                {
                    dbConn.Database.Create();
                }
            }
            return i;
        }
    }
}
