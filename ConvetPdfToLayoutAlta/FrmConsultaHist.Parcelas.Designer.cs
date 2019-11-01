namespace ConvetPdfToLayoutAlta
{
    partial class FrmCabecalho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCabecalho));
            this.groupBoxHistoricoParcela = new System.Windows.Forms.GroupBox();
            this.btnLimpaFiltro = new System.Windows.Forms.Button();
            this.textBoxContrato = new System.Windows.Forms.TextBox();
            this.btnPesquisaContrato = new System.Windows.Forms.Button();
            this.dataGridViewHistoricaParcelas = new System.Windows.Forms.DataGridView();
            this.groupBoxHistoricoParcela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoricaParcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxHistoricoParcela
            // 
            this.groupBoxHistoricoParcela.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHistoricoParcela.Controls.Add(this.btnLimpaFiltro);
            this.groupBoxHistoricoParcela.Controls.Add(this.textBoxContrato);
            this.groupBoxHistoricoParcela.Controls.Add(this.btnPesquisaContrato);
            this.groupBoxHistoricoParcela.Location = new System.Drawing.Point(12, 12);
            this.groupBoxHistoricoParcela.Name = "groupBoxHistoricoParcela";
            this.groupBoxHistoricoParcela.Size = new System.Drawing.Size(1137, 67);
            this.groupBoxHistoricoParcela.TabIndex = 1;
            this.groupBoxHistoricoParcela.TabStop = false;
            this.groupBoxHistoricoParcela.Text = "Pesquisa por Contrato";
            // 
            // btnLimpaFiltro
            // 
            this.btnLimpaFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpaFiltro.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.clear_filters_48_45590__1_;
            this.btnLimpaFiltro.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLimpaFiltro.Location = new System.Drawing.Point(1077, 19);
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
            this.textBoxContrato.Size = new System.Drawing.Size(922, 38);
            this.textBoxContrato.TabIndex = 0;
            // 
            // btnPesquisaContrato
            // 
            this.btnPesquisaContrato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisaContrato.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaContrato.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.xmag_search_find_export_locate_5984;
            this.btnPesquisaContrato.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisaContrato.Location = new System.Drawing.Point(927, 19);
            this.btnPesquisaContrato.Name = "btnPesquisaContrato";
            this.btnPesquisaContrato.Size = new System.Drawing.Size(142, 40);
            this.btnPesquisaContrato.TabIndex = 1;
            this.btnPesquisaContrato.Text = "Pesquisar";
            this.btnPesquisaContrato.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaContrato.UseVisualStyleBackColor = true;
            this.btnPesquisaContrato.Click += new System.EventHandler(this.btnPesquisaContrato_Click);
            // 
            // dataGridViewHistoricaParcelas
            // 
            this.dataGridViewHistoricaParcelas.AllowUserToAddRows = false;
            this.dataGridViewHistoricaParcelas.AllowUserToDeleteRows = false;
            this.dataGridViewHistoricaParcelas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewHistoricaParcelas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewHistoricaParcelas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewHistoricaParcelas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewHistoricaParcelas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistoricaParcelas.Location = new System.Drawing.Point(12, 83);
            this.dataGridViewHistoricaParcelas.Name = "dataGridViewHistoricaParcelas";
            this.dataGridViewHistoricaParcelas.ReadOnly = true;
            this.dataGridViewHistoricaParcelas.RowHeadersVisible = false;
            this.dataGridViewHistoricaParcelas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewHistoricaParcelas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistoricaParcelas.Size = new System.Drawing.Size(1137, 362);
            this.dataGridViewHistoricaParcelas.TabIndex = 2;
            // 
            // FrmCabecalho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 450);
            this.Controls.Add(this.dataGridViewHistoricaParcelas);
            this.Controls.Add(this.groupBoxHistoricoParcela);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmCabecalho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "08 - Histórico de Parcelas ";
            this.groupBoxHistoricoParcela.ResumeLayout(false);
            this.groupBoxHistoricoParcela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoricaParcelas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxHistoricoParcela;
        private System.Windows.Forms.Button btnLimpaFiltro;
        private System.Windows.Forms.TextBox textBoxContrato;
        private System.Windows.Forms.Button btnPesquisaContrato;
        private System.Windows.Forms.DataGridView dataGridViewHistoricaParcelas;
    }
}