using ConvetPdfToLayoutAlta.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmDuplicadoFiltro : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        UserObject obj = null;
        int countpercent = 0, MaximumProgress = 0, _indiceSubstring = 0;
        List<string> lst16 = null, lst18 = null, lst20 = null, lst25 = null;
        
        List<string> lstArquiPoint = new List<string>();
        List<string> lstDamp3 = new List<string>();

        string diretorioOrigemPdf, tmp, _descricao, diretorioConfig;

        public FrmDuplicadoFiltro(string _diretoioPdf,  List<string> _lstDamp03)
        {
            lstDamp3 = _lstDamp03;
            diretorioOrigemPdf = _diretoioPdf;
            InitializeComponent();
        }

        void GerarPonteiro()
        {
            Cursor.Current = Cursors.WaitCursor;
            IEnumerable<string> fileContract = Directory.EnumerateFiles(diretorioOrigemPdf, "*_16.pdf", SearchOption.AllDirectories);
            string diretorioConfig = Directory.GetCurrentDirectory() + @"\config\ARQUPONT.txt";
            List<string> listagemContratos16 = new List<string>();
            
            FileInfo f = null;
            fileContract.ToList().ForEach(w =>
            {
                f = new FileInfo(w);
                if (!listagemContratos16.Any(c => c.Trim().Equals(f.Name.Split('_')[0].Trim())))
                    listagemContratos16.Add(f.Name.Split('_')[0].Trim());
            });

            fileContract = listagemContratos16.OrderBy(o => o);

            using (StreamWriter sw = new StreamWriter(diretorioConfig, true, Encoding.ASCII))
            {
                fileContract.ToList().ForEach(w =>
                {
                    sw.WriteLine(w.Trim());
                });
            }

            Cursor.Current = Cursors.Default;
        }

        private void FrmDuplicadoFiltro_Load(object sender, EventArgs e)
        {
          
            try
            {
                lst16 = Directory.GetFiles(diretorioOrigemPdf, "*_16.pdf", SearchOption.AllDirectories).ToList();
                lst18 = Directory.GetFiles(diretorioOrigemPdf, "*_18.pdf", SearchOption.AllDirectories).ToList();
                lst20 = Directory.GetFiles(diretorioOrigemPdf, "*_20.pdf", SearchOption.AllDirectories).ToList();
                lst25 = Directory.GetFiles(diretorioOrigemPdf, "*_25.pdf", SearchOption.AllDirectories).ToList();

                diretorioConfig = Directory.GetCurrentDirectory();

                string _path = string.Format("{0}{1}", diretorioConfig, @"\config\ARQUPONT.txt");

                if (!File.Exists(_path))
                {
                    string msgAviso = string.Format("{0}{1}{2}", "O arquivo ARQUPONT.TXT não foi encontrado no diretório:" +Environment.NewLine , diretorioConfig, @"\config "+ Environment.NewLine + "Deseja gerar o ARQUPONT.txt a partir dos contrados?");
                    DialogResult dialogResult = MessageBox.Show(msgAviso, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (dialogResult == DialogResult.Yes)
                        GerarPonteiro();
                    else
                    {
                        Close();
                        return;
                    }
                }   

                using (StreamReader sw = new StreamReader(_path, Encoding.UTF8))
                {
                    while (!sw.EndOfStream)
                        lstArquiPoint.Add(sw.ReadLine());
                }

                lblTempo.Text = "";
                    BackgroundWorkerDuplicadoFiltro.RunWorkerAsync();
                    stopwatch.Restart();
            }
            catch (Exception ex)
            {
                Close();
                MessageBox.Show("Erro ao tentar iniciar o processo de leitura de contrato,\n Descrição: " + ex.Message,"Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BackgroundWorkerDuplicadoFiltro_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

                obj = (UserObject)e.UserState;

                progressBarReaderPdf.Maximum = obj.TotalArquivoPorPasta;
                progressBarReaderPdf.Value = e.ProgressPercentage;

                if (obj != null)
                {
                    tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);

                    if (obj.PdfInfo != null)
                        _indiceSubstring = obj.PdfInfo.FullName.Length / 2;

                    lblQtd.Text = $"Total: {obj.TotalArquivoPorPasta}";
                    lblDescricao.Text = _descricao;
                    lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
                    lblTempo.Text = tmp;
                    lblContrato.Text = string.Format("Contrato: {0}", obj.Contrato);
                    if (obj.PdfInfo != null)
                        lblDiretorio.Text = string.Format("Diretório: ...{0}", obj.PdfInfo.FullName.Substring(_indiceSubstring));
                    else
                        lblDiretorio.Text = "../../../" + obj.Contrato;

                    lbltotalDuplicado.Text = "Aguarde...";

                }
            }
            catch (Exception exePro)
            {
                throw exePro;
            }

        }

        private void BackgroundWorkerDuplicadoFiltro_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void BackgroundWorkerDuplicadoFiltro_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                FileInfo f = null;

                // VERIFICA SE OS CONTRATOS DO PONTEIRO TEM DAMP 
                // SE HOUVER DAMP E NAO EXISTIR A TELA 18, ENTÃO RENOMEIA OS ARQUISO 16,20,25

                    List<string> result = lstArquiPoint.AsEnumerable().Intersect(lstDamp3.AsEnumerable()).ToList();
                    string strTela = string.Empty;

                    _descricao = "Validando arquivo TELA 18...";

                    countpercent = 0;
                    MaximumProgress = lst18.Count;
                    string _numeroContrato = string.Empty;

                    lst18.ForEach(t18 => {

                        countpercent++;
                        f = new FileInfo(t18);
                        _numeroContrato = f.Name.Split('_')[0].Trim();

                        var o = new UserObject() { Contrato = _numeroContrato, PdfInfo = f, TotalArquivoPorPasta = MaximumProgress };
                        if (!Ambiente.dicionario18.Any(t => t.Key.Equals(_numeroContrato)))
                            Ambiente.dicionario18.Add(_numeroContrato, t18);
                        else
                            File.Move(f.FullName, System.IO.Path.ChangeExtension(f.FullName, ".dup"));

                        BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                        Thread.Sleep(1);
                    });

                    lst18 = null;

                    Thread.Sleep(10);
                    _descricao = "Validando arquivo TELA 16...";
                    countpercent = 0;
                    MaximumProgress = lst16.Count;
                    _numeroContrato = string.Empty;

                string pagina = string.Empty;
                string _contrato = string.Empty;
                List<string> lstLinha = null;

                lst16.ForEach(t16 =>
                {
                    ITextExtractionStrategy its;
                    countpercent++;
                    f = new FileInfo(t16);

                    using (PdfReader reader = new PdfReader(f.FullName))
                    {
                        its = new LocationTextExtractionStrategy();
                        pagina = PdfTextExtractor.GetTextFromPage(reader, 1, its).Trim();
                        pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));

                        using (StringReader strReader = new StringReader(pagina))
                        {
                            string line;
                            while ((line = strReader.ReadLine()) != null)
                            {
                                lstLinha = line.Split(' ').Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

                                if (lstLinha.Any(c => Regex.IsMatch(c, @"(^\d{4}.\d{5}.\d{3}-\d{1}$)")))
                                {
                                    pagina = lstLinha.Find(c => Regex.IsMatch(c, @"(^\d{4}.\d{5}.\d{3}-\d{1}$)"));
                                    _contrato = $"{lstLinha.Find(c => Regex.IsMatch(c, @"(^\d{4}$)")).Substring(2)}{ Regex.Replace(pagina, @"[^0-9$]", "")}";
                                    break;
                                }
                            }
                        }
                    }

                    pagina = string.Empty;
                    _numeroContrato = _contrato;

                    var o = new UserObject() { Contrato = _numeroContrato, PdfInfo = f, TotalArquivoPorPasta = MaximumProgress };
                    if (!Ambiente.dicionario16.Any(t => t.Key.Equals(_numeroContrato)))
                        Ambiente.dicionario16.Add(_numeroContrato, t16);
                    else
                        File.Move(f.FullName, System.IO.Path.ChangeExtension(f.FullName, ".dup"));

                    BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                    Thread.Sleep(1);
                    f = null;
                });

                lst16 = null;

                    _descricao = "Validando arquivo TELA 20...";
                    countpercent = 0;
                    MaximumProgress = lst20.Count;
                    _numeroContrato = string.Empty;

                    lst20.ForEach(t20 => {

                        countpercent++;
                        f = new FileInfo(t20);
                        _numeroContrato = f.Name.Split('_')[0].Trim();

                        var o = new UserObject() { Contrato = _numeroContrato, PdfInfo = f , TotalArquivoPorPasta = MaximumProgress };
                        if (!Ambiente.dicionario20.Any(t => t.Key.Equals(_numeroContrato)))
                            Ambiente.dicionario20.Add(_numeroContrato, t20);
                        else
                            File.Move(f.FullName, System.IO.Path.ChangeExtension(f.FullName, ".dup"));

                        BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                        Thread.Sleep(1);
                    });

                    lst20 = null;

                    _descricao = "Validando arquivo TELA 25...";
                    countpercent = 0;
                    MaximumProgress = lst25.Count;
                    _numeroContrato = string.Empty;

                    lst25.ForEach(t25 => {

                        countpercent++;
                        f = new FileInfo(t25);
                        _numeroContrato = f.Name.Split('_')[0].Trim();

                        var o = new UserObject() { Contrato = _numeroContrato, PdfInfo = f , TotalArquivoPorPasta = MaximumProgress };
                        if (!Ambiente.dicionario25.Any(t => t.Key.Equals(_numeroContrato)))
                            Ambiente.dicionario25.Add(_numeroContrato, t25);
                        else
                            File.Move(f.FullName, System.IO.Path.ChangeExtension(f.FullName, ".dup"));

                        BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                        Thread.Sleep(1);
                    });

                    lst25 = null;


                    _descricao = "Renomeando contratos inconsistentes de DAMP...";
                    countpercent = 0;
                    MaximumProgress = result.Count;
                    _numeroContrato = string.Empty;
                    result.ForEach(dmp =>
                    {
                        var o = new UserObject() { Contrato = dmp, PdfInfo = f, TotalArquivoPorPasta = MaximumProgress };
                        countpercent++;
                        BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                        Thread.Sleep(5);

                        if (!Ambiente.dicionario18.Any(gg => gg.Key.Equals(dmp)))
                        {
                            strTela = Ambiente.dicionario16.FirstOrDefault(c => c.Key.Equals(dmp)).Value;
                            if (!string.IsNullOrWhiteSpace(strTela))
                            {
                                f = new FileInfo(strTela);
                                if (!string.IsNullOrWhiteSpace(strTela))
                                    File.Move(strTela, System.IO.Path.ChangeExtension(strTela, ".damp"));

                                strTela = Ambiente.dicionario20.FirstOrDefault(c => c.Key.Equals(dmp)).Value;
                                if (!string.IsNullOrWhiteSpace(strTela))
                                    File.Move(strTela, System.IO.Path.ChangeExtension(strTela, ".damp"));

                                strTela = Ambiente.dicionario25.FirstOrDefault(c => c.Key.Equals(dmp)).Value;
                                if (!string.IsNullOrWhiteSpace(strTela))
                                    File.Move(strTela, System.IO.Path.ChangeExtension(strTela, ".damp"));

                            }
                        }
                    });

                    strTela = string.Empty;

                // Aplica filtro nos contratos que não estão nos ponteiros
                var z = Ambiente.dicionario16.GroupJoin(lstArquiPoint,
                        k => k.Key,
                        y => y,
                        (k, y)
                        => new { campoPrimary = k, campoKey = y.FirstOrDefault() })
                        .Where(g => string.IsNullOrWhiteSpace(g.campoKey))
                        .ToList();

                z.ForEach(l16 => {
                    Ambiente.dicionario16.Remove(l16.campoPrimary.Key);
                    File.Move(l16.campoPrimary.Value, System.IO.Path.ChangeExtension(l16.campoPrimary.Value, ".fil"));
                });


                // Verifica se as telas 18,20,25 tambem tem tela 16
                // se existir telas 18,20,25, obrigatoriamente devem ter tela 16

                var q = Ambiente.dicionario18.GroupJoin(Ambiente.dicionario16,
                        k => k.Key,
                        y => y.Key,
                        (k, y)
                        => new { campoPrimary = k, campoKey = y.FirstOrDefault() })
                        .Where(g => string.IsNullOrWhiteSpace(g.campoKey.Key))
                        .ToList();

                q.ForEach(l18 =>
                {
                    Ambiente.dicionario18.Remove(l18.campoPrimary.Key);
                    File.Move(l18.campoPrimary.Value, System.IO.Path.ChangeExtension(l18.campoPrimary.Value, ".rej"));
                });

                var w = Ambiente.dicionario20.GroupJoin(Ambiente.dicionario16,
                        k => k.Key,
                        y => y.Key,
                        (k, y)
                        => new { campoPrimary = k, campoKey = y.FirstOrDefault() })
                        .Where(g => string.IsNullOrWhiteSpace(g.campoKey.Key))
                        .ToList();

                w.ForEach(l20 =>
                {
                    Ambiente.dicionario20.Remove(l20.campoPrimary.Key);
                    File.Move(l20.campoPrimary.Value, System.IO.Path.ChangeExtension(l20.campoPrimary.Value, ".rej"));
                });

                var x = Ambiente.dicionario25.GroupJoin(Ambiente.dicionario16,
                        k => k.Key,
                        y => y.Key,
                        (k, y)
                        => new { campoPrimary = k, campoKey = y.FirstOrDefault() })
                        .Where(g => string.IsNullOrWhiteSpace(g.campoKey.Key))
                        .ToList();

                x.ForEach(l25 => {
                    Ambiente.dicionario25.Remove(l25.campoPrimary.Key);
                    File.Move(l25.campoPrimary.Value, System.IO.Path.ChangeExtension(l25.campoPrimary.Value, ".rej"));
                });

               q = w = x =  null; z = null;
              
            }
            catch (Exception exWork)
            {
                MessageBox.Show(exWork.Message);
            }

        }
    }
}
