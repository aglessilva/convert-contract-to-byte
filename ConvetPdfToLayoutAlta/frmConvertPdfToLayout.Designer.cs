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
            this.comboBoxTela = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panelSpinner = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panelSpinner.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
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
            this.btnSelectDiretorioDestino.Visible = false;
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
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selecione o diretório das VM\'s";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Diretório da conversão";
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
            this.btnIniciarConvercao.Image = global::ConvetPdfToLayoutAlta.Properties.Resources._1485477153_arrow_right_78596;
            this.btnIniciarConvercao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIniciarConvercao.Location = new System.Drawing.Point(224, 170);
            this.btnIniciarConvercao.Name = "btnIniciarConvercao";
            this.btnIniciarConvercao.Size = new System.Drawing.Size(128, 32);
            this.btnIniciarConvercao.TabIndex = 1;
            this.btnIniciarConvercao.Text = "Iniciar Conversão";
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
            this.button1.Location = new System.Drawing.Point(479, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // comboBoxTela
            // 
            this.comboBoxTela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTela.FormattingEnabled = true;
            this.comboBoxTela.Items.AddRange(new object[] {
            "TELA 16",
            "TELA 18",
            "TELA 20",
            "TELA 25",
            "Selecione o tipo de arquivo"});
            this.comboBoxTela.Location = new System.Drawing.Point(12, 13);
            this.comboBoxTela.Name = "comboBoxTela";
            this.comboBoxTela.Size = new System.Drawing.Size(190, 21);
            this.comboBoxTela.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(28, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // panelSpinner
            // 
            this.panelSpinner.Controls.Add(this.button2);
            this.panelSpinner.Controls.Add(this.comboBoxTela);
            this.panelSpinner.Controls.Add(this.button1);
            this.panelSpinner.Controls.Add(this.btnIniciarConvercao);
            this.panelSpinner.Controls.Add(this.groupBox1);
            this.panelSpinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSpinner.Location = new System.Drawing.Point(0, 0);
            this.panelSpinner.Name = "panelSpinner";
            this.panelSpinner.Size = new System.Drawing.Size(580, 210);
            this.panelSpinner.TabIndex = 5;
            // 
            // FrmSelectFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 210);
            this.Controls.Add(this.panelSpinner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Converter contratos para formato sequencial   (v1.0) ";
            this.Load += new System.EventHandler(this.FrmSelectFolder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelSpinner.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox comboBoxTela;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panelSpinner;
    }
}

