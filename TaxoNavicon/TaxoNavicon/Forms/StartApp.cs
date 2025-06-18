using System;
using System.Windows.Forms;
using TaxoNavicon.Forms;

namespace TaxoNavicon
{
    public partial class StartApp : Form
    {
        public string filePath;
        public StartApp()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            #region
            // Создаем контекстное меню
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            /*ToolStripMenuItem OpenRussPanel = new ToolStripMenuItem("Российский документ");
            OpenRussPanel.Click += (s, e) => buttonOpenRussianPanel_Click(s, e); // Закрытие приложения
            contextMenu.Items.Add(OpenRussPanel);*/

            ToolStripMenuItem OpenEuroPanel = new ToolStripMenuItem("Европейский документ");
            OpenEuroPanel.Click += (s, e) => buttonOpenEuropeanTypeForm_Click(s, e); // Закрытие приложения
            contextMenu.Items.Add(OpenEuroPanel);

            ToolStripMenuItem translatedItem = new ToolStripMenuItem("Переводы");
            translatedItem.Click += (s, e) => OpenTranslatedItemPanel(); // Закрытие приложения
            contextMenu.Items.Add(translatedItem);

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
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // Отменяем закрытие окна
            Hide();           // Скрываем окно
        }
        private void buttonOpenEuropeanTypeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            EuropeanTypeForm europeanTypeForm = new EuropeanTypeForm();

            europeanTypeForm.ShowDialog();
        }

        private void OpenTranslatedItemPanel()
        {
            Translated translated = new Translated();

            translated.Show();
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

        private void OpenTranslater_Click(object sender, EventArgs e)
        {
            OpenTranslatedItemPanel();
        }
    }
}
