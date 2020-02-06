using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmFoders : Form
    {
        FolderBrowserDialog folderBrowserDialog1 = null;
        public FrmFoders()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            List<DirectoryInfo> _diretorio = new DirectoryInfo(@"\\mscluster40fs\plataformaPF2\TOMBAMENTO_PF\Processamento\").GetDirectories().ToList();
            List<object> listDatas = new List<object>();
            _diretorio.ForEach(f => {
                var item = new { isvalues = false, folderData = f.Name };
                listDatas.Add(f.Name);
            });


            folderBrowserDialog1  = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Selecione o diretório onde serão salvos os arquivos da ALTA";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                panelNovaExtracao.Visible = !panelNovaExtracao.Visible;
                checkedListBoxDatas.Items.AddRange(listDatas.ToArray());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strDatas = string.Empty;

            for (int i = 0; i < checkedListBoxDatas.CheckedItems.Count; i++)
            {
                strDatas += checkedListBoxDatas.CheckedItems[i].ToString() +"|";
            }


            if (string.IsNullOrWhiteSpace(strDatas))
            {
                MessageBox.Show("nenhumna data foi selecionada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            List<string> listDatasFolder = strDatas.Split('|').ToList().Where(k => !string.IsNullOrWhiteSpace(k)).ToList();
            panelNovaExtracao.Visible = false;
            buttonNovaExtracao.Visible = buttonConsultarReprocessar.Visible = false;


            FrmDownload f = new FrmDownload(listDatasFolder, folderBrowserDialog1.SelectedPath);
            f.ShowDialog();
            Hide();
            FrmSelectFolder frmSelect = new FrmSelectFolder(folderBrowserDialog1.SelectedPath, true);
            frmSelect.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            FrmSelectFolder frmSelect = new FrmSelectFolder(string.Empty, false);
            frmSelect.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
