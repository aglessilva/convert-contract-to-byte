namespace ConvetPdfToLayoutAlta
{
    partial class FrmFoders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFoders));
            this.panelNovaExtracao = new System.Windows.Forms.Panel();
            this.BtnVoltar = new System.Windows.Forms.Button();
            this.checkedListBoxDatas = new System.Windows.Forms.CheckedListBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.buttonNovaExtracao = new System.Windows.Forms.Button();
            this.buttonConsultarReprocessar = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelload = new System.Windows.Forms.Panel();
            this.lblLoad = new System.Windows.Forms.Label();
            this.lblRotulo = new System.Windows.Forms.Label();
            this.pictureBoxImgLoad = new System.Windows.Forms.PictureBox();
            this.panelNovaExtracao.SuspendLayout();
            this.panelload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // panelNovaExtracao
            // 
            this.panelNovaExtracao.Controls.Add(this.BtnVoltar);
            this.panelNovaExtracao.Controls.Add(this.checkedListBoxDatas);
            this.panelNovaExtracao.Controls.Add(this.btnDownload);
            this.panelNovaExtracao.Controls.Add(this.lblTitulo);
            this.panelNovaExtracao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.panelNovaExtracao.Location = new System.Drawing.Point(53, 0);
            this.panelNovaExtracao.Name = "panelNovaExtracao";
            this.panelNovaExtracao.Size = new System.Drawing.Size(455, 201);
            this.panelNovaExtracao.TabIndex = 6;
            this.panelNovaExtracao.Visible = false;
            // 
            // BtnVoltar
            // 
            this.BtnVoltar.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.back_12955;
            this.BtnVoltar.Location = new System.Drawing.Point(187, 149);
            this.BtnVoltar.Name = "BtnVoltar";
            this.BtnVoltar.Size = new System.Drawing.Size(37, 40);
            this.BtnVoltar.TabIndex = 7;
            this.BtnVoltar.UseVisualStyleBackColor = true;
            this.BtnVoltar.Click += new System.EventHandler(this.BtnVoltar_Click);
            // 
            // checkedListBoxDatas
            // 
            this.checkedListBoxDatas.CheckOnClick = true;
            this.checkedListBoxDatas.FormattingEnabled = true;
            this.checkedListBoxDatas.Location = new System.Drawing.Point(20, 26);
            this.checkedListBoxDatas.Name = "checkedListBoxDatas";
            this.checkedListBoxDatas.Size = new System.Drawing.Size(154, 164);
            this.checkedListBoxDatas.TabIndex = 3;
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.download_arrow_14460;
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownload.Location = new System.Drawing.Point(187, 66);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(255, 77);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Baixar Pastas e Arquivos";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(45, 4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(380, 20);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Selecione as datas  disponíveis para  extração";
            // 
            // buttonNovaExtracao
            // 
            this.buttonNovaExtracao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.buttonNovaExtracao.Image = global::ConvetPdfToLayoutAlta.Properties.Resources._1490890032_24_82551;
            this.buttonNovaExtracao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNovaExtracao.Location = new System.Drawing.Point(153, 20);
            this.buttonNovaExtracao.Name = "buttonNovaExtracao";
            this.buttonNovaExtracao.Size = new System.Drawing.Size(292, 76);
            this.buttonNovaExtracao.TabIndex = 4;
            this.buttonNovaExtracao.Text = "Nova Extração";
            this.buttonNovaExtracao.UseVisualStyleBackColor = true;
            this.buttonNovaExtracao.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonConsultarReprocessar
            // 
            this.buttonConsultarReprocessar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.buttonConsultarReprocessar.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.xmag_search_find_export_locate_5984;
            this.buttonConsultarReprocessar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConsultarReprocessar.Location = new System.Drawing.Point(153, 102);
            this.buttonConsultarReprocessar.Name = "buttonConsultarReprocessar";
            this.buttonConsultarReprocessar.Size = new System.Drawing.Size(292, 76);
            this.buttonConsultarReprocessar.TabIndex = 5;
            this.buttonConsultarReprocessar.Text = "Consultar / Reprocessar";
            this.buttonConsultarReprocessar.UseVisualStyleBackColor = true;
            this.buttonConsultarReprocessar.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.folder_my_documents_22605;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 5;
            // 
            // panelload
            // 
            this.panelload.Controls.Add(this.lblLoad);
            this.panelload.Controls.Add(this.lblRotulo);
            this.panelload.Controls.Add(this.pictureBoxImgLoad);
            this.panelload.Location = new System.Drawing.Point(2, 0);
            this.panelload.Name = "panelload";
            this.panelload.Size = new System.Drawing.Size(571, 198);
            this.panelload.TabIndex = 8;
            this.panelload.Visible = false;
            // 
            // lblLoad
            // 
            this.lblLoad.AutoSize = true;
            this.lblLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblLoad.ForeColor = System.Drawing.Color.Red;
            this.lblLoad.Location = new System.Drawing.Point(263, 110);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(56, 13);
            this.lblLoad.TabIndex = 9;
            this.lblLoad.Text = "Aguarde...";
            // 
            // lblRotulo
            // 
            this.lblRotulo.AutoSize = true;
            this.lblRotulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblRotulo.Location = new System.Drawing.Point(161, 88);
            this.lblRotulo.Name = "lblRotulo";
            this.lblRotulo.Size = new System.Drawing.Size(269, 16);
            this.lblRotulo.TabIndex = 8;
            this.lblRotulo.Text = "Carregando números do BEM de contratos. ";
            // 
            // pictureBoxImgLoad
            // 
            this.pictureBoxImgLoad.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.ajax_loader;
            this.pictureBoxImgLoad.Location = new System.Drawing.Point(266, 53);
            this.pictureBoxImgLoad.Name = "pictureBoxImgLoad";
            this.pictureBoxImgLoad.Size = new System.Drawing.Size(31, 31);
            this.pictureBoxImgLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxImgLoad.TabIndex = 7;
            this.pictureBoxImgLoad.TabStop = false;
            // 
            // FrmFoders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 201);
            this.Controls.Add(this.panelload);
            this.Controls.Add(this.panelNovaExtracao);
            this.Controls.Add(this.buttonNovaExtracao);
            this.Controls.Add(this.buttonConsultarReprocessar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFoders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmFoders_Load);
            this.panelNovaExtracao.ResumeLayout(false);
            this.panelNovaExtracao.PerformLayout();
            this.panelload.ResumeLayout(false);
            this.panelload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgLoad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonConsultarReprocessar;
        private System.Windows.Forms.Button buttonNovaExtracao;
        private System.Windows.Forms.Panel panelNovaExtracao;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.CheckedListBox checkedListBoxDatas;
        private System.Windows.Forms.Button BtnVoltar;
        private System.Windows.Forms.Panel panelload;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.Label lblRotulo;
        private System.Windows.Forms.PictureBox pictureBoxImgLoad;
    }
}