namespace ConvetPdfToLayoutAlta
{
    partial class FrmConsultaFgts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaFgts));
            this.groupBoxHistoricoParcela = new System.Windows.Forms.GroupBox();
            this.btnLimpaFiltro = new System.Windows.Forms.Button();
            this.textBoxContrato = new System.Windows.Forms.TextBox();
            this.btnPesquisaContrato = new System.Windows.Forms.Button();
            this.dataGridViewDampfgts = new System.Windows.Forms.DataGridView();
            this.buttonExportarExcel = new System.Windows.Forms.Button();
            this.Contrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataVencimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelaQuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuotaNominal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoFgtsJAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoFgtsQUO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SobraMes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SobraMesJAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SobraAcumulada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorUtilizado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxHistoricoParcela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDampfgts)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxHistoricoParcela
            // 
            this.groupBoxHistoricoParcela.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHistoricoParcela.Controls.Add(this.buttonExportarExcel);
            this.groupBoxHistoricoParcela.Controls.Add(this.btnLimpaFiltro);
            this.groupBoxHistoricoParcela.Controls.Add(this.textBoxContrato);
            this.groupBoxHistoricoParcela.Controls.Add(this.btnPesquisaContrato);
            this.groupBoxHistoricoParcela.Location = new System.Drawing.Point(12, 12);
            this.groupBoxHistoricoParcela.Name = "groupBoxHistoricoParcela";
            this.groupBoxHistoricoParcela.Size = new System.Drawing.Size(1137, 67);
            this.groupBoxHistoricoParcela.TabIndex = 2;
            this.groupBoxHistoricoParcela.TabStop = false;
            this.groupBoxHistoricoParcela.Text = "Pesquisa por Contrato";
            // 
            // btnLimpaFiltro
            // 
            this.btnLimpaFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpaFiltro.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.clear_filters_48_45590__1_;
            this.btnLimpaFiltro.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLimpaFiltro.Location = new System.Drawing.Point(1035, 19);
            this.btnLimpaFiltro.Name = "btnLimpaFiltro";
            this.btnLimpaFiltro.Size = new System.Drawing.Size(49, 40);
            this.btnLimpaFiltro.TabIndex = 2;
            this.btnLimpaFiltro.UseVisualStyleBackColor = true;
            this.btnLimpaFiltro.Click += new System.EventHandler(this.btnLimpaFiltro_Click);
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
            // 
            // btnPesquisaContrato
            // 
            this.btnPesquisaContrato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisaContrato.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaContrato.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.xmag_search_find_export_locate_5984;
            this.btnPesquisaContrato.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisaContrato.Location = new System.Drawing.Point(894, 19);
            this.btnPesquisaContrato.Name = "btnPesquisaContrato";
            this.btnPesquisaContrato.Size = new System.Drawing.Size(142, 40);
            this.btnPesquisaContrato.TabIndex = 1;
            this.btnPesquisaContrato.Text = "Pesquisar";
            this.btnPesquisaContrato.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaContrato.UseVisualStyleBackColor = true;
            this.btnPesquisaContrato.Click += new System.EventHandler(this.btnPesquisaContrato_Click);
            // 
            // dataGridViewDampfgts
            // 
            this.dataGridViewDampfgts.AllowUserToAddRows = false;
            this.dataGridViewDampfgts.AllowUserToDeleteRows = false;
            this.dataGridViewDampfgts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewDampfgts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDampfgts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDampfgts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDampfgts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDampfgts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Contrato,
            this.DataVencimento,
            this.ParcelaQuota,
            this.QuotaNominal,
            this.SaldoFgtsJAM,
            this.SaldoFgtsQUO,
            this.SobraMes,
            this.SobraMesJAM,
            this.SobraAcumulada,
            this.ValorUtilizado});
            this.dataGridViewDampfgts.Location = new System.Drawing.Point(12, 83);
            this.dataGridViewDampfgts.Name = "dataGridViewDampfgts";
            this.dataGridViewDampfgts.ReadOnly = true;
            this.dataGridViewDampfgts.RowHeadersVisible = false;
            this.dataGridViewDampfgts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewDampfgts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDampfgts.Size = new System.Drawing.Size(1137, 362);
            this.dataGridViewDampfgts.TabIndex = 3;
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
            // Contrato
            // 
            this.Contrato.DataPropertyName = "Contrato";
            this.Contrato.HeaderText = "Contrato";
            this.Contrato.Name = "Contrato";
            this.Contrato.ReadOnly = true;
            this.Contrato.Width = 80;
            // 
            // DataVencimento
            // 
            this.DataVencimento.DataPropertyName = "DataVencimento";
            this.DataVencimento.HeaderText = "Vencimento";
            this.DataVencimento.Name = "DataVencimento";
            this.DataVencimento.ReadOnly = true;
            this.DataVencimento.Width = 80;
            // 
            // ParcelaQuota
            // 
            this.ParcelaQuota.DataPropertyName = "ParcelaQuota";
            this.ParcelaQuota.HeaderText = "Parcela Quota";
            this.ParcelaQuota.Name = "ParcelaQuota";
            this.ParcelaQuota.ReadOnly = true;
            this.ParcelaQuota.Width = 120;
            // 
            // QuotaNominal
            // 
            this.QuotaNominal.DataPropertyName = "QuotaNominal";
            this.QuotaNominal.HeaderText = "Quota Nominal";
            this.QuotaNominal.Name = "QuotaNominal";
            this.QuotaNominal.ReadOnly = true;
            this.QuotaNominal.Width = 120;
            // 
            // SaldoFgtsJAM
            // 
            this.SaldoFgtsJAM.DataPropertyName = "SaldoFgtsJAM";
            this.SaldoFgtsJAM.HeaderText = "Saldo Fgts JAM";
            this.SaldoFgtsJAM.Name = "SaldoFgtsJAM";
            this.SaldoFgtsJAM.ReadOnly = true;
            this.SaldoFgtsJAM.Width = 120;
            // 
            // SaldoFgtsQUO
            // 
            this.SaldoFgtsQUO.DataPropertyName = "SaldoFgtsQUO";
            this.SaldoFgtsQUO.HeaderText = "Saldo Fgts QUO";
            this.SaldoFgtsQUO.Name = "SaldoFgtsQUO";
            this.SaldoFgtsQUO.ReadOnly = true;
            this.SaldoFgtsQUO.Width = 125;
            // 
            // SobraMes
            // 
            this.SobraMes.DataPropertyName = "SobraMes";
            this.SobraMes.HeaderText = "Sobra Mes";
            this.SobraMes.Name = "SobraMes";
            this.SobraMes.ReadOnly = true;
            // 
            // SobraMesJAM
            // 
            this.SobraMesJAM.DataPropertyName = "SobraMesJAM";
            this.SobraMesJAM.HeaderText = "Sobra Mes JAM";
            this.SobraMesJAM.Name = "SobraMesJAM";
            this.SobraMesJAM.ReadOnly = true;
            this.SobraMesJAM.Width = 120;
            // 
            // SobraAcumulada
            // 
            this.SobraAcumulada.DataPropertyName = "SobraAcumulada";
            this.SobraAcumulada.HeaderText = "Sobra Acumulada";
            this.SobraAcumulada.Name = "SobraAcumulada";
            this.SobraAcumulada.ReadOnly = true;
            this.SobraAcumulada.Width = 130;
            // 
            // ValorUtilizado
            // 
            this.ValorUtilizado.DataPropertyName = "ValorUtilizado";
            this.ValorUtilizado.HeaderText = "Valor Utilizado";
            this.ValorUtilizado.Name = "ValorUtilizado";
            this.ValorUtilizado.ReadOnly = true;
            this.ValorUtilizado.Width = 120;
            // 
            // FrmConsultaFgts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 450);
            this.Controls.Add(this.dataGridViewDampfgts);
            this.Controls.Add(this.groupBoxHistoricoParcela);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmConsultaFgts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de FGTS (Damp)";
            this.groupBoxHistoricoParcela.ResumeLayout(false);
            this.groupBoxHistoricoParcela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDampfgts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxHistoricoParcela;
        private System.Windows.Forms.Button btnLimpaFiltro;
        private System.Windows.Forms.TextBox textBoxContrato;
        private System.Windows.Forms.Button btnPesquisaContrato;
        private System.Windows.Forms.DataGridView dataGridViewDampfgts;
        private System.Windows.Forms.Button buttonExportarExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataVencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelaQuota;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuotaNominal;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoFgtsJAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoFgtsQUO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SobraMes;
        private System.Windows.Forms.DataGridViewTextBoxColumn SobraMesJAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SobraAcumulada;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorUtilizado;
    }
}