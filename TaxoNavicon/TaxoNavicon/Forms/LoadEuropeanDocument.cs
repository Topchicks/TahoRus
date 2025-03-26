using Guna.UI2.WinForms;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
        private Dictionary<int, string> data = new Dictionary<int, string>();
        private Button buttonLoad;
        private PoleDataEuropean poleDataEuropean;
        private string filePathCertificate;

        // Делегат для метода
        public delegate void MyDelegate(/*int orderNumber,
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
                                        string k*/ PoleDataEuropean poleDataEuropean
                                        );
        private MyDelegate _myMethod;
        public LoadEuropeanDocument(MyDelegate myMethod, string filePathCertificate)
        {
            InitializeComponent();
            this.filePathCertificate = filePathCertificate;
            poleDataEuropean = new PoleDataEuropean();
            _myMethod = myMethod; // Сохраняем ссылку на метод
            FileInfo existingFile = new FileInfo(this.filePathCertificate);

            using (ExcelPackage excelPackage = new ExcelPackage(existingFile))
            {
                // Получаем существующий лист
                var worksheet = excelPackage.Workbook.Worksheets["EuropeanCertificate"];
                if (worksheet == null)
                {
                    MessageBox.Show("Лист 'EuropeanCertificate' не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    data.Add(Convert.ToInt32(order), worksheet.Cells[row, 4].Text);
                }
            }
            CreatePanelsForLastEntries();
        }
        private void CreatPanel(int numberOrderText, string nameCustomerText)
        {
            // Создание панели
            Guna2Panel panel = new Guna2Panel
            {
                BorderColor = Color.FromArgb(24, 175, 240),
                BorderRadius = 6,
                BorderThickness = 1,
                FillColor = Color.White,
                Size = new System.Drawing.Size(381, 34)
            };

            // Создание первого текстового элемента
            Label orderNumber = new Label
            {
                Text = numberOrderText.ToString(),
                AutoSize = false, // Автоматическая подстройка размера
                Location = new System.Drawing.Point(-3, 0), // Положение на панели
                Size = new System.Drawing.Size(100, 34),
                Font = new Font("Arial", 12f),
                TextAlign = ContentAlignment.MiddleCenter, // Выравнивание текста по центру
                BackColor = Color.Transparent,
            };

            // Создание второго текстового элемента
            Label nameCustomer = new Label
            {
                Text = nameCustomerText,
                AutoSize = false,
                Location = new System.Drawing.Point(103, 0), // Положение на панели
                Size = new System.Drawing.Size(159, 34),
                Font = new Font("Arial", 12f),
                TextAlign = ContentAlignment.MiddleCenter, // Выравнивание текста по центру
                BackColor = Color.Transparent,
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
                BackColor = Color.Transparent,

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
            using (ExcelPackage excelPackage = new ExcelPackage(filePathCertificate))
            {
                FileInfo existingFile = new FileInfo(filePathCertificate);
                var worksheet = excelPackage.Workbook.Worksheets["EuropeanCertificate"];
                if (worksheet == null)
                {
                    MessageBox.Show("Лист 'EuropeanCertificate' не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Проверим есть ли такой ключ в дикшенери
                if (data.ContainsKey(orderNumber))
                {
                    // То запускаем цикл на поиск данных в нашей таблице
                    for (int row = 3; row <= worksheet.Dimension.End.Row; row++)
                    {
                        // Проверка на пустую ячейку в первом столбце
                        if (string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                        {
                            Console.WriteLine("Пустая строчка выход");

                            break; // Выход из цикла, если ячейка пустая
                        }

                        string order = worksheet.Cells[row, 1].Text;

                        // Тут проверка этого столбика и если такие данные есть то запускаем процесс записи данных в poleDataRussian
                        if (Convert.ToInt32(order) == orderNumber)
                        {
                            poleDataEuropean.orderNumber = Convert.ToInt32(order);
                            poleDataEuropean.master = worksheet.Cells[row, 2].Text;
                            poleDataEuropean.dataJob = worksheet.Cells[row, 3].Text;
                            
                            poleDataEuropean.nameCustomer = worksheet.Cells[row, 4].Text;
                            poleDataEuropean.nameCustomerEng = worksheet.Cells[row, 5].Text;
                            poleDataEuropean.adresCustomer = worksheet.Cells[row, 6].Text;
                            
                            poleDataEuropean.manufacturerTahograph = worksheet.Cells[row, 7].Text;
                            poleDataEuropean.serialNumberTahograph = worksheet.Cells[row, 8].Text;
                            poleDataEuropean.modelTachograph = worksheet.Cells[row, 9].Text;

                            
                            poleDataEuropean.manufacturerVehicle = worksheet.Cells[row, 10].Text;
                            poleDataEuropean.vinVehicle = worksheet.Cells[row, 11].Text;
                            poleDataEuropean.tireMarkingsVehicle = worksheet.Cells[row, 12].Text;
                            poleDataEuropean.modelVehicle = worksheet.Cells[row, 13].Text;
                            poleDataEuropean.yearOfIssueVehiccle = worksheet.Cells[row, 14].Text;
                            poleDataEuropean.registrationNumberVehicle = worksheet.Cells[row, 15].Text;
                            poleDataEuropean.odometerKmVehicle = worksheet.Cells[row, 16].Text;
                            
                            poleDataEuropean.w = worksheet.Cells[row, 17].Text;
                            poleDataEuropean.k = worksheet.Cells[row, 18].Text;
                            poleDataEuropean.l = worksheet.Cells[row, 19].Text;

                            poleDataEuropean.adresCustomerEng = worksheet.Cells[row, 20].Text;
                            poleDataEuropean.temperature = worksheet.Cells[row, 21].Text;
                            poleDataEuropean.protectore = worksheet.Cells[row, 22].Text;
                        }
                    }
                }
            }

            // Тут закрываем окно и запускается процесс передачи данных на другое окно
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
           
            if(poleDataEuropean.master != null)
            {
                _myMethod?.Invoke(poleDataEuropean);
                //Console.WriteLine("Окно закрыто с возвратом данных");
            }
            else
            {
                Console.WriteLine("Окно закрыто без возврата данных");
            }
            base.OnFormClosing(e);
        }

        // Тут когда вводим данные будем сортировать и выводить все заявки которые есть с таким номером заказа
        private void textBoxSearchOrder_TextChanged(object sender, EventArgs e)
        {
            startOrderPanel.Controls.Clear();
            if(textBoxSearchOrder.Text != "")
            {
                int order = Convert.ToInt32(textBoxSearchOrder.Text);

                foreach (var idorder in data)
                {
                    if (order == idorder.Key)
                    {
                        CreatPanel(idorder.Key, idorder.Value);
                    }
                    
                }
            }
            else
            {
                CreatePanelsForLastEntries();
            }
        }


        // Доделать сделать болье нормальное воявление панелей
        private  void CreatePanelsForLastEntries()
        {
            // Получаем последние 10 элементов
            var lastEntries = data.Reverse().Take(50);

            foreach (var kvp in lastEntries)
            {
                CreatPanel(kvp.Key, kvp.Value);
            }
        }
    }
}
