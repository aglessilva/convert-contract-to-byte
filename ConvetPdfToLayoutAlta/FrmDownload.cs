using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmDownload : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        List<string> lstDiretoriosFoldes = null;
        string diretorioDestinoALTA = string.Empty, tmp = string.Empty;
        string diretorioOrigemPastas = @"\\mscluster40fs\plataformaPF2\TOMBAMENTO_PF\Processamento\";
        int totalArquivos = 0;
        FileInfo FileInfo = null;
        Task<Task> nucleo1, nucleo2, nucleo3, nucleo4, nucleo5, nucleo6;

        public FrmDownload(List<string> _lstFoldes, string _diretorioDestino)
        {
            InitializeComponent();
    
            diretorioDestinoALTA = _diretorioDestino + @"\";
            lstDiretoriosFoldes = _lstFoldes;
        }

        private void backgroundWorkerDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<string> lstDiretorios = new List<string>();

                lstDiretoriosFoldes.ForEach(l => { lstDiretorios.Add(diretorioOrigemPastas + l); Directory.CreateDirectory(diretorioDestinoALTA + l); });

                int countProgress = 0;
                int dividir = 0;

                foreach (var item in lstDiretorios)
                {
                    DirectoryInfo itemDiretorio = new DirectoryInfo(item);
                    totalArquivos += itemDiretorio.GetFiles("*.pdf", SearchOption.AllDirectories).Count();
                }


                // GERENCIAMENTO DE MULTITHREAD DOS NUCLEOS DOS PROCESSADOR
                foreach (string dataDiretorio in lstDiretorios)
                {
                    DirectoryInfo itemDiretorio = new DirectoryInfo(dataDiretorio);
                    List<FileInfo> lstVms = itemDiretorio.GetFiles("*.pdf", SearchOption.AllDirectories).ToList();

                    dividir = lstVms.Count / 6;

                    List<FileInfo> lstVms01 = lstVms.Take(dividir).ToList();
                    lstVms.RemoveRange(0, dividir);

                    List<FileInfo> lstVms02 = lstVms.Take(dividir).ToList();
                    lstVms.RemoveRange(0, dividir);

                    List<FileInfo> lstVms03 = lstVms.Take(dividir).ToList();
                    lstVms.RemoveRange(0, dividir);

                    List<FileInfo> lstVms04 = lstVms.Take(dividir).ToList();
                    lstVms.RemoveRange(0, dividir);

                    List<FileInfo> lstVms05 = lstVms.Take(dividir).ToList();
                    lstVms.RemoveRange(0, dividir);

                    // DISPARAR 1º NUCLEO DO PROCESSADOR 
                    nucleo1 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            lstVms02.ForEach(x2 =>
                            {
                                DirectoryInfo directoryInfo = null;
                                countProgress++;
                                backgroundWorkerDownload.ReportProgress(countProgress, x2);
                                directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x2.Directory.Parent.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x2.Directory.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x2.Directory.Name.ToString());

                                byte[] arquivo = File.ReadAllBytes(x2.FullName);
                                File.WriteAllBytes($@"{directoryInfo.FullName}\{x2.Name}", arquivo);
                            });

                        });
                    });
                    nucleo1.Start();
                    nucleo1.Wait();

                    // DISPARAR 2º NUCLEO DO PROCESSADOR
                    nucleo2 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            lstVms01.ForEach(x3 =>
                            {
                                DirectoryInfo directoryInfo = null;
                                countProgress++;
                                backgroundWorkerDownload.ReportProgress(countProgress, x3);
                                directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x3.Directory.Parent.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x3.Directory.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x3.Directory.Name.ToString());

                                byte[] arquivo = File.ReadAllBytes(x3.FullName);
                                File.WriteAllBytes($@"{directoryInfo.FullName}\{x3.Name}", arquivo);
                            });

                        });
                    });
                    nucleo2.Start();
                    nucleo2.Wait();

                    // DISPARAR 3º NUCLEO DO PROCESSADOR
                    nucleo3 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            lstVms03.ForEach(x4 =>
                            {
                                DirectoryInfo directoryInfo = null;
                                countProgress++;
                                backgroundWorkerDownload.ReportProgress(countProgress, x4);
                                directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x4.Directory.Parent.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x4.Directory.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x4.Directory.Name.ToString());

                                byte[] arquivo = File.ReadAllBytes(x4.FullName);
                                File.WriteAllBytes($@"{directoryInfo.FullName}\{x4.Name}", arquivo);
                            });

                        });
                    });
                    nucleo3.Start();
                    nucleo3.Wait();

                    // DISPARAR 4º NUCLEO DO DO PROCESSADOR
                    nucleo4 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            lstVms.ForEach(x5 =>
                            {
                                DirectoryInfo directoryInfo = null;
                                countProgress++;
                                backgroundWorkerDownload.ReportProgress(countProgress, x5);
                                directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x5.Directory.Parent.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x5.Directory.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x5.Directory.Name.ToString());

                                byte[] arquivo = File.ReadAllBytes(x5.FullName);
                                File.WriteAllBytes($@"{directoryInfo.FullName}\{x5.Name}", arquivo);
                            });

                        });
                    });
                    nucleo4.Start();
                    nucleo4.Wait();

                    // DISPARAR 5º NUCLEO DO DO PROCESSADOR
                    nucleo5 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            lstVms04.ForEach(x6 =>
                            {
                                DirectoryInfo directoryInfo = null;
                                countProgress++;
                                backgroundWorkerDownload.ReportProgress(countProgress, x6);
                                directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x6.Directory.Parent.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x6.Directory.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x6.Directory.Name.ToString());

                                byte[] arquivo = File.ReadAllBytes(x6.FullName);
                                File.WriteAllBytes($@"{directoryInfo.FullName}\{x6.Name}", arquivo);
                            });

                        });
                    });
                    nucleo5.Start();
                    nucleo5.Wait();


                    // DISPARAR 6º NUCLEO DO DO PROCESSADOR
                    nucleo6 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            lstVms05.ForEach(x7 =>
                            {
                                DirectoryInfo directoryInfo = null;
                                countProgress++;
                                backgroundWorkerDownload.ReportProgress(countProgress, x7);
                                directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x7.Directory.Parent.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x7.Directory.Parent.ToString());
                                directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x7.Directory.Name.ToString());

                                byte[] arquivo = File.ReadAllBytes(x7.FullName);
                                File.WriteAllBytes($@"{directoryInfo.FullName}\{x7.Name}", arquivo);
                            });

                        });
                    });
                    nucleo6.Start();
                    nucleo6.Wait();
                    //ENQUANTO OS NUCLEOS DE CADA PROCESSADOR ESTIVER EXECUTANDO AS TASKS 
                    //O METODO DE EXTENÇAO 'Wait' AGUARDA A FINALIZAÇÃO DA THREADING ANTES DO TERMINO DO EVENTO PAI ASSINCRONO FINALIZAR A EXECUÇÃO INVOCADA 
                    if (nucleo1.Status != TaskStatus.Running || !nucleo1.IsCompleted)
                        nucleo1.Result.Wait();// AGUARDA A FINALIZAÇÃO DA TASK

                    if (nucleo2.Status != TaskStatus.Running || !nucleo2.IsCompleted)
                        nucleo2.Result.Wait();// AGUARDA A FINALIZAÇÃO DA TASK

                    if (nucleo3.Status != TaskStatus.Running || !nucleo3.IsCompleted)
                        nucleo3.Result.Wait();// AGUARDA A FINALIZAÇÃO DA TASK

                    if (nucleo4.Status != TaskStatus.Running || !nucleo4.IsCompleted)
                        nucleo4.Result.Wait();// AGUARDA A FINALIZAÇÃO DA TASK

                    if (nucleo5.Status != TaskStatus.Running || !nucleo5.IsCompleted)
                        nucleo5.Result.Wait();// AGUARDA A FINALIZAÇÃO DA TASK

                    if (nucleo6.Status != TaskStatus.Running || !nucleo6.IsCompleted)
                        nucleo6.Result.Wait();// AGUARDA A FINALIZAÇÃO DA TASK
                }
            }
            catch (Exception exeDown)
            {
                MessageBox.Show(exeDown.Message);
            }
        }

        private void backgroundWorkerDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void FrmDownload_Load(object sender, EventArgs e)
        {
            stopwatch.Restart();
            backgroundWorkerDownload.RunWorkerAsync();
        }

        private void backgroundWorkerDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBarReaderPdf.Maximum = totalArquivos;
                lblLidos.Text = string.Format("Contratos baixados: {0}", e.ProgressPercentage.ToString());

                FileInfo = (FileInfo)e.UserState;

                if (progressBarReaderPdf.Value <= progressBarReaderPdf.Maximum)
                    progressBarReaderPdf.Value = e.ProgressPercentage;

                if (FileInfo != null)
                {
                    tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
                    lblQtd.Text = string.Format("Total de Arquivos: {0}", totalArquivos);
                    lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
                    lblTempo.Text = tmp;
                    lblContrato.Text = string.Format("Contrato: {0}", FileInfo.Name.Split('_')[0]);
                  //  lblPasta.Text = string.Format("Diretorio: {0} - Total: {1}", FileInfo.Directory.Parent.Parent, (totalArquivos - e.ProgressPercentage));
                }
            }
            catch (Exception exProgress)
            {
                MessageBox.Show(exProgress.Message);
            }
        }
    }
}
