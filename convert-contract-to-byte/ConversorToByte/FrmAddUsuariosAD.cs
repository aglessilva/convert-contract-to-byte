using ConversorToByte.BLL;
using ConversorToByte.DTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Windows.Forms;

namespace ConversorToByte
{
    public partial class FrmAddUsuariosAD : Form
    {
        private FileSafeOperations objFileOperation = null;
        private List<Users> lst = null;
        FileSafeOperations fso = null;
        public FrmAddUsuariosAD()
        {
            InitializeComponent();
            fso = new FileSafeOperations();
        }

        private void FrmAddUsuariosAD_Load(object sender, EventArgs e)
        {
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
          
            lblNome.Text = string.Empty;
            lblEmail.Text = string.Empty;

            pnlUser.Enabled = false;

            DirectoryEntry entry = new DirectoryEntry("LDAP://DCBS05");

            DirectorySearcher dSearch = new DirectorySearcher(entry);

            dSearch.Filter = "(&(objectClass=user)(samaccountname=" + textBoxLogin.Text.Trim() + "))";
            dSearch.PageSize = 100;
            dSearch.SizeLimit = 100;

            string dados = null;

            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                dados =  GetProperty(sResultSet, "cn") +"|"; // Nome do usuario
                dados += GetProperty(sResultSet, "mail"); // Email do usuario
            }

            pnlUser.Enabled = true;

            if (!string.IsNullOrWhiteSpace(dados))
            {
                lblNome.Text = dados.Split('|')[0].ToString();
                lblEmail.Text = dados.Split('|')[1].ToString();
                btnAdd.Enabled = true;
            }
            else
            {
                lblNome.Text = "Não Encontrado";
                lblEmail.Text = "Não Encontrado";
                btnAdd.Enabled = false;
            }

           
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            btnPesquisar.Enabled = (textBoxLogin.Text.Length >= 6);
        }

        private void dataGridViewUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                Users obj = (Users)dataGridViewUsuario.Rows[e.RowIndex].DataBoundItem;
                fso.UdtUser(obj.UserLogin);
                lst = fso.GetUsers();
                dataGridViewUsuario.DataSource = lst;
            }
        }

        private void textBoxPesquisa_TextChanged(object sender, EventArgs e)
        {
            dataGridViewUsuario.AutoGenerateColumns = false;
            if(textBoxPesquisa.Text.Length > 2)
            {
                dataGridViewUsuario.DataSource = null;
                lst = fso.GetUsers(textBoxPesquisa.Text);
                dataGridViewUsuario.DataSource = lst;
            }

            if (textBoxPesquisa.Text.Length == 0)
            {
                dataGridViewUsuario.DataSource = null;
                lst = fso.GetUsers();
                dataGridViewUsuario.DataSource = lst;
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

            BindGrid();
        }


        void BindGrid()
        {
            FileSafeOperations fso = new FileSafeOperations();
            dataGridViewUsuario.DataSource = fso.GetUsers();   
        }
    }
}

