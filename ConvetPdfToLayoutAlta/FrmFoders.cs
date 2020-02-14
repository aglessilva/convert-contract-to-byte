using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
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
            const string path = @"\\mscluster40fs\plataformaPF2\TOMBAMENTO_PF\Processamento\";

           if (!VerificaAcesso(path))
            {
                MessageBox.Show($"Descrição: Acesso negado!\n\nUsuário {Environment.UserName} não tem permissão de acesso ao diretório para realizar o download dos contratos.\nSolicite autorização ao gestor do diretório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            List<DirectoryInfo> _diretorio = new DirectoryInfo(path).GetDirectories().ToList();

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

        private void btnDownload_Click(object sender, EventArgs e)
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

       

        private void FrmFoders_Load(object sender, EventArgs e)
        {
            try
            {
                if(!File.Exists($@"{Directory.GetCurrentDirectory()}\config\QUERY_FOR_FILTER_FOR_QUERY__000.xlsx"))
                {
                    MessageBox.Show("O arquivo 'QUERY_FOR_FILTER_FOR_QUERY__000.xlsx' para consulta do número do BEM dos contratos, não foi encontrato na pasta 'config'", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Close();
                }

                if (Ambiente.listGTBem.Count == 0)
                {
                    Task threadInput = new Task(() =>
                    {
                        EnabledDisabled(true);
                        Ambiente.GetContratoNumeroBem();
                        EnabledDisabled(false);
                    });
                    threadInput.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na tentantida de pesquisa..\nERRO: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        void EnabledDisabled(bool setLoad)
        {
            if (setLoad)
                Invoke((MethodInvoker)delegate
                    {
                        Cursor = Cursors.WaitCursor;
                        panelload.Visible = setLoad;
                    });
            else
                Invoke((MethodInvoker)delegate
                    {
                        panelload.Visible = setLoad;
                        Cursor = Cursors.Default;
                    });

        }

        
        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            panelNovaExtracao.Visible = !panelNovaExtracao.Visible;
        }
        

        bool VerificaAcesso(string directoryPath)
        {
            bool isWriteAccess = false;
            try
            {
                AuthorizationRuleCollection collection = Directory.GetAccessControl(directoryPath).GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (FileSystemAccessRule rule in collection)
                {
                    if (rule.AccessControlType == AccessControlType.Allow)
                    {
                        isWriteAccess = true;
                        break;
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                isWriteAccess = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na tentativa de inicializar o sistema\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }


            return isWriteAccess;
        }
    }
}
