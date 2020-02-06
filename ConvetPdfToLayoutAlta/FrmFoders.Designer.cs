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
            this.buttonConsultarReprocessar = new System.Windows.Forms.Button();
            this.buttonNovaExtracao = new System.Windows.Forms.Button();
            this.panelNovaExtracao = new System.Windows.Forms.Panel();
            this.checkedListBoxDatas = new System.Windows.Forms.CheckedListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelNovaExtracao.SuspendLayout();
            this.SuspendLayout();
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
            // panelNovaExtracao
            // 
            this.panelNovaExtracao.Controls.Add(this.checkedListBoxDatas);
            this.panelNovaExtracao.Controls.Add(this.button3);
            this.panelNovaExtracao.Controls.Add(this.label1);
            this.panelNovaExtracao.Location = new System.Drawing.Point(53, 0);
            this.panelNovaExtracao.Name = "panelNovaExtracao";
            this.panelNovaExtracao.Size = new System.Drawing.Size(455, 201);
            this.panelNovaExtracao.TabIndex = 6;
            this.panelNovaExtracao.Visible = false;
            // 
            // checkedListBoxDatas
            // 
            this.checkedListBoxDatas.CheckOnClick = true;
            this.checkedListBoxDatas.FormattingEnabled = true;
            this.checkedListBoxDatas.Location = new System.Drawing.Point(20, 26);
            this.checkedListBoxDatas.Name = "checkedListBoxDatas";
            this.checkedListBoxDatas.Size = new System.Drawing.Size(154, 169);
            this.checkedListBoxDatas.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.download_arrow_14460;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(187, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(255, 77);
            this.button3.TabIndex = 2;
            this.button3.Text = "Baixar Pastas e Arquivos";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selecione as datas  disponíveis para  extração";
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
            // FrmFoders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 201);
            this.Controls.Add(this.panelNovaExtracao);
            this.Controls.Add(this.buttonNovaExtracao);
            this.Controls.Add(this.buttonConsultarReprocessar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFoders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelNovaExtracao.ResumeLayout(false);
            this.panelNovaExtracao.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonConsultarReprocessar;
        private System.Windows.Forms.Button buttonNovaExtracao;
        private System.Windows.Forms.Panel panelNovaExtracao;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.CheckedListBox checkedListBoxDatas;
    }
}