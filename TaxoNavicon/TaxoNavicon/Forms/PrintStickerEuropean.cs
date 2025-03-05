using Microsoft.Office.Interop.Word;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Font = System.Drawing.Font;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace TaxoNavicon.Forms
{
    
    public partial class PrintStickerEuropean : Form
    {
        private string filePath;
        private PrintDocument printDocument;
        private PoleDataEuropean poleDataEuropean;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        public PrintStickerEuropean(PoleDataEuropean poleDataEuropean)
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            // Устанавливаем размер страницы
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 94, 512); // 24mm x 130mm в 1/100 дюймах
            printPreviewControl.Document = printDocument;

            // Задаем масштабирование
            printPreviewControl.Zoom = 1.0;

            // Для отображения в предварительном просмотре
            printPreviewControl.Document = printDocument;
            this.poleDataEuropean = poleDataEuropean;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            Font font = new Font("Microsoft Sans Serif", 9);
            Font boldFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            // Установим начальные координаты
            int xOffset = 10; // Начальное смещение по X
            int yOffset = 5; // Начальное смещение по Y
            int lineSpacing = 15; // расстояние между строками

            // Отображение данных
            g.DrawString("Date: " + poleDataEuropean.dataJob, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("Tyres: " + poleDataEuropean.tireMarkingsVehicle, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("Ø1: " + poleDataEuropean.l, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("s/n Tacho: " + poleDataEuropean.serialNumberTahograph, boldFont, Brushes.Black, xOffset, yOffset);

            xOffset = 160;
            yOffset = 5;

            g.DrawString("VIN: " + poleDataEuropean.vinVehicle, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("TNo: " + poleDataEuropean.registrationNumberVehicle, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("w: " + poleDataEuropean.w, boldFont, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("k: " + poleDataEuropean.k, boldFont, Brushes.Black, xOffset, yOffset);

            xOffset = 305;
            yOffset = 5;

            // Возможно, добавьте отступы для визуального разделения
            xOffset += lineSpacing * 2; // Дополнительный отступ перед следующей секцией

            // Ниже можно добавить информацию о компании
            g.DrawString("  NaviCon OOO", boldFont, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing;
            g.DrawString("Bulvar stroiteley st., 3G, \n       Tambov", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing + 7;
            g.DrawString("+7(4752)55-94-00", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing + 1;
            g.DrawString("navicontmb.ru", font, Brushes.Black, xOffset, yOffset);

            // Вертикальный текст "RUS 526"
            string verticalText = "RUS 526";
            // Устанавливаем позицию текста
            int verticalTextX = 495; // Позиция по X для вертикального текста
            int verticalTextY = 12;   // Позиция по Y для вертикального текста

            // Поворачиваем графику для вертикального текста
            g.TranslateTransform(verticalTextX, verticalTextY);
            g.RotateTransform(90); // Поворачиваем на 90 градусов влево
            g.DrawString(verticalText, boldFont, Brushes.Black, 0, 0); // Рисуем вертикальный текст
            g.ResetTransform(); // Сбрасываем трансформацию
        }

        private void toolStripLabelPrint_Click(object sender, EventArgs e)
        {
            /* string relativePath = @"EuropianSticker.lbx"; // Относительный путь к файлу
             filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

             // Открытие документа Word
             Word.Application wordApp = new Word.Application();
             Word.Document doc = null;

             try
             {
                 // Открываем файл в Word
                 doc = wordApp.Documents.Open(filePath);

                 // Заменяем текст (пример замены)
                 FindAndReplace(doc, "<Data>", poleDataEuropean.dataJob);

                 // Сохраняем и закрываем документ
                 doc.Save();
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Ошибка: " + ex.Message);
             }
             finally
             {
                 // Закрываем документ и приложение Word
                 if (doc != null)
                 {
                     doc.Close();
                 }
                 wordApp.Quit();
             }


             // Открытие диалогового окна для выбора принтера
             PrintDialog printDialog = new PrintDialog();
             if (printDialog.ShowDialog() == DialogResult.OK)
             {
                 // Здесь вы должны указать, как именно печатать файл .lbx
                 // Например, если у вас есть программа, которая может печатать этот файл:
                 PrintLbxFile(filePath, printDialog.PrinterSettings.PrinterName);
             }*/

            // Открытие диалогового окна для выбора принтера
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintLbxFile(string filePath, string printerName)
        {
            // Создаем новый процесс для печати .lbx файла
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Program Files (x86)\\Brother\\P-touch Editor\\6\\PtouchEditor6.Wpf.exe"; // Укажите путь к программе для печати
            process.StartInfo.Arguments = $"{filePath} /P {printerName}"; // Пример аргументов, зависит от программы
            process.StartInfo.UseShellExecute = false; // Убедитесь, что программа может быть запущена без оболочки
            process.StartInfo.CreateNoWindow = true; // Не показывать окно программы

            try
            {
                process.Start();
                process.WaitForExit(); // Ожидание завершения процесса
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отправке на печать: " + ex.Message);
            }
        }


        private void FindAndReplace(Word.Document doc, string findText, string replaceText)
        {
            Word.Find findObject = doc.Application.Selection.Find;
            findObject.ClearFormatting();
            findObject.Text = findText;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = replaceText;

            object missing = Type.Missing;
            findObject.Execute(FindText: missing, ReplaceWith: missing,
                               Replace: Word.WdReplace.wdReplaceAll);
        }
    }
}
