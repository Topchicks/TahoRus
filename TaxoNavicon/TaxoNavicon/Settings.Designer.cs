namespace TaxoNavicon
{
    partial class Settings
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
            this.FileSavePath = new System.Windows.Forms.Button();
            this.textBoxFileSavePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileSavePath
            // 
            this.FileSavePath.Location = new System.Drawing.Point(12, 490);
            this.FileSavePath.Name = "FileSavePath";
            this.FileSavePath.Size = new System.Drawing.Size(95, 23);
            this.FileSavePath.TabIndex = 0;
            this.FileSavePath.Text = "Выбрать файл";
            this.FileSavePath.UseVisualStyleBackColor = true;
            this.FileSavePath.Click += new System.EventHandler(this.FileSavePath_Click);
            // 
            // textBoxFileSavePath
            // 
            this.textBoxFileSavePath.Location = new System.Drawing.Point(113, 492);
            this.textBoxFileSavePath.Name = "textBoxFileSavePath";
            this.textBoxFileSavePath.Size = new System.Drawing.Size(484, 20);
            this.textBoxFileSavePath.TabIndex = 1;
            this.textBoxFileSavePath.TextChanged += new System.EventHandler(this.textBoxFileSavePath_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 464);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Место сохранения";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 525);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFileSavePath);
            this.Controls.Add(this.FileSavePath);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileSavePath;
        private System.Windows.Forms.TextBox textBoxFileSavePath;
        private System.Windows.Forms.Label label1;
    }
}