using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmCabecalho : Form
    {
        List<HistoricoParcela> _historicoParcelas = null;

        public FrmCabecalho()
        {
            InitializeComponent();
        }

       

        private void btnPesquisaContrato_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBoxContrato.Text.Trim()))
            {
                MessageBox.Show("Informe o número de contrato para realizar a consulta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxContrato.Focus();
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            groupBoxHistoricoParcela.Enabled = !groupBoxHistoricoParcela.Enabled;
            dataGridViewHistoricaParcelas.DataSource = null;
            dataGridViewHistoricaParcelas.Enabled = false;
            dataGridViewHistoricaParcelas.DataSource = GetHistoricoParcelas();
            dataGridViewHistoricaParcelas.Enabled = true;
            groupBoxHistoricoParcela.Enabled = !groupBoxHistoricoParcela.Enabled;
            Cursor.Current = Cursors.Default;

            if (dataGridViewHistoricaParcelas.Rows.Count == 0)
                MessageBox.Show("Nenhum registro encontrado para o contrato " + textBoxContrato.Text, "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnLimpaFiltro_Click(object sender, EventArgs e)
        {
            textBoxContrato.Text = string.Empty;
            dataGridViewHistoricaParcelas.DataSource = null;
        }

        List<HistoricoParcela> GetHistoricoParcelas()
        {
            BusinessHistoricoParcelas businessHistoricoParcelas = new BusinessHistoricoParcelas();
            try
            {
                _historicoParcelas =  businessHistoricoParcelas.GetHistoricoParcelas(textBoxContrato.Text.Trim());
            }
            catch (Exception sqlErro)
            {
                MessageBox.Show($"Erro ao consultar hitórico de parcelas\nMsg:{sqlErro.Message}");
            }

            return _historicoParcelas;
        }
    }
}
