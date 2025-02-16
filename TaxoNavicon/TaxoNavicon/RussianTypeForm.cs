using Microsoft.Office.Interop.Word;
using Npgsql;
using System;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using TaxoNaviconRussian;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Word = Microsoft.Office.Interop.Word;

namespace TaxoNavicon
{

    public partial class RussianTypeForm : Form
    {
        /*
        --order
        <orderNumber>
        <master>
        <dataJob> - дата выполнение работ
        <newDataJob> - дата выполнение новых работ

         --customer
        <nameCustomer>
        <adresCustomer>

        --vehicle
        <markaVehicle>
        <modelVehicle>
        <vinVehicle>
        <registrationNumberVehicle>
        <tireMarkingsVehicle>
        <odometrKmVehicle>

        --Tachograph
        <manufacturerTahograph>
        <serialNumberTahograph>
        <modelTahograph>
        <dataTahograph>
        <productionTahograph>

        <L>
        <W>
        <k>

        <locationInstallationTable> // Расположение установочной таблицы
        <inspectionResult> // Результат инспекции
        <signsManipulation> // Признаки манипуляции
        <specialMarks> // Особые отметки
        */

        string sql = "Server=localhost;Port=5432;Database=Certificate; User Id = postgres; Password=123;";
        PoleDataRussian poleDataRussian;
        private PrintDocument printDocument;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;
        public RussianTypeForm()
        {
            InitializeComponent();
            poleDataRussian = new PoleDataRussian();
            string relativePath = @"RussianCertificate.docx"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();
            dateTimePickerJob.CustomFormat = "dd/MM/yyyy"; // Устанавливаем только дату
        }


        // Открытие окна загрузок
        private void toolStripMenuItemLoadRussianDocument_Click(object sender, System.EventArgs e)
        {
            LoadRussianDocument loadRussianDocument = new LoadRussianDocument();

            loadRussianDocument.Show();
        }
        private void SetData()
        {
            //Order - заказ
            poleDataRussian.orderNumber = (int)numericUpDowntextBoxOrderNumber.Value;// номер заказа
            poleDataRussian.master = comboBoxMaster.Text; // мастер


            //Customer - заказчик
            poleDataRussian.nameCustomer = textBoxNameCustomer.Text; // имя русском
            poleDataRussian.adresCustomer = textBoxAdresCustomer.Text;// адрес заказчика

            //Vehicle - транспорт
            poleDataRussian.markaVehicle = textBoxMarkaVehicle.Text; // марка машины
            poleDataRussian.modelVehicle = textBoxModelVehicle.Text; // модель машины
            poleDataRussian.vinVehicle = textBoxVinNumberVehicle.Text; // вин номер машины
            poleDataRussian.registrationNumberVehicle = textBoxRegistrationNumberVehicle.Text; // рег. номер машины
            poleDataRussian.odometerKmVehicle = textBoxOdometerKmVehicle.Text; // одометр км
            poleDataRussian.tireMarkingsVehicle = textBoxTireMarkingsVehicle.Text;// маркировка шин

            //Tahograf - тахограф
            poleDataRussian.manufacturerTahograph = textBoxManufacturerTachograph.Text; // производитель
            poleDataRussian.modelTachograph = textBoxModelTachograph.Text; // модель тахографа
            poleDataRussian.producedTachograph = textBoxProducedTachograph.Text; // год производства
            poleDataRussian.serialNumberTahograph = textBoxSerialNumberTahograph.Text; // год производства

            poleDataRussian.l = textBoxL.Text;
            poleDataRussian.w = textBoxW.Text;
            poleDataRussian.k = textBoxK.Text;

            poleDataRussian.dataJob = dateTimePickerJob.Value.ToShortDateString();//  время выполнения работ

            // Тут обработаем новую дату выполнения работ
            poleDataRussian.newDataJob = dateTimePickerJob.Value.AddYears(3).ToShortDateString();//  время выполнения работ

            poleDataRussian.locationInstallationTable = textBoxLocationInstallationTable.Text;
            poleDataRussian.inspectionResult = comboBoxInspectionResult.Text;
            poleDataRussian.signsManipulation = comboBoxSignsManipulation.Text;
            poleDataRussian.specialMarks = textBoxSpecialMarks.Text;
        }

        private void ToolStripMenuItemPrintCertificate_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine(poleDataRussian.newDataJob);
            Console.WriteLine(poleDataRussian.dataJob);
            Console.WriteLine(poleDataRussian.markaVehicle);
            Console.WriteLine(poleDataRussian.manufacturerTahograph);
            CheckOpenDock();
            //wordDoc = wordApp.Documents.Open(filePath);

            #region money
            FindAndReplace(wordDoc, "<orderNumber>", poleDataRussian.orderNumber.ToString());
            FindAndReplace(wordDoc, "<master>", poleDataRussian.master);
            FindAndReplace(wordDoc, "<dataJob>", poleDataRussian.dataJob);
            FindAndReplace(wordDoc, "<newData>", poleDataRussian.newDataJob);

            FindAndReplace(wordDoc, "<nameCustomer>", poleDataRussian.nameCustomer);
            FindAndReplace(wordDoc, "<adresCustomer>", poleDataRussian.adresCustomer);

            FindAndReplace(wordDoc, "<markaVehicle>", poleDataRussian.markaVehicle);
            FindAndReplace(wordDoc, "<modelVehicle>", poleDataRussian.modelVehicle);
            FindAndReplace(wordDoc, "<vinVehicle>", poleDataRussian.vinVehicle);
            FindAndReplace(wordDoc, "<registrationNumberVehicle>", poleDataRussian.registrationNumberVehicle);
            FindAndReplace(wordDoc, "<tireMarkingsVehicle>", poleDataRussian.tireMarkingsVehicle);
            FindAndReplace(wordDoc, "<odometrKmVehicle>", poleDataRussian.odometerKmVehicle);

            FindAndReplace(wordDoc, "<manufacturerTahograph>", poleDataRussian.manufacturerTahograph);
            FindAndReplace(wordDoc, "<serialNumberTahograph>", poleDataRussian.serialNumberTahograph);
            FindAndReplace(wordDoc, "<modelTahograph>", poleDataRussian.modelTachograph);
            FindAndReplace(wordDoc, "<productionTahograph>", poleDataRussian.producedTachograph);


            FindAndReplace(wordDoc, "<locationInstallationTable>", poleDataRussian.locationInstallationTable);
            FindAndReplace(wordDoc, "<inspectionResult>", poleDataRussian.inspectionResult);
            FindAndReplace(wordDoc, "<signsManipulation>", poleDataRussian.signsManipulation);
            FindAndReplace(wordDoc, "<specialMarks>", poleDataRussian.specialMarks);

            FindAndReplace(wordDoc, "<L>", poleDataRussian.l);
            FindAndReplace(wordDoc, "<W>", poleDataRussian.w);
            FindAndReplace(wordDoc, "<K>", poleDataRussian.k);
            #endregion

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                wordDoc.PrintOut();
                ClouseConnectionWord();
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

        private void ToolStripMenuItemSetData_Click(object sender, EventArgs e)
        {
            SetData();
        }

        private void toolStripMenuItemSaveData_Click(object sender, EventArgs e)
        {
            SqlConnection();
        }

        private void SqlConnection()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(sql);

            try
            {
                npgsqlConnection.Open();

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand();
                npgsqlCommand.Connection = npgsqlConnection;
                npgsqlCommand.CommandType = System.Data.CommandType.Text;

                // Подготовка команды на вставку данныхs
                npgsqlCommand.CommandText = "INSERT INTO russiancertificate(orderNumber,master) VALUES (1,Sergey)";

                NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                if (npgsqlConnection.State == System.Data.ConnectionState.Open)
                {
                    npgsqlConnection.Close(); // Закрываем соединение, если оно открыто
                }
            }
        }
    }
}
