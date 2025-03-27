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
            DateTime apps = new DateTime(2025, 04, 05);
            DateTime currentDate = DateTime.Now;

            // Вычисляем разницу между датами
            TimeSpan difference = apps - currentDate;

            // Получаем количество оставшихся дней
            int daysRemaining = difference.Days;
            if (apps > currentDate)
            {
                MessageBox.Show($"Это пробная версия приложения \n до конца пробной весии осталось {daysRemaining}");
            }
            else
            {
                MessageBox.Show($"Срок пробной версии истёк приложение будет закрыто");
                System.Threading.Thread.Sleep(3000); // Задержка на 5 секунд
                Environment.Exit(0); // Закрыть программу
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartApp());
        }
    }
}
