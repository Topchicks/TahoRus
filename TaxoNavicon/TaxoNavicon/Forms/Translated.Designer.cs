namespace TaxoNavicon.Forms
{
    partial class Translated
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Translated));
            this.panelTextTranslated = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // panelTextTranslated
            // 
            this.panelTextTranslated.AutoScroll = true;
            this.panelTextTranslated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTextTranslated.Location = new System.Drawing.Point(0, 0);
            this.panelTextTranslated.Name = "panelTextTranslated";
            this.panelTextTranslated.Size = new System.Drawing.Size(375, 367);
            this.panelTextTranslated.TabIndex = 0;
            // 
            // Translated
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 367);
            this.Controls.Add(this.panelTextTranslated);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(391, 406);
            this.Name = "Translated";
            this.Text = "Translated";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelTextTranslated;
    }
}