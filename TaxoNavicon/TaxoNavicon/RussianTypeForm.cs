using Microsoft.Office.Interop.Word;
using Npgsql;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
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
        private PrintDocument printDocument;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;
        public RussianTypeForm()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Или LicenseContext.Commercial, если у вас коммерческая лицензия
            poleDataRussian = new PoleDataRussian();
        }


        // Открытие окна загрузок
        private void toolStripMenuItemLoadRussianDocument_Click(object sender, System.EventArgs e)
        {
            LoadData();
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
            
            string relativePath = @"RussianCertificate.docx"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();
            dateTimePickerJob.CustomFormat = "dd/MM/yyyy"; // Устанавливаем только дату

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

        // Тут происходит сохранение в переменные программы
        private void ToolStripMenuItemSetData_Click(object sender, EventArgs e)
        {
            SetData();
        }

        // Тут происходит сохранение в базу данных
        private void toolStripMenuItemSaveData_Click(object sender, EventArgs e)
        {
            SetData();
            var filePath = "C:/Users/TGS_Navicon/Desktop/Git/TahoRus/TaxoNavicon/Certificate.xlsx"; // Укажите путь к вашему существующему файлу.xlsx"; // Укажите путь к вашему существующему файлу
            FileInfo existingFile = new FileInfo(filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(existingFile))
            {
                // Получаем существующий лист или создаем новый, если его нет
                var worksheet = excelPackage.Workbook.Worksheets["RussianCertificate"] ?? excelPackage.Workbook.Worksheets.Add("RussianCertificate");

                // Находим первую пустую строку, чтобы добавить данные
                int row = worksheet.Dimension?.End.Row + 1 ?? 1;
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
            var filePath = "C:/Users/TGS_Navicon/Desktop/Git/TahoRus/TaxoNavicon/Certificate.xlsx"; // Укажите путь к вашему существующему файлу.xlsx
            FileInfo existingFile = new FileInfo(filePath);

            using (ExcelPackage excelPackage = new ExcelPackage(existingFile))
            {
                // Получаем существующий лист
                var worksheet = excelPackage.Workbook.Worksheets["RussianCertificate"];
                if (worksheet == null)
                {
                    MessageBox.Show("Лист 'RussianCertificate' не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Проходим по всем строкам, начиная со второй (первая - заголовки)
                for (int row = 3; row <= worksheet.Dimension.End.Row; row++)
                {
                    // Проверка на пустую ячейку в первом столбце
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                    {
                        Console.WriteLine("Пустая строчка выход");

                        break; // Выход из цикла, если ячейка пустая
                    }
                    // Пример загрузки данных в поля (предполагается, что у вас есть соответствующие свойства)
                    string order = worksheet.Cells[row, 1].Text;
                    Console.WriteLine("Номер заявки" + order);
                    poleDataRussian.orderNumber = Convert.ToInt32(order);
                    poleDataRussian.master = worksheet.Cells[row, 2].Text;
                    poleDataRussian.dataJob = worksheet.Cells[row, 3].Text;
                    poleDataRussian.newDataJob = worksheet.Cells[row, 4].Text;

                    poleDataRussian.nameCustomer = worksheet.Cells[row, 5].Text;
                    poleDataRussian.adresCustomer = worksheet.Cells[row, 6].Text;

                    poleDataRussian.manufacturerTahograph = worksheet.Cells[row, 7].Text;
                    poleDataRussian.serialNumberTahograph = worksheet.Cells[row, 8].Text;
                    poleDataRussian.modelTachograph = worksheet.Cells[row, 9].Text;
                    poleDataRussian.producedTachograph = worksheet.Cells[row, 10].Text;

                    poleDataRussian.markaVehicle = worksheet.Cells[row, 11].Text;
                    poleDataRussian.vinVehicle = worksheet.Cells[row, 12].Text;
                    poleDataRussian.tireMarkingsVehicle = worksheet.Cells[row, 13].Text;
                    poleDataRussian.modelVehicle = worksheet.Cells[row, 14].Text;
                    poleDataRussian.registrationNumberVehicle = worksheet.Cells[row, 15].Text;
                    poleDataRussian.odometerKmVehicle = worksheet.Cells[row, 16].Text;

                    poleDataRussian.w = worksheet.Cells[row, 17].Text;
                    poleDataRussian.k = worksheet.Cells[row, 18].Text;
                    poleDataRussian.l = worksheet.Cells[row, 19].Text;

                    poleDataRussian.locationInstallationTable = worksheet.Cells[row, 20].Text;
                    poleDataRussian.inspectionResult = worksheet.Cells[row, 21].Text;
                    poleDataRussian.signsManipulation = worksheet.Cells[row, 22].Text;
                    poleDataRussian.specialMarks = worksheet.Cells[row, 23].Text;
                }
            }
        }

        /// <summary>
        /// Метод который будет принимать параметры из окна загрузок
        /// </summary>
        public void GetDataLoad(
                                int orderNumber,
                                string master,
                                string dataJob,

                                string nameCustomer,
                                string adresCustomer,

                                string markaVehicle,
                                string modelVehicle,
                                string vinVehicle,
                                string registrationNumberVehicle,
                                string tireMarkingsVehicle,
                                string odometerKmVehicle,

                                string manufacturerTahograph,
                                string serialNumberTahograph,
                                string modelTachograph,
                                string producedTachograph,

                                string locationInstallationTable,
                                string inspectionResult,
                                string signsManipulation,
                                string specialMarks,

                                string l,
                                string w,
                                string k
                                )
        {
            //Order - заказ
            numericUpDowntextBoxOrderNumber.Value = orderNumber;// номер заказа
            comboBoxMaster.Text = master; // мастер
            dateTimePickerJob.Value = DateTime.Parse(dataJob); // Установка значения в DateTimePicker

            //Customer - заказчик
            textBoxNameCustomer.Text = nameCustomer; // имя русском
            textBoxAdresCustomer.Text = adresCustomer;// адрес заказчика

            //Vehicle - транспорт
            textBoxMarkaVehicle.Text = markaVehicle; // марка машины
            textBoxModelVehicle.Text = modelVehicle; // модель машины
            textBoxVinNumberVehicle.Text = vinVehicle; // вин номер машины
            textBoxRegistrationNumberVehicle.Text = registrationNumberVehicle; // рег. номер машины
            textBoxOdometerKmVehicle.Text = odometerKmVehicle; // одометр км
            textBoxTireMarkingsVehicle.Text = tireMarkingsVehicle;// маркировка шин

            //Tahograf - тахограф
            textBoxManufacturerTachograph.Text = manufacturerTahograph; // производитель
            textBoxModelTachograph.Text = modelTachograph; // модель тахографа
            textBoxProducedTachograph.Text = producedTachograph; // год производства
            textBoxSerialNumberTahograph.Text = serialNumberTahograph; // год производства

            

            textBoxLocationInstallationTable.Text = locationInstallationTable;
            comboBoxInspectionResult.Text = inspectionResult;
            comboBoxSignsManipulation.Text = signsManipulation;
            textBoxSpecialMarks.Text = specialMarks;

            textBoxL.Text = l;
            textBoxW.Text = w;
            textBoxK.Text = k;

            // Подгружаем наши данные в переменные экземпляра то есть локально
            SetData();
        }
    }
}
