using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConvetPdfToLayoutAlta
{
    public partial class frmSelectFolder : Form
    {
        public frmSelectFolder()
        {
            InitializeComponent();
        }

        private void btnSelectDiretorioOrigem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectFolders();
                if (!File.Exists(textOrigemContratosPdf.Text + @"\SITU115A.TXT"))
                {
                    MessageBox.Show("Não foi encontrato o arquivo de Situações(SITU115A.TXT) de contratos neste diretório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnIniciarConvercao.Enabled = (textDestinoLayout.TextLength > 0 && textOrigemContratosPdf.TextLength > 0);
            }
        }


        private void btnSelectDiretorioDestino_Click(object sender, EventArgs e)
        {
            btnSelectDiretorioOrigem_Click(null, null);
        }

        /// <summary>
        /// Seleciona os diretório de Origem e Destino
        /// </summary>
        void SelectFolders()
        {
            if (string.IsNullOrWhiteSpace(textOrigemContratosPdf.Text))
                textOrigemContratosPdf.Text = folderBrowserDialog1.SelectedPath;
            else
                textDestinoLayout.Text = folderBrowserDialog1.SelectedPath;

        }

        private void btnIniciarConvercao_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmGerarLayoutAlta f = new frmGerarLayoutAlta(this.textOrigemContratosPdf.Text, this.textDestinoLayout.Text);
            f.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

          //  var sbDados2 = "1,00200.00";
          //  var match = Regex.IsMatch(sbDados2.ToString(), @"^[0-2,]$");

            frmGerarLayoutAlta f = new frmGerarLayoutAlta(@"C:\Blocado\", "");
            f.ShowDialog();
            this.Show();
        }

        
    }
}
