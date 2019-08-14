namespace ConvetPdfToLayoutAlta
{
    partial class FrmDuplicadoFiltro
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
            this.BackgroundWorkerDuplicadoFiltro = new System.ComponentModel.BackgroundWorker();
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.lblContrato = new System.Windows.Forms.Label();
            this.progressBarReaderPdf = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDiretorio = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lbltotalDuplicado = new System.Windows.Forms.Label();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackgroundWorkerDuplicadoFiltro
            // 
            this.BackgroundWorkerDuplicadoFiltro.WorkerReportsProgress = true;
            this.BackgroundWorkerDuplicadoFiltro.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDuplicadoFiltro_DoWork);
            this.BackgroundWorkerDuplicadoFiltro.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerDuplicadoFiltro_ProgressChanged);
            this.BackgroundWorkerDuplicadoFiltro.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerDuplicadoFiltro_RunWorkerCompleted);
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.Location = new System.Drawing.Point(243, 55);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(13, 13);
            this.lblPorcentagem.TabIndex = 5;
            this.lblPorcentagem.Text = "0";
            this.lblPorcentagem.UseWaitCursor = true;
            // 
            // lblContrato
            // 
            this.lblContrato.AutoSize = true;
            this.lblContrato.Location = new System.Drawing.Point(11, 54);
            this.lblContrato.Name = "lblContrato";
            this.lblContrato.Size = new System.Drawing.Size(47, 13);
            this.lblContrato.TabIndex = 4;
            this.lblContrato.Text = "Contrato";
            this.lblContrato.UseWaitCursor = true;
            // 
            // progressBarReaderPdf
            // 
            this.progressBarReaderPdf.Location = new System.Drawing.Point(12, 26);
            this.progressBarReaderPdf.Name = "progressBarReaderPdf";
            this.progressBarReaderPdf.Size = new System.Drawing.Size(470, 23);
            this.progressBarReaderPdf.Step = 1;
            this.progressBarReaderPdf.TabIndex = 3;
            this.progressBarReaderPdf.UseWaitCursor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDiretorio);
            this.panel1.Controls.Add(this.lblDescricao);
            this.panel1.Controls.Add(this.lbltotalDuplicado);
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblContrato);
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 97);
            this.panel1.TabIndex = 6;
            this.panel1.UseWaitCursor = true;
            // 
            // lblDiretorio
            // 
            this.lblDiretorio.AutoSize = true;
            this.lblDiretorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiretorio.ForeColor = System.Drawing.Color.Red;
            this.lblDiretorio.Location = new System.Drawing.Point(10, 79);
            this.lblDiretorio.Name = "lblDiretorio";
            this.lblDiretorio.Size = new System.Drawing.Size(10, 13);
            this.lblDiretorio.TabIndex = 10;
            this.lblDiretorio.Text = "-";
            this.lblDiretorio.UseWaitCursor = true;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDescricao.Location = new System.Drawing.Point(245, 8);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(10, 13);
            this.lblDescricao.TabIndex = 9;
            this.lblDescricao.Text = "-";
            this.lblDescricao.UseWaitCursor = true;
            // 
            // lbltotalDuplicado
            // 
            this.lbltotalDuplicado.AutoSize = true;
            this.lbltotalDuplicado.ForeColor = System.Drawing.Color.Green;
            this.lbltotalDuplicado.Location = new System.Drawing.Point(108, 6);
            this.lbltotalDuplicado.Name = "lbltotalDuplicado";
            this.lbltotalDuplicado.Size = new System.Drawing.Size(10, 13);
            this.lbltotalDuplicado.TabIndex = 8;
            this.lbltotalDuplicado.Text = "-";
            this.lbltotalDuplicado.UseWaitCursor = true;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.Location = new System.Drawing.Point(11, 6);
            this.lblQtd.Name = "lblQtd";
            this.lblQtd.Size = new System.Drawing.Size(31, 13);
            this.lblQtd.TabIndex = 7;
            this.lblQtd.Text = "Total";
            this.lblQtd.UseWaitCursor = true;
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(299, 54);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(40, 13);
            this.lblTempo.TabIndex = 7;
            this.lblTempo.Text = "Tempo";
            this.lblTempo.UseWaitCursor = true;
            // 
            // FrmDuplicadoFiltro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 97);
            this.Controls.Add(this.lblPorcentagem);
            this.Controls.Add(this.progressBarReaderPdf);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDuplicadoFiltro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtrar Remover duplicado";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.FrmDuplicadoFiltro_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker BackgroundWorkerDuplicadoFiltro;
        private System.Windows.Forms.Label lblPorcentagem;
        private System.Windows.Forms.Label lblContrato;
        private System.Windows.Forms.ProgressBar progressBarReaderPdf;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lbltotalDuplicado;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Label lblDiretorio;
    }
}