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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.FileSavePath = new System.Windows.Forms.Button();
            this.textBoxFileSavePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.adressRus = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.adressEngBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.adressRusBox = new System.Windows.Forms.TextBox();
            this.linkLabelOpenPanelInfo = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPrinterSticker = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPrinterWord = new System.Windows.Forms.ComboBox();
            this.checkBoxFormateSticker = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.adressRus.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileSavePath
            // 
            this.FileSavePath.Location = new System.Drawing.Point(15, 39);
            this.FileSavePath.Name = "FileSavePath";
            this.FileSavePath.Size = new System.Drawing.Size(95, 23);
            this.FileSavePath.TabIndex = 0;
            this.FileSavePath.Text = "Выбрать файл";
            this.FileSavePath.UseVisualStyleBackColor = true;
            this.FileSavePath.Click += new System.EventHandler(this.FileSavePath_Click);
            // 
            // textBoxFileSavePath
            // 
            this.textBoxFileSavePath.Location = new System.Drawing.Point(163, 14);
            this.textBoxFileSavePath.Multiline = true;
            this.textBoxFileSavePath.Name = "textBoxFileSavePath";
            this.textBoxFileSavePath.Size = new System.Drawing.Size(306, 48);
            this.textBoxFileSavePath.TabIndex = 1;
            this.textBoxFileSavePath.TextChanged += new System.EventHandler(this.textBoxFileSavePath_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Место сохранения";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 68);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial Black", 32F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(728, 68);
            this.label2.TabIndex = 1;
            this.label2.Text = "Настройки";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // adressRus
            // 
            this.adressRus.Controls.Add(this.label7);
            this.adressRus.Controls.Add(this.textBox1);
            this.adressRus.Controls.Add(this.label6);
            this.adressRus.Controls.Add(this.adressEngBox);
            this.adressRus.Controls.Add(this.label5);
            this.adressRus.Controls.Add(this.adressRusBox);
            this.adressRus.Controls.Add(this.linkLabelOpenPanelInfo);
            this.adressRus.Controls.Add(this.label4);
            this.adressRus.Controls.Add(this.comboBoxPrinterSticker);
            this.adressRus.Controls.Add(this.label3);
            this.adressRus.Controls.Add(this.comboBoxPrinterWord);
            this.adressRus.Controls.Add(this.checkBoxFormateSticker);
            this.adressRus.Controls.Add(this.label1);
            this.adressRus.Controls.Add(this.FileSavePath);
            this.adressRus.Controls.Add(this.textBoxFileSavePath);
            this.adressRus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adressRus.Location = new System.Drawing.Point(0, 68);
            this.adressRus.Name = "adressRus";
            this.adressRus.Size = new System.Drawing.Size(728, 353);
            this.adressRus.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(7, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(257, 23);
            this.label7.TabIndex = 14;
            this.label7.Text = "Адрес для наклейки:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(270, 281);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(306, 23);
            this.textBox1.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(7, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(257, 23);
            this.label6.TabIndex = 12;
            this.label6.Text = "Адрес мастерской на Английском:";
            // 
            // adressEngBox
            // 
            this.adressEngBox.Location = new System.Drawing.Point(270, 252);
            this.adressEngBox.Multiline = true;
            this.adressEngBox.Name = "adressEngBox";
            this.adressEngBox.Size = new System.Drawing.Size(306, 23);
            this.adressEngBox.TabIndex = 11;
            this.adressEngBox.TextChanged += new System.EventHandler(this.adressRusBox_TextChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(7, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Адрес мастерской на Русском:";
            // 
            // adressRusBox
            // 
            this.adressRusBox.Location = new System.Drawing.Point(270, 223);
            this.adressRusBox.Multiline = true;
            this.adressRusBox.Name = "adressRusBox";
            this.adressRusBox.Size = new System.Drawing.Size(306, 23);
            this.adressRusBox.TabIndex = 9;
            this.adressRusBox.TextChanged += new System.EventHandler(this.adressRusBox_TextChanged);
            // 
            // linkLabelOpenPanelInfo
            // 
            this.linkLabelOpenPanelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelOpenPanelInfo.AutoSize = true;
            this.linkLabelOpenPanelInfo.Font = new System.Drawing.Font("Arial", 10F);
            this.linkLabelOpenPanelInfo.Location = new System.Drawing.Point(645, 326);
            this.linkLabelOpenPanelInfo.Name = "linkLabelOpenPanelInfo";
            this.linkLabelOpenPanelInfo.Size = new System.Drawing.Size(62, 16);
            this.linkLabelOpenPanelInfo.TabIndex = 8;
            this.linkLabelOpenPanelInfo.TabStop = true;
            this.linkLabelOpenPanelInfo.Text = "Справка";
            this.linkLabelOpenPanelInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOpenPanelInfo_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(7, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Принтер для печати наклеек";
            // 
            // comboBoxPrinterSticker
            // 
            this.comboBoxPrinterSticker.FormattingEnabled = true;
            this.comboBoxPrinterSticker.Location = new System.Drawing.Point(270, 180);
            this.comboBoxPrinterSticker.Name = "comboBoxPrinterSticker";
            this.comboBoxPrinterSticker.Size = new System.Drawing.Size(306, 21);
            this.comboBoxPrinterSticker.TabIndex = 6;
            this.comboBoxPrinterSticker.SelectedIndexChanged += new System.EventHandler(this.comboBoxPrinterSticker_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(7, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Принтер для печати сертификата";
            // 
            // comboBoxPrinterWord
            // 
            this.comboBoxPrinterWord.FormattingEnabled = true;
            this.comboBoxPrinterWord.Location = new System.Drawing.Point(270, 141);
            this.comboBoxPrinterWord.Name = "comboBoxPrinterWord";
            this.comboBoxPrinterWord.Size = new System.Drawing.Size(306, 21);
            this.comboBoxPrinterWord.TabIndex = 4;
            this.comboBoxPrinterWord.SelectedIndexChanged += new System.EventHandler(this.comboBoxPrinterWord_SelectedIndexChanged);
            // 
            // checkBoxFormateSticker
            // 
            this.checkBoxFormateSticker.AutoSize = true;
            this.checkBoxFormateSticker.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxFormateSticker.Location = new System.Drawing.Point(10, 86);
            this.checkBoxFormateSticker.Name = "checkBoxFormateSticker";
            this.checkBoxFormateSticker.Size = new System.Drawing.Size(220, 22);
            this.checkBoxFormateSticker.TabIndex = 3;
            this.checkBoxFormateSticker.Text = "Форматирование наклейки";
            this.checkBoxFormateSticker.UseVisualStyleBackColor = true;
            this.checkBoxFormateSticker.CheckedChanged += new System.EventHandler(this.checkBoxFormateSticker_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 421);
            this.Controls.Add(this.adressRus);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(744, 460);
            this.Name = "Settings";
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.adressRus.ResumeLayout(false);
            this.adressRus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FileSavePath;
        private System.Windows.Forms.TextBox textBoxFileSavePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel adressRus;
        private System.Windows.Forms.CheckBox checkBoxFormateSticker;
        private System.Windows.Forms.ComboBox comboBoxPrinterWord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxPrinterSticker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabelOpenPanelInfo;
        private System.Windows.Forms.TextBox adressRusBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox adressEngBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
    }
}