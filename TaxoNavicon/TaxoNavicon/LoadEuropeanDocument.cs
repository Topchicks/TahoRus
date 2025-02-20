using Npgsql;
using RestSharp;
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
        серийныйНомерТахографаЕвропа
        модельТахографа

        l
        w
        k
    */
    public partial class LoadEuropeanDocument : Form
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();
        private Button buttonLoad;
        private PoleDataEuropean poleDataEuropean;

        // Делегат для метода
        public delegate void MyDelegate(int orderNumber,
                                        string master,
                                        string dataJob,

                                        string nameCustomer,
                                        string nameCustomerEng,
                                        string adresCustomer,

                                        string markaVehicle,
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
                                        );
        private MyDelegate _myMethod;
        public LoadEuropeanDocument(MyDelegate myMethod)
        {
            InitializeComponent();
            _myMethod = myMethod; // Сохраняем ссылку на метод
        }

        private void LoadEuropeanDocument_Load(object sender, EventArgs e)
        {

            string connectionString = "Host=localhost;Username=postgres;Password=123;Database=Certificate";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                // Открываем соединение
                connection.Open();

                // Создаем запрос
                string selectQuery = "SELECT номерЗаказа, имяКлиента FROM \"EuropeanCertificate\"";

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    // Выполняем запрос и получаем данные
                    using (var reader = command.ExecuteReader())
                    {
                        // Читаем данные
                        while (reader.Read())
                        {
                            // Получаем значения столбцов
                            var orderNumber = reader["номерЗаказа"].ToString();
                            var nameCustomer = reader["имяКлиента"].ToString();
                            data.Add(orderNumber, nameCustomer);
                            // Выводим данные (или обрабатываем их как нужно)
                            Console.WriteLine($"Номер Заказа: {orderNumber}, Имя Клиента: {nameCustomer}");
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
                int orderNumber = Convert.ToInt32(clickedButton.Tag);
                ShowMessage(orderNumber);
            }
        }

        // Метод для отображения сообщения
        private void ShowMessage(int orderNumber)
        {
            LoadDataByOrderNumber(orderNumber);
        }

        private void LoadDataByOrderNumber(int orderNumber)
        {
            poleDataEuropean = null;
            poleDataEuropean = new PoleDataEuropean();
            string connectionString = "Host=localhost;Username=postgres;Password=123;Database=Certificate";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                
                    // Создание команды на выборку данных по номеру заказа
                    string selectQuery = "SELECT * FROM \"EuropeanCertificate\" WHERE \"номерЗаказа\" = @номерЗаказа";

                    using (var selectCommand = new NpgsqlCommand(selectQuery, connection))
                    {
                        // Открываем соединение
                        connection.Open();
                        selectCommand.Parameters.AddWithValue("@номерЗаказа", orderNumber);
                       

                        using (var reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read()) // Если есть результаты
                            {
                                // Получаем значения столбцов и сохраняем их в переменные
                                poleDataEuropean.orderNumber = Convert.ToInt32(reader["номерЗаказа"]);
                                poleDataEuropean.master = reader["мастер"].ToString();
                                poleDataEuropean.dataJob = reader["датаВыполненияРабот"].ToString();

                                poleDataEuropean.nameCustomer = reader["имяКлиента"].ToString();
                                poleDataEuropean.nameCustomerEng = reader["имяКлиентаАнлийский"].ToString();
                                poleDataEuropean.adresCustomer = reader["адресЗаказчика"].ToString();

                                poleDataEuropean.manufacturerVehicle = reader["производительТранспорта"].ToString();
                                poleDataEuropean.modelVehicle = reader["модельТранспорта"].ToString();
                                poleDataEuropean.vinVehicle = reader["винНомерТранспорта"].ToString();
                                poleDataEuropean.registrationNumberVehicle = reader["регНомерТранспорта"].ToString();
                                poleDataEuropean.tireMarkingsVehicle = reader["маркировкаШин"].ToString();
                                poleDataEuropean.odometerKmVehicle = reader["одометрКм"].ToString();
                                poleDataEuropean.yearOfIssueVehiccle = reader["годВыпуска"].ToString();

                                poleDataEuropean.manufacturerTahograph = reader["производительТахографа"].ToString();
                                poleDataEuropean.serialNumberTahograph = reader["серийныйНомерТахографа"].ToString();
                                poleDataEuropean.modelTachograph = reader["модельТахографа"].ToString();

                                poleDataEuropean.w = reader["w"].ToString();
                                poleDataEuropean.l = reader["l"].ToString();
                                poleDataEuropean.k = reader["k"].ToString();
                                
                        }
                    }
                    }
                    connection.Close();
            }
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Console.WriteLine("Окно закрыто");

            _myMethod?.Invoke(poleDataEuropean.orderNumber,
                                poleDataEuropean.master,
                                poleDataEuropean.dataJob,
                                poleDataEuropean.nameCustomer,
                                poleDataEuropean.nameCustomerEng,
                                poleDataEuropean.adresCustomer,
                                poleDataEuropean.manufacturerVehicle,
                                poleDataEuropean.modelVehicle,
                                poleDataEuropean.vinVehicle,
                                poleDataEuropean.yearOfIssueVehiccle,
                                poleDataEuropean.registrationNumberVehicle,
                                poleDataEuropean.tireMarkingsVehicle,
                                poleDataEuropean.odometerKmVehicle,
                                poleDataEuropean.manufacturerTahograph,
                                poleDataEuropean.serialNumberTahograph,
                                poleDataEuropean.modelTachograph,
                                poleDataEuropean.l,
                                poleDataEuropean.w,
                                poleDataEuropean.k
                             );
            base.OnFormClosing(e);
        }

    }
}
