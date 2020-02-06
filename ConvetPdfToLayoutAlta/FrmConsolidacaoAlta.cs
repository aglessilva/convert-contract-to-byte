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
        }

        private void backgroundWorkerConsolida_DoWork(object sender, DoWorkEventArgs e)
        {
            string arq = $@"{diretorioDestinoLayout}\ARQUPONT.txt";
            if (File.Exists(arq))
                File.Delete(arq);

            using (StreamWriter streamWriterArquPont = new StreamWriter($@"{diretorioDestinoLayout}\ARQUPONT.txt", true, Encoding.Default))
            {
                MaximumProgress = tbl16Cont.Rows.Count;

                foreach (DataRow item in tbl16Cont.Rows)
                {
                    countpercent++;
                    obj = new UserObject() { DescricaoPercentural = "Atualizando arquivos de Ponteiro...", TotalArquivoPorPasta = MaximumProgress };
                    backgroundWorkerConsolida.ReportProgress(countpercent, obj);
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
            int erros = Directory.EnumerateFiles(_diretorioOrigemPdf, "*_16.err", SearchOption.AllDirectories).Count();
            int dups = Directory.EnumerateFiles(_diretorioOrigemPdf, "*_16.dup", SearchOption.AllDirectories).Count();
            int fil = Directory.EnumerateFiles(_diretorioOrigemPdf, "*_16.fil", SearchOption.AllDirectories).Count();
            int damp = Directory.EnumerateFiles(_diretorioOrigemPdf, "*_16.damp", SearchOption.AllDirectories).Count();
            int rej = Directory.EnumerateFiles(_diretorioOrigemPdf, "*_16.rej", SearchOption.AllDirectories).Count();

            File.Copy($@"{Directory.GetCurrentDirectory()}\Config\SITU115A.TXT", $@"{_diretorioOrigemPdf}\ALTA\SITU115A.TXT");
            File.Copy($@"{Directory.GetCurrentDirectory()}\Config\RELADAMP.TXT", $@"{_diretorioOrigemPdf}\ALTA\RELADAMP.TXT");

            string msgResult = $"Ponteiro atualizado!\nResultado:\n\nTotal Extraidos: {MaximumProgress}\nTotal de Erros: {erros}\nTotal Duplicados: {dups}\nTotal Filtrado: {fil}\nTotal sem Damp3: {damp} \nTotal Rejeitado: {rej}";
            MessageBox.Show(msgResult,"Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

            Process.Start(_diretorioOrigemPdf + @"\ALTA\");
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
                    tbl16Cont.Rows.Add(string.Format("01{0}1", linha.Substring(0, 15)));
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
