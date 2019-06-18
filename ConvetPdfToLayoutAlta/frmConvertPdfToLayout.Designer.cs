namespace ConvetPdfToLayoutAlta
{
    partial class FrmSelectFolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectFolder));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectDiretorioDestino = new System.Windows.Forms.Button();
            this.btnSelectDiretorioOrigem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textDestinoLayout = new System.Windows.Forms.TextBox();
            this.textOrigemContratosPdf = new System.Windows.Forms.TextBox();
            this.btnIniciarConvercao = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectDiretorioDestino);
            this.groupBox1.Controls.Add(this.btnSelectDiretorioOrigem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textDestinoLayout);
            this.groupBox1.Controls.Add(this.textOrigemContratosPdf);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleção de Diretórios ";
            // 
            // btnSelectDiretorioDestino
            // 
            this.btnSelectDiretorioDestino.Location = new System.Drawing.Point(502, 83);
            this.btnSelectDiretorioDestino.Name = "btnSelectDiretorioDestino";
            this.btnSelectDiretorioDestino.Size = new System.Drawing.Size(40, 22);
            this.btnSelectDiretorioDestino.TabIndex = 5;
            this.btnSelectDiretorioDestino.Text = "...";
            this.btnSelectDiretorioDestino.UseVisualStyleBackColor = true;
            this.btnSelectDiretorioDestino.Click += new System.EventHandler(this.BtnSelectDiretorioDestino_Click);
            // 
            // btnSelectDiretorioOrigem
            // 
            this.btnSelectDiretorioOrigem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectDiretorioOrigem.Location = new System.Drawing.Point(502, 39);
            this.btnSelectDiretorioOrigem.Name = "btnSelectDiretorioOrigem";
            this.btnSelectDiretorioOrigem.Size = new System.Drawing.Size(40, 22);
            this.btnSelectDiretorioOrigem.TabIndex = 4;
            this.btnSelectDiretorioOrigem.Text = "...";
            this.btnSelectDiretorioOrigem.UseVisualStyleBackColor = true;
            this.btnSelectDiretorioOrigem.Click += new System.EventHandler(this.BtnSelectDiretorioOrigem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selecione o diretório dos contratos bloqueados";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selecione o diretório para os arquivos sequenciais que serão gerados";
            // 
            // textDestinoLayout
            // 
            this.textDestinoLayout.Location = new System.Drawing.Point(15, 84);
            this.textDestinoLayout.Name = "textDestinoLayout";
            this.textDestinoLayout.ReadOnly = true;
            this.textDestinoLayout.Size = new System.Drawing.Size(487, 20);
            this.textDestinoLayout.TabIndex = 1;
            // 
            // textOrigemContratosPdf
            // 
            this.textOrigemContratosPdf.Location = new System.Drawing.Point(15, 40);
            this.textOrigemContratosPdf.Name = "textOrigemContratosPdf";
            this.textOrigemContratosPdf.ReadOnly = true;
            this.textOrigemContratosPdf.Size = new System.Drawing.Size(487, 20);
            this.textOrigemContratosPdf.TabIndex = 0;
            // 
            // btnIniciarConvercao
            // 
            this.btnIniciarConvercao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnIniciarConvercao.Enabled = false;
            this.btnIniciarConvercao.Image = global::ConvetPdfToLayoutAlta.Properties.Resources._1485477153_arrow_right_78596;
            this.btnIniciarConvercao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIniciarConvercao.Location = new System.Drawing.Point(224, 140);
            this.btnIniciarConvercao.Name = "btnIniciarConvercao";
            this.btnIniciarConvercao.Size = new System.Drawing.Size(128, 32);
            this.btnIniciarConvercao.TabIndex = 1;
            this.btnIniciarConvercao.Text = "Iniciar Converção";
            this.btnIniciarConvercao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIniciarConvercao.UseVisualStyleBackColor = true;
            this.btnIniciarConvercao.Click += new System.EventHandler(this.BtnIniciarConvercao_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Seleção de diretórios Origem/Destino";
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.SelectedPath = "C:\\Blocado";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(479, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // frmSelectFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 178);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnIniciarConvercao);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Converter contratos para formato sequencial   (v1.0) ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectDiretorioDestino;
        private System.Windows.Forms.Button btnSelectDiretorioOrigem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textDestinoLayout;
        private System.Windows.Forms.TextBox textOrigemContratosPdf;
        private System.Windows.Forms.Button btnIniciarConvercao;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
    }
}

