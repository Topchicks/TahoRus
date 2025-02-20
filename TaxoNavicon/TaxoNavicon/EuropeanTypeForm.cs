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

        // Тут происходит сохранение в переменные программы
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
            string connectionString = "Host=localhost;Username=postgres;Password=123;Database=Certificate";

            using (var connection = new NpgsqlConnection(connectionString))
            {   
                // Создание команды на вставку данных
                string insertQuery = "INSERT INTO \"EuropeanCertificate\" " +
            "(номерЗаказа,мастер,датаВыполненияРабот," +
            "имяКлиента,имяКлиентаАнлийский,адресЗаказчика," +
            "производительТранспорта,модельТранспорта,винНомерТранспорта," +
            "регНомерТранспорта,маркировкаШин,одометрКм,годВыпуска,производительТахографа," +
            "серийныйНомерТахографа,модельТахографа,l,w,k) " +
            "VALUES " +
            "(@номерЗаказа,@мастер,@датаВыполненияРабот," +
            "@имяКлиента,@имяКлиентаАнлийский,@адресЗаказчика," +
            "@производительТранспорта,@модельТранспорта,@винНомерТранспорта," +
            "@регНомерТранспорта,@маркировкаШин,@одометрКм,@годВыпуска,@производительТахографа," +
            "@серийныйНомерТахографа,@модельТахографа,@l,@w,@k)";

                using (var command = new NpgsqlCommand(insertQuery, connection))
                {

                    // Добавление параметров
                    command.Parameters.AddWithValue("@номерЗаказа", poleDataEuropean.orderNumber);
                    command.Parameters.AddWithValue("@мастер", poleDataEuropean.master);
                    command.Parameters.AddWithValue("@датаВыполненияРабот", poleDataEuropean.dataJob);
                                                     
                    command.Parameters.AddWithValue("@имяКлиента", poleDataEuropean.nameCustomer);
                    command.Parameters.AddWithValue("@имяКлиентаАнлийский", poleDataEuropean.nameCustomerEng);
                    command.Parameters.AddWithValue("@адресЗаказчика", poleDataEuropean.adresCustomer);
                                                     
                    command.Parameters.AddWithValue("@производительТранспорта",poleDataEuropean.manufacturerVehicle);
                    command.Parameters.AddWithValue("@модельТранспорта", poleDataEuropean.modelVehicle);
                    command.Parameters.AddWithValue("@винНомерТранспорта", poleDataEuropean.vinVehicle);
                    command.Parameters.AddWithValue("@регНомерТранспорта", poleDataEuropean.registrationNumberVehicle);
                    command.Parameters.AddWithValue("@маркировкаШин",poleDataEuropean.tireMarkingsVehicle);
                    command.Parameters.AddWithValue("@одометрКм", poleDataEuropean.odometerKmVehicle);
                    command.Parameters.AddWithValue("@годВыпуска", poleDataEuropean.yearOfIssueVehiccle);
                                                     
                    command.Parameters.AddWithValue("@производительТахографа", poleDataEuropean.manufacturerTahograph);
                    command.Parameters.AddWithValue("@серийныйНомерТахографа", poleDataEuropean.serialNumberTahograph);
                    command.Parameters.AddWithValue("@модельТахографа", poleDataEuropean.modelTachograph);
                                                     
                    command.Parameters.AddWithValue("@l", poleDataEuropean.l);
                    command.Parameters.AddWithValue("@k", poleDataEuropean.k);
                    command.Parameters.AddWithValue("@w", poleDataEuropean.w);

                    Console.WriteLine(poleDataEuropean.orderNumber.ToString());
                    Console.WriteLine(poleDataEuropean.master);
                    Console.WriteLine(poleDataEuropean.dataJob);
                    Console.WriteLine(poleDataEuropean.nameCustomer);
                    Console.WriteLine(poleDataEuropean.nameCustomerEng);
                    Console.WriteLine(poleDataEuropean.adresCustomer);
                    Console.WriteLine(poleDataEuropean.manufacturerVehicle);
                    Console.WriteLine(poleDataEuropean.modelVehicle);
                    Console.WriteLine(poleDataEuropean.vinVehicle);
                    Console.WriteLine(poleDataEuropean.registrationNumberVehicle);
                    Console.WriteLine(poleDataEuropean.tireMarkingsVehicle);
                    Console.WriteLine(poleDataEuropean.odometerKmVehicle);
                    Console.WriteLine(poleDataEuropean.yearOfIssueVehiccle);
                    Console.WriteLine(poleDataEuropean.manufacturerTahograph);
                    Console.WriteLine(poleDataEuropean.serialNumberTahograph);
                    Console.WriteLine(poleDataEuropean.modelTachograph);
                    Console.WriteLine(poleDataEuropean.l);
                    Console.WriteLine(poleDataEuropean.k);
                    Console.WriteLine(poleDataEuropean.w);
                // Открываем соединение
                connection.Open();
                    // Выполняем команду
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно сохранены в базе данных.");
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
                        Console.WriteLine(ex.Message);
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Метод который будет принимать параметры из окна загрузок
        /// </summary>
        public void GetDataLoad(int orderNumber,
                                string master,
                                string dataJob,

                                string nameCustomer,
                                string nameCustomerEng,
                                string adresCustomer,

                                string manufacturerVehicle,

                                string modelVehicle,
                                string vinVehicle,
                                string registrationNumberVehicle,

                                string tireMarkingsVehicle,
                                string odometerKmVehicle,
                                string yearOfIssueVehiccle,

                                string manufacturerTahograph,
                                string serialNumberTahograph,
                                string modelTachograph,

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
            textBoxNameCustomerEng.Text = nameCustomerEng; // имя английский
            textBoxAdresCustomer.Text = adresCustomer;// адрес заказчика

            //Vehicle - транспорт
            textBoxManufacturerVehicle.Text = manufacturerVehicle; // Производитель машины
            textBoxModelVehicle.Text = modelVehicle; // модель машины
            textBoxVinNumberVehicle.Text = vinVehicle; // вин номер машины
            
            textBoxOdometerKmVehicle.Text = odometerKmVehicle; // одометр км
            textBoxTireMarkingsVehicle.Text = tireMarkingsVehicle;// маркировка шин
            textBoxYearOfIssueVehiccle.Text = yearOfIssueVehiccle;// маркировка шин

            //Tahograf - тахограф
            textBoxManufacturerTachograph.Text = manufacturerTahograph; // производитель
            textBoxModelTachograph.Text = modelTachograph; // модель тахографа
            textBoxSerialNumberTahograph.Text = serialNumberTahograph; // год производства

            textBoxL.Text = l;
            textBoxW.Text = w;
            textBoxK.Text = k;

            textBoxRegistrationNumberVehicle.Text = registrationNumberVehicle; // рег. номер машины

            // Подгружаем наши данные в переменные экземпляра то есть локально
            SetData();
        }


        // Тут происходит сохранение в базу данных
        private void ToolStripMenuItemSaveData_Click(object sender, EventArgs e)
        {
            SetData();
            SqlConnection();
        }
    }
}
