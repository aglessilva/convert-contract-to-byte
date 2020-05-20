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
        FileInfo ObjPdf, x2,x3,x4,x5,x6,x7;
        List<FileInfo> lstVms = null;

        static readonly object bloqueador = new object();

        Task<Task> nucleo1, nucleo2, nucleo3, nucleo4, nucleo5, nucleo6;

        public FrmDownload(List<string> _lstFoldes, string _diretorioDestino)
        {
            InitializeComponent();
    
            diretorioDestinoALTA = _diretorioDestino + @"\";
            lstDiretoriosFoldes = _lstFoldes;
        }

        FileInfo GetFileInfo()
        {
            try
            {
                lock (bloqueador)
                {
                    ObjPdf = null;
                    if (lstVms.Count > 0)
                    {
                        ObjPdf = lstVms[0];
                        lstVms.RemoveAt(0);
                    }

                    return ObjPdf;
                }
            }
            catch (Exception exThread)
            {
                throw exThread;
            }
        }

        private void backgroundWorkerDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
               
                List<string> lstDiretorios = new List<string>();

                lstDiretoriosFoldes.ForEach(l => { lstDiretorios.Add(diretorioOrigemPastas + l); Directory.CreateDirectory(diretorioDestinoALTA + l); });

                int countProgress = 0;

                foreach (var item in lstDiretorios)
                {
                    DirectoryInfo itemDiretorio = new DirectoryInfo(item);
                    totalArquivos += itemDiretorio.GetFiles("*.pdf", SearchOption.AllDirectories).Count();
                }

                stopwatch.Restart();

                // GERENCIAMENTO DE MULTITHREAD DOS NUCLEOS DOS PROCESSADOR
                foreach (string dataDiretorio in lstDiretorios)
                {
                    DirectoryInfo itemDiretorio = new DirectoryInfo(dataDiretorio);
                    lstVms = itemDiretorio.GetFiles("*.pdf", SearchOption.AllDirectories).ToList();

                    #region// DISPARAR 1º NUCLEO DO PROCESSADOR 
                    nucleo1 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (lstVms.Count > 0)
                            {
                                try
                                {
                                    DirectoryInfo directoryInfo = null;
                                    countProgress++;

                                    x2 = GetFileInfo();
                                    
                                    if (x2 != null)
                                    {
                                        backgroundWorkerDownload.ReportProgress(countProgress, x2);
                                        directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x2.Directory.Parent.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x2.Directory.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x2.Directory.Name.ToString());

                                        byte[] arquivo = File.ReadAllBytes(x2.FullName);
                                        File.WriteAllBytes($@"{directoryInfo.FullName}\{x2.Name}", arquivo);
                                        x2 = null;
                                    }
                                }
                                catch (Exception exX2)
                                {
                                    throw exX2;
                                }
                            }

                        });
                    });
                    nucleo1.Start();
                    nucleo1.Wait();
                    #endregion

                    #region// DISPARAR 2º NUCLEO DO PROCESSADOR
                    nucleo2 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (lstVms.Count > 0)
                            {
                                try
                                {
                                    DirectoryInfo directoryInfo = null;
                                    countProgress++;

                                    x3 = GetFileInfo();

                                    if (x3 != null)
                                    {
                                        backgroundWorkerDownload.ReportProgress(countProgress, x3);
                                        directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x3.Directory.Parent.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x3.Directory.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x3.Directory.Name.ToString());

                                        byte[] arquivo = File.ReadAllBytes(x3.FullName);
                                        File.WriteAllBytes($@"{directoryInfo.FullName}\{x3.Name}", arquivo);
                                        x3 = null;
                                    }
                                }
                                catch (Exception exX3)
                                {
                                    throw exX3;
                                }
                            }
                        });
                    });
                    nucleo2.Start();
                    nucleo2.Wait();
                    #endregion

                    #region// DISPARAR 3º NUCLEO DO PROCESSADOR
                    nucleo3 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (lstVms.Count > 0)
                            {
                                try
                                {
                                    DirectoryInfo directoryInfo = null;
                                    countProgress++;

                                    x4 = GetFileInfo();

                                    if (x4 != null)
                                    {
                                        backgroundWorkerDownload.ReportProgress(countProgress, x4);
                                        directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x4.Directory.Parent.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x4.Directory.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x4.Directory.Name.ToString());

                                        byte[] arquivo = File.ReadAllBytes(x4.FullName);
                                        File.WriteAllBytes($@"{directoryInfo.FullName}\{x4.Name}", arquivo);
                                        x4 = null;
                                    }
                                }
                                catch (Exception ExcX4)
                                {
                                    throw ExcX4;
                                }
                            }
                        });
                    });
                    nucleo3.Start();
                    nucleo3.Wait();
                    #endregion

                    #region// DISPARAR 4º NUCLEO DO PROCESSADOR
                    nucleo4 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (lstVms.Count > 0)
                            {
                                try
                                {
                                    DirectoryInfo directoryInfo = null;
                                    countProgress++;

                                    x5 = GetFileInfo();

                                    if (x5 != null)
                                    {
                                        backgroundWorkerDownload.ReportProgress(countProgress, x5);
                                        directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x5.Directory.Parent.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x5.Directory.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x5.Directory.Name.ToString());

                                        byte[] arquivo = File.ReadAllBytes(x5.FullName);
                                        File.WriteAllBytes($@"{directoryInfo.FullName}\{x5.Name}", arquivo);
                                        x5 = null;
                                    }
                                }
                                catch (Exception excX5)
                                {
                                    throw excX5;
                                }
                            }
                        });
                    });
                    nucleo4.Start();
                    nucleo4.Wait();
                    #endregion

                    #region// DISPARAR 5º NUCLEO DO PROCESSADOR
                    nucleo5 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (lstVms.Count > 0)
                            {
                                try
                                {
                                    DirectoryInfo directoryInfo = null;
                                    countProgress++;

                                    x6 = GetFileInfo();

                                    if (x6 != null)
                                    {
                                        backgroundWorkerDownload.ReportProgress(countProgress, x6);
                                        directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x6.Directory.Parent.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x6.Directory.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x6.Directory.Name.ToString());

                                        byte[] arquivo = File.ReadAllBytes(x6.FullName);
                                        File.WriteAllBytes($@"{directoryInfo.FullName}\{x6.Name}", arquivo);
                                        x6 = null;
                                    }
                                }
                                catch (Exception exX6)
                                {
                                    throw exX6;
                                }
                            }
                        });
                    });
                    nucleo5.Start();
                    nucleo5.Wait();
                    #endregion

                    #region// DISPARAR 6º NUCLEO DO PROCESSADOR
                    nucleo6 = new Task<Task>(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (lstVms.Count > 0)
                            {
                                try
                                {
                                    DirectoryInfo directoryInfo = null;
                                    countProgress++;
                                    x7 = GetFileInfo();

                                    if (x7 != null)
                                    {
                                        backgroundWorkerDownload.ReportProgress(countProgress, x7);
                                        directoryInfo = Directory.CreateDirectory(diretorioDestinoALTA + x7.Directory.Parent.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x7.Directory.Parent.ToString());
                                        directoryInfo = Directory.CreateDirectory(directoryInfo.FullName + @"\" + x7.Directory.Name.ToString());

                                        byte[] arquivo = File.ReadAllBytes(x7.FullName);
                                        File.WriteAllBytes($@"{directoryInfo.FullName}\{x7.Name}", arquivo);
                                        x7 = null;
                                    }
                                }
                                catch (Exception exX7)
                                {
                                    throw exX7;
                                }
                            }
                        });
                    });
                    nucleo6.Start();
                    nucleo6.Wait();
                    #endregion

                    //ENQUANTO OS NUCLEOS DE CADA PROCESSADOR ESTIVER EXECUTANDO AS TASKS 
                    #region//O METODO DE EXTENÇAO 'Wait' AGUARDA A FINALIZAÇÃO DA THREADING ANTES DO TERMINO DO EVENTO PAI ASSINCRONO FINALIZAR A EXECUÇÃO INVOCADA 

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
                    #endregion
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
