using Microsoft.Office.Interop.Word;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TaxoNaviconRussian;
using Font = System.Drawing.Font;

namespace TaxoNavicon
{
    public partial class LoadRussianDocument : Form
    {
        private Dictionary<int, string> data = new Dictionary<int, string>();
        private Button buttonLoad;
        private PoleDataRussian poleDataRussian;
        private string filePathCertificate;

        // Делегат для метода
        public delegate void MyDelegate(PoleDataRussian poleDataRussian);
        private MyDelegate _myMethod;

        public LoadRussianDocument(MyDelegate myMethod, string filePathCertificate)
        {
            InitializeComponent();
            this.filePathCertificate = filePathCertificate;
            poleDataRussian = new PoleDataRussian();
            _myMethod = myMethod; // Сохраняем ссылку на метод

            FileInfo existingFile = new FileInfo(this.filePathCertificate);

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

                    data.Add(Convert.ToInt32(order), worksheet.Cells[row, 5].Text);
                }

                foreach (var kvp in data)
                {
                    CreatPanel(kvp.Key, kvp.Value);
                }
            }
        }

        // Создаём панельки с данными 
        private void CreatPanel(int numberOrderText, string nameCustomerText)
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
                Text = numberOrderText.ToString(),
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
                // Передаем данные в методS
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
                var worksheet = excelPackage.Workbook.Worksheets["RussianCertificate"];
                if (worksheet == null)
                {
                    MessageBox.Show("Лист 'RussianCertificate' не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }

            // Тут закрываем окно и запускается процесс передачи данных на другое окно
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (poleDataRussian.master != null)
            {
                _myMethod?.Invoke(poleDataRussian);
                Console.WriteLine("Окно закрыто с возвратом данных");
            }
            else
            {
                Console.WriteLine("Окно закрыто без возврата данных");
            }

            
            base.OnFormClosing(e);
        }

        private void textBoxSearchOrder_TextChanged(object sender, EventArgs e)
        {
            startOrderPanel.Controls.Clear();
            if (textBoxSearchOrder.Text != "")
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
            else if (textBoxSearchOrder.Text == "")
            {
                foreach (var allOrder in data)
                {
                    CreatPanel(allOrder.Key, allOrder.Value);
                }
            }

        }
    }
}
