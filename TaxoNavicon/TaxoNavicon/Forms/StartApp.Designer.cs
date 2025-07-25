﻿namespace TaxoNavicon
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
            System.Windows.Forms.PictureBox pictureBox3;
            System.Windows.Forms.PictureBox pictureBox1;
            System.Windows.Forms.PictureBox pictureBox2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartApp));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButton = new System.Windows.Forms.Panel();
            this.OpenTranslater = new System.Windows.Forms.Button();
            this.buttonOpenRussianPanel = new System.Windows.Forms.Button();
            this.btnOpenSettings = new System.Windows.Forms.Button();
            this.buttonOpenEuropeanTypeForm = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            pictureBox3.BackgroundImage = global::TaxoNavicon.Properties.Resources.Copter;
            pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            pictureBox3.Location = new System.Drawing.Point(629, 47);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(331, 239);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            pictureBox1.BackgroundImage = global::TaxoNavicon.Properties.Resources.Truck;
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.Location = new System.Drawing.Point(208, 78);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(415, 409);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            pictureBox2.BackgroundImage = global::TaxoNavicon.Properties.Resources.Gazel;
            pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            pictureBox2.Location = new System.Drawing.Point(607, 280);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(331, 239);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Навикон";
            this.notifyIcon1.Visible = true;
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
            // panelButton
            // 
            this.panelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.panelButton.Controls.Add(this.OpenTranslater);
            this.panelButton.Controls.Add(this.buttonOpenRussianPanel);
            this.panelButton.Controls.Add(this.btnOpenSettings);
            this.panelButton.Controls.Add(this.buttonOpenEuropeanTypeForm);
            this.panelButton.Location = new System.Drawing.Point(0, 70);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(200, 497);
            this.panelButton.TabIndex = 4;
            // 
            // OpenTranslater
            // 
            this.OpenTranslater.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OpenTranslater.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpenTranslater.FlatAppearance.BorderSize = 0;
            this.OpenTranslater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenTranslater.Font = new System.Drawing.Font("Arial", 12F);
            this.OpenTranslater.ForeColor = System.Drawing.Color.White;
            this.OpenTranslater.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OpenTranslater.Location = new System.Drawing.Point(0, 88);
            this.OpenTranslater.Name = "OpenTranslater";
            this.OpenTranslater.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OpenTranslater.Size = new System.Drawing.Size(200, 44);
            this.OpenTranslater.TabIndex = 3;
            this.OpenTranslater.Text = "Переводы";
            this.OpenTranslater.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OpenTranslater.UseVisualStyleBackColor = true;
            this.OpenTranslater.Click += new System.EventHandler(this.OpenTranslater_Click);
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
            this.buttonOpenRussianPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOpenRussianPanel.UseVisualStyleBackColor = true;
            this.buttonOpenRussianPanel.Visible = false;
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
            this.btnOpenSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.buttonOpenEuropeanTypeForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOpenEuropeanTypeForm.UseVisualStyleBackColor = true;
            this.buttonOpenEuropeanTypeForm.Click += new System.EventHandler(this.buttonOpenEuropeanTypeForm_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(pictureBox2);
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(pictureBox1);
            this.panel1.Controls.Add(pictureBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 567);
            this.panel1.TabIndex = 1;
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            ((System.ComponentModel.ISupportInitialize)(pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button buttonOpenRussianPanel;
        private System.Windows.Forms.Button btnOpenSettings;
        private System.Windows.Forms.Button buttonOpenEuropeanTypeForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OpenTranslater;
    }
}