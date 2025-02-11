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

        public Certificate(PoleData poleData)
        {
            InitializeComponent();

            string relativePath = @"test.doc"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();
            wordDoc = wordApp.Documents.Open(filePath);
            //Console.WriteLine("Номер заказа: " + poleData.orderNumber);
            //Console.WriteLine("Адрес заказчика: " + poleData.adresCustomer);


            FindAndReplace(wordDoc, "<adresCustomer>", poleData.adresCustomer);
        }

        private void toolStripLabelPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                wordDoc.PrintOut();
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
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            /*if (wordDoc != null)
            {
                // Извлечение текста из документа Word
                string text = wordDoc.Content.Text;
                e.Graphics.DrawString(text, new Font("Arial", 12), Brushes.Black, new RectangleF(100, 100, e.MarginBounds.Width, e.MarginBounds.Height));
            }*/
        }

        private void GenerateCertificate_Click(object sender, EventArgs e)
        {
            /*// Здесь можно вызвать предварительный просмотр
            printDocument = new PrintDocument();
            
            printPreviewControl.Document = printDocument; // Установите документ для предварительного просмотра
            printPreviewControl.Invalidate();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);*/
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
