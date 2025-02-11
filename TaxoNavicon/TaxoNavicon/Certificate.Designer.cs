namespace TaxoNavicon
{
    partial class Certificate
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelPrint = new System.Windows.Forms.ToolStripLabel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.GenerateCertificate = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelPrint,
            this.GenerateCertificate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1068, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelPrint
            // 
            this.toolStripLabelPrint.Name = "toolStripLabelPrint";
            this.toolStripLabelPrint.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabelPrint.Text = "Печать";
            this.toolStripLabelPrint.Click += new System.EventHandler(this.toolStripLabelPrint_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 25);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1068, 646);
            this.webBrowser.TabIndex = 1;
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.Location = new System.Drawing.Point(0, 25);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(602, 586);
            this.printPreviewControl.TabIndex = 2;
            // 
            // GenerateCertificate
            // 
            this.GenerateCertificate.Name = "GenerateCertificate";
            this.GenerateCertificate.Size = new System.Drawing.Size(91, 22);
            this.GenerateCertificate.Text = "Сформировать";
            // 
            // Certificate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 671);
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(1084, 710);
            this.MinimumSize = new System.Drawing.Size(1084, 710);
            this.Name = "Certificate";
            this.Text = "Certificate";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPrint;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.ToolStripLabel GenerateCertificate;
    }
}