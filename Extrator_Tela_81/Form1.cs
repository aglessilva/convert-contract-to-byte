using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Extrator_Tela_81
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FolderBrowserDialog folderBrowserDialog = null;

        public List<string> TrataArray(string _linha)
        {
            List<string> arrayLinha = _linha.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            return arrayLinha;
        }

        private void IniciarExtracao(string _pathDiretorioTela81)
        {
            int contador = 0;
            int indice = 0;
            string pagina  = string.Empty;
            string _dataPagamento = string.Empty;
            string _contrato = string.Empty;
            string seguro = string.Empty;
            List<decimal> listaDecimal = new List<decimal>();
            List<string> lista = new List<string>();
            List<string> ListaLinhas = new List<string>();

            IEnumerable<string> listDiretory = Directory.GetFiles(_pathDiretorioTela81, "*.pdf", SearchOption.AllDirectories);

            using (StreamWriter streamWriter = new StreamWriter($@"{textBoxPathArquivo.Text}\Tela81_MIP_DFI.txt", true, Encoding.Default))
            {
                listDiretory.ToList().ForEach(d =>
                {
                    using (PdfReader reader = new PdfReader(d))
                    {
                        ITextExtractionStrategy its;
                        _contrato = string.Empty;

                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            its = new LocationTextExtractionStrategy();
                            ListaLinhas.Clear();
                            contador = 0;
                            pagina = PdfTextExtractor.GetTextFromPage(reader, i, its).Trim();
                            pagina = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pagina)));

                            using (StringReader strReader = new StringReader(pagina))
                            {
                                string line;

                                while ((line = strReader.ReadLine()) != null)
                                {
                                    seguro = _dataPagamento = string.Empty;
                                    listaDecimal.Clear();
                                    lista.Clear();

                                    if (string.IsNullOrWhiteSpace(line))
                                        continue;

                                    lista = TrataArray(line).ToList();

                                    if (lista.Any(c => Regex.IsMatch(c, @"(^\d{4}.\d{5}.\d{3}-\d{1}$)")))
                                    {
                                        _contrato = $"{lista[1].Substring(2)}{ Regex.Replace(lista[2], @"[^0-9$]", "")}";
                                        continue;
                                    }

                                    contador++;
                                    if (!lista.Any(c => Regex.IsMatch(c, @"(^\d{3}\/\d{3}$)"))) continue;

                                    if (lista.Count(r => Regex.IsMatch(r, @"(^\d{1},\d{6}$)")) < 2)
                                    {
                                        lista.RemoveAll(r => Regex.IsMatch(r, @"(^\d{1},\d{6}$)"));
                                        lista.Insert(1, "0");
                                        lista.Insert(2, "0");
                                    }

                                    if (lista.Count(r => Regex.IsMatch(r, @"(^\d{2}\/\d{2}\/\d{4}$)")) < 2)
                                        lista.Insert(4, "0001/01/01");


                                    // SE A LINHA DA EXTRAÇÃO ESTIVER DANIFICADA, ESSE BLOCO FAZ O AJUSTE E REMODELA A LINHA
                                    if (lista.Count <= 6)
                                    {
                                        List<string> lstAjuste = new List<string>();

                                        string s = lista.FirstOrDefault(k => Regex.IsMatch(k, @"(^\d{3}\/\d{3}$)"));
                                        lista.Remove(s);

                                        var tab = pagina.Split('\n').Select((k, y) => new { key = y, valor = k }).Where(o => o.key >= (contador - 2) && o.key <= (contador + 2)).ToList();

                                        lstAjuste = tab.FirstOrDefault(f => !f.valor.Split(' ').Any(sq => Regex.IsMatch(sq, @"(^\d{3}\/\d{3}$)"))).valor.Split(' ').ToList();
                                        lstAjuste.Insert(0, s);

                                        lista.ForEach(u => {
                                            if (!u.Equals("0") && !u.Equals("0001/01/01"))
                                                lstAjuste.Add(u);
                                        });

                                        // AJUSTA OS INDEXADORES
                                        if (lstAjuste.Count(r => Regex.IsMatch(r, @"(^\d{1},\d{6}$)")) < 2)
                                        {
                                            lstAjuste.RemoveAll(r => Regex.IsMatch(r, @"(^\d{1},\d{6}$)"));
                                            lstAjuste.Insert(1, "0");
                                            lstAjuste.Insert(2, "0");
                                        }

                                        // AJUSTA AS DATA DE VENCIMENTO E PAGAMENTO
                                        if (lstAjuste.Count(r => Regex.IsMatch(r, @"(^\d{2}\/\d{2}\/\d{4}$)")) < 2)
                                            lstAjuste.Insert(4, "0001/01/01");

                                        lista = lstAjuste;
                                    }

                                    _dataPagamento = lista.FirstOrDefault(dt => Regex.IsMatch(dt, @"(^\d{2}\/\d{2}\/\d{4}$)"));

                                    indice =  lista.FindLastIndex(dt => Regex.IsMatch(dt, @"(^\d{2}\/\d{2}\/\d{4}$)"));

                                    // CARENCIA NÃO TEM VALOR DE PRESTAÇÃO, ENTÃO RESGATA O SEGUNDO MAIOR VALOR DA LISTA
                                    if (lista.Any(s => s.Contains("-")))
                                        lista.Insert(7, lista[6]);

                                    lista.RemoveRange(0, (indice + 3));

                                    lista.ForEach(fd => { listaDecimal.Add(Convert.ToDecimal(fd));});

                                    int max = listaDecimal.FindIndex(m => m.Equals(listaDecimal.Max(x => x)));

                                    listaDecimal = listaDecimal.Distinct().ToList();

                                    listaDecimal.RemoveRange(max, ((listaDecimal.Count - max) == 0 ? 0 : listaDecimal.Count - max));

                                    if (listaDecimal.Count == 5)
                                    {
                                        listaDecimal = listaDecimal.Where(w => w < listaDecimal[0] && w < listaDecimal[4]).ToList();

                                        if (listaDecimal.Count == 2)
                                        {
                                            seguro = _contrato + _dataPagamento + listaDecimal[0].ToString().PadLeft(18, '0') + listaDecimal[1].ToString().PadLeft(18, '0');
                                            streamWriter.WriteLine(seguro);
                                            seguro = string.Empty;
                                            listaDecimal.Clear();
                                            continue;
                                        }
                                        else
                                        {
                                            if (listaDecimal.Count == 3)
                                            {
                                                listaDecimal.RemoveAt(2);

                                                if (listaDecimal.Count == 2)
                                                {
                                                    seguro = _contrato + _dataPagamento + listaDecimal[0].ToString().PadLeft(18, '0') + listaDecimal[1].ToString().PadLeft(18, '0');
                                                    streamWriter.WriteLine(seguro);
                                                    seguro = string.Empty;
                                                    listaDecimal.Clear();
                                                    continue;
                                                }
                                            }
                                        }

                                        continue;
                                    }
                                    else
                                    {
                                        if (listaDecimal.Count == 4)
                                        {
                                            listaDecimal = listaDecimal.Where(w => w < listaDecimal[0] && w < listaDecimal[3]).ToList();

                                            if (listaDecimal.Count == 2)
                                            {
                                                seguro = _contrato + _dataPagamento + listaDecimal[0].ToString().PadLeft(18, '0') + listaDecimal[1].ToString().PadLeft(18, '0');
                                                streamWriter.WriteLine(seguro);
                                                seguro = string.Empty;
                                                listaDecimal.Clear();
                                                continue;
                                            }

                                        }
                                        else
                                        {
                                            if(listaDecimal.Count == 3)
                                            {
                                                listaDecimal.RemoveAt(2);

                                                if(listaDecimal.Count == 2)
                                                {
                                                    seguro = _contrato + _dataPagamento + listaDecimal[0].ToString().PadLeft(18,'0') + listaDecimal[1].ToString().PadLeft(18, '0');
                                                    streamWriter.WriteLine(seguro);
                                                    seguro = string.Empty;
                                                    listaDecimal.Clear();
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                indice = (listaDecimal.Count() - 1);
                                                listaDecimal = listaDecimal.Where(w => w < listaDecimal[0] && w < listaDecimal[indice]).ToList();

                                                if(listaDecimal.Count >= 2)
                                                {
                                                    seguro = _contrato + _dataPagamento + listaDecimal[0].ToString().PadLeft(18, '0') + listaDecimal[1].ToString().PadLeft(18, '0');
                                                    streamWriter.WriteLine(seguro);
                                                    seguro = string.Empty;
                                                    listaDecimal.Clear();
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    if(listaDecimal.Count == 1)
                                    {
                                        seguro = _contrato + _dataPagamento + listaDecimal[0].ToString().PadLeft(18, '0') + listaDecimal[0].ToString().PadLeft(18, '0');
                                        streamWriter.WriteLine(seguro);
                                        seguro = string.Empty;
                                        listaDecimal.Clear();
                                        continue;
                                    }

                                    listaDecimal.Clear();
                                }

                            }
                        }
                    }
                });
            }

            MessageBox.Show("Extração conlcuída", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog = new FolderBrowserDialog();

            folderBrowserDialog.Description = "Selecionne o diretório das Telas 81";
            folderBrowserDialog.ShowNewFolderButton = false;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxPathDiretorioTela81.Text = folderBrowserDialog.SelectedPath;
            }
            else
                textBoxPathDiretorioTela81.Text = string.Empty;
            button3.Enabled = (!string.IsNullOrWhiteSpace(textBoxPathArquivo.Text) && !string.IsNullOrWhiteSpace(textBoxPathDiretorioTela81.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Selecionte onde será gerado o arquivo";
            folderBrowserDialog.ShowNewFolderButton = true;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                textBoxPathArquivo.Text = folderBrowserDialog.SelectedPath;
            else
                textBoxPathArquivo.Text = string.Empty;

            button3.Enabled = (!string.IsNullOrWhiteSpace(textBoxPathArquivo.Text) && !string.IsNullOrWhiteSpace(textBoxPathDiretorioTela81.Text));

        }

        private void button3_Click(object sender, EventArgs e)
        {
           // textBoxPathArquivo.Text = @"C:\Tela81\testeTela81";
          //  textBoxPathDiretorioTela81.Text = @"C:\Tela81\testeTela81";

            Opacity = .70;
            if (File.Exists($@"{textBoxPathArquivo.Text}\Tela81_MIP_DFI.txt"))
                File.Delete($@"{textBoxPathArquivo.Text}\Tela81_MIP_DFI.txt");

            button3.Enabled = !button3.Enabled;
            IniciarExtracao(textBoxPathDiretorioTela81.Text);
            button3.Enabled = !button3.Enabled;
            Opacity = 1;
        }
    }
}
