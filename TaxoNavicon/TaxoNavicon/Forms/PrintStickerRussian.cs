using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using TaxoNaviconRussian;

namespace TaxoNavicon.Forms
{
    public partial class PrintStickerRussian : Form
    {
        private PrintDocument printDocument;
        private PoleDataRussian poleDataRussian;
        public PrintStickerRussian(PoleDataRussian poleDataRussian)
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            // Устанавливаем размер страницы
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 612, 94); // 24mm x 130mm в 1/100 дюймах
            printPreviewControl.Document = printDocument;

            // Задаем масштабирование
            printPreviewControl.Zoom = 1.0;

            // Для отображения в предварительном просмотре
            printPreviewControl.Document = printDocument;
            this.poleDataRussian = poleDataRussian;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            Font font = new Font("Microsoft Sans Serif", 9);
            Font boldFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            // Установим начальные координаты
            int xOffset = 10; // Начальное смещение по X
            int yOffset = 10; // Начальное смещение по Y
            int lineSpacing = 20; // расстояние между строками

            // Отображение данных
            g.DrawString("Date: " + poleDataRussian.dataJob, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("Tyres: " + poleDataRussian.tireMarkingsVehicle, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("Ø1: " + poleDataRussian.l, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("s/n Tacho: " + poleDataRussian.serialNumberTahograph, boldFont, Brushes.Black, xOffset, yOffset);

            xOffset = 150;
            yOffset = 10;

            g.DrawString("VIN: " + poleDataRussian.vinVehicle, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("TNo: " + poleDataRussian.registrationNumberVehicle, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("w: " + poleDataRussian.w, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("k: " + poleDataRussian.k, boldFont, Brushes.Black, xOffset, yOffset);

            xOffset = 250;
            yOffset = 10;

            // Возможно, добавьте отступы для визуального разделения
            xOffset += lineSpacing * 2; // Дополнительный отступ перед следующей секцией

            // Ниже можно добавить информацию о компании
            g.DrawString("NaviCon OOO", boldFont, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing;
            g.DrawString("Bulvar stroiteley st., 3G, \n     Tambov", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing + 4;
            g.DrawString("+7(4752)55-94-00", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing;
            g.DrawString("navicontmb.ru", font, Brushes.Black, xOffset, yOffset);

            // Вертикальный текст "RUS 526"
            string verticalText = "RUS 526";
            // Устанавливаем позицию текста
            int verticalTextX = 470; // Позиция по X для вертикального текста
            int verticalTextY = 15;   // Позиция по Y для вертикального текста

            // Поворачиваем графику для вертикального текста
            g.TranslateTransform(verticalTextX, verticalTextY);
            g.RotateTransform(90); // Поворачиваем на 90 градусов влево
            g.DrawString(verticalText, boldFont, Brushes.Black, 0, 0); // Рисуем вертикальный текст
            g.ResetTransform(); // Сбрасываем трансформацию
        }
        private void ToolStripMenuItemPrintSticker_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
    }
}
