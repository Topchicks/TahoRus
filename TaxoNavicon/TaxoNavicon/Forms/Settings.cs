using System;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TaxoNavicon.Forms;
using TaxoNavicon.Model;

namespace TaxoNavicon
{
    public partial class Settings : Form
    {
        private string pathSettingsFile; // Путь к файлу json сохранения
        public string filePath; // Путь к файлу с таблицей Certificate
        public bool formatingSticker;

        private string defualtPrinterWord;
        private string defualtPrinterSticker;

        private string adressMasterRus;
        private string adressMasterEng;
        private string adressSticker;
        public Settings()
        {
            InitializeComponent();
            // Тут получим относительный путь к файлу JSon настроек
            //pathSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSetting.json");
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appName = "TachoPrintData"; // Замените на название Вашего приложения
            string settingsFolder = Path.Combine(documentsPath, appName);

            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
            }

            pathSettingsFile = Path.Combine(settingsFolder, "JsonSetting.json");

            LoadSaveJson();


            
            adressRusBox.Text = adressMasterRus;
            adressEngBox.Text = adressMasterEng;
            textBoxAdressSticker.Text = adressSticker;
            checkBoxFormateSticker.Checked = formatingSticker;

            



            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                comboBoxPrinterWord.Items.Add(printer);
                comboBoxPrinterSticker.Items.Add(printer);
            }

            comboBoxPrinterWord.Text = defualtPrinterWord;
            comboBoxPrinterSticker.Text = defualtPrinterSticker;

            comboBoxPrinterSticker.SelectedIndexChanged += ChangeSettings;
            comboBoxPrinterWord.SelectedIndexChanged += ChangeSettings;
            textBoxAdressSticker.TextChanged += ChangeSettings;
            adressEngBox.TextChanged += ChangeSettings;
            adressRusBox.TextChanged += ChangeSettings;
            textBoxFileSavePath.TextChanged += ChangeSettings;
            checkBoxFormateSticker.Click += ChangeSettings;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем форму настроек
        }

        // Метод для сохранения пути к файлу
        private void SaveJsonSettings()
        {
            SettingsJS settingsJS = new SettingsJS()
            {
                FilePath = textBoxFileSavePath.Text,
                FormatingSticker = checkBoxFormateSticker.Checked,
                DefualtPrinterWord = defualtPrinterWord,
                DefualtPrinterSticker = defualtPrinterSticker,
                AdressMasterRus = adressRusBox.Text,
                AdressMasterEng = adressEngBox.Text,
                AdressSticker = textBoxAdressSticker.Text,
            };
            Console.WriteLine($"DefualtPrinterWord {settingsJS.DefualtPrinterWord}  DefualtPrinterSticker {settingsJS.DefualtPrinterSticker}");


            var options = new JsonSerializerOptions();

            // Для визуального красивого расположения
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(settingsJS);

            File.WriteAllText(pathSettingsFile, jsonString);
        }

        private void LoadSaveJson()
        {
            try
            {
                var saveJson = File.ReadAllText(pathSettingsFile);
                
                SettingsJS settingsJS = JsonSerializer.Deserialize<SettingsJS>(saveJson);

                textBoxFileSavePath.Text = settingsJS.FilePath;
                formatingSticker = settingsJS.FormatingSticker;
                defualtPrinterSticker = settingsJS.DefualtPrinterSticker;
                defualtPrinterWord = settingsJS.DefualtPrinterWord;
                adressMasterRus = settingsJS.AdressMasterRus;
                adressMasterEng = settingsJS.AdressMasterEng;
                adressSticker = settingsJS.AdressSticker;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

       
        private void ChangeSettings(object sender, EventArgs e)
        {
            defualtPrinterSticker = comboBoxPrinterSticker.Text;
            defualtPrinterWord = comboBoxPrinterWord.Text;
            Console.WriteLine($"Word {comboBoxPrinterWord.Text}  Sticker {comboBoxPrinterSticker.Text}");
            SaveJsonSettings();
        }

        // Для открытия окна справки
        private void linkLabelOpenPanelInfo_Click(object sender, EventArgs e)
        {
            InfoSettings infoSettings = new InfoSettings();

            infoSettings.ShowDialog();
        }

        private void FileSavePath_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Устанавливаем фильтры для файлов
                openFileDialog.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите файл Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK) // Если пользователь выбрал файл
                {
                    filePath = openFileDialog.FileName; // Получаем путь к выбранному файлу

                    // Заполним поле для видимости пути к сохранению
                    textBoxFileSavePath.Text = filePath;
                    SaveJsonSettings();
                }
            }
        }

        Translate translate = new Translate();
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            adressEngBox.Text = translate.Transliterate(adressRusBox.Text);
            SaveJsonSettings();
        }
    }
}
