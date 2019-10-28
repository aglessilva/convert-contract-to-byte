using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            catch (SqlException sqlErro)
            {
                MessageBox.Show($"Erro ao consultar hitórico de parcelas\nMsg:{sqlErro.Message}\nProcedure:{sqlErro.Procedure}\nLinha:{sqlErro.LineNumber}");
            }

            return _historicoParcelas;
        }
    }
}
