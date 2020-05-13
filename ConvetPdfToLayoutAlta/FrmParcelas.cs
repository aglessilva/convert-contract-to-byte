using ClosedXML.Excel;
using ConvetPdfToLayoutAlta.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        private void buttonExportarExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xlsx)|*.xlsx";
            fichero.FileName = $"{textBoxContrato.Text}-parcelas";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                //Creating DataTable.
                DataTable dt = new DataTable();

                //Adding the Columns.
                foreach (DataGridViewColumn column in dataGridViewParcelas.Columns)
                {
                    dt.Columns.Add(column.HeaderText, column.ValueType);
                }

                //Adding the Rows.
                foreach (DataGridViewRow row in dataGridViewParcelas.Rows)
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
                    wb.Worksheets.Add(dt, "Parcelas");

                    //Set the color of Header Row.
                    //A resembles First Column while C resembles Third column.
                    //wb.Worksheet(1).Cells("A1:C1").Style.Fill.BackgroundColor = XLColor.DarkGreen;
                    //for (int i = 1; i <= dt.Rows.Count; i++)
                    //{
                    //    //A resembles First Column while C resembles Third column.
                    //    //Header row is at Position 1 and hence First row starts from Index 2.
                    //    string cellRange = string.Format("A{0}:C{0}", i + 1);
                    //    if (i % 2 != 0)
                    //    {
                    //        wb.Worksheet(1).Cells(cellRange).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                    //    }
                    //    else
                    //    {
                    //        wb.Worksheet(1).Cells(cellRange).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //    }

                    //}
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
