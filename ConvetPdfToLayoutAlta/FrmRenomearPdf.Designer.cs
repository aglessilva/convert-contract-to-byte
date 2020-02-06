namespace ConvetPdfToLayoutAlta
{
    partial class FrmRenomearPdf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRenomearPdf));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.progressBarReaderPdf = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblArquivo = new System.Windows.Forms.Label();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.backgroundWorkerRenomearPdf = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblPorcentagem);
            this.panel1.Controls.Add(this.progressBarReaderPdf);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblArquivo);
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 97);
            this.panel1.TabIndex = 8;
            this.panel1.UseWaitCursor = true;
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.Location = new System.Drawing.Point(243, 55);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(13, 13);
            this.lblPorcentagem.TabIndex = 13;
            this.lblPorcentagem.Text = "0";
            this.lblPorcentagem.UseWaitCursor = true;
            // 
            // progressBarReaderPdf
            // 
            this.progressBarReaderPdf.Location = new System.Drawing.Point(12, 26);
            this.progressBarReaderPdf.Name = "progressBarReaderPdf";
            this.progressBarReaderPdf.Size = new System.Drawing.Size(470, 23);
            this.progressBarReaderPdf.Step = 1;
            this.progressBarReaderPdf.TabIndex = 12;
            this.progressBarReaderPdf.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(11, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Renomeando Contratos...";
            this.label1.UseWaitCursor = true;
            // 
            // lblArquivo
            // 
            this.lblArquivo.AutoSize = true;
            this.lblArquivo.ForeColor = System.Drawing.Color.Red;
            this.lblArquivo.Location = new System.Drawing.Point(11, 75);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(46, 13);
            this.lblArquivo.TabIndex = 8;
            this.lblArquivo.Text = "Arquivo:";
            this.lblArquivo.UseWaitCursor = true;
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
            this.lblTempo.Location = new System.Drawing.Point(296, 6);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(40, 13);
            this.lblTempo.TabIndex = 7;
            this.lblTempo.Text = "Tempo";
            this.lblTempo.UseWaitCursor = true;
            // 
            // backgroundWorkerRenomearPdf
            // 
            this.backgroundWorkerRenomearPdf.WorkerReportsProgress = true;
            this.backgroundWorkerRenomearPdf.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerRenomearPdf_DoWork);
            this.backgroundWorkerRenomearPdf.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerRenomearPdf_ProgressChanged);
            this.backgroundWorkerRenomearPdf.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerRenomearPdf_RunWorkerCompleted);
            // 
            // FrmRenomearPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 97);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRenomearPdf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRenomearPdf";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.FrmRenomearPdf_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPorcentagem;
        private System.Windows.Forms.ProgressBar progressBarReaderPdf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblArquivo;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblTempo;
        private System.ComponentModel.BackgroundWorker backgroundWorkerRenomearPdf;
    }
}