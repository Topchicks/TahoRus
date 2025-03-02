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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelButton = new System.Windows.Forms.Panel();
            this.buttonOpenEuropeanTypeForm = new System.Windows.Forms.Button();
            this.btnOpenSettings = new System.Windows.Forms.Button();
            this.buttonOpenRussianPanel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelButton.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.panelButton.Controls.Add(this.buttonOpenRussianPanel);
            this.panelButton.Controls.Add(this.btnOpenSettings);
            this.panelButton.Controls.Add(this.buttonOpenEuropeanTypeForm);
            resources.ApplyResources(this.panelButton, "panelButton");
            this.panelButton.Name = "panelButton";
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
            // btnOpenSettings
            // 
            resources.ApplyResources(this.btnOpenSettings, "btnOpenSettings");
            this.btnOpenSettings.FlatAppearance.BorderSize = 0;
            this.btnOpenSettings.ForeColor = System.Drawing.Color.White;
            this.btnOpenSettings.Name = "btnOpenSettings";
            this.btnOpenSettings.UseVisualStyleBackColor = true;
            this.btnOpenSettings.Click += new System.EventHandler(this.btnOpenSettings_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Controls.Add(this.panel2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.label1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // StartApp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "StartApp";
            this.panelButton.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button buttonOpenRussianPanel;
        private System.Windows.Forms.Button btnOpenSettings;
        private System.Windows.Forms.Button buttonOpenEuropeanTypeForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
    }
}