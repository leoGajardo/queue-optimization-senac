namespace SafePI3
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selecionarArquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arquivoSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arquivoFilaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.créditosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayButton = new System.Windows.Forms.Button();
            this.Faster1Button = new System.Windows.Forms.Button();
            this.Faster3Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opçõesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(986, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opçõesToolStripMenuItem
            // 
            this.opçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selecionarArquivosToolStripMenuItem,
            this.toolStripSeparator1,
            this.créditosToolStripMenuItem});
            this.opçõesToolStripMenuItem.Name = "opçõesToolStripMenuItem";
            this.opçõesToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.opçõesToolStripMenuItem.Text = "Opções";
            // 
            // selecionarArquivosToolStripMenuItem
            // 
            this.selecionarArquivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoSetupToolStripMenuItem,
            this.arquivoFilaToolStripMenuItem});
            this.selecionarArquivosToolStripMenuItem.Name = "selecionarArquivosToolStripMenuItem";
            this.selecionarArquivosToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.selecionarArquivosToolStripMenuItem.Text = "Selecionar Arquivos";
            // 
            // arquivoSetupToolStripMenuItem
            // 
            this.arquivoSetupToolStripMenuItem.Name = "arquivoSetupToolStripMenuItem";
            this.arquivoSetupToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.arquivoSetupToolStripMenuItem.Text = "Arquivo Setup";
            this.arquivoSetupToolStripMenuItem.Click += new System.EventHandler(this.arquivoSetupToolStripMenuItem_Click);
            // 
            // arquivoFilaToolStripMenuItem
            // 
            this.arquivoFilaToolStripMenuItem.Name = "arquivoFilaToolStripMenuItem";
            this.arquivoFilaToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.arquivoFilaToolStripMenuItem.Text = "Arquivo Fila";
            this.arquivoFilaToolStripMenuItem.Click += new System.EventHandler(this.arquivoFilaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // créditosToolStripMenuItem
            // 
            this.créditosToolStripMenuItem.Name = "créditosToolStripMenuItem";
            this.créditosToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.créditosToolStripMenuItem.Text = "Créditos";
            this.créditosToolStripMenuItem.Click += new System.EventHandler(this.creditosToolStripMenuItem_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlayButton.BackgroundImage")));
            this.PlayButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayButton.Location = new System.Drawing.Point(12, 433);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(86, 86);
            this.PlayButton.TabIndex = 1;
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // Faster1Button
            // 
            this.Faster1Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Faster1Button.BackgroundImage")));
            this.Faster1Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Faster1Button.Location = new System.Drawing.Point(104, 433);
            this.Faster1Button.Name = "Faster1Button";
            this.Faster1Button.Size = new System.Drawing.Size(86, 86);
            this.Faster1Button.TabIndex = 2;
            this.Faster1Button.UseVisualStyleBackColor = true;
            this.Faster1Button.Click += new System.EventHandler(this.Faster1Button_Click);
            // 
            // Faster3Button
            // 
            this.Faster3Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Faster3Button.BackgroundImage")));
            this.Faster3Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Faster3Button.Cursor = System.Windows.Forms.Cursors.Default;
            this.Faster3Button.Location = new System.Drawing.Point(196, 433);
            this.Faster3Button.Name = "Faster3Button";
            this.Faster3Button.Size = new System.Drawing.Size(86, 86);
            this.Faster3Button.TabIndex = 3;
            this.Faster3Button.UseVisualStyleBackColor = true;
            this.Faster3Button.Click += new System.EventHandler(this.Faster3Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 404);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Turnos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 417);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 523);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Faster3Button);
            this.Controls.Add(this.Faster1Button);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Gerenciador de Filas";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem créditosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selecionarArquivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arquivoSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arquivoFilaToolStripMenuItem;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button Faster1Button;
        private System.Windows.Forms.Button Faster3Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

