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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxTela = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.btnSelectDiretorioDestino = new System.Windows.Forms.Button();
            this.btnSelectDiretorioOrigem = new System.Windows.Forms.Button();
            this.btnDuplicata = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textDestinoLayout = new System.Windows.Forms.TextBox();
            this.textOrigemContratosPdf = new System.Windows.Forms.TextBox();
            this.btnIniciarConvercao = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelDamp3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDamp3 = new System.Windows.Forms.Button();
            this.btnLocalizar = new System.Windows.Forms.Button();
            this.textBoxDamp3 = new System.Windows.Forms.TextBox();
            this.pnlHistoricoParcela = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.btnLocalizarHistoricoParcela = new System.Windows.Forms.Button();
            this.textBoxHistoricoParcelas = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cabeçalhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parcelasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSubHistoricoParcela = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemGravarHistoricoParcelas = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarHistóricoDeParcelasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerarArquivoDeDamp3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSpinner = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panelDamp3.SuspendLayout();
            this.pnlHistoricoParcela.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelSpinner.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Seleção de diretórios Origem/Destino";
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.SelectedPath = "C:\\Blocado";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBoxTela
            // 
            this.comboBoxTela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTela.Enabled = false;
            this.comboBoxTela.FormattingEnabled = true;
            this.comboBoxTela.Items.AddRange(new object[] {
            "TELA 16",
            "TELA 18",
            "TELA 20",
            "TELA 25",
            "Telas..."});
            this.comboBoxTela.Location = new System.Drawing.Point(12, 27);
            this.comboBoxTela.Name = "comboBoxTela";
            this.comboBoxTela.Size = new System.Drawing.Size(190, 21);
            this.comboBoxTela.TabIndex = 3;
            this.comboBoxTela.SelectedIndexChanged += new System.EventHandler(this.comboBoxTela_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(490, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(51, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "18 All";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(211, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "16 Ex";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(271, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "18 Ex";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(214, 34);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(51, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "20 All";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(325, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "20 Ex";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(379, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(48, 23);
            this.button7.TabIndex = 9;
            this.button7.Text = "25 Ex";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "16 All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(277, 34);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(51, 23);
            this.button8.TabIndex = 10;
            this.button8.Text = "25 All";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.btnSelectDiretorioDestino);
            this.groupBox1.Controls.Add(this.btnSelectDiretorioOrigem);
            this.groupBox1.Controls.Add(this.btnDuplicata);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textDestinoLayout);
            this.groupBox1.Controls.Add(this.textOrigemContratosPdf);
            this.groupBox1.Controls.Add(this.btnIniciarConvercao);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleção de Diretórios ";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(467, 119);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 11;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Visible = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(6, 129);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 12;
            this.button10.Text = "Consolidado";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Visible = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
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
            // btnDuplicata
            // 
            this.btnDuplicata.BackColor = System.Drawing.SystemColors.Control;
            this.btnDuplicata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDuplicata.Image = global::ConvetPdfToLayoutAlta.Properties.Resources._1490890032_24_82551;
            this.btnDuplicata.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDuplicata.Location = new System.Drawing.Point(127, 110);
            this.btnDuplicata.Name = "btnDuplicata";
            this.btnDuplicata.Size = new System.Drawing.Size(128, 40);
            this.btnDuplicata.TabIndex = 10;
            this.btnDuplicata.Text = "Filtrar Arquivos...";
            this.btnDuplicata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDuplicata.UseVisualStyleBackColor = true;
            this.btnDuplicata.Click += new System.EventHandler(this.BtnDuplicata_Click);
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
            this.btnIniciarConvercao.Enabled = false;
            this.btnIniciarConvercao.Image = global::ConvetPdfToLayoutAlta.Properties.Resources._1485477153_arrow_right_78596;
            this.btnIniciarConvercao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIniciarConvercao.Location = new System.Drawing.Point(318, 110);
            this.btnIniciarConvercao.Name = "btnIniciarConvercao";
            this.btnIniciarConvercao.Size = new System.Drawing.Size(128, 40);
            this.btnIniciarConvercao.TabIndex = 1;
            this.btnIniciarConvercao.Text = "Iniciar Conversão";
            this.btnIniciarConvercao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIniciarConvercao.UseVisualStyleBackColor = true;
            this.btnIniciarConvercao.Click += new System.EventHandler(this.BtnIniciarConvercao_Click);
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
            // panelDamp3
            // 
            this.panelDamp3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDamp3.Controls.Add(this.label3);
            this.panelDamp3.Controls.Add(this.btnDamp3);
            this.panelDamp3.Controls.Add(this.btnLocalizar);
            this.panelDamp3.Controls.Add(this.textBoxDamp3);
            this.panelDamp3.Location = new System.Drawing.Point(4, 52);
            this.panelDamp3.Name = "panelDamp3";
            this.panelDamp3.Size = new System.Drawing.Size(567, 114);
            this.panelDamp3.TabIndex = 15;
            this.panelDamp3.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Selecione o arquivo de Damp para  filtrar os contratos... ";
            // 
            // btnDamp3
            // 
            this.btnDamp3.Enabled = false;
            this.btnDamp3.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.txtdocument_text_tx_9804;
            this.btnDamp3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDamp3.Location = new System.Drawing.Point(228, 67);
            this.btnDamp3.Name = "btnDamp3";
            this.btnDamp3.Size = new System.Drawing.Size(121, 40);
            this.btnDamp3.TabIndex = 5;
            this.btnDamp3.Text = "Gerar Damp 3";
            this.btnDamp3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDamp3.UseVisualStyleBackColor = true;
            this.btnDamp3.Click += new System.EventHandler(this.btnDamp3_Click);
            // 
            // btnLocalizar
            // 
            this.btnLocalizar.Location = new System.Drawing.Point(509, 40);
            this.btnLocalizar.Name = "btnLocalizar";
            this.btnLocalizar.Size = new System.Drawing.Size(40, 22);
            this.btnLocalizar.TabIndex = 4;
            this.btnLocalizar.Text = "...";
            this.btnLocalizar.UseVisualStyleBackColor = true;
            this.btnLocalizar.Click += new System.EventHandler(this.btnLocalizar_Click);
            // 
            // textBoxDamp3
            // 
            this.textBoxDamp3.Location = new System.Drawing.Point(22, 41);
            this.textBoxDamp3.Name = "textBoxDamp3";
            this.textBoxDamp3.ReadOnly = true;
            this.textBoxDamp3.Size = new System.Drawing.Size(487, 20);
            this.textBoxDamp3.TabIndex = 3;
            // 
            // pnlHistoricoParcela
            // 
            this.pnlHistoricoParcela.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHistoricoParcela.Controls.Add(this.label4);
            this.pnlHistoricoParcela.Controls.Add(this.button11);
            this.pnlHistoricoParcela.Controls.Add(this.btnLocalizarHistoricoParcela);
            this.pnlHistoricoParcela.Controls.Add(this.textBoxHistoricoParcelas);
            this.pnlHistoricoParcela.Location = new System.Drawing.Point(4, 52);
            this.pnlHistoricoParcela.Name = "pnlHistoricoParcela";
            this.pnlHistoricoParcela.Size = new System.Drawing.Size(567, 114);
            this.pnlHistoricoParcela.TabIndex = 16;
            this.pnlHistoricoParcela.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(292, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Selecione o arquivo de ARQ.EXT.08.HIST.PARCELAS.TXT";
            // 
            // button11
            // 
            this.button11.Enabled = false;
            this.button11.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.txtdocument_text_tx_9804;
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.Location = new System.Drawing.Point(212, 67);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(168, 40);
            this.button11.TabIndex = 5;
            this.button11.Text = "Gravar dados de parcelas";
            this.button11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // btnLocalizarHistoricoParcela
            // 
            this.btnLocalizarHistoricoParcela.Location = new System.Drawing.Point(509, 40);
            this.btnLocalizarHistoricoParcela.Name = "btnLocalizarHistoricoParcela";
            this.btnLocalizarHistoricoParcela.Size = new System.Drawing.Size(40, 22);
            this.btnLocalizarHistoricoParcela.TabIndex = 4;
            this.btnLocalizarHistoricoParcela.Text = "...";
            this.btnLocalizarHistoricoParcela.UseVisualStyleBackColor = true;
            this.btnLocalizarHistoricoParcela.Click += new System.EventHandler(this.btnLocalizarHistoricoParcela_Click);
            // 
            // textBoxHistoricoParcelas
            // 
            this.textBoxHistoricoParcelas.Location = new System.Drawing.Point(22, 41);
            this.textBoxHistoricoParcelas.Name = "textBoxHistoricoParcelas";
            this.textBoxHistoricoParcelas.ReadOnly = true;
            this.textBoxHistoricoParcelas.Size = new System.Drawing.Size(487, 20);
            this.textBoxHistoricoParcelas.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(574, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cabeçalhoToolStripMenuItem,
            this.parcelasToolStripMenuItem,
            this.menuItemSubHistoricoParcela,
            this.gerarArquivoDeDamp3ToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // cabeçalhoToolStripMenuItem
            // 
            this.cabeçalhoToolStripMenuItem.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.xmag_search_find_export_locate_5984;
            this.cabeçalhoToolStripMenuItem.Name = "cabeçalhoToolStripMenuItem";
            this.cabeçalhoToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.cabeçalhoToolStripMenuItem.Text = "Cabeçalhos";
            // 
            // parcelasToolStripMenuItem
            // 
            this.parcelasToolStripMenuItem.Image = global::ConvetPdfToLayoutAlta.Properties.Resources.search_file256_25202;
            this.parcelasToolStripMenuItem.Name = "parcelasToolStripMenuItem";
            this.parcelasToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.parcelasToolStripMenuItem.Text = "Parcelas";
            this.parcelasToolStripMenuItem.Click += new System.EventHandler(this.parcelasToolStripMenuItem_Click);
            // 
            // menuItemSubHistoricoParcela
            // 
            this.menuItemSubHistoricoParcela.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemGravarHistoricoParcelas,
            this.consultarHistóricoDeParcelasToolStripMenuItem});
            this.menuItemSubHistoricoParcela.Name = "menuItemSubHistoricoParcela";
            this.menuItemSubHistoricoParcela.Size = new System.Drawing.Size(207, 22);
            this.menuItemSubHistoricoParcela.Text = "Histórico de Parcelas";
            // 
            // MenuItemGravarHistoricoParcelas
            // 
            this.MenuItemGravarHistoricoParcelas.Name = "MenuItemGravarHistoricoParcelas";
            this.MenuItemGravarHistoricoParcelas.Size = new System.Drawing.Size(238, 22);
            this.MenuItemGravarHistoricoParcelas.Text = "Gravar Histórico de Parcelas";
            this.MenuItemGravarHistoricoParcelas.Click += new System.EventHandler(this.MenuItemGravarHistoricoParcelas_Click);
            // 
            // consultarHistóricoDeParcelasToolStripMenuItem
            // 
            this.consultarHistóricoDeParcelasToolStripMenuItem.Name = "consultarHistóricoDeParcelasToolStripMenuItem";
            this.consultarHistóricoDeParcelasToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.consultarHistóricoDeParcelasToolStripMenuItem.Text = "Consultar Histórico de Parcelas";
            this.consultarHistóricoDeParcelasToolStripMenuItem.Click += new System.EventHandler(this.consultarHistóricoDeParcelasToolStripMenuItem_Click);
            // 
            // gerarArquivoDeDamp3ToolStripMenuItem
            // 
            this.gerarArquivoDeDamp3ToolStripMenuItem.Name = "gerarArquivoDeDamp3ToolStripMenuItem";
            this.gerarArquivoDeDamp3ToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.gerarArquivoDeDamp3ToolStripMenuItem.Text = "Gerar Arquivo de Damp 3";
            this.gerarArquivoDeDamp3ToolStripMenuItem.Click += new System.EventHandler(this.gerarArquivoDeDamp3ToolStripMenuItem_Click);
            // 
            // panelSpinner
            // 
            this.panelSpinner.BackColor = System.Drawing.SystemColors.Control;
            this.panelSpinner.Controls.Add(this.pnlHistoricoParcela);
            this.panelSpinner.Controls.Add(this.panelDamp3);
            this.panelSpinner.Controls.Add(this.groupBox1);
            this.panelSpinner.Controls.Add(this.button8);
            this.panelSpinner.Controls.Add(this.button1);
            this.panelSpinner.Controls.Add(this.button7);
            this.panelSpinner.Controls.Add(this.button4);
            this.panelSpinner.Controls.Add(this.button5);
            this.panelSpinner.Controls.Add(this.button3);
            this.panelSpinner.Controls.Add(this.button2);
            this.panelSpinner.Controls.Add(this.button6);
            this.panelSpinner.Controls.Add(this.comboBoxTela);
            this.panelSpinner.Controls.Add(this.menuStrip1);
            this.panelSpinner.Location = new System.Drawing.Point(0, 0);
            this.panelSpinner.Name = "panelSpinner";
            this.panelSpinner.Size = new System.Drawing.Size(574, 224);
            this.panelSpinner.TabIndex = 5;
            // 
            // FrmSelectFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 224);
            this.Controls.Add(this.panelSpinner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Converter contratos para formato sequencial  -";
            this.Load += new System.EventHandler(this.FrmSelectFolder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelDamp3.ResumeLayout(false);
            this.panelDamp3.PerformLayout();
            this.pnlHistoricoParcela.ResumeLayout(false);
            this.pnlHistoricoParcela.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelSpinner.ResumeLayout(false);
            this.panelSpinner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox comboBoxTela;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button btnSelectDiretorioDestino;
        private System.Windows.Forms.Button btnSelectDiretorioOrigem;
        private System.Windows.Forms.Button btnDuplicata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textDestinoLayout;
        private System.Windows.Forms.TextBox textOrigemContratosPdf;
        private System.Windows.Forms.Button btnIniciarConvercao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelDamp3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDamp3;
        private System.Windows.Forms.Button btnLocalizar;
        private System.Windows.Forms.TextBox textBoxDamp3;
        private System.Windows.Forms.Panel pnlHistoricoParcela;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button btnLocalizarHistoricoParcela;
        private System.Windows.Forms.TextBox textBoxHistoricoParcelas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cabeçalhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parcelasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemSubHistoricoParcela;
        private System.Windows.Forms.ToolStripMenuItem gerarArquivoDeDamp3ToolStripMenuItem;
        private System.Windows.Forms.Panel panelSpinner;
        private System.Windows.Forms.ToolStripMenuItem MenuItemGravarHistoricoParcelas;
        private System.Windows.Forms.ToolStripMenuItem consultarHistóricoDeParcelasToolStripMenuItem;
    }
}

