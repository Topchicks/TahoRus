using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TaxoNavicon
{
    public partial class StartApp : Form
    {
        public string filePath;
        public StartApp()
        {
            InitializeComponent();
            #region
            // Создаем контекстное меню
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Создаем элементы меню
            ToolStripMenuItem openItem = new ToolStripMenuItem("Открыть");
            openItem.Click += (s, e) => this.Show(); // Действие при нажатии
            contextMenu.Items.Add(openItem);

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Выход");
            exitItem.Click += (s, e) => Application.Exit(); // Закрытие приложения
            contextMenu.Items.Add(exitItem);

            // Привязываем контекстное меню к NotifyIcon
            notifyIcon1.ContextMenuStrip = contextMenu;

            // Обработчик события двойного щелчка
            notifyIcon1.DoubleClick += (s, e) => this.Show();
            #endregion

        }
        private void buttonOpenEuropeanTypeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            EuropeanTypeForm europeanTypeForm = new EuropeanTypeForm();

            europeanTypeForm.ShowDialog();

        }

        private void buttonOpenRussianPanel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RussianTypeForm russianTypeForm = new RussianTypeForm();

            russianTypeForm.Show();
        }

        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings settings = new Settings();
            // Подписываемся на событие закрытия формы
            settings.FormClosed += (s, args) => this.Show();

            settings.Show();
        }
    }
}
