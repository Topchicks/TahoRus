using Microsoft.Office.Interop.Word;
using Npgsql;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace TaxoNavicon
{
    public partial class EuropeanTypeForm : Form
    {
        PoleDataEuropean poleDataEuropean;
        private PrintDocument printDocument;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;
        public EuropeanTypeForm()
        {
            InitializeComponent();
            poleDataEuropean = new PoleDataEuropean();
        }

        private void SetData()
        {
            //Order
            poleDataEuropean.orderNumber = (int)numericUpDowntextBoxOrderNumber.Value;// номер заказа
            poleDataEuropean.master = comboBoxMaster.Text; // мастер


            //Customer
            poleDataEuropean.nameCustomer = textBoxNameCustomer.Text; // имя русском
            poleDataEuropean.nameCustomerEng = toolStripMenuItemLoadEuropeanDocument.Text; // имя на английском
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
            poleDataEuropean.modelTachograph = textBoxModelTachograph.Text; // модель тахографа

            poleDataEuropean.l = textBoxL.Text;
            poleDataEuropean.w = textBoxW.Text;
            poleDataEuropean.k = textBoxK.Text;

            poleDataEuropean.dataJob = dateTimePickerJob.Value.ToShortDateString();//  время выполнения работ
        }

        private void ToolStripMenuItemPrintCertificate_Click(object sender, EventArgs e)
        {

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
            FindAndReplace(wordDoc, "<producedTachograph>", poleDataEuropean.producedTachograph);

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

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadEuropeanDocument loadEuropeanDocument = new LoadEuropeanDocument(GetDataLoad);

            loadEuropeanDocument.Show();
        }

        private void SqlConnection()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(sql);

            string connectionString = "Host=localhost;Username=postgres;Password=123;Database=Certificate";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                // Создание команды на вставку данных
                string insertQuery = "INSERT INTO \"RussianCertificate\" " +
                    "(номерЗаказа,мастер,датаВыполнениеРабот,датаВыполнениеНовыхРабот,имяКлиента,адресКлиента,маркаТранспорта," +
                    "модельТранспорта,винТранспорта,регНомерТранспорта, маркировкаШинТранспорта,одометрТранспорта," +
                    "производительТахографа,серийныйНомерТахографа,модельТахографа,датаПроизводстваТахографа," +
                    "расположениеУстановочнойТаблицы,результатИнспекции,признакиМанипуляции,особыеОтметки) " +
                    "VALUES " +
                    "(@номерЗаказа,@мастер,@датаВыполнениеРабот,@датаВыполнениеНовыхРабот,@имяКлиента,@адресКлиента,@маркаТранспорта," +
                    "@модельТранспорта,@винТранспорта,@регНомерТранспорта, @маркировкаШинТранспорта,@одометрТранспорта," +
                    "@производительТахографа,@серийныйНомерТахографа,@модельТахографа,@датаПроизводстваТахографа," +
                    "@расположениеУстановочнойТаблицы,@результатИнспекции,@признакиМанипуляции,@особыеОтметки)";

                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    // Добавление параметров
                    command.Parameters.AddWithValue("@номерЗаказа", poleDataEuropean.orderNumber);
                    command.Parameters.AddWithValue("@мастер", poleDataEuropean.master);
                    command.Parameters.AddWithValue("@датаВыполнениеРабот", poleDataEuropean.dataJob);
                    command.Parameters.AddWithValue("@датаВыполнениеНовыхРабот", poleDataEuropean.newDataJob); /// удалить
                    command.Parameters.AddWithValue("@имяКлиента", poleDataEuropean.nameCustomer);
                    command.Parameters.AddWithValue("@адресКлиента", poleDataEuropean.adresCustomer);
                    command.Parameters.AddWithValue("@маркаТранспорта", poleDataEuropean.manufacturerVehicle); /// редактировать
                    command.Parameters.AddWithValue("@модельТранспорта", poleDataEuropean.modelVehicle);
                    command.Parameters.AddWithValue("@винТранспорта", poleDataEuropean.vinVehicle);
                    command.Parameters.AddWithValue("@регНомерТранспорта", poleDataEuropean.registrationNumberVehicle);
                    command.Parameters.AddWithValue("@маркировкаШинТранспорта", poleDataEuropean.tireMarkingsVehicle);
                    command.Parameters.AddWithValue("@одометрТранспорта", poleDataEuropean.odometerKmVehicle);
                    command.Parameters.AddWithValue("@производительТахографа", poleDataEuropean.manufacturerTahograph);
                    command.Parameters.AddWithValue("@серийныйНомерТахографа", poleDataEuropean.serialNumberTahograph);
                    command.Parameters.AddWithValue("@модельТахографа", poleDataEuropean.modelTachograph);
                    command.Parameters.AddWithValue("@датаПроизводстваТахографа", poleDataEuropean.modelTachograph);
                    command.Parameters.AddWithValue("@расположениеУстановочнойТаблицы", poleDataEuropean.locationInstallationTable); // Удалить
                    command.Parameters.AddWithValue("@результатИнспекции", poleDataEuropean.inspectionResult); // Удалить
                    command.Parameters.AddWithValue("@признакиМанипуляции", poleDataEuropean.signsManipulation); // Удалить
                    command.Parameters.AddWithValue("@особыеОтметки", poleDataEuropean.specialMarks); // Удалить

                    // Открываем соединение
                    connection.Open();

                    // Выполняем команду
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Метод который будет принимать параметры из окна загрузок
        /// </summary>
        public void GetDataLoad(int orderNumber,
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

            textBoxL.Text = l;
            textBoxW.Text = w;
            textBoxK.Text = k;

            dateTimePickerJob.Value = DateTime.Parse(dataJob); // Установка значения в DateTimePicker

            textBoxLocationInstallationTable.Text = locationInstallationTable;
            comboBoxInspectionResult.Text = inspectionResult;
            comboBoxSignsManipulation.Text = signsManipulation;
            textBoxSpecialMarks.Text = specialMarks;
        }

        private void ToolStripMenuItemSaveData_Click(object sender, EventArgs e)
        {
            SqlConnection();
        }
    }
}
