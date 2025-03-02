using Microsoft.Office.Interop.Word;
using OfficeOpenXml;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TaxoNaviconRussian;
using Word = Microsoft.Office.Interop.Word;

namespace TaxoNavicon
{
    public partial class EuropeanTypeForm : Form
    {
        /*
            номерЗаказа
            мастер
            датаВыполненияРабот
        
            имяЗаказчика
            имяЗаказчикаАнлийский
            адресЗаказчика

            производительТранспорта
            модельТранспорта
            винНомерТранспорта
            регНомерТранспорта
            маркировкаШин
            одометрКм
            годВыпуска
            
            производительТахографа
            серийныйНомерТахографапа
            модельТахографа

            l
            w
            k
        */
        PoleDataEuropean poleDataEuropean;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;

        private string filePathSaveJson;
        private string filePathCertificate;
        public EuropeanTypeForm()
        {
            InitializeComponent();
            poleDataEuropean = new PoleDataEuropean();


            filePathSaveJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSetting.json");
            LoadSettingJS();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Или LicenseContext.Commercial, если у вас коммерческая лицензия
        }

        private void SetData()
        {
            //Order
            poleDataEuropean.orderNumber = (int)numericUpDowntextBoxOrderNumber.Value;// номер заказа
            poleDataEuropean.master = comboBoxMaster.Text; // мастер


            //Customer
            poleDataEuropean.nameCustomer = textBoxNameCustomer.Text; // имя русском
            poleDataEuropean.nameCustomerEng = textBoxNameCustomerEng.Text; // имя на английском
            poleDataEuropean.adresCustomer = textBoxAdresCustomer.Text;// адрес заказчика

            //Vehicle
            poleDataEuropean.manufacturerVehicle = textBoxManufacturerTachograph.Text; // марка машины
            poleDataEuropean.modelVehicle = textBoxModelVehicle.Text; // модель машины
            poleDataEuropean.vinVehicle = textBoxVinNumberVehicle.Text; // вин номер машины
            poleDataEuropean.registrationNumberVehicle = textBoxRegistrationNumberVehicle.Text; // рег. номер машины
            poleDataEuropean.odometerKmVehicle = textBoxOdometerKmVehicle.Text; // одометр км
            poleDataEuropean.yearOfIssueVehiccle = textBoxYearOfIssueVehiccle.Text; // год выпуска
            poleDataEuropean.tireMarkingsVehicle = textBoxTireMarkingsVehicle.Text; // маркировка шин

            //Tahograf
            poleDataEuropean.manufacturerTahograph = textBoxManufacturerTachograph.Text; // производитель
            poleDataEuropean.serialNumberTahograph = textBoxSerialNumberTahograph.Text; // производитель
            poleDataEuropean.modelTachograph = textBoxModelTachograph.Text; // модель тахографа

            poleDataEuropean.l = textBoxL.Text;
            poleDataEuropean.w = textBoxW.Text;
            poleDataEuropean.k = textBoxK.Text;

            poleDataEuropean.dataJob = dateTimePickerJob.Value.ToShortDateString();//  время выполнения работ
        }
        // Для Word
        private void ToolStripMenuItemPrintCertificate_Click(object sender, EventArgs e)
        {
            SetData();
            string relativePath = @"EuropeanCertidicate.doc"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();
            dateTimePickerJob.CustomFormat = "dd/MM/yyyy"; // Устанавливаем только дату

            CheckOpenDock();
            //wordDoc = wordApp.Documents.Open(filePath);

            #region money
            FindAndReplace(wordDoc, "<orderNumber>", poleDataEuropean.orderNumber.ToString());
            FindAndReplace(wordDoc, "<master>", poleDataEuropean.master);
            FindAndReplace(wordDoc, "<dataJob>", poleDataEuropean.dataJob);

            FindAndReplace(wordDoc, "<nameCustomer>", poleDataEuropean.nameCustomer);
            FindAndReplace(wordDoc, "<nameCustomerEng>", poleDataEuropean.nameCustomerEng);
            FindAndReplace(wordDoc, "<adresCustomer>", poleDataEuropean.adresCustomer);

            FindAndReplace(wordDoc, "<manufacturerVehicle>", poleDataEuropean.manufacturerVehicle);
            FindAndReplace(wordDoc, "<modelVehicle>", poleDataEuropean.modelVehicle);
            FindAndReplace(wordDoc, "<yearOfIssueVehicle>", poleDataEuropean.yearOfIssueVehiccle);
            FindAndReplace(wordDoc, "<vinVehicle>", poleDataEuropean.vinVehicle);
            FindAndReplace(wordDoc, "<registrationNumberVehicle>", poleDataEuropean.registrationNumberVehicle);
            FindAndReplace(wordDoc, "<tireMarkingsVehicle>", poleDataEuropean.tireMarkingsVehicle);
            FindAndReplace(wordDoc, "<odometrKmVehicle>", poleDataEuropean.odometerKmVehicle);

            FindAndReplace(wordDoc, "<manufacturerTahograph>", poleDataEuropean.manufacturerTahograph);
            FindAndReplace(wordDoc, "<serialNumberTahograph>", poleDataEuropean.serialNumberTahograph);
            FindAndReplace(wordDoc, "<modelTahograph>", poleDataEuropean.modelTachograph);

            FindAndReplace(wordDoc, "<L>", poleDataEuropean.l);
            FindAndReplace(wordDoc, "<W>", poleDataEuropean.w);
            FindAndReplace(wordDoc, "<K>", poleDataEuropean.k);
            #endregion

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                wordDoc.PrintOut();
            }
            ClouseConnectionWord();
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

        // Для Word
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
        // Для Word
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

        // Открытие окна для зпгрузки д
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadEuropeanDocument loadEuropeanDocument = new LoadEuropeanDocument(GetDataLoad, filePathCertificate);

            loadEuropeanDocument.Show();
        }

        /// <summary>
        /// Метод который будет принимать параметры из окна загрузок
        /// </summary>
        public void GetDataLoad(PoleDataEuropean poleDataEuropean)
        {
            //Order - заказ
            numericUpDowntextBoxOrderNumber.Value = poleDataEuropean.orderNumber;// номер заказа
            comboBoxMaster.Text = poleDataEuropean.master; // мастер
            dateTimePickerJob.Value = DateTime.Parse(poleDataEuropean.dataJob); // Установка значения в DateTimePicker

            //Customer - заказчик
            textBoxNameCustomer.Text = poleDataEuropean.nameCustomer; // имя русском
            textBoxNameCustomerEng.Text = poleDataEuropean.nameCustomerEng; // имя английский
            textBoxAdresCustomer.Text = poleDataEuropean.adresCustomer;// адрес заказчика

            //Vehicle - транспорт
            textBoxManufacturerVehicle.Text = poleDataEuropean.manufacturerVehicle; // Производитель машины
            textBoxModelVehicle.Text = poleDataEuropean.modelVehicle; // модель машины
            textBoxVinNumberVehicle.Text = poleDataEuropean.vinVehicle; // вин номер машины

            textBoxOdometerKmVehicle.Text = poleDataEuropean.odometerKmVehicle; // одометр км
            textBoxTireMarkingsVehicle.Text = poleDataEuropean.tireMarkingsVehicle;// маркировка шин
            textBoxYearOfIssueVehiccle.Text = poleDataEuropean.yearOfIssueVehiccle;// 
            textBoxRegistrationNumberVehicle.Text = poleDataEuropean.registrationNumberVehicle; // рег. номер машины

            //Tahograf - тахограф
            textBoxManufacturerTachograph.Text = poleDataEuropean.manufacturerTahograph; // производитель
            textBoxModelTachograph.Text = poleDataEuropean.modelTachograph; // модель тахографа
            textBoxSerialNumberTahograph.Text = poleDataEuropean.serialNumberTahograph; // год производства

            textBoxL.Text = poleDataEuropean.l;
            textBoxW.Text = poleDataEuropean.w;
            textBoxK.Text = poleDataEuropean.k;

            // Подгружаем наши данные в переменные экземпляра то есть локально
            SetData();
        }


        // Тут происходит сохранение в базу данных и проверка на повторение номера заказа
        private void ToolStripMenuItemSaveData_Click(object sender, EventArgs e)
        {
            SetData();

            FileInfo existingFile = new FileInfo(filePathCertificate);
            using (ExcelPackage excelPackage = new ExcelPackage(existingFile))
            {
                // Получаем существующий лист
                var worksheet = excelPackage.Workbook.Worksheets["EuropeanCertificate"];
                int startRow = 3; // Первые 2 строчки это заголовки
                int row = startRow;

                // Ищем первую пустую строку
                while (worksheet.Cells[row, 1].Value != null) // Проверяем первую ячейку в строке
                {
                    var cellValue = worksheet.Cells[row, 1].Text;
                    if (string.Equals(cellValue, poleDataEuropean.orderNumber.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Такой номер заказа уже есть!");

                        return; // Копия найдена
                    }
                    row++;
                }
                // Заполняем данные из формы
                worksheet.Cells[row, 1].Value = poleDataEuropean.orderNumber.ToString();
                worksheet.Cells[row, 2].Value = poleDataEuropean.master;
                worksheet.Cells[row, 3].Value = poleDataEuropean.dataJob;

                worksheet.Cells[row, 4].Value = poleDataEuropean.nameCustomer;
                worksheet.Cells[row, 5].Value = poleDataEuropean.nameCustomerEng;
                worksheet.Cells[row, 6].Value = poleDataEuropean.adresCustomer;

                worksheet.Cells[row, 7].Value = poleDataEuropean.manufacturerTahograph;
                worksheet.Cells[row, 8].Value = poleDataEuropean.serialNumberTahograph;
                worksheet.Cells[row, 9].Value = poleDataEuropean.modelTachograph;

                worksheet.Cells[row, 10].Value = poleDataEuropean.manufacturerVehicle;
                worksheet.Cells[row, 11].Value = poleDataEuropean.vinVehicle;
                worksheet.Cells[row, 12].Value = poleDataEuropean.tireMarkingsVehicle;
                worksheet.Cells[row, 13].Value = poleDataEuropean.modelVehicle;
                worksheet.Cells[row, 14].Value = poleDataEuropean.yearOfIssueVehiccle;
                worksheet.Cells[row, 15].Value = poleDataEuropean.registrationNumberVehicle;
                worksheet.Cells[row, 16].Value = poleDataEuropean.odometerKmVehicle;

                worksheet.Cells[row, 17].Value = poleDataEuropean.w;
                worksheet.Cells[row, 18].Value = poleDataEuropean.k;
                worksheet.Cells[row, 19].Value = poleDataEuropean.l;

                // Сохраняем изменения
                excelPackage.Save();
                MessageBox.Show("Данные успешно добавлены в Excel!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void LoadSettingJS()
        {
            var saveJson = File.ReadAllText(filePathSaveJson);

            SettingsJS settingsJS = JsonSerializer.Deserialize<SettingsJS>(saveJson);
            filePathCertificate = settingsJS.FilePath;
        }

        private void ToolStripMenuItemResetData_Click(object sender, EventArgs e)
        {
            SetData();
            FileInfo existingFile = new FileInfo(filePathCertificate);
            using (ExcelPackage excelPackage = new ExcelPackage(existingFile))
            {
                // Получаем существующий лист
                var worksheet = excelPackage.Workbook.Worksheets["EuropeanCertificate"];

                int startRow = 3; // Первые 2 строчки это заголовки
                int row = startRow;

                // Ищем первую пустую строку
                while (worksheet.Cells[row, 1].Value != null) // Проверяем первую ячейку в строке
                {
                    var cellValue = worksheet.Cells[row, 1].Text;
                    if (string.Equals(cellValue, poleDataEuropean.orderNumber.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Данные перезаписанны!");
                        // Заполняем данные из формы
                        worksheet.Cells[row, 1].Value = poleDataEuropean.orderNumber.ToString();
                        worksheet.Cells[row, 2].Value = poleDataEuropean.master;
                        worksheet.Cells[row, 3].Value = poleDataEuropean.dataJob;

                        worksheet.Cells[row, 4].Value = poleDataEuropean.nameCustomer;
                        worksheet.Cells[row, 5].Value = poleDataEuropean.nameCustomerEng;
                        worksheet.Cells[row, 6].Value = poleDataEuropean.adresCustomer;

                        worksheet.Cells[row, 7].Value = poleDataEuropean.manufacturerTahograph;
                        worksheet.Cells[row, 8].Value = poleDataEuropean.serialNumberTahograph;
                        worksheet.Cells[row, 9].Value = poleDataEuropean.modelTachograph;

                        worksheet.Cells[row, 10].Value = poleDataEuropean.manufacturerVehicle;
                        worksheet.Cells[row, 11].Value = poleDataEuropean.vinVehicle;
                        worksheet.Cells[row, 12].Value = poleDataEuropean.tireMarkingsVehicle;
                        worksheet.Cells[row, 13].Value = poleDataEuropean.modelVehicle;
                        worksheet.Cells[row, 14].Value = poleDataEuropean.registrationNumberVehicle;
                        worksheet.Cells[row, 15].Value = poleDataEuropean.odometerKmVehicle;

                        worksheet.Cells[row, 16].Value = poleDataEuropean.w;
                        worksheet.Cells[row, 17].Value = poleDataEuropean.k;
                        worksheet.Cells[row, 18].Value = poleDataEuropean.l;
                        excelPackage.Save();
                        return; // Копия найдена
                    }
                    row++;
                }
            }
        }

        private void ToolStripMenuItemPrintSticker_Click(object sender, EventArgs e)
        {

        }
    }
}
