namespace ConvetPdfToLayoutAlta
{
    partial class FrmParcelas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParcelas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxContrato = new System.Windows.Forms.TextBox();
            this.dataGridViewParcelas = new System.Windows.Forms.DataGridView();
            this.Carteira = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vencimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataBaseContrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Indice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pagamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroPrazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prestacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seguro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Taxa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fgts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmortizacaoCorrecao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoDevedorCorrecao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Encargo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Juros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amortizacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Indicador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoDevedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proc_Emi_Pag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iof = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonExportarExcel = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonExportarExcel);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBoxContrato);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1137, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pesquisa de Parcelas  por Contrato";
            // 
            // textBoxContrato
            // 
            this.textBoxContrato.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContrato.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.textBoxContrato.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.textBoxContrato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContrato.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxContrato.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBoxContrato.Location = new System.Drawing.Point(6, 20);
            this.textBoxContrato.MaxLength = 15;
            this.textBoxContrato.Name = "textBoxContrato";
            this.textBoxContrato.Size = new System.Drawing.Size(888, 38);
            this.textBoxContrato.TabIndex = 0;
            this.textBoxContrato.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxContrato_KeyPress);
            // 
            // dataGridViewParcelas
            // 
            this.dataGridViewParcelas.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewParcelas.AllowUserToAddRows = false;
            this.dataGridViewParcelas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewParcelas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewParcelas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewParcelas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewParcelas.CausesValidation = false;
            this.dataGridViewParcelas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewParcelas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewParcelas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParcelas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Carteira,
            this.Contrato,
            this.Vencimento,
            this.DataBaseContrato,
            this.Indice,
            this.Pagamento,
            this.NumeroPrazo,
            this.Prestacao,
            this.Seguro,
            this.Taxa,
            this.Fgts,
            this.AmortizacaoCorrecao,
            this.SaldoDevedorCorrecao,
            this.Encargo,
            this.Pago,
            this.Juros,
            this.Mora,
            this.Amortizacao,
            this.Indicador,
            this.SaldoDevedor,
            this.Proc_Emi_Pag,
            this.Iof});
            this.dataGridViewParcelas.Location = new System.Drawing.Point(18, 85);
            this.dataGridViewParcelas.Name = "dataGridViewParcelas";
            this.dataGridViewParcelas.ReadOnly = true;
            this.dataGridViewParcelas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewParcelas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewParcelas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewParcelas.RowHeadersVisible = false;
            this.dataGridViewParcelas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewParcelas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewParcelas.Size = new System.Drawing.Size(1137, 359);
            this.dataGridViewParcelas.TabIndex = 3;
            // 
            // Carteira
            // 
            this.Carteira.DataPropertyName = "Carteira";
            this.Carteira.HeaderText = "Carteira";
            this.Carteira.Name = "Carteira";
            this.Carteira.ReadOnly = true;
            this.Carteira.Width = 68;
            // 
            // Contrato
            // 
            this.Contrato.DataPropertyName = "Contrato";
            this.Contrato.HeaderText = "Contrato";
            this.Contrato.Name = "Contrato";
            this.Contrato.ReadOnly = true;
            this.Contrato.Width = 72;
            // 
            // Vencimento
            // 
            this.Vencimento.DataPropertyName = "Vencimento";
            this.Vencimento.HeaderText = "Vencimento";
            this.Vencimento.Name = "Vencimento";
            this.Vencimento.ReadOnly = true;
            this.Vencimento.Width = 88;
            // 
            // DataBaseContrato
            // 
            this.DataBaseContrato.DataPropertyName = "DataBaseContrato";
            this.DataBaseContrato.HeaderText = "Data Base";
            this.DataBaseContrato.Name = "DataBaseContrato";
            this.DataBaseContrato.ReadOnly = true;
            this.DataBaseContrato.Width = 120;
            // 
            // Indice
            // 
            this.Indice.DataPropertyName = "Indice";
            this.Indice.HeaderText = "Indice";
            this.Indice.Name = "Indice";
            this.Indice.ReadOnly = true;
            this.Indice.Width = 61;
            // 
            // Pagamento
            // 
            this.Pagamento.DataPropertyName = "Pagamento";
            this.Pagamento.HeaderText = "Pagamento";
            this.Pagamento.Name = "Pagamento";
            this.Pagamento.ReadOnly = true;
            this.Pagamento.Width = 86;
            // 
            // NumeroPrazo
            // 
            this.NumeroPrazo.DataPropertyName = "NumeroPrazo";
            this.NumeroPrazo.HeaderText = "Prazo";
            this.NumeroPrazo.Name = "NumeroPrazo";
            this.NumeroPrazo.ReadOnly = true;
            this.NumeroPrazo.Width = 59;
            // 
            // Prestacao
            // 
            this.Prestacao.DataPropertyName = "Prestacao";
            this.Prestacao.HeaderText = "Prestação";
            this.Prestacao.Name = "Prestacao";
            this.Prestacao.ReadOnly = true;
            this.Prestacao.Width = 80;
            // 
            // Seguro
            // 
            this.Seguro.DataPropertyName = "Seguro";
            this.Seguro.HeaderText = "Seguro";
            this.Seguro.Name = "Seguro";
            this.Seguro.ReadOnly = true;
            this.Seguro.Width = 66;
            // 
            // Taxa
            // 
            this.Taxa.DataPropertyName = "Taxa";
            this.Taxa.HeaderText = "Taxa";
            this.Taxa.Name = "Taxa";
            this.Taxa.ReadOnly = true;
            this.Taxa.Width = 56;
            // 
            // Fgts
            // 
            this.Fgts.DataPropertyName = "Fgts";
            this.Fgts.HeaderText = "FGTS";
            this.Fgts.Name = "Fgts";
            this.Fgts.ReadOnly = true;
            this.Fgts.Width = 60;
            // 
            // AmortizacaoCorrecao
            // 
            this.AmortizacaoCorrecao.DataPropertyName = "AmortizacaoCorrecao";
            this.AmortizacaoCorrecao.HeaderText = "Amtz Correção";
            this.AmortizacaoCorrecao.Name = "AmortizacaoCorrecao";
            this.AmortizacaoCorrecao.ReadOnly = true;
            this.AmortizacaoCorrecao.Width = 120;
            // 
            // SaldoDevedorCorrecao
            // 
            this.SaldoDevedorCorrecao.DataPropertyName = "SaldoDevedorCorrecao";
            this.SaldoDevedorCorrecao.HeaderText = "S.D Correção";
            this.SaldoDevedorCorrecao.Name = "SaldoDevedorCorrecao";
            this.SaldoDevedorCorrecao.ReadOnly = true;
            this.SaldoDevedorCorrecao.ToolTipText = "Saldo Devedor Correção";
            this.SaldoDevedorCorrecao.Width = 120;
            // 
            // Encargo
            // 
            this.Encargo.DataPropertyName = "Encargo";
            this.Encargo.HeaderText = "Encargo";
            this.Encargo.Name = "Encargo";
            this.Encargo.ReadOnly = true;
            this.Encargo.Width = 72;
            // 
            // Pago
            // 
            this.Pago.DataPropertyName = "Pago";
            this.Pago.HeaderText = "Pago";
            this.Pago.Name = "Pago";
            this.Pago.ReadOnly = true;
            this.Pago.Width = 57;
            // 
            // Juros
            // 
            this.Juros.DataPropertyName = "Juros";
            this.Juros.HeaderText = "Juros";
            this.Juros.Name = "Juros";
            this.Juros.ReadOnly = true;
            this.Juros.Width = 57;
            // 
            // Mora
            // 
            this.Mora.DataPropertyName = "Mora";
            this.Mora.HeaderText = "Mora";
            this.Mora.Name = "Mora";
            this.Mora.ReadOnly = true;
            this.Mora.Width = 56;
            // 
            // Amortizacao
            // 
            this.Amortizacao.DataPropertyName = "Amortizacao";
            this.Amortizacao.HeaderText = "Amortização";
            this.Amortizacao.Name = "Amortizacao";
            this.Amortizacao.ReadOnly = true;
            this.Amortizacao.Width = 90;
            // 
            // Indicador
            // 
            this.Indicador.DataPropertyName = "Indicador";
            this.Indicador.HeaderText = "Sinal";
            this.Indicador.Name = "Indicador";
            this.Indicador.ReadOnly = true;
            this.Indicador.Width = 55;
            // 
            // SaldoDevedor
            // 
            this.SaldoDevedor.DataPropertyName = "SaldoDevedor";
            this.SaldoDevedor.HeaderText = "Saldo Devedor";
            this.SaldoDevedor.Name = "SaldoDevedor";
            this.SaldoDevedor.ReadOnly = true;
            this.SaldoDevedor.Width = 120;
            // 
            // Proc_Emi_Pag
            // 
            this.Proc_Emi_Pag.DataPropertyName = "Proc_Emi_Pag";
            this.Proc_Emi_Pag.HeaderText = "Proximo Pgto";
            this.Proc_Emi_Pag.Name = "Proc_Emi_Pag";
            this.Proc_Emi_Pag.ReadOnly = true;
            this.Proc_Emi_Pag.Width = 120;
            // 
            // Iof
            // 
            this.Iof.DataPropertyName = "Iof";
            this.Iof.HeaderText = "IOF";
            this.Iof.Name = "Iof";
            this.Iof.ReadOnly = true;
            this.Iof.Width = 49;
            // 
            // buttonExportarExcel
            // 
            this.buttonExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportarExcel.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.microsoft_office_excel;
            this.buttonExportarExcel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonExportarExcel.Location = new System.Drawing.Point(1083, 19);
            this.buttonExportarExcel.Name = "buttonExportarExcel";
            this.buttonExportarExcel.Size = new System.Drawing.Size(49, 40);
            this.buttonExportarExcel.TabIndex = 3;
            this.buttonExportarExcel.UseVisualStyleBackColor = true;
            this.buttonExportarExcel.Click += new System.EventHandler(this.buttonExportarExcel_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.clear_filters_48_45590__1_;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.Location = new System.Drawing.Point(1035, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 40);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.xmag_search_find_export_locate_5984;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(894, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Pesquisar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmParcelas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 450);
            this.Controls.Add(this.dataGridViewParcelas);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmParcelas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Contratos - Parcelas";
            this.Load += new System.EventHandler(this.FrmParcelas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParcelas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxContrato;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewParcelas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Carteira;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataBaseContrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn Indice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pagamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroPrazo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prestacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seguro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Taxa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fgts;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmortizacaoCorrecao;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoDevedorCorrecao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Encargo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Juros;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mora;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amortizacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Indicador;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoDevedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proc_Emi_Pag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iof;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonExportarExcel;
    }
}