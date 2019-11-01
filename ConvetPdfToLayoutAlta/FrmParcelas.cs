using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmParcelas : Form
    {
        BusinessParcelas businessParcelas = null;

        public FrmParcelas()
        {
            InitializeComponent();
        }

        List<Parcela> GetParcelas()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxContrato.Text))
                    return null;

                businessParcelas = new BusinessParcelas();
                return businessParcelas.GetParcelas(textBoxContrato.Text);
            }
            
            catch (Exception exErro)
            {
                throw new Exception("Erro na tentativar de carregar dos dados: " + exErro.Message);
            }

        }

        private void FrmParcelas_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewParcelas.DataSource = null;
                dataGridViewParcelas.AutoGenerateColumns = false;
                dataGridViewParcelas.DataSource = GetParcelas();
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
            Cursor.Current = Cursors.WaitCursor;
            Enabled = !Enabled;
           
            if (string.IsNullOrWhiteSpace(textBoxContrato.Text.Trim()))
            {
                MessageBox.Show("Informe o número de contrato para realizar a consulta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxContrato.Focus();
                Enabled = !Enabled;
                return;
            }
            dataGridViewParcelas.DataSource = null;
            dataGridViewParcelas.DataSource = GetParcelas();

            if (dataGridViewParcelas.Rows.Count == 0)
                MessageBox.Show("Nenhum registro encontrado para o contrato " + textBoxContrato.Text, "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Enabled = !Enabled;
            Cursor.Current = Cursors.Default;
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
