﻿using Guna.UI2.WinForms;
using Microsoft.Office.Interop.Word;
using OfficeOpenXml;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TaxoNavicon.Forms;
using TaxoNaviconRussian;
using Word = Microsoft.Office.Interop.Word;

namespace TaxoNavicon
{

    public partial class RussianTypeForm : Form
    {
        /*
        --order
        <orderNumber> номерЗаказа
        <master>  мастер
        <dataJob> - датаВыполнениеРабот
        <newDataJob> - датаВыполнениеНовыхРабот

         --customer
        <nameCustomer> имяКлиента
        <adresCustomer> адресКлиента

        --vehicle
        <markaVehicle> маркаТранспорта
        <modelVehicle> модельТранспорта
        <vinVehicle> винТранспорта
        <registrationNumberVehicle> регНомерТранспорта
        <tireMarkingsVehicle> маркировкаШинТранспорта
        <odometrKmVehicle> одометрТранспорта

        --Tachograph
        <manufacturerTahograph> производительТахографа
        <serialNumberTahograph> серийныйНомерТахографа
        <modelTahograph> модельТахографа
        <productionTahograph> датаПроизводстваТахографа

        <L>
        <W>
        <k>

        <locationInstallationTable> // расположениеУстановочнойТаблицы
        <inspectionResult> // результатИнспекции
        <signsManipulation> // признакиМанипуляции
        <specialMarks> // особыеОтметки
        */
        PoleDataRussian poleDataRussian;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;

        private string filePathSaveJson;
        private string filePathCertificate;
        public RussianTypeForm()
        {
            InitializeComponent();

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appName = "TachoPrintData"; // Замените на название Вашего приложения
            string settingsFolder = Path.Combine(documentsPath, appName);

            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
            }

            filePathSaveJson = Path.Combine(settingsFolder, "JsonSetting.json");
            LoadSettingJS();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Или LicenseContext.Commercial, если у вас коммерческая лицензия
            poleDataRussian = new PoleDataRussian();
        }


        // Открытие окна загрузок
        private void toolStripMenuItemLoadRussianDocument_Click(object sender, System.EventArgs e)
        {
            LoadData();
        }

        #region SetData
        private void SetData()
        {
            //Order - заказ
            poleDataRussian.orderNumber = (int)guna2NumericUpDown1.Value;// номер заказа
            poleDataRussian.master = comboBoxMaster.Text; // мастер


            //Customer - заказчик
            poleDataRussian.nameCustomer = textBoxNameCustomer.Text; // имя русском
            poleDataRussian.adresCustomer = textBoxAdresCustomer.Text;// адрес заказчика

            //Vehicle - транспорт
            poleDataRussian.markaVehicle = MarkaVehical.Text; // марка машины
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

            poleDataRussian.dataJob = dataJob.Value.ToShortDateString();//  время выполнения работ

            // Тут обработаем новую дату выполнения работ
            poleDataRussian.newDataJob = dataJob.Value.AddYears(3).ToShortDateString();//  время выполнения работ

            poleDataRussian.locationInstallationTable = guna2TextBox4.Text; 
            poleDataRussian.inspectionResult = guna2TextBox1.Text;
            poleDataRussian.signsManipulation = guna2TextBox2.Text;
            poleDataRussian.specialMarks = guna2TextBox3.Text;
        }
        #endregion

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

        #region ClouseConnection
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
        #endregion
        // Word документ
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
        // Тут происходит сохранение в базу данных и проверка на повторение номера заказа
        private void toolStripMenuItemSaveData_Click(object sender, EventArgs e)
        {
            SetData();
            FileInfo existingFile = new FileInfo(filePathCertificate);
            using (ExcelPackage excelPackage = new ExcelPackage(existingFile))
            {
                // Получаем существующий лист
                var worksheet = excelPackage.Workbook.Worksheets["RussianCertificate"];
                
                int startRow = 3; // Первые 2 строчки это заголовки
                int row = startRow;

                // Ищем первую пустую строку
                while (worksheet.Cells[row, 1].Value != null) // Проверяем первую ячейку в строке
                {
                    var cellValue = worksheet.Cells[row, 1].Text;
                    if (string.Equals(cellValue, poleDataRussian.orderNumber.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Такой номер заказа уже есть!");
                        return; // Копия найдена
                    }
                    row++;
                }   

                // Заполняем данные из формы
                worksheet.Cells[row, 1].Value = poleDataRussian.orderNumber.ToString();
                worksheet.Cells[row, 2].Value = poleDataRussian.master; 
                worksheet.Cells[row, 3].Value = poleDataRussian.dataJob; 
                worksheet.Cells[row, 4].Value = poleDataRussian.newDataJob;

                worksheet.Cells[row, 5].Value = poleDataRussian.nameCustomer;
                worksheet.Cells[row, 6].Value = poleDataRussian.adresCustomer; 

                worksheet.Cells[row, 7].Value = poleDataRussian.manufacturerTahograph; 
                worksheet.Cells[row, 8].Value = poleDataRussian.serialNumberTahograph; 
                worksheet.Cells[row, 9].Value = poleDataRussian.modelTachograph; 
                worksheet.Cells[row, 10].Value = poleDataRussian.producedTachograph;
                
                worksheet.Cells[row, 11].Value = poleDataRussian.markaVehicle; 
                worksheet.Cells[row, 12].Value = poleDataRussian.vinVehicle; 
                worksheet.Cells[row, 13].Value = poleDataRussian.tireMarkingsVehicle;  
                worksheet.Cells[row, 14].Value = poleDataRussian.modelVehicle;  
                worksheet.Cells[row, 15].Value = poleDataRussian.registrationNumberVehicle;  
                worksheet.Cells[row, 16].Value = poleDataRussian.odometerKmVehicle; 
                
                worksheet.Cells[row, 17].Value = poleDataRussian.w;  
                worksheet.Cells[row, 18].Value = poleDataRussian.k;  
                worksheet.Cells[row, 19].Value = poleDataRussian.l; 
                

                worksheet.Cells[row, 20].Value = poleDataRussian.locationInstallationTable;
                worksheet.Cells[row, 21].Value = poleDataRussian.inspectionResult;
                worksheet.Cells[row, 22].Value = poleDataRussian.signsManipulation;
                worksheet.Cells[row, 23].Value = poleDataRussian.specialMarks;

                // Сохраняем изменения
                excelPackage.Save();
                MessageBox.Show("Данные успешно добавлены в Excel!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadData()
        {
            LoadRussianDocument loadRussianDocument = new LoadRussianDocument(GetDataLoad, filePathCertificate);

            loadRussianDocument.Show();
        }

        /// <summary>
        /// Метод который будет принимать параметры из окна загрузок
        /// </summary>
        public void GetDataLoad(PoleDataRussian poleDataRussian)
        {
            //Order - заказ
            guna2NumericUpDown1.Value = poleDataRussian.orderNumber;// номер заказа
            comboBoxMaster.Text = poleDataRussian.master; // мастер
            dataJob.Value = DateTime.Parse(poleDataRussian.dataJob); // Установка значения в DateTimePicker

            //Customer - заказчик
            textBoxNameCustomer.Text = poleDataRussian.nameCustomer; // имя русском
            textBoxAdresCustomer.Text = poleDataRussian.adresCustomer;// адрес заказчика

            //Vehicle - транспорт
            textBoxManufacturerVehicle.Text = poleDataRussian.markaVehicle; // марка машины
            textBoxModelVehicle.Text = poleDataRussian.modelVehicle; // модель машины
            textBoxVinNumberVehicle.Text = poleDataRussian.vinVehicle; // вин номер машины
            textBoxRegistrationNumberVehicle.Text = poleDataRussian.registrationNumberVehicle; // рег. номер машины
            textBoxOdometerKmVehicle.Text = poleDataRussian.odometerKmVehicle; // одометр км
            textBoxTireMarkingsVehicle.Text = poleDataRussian.tireMarkingsVehicle;// маркировка шин

            //Tahograf - тахограф
            textBoxManufacturerTachograph.Text = poleDataRussian.manufacturerTahograph; // производитель
            textBoxModelTachograph.Text = poleDataRussian.modelTachograph; // модель тахографа
            textBoxProducedTachograph.Text = poleDataRussian.producedTachograph; // год производства
            textBoxSerialNumberTahograph.Text = poleDataRussian.serialNumberTahograph; // год производства


            
            guna2TextBox4.Text = poleDataRussian.locationInstallationTable;
            guna2TextBox1.Text = poleDataRussian.inspectionResult;
            guna2TextBox2.Text = poleDataRussian.signsManipulation;
            guna2TextBox3.Text = poleDataRussian.specialMarks;

            textBoxL.Text = poleDataRussian.l;
            textBoxW.Text = poleDataRussian.w;
            textBoxK.Text = poleDataRussian.k;

            // Подгружаем наши данные в переменные экземпляра то есть локально
            SetData();
        }

        /// <summary>
        /// Метод загружает путь к таблице
        /// </summary>
        public void LoadSettingJS()
        {
            var saveJson = File.ReadAllText(filePathSaveJson);

            SettingsJS settingsJS = JsonSerializer.Deserialize<SettingsJS>(saveJson);
            filePathCertificate = settingsJS.FilePath;
        }

        // Метод будет следить за именением данных в полях
        public void ChangeBox(object sender, EventArgs e)
        {
            SetData();
        }

        // Метод просто перезаписывает данные
        private void ToolStripMenuItemResetData_Click(object sender, EventArgs e)
        {
            SetData();
            FileInfo existingFile = new FileInfo(filePathCertificate);
            using (ExcelPackage excelPackage = new ExcelPackage(existingFile))
            {
                // Получаем существующий лист
                var worksheet = excelPackage.Workbook.Worksheets["RussianCertificate"];

                int startRow = 3; // Первые 2 строчки это заголовки
                int row = startRow;

                // Ищем первую пустую строку
                while (worksheet.Cells[row, 1].Value != null) // Проверяем первую ячейку в строке
                {
                    var cellValue = worksheet.Cells[row, 1].Text;
                    if (string.Equals(cellValue, poleDataRussian.orderNumber.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Данные перезаписанны!");
                        // Заполняем данные из формы
                        worksheet.Cells[row, 1].Value = poleDataRussian.orderNumber.ToString();
                        worksheet.Cells[row, 2].Value = poleDataRussian.master;
                        worksheet.Cells[row, 3].Value = poleDataRussian.dataJob;
                        worksheet.Cells[row, 4].Value = poleDataRussian.newDataJob;

                        worksheet.Cells[row, 5].Value = poleDataRussian.nameCustomer;
                        worksheet.Cells[row, 6].Value = poleDataRussian.adresCustomer;

                        worksheet.Cells[row, 7].Value = poleDataRussian.manufacturerTahograph;
                        worksheet.Cells[row, 8].Value = poleDataRussian.serialNumberTahograph;
                        worksheet.Cells[row, 9].Value = poleDataRussian.modelTachograph;
                        worksheet.Cells[row, 10].Value = poleDataRussian.producedTachograph;

                        worksheet.Cells[row, 11].Value = poleDataRussian.markaVehicle;
                        worksheet.Cells[row, 12].Value = poleDataRussian.vinVehicle;
                        worksheet.Cells[row, 13].Value = poleDataRussian.tireMarkingsVehicle;
                        worksheet.Cells[row, 14].Value = poleDataRussian.modelVehicle;
                        worksheet.Cells[row, 15].Value = poleDataRussian.registrationNumberVehicle;
                        worksheet.Cells[row, 16].Value = poleDataRussian.odometerKmVehicle;

                        worksheet.Cells[row, 17].Value = poleDataRussian.w;
                        worksheet.Cells[row, 18].Value = poleDataRussian.k;
                        worksheet.Cells[row, 19].Value = poleDataRussian.l;


                        worksheet.Cells[row, 20].Value = poleDataRussian.locationInstallationTable;
                        worksheet.Cells[row, 21].Value = poleDataRussian.inspectionResult;
                        worksheet.Cells[row, 22].Value = poleDataRussian.signsManipulation;
                        worksheet.Cells[row, 23].Value = poleDataRussian.specialMarks;
                        excelPackage.Save();
                        return; // Копия найдена
                    }
                    row++;
                }
            }
        }

        private void ToolStripMenuItemPrintSticker_Click(object sender, EventArgs e)
        {
            PrintStickerRussian printStickerRussian = new PrintStickerRussian(poleDataRussian);

            printStickerRussian.ShowDialog();
        }

        private void ToolStripMenuItemPrintCertificate_Click(object sender, EventArgs e)
        {
            SetData();
            string relativePath = @"RussianCertificate.docx"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();
            dataJob.CustomFormat = "dd/MM/yyyy"; // Устанавливаем только дату

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
            }
            ClouseConnectionWord();
        }
    }
}
