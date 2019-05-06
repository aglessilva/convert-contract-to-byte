namespace ConversorToByte
{
    partial class frmListContrato
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.textBoxContratocpf = new System.Windows.Forms.TextBox();
            this.dataGridViewContract = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameContract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameCpf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Download = new System.Windows.Forms.DataGridViewButtonColumn();
            this.addUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baixarContratoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContract)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Contratos Liquidados";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpar);
            this.groupBox1.Controls.Add(this.textBoxContratocpf);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 51);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro por Contrato ou CPF";
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(345, 19);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(51, 21);
            this.btnLimpar.TabIndex = 3;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // textBoxContratocpf
            // 
            this.textBoxContratocpf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContratocpf.Location = new System.Drawing.Point(6, 19);
            this.textBoxContratocpf.MaxLength = 20;
            this.textBoxContratocpf.Name = "textBoxContratocpf";
            this.textBoxContratocpf.Size = new System.Drawing.Size(333, 20);
            this.textBoxContratocpf.TabIndex = 2;
            this.textBoxContratocpf.TextChanged += new System.EventHandler(this.textBoxContratocpf_TextChanged);
            // 
            // dataGridViewContract
            // 
            this.dataGridViewContract.AllowUserToAddRows = false;
            this.dataGridViewContract.AllowUserToDeleteRows = false;
            this.dataGridViewContract.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewContract.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.NameContract,
            this.NameCpf,
            this.Download});
            this.dataGridViewContract.Location = new System.Drawing.Point(12, 85);
            this.dataGridViewContract.MultiSelect = false;
            this.dataGridViewContract.Name = "dataGridViewContract";
            this.dataGridViewContract.ReadOnly = true;
            this.dataGridViewContract.RowHeadersVisible = false;
            this.dataGridViewContract.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewContract.Size = new System.Drawing.Size(403, 245);
            this.dataGridViewContract.TabIndex = 3;
            this.dataGridViewContract.TabStop = false;
            this.dataGridViewContract.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewContract_CellContentClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "Id";
            this.ID.HeaderText = "";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // NameContract
            // 
            this.NameContract.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameContract.DataPropertyName = "NameContract";
            this.NameContract.HeaderText = "Contrato";
            this.NameContract.Name = "NameContract";
            this.NameContract.ReadOnly = true;
            this.NameContract.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // NameCpf
            // 
            this.NameCpf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameCpf.DataPropertyName = "NameCpf";
            this.NameCpf.HeaderText = "Cliente";
            this.NameCpf.Name = "NameCpf";
            this.NameCpf.ReadOnly = true;
            this.NameCpf.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Download
            // 
            this.Download.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Download.HeaderText = "";
            this.Download.Name = "Download";
            this.Download.ReadOnly = true;
            this.Download.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Download.Text = "Download";
            this.Download.UseColumnTextForButtonValue = true;
            this.Download.Width = 65;
            // 
            // addUsuarioToolStripMenuItem
            // 
            this.addUsuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baixarContratoToolStripMenuItem});
            this.addUsuarioToolStripMenuItem.Enabled = false;
            this.addUsuarioToolStripMenuItem.Image = global::ConversorToByte.Properties.Resources.business_application_addmale_useradd_insert_add_user_client_2312;
            this.addUsuarioToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addUsuarioToolStripMenuItem.Name = "addUsuarioToolStripMenuItem";
            this.addUsuarioToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.addUsuarioToolStripMenuItem.Text = "Add Usuário";
            // 
            // baixarContratoToolStripMenuItem
            // 
            this.baixarContratoToolStripMenuItem.Image = global::ConversorToByte.Properties.Resources.ic_cloud_download_128_28299;
            this.baixarContratoToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.baixarContratoToolStripMenuItem.Name = "baixarContratoToolStripMenuItem";
            this.baixarContratoToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.baixarContratoToolStripMenuItem.Text = "Baixar Contrato";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUsuarioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(427, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // frmListContrato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 333);
            this.Controls.Add(this.dataGridViewContract);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListContrato";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Contratos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContract)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.TextBox textBoxContratocpf;
        private System.Windows.Forms.DataGridView dataGridViewContract;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameContract;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameCpf;
        private System.Windows.Forms.DataGridViewButtonColumn Download;
        private System.Windows.Forms.ToolStripMenuItem addUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baixarContratoToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

