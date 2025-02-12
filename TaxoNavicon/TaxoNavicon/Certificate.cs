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
    /*
    --order
    <orderNumber>
    <master>
    <responsible> // 
    <dataJob> - дата выполнение работ

     --customer
    <nameCustomer>
    <nameCustomerEng>
    <adresCustomer>

    --vehicle
    <manufacturerVehicle>
    <modelVehicle>
    <yearOfIssueVehicle>
    <vinVehicle>
    <registrationNumberVehicle>
    <tireMarkingsVehicle>
    <odometrKmVehicle>

    --Tachograph
    <manufacturerTahograph>
    <serialNumberTachograph>
    <modelTahograph>

    <L>
    <W>
    <k>
    <noteOrder> - примечание 
    */
    public partial class Certificate : Form
    {
        private PrintDocument printDocument;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;
        
        public Certificate(PoleDataEuropean poleData)
        {
            InitializeComponent();
            string relativePath = @"test.doc"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();

            CheckOpenDock();
            //wordDoc = wordApp.Documents.Open(filePath);

            #region money
            FindAndReplace(wordDoc, "<orderNumber>", poleData.orderNumber.ToString());
            FindAndReplace(wordDoc, "<master>", poleData.master);
            FindAndReplace(wordDoc, "<dataJob>", poleData.dataJob);

            FindAndReplace(wordDoc, "<nameCustomer>", poleData.nameCustomer);
            FindAndReplace(wordDoc, "<nameCustomerEng>", poleData.nameCustomerEng);
            FindAndReplace(wordDoc, "<adresCustomer>", poleData.adresCustomer);

            FindAndReplace(wordDoc, "<manufacturerVehicle>", poleData.manufacturerVehicle);
            FindAndReplace(wordDoc, "<modelVehicle>", poleData.modelVehicle);
            FindAndReplace(wordDoc, "<yearOfIssueVehicle>", poleData.yearOfIssueVehiccle);
            FindAndReplace(wordDoc, "<vinVehicle>", poleData.vinVehicle);
            FindAndReplace(wordDoc, "<registrationNumberVehicle>", poleData.registrationNumberVehicle);
            FindAndReplace(wordDoc, "<tireMarkingsVehicle>", poleData.tireMarkingsVehicle);
            FindAndReplace(wordDoc, "<odometrKmVehicle>", poleData.odometerKmVehicle);

            FindAndReplace(wordDoc, "<manufacturerTahograph>", poleData.manufacturerTahograph);
            FindAndReplace(wordDoc, "<serialNumberTahograph>", poleData.serialNumberTachograph);
            FindAndReplace(wordDoc, "<modelTachograph>", poleData.modelTachograph);
            FindAndReplace(wordDoc, "<producedTachograph>", poleData.producedTachograph);

            FindAndReplace(wordDoc, "<L>", poleData.l);
            FindAndReplace(wordDoc, "<W>", poleData.w);
            FindAndReplace(wordDoc, "<K>", poleData.k);
            FindAndReplace(wordDoc, "<noteOrder>", poleData.noteOrder);
            #endregion
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ClouseConnectionWord();
            Console.WriteLine("Окно закрыто");
            base.OnFormClosing(e);
        }

        private void ClouseConnectionWord()
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
        }

        private void CheckOpenDock()
        {
            bool isOpen = false;
            Document openDoc = null;

            foreach (Word.Document doc in wordApp.Documents)
            {
                // Сравниваем полные пути документов
                if (doc.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                {
                    isOpen = true;
                    openDoc = doc; // Сохраняем ссылку на открытый документ
                    break;
                }
            }

            if (isOpen)
            {
                // Закрываем документ, спрашивая, нужно ли сохранить изменения
                DialogResult result = MessageBox.Show("Документ уже открыт. Закрыть его?", "Закрытие документа", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    openDoc.Close(WdSaveOptions.wdSaveChanges); // Сохранить изменения
                    MessageBox.Show("Документ закрыт.");
                }
                else
                {
                    MessageBox.Show("Документ остается открытым.");
                }
            }
            else
            {
                try
                {
                    // Открытие документа, если он не открыт
                    wordDoc = wordApp.Documents.Open(filePath);
                    MessageBox.Show("Документ успешно открыт.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии документа: " + ex.Message);
                }
            }
        }
    }
}
