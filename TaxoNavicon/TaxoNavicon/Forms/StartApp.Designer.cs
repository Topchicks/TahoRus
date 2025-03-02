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
            this.buttonOpenRussianPanel = new System.Windows.Forms.Button();
            this.btnOpenSettings = new System.Windows.Forms.Button();
            this.buttonOpenEuropeanTypeForm = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButton.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Навикон";
            this.notifyIcon1.Visible = true;
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.panelButton.Controls.Add(this.buttonOpenRussianPanel);
            this.panelButton.Controls.Add(this.btnOpenSettings);
            this.panelButton.Controls.Add(this.buttonOpenEuropeanTypeForm);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButton.Location = new System.Drawing.Point(0, 70);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(200, 497);
            this.panelButton.TabIndex = 4;
            // 
            // buttonOpenRussianPanel
            // 
            this.buttonOpenRussianPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonOpenRussianPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonOpenRussianPanel.FlatAppearance.BorderSize = 0;
            this.buttonOpenRussianPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenRussianPanel.Font = new System.Drawing.Font("Arial", 12F);
            this.buttonOpenRussianPanel.ForeColor = System.Drawing.Color.White;
            this.buttonOpenRussianPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOpenRussianPanel.Location = new System.Drawing.Point(0, 44);
            this.buttonOpenRussianPanel.Name = "buttonOpenRussianPanel";
            this.buttonOpenRussianPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonOpenRussianPanel.Size = new System.Drawing.Size(200, 44);
            this.buttonOpenRussianPanel.TabIndex = 1;
            this.buttonOpenRussianPanel.Text = "Российский документ";
            this.buttonOpenRussianPanel.UseVisualStyleBackColor = true;
            this.buttonOpenRussianPanel.Click += new System.EventHandler(this.buttonOpenRussianPanel_Click);
            // 
            // btnOpenSettings
            // 
            this.btnOpenSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOpenSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnOpenSettings.FlatAppearance.BorderSize = 0;
            this.btnOpenSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSettings.Font = new System.Drawing.Font("Arial", 12F);
            this.btnOpenSettings.ForeColor = System.Drawing.Color.White;
            this.btnOpenSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenSettings.Location = new System.Drawing.Point(0, 453);
            this.btnOpenSettings.Name = "btnOpenSettings";
            this.btnOpenSettings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOpenSettings.Size = new System.Drawing.Size(200, 44);
            this.btnOpenSettings.TabIndex = 2;
            this.btnOpenSettings.Text = "Настройки";
            this.btnOpenSettings.UseVisualStyleBackColor = true;
            this.btnOpenSettings.Click += new System.EventHandler(this.btnOpenSettings_Click);
            // 
            // buttonOpenEuropeanTypeForm
            // 
            this.buttonOpenEuropeanTypeForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonOpenEuropeanTypeForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonOpenEuropeanTypeForm.FlatAppearance.BorderSize = 0;
            this.buttonOpenEuropeanTypeForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenEuropeanTypeForm.Font = new System.Drawing.Font("Arial", 12F);
            this.buttonOpenEuropeanTypeForm.ForeColor = System.Drawing.Color.White;
            this.buttonOpenEuropeanTypeForm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOpenEuropeanTypeForm.Location = new System.Drawing.Point(0, 0);
            this.buttonOpenEuropeanTypeForm.Name = "buttonOpenEuropeanTypeForm";
            this.buttonOpenEuropeanTypeForm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonOpenEuropeanTypeForm.Size = new System.Drawing.Size(200, 44);
            this.buttonOpenEuropeanTypeForm.TabIndex = 0;
            this.buttonOpenEuropeanTypeForm.Text = "Европейский документ";
            this.buttonOpenEuropeanTypeForm.UseVisualStyleBackColor = true;
            this.buttonOpenEuropeanTypeForm.Click += new System.EventHandler(this.buttonOpenEuropeanTypeForm_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 567);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1032, 70);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Arial Black", 32F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1032, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "TachoPrint";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 567);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1048, 606);
            this.Name = "StartApp";
            this.Text = "Главное меню";
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