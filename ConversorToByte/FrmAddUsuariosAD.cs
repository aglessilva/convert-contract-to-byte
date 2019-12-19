using ConversorToByte.BLL;
using ConversorToByte.DTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Threading;
using System.Windows.Forms;

namespace ConversorToByte
{
    public partial class FrmAddUsuariosAD : Form
    {
        
        private List<Users> lst = null;
        FileSafeOperations fso = null;
        string dados = null;

        public FrmAddUsuariosAD()
        {
            InitializeComponent();
            fso = new FileSafeOperations();
        }

        private void FrmAddUsuariosAD_Load(object sender, EventArgs e)
        {
            DataGridViewUsuario.AutoGenerateColumns = false;
            Height = (Screen.PrimaryScreen.Bounds.Height - 30);
            BindGrid();
        }



        public string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            return string.Empty;
        }

        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                Invoke((MethodInvoker)delegate
                {
                    pnlSpinner.Visible = groupBox1.Enabled;
                    Cursor =Cursors.WaitCursor;
                    pnlUser.Enabled = false;
                    lblNome.Text = string.Empty;
                    lblEmail.Text = string.Empty;
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    pnlSpinner.Visible = !groupBox1.Enabled;
                    Cursor = Cursors.Default;
                    HabilitarCampo();
                });
            }
        }


        private void DisplayData()
        {
            SetLoading(true);

            DirectoryEntry entry = new DirectoryEntry("LDAP://DCBS05");

            DirectorySearcher dSearch = new DirectorySearcher(entry);

            dSearch.Filter = "(&(objectClass=user)(samaccountname=" + textBoxLogin.Text.Trim() + "))";
            dSearch.PageSize = 100;
            dSearch.SizeLimit = 100;

            

            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                dados = GetProperty(sResultSet, "cn") + "|"; // Nome do usuario
                dados += GetProperty(sResultSet, "mail"); // Email do usuario
            }

            SetLoading(false);

        }


        void HabilitarCampo()
        {
            pnlUser.Enabled = true;

            if (!string.IsNullOrWhiteSpace(dados))
            {
                lblNome.Text = dados.Split('|')[0].ToString();
                lblEmail.Text = dados.Split('|')[1].ToString();
                btnAdd.Enabled = true;
                dados = null;
            }
            else
            {
                lblNome.Text = "Não Encontrado";
                lblEmail.Text = "Não Encontrado";
                btnAdd.Enabled = false;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {

                Thread threadInput = new Thread(DisplayData);
                threadInput.Start();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na tentantida de pesquisa..\nERRO: " + ex.Message, "ERRO" , MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            btnPesquisar.Enabled = (textBoxLogin.Text.Length >= 6);

            if (pnlUser.Enabled)
                btnAdd.Enabled = false;
        }


        private void textBoxPesquisa_TextChanged(object sender, EventArgs e)
        {
            DataGridViewUsuario.AutoGenerateColumns = false;
            if(textBoxPesquisa.Text.Length > 2)
            {
                DataGridViewUsuario.DataSource = null;
                lst = fso.GetUsers(textBoxPesquisa.Text);
                DataGridViewUsuario.DataSource = lst;
            }

            if (textBoxPesquisa.Text.Length == 0)
            {
                DataGridViewUsuario.DataSource = null;
                lst = fso.GetUsers();
                DataGridViewUsuario.DataSource = lst;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Users obj = new Users()
            {
                UserLogin = textBoxLogin.Text,
                UserEmail = lblEmail.Text,
                UserName = lblNome.Text
            };

            int ret = fso.AddUser(obj);
            if (ret > 1)
            {
                MessageBox.Show("Usuário " + textBoxLogin.Text + " ja cadastrado", "Validação de Usuário", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            lblEmail.Text = lblNome.Text = "-";
            pnlUser.Enabled = btnPesquisar.Enabled = false;
            textBoxLogin.Text = string.Empty;
            textBoxLogin.Focus();
            BindGrid();

            
        }


        void BindGrid()
        {
            FileSafeOperations fso = new FileSafeOperations();
            DataGridViewUsuario.DataSource = fso.GetUsers();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxPesquisa.Text = string.Empty;
        }

        private void DataGridViewUsuario_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 3)
                    return;

                DataGridView dataGridUser = (DataGridView)sender;
                if (dataGridUser.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    Users obj = (Users)DataGridViewUsuario.Rows[e.RowIndex].DataBoundItem;
                    fso.UdtUser(obj.UserLogin);
                    lst = fso.GetUsers(string.IsNullOrWhiteSpace(textBoxPesquisa.Text) ? null : textBoxPesquisa.Text);
                    DataGridViewUsuario.DataSource = lst;
                }

                if (dataGridUser.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0 && e.ColumnIndex == 4)
                {
                    Users obj = (Users)DataGridViewUsuario.Rows[e.RowIndex].DataBoundItem;
                    if (MessageBox.Show($"Confirma a exclusão do usuário { obj.UserName } ?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                   fso.DltUser(obj.UserLogin);
                    lst = fso.GetUsers(string.IsNullOrWhiteSpace(textBoxPesquisa.Text) ? null : textBoxPesquisa.Text);
                    DataGridViewUsuario.DataSource = lst;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                throw;
            }
        }
    }
}

