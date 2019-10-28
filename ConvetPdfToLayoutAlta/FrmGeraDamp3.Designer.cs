namespace ConvetPdfToLayoutAlta
{
    partial class FrmGeraDamp3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeraDamp3));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.progressBarReaderPdf = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLidos = new System.Windows.Forms.Label();
            this.lblPendente = new System.Windows.Forms.Label();
            this.lblQtd = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.backgroundWorkerDamp3 = new System.ComponentModel.BackgroundWorker();
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
            this.panel1.Controls.Add(this.lblLidos);
            this.panel1.Controls.Add(this.lblPendente);
            this.panel1.Controls.Add(this.lblQtd);
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 97);
            this.panel1.TabIndex = 7;
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
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Location = new System.Drawing.Point(11, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Analizado:";
            this.label1.UseWaitCursor = true;
            // 
            // lblLidos
            // 
            this.lblLidos.AutoSize = true;
            this.lblLidos.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblLidos.Location = new System.Drawing.Point(67, 51);
            this.lblLidos.Name = "lblLidos";
            this.lblLidos.Size = new System.Drawing.Size(13, 13);
            this.lblLidos.TabIndex = 10;
            this.lblLidos.Text = "0";
            this.lblLidos.UseWaitCursor = true;
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
            // backgroundWorkerDamp3
            // 
            this.backgroundWorkerDamp3.WorkerReportsProgress = true;
            this.backgroundWorkerDamp3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDamp3_DoWork);
            this.backgroundWorkerDamp3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerDamp3_ProgressChanged);
            this.backgroundWorkerDamp3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDamp3_RunWorkerCompleted);
            // 
            // FrmGeraDamp3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 97);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmGeraDamp3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerar Damp 3";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.FrmGeraDamp3_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLidos;
        private System.Windows.Forms.Label lblPendente;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.ProgressBar progressBarReaderPdf;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDamp3;
        private System.Windows.Forms.Label lblPorcentagem;
    }
}