using Microsoft.Office.Interop.Word;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Font = System.Drawing.Font;
using Word = Microsoft.Office.Interop.Word;

namespace TaxoNavicon
{
    public partial class Certificate : Form
    {
        private PrintDocument printDocument;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;


        public Certificate()
        {
            InitializeComponent();  

            string relativePath = @"test.docx"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();
            wordDoc = wordApp.Documents.Open(filePath);
        }

        private void toolStripLabelPrint_Click(object sender, EventArgs e)
        {
            // Выводим путь для отладки
            //MessageBox.Show($"Путь к файлу: {filePath}");
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
                printDialog = null;
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (wordDoc != null)
            {
                // Извлечение текста из документа Word
                string text = wordDoc.Content.Text;
                e.Graphics.DrawString(text, new Font("Arial", 12), Brushes.Black, new RectangleF(100, 100, e.MarginBounds.Width, e.MarginBounds.Height));
            }
        }

        private void GenerateCertificate_Click(object sender, EventArgs e)
        {
            // Здесь можно вызвать предварительный просмотр
            printDocument = new PrintDocument();
            
            printPreviewControl.Document = printDocument; // Установите документ для предварительного просмотра
            printPreviewControl.Invalidate();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Закрываем документ и приложение Word
            if (wordDoc != null)
            {
                wordDoc.Close(false); // Закрываем документ без сохранения
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc);
                wordDoc = null;
            }

            if (wordApp != null)
            {
                wordApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                wordApp = null;
            }
            Console.WriteLine("Окно закрыто");
            base.OnFormClosing(e);
        }
    }
}
