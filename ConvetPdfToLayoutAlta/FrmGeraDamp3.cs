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
    public partial class FrmGeraDamp3 : Form
    {
        string _damp3Path = string.Empty;
        string tmp = string.Empty;
        Stopwatch stopwatch = new Stopwatch();
        UserObject obj = null;
        int countpercent = 0, MaximumProgress = 0, countDamp = 0;
        List<string> listContratoDamp = new List<string>();

        public FrmGeraDamp3(string _arquivoRelaDamp, List<string> lstDamp3)
        {
            InitializeComponent();
            _damp3Path = _arquivoRelaDamp;
            listContratoDamp = lstDamp3;
            countDamp = lstDamp3.Count;
        }

        private void backgroundWorkerDamp3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(_damp3Path, true))
                {
                    string _linha = string.Empty;
                    List<string> _arrayLinha = sr.ReadToEnd().Split('\n').Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
                    string[] _arrayItem = { };

                    _arrayLinha.RemoveAt(0);
                    MaximumProgress = _arrayLinha.Count();

                    _arrayLinha.ForEach(p =>
                    {
                        _linha = Encoding.ASCII.GetString(Encoding.Convert(Encoding.ASCII, Encoding.ASCII, Encoding.ASCII.GetBytes(p)));
                        _arrayItem = _linha.Split(';').Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                        if (_arrayItem.Any(t => t.Contains("PGTO")))
                            if (_arrayItem.Any(t => t.Contains("ABERTURA")))
                                if (!listContratoDamp.Any(c => c.Equals(_arrayItem[0].Split('-')[0].Trim())))
                                    listContratoDamp.Add(_arrayItem[0].Split('-')[0].Trim());

                        countpercent++;
                        obj = new UserObject() { DescricaoPercentural = "Realizando leitura de arquivo e consistindo filtro  -  Contrato: " + _arrayItem[0].Split('-')[0].Trim(), TotalArquivoPorPasta = MaximumProgress };
                        backgroundWorkerDamp3.ReportProgress(countpercent, obj);
                        Thread.Sleep(1);
                    });
                }

                obj = new UserObject() { DescricaoPercentural = "Ordenando Lista de contratos filtrados....aguarde!", TotalArquivoPorPasta = MaximumProgress };
                backgroundWorkerDamp3.ReportProgress(countpercent, obj);

                Thread.Sleep(2000);
                countpercent = 0;
                MaximumProgress = listContratoDamp.Count();
                listContratoDamp.OrderBy(o => o).ToList();

                // se entrar um novo contrado na lista de Damp atual, então atualiza o arquivo de damp
                if (listContratoDamp.Count != countDamp)
                {
                    FileInfo f = new FileInfo(Directory.GetCurrentDirectory() + @"\config\DAMP03.TXT");

                    if (f.Exists)
                        f.Delete();

                    using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\config\DAMP03.TXT"))
                    {
                        listContratoDamp.ForEach(n =>
                        {

                            sw.WriteLine(n);

                            countpercent++;
                            obj = new UserObject() { DescricaoPercentural = $"Atualizando Arquivo de Damp - Contratos: {n}", TotalArquivoPorPasta = MaximumProgress };
                            backgroundWorkerDamp3.ReportProgress(countpercent, obj);
                            Thread.Sleep(1);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no processo para gerar o arquivo Damp\nErro: " + ex.Message);
            }
        }

        private void FrmGeraDamp3_Load(object sender, EventArgs e)
        {
            lblPendente.Text = "";
            stopwatch.Restart();
            backgroundWorkerDamp3.RunWorkerAsync();

        }

        private void backgroundWorkerDamp3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

                progressBarReaderPdf.Maximum = MaximumProgress;
                progressBarReaderPdf.Value = e.ProgressPercentage;
                tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
                lblQtd.Text = string.Format("Total de Registros: {0}  ", obj.TotalArquivoPorPasta);
                lblLidos.Text = e.ProgressPercentage.ToString();
                lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
                lblTempo.Text = tmp;
                lblPendente.Text = string.Format("{0}", obj.DescricaoPercentural);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void backgroundWorkerDamp3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Arquivo de Damp Atualizado!!!", "Finalizado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
