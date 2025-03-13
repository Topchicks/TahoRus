using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;
using System.Text.Json;

namespace TaxoNavicon.Forms
{
    
    public partial class PrintStickerEuropean : Form
    {
        private string filePath; // Путь к фалу с данными
        private PrintDocument printDocument;
        private PoleDataEuropean poleDataEuropean;
        private bool formatingSticker;
        private string pathSettingsFile; // Путь к файлу с настройками
        private string defualtPrinterSticker; // 
        public PrintStickerEuropean(PoleDataEuropean poleDataEuropean)
        {
            InitializeComponent();
            pathSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSetting.json");
            LoadSaveJson();
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            // Устанавливаем размер страницы
            if(formatingSticker == true)
            {
                printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 94, 512); // 24mm x 130mm в 1/100 дюймах
            }
            else
            {
                printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 512, 94); // 24mm x 130mm в 1/100 дюймах
            }
            printPreviewControl.Document = printDocument;

            // Задаем масштабирование
            printPreviewControl.Zoom = 1.0;

            // Для отображения в предварительном просмотре
            printPreviewControl.Document = printDocument;
            this.poleDataEuropean = poleDataEuropean;

            
        }
        private void LoadSaveJson()
        {
            try
            {
                var saveJson = File.ReadAllText(pathSettingsFile);
                SettingsJS settingsJS = JsonSerializer.Deserialize<SettingsJS>(saveJson);
                formatingSticker = settingsJS.FormatingSticker;
                defualtPrinterSticker = settingsJS.DefualtPrinterSticker;
                Console.WriteLine("Данные успешно подгруженны");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            Font font = new Font("Microsoft Sans Serif", 8);

            // Установим начальные координаты
            int xOffset = 10; // Начальное смещение по X
            int yOffset = 5; // Начальное смещение по Y
            int lineSpacing = 15; // расстояние между строками

            // Отображение данных
            g.DrawString("Date: " + poleDataEuropean.dataJob, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("Tyres: " + poleDataEuropean.tireMarkingsVehicle, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("Ø1: " + poleDataEuropean.l, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("s/n Tacho: " + poleDataEuropean.serialNumberTahograph, font, Brushes.Black, xOffset, yOffset);

            xOffset = 160;
            yOffset = 5;

            g.DrawString("VIN: " + poleDataEuropean.vinVehicle, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("TNo: " + poleDataEuropean.registrationNumberVehicle, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("w: " + poleDataEuropean.w, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("k: " + poleDataEuropean.k, font, Brushes.Black, xOffset, yOffset);

            xOffset = 305;
            yOffset = 2;
            // Возможно, добавьте отступы для визуального разделения
            xOffset += lineSpacing * 2; // Дополнительный отступ перед следующей секцией

            // Ниже можно добавить информацию о компании
            g.DrawString("  NaviCon OOO", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing;
            g.DrawString("BulvarStroiteley, 3G, \n       Tambov", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing + 12;
            g.DrawString("+7(4752)55-94-00", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing -1;
            g.DrawString("   navicontmb.ru", font, Brushes.Black, xOffset, yOffset);

            // Вертикальный текст "RUS 526"
            string verticalText = "RUS 526";
            // Устанавливаем позицию текста
            int verticalTextX = 489; // Позиция по X для вертикального текста
            int verticalTextY = 10;   // Позиция по Y для вертикального текста

            // Поворачиваем графику для вертикального текста
            g.TranslateTransform(verticalTextX, verticalTextY);
            g.RotateTransform(90); // Поворачиваем на 90 градусов влево
            g.DrawString(verticalText, font, Brushes.Black, 0, 0); // Рисуем вертикальный текст
            g.ResetTransform(); // Сбрасываем трансформацию

            // Рисуем обводку
            Pen pen = new Pen(Color.Black, 2); // Черная обводка шириной 2 пикселя
            Rectangle rect = new Rectangle(472, 9, 20, 50); // Прямоугольник для обводки
            g.DrawRectangle(pen, rect);
        }

           

        private void toolStripLabelPrint_Click(object sender, EventArgs e)
        {
            // Открытие диалогового окна для выбора принтера
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrinterSettings = new PrinterSettings();

            // Устанавливаем выбранный принтер
            //printDialog.PrinterSettings.PrinterName = defualtPrinterSticker;

            // Также есть автоматический вывод на печать

            printDocument.DefaultPageSettings.PrinterSettings.PrinterName = defualtPrinterSticker;
            printDocument.Print();
            /*if (printDialog.ShowDialog() == DialogResult.OK)
            {
                
            }*/
        }
    }
}
