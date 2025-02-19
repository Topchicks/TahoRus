using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace TaxoNavicon
{
    public partial class StartApp : Form
    {
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

            russianTypeForm.ShowDialog();
        }
    }
}
