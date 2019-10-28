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
        List<string> _arrayDuplicado = null;
        List<string> lst16 = null;
        private List<string> lstDuplicado = null;
        List<string> lstArquiPoint = new List<string>();
        List<string> lstDamp3 = new List<string>();

        string diretorioOrigemPdf, tmp, tela, _descricao, diretorioConfig;

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

                string _tela = string.Format("*_{0}.pdf", Regex.Replace(tela, @"[^0-9$]", ""));
                _arrayDuplicado = Directory.GetFiles(diretorioOrigemPdf, _tela, SearchOption.AllDirectories).ToList();


                // localiza todos os arquivos duplicados na lista de diretorios
                lstDuplicado = _arrayDuplicado
                    .GroupBy(c => new FileInfo(c).Name.Split('_')[0])
                    .Where(pdf => pdf.Count() > 1)
                    .Select(rej => rej.Key).ToList();


                lblQtd.Text = string.Format("Total: {0}", _arrayDuplicado.Count.ToString());
               // lbltotalDuplicado.Text = string.Format("Duplicados: {0}", lstDuplicado.Count);
                lblTempo.Text = "";

                if (lstDuplicado.Count > 0)
                {
                    BackgroundWorkerDuplicadoFiltro.RunWorkerAsync();
                    stopwatch.Restart();
                }
                else
                {
                    MessageBox.Show("Nenhum arquivo duplicado foi localizado para tela" + tela, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                Close();
                MessageBox.Show("Erro ao tentar iniciar o processo de leitura de contrato,\n Descrição: " + ex.Message,"Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
        public FrmDuplicadoFiltro(string _diretoioPdf, string _tela, List<string> _lstDamp03)
        {
            lstDamp3 = _lstDamp03;
            diretorioOrigemPdf = _diretoioPdf; tela = Regex.Replace(_tela, @"[^A-Z0-9$]", "");
            InitializeComponent();
        }


        private void BackgroundWorkerDuplicadoFiltro_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            obj = (UserObject)e.UserState;

            progressBarReaderPdf.Value = e.ProgressPercentage;

            if (obj != null)
            {
                tmp = string.Format("Tempo de Execução: {0}:{1}:{2}:{3} ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);

                _indiceSubstring = obj.PdfInfo.FullName.Length / 2;

                lblDescricao.Text = _descricao;
                lblPorcentagem.Text = string.Format("{0:P2}", (double)e.ProgressPercentage / (double)(progressBarReaderPdf.Maximum));
                lblTempo.Text = tmp;
                lblContrato.Text = string.Format("Contrato: {0}", obj.Contrato);
                lblDiretorio.Text = string.Format("Diretório: ...{0}", obj.PdfInfo.FullName.Substring(_indiceSubstring));
                lbltotalDuplicado.Text = "Aguarde...";

                progressBarReaderPdf.Maximum = MaximumProgress;
            }

        }

        private void BackgroundWorkerDuplicadoFiltro_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbltotalDuplicado.Text = "Concluído!";
            // TOTAL DE ARQUIVOS DUPLICADO
            int _totalDup = Directory.GetFiles(diretorioOrigemPdf, string.Format("*_{0}.dup", Regex.Replace(tela, @"[^0-9$]", "")), SearchOption.AllDirectories).Count();

            // TOTAL DE ARQUIVOS FILTRADOS , CONTRATOS QUE NAO ESTÃO NO ARQUIVO DE PONTEIRO
            int _totalFiltro = Directory.GetFiles(diretorioOrigemPdf, string.Format("*_{0}.fil", Regex.Replace(tela, @"[^0-9$]", "")), SearchOption.AllDirectories).Count();

            string result = string.Format("Resultado\n\n");
            result += string.Format("Total Filtrado: {0}{1}\n", _totalFiltro, " arquivo(s)");
            result += string.Format("Total Duplicado: {0}{1}", _totalDup, " arquivo(s)");

            MessageBox.Show(result, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }

        private void BackgroundWorkerDuplicadoFiltro_DoWork(object sender, DoWorkEventArgs e)
        {
            lst16 = Directory.GetFiles(diretorioOrigemPdf, "*_16.pdf", SearchOption.AllDirectories).ToList();
            FileInfo f = null;

            // VERIFICA SE OS CONTRATOS DO PONTEIRO TEM DAMP 
            // SE HOUVER DAMP E NAO EXISTIR A TELA 18, ENTÃO RENOMEIA OS ARQUISO 16,20,25
            if (Regex.Replace(tela, @"[^0-9$]", "").Equals("18"))
            {
                BackgroundWorkerDuplicadoFiltro.ReportProgress(0, null);
                List<string> result = lstDamp3.Join(lstArquiPoint, dmp => dmp.Trim(), pont => pont, (dmp, pont) => pont).ToList();

                List<string> lstTela18 = Directory.GetFiles(diretorioOrigemPdf, string.Format("*_{0}.pdf", Regex.Replace(tela, @"[^0-9$]", "")), SearchOption.AllDirectories).ToList();
                List<string> lst20 = Directory.GetFiles(diretorioOrigemPdf, "*_20.pdf", SearchOption.AllDirectories).ToList();
                List<string> lst25 = Directory.GetFiles(diretorioOrigemPdf, "*_25.pdf", SearchOption.AllDirectories).ToList();
                string strTela = string.Empty;

                MaximumProgress = result.Count;

                _descricao = "Verificação de DAMPs...";
                result.ForEach(dmp =>
                {
                    countpercent++;
                    if (!lstTela18.Any(gg => gg.Contains(dmp)))
                    {
                        strTela = lst16.FirstOrDefault(c => c.Contains(dmp));
                        if (!string.IsNullOrWhiteSpace(strTela))
                        {
                            f = new FileInfo(strTela);
                            if (!string.IsNullOrWhiteSpace(strTela))
                                File.Move(strTela, System.IO.Path.ChangeExtension(strTela, ".damp"));

                            strTela = lst20.FirstOrDefault(c => c.Contains(dmp));
                            if (!string.IsNullOrWhiteSpace(strTela))
                                File.Move(strTela, System.IO.Path.ChangeExtension(strTela, ".damp"));

                            strTela = lst25.FirstOrDefault(c => c.Contains(dmp));
                            if (!string.IsNullOrWhiteSpace(strTela))
                                File.Move(strTela, System.IO.Path.ChangeExtension(strTela, ".damp"));

                            var o = new UserObject() { Contrato = dmp, PdfInfo = f };
                            BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                        }
                    }
                });

                strTela = string.Empty;
            }


            // VERIFICA OS ARQUIVOS QUE ESTÃO DUPLICADOS E RENOMEIA COM A EXTENSÃO .dup
            if (_arrayDuplicado.Count > 0)
            {
                countpercent = 0;
                BackgroundWorkerDuplicadoFiltro.ReportProgress(0, null);
                MaximumProgress = lstDuplicado.Count;

                _descricao = "Verificando duplicidade de contratos...";
                int countDuplicado = 0;
                lstDuplicado.ForEach(dup =>
                {
                    countpercent++;
                    // se houver mais de 2 arquivos duplicados, altera a extensão de todos os arquivos para '.dup' deixando apenas uma arquivo
                    countDuplicado = _arrayDuplicado.Count(k => k.Contains(dup));
                    if (countDuplicado > 2)
                    {
                        for (int i = 1; i < countDuplicado; i++)
                        {
                            f = new FileInfo(_arrayDuplicado.FirstOrDefault(k => k.Contains(dup)));
                            File.Move(f.FullName, System.IO.Path.ChangeExtension(f.FullName, ".dup"));
                            _arrayDuplicado.Remove(f.FullName);
                        }
                    }
                    else
                    {
                        f = new FileInfo(_arrayDuplicado.FirstOrDefault(k => k.Contains(dup)));
                        File.Move(f.FullName, System.IO.Path.ChangeExtension(f.FullName, ".dup"));
                        _arrayDuplicado.Remove(f.FullName);
                    }
                    var o = new UserObject() { Contrato = dup.Split('_')[0], PdfInfo = f };
                    BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                });

                BackgroundWorkerDuplicadoFiltro.ReportProgress(0, null);
                _descricao = "Aplicando Filtro nos contratos sem ponteiro";
                MaximumProgress = _arrayDuplicado.Count;
                countpercent = 0;

                // altera a extensão dos arquivos pdfs que não estão na relação do arquivo de ponteiro 'ARQUIPOINT.TXT'
                // se não tiver no ponteiro então renomeia para extensão .fil
                _arrayDuplicado.ForEach(pdf =>
                {
                    countpercent++;
                    f = new FileInfo(pdf);
                    if (!lstArquiPoint.Any(n => n.Contains(f.Name.Split('_')[0])))
                        File.Move(f.FullName, System.IO.Path.ChangeExtension(f.FullName, ".fil"));

                    var o = new UserObject() { Contrato = f.Name.Split('_')[0], PdfInfo = f };
                    BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                });

            }

            // Verifica se as telas 18,20,25 tambem tem tela 16
            // se existir telas 18,20,25, obrigatoriamente devem ter tela 16

            if (!tela.Equals("TELA16"))
            {

                List<string> listaTela16 = new List<string>();
                lst16.ForEach(h => { listaTela16.Add(new FileInfo(h).Name.Split('_')[0].Trim()); });

                List<string> listaTelaFiltro = new List<string>();
                _arrayDuplicado.ForEach(h => { listaTelaFiltro.Add(new FileInfo(h).Name.Split('_')[0].Trim()); });

                var q = listaTelaFiltro.GroupJoin(listaTela16,
                        k => k,
                        y => y,
                        (k, y)
                        => new { campoPrimary = k, campoKey = y.FirstOrDefault() })
                        .Where(g => string.IsNullOrWhiteSpace(g.campoKey))
                        .ToList();

                if (q.Count > 0)
                {
                    string _contratoPdf = string.Empty;

                    BackgroundWorkerDuplicadoFiltro.ReportProgress(0, null);
                    _descricao = "Aplicar rejeição de contratos sem ponteiro";
                    MaximumProgress = q.Count;
                    countpercent = 0;
                    q.ForEach(p =>
                    {
                        countpercent++;
                        string arquivo = _arrayDuplicado.SingleOrDefault(r => r.Contains(p.campoPrimary));
                        f = new FileInfo(arquivo);

                        if (f.Exists)
                            if (f.Extension.Equals(".pdf"))
                                File.Move(arquivo, System.IO.Path.ChangeExtension(arquivo, ".rej"));

                        var o = new UserObject() { Contrato = f.Name.Split('_')[0], PdfInfo = f };
                        BackgroundWorkerDuplicadoFiltro.ReportProgress(countpercent, o);
                    });
                }
            }
        }
    }
}
