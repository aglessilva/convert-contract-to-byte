namespace ConvetPdfToLayoutAlta
{
    partial class frmGerarLayoutAlta
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.lblContrato = new System.Windows.Forms.Label();
            this.progressBarReaderPdf = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPendente = new System.Windows.Forms.Label();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.Location = new System.Drawing.Point(208, 54);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(13, 13);
            this.lblPorcentagem.TabIndex = 5;
            this.lblPorcentagem.Text = "0";
            this.lblPorcentagem.UseWaitCursor = true;
            // 
            // lblContrato
            // 
            this.lblContrato.AutoSize = true;
            this.lblContrato.Location = new System.Drawing.Point(11, 53);
            this.lblContrato.Name = "lblContrato";
            this.lblContrato.Size = new System.Drawing.Size(47, 13);
            this.lblContrato.TabIndex = 4;
            this.lblContrato.Text = "Contrato";
            this.lblContrato.UseWaitCursor = true;
            // 
            // progressBarReaderPdf
            // 
            this.progressBarReaderPdf.Location = new System.Drawing.Point(12, 25);
            this.progressBarReaderPdf.Name = "progressBarReaderPdf";
            this.progressBarReaderPdf.Size = new System.Drawing.Size(420, 23);
            this.progressBarReaderPdf.Step = 1;
            this.progressBarReaderPdf.TabIndex = 3;
            this.progressBarReaderPdf.UseWaitCursor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblPendente);
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblContrato);
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 97);
            this.panel1.TabIndex = 6;
            this.panel1.UseWaitCursor = true;
            // 
            // lblPendente
            // 
            this.lblPendente.AutoSize = true;
            this.lblPendente.ForeColor = System.Drawing.Color.Red;
            this.lblPendente.Location = new System.Drawing.Point(11, 74);
            this.lblPendente.Name = "lblPendente";
            this.lblPendente.Size = new System.Drawing.Size(53, 13);
            this.lblPendente.TabIndex = 8;
            this.lblPendente.Text = "Pendente";
            this.lblPendente.UseWaitCursor = true;
            // 
            // lblQtd
            // 
            this.lblQtd.AutoSize = true;
            this.lblQtd.Location = new System.Drawing.Point(11, 8);
            this.lblQtd.Name = "lblQtd";
            this.lblQtd.Size = new System.Drawing.Size(31, 13);
            this.lblQtd.TabIndex = 7;
            this.lblQtd.Text = "Total";
            this.lblQtd.UseWaitCursor = true;
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(247, 8);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(40, 13);
            this.lblTempo.TabIndex = 7;
            this.lblTempo.Text = "Tempo";
            this.lblTempo.UseWaitCursor = true;
            // 
            // frmGerarLayoutAlta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 97);
            this.Controls.Add(this.lblPorcentagem);
            this.Controls.Add(this.progressBarReaderPdf);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmGerarLayoutAlta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGerarLayoutAlta";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.FrmGerarLayoutAlta_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblPorcentagem;
        private System.Windows.Forms.Label lblContrato;
        private System.Windows.Forms.ProgressBar progressBarReaderPdf;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Label lblPendente;
        private System.Windows.Forms.Label lblQtd;
    }
}