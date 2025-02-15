namespace TaxoNavicon
{
    partial class StartApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartApp));
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonOpenRussianPanel = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonOpenEuropeanTypeForm = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // webBrowser1
            // 
            resources.ApplyResources(this.webBrowser1, "webBrowser1");
            this.webBrowser1.Name = "webBrowser1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.buttonOpenRussianPanel);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.buttonOpenEuropeanTypeForm);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // buttonOpenRussianPanel
            // 
            resources.ApplyResources(this.buttonOpenRussianPanel, "buttonOpenRussianPanel");
            this.buttonOpenRussianPanel.FlatAppearance.BorderSize = 0;
            this.buttonOpenRussianPanel.ForeColor = System.Drawing.Color.White;
            this.buttonOpenRussianPanel.Name = "buttonOpenRussianPanel";
            this.buttonOpenRussianPanel.UseVisualStyleBackColor = true;
            this.buttonOpenRussianPanel.Click += new System.EventHandler(this.buttonOpenRussianPanel_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonOpenEuropeanTypeForm
            // 
            resources.ApplyResources(this.buttonOpenEuropeanTypeForm, "buttonOpenEuropeanTypeForm");
            this.buttonOpenEuropeanTypeForm.FlatAppearance.BorderSize = 0;
            this.buttonOpenEuropeanTypeForm.ForeColor = System.Drawing.Color.White;
            this.buttonOpenEuropeanTypeForm.Name = "buttonOpenEuropeanTypeForm";
            this.buttonOpenEuropeanTypeForm.UseVisualStyleBackColor = true;
            this.buttonOpenEuropeanTypeForm.Click += new System.EventHandler(this.buttonOpenEuropeanTypeForm_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(77)))), ((int)(((byte)(237)))));
            this.panel2.Controls.Add(this.label1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // StartApp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "StartApp";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOpenEuropeanTypeForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOpenRussianPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}