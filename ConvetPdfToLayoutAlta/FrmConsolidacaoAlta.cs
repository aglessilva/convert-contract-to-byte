using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmConsolidacaoAlta : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        UserObject obj = null;
        int countpercent = 0, MaximumProgress = 0;
        string tmp = string.Empty, linha = string.Empty;
        List<string> lstErr = null;

       
        DataTable tbl16Cont = new DataTable("TL16CONT");
        DataTable tbl16Ocor = new DataTable("TL16OCOR");

        string diretorioDestinoLayout, _diretorioOrigemPdf;


        public FrmConsolidacaoAlta(string _diretorioDestino, string _origemPdf)
        {
            diretorioDestinoLayout =  _diretorioDestino;
            _diretorioOrigemPdf = _origemPdf;
            InitializeComponent();
        }

        private void FrmConsolidacaoAlta_Load(object sender, EventArgs e)
        {
            tbl16Cont.Columns.Add("Contrato");
            tbl16Cont.Columns.Add("Registro");
            tbl16Ocor.Columns.Add("Contrato");
            tbl16Ocor.Columns.Add("Registro");
        }

        private void backgroundWorkerConsolida_DoWork(object sender, DoWorkEventArgs e)
        {
            string lineValue = string.Empty;
            StreamWriter streamWriterArquPont = new StreamWriter(string.Format(@"{0}\NEW_ARQUPONT.txt", diretorioDestinoLayout), true, Encoding.Default);
            using (StreamWriter escreverContrato = new StreamWriter(string.Format(@"{0}\NEW_TL16CONT.txt", diretorioDestinoLayout), true, Encoding.Default))
            {
                MaximumProgress = tbl16Cont.Rows.Count;

                foreach (DataRow item in tbl16Cont.Rows)
                {
                    countpercent++;
                    obj = new UserObject() { DescricaoPercentural = "Atualizando arquivos de Ponteiro e Tela 16...", TotalArquivoPorPasta = MaximumProgress };
                    backgroundWorkerConsolida.ReportProgress(countpercent, obj);
                    lineValue = string.Format("{0}", item[1].ToString());
                    escreverContrato.WriteLine(lineValue);
                    streamWriterArquPont.WriteLine(item[0].ToString());
                    Thread.Sleep(1);
                }
                streamWriterArquPont.Dispose();
            }
        }

        private void backgroundWorkerConsolida_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            obj = (UserObject)e.UserState;

            progressBarReaderPdf.Maximum = MaximumProgress;
            progressBarReaderPdf.Value = e.ProgressPercentage;
            tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            lblQtd.Text = string.Format("Total de Registros: {0}  ", obj.TotalArquivoPorPasta);
            lblLidos.Text = e.ProgressPercentage.ToString();
            lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
            lblTempo.Text = tmp;
            lblPendente.Text = string.Format("{0}", obj.DescricaoPercentural);
        }

        private void backgroundWorkerConsolida_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Ponteiro Atualizado","Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void FrmConsolidacaoAlta_Shown(object sender, EventArgs e)
        {
            lblQtd.Text = "Aguarde...";
            lblPendente.Text = "Carregando arquivos para analise e consolidação....";
            Refresh();
            #region ==================INICIALIA A LEITURA ARQUIVO TXT DE CONTRATOS =================================
            using (StreamReader sw = new StreamReader(string.Format(@"{0}\TL16CONT.txt", diretorioDestinoLayout), Encoding.Default))
            {
                while (!sw.EndOfStream)
                {
                    linha = sw.ReadLine();
                    tbl16Cont.Rows.Add(linha.Substring(0, 15), linha);
                }
            }
            #endregion
            
            lblQtd.Text = lblPendente.Text = "";

            lstErr = Directory.EnumerateFiles(_diretorioOrigemPdf, "*.err", SearchOption.AllDirectories).ToList();

            lblPendente.Text = "analisando arquivos para atualizações...";
            Refresh();

            DataRow[] _dataRows = null;
            FileInfo fileInfo = null;
            string filtro = string.Empty;
            lstErr.ForEach(k => {

                fileInfo = new FileInfo(k);

                filtro = string.Format("Contrato = {0}", fileInfo.Name.Split('_')[0]);

                _dataRows = tbl16Cont.Select(filtro);
                if (_dataRows.Length > 0)
                {
                    foreach (DataRow item in _dataRows)
                    {
                        tbl16Cont.Rows.Remove(item);
                    }
                    _dataRows = null;
                }
             
            });

            lblPendente.Text = "";
            stopwatch.Restart();
            backgroundWorkerConsolida.RunWorkerAsync();
        }
       
    }
    
}
