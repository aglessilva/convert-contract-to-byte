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
    public partial class FrmRenomearPdf : Form
    {
        string _diretorioPdf, tmp;
        List<string> extencao = null;
        Stopwatch stopwatch = new Stopwatch();
        IEnumerable<string> listaRenomeada;
        UserObject obj = null;
        int countpercent = 0, MaximumProgress = 0;
        string _tela = string.Empty;
 

        public FrmRenomearPdf(string _dretorioOrigemPdf)
        {
            InitializeComponent();
            _diretorioPdf = _dretorioOrigemPdf;
        }

        private void FrmRenomearPdf_Load(object sender, EventArgs e)
        {
            //listaRenomeada = Directory.EnumerateFiles(_diretorioPdf, _tela, SearchOption.AllDirectories);
            // MaximumProgress = listaRenomeada.Count();
            // lblQtd.Text = $"Total: {MaximumProgress}";
            //progressBarReaderPdf.Maximum = MaximumProgress;

            stopwatch.Restart();
            backgroundWorkerRenomearPdf.RunWorkerAsync();
        }

        private void backgroundWorkerRenomearPdf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void backgroundWorkerRenomearPdf_DoWork(object sender, DoWorkEventArgs e)
        {
            extencao = new List<string>() { "*.dup", "*.damp", "*.fil", "*.err", "*.rej" };
            try
            {
                FileInfo fileInfo = null;
                extencao.ForEach(ex =>
                {
                    listaRenomeada = Directory.EnumerateFiles(_diretorioPdf, ex, SearchOption.AllDirectories);
                    MaximumProgress = listaRenomeada.Count();
                    countpercent = 0;

                    if (MaximumProgress > 0)
                    {
                        listaRenomeada.ToList().ForEach(arq =>
                        {
                            fileInfo = new FileInfo(arq);
                            File.Move(fileInfo.FullName, Path.ChangeExtension(fileInfo.FullName, ".pdf"));
                            countpercent++;
                            obj = new UserObject() { PdfInfo = fileInfo, DescricaoPercentural = ex };
                            backgroundWorkerRenomearPdf.ReportProgress(countpercent, obj);
                        });
                    }
                });
            }
            catch (Exception exe)
            {
                 new Exception("Erro no processo de renomear os PDFs: " + exe.Message);
            }
           
        }

        private void backgroundWorkerRenomearPdf_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                obj = (UserObject)e.UserState;

                if (progressBarReaderPdf.Maximum != MaximumProgress)
                {
                    progressBarReaderPdf.Maximum = MaximumProgress;
                    progressBarReaderPdf.Value = 0;
                }

                progressBarReaderPdf.Value = e.ProgressPercentage;

                if (obj != null)
                {
                    if (obj.DescricaoPercentural.Contains(".dup"))
                        lblQtd.Text = $"Total: {MaximumProgress} Arquivos duplicado";

                    if (obj.DescricaoPercentural.Contains(".damp"))
                        lblQtd.Text = $"Total: {MaximumProgress} Arquivos de Damp";

                    if (obj.DescricaoPercentural.Contains(".fil"))
                        lblQtd.Text = $"Total: {MaximumProgress} Arquivos de Filtro";

                    if (obj.DescricaoPercentural.Contains(".err"))
                        lblQtd.Text = $"Total: {MaximumProgress} Arquivos de Erro";

                    if (obj.DescricaoPercentural.Contains(".rej"))
                        lblQtd.Text = $"Total: {MaximumProgress} Arquivos de Rejeição";

                    tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
                    int indiceNameArquivo = obj.PdfInfo.FullName.Length / 2;
                    lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
                    lblTempo.Text = tmp;
                    lblArquivo.Text = string.Format(@"Arquivo: ...{0}", obj.PdfInfo.FullName.Substring(indiceNameArquivo));

                }
            }
            catch (Exception ex)
            {

                string erro = ex.Message;
            }
        }
        
    }
}
