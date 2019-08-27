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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListContrato));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxContratocpf = new System.Windows.Forms.TextBox();
            this.dataGridViewContract = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.imageListItem = new System.Windows.Forms.ImageList(this.components);
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.addUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameContract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Download = new System.Windows.Forms.DataGridViewImageColumn();
            this.Vizualizar = new System.Windows.Forms.DataGridViewImageColumn();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 67);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro por Contrato";
            // 
            // textBoxContratocpf
            // 
            this.textBoxContratocpf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContratocpf.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxContratocpf.Location = new System.Drawing.Point(6, 22);
            this.textBoxContratocpf.MaxLength = 20;
            this.textBoxContratocpf.Name = "textBoxContratocpf";
            this.textBoxContratocpf.Size = new System.Drawing.Size(385, 35);
            this.textBoxContratocpf.TabIndex = 2;
            this.textBoxContratocpf.TextChanged += new System.EventHandler(this.textBoxContratocpf_TextChanged);
            // 
            // dataGridViewContract
            // 
            this.dataGridViewContract.AllowUserToAddRows = false;
            this.dataGridViewContract.AllowUserToDeleteRows = false;
            this.dataGridViewContract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewContract.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewContract.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.NameContract,
            this.Download,
            this.Vizualizar});
            this.dataGridViewContract.Location = new System.Drawing.Point(12, 101);
            this.dataGridViewContract.MultiSelect = false;
            this.dataGridViewContract.Name = "dataGridViewContract";
            this.dataGridViewContract.ReadOnly = true;
            this.dataGridViewContract.RowHeadersVisible = false;
            this.dataGridViewContract.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewContract.Size = new System.Drawing.Size(443, 396);
            this.dataGridViewContract.TabIndex = 3;
            this.dataGridViewContract.TabStop = false;
            this.dataGridViewContract.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewContract_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUsuarioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(467, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // imageListItem
            // 
            this.imageListItem.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListItem.ImageStream")));
            this.imageListItem.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListItem.Images.SetKeyName(0, "ic_cloud_download_128_28299.ico");
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ConversorToByte.Properties.Resources.ic_cloud_download_128_28299;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.ToolTipText = "Baixar Arquivo compactado";
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.ToolTipText = "Vizualizar Arquivo";
            this.dataGridViewImageColumn2.Width = 30;
            // 
            // btnLimpar
            // 
            this.btnLimpar.Image = global::ConversorToByte.Properties.Resources.clear_filters_48_45590__1_;
            this.btnLimpar.Location = new System.Drawing.Point(397, 18);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(40, 39);
            this.btnLimpar.TabIndex = 3;
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // addUsuarioToolStripMenuItem
            // 
            this.addUsuarioToolStripMenuItem.Image = global::ConversorToByte.Properties.Resources.business_application_addmale_useradd_insert_add_user_client_2312;
            this.addUsuarioToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addUsuarioToolStripMenuItem.Name = "addUsuarioToolStripMenuItem";
            this.addUsuarioToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.addUsuarioToolStripMenuItem.Text = "Add Usuário";
            this.addUsuarioToolStripMenuItem.Click += new System.EventHandler(this.addUsuarioToolStripMenuItem_Click);
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
            // Download
            // 
            this.Download.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Download.Description = "Baixar Arquivo";
            this.Download.HeaderText = "";
            this.Download.Image = global::ConversorToByte.Properties.Resources.ic_cloud_download_128_28299;
            this.Download.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Download.Name = "Download";
            this.Download.ReadOnly = true;
            this.Download.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Download.ToolTipText = "Baixar Arquivo compactado";
            this.Download.Width = 30;
            // 
            // Vizualizar
            // 
            this.Vizualizar.HeaderText = "";
            this.Vizualizar.Image = ((System.Drawing.Image)(resources.GetObject("Vizualizar.Image")));
            this.Vizualizar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Vizualizar.Name = "Vizualizar";
            this.Vizualizar.ReadOnly = true;
            this.Vizualizar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Vizualizar.ToolTipText = "Vizualizar Arquivo";
            this.Vizualizar.Width = 30;
            // 
            // frmListContrato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 509);
            this.Controls.Add(this.dataGridViewContract);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListContrato";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
        private System.Windows.Forms.ToolStripMenuItem addUsuarioToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ImageList imageListItem;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameContract;
        private System.Windows.Forms.DataGridViewImageColumn Download;
        private System.Windows.Forms.DataGridViewImageColumn Vizualizar;
    }
}

