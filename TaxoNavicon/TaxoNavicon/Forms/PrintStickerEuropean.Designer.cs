namespace TaxoNavicon.Forms
{
    partial class PrintStickerEuropean
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
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelPrint = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelAddLable = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Location = new System.Drawing.Point(0, 0);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(892, 423);
            this.printPreviewControl.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelPrint,
            this.toolStripLabelAddLable});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(892, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelPrint
            // 
            this.toolStripLabelPrint.Name = "toolStripLabelPrint";
            this.toolStripLabelPrint.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabelPrint.Text = "Печать";
            this.toolStripLabelPrint.Click += new System.EventHandler(this.toolStripLabelPrint_Click);
            // 
            // PrintStickerEuropean
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 423);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.printPreviewControl);
            this.Name = "PrintStickerEuropean";
            this.Text = "PrintStickerEuropean";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPrint;
        private System.Windows.Forms.ToolStripLabel toolStripLabelAddLable;
    }
}