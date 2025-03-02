using System;
using System.Threading;
using System.Windows.Forms;

namespace TaxoNavicon
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Создаем мьютекс с уникальным именем
            bool createdNew;
            Mutex mutex = new Mutex(true, "TachoPrint", out createdNew);

            // Проверяем, был ли создан новый экземпляр
            if (!createdNew)
            {
                // Если это не новый экземпляр, показываем сообщение и выходим
                MessageBox.Show("Приложение уже запущено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartApp());
        }
    }
}
