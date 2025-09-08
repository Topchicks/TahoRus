using OfficeOpenXml;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;
using Word = Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;
using Guna.UI2.WinForms;
using TaxoNavicon.Model;

namespace TaxoNavicon
{
    public partial class EuropeanTypeForm : Form
    {
        PoleDataEuropean poleDataEuropean;
        Translate translate = new Translate();
        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;

        private string filePathSaveJson;
        private string filePathCertificate;

        private string defualtPrinterWord;
        private string defualtPrinterSticker;

        private string adressMasterRus;
        private string adressMasterEng;

        private string adressSticker;
        public EuropeanTypeForm()
        {
            InitializeComponent();
            poleDataEuropean = new PoleDataEuropean();


            // Тут получим относительный путь к файлу JSon настроек
            //pathSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSetting.json");
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
            poleDataEuropean.adresCustomerEng = textBoxAdresCustomerEng.Text;// адрес заказчика

            //Vehicle
            poleDataEuropean.manufacturerVehicle = textBoxManufacturerVehicle.Text; // марка машины
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

            poleDataEuropean.dataJob = dataJob.Value.ToString("dd.MM.yyyy"); // Формат: день.месяц.год

            poleDataEuropean.russAdresMaster = adressMasterRus;
            poleDataEuropean.engAdresMaster = adressMasterEng;
            poleDataEuropean.temperature = textBoxTemperature.Text;
            poleDataEuropean.protectore = textBoxTyreWear.Text;
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
            Word.Document openDoc = null;

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
                    openDoc.Close(Word.WdSaveOptions.wdSaveChanges); // Сохранить изменения
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
        /// <summary>
        /// Метод который будет принимать параметры из окна загрузок
        /// </summary>
        public void GetDataLoad(PoleDataEuropean poleDataEuropean)
        {
            //Order - заказ
            numericUpDowntextBoxOrderNumber.Value = poleDataEuropean.orderNumber;// номер заказа
            comboBoxMaster.Text = poleDataEuropean.master; // мастер
            dataJob.Value = DateTime.Parse(poleDataEuropean.dataJob); // Установка значения в DateTimePicker

            //Customer - заказчик
            textBoxNameCustomer.Text = poleDataEuropean.nameCustomer; // имя русском
            textBoxNameCustomerEng.Text = poleDataEuropean.nameCustomerEng; // имя английский
            textBoxAdresCustomer.Text = poleDataEuropean.adresCustomer;// адрес заказчика
            textBoxAdresCustomerEng.Text = poleDataEuropean.adresCustomerEng;// адрес заказчика

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

            textBoxTemperature.Text = poleDataEuropean.temperature;
            textBoxTyreWear.Text = poleDataEuropean.protectore;
            textBoxAdresCustomerEng.Text = poleDataEuropean.adresCustomerEng;

            // Подгружаем наши данные в переменные экземпляра то есть локально
            SetData();
        }


        // Тут происходит сохранение в базу данных и проверка на повторение номера заказа
        private void SaveButton()
        {
            if(textBoxNameCustomer.Text != "")
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
                    worksheet.Cells[row, 20].Value = poleDataEuropean.adresCustomerEng;

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


                    worksheet.Cells[row, 21].Value = poleDataEuropean.temperature;
                    worksheet.Cells[row, 22].Value = poleDataEuropean.protectore;

                    // Сохраняем изменения
                    excelPackage.Save();
                    MessageBox.Show("Данные успешно добавлены в Excel!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Ошибка ячейка имя клиента");
            }
        }
        #region Json
        public void LoadSettingJS()
        {
            var saveJson = File.ReadAllText(filePathSaveJson);

            SettingsJS settingsJS = JsonSerializer.Deserialize<SettingsJS>(saveJson);
            filePathCertificate = settingsJS.FilePath;
            defualtPrinterWord = settingsJS.DefualtPrinterWord;
            defualtPrinterSticker = settingsJS.DefualtPrinterSticker;
            formatingSticker = settingsJS.FormatingSticker;
            adressMasterRus = settingsJS.AdressMasterRus;
            adressMasterEng = settingsJS.AdressMasterEng;
            adressSticker = settingsJS.AdressSticker;
        }

        #endregion

        // Логика перезаписи данных
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
                        worksheet.Cells[row, 20].Value = poleDataEuropean.adresCustomerEng;

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

                        worksheet.Cells[row, 21].Value = poleDataEuropean.temperature;
                        worksheet.Cells[row, 22].Value = poleDataEuropean.protectore;
                        excelPackage.Save();
                        return; // Копия найдена
                    }
                    row++;
                }
            }
        }



        // Открытие окна печати стикера

        private PrintDocument printDocument;
        private bool formatingSticker;
        private string pathSettingsFile; // Путь к файлу с настройками

        // Это использовать для печати наклейки
        private void ToolStripMenuItemPrintSticker_Click(object sender, EventArgs e)
        {
            PrintPreviewControl printPreviewControl = new PrintPreviewControl();
            pathSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSetting.json");
            LoadSettingJS();
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            // Устанавливаем размер страницы
            if (formatingSticker == true)
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

            /* Вернуть блок если нужно чтобы было окно песчати
            PrintStickerEuropean printStickerEuropean = null;
            if (printStickerEuropean == null)
            {
                printStickerEuropean = new PrintStickerEuropean(poleDataEuropean);

                printStickerEuropean.ShowDialog();
            }*/

            // Открытие диалогового окна для выбора принтера
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrinterSettings = new PrinterSettings();

            // Также есть автоматический вывод на печать

            printDocument.DefaultPageSettings.PrinterSettings.PrinterName = defualtPrinterSticker;
            printDocument.Print();
        }

        // Основная логика печати наклейки
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
            g.DrawString("Øl: " + poleDataEuropean.l + " mm", font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("s/n Tacho: " + poleDataEuropean.serialNumberTahograph, font, Brushes.Black, xOffset, yOffset);

            xOffset = 160;
            yOffset = 5;

            g.DrawString("VIN: " + poleDataEuropean.vinVehicle, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("TNo: " + poleDataEuropean.registrationNumberVehicle, font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("w: " + poleDataEuropean.w + " imp/km", font, Brushes.Black, xOffset, yOffset);

            yOffset += lineSpacing;
            g.DrawString("k: " + poleDataEuropean.k + " imp/km", font, Brushes.Black, xOffset, yOffset);

            xOffset = 305;
            yOffset = 2;
            // Возможно, добавьте отступы для визуального разделения
            xOffset += lineSpacing * 2; // Дополнительный отступ перед следующей секцией

            // Ниже можно добавить информацию о компании
            g.DrawString("  NaviCon OOO", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing;
            g.DrawString($"{adressSticker}, \n       Tambov", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing + 12;
            g.DrawString("+7(4752)55-94-00", font, Brushes.Black, xOffset, yOffset);
            yOffset += lineSpacing - 1;
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

        // Печать сертификата
        private void ToolStripMenuItemPrintCertificate_Click_1(object sender, EventArgs e)
        {
       /*     PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.DocumentName = filePathCertificate;
            printDialog.ShowDialog();
            printDocument.Print();*/
              try
              {
                  SetData();
                  string relativePath = @"EuropeanCertidicate.doc"; // Относительный путь к файлу
                  filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                  wordApp = new Word.Application();
                  dataJob.CustomFormat = "dd/MM/yyyy"; // Устанавливаем только дату

                  CheckOpenDock();

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


                  FindAndReplace(wordDoc, "<russAdresMaster>", poleDataEuropean.russAdresMaster);
                  FindAndReplace(wordDoc, "<euroAdresMaster>", poleDataEuropean.engAdresMaster);
                  FindAndReplace(wordDoc, "<Tem>", poleDataEuropean.temperature);
                  FindAndReplace(wordDoc, "<Tw>", poleDataEuropean.protectore);
                  FindAndReplace(wordDoc, "<adresCustomerEng>", poleDataEuropean.adresCustomerEng);


                  #endregion

                  PrintDialog printDialog = new PrintDialog();
                  printDialog.PrinterSettings = new PrinterSettings();

                  // Устанавливаем выбранный принтер
                  wordApp.ActivePrinter = defualtPrinterWord;

                  // Печатаем документ
                  wordDoc.PrintOut();

                  ClouseConnectionWord();
              }
              catch(Exception ex)
              {
                  ClouseConnectionWord();
                  MessageBox.Show("Ошибка: " + ex);
              }
        }

        // Проерка введёного текста что он на английском
        private void OnlyEng(object sender, KeyPressEventArgs e)
        {
            // Приводим sender к типу TextBox
            Guna2TextBox currentTextBox = sender as Guna2TextBox;

            // Проверяем, является ли введенный символ русским
            if (IsCyrillic(e.KeyChar))
            {
                e.Handled = true; // Запрещаем ввод
                FlashTextBox(currentTextBox); // Передаем текущий TextBox в метод
            }
        }

        // Проерка введёного текста что он на русском
        private void OnlyRuss(object sender, KeyPressEventArgs e)
        {
            // Приводим sender к типу TextBox
            Guna2TextBox currentTextBox = sender as Guna2TextBox;

            // Проверяем, является ли введенный символ русским
            if (IsLatin(e.KeyChar))
            {
                e.Handled = true; // Запрещаем ввод
                FlashTextBox(currentTextBox); // Передаем текущий TextBox в метод
            }
        }

        private void Translater()
        {
            textBoxNameCustomerEng.Text = translate.Transliterate(textBoxNameCustomer.Text);
            textBoxAdresCustomerEng.Text = translate.Transliterate(textBoxAdresCustomer.Text);

        }

        // Это для текста VIN номер ТС
        private void OnlyEngNo_o(object sender, KeyPressEventArgs e)
        {
            // Приводим sender к типу TextBox
            Guna2TextBox currentTextBox = sender as Guna2TextBox;
            // Проверяем, является ли введенный символ русским или английской буквой 'o'
            if (IsCyrillicOrO(e.KeyChar))
            {
                e.Handled = true; // Запрещаем ввод
                FlashTextBox(currentTextBox); // Вызываем метод для моргания
            }
        }
        // запрет на русский
        private bool IsCyrillic(char c)
        {
            // Проверяем, находится ли символ в диапазоне кириллических символов
            return (c >= 0x0400 && c <= 0x04FF);
        }

        // запрет на английский
        private bool IsLatin(char c)
        {
            // Проверяем, находится ли символ в диапазоне английских букв
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }

        private bool IsCyrillicOrO(char c)
        {
            // Проверяем на кириллицу или букву 'o' (как заглавную, так и строчную)
            return (c >= 0x0400 && c <= 0x04FF) || c == 'o' || c == 'O';
        }
        int deley = 200;
        private async void FlashTextBox(Guna2TextBox textBox)
        {
            // Сохраняем исходный цвет фона
            var originalColor = Color.White;

            // Устанавливаем красный цвет фона
            textBox.FillColor = System.Drawing.Color.Red;

            // Ждем 100 миллисекунд
            await Task.Delay(deley);

            // Возвращаем к оригинальному цвету
            textBox.FillColor = originalColor;

            // Ждем 100 миллисекунд
            await Task.Delay(deley);

            // Снова устанавливаем красный цвет фона
            textBox.FillColor = System.Drawing.Color.Red;

            // Ждем 100 миллисекунд
            await Task.Delay(deley);

            // Возвращаем к оригинальному цвету
            textBox.FillColor = originalColor;
        }



        #region Button
        LoadEuropeanDocument loadEuropeanDocument = null;
        //Кнопка открытия списка документов
        private void LoadEuropeanDocuments_Click(object sender, EventArgs e)
        {
            if (loadEuropeanDocument == null)
            {
                loadEuropeanDocument = new LoadEuropeanDocument(GetDataLoad, filePathCertificate);
                loadEuropeanDocument.ShowDialog();
            }
            else
            {
                loadEuropeanDocument.ShowDialog();
            }
        }

        // Кнопка перевода
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            Translater();
        }

        // Кнопка печати наклейки
        private void ToolStripMenuItemPrintSticker_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItemPrintSticker_Click(sender, e);
        }

        // Кнопка печати сертификата
        private void ToolStripMenuItemPrintCertificate_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemPrintCertificate_Click_1(sender, e);
        }

        // Кнопка перезаписи данных
        private void ToolStripMenuItemResetData_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItemResetData_Click(sender, e);
        }

        // Кнопка сохранения        
        private void ToolStripMenuItemSaveData_Click_1(object sender, EventArgs e)
        {
            SaveButton();
        }

        #endregion
    }
}
