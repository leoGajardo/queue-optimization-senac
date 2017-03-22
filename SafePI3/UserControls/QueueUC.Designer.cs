namespace SafePI3.UserControls
{
    partial class QueueUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.QueueName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // QueueName
            // 
            this.QueueName.AutoSize = true;
            this.QueueName.Location = new System.Drawing.Point(54, 17);
            this.QueueName.Name = "QueueName";
            this.QueueName.Size = new System.Drawing.Size(35, 13);
            this.QueueName.TabIndex = 0;
            this.QueueName.Text = "label1";
            this.QueueName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QueueUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.QueueName);
            this.Name = "QueueUC";
            this.Size = new System.Drawing.Size(137, 315);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label QueueName;
    }
}
