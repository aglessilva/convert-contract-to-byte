using ClosedXML.Excel;
using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvetPdfToLayoutAlta
{
    public partial class FrmConsultaFgts : Form
    {
        public FrmConsultaFgts()
        {
            InitializeComponent();
        }

        private void btnLimpaFiltro_Click(object sender, EventArgs e)
        {
            textBoxContrato.Text = string.Empty;
            dataGridViewDampfgts.DataSource = null;
        }

        private void btnPesquisaContrato_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessTela18 businessTela18 = new BusinessTela18();

                if (string.IsNullOrWhiteSpace(textBoxContrato.Text.Trim()))
                {
                    MessageBox.Show("Informe o número de contrato para realizar a consulta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxContrato.Focus();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                dataGridViewDampfgts.AutoGenerateColumns = false;
                groupBoxHistoricoParcela.Enabled = !groupBoxHistoricoParcela.Enabled;
                dataGridViewDampfgts.DataSource = null;
                dataGridViewDampfgts.Enabled = false;
               
                dataGridViewDampfgts.DataSource = businessTela18.GetParcelaFgts(textBoxContrato.Text.Trim());
               // dataGridViewDampfgts.Columns[0].Visible = false;
               // dataGridViewDampfgts.Columns[2].Visible = false;

                dataGridViewDampfgts.Enabled = true;
                groupBoxHistoricoParcela.Enabled = !groupBoxHistoricoParcela.Enabled;
                Cursor.Current = Cursors.Default;

                if (dataGridViewDampfgts.Rows.Count == 0)
                    MessageBox.Show("Nenhum registro encontrado para o contrato " + textBoxContrato.Text, "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExportarExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xlsx)|*.xlsx";
            fichero.FileName = $"{textBoxContrato.Text}-FGTS";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                //Creating DataTable.
                DataTable dt = new DataTable();

                //Adding the Columns.
                foreach (DataGridViewColumn column in dataGridViewDampfgts.Columns)
                {
                    dt.Columns.Add(column.HeaderText, column.ValueType);
                }

                //Adding the Rows.
                foreach (DataGridViewRow row in dataGridViewDampfgts.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }

                //Exporting to Excel.

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "FGTS");

                    //Adjust widths of Columns.
                    wb.Worksheet(1).Columns().AdjustToContents();

                    //Save the Excel file.
                    wb.SaveAs(fichero.FileName);

                    MessageBox.Show("Exportação concluída", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
