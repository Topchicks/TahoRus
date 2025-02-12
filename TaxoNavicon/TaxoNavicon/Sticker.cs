using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = System.Drawing.Font;
using Word = Microsoft.Office.Interop.Word;

namespace TaxoNavicon
{
    public partial class Sticker : Form
    {
        private PrintDocument printDocument = new PrintDocument();
        public Sticker()
        {
            InitializeComponent();
            PrintPreviewControl printPreviewControlSticker = new PrintPreviewControl()
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(printPreviewControlSticker);

            printDocument = new PrintDocument();

            // Создание нестандартного размера бумаги
            int widthInHundredthsOfInch = (int)(24 / 25.4 * 100);  // 24 мм в сотых долях дюйма
            int heightInHundredthsOfInch = (int)(134 / 25.4 * 100); // 134 мм в сотых долях дюйма

            // Установка размера бумаги для наклеек
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom Size", widthInHundredthsOfInch, heightInHundredthsOfInch); // 240 мм x 1340 мм

            // Установка ориентации на альбомную
            printDocument.DefaultPageSettings.Landscape = true;


            // Привязка обработчика события печати
            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            // Назначение документа для предварительного просмотра
            printPreviewControlSticker.Document = printDocument;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Установка шрифта
            Font font = new Font("Arial", 10);
            Brush brush = Brushes.Black;

            // Текст для печати
            string text = "Data: <Data>          VIN: <VINVEHICLE>       NaviCon OOO\n" +
                          "Tyres: <tire>         TNo:<regCar>            Bulvar stroiteley st., 3G, \n " +
                          "øl = <L>              w=<W>(imp/km)                Tambov\n" +
                          "s/n Tahoo:<sinTaho>   k=<k>(imp/km)           +7(4752)55-94-00\n   " +
                          "                                               navicontmb.ru";

            // Рисование текста
            e.Graphics.DrawString(text, font, brush, new PointF(10, 10));
        }

        private void toolStripLabelStartPrint_Click(object sender, EventArgs e)
        {
            // Открываем диалог выбора принтера
            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
    }
}
