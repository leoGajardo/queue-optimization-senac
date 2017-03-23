namespace SafePI3
{
    partial class Results
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClientMostID = new System.Windows.Forms.Label();
            this.ClientMostTurn = new System.Windows.Forms.Label();
            this.Turns = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AverageTimeInSystem = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ATime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Turnos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cliente que ficou mais tempo:";
            // 
            // ClientMostID
            // 
            this.ClientMostID.AutoSize = true;
            this.ClientMostID.Location = new System.Drawing.Point(13, 74);
            this.ClientMostID.Name = "ClientMostID";
            this.ClientMostID.Size = new System.Drawing.Size(35, 13);
            this.ClientMostID.TabIndex = 2;
            this.ClientMostID.Text = "label3";
            // 
            // ClientMostTurn
            // 
            this.ClientMostTurn.AutoSize = true;
            this.ClientMostTurn.Location = new System.Drawing.Point(160, 74);
            this.ClientMostTurn.Name = "ClientMostTurn";
            this.ClientMostTurn.Size = new System.Drawing.Size(35, 13);
            this.ClientMostTurn.TabIndex = 3;
            this.ClientMostTurn.Text = "label4";
            // 
            // Turns
            // 
            this.Turns.AutoSize = true;
            this.Turns.Location = new System.Drawing.Point(65, 13);
            this.Turns.Name = "Turns";
            this.Turns.Size = new System.Drawing.Size(35, 13);
            this.Turns.TabIndex = 4;
            this.Turns.Text = "label5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Turnos:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tempo médio de um usuário no sistema:";
            // 
            // AverageTimeInSystem
            // 
            this.AverageTimeInSystem.AutoSize = true;
            this.AverageTimeInSystem.Location = new System.Drawing.Point(485, 46);
            this.AverageTimeInSystem.Name = "AverageTimeInSystem";
            this.AverageTimeInSystem.Size = new System.Drawing.Size(35, 13);
            this.AverageTimeInSystem.TabIndex = 7;
            this.AverageTimeInSystem.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tempo médio de espera por tipo de fila: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "A:";
            // 
            // ATime
            // 
            this.ATime.AutoSize = true;
            this.ATime.Location = new System.Drawing.Point(471, 140);
            this.ATime.Name = "ATime";
            this.ATime.Size = new System.Drawing.Size(0, 13);
            this.ATime.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(229, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Tempo médio por combinação de atendimento:";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(432, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 216);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ATime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AverageTimeInSystem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Turns);
            this.Controls.Add(this.ClientMostTurn);
            this.Controls.Add(this.ClientMostID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Results";
            this.Text = "Results";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ClientMostID;
        private System.Windows.Forms.Label ClientMostTurn;
        private System.Windows.Forms.Label Turns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label AverageTimeInSystem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ATime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}