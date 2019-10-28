using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmParcelas : Form
    {

        private Conn cnx = null;
        private SqlCommand command = null;
        List<Parcela> lst = null;

        public FrmParcelas()
        {
            InitializeComponent();
        }

        List<Parcela> GetParcelas(string _contrato = null)
        {
            List<Parcela> listaParcelas = new List<Parcela>();
            try
            {
                Parcela objParcela = null;
                cnx = new Conn();
                command = cnx.Parametriza("SP_GET_PARCELAS");
                command.Connection = command.Connection;
                command.Connection.Open();

                if (!string.IsNullOrWhiteSpace(_contrato))
                    command.Parameters.Add(new SqlParameter("@Contrato", _contrato.Trim()));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    objParcela = new Parcela
                    {
                        Carteira = reader[0].ToString(),
                        Contrato = reader[1].ToString(),
                        Vencimento = reader[2].ToString(),
                        DataBaseContrato = reader[3].ToString(),
                        Indice = reader[4].ToString(),
                        Pagamento = reader[5].ToString(),
                        NumeroPrazo = reader[6].ToString(),
                        Prestacao = reader[7].ToString(),
                        Seguro = reader[8].ToString(),
                        Taxa = reader[9].ToString(),
                        Fgts = reader[10].ToString(),
                        AmortizacaoCorrecao = reader[11].ToString(),
                        SaldoDevedorCorrecao = reader[12].ToString(),
                        Encargo = reader[13].ToString(),
                        Pago = reader[14].ToString(),
                        Juros = reader[15].ToString(),
                        Mora = reader[16].ToString(),
                        Amortizacao = reader[17].ToString(),
                        Indicador = reader[18].ToString(),
                        SaldoDevedor = reader[19].ToString(),
                        Proc_Emi_Pag = reader[20].ToString(),
                        Iof = reader[21].ToString()
                    };

                    listaParcelas.Add(objParcela);
                }

                return listaParcelas;
            }
            catch (Exception exErro)
            {
                cnx.FecharConexao(command);
                throw new Exception("Erro na tentativar de carregar dos dados: " + exErro.Message);
            }
            finally
            {
                cnx.FecharConexao(command);
            }

        }

        private void FrmParcelas_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewParcelas.DataSource = null;
                List<Parcela> Parcelas = GetParcelas();

                dataGridViewParcelas.AutoGenerateColumns = false;
                dataGridViewParcelas.DataSource = Parcelas;
                textBoxContrato.Focus();

            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lst = GetParcelas(textBoxContrato.Text);
            dataGridViewParcelas.DataSource = null;
            dataGridViewParcelas.DataSource = lst;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxContrato.Text = string.Empty;
            dataGridViewParcelas.DataSource = null;
            dataGridViewParcelas.DataSource = GetParcelas();
            textBoxContrato.Focus();
        }


        private void textBoxContrato_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)022)
                e.Handled = true;
        }

       
    }
}
