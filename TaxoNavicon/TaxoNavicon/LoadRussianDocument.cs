using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxoNaviconRussian;

namespace TaxoNavicon
{
    public partial class LoadRussianDocument : Form
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();
        private Button buttonLoad;
        private PoleDataRussian poleDataRussian;

        // Делегат для метода
        public delegate void MyDelegate(int orderNumber,
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
                                        );
        private MyDelegate _myMethod;

        public LoadRussianDocument(MyDelegate myMethod)
        {
            InitializeComponent();
            _myMethod = myMethod; // Сохраняем ссылку на метод
            
        }

        private void LoadRussianDocument_Load(object sender, EventArgs e)
        {
            
            string connectionString = "Host=localhost;Username=postgres;Password=123;Database=Certificate";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                // Открываем соединение
                connection.Open();

                // Создаем запрос
                string selectQuery = "SELECT номерЗаказа, имяКлиента FROM \"RussianCertificate\"";

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    // Выполняем запрос и получаем данные
                    using (var reader = command.ExecuteReader())
                    {
                        // Читаем данные
                        while (reader.Read())
                        {
                            // Получаем значения столбцов
                            var номерЗаказа = reader["номерЗаказа"].ToString();
                            var имяКлиента = reader["имяКлиента"].ToString();
                            data.Add(номерЗаказа, имяКлиента);
                            // Выводим данные (или обрабатываем их как нужно)
                            Console.WriteLine($"Номер Заказа: {номерЗаказа}, Имя Клиента: {имяКлиента}");
                        }
                    }
                }
                connection.Close();
            }

            // Пример вывода значений из словаря
            foreach (var kvp in data)
            {
                CreatPanel(kvp.Key, kvp.Value);
            }
            // удаляем мусор
            data.Clear();
        }

        private void CreatPanel(string numberOrderText, string nameCustomerText)
        {
            // Создание панели
            Panel panel = new Panel
            {
                BackColor = System.Drawing.Color.Gray, // Цвет фона панели
                Size = new System.Drawing.Size(381, 34)
            };

            // Создание первого текстового элемента
            Label orderNumber = new Label
            {
                Text = numberOrderText,
                AutoSize = false, // Автоматическая подстройка размера
                Location = new System.Drawing.Point(-3, 0), // Положение на панели
                Size = new System.Drawing.Size(100, 34),
                Font = new Font("Arial", 12f),
                TextAlign = ContentAlignment.MiddleCenter // Выравнивание текста по центру
            };

            // Создание второго текстового элемента
            Label nameCustomer = new Label
            {
                Text = nameCustomerText,
                AutoSize = false,
                Location = new System.Drawing.Point(103, 0), // Положение на панели
                Size = new System.Drawing.Size(159, 34),
                Font = new Font("Arial", 12f),
                TextAlign = ContentAlignment.MiddleCenter // Выравнивание текста по центру
            };

            buttonLoad = new Button
            {
                Text = "Загрузить",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter, // Выравнивание текста по центру
                Font = new Font("Arial", 12f),
                Location = new System.Drawing.Point(277, 0), // Положение на панели
                Size = new System.Drawing.Size(104, 34),
                BackgroundImageLayout = ImageLayout.Tile,
                FlatStyle = FlatStyle.Flat,

            };
            buttonLoad.Tag = orderNumber.Text;
            // Подписка на событие Click
            buttonLoad.Click += Button_Click;

            // Добавление текстовых элементов на панель
            panel.Controls.Add(buttonLoad);
            panel.Controls.Add(orderNumber);
            panel.Controls.Add(nameCustomer);

            // Добавление панели на форму
            startOrderPanel.Controls.Add(panel);
        }

        // Обработчик события Click
        private void Button_Click(object sender, EventArgs e)
        {
            // Получаем кнопку, которая была нажата
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Передаем данные в метод
                string orderNumber = clickedButton.Tag.ToString();
                ShowMessage(orderNumber);
            }
        }

        // Метод для отображения сообщения
        private void ShowMessage(string orderNumber)
        {
            LoadDataByOrderNumber(orderNumber);
        }

        private void LoadDataByOrderNumber(string orderNumber)
        {
            poleDataRussian = new PoleDataRussian();
            string connectionString = "Host=localhost;Username=postgres;Password=123;Database=Certificate";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                // Открываем соединение
                connection.Open();

                // Создание команды на вставку данных
                string insertQuery = "INSERT INTO \"RussianCertificate\" " +
                    "(номерЗаказа, мастер, датаВыполнениеРабот, датаВыполнениеНовыхРабот, имяКлиента, адресКлиента, маркаТранспорта, " +
                    "модельТранспорта, винТранспорта, регНомерТранспорта, маркировкаШинТранспорта, одометрТранспорта, " +
                    "производительТахографа, серийныйНомерТахографа, модельТахографа, датаПроизводстваТахографа, " +
                    "расположениеУстановочнойТаблицы, результатИнспекции, признакиМанипуляции, особыеОтметки) " +
                    "VALUES " +
                    "(@номерЗаказа, @мастер, @датаВыполнениеРабот, @датаВыполнениеНовыхРабот, @имяКлиента, @адресКлиента, @маркаТранспорта, " +
                    "@модельТранспорта, @винТранспорта, @регНомерТранспорта, @маркировкаШинТранспорта, @одометрТранспорта, " +
                    "@производительТахографа, @серийныйНомерТахографа, @модельТахографа, @датаПроизводстваТахографа, " +
                    "@расположениеУстановочнойТаблицы, @результатИнспекции, @признакиМанипуляции, @особыеОтметки)";

                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    // Создание команды на выборку данных по номеру заказа
                    string selectQuery = "SELECT * FROM \"RussianCertificate\" WHERE \"номерЗаказа\" = @номерЗаказа";

                    using (var selectCommand = new NpgsqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@номерЗаказа", orderNumber);

                        using (var reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read()) // Если есть результаты
                            {
                                // Получаем значения столбцов и сохраняем их в переменные
                                poleDataRussian.orderNumber = Convert.ToInt32(reader["номерЗаказа"]);
                                poleDataRussian.master = reader["мастер"].ToString();
                                poleDataRussian.dataJob = reader["датаВыполнениеРабот"].ToString();
                                poleDataRussian.newDataJob = reader["датаВыполнениеНовыхРабот"].ToString();
                                poleDataRussian.nameCustomer = reader["имяКлиента"].ToString();
                                poleDataRussian.adresCustomer = reader["адресКлиента"].ToString();
                                poleDataRussian.markaVehicle = reader["маркаТранспорта"].ToString();
                                poleDataRussian.modelVehicle = reader["модельТранспорта"].ToString();
                                poleDataRussian.vinVehicle = reader["винТранспорта"].ToString();
                                poleDataRussian.registrationNumberVehicle = reader["регНомерТранспорта"].ToString();
                                poleDataRussian.tireMarkingsVehicle = reader["маркировкаШинТранспорта"].ToString();
                                poleDataRussian.odometerKmVehicle = reader["одометрТранспорта"].ToString();
                                poleDataRussian.manufacturerTahograph = reader["производительТахографа"].ToString();
                                poleDataRussian.serialNumberTahograph = reader["серийныйНомерТахографа"].ToString();
                                poleDataRussian.modelTachograph = reader["модельТахографа"].ToString();
                                poleDataRussian.producedTachograph = reader["датаПроизводстваТахографа"].ToString();
                                poleDataRussian.locationInstallationTable = reader["расположениеУстановочнойТаблицы"].ToString();
                                poleDataRussian.inspectionResult = reader["результатИнспекции"].ToString();
                                poleDataRussian.signsManipulation = reader["признакиМанипуляции"].ToString();
                                poleDataRussian.specialMarks = reader["особыеОтметки"].ToString();

   /*                             // Здесь вы можете использовать переменные по своему усмотрению
                                Console.WriteLine($"Номер Заказа: {poleDataRussian.orderNumber}, " +
                                                  $"Мастер: {poleDataRussian.master}, " +
                                                  $"Имя Клиента: {poleDataRussian.nameCustomer}" +
                                                  $"датаВыполнениеРабот: {poleDataRussian.dataJob}" + 
                                                  $"адресКлиента: {poleDataRussian.adresCustomer}");*/
                                // И так далее для остальных переменных...
                            }
                        }
                    }

                    connection.Close();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Console.WriteLine("Окно закрыто");
            
            _myMethod?.Invoke(  poleDataRussian.orderNumber,
                                poleDataRussian.master,
                                poleDataRussian.dataJob,
                                poleDataRussian.nameCustomer,
                                poleDataRussian.adresCustomer,
                                poleDataRussian.markaVehicle,
                                poleDataRussian.modelVehicle,
                                poleDataRussian.vinVehicle,
                                poleDataRussian.registrationNumberVehicle,
                                poleDataRussian.tireMarkingsVehicle,
                                poleDataRussian.odometerKmVehicle,
                                poleDataRussian.manufacturerTahograph,
                                poleDataRussian.serialNumberTahograph,
                                poleDataRussian.modelTachograph,
                                poleDataRussian.producedTachograph,
                                poleDataRussian.locationInstallationTable,
                                poleDataRussian.inspectionResult,
                                poleDataRussian.signsManipulation,
                                poleDataRussian.specialMarks,
                                poleDataRussian.l,
                                poleDataRussian.w,
                                poleDataRussian.k
                             );
            base.OnFormClosing(e);
        }

    }
}
