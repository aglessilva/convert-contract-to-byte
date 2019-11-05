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
    public partial class FrmCarregaOcorrencia : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Thread _thread = null;
        UserObject obj = null;
        BusinessParcelas businessParcelas = null;

        string _diretorioArquivoHistoricoParcelas = string.Empty, tmp = string.Empty;
        int contador = 0, countPercent = 0;


        public FrmCarregaOcorrencia(string _diretorioOrigemPdf)
        {
            InitializeComponent();
            _diretorioArquivoHistoricoParcelas = _diretorioOrigemPdf;
        }

        private void FrmCarregaOcorrencia_Load(object sender, EventArgs e)
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

                backgroundWorkerOcorrencia.RunWorkerAsync();
            }
            catch (Exception exsql)
            {
                MessageBox.Show("Erro ao tentar ler o arquivo:" + exsql.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void backgroundWorkerOcorrencia_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            obj = (UserObject)e.UserState;

            label1.Text = "Armazenando ocorrências de contratos pdf...";

            progressBarReaderPdf.Value = e.ProgressPercentage;

            if (obj != null)
            {
                lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
            }
        }

        private void backgroundWorkerOcorrencia_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            MessageBox.Show("Armazenamento conlcuído\n" + tmp, "Ocorrências pdf", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void backgroundWorkerOcorrencia_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_thread.ThreadState == System.Threading.ThreadState.Running)
            {
                _thread.Join();
                stopwatch.Restart();
            }
            try
            {
                businessParcelas = new BusinessParcelas();
                DataTable dataTable = businessParcelas.CriaTabelaOcorrencia();
                DataRow dataRow = null;

                using (StreamReader sr = new StreamReader(_diretorioArquivoHistoricoParcelas, Encoding.Default))
                {
                    string linha = string.Empty;

                    while (!sr.EndOfStream)
                    {
                        linha = sr.ReadLine();
                        if (linha.Length >= 281)
                        {
                            dataRow = dataTable.NewRow();
                            dataRow["Contrato"] = linha.Substring(1, 14).Trim(); 
                            dataRow["DataVencimento"] = linha.Substring(15, 10).Trim();
                            dataRow["DataPagamento"] = linha.Substring(25, 10).Trim();
                            dataRow["Simbulo"] = linha.Substring(35, 3).Trim();
                            dataRow["CodigoOcorrencia"] = linha.Substring(38, 3).Trim();
                            dataRow["Descricao"] = linha.Substring(41, 30).Trim();
                            dataRow["Enc_Pago"] = linha.Substring(71, 18).Trim();
                            dataRow["Juros"] = linha.Substring(89, 18).Trim();
                            dataRow["Mora"] = linha.Substring(107, 18).Trim();
                            dataRow["ValorAmortizado"] = linha.Substring(125, 17).Trim();
                            dataRow["Sinal"] = linha.Substring(142, 1).Trim();
                            dataRow["SaldoDevedor"] = linha.Substring(143, 18).Trim();
                            dataRow["Alterado"] = linha.Substring(161, 30).Trim();
                            dataRow["Sit_Anterior"] = linha.Substring(191, 30).Trim();
                            dataRow["Sit_Atual"] = linha.Substring(221, 20).Trim();
                            dataRow["Sit_Aux"] = linha.Substring(251, 30).Trim();

                            dataTable.Rows.Add(dataRow);
                        }

                        contador++;
                        countPercent++;

                        obj = new UserObject() { Contrato = dataRow[0].ToString().Substring(1) };

                        backgroundWorkerOcorrencia.ReportProgress(countPercent, obj);
                        if (contador == 10000)
                        {
                            var tab = new
                            {
                                item1 = dataTable,
                                item2 = "Ocorrencias"

                            };

                            if (_thread != null)
                                if (_thread.ThreadState == System.Threading.ThreadState.Running)
                                    _thread.Join();

                            _thread = new Thread(new ParameterizedThreadStart(businessParcelas.AddBulkItens));
                            _thread.Start(tab);

                            contador = 0;
                            dataTable = businessParcelas.CriaTabelaOcorrencia();
                        }
                    }

                }

                if (contador > 0)
                {
                    var tab = new
                    {
                        item1 = dataTable,
                        item2 = "Ocorrencias"
                    };

                    if (_thread != null)
                        if (_thread.ThreadState == System.Threading.ThreadState.Running)
                            _thread.Join();

                    _thread = new Thread(new ParameterizedThreadStart(businessParcelas.AddBulkItens));
                    _thread.Start(tab);

                    dataTable = businessParcelas.CriaTabelaOcorrencia();
                }
            }
            catch (Exception exErr)
            {
                MessageBox.Show("Erro ao tentar adicionar itens na tabela\nDescrição: " + exErr.Message);
            }
        }


        private int RecreatingTable()
        {
            int i = 0;
            using (DbConnEntity dbConn = new DbConnEntity())
            {
                if (dbConn.Database.Exists())
                {
                    i = dbConn.Database.ExecuteSqlCommand("DELETE FROM [Ocorrencias];");
                    dbConn.Database.ExecuteSqlCommand("ALTER TABLE [Ocorrencias] ALTER COLUMN [Id] IDENTITY (1, 1);");
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
