namespace Extrator_Tela_81
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxPathDiretorioTela81 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxPathArquivo = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPathDiretorioTela81
            // 
            this.textBoxPathDiretorioTela81.Enabled = false;
            this.textBoxPathDiretorioTela81.Location = new System.Drawing.Point(12, 21);
            this.textBoxPathDiretorioTela81.Name = "textBoxPathDiretorioTela81";
            this.textBoxPathDiretorioTela81.Size = new System.Drawing.Size(444, 20);
            this.textBoxPathDiretorioTela81.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Selecione o diretório das telas 81";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(327, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Selecione o diretório onde será gerado o arquivo extraido dos PDF\'s";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(456, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 22);
            this.button2.TabIndex = 5;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxPathArquivo
            // 
            this.textBoxPathArquivo.Enabled = false;
            this.textBoxPathArquivo.Location = new System.Drawing.Point(12, 58);
            this.textBoxPathArquivo.Name = "textBoxPathArquivo";
            this.textBoxPathArquivo.Size = new System.Drawing.Size(444, 20);
            this.textBoxPathArquivo.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(168, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(146, 22);
            this.button3.TabIndex = 7;
            this.button3.Text = "INICIAR EXTRAÇÃO.";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 117);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxPathArquivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxPathDiretorioTela81);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extração da Tela 81 ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxPathDiretorioTela81;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxPathArquivo;
        private System.Windows.Forms.Button button3;
    }
}

