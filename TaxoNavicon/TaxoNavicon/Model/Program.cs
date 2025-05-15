using System;
using System.Threading;
using System.Windows.Forms;

namespace TaxoNavicon
{
    internal static class Program
    {
        // Объявляем мьютекс как статическое поле
        private static Mutex mutex = null;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Уникальное имя мьютекса
            const string mutexName = "TachoPrint";

            // Пытаемся создать мьютекс
            mutex = new Mutex(true, mutexName, out bool createdNew);

            // Проверяем, был ли создан новый экземпляр мьютекса
            if (!createdNew)
            {
                // Если это не новый экземпляр, показываем сообщение и выходим
                MessageBox.Show("Приложение уже запущено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Подписываемся на событие закрытия приложения
            Application.ApplicationExit += Application_ApplicationExit;

            // Запускаем приложение
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartApp());
        }

        // Обработчик события закрытия приложения
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            // Освобождаем мьютекс
            if (mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}