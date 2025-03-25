using System;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TaxoNavicon.Forms;

namespace TaxoNavicon
{
    public partial class Settings : Form
    {
        private string pathSettingsFile; // Путь к файлу json сохранения
        public string filePath; // Путь к файлу с таблицей Certificate
        public bool formatingSticker;

        public string defualtPrinterWord;
        public string defualtPrinterSticker;

        private string adressMasterRus;
        private string adressMasterEng;
        public Settings()
        {
            InitializeComponent();
            // Тут получим относительный путь к файлу JSon настроек
            pathSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSetting.json");
            LoadSaveJson();
            checkBoxFormateSticker.Checked = formatingSticker;
            comboBoxPrinterWord.Text = defualtPrinterWord;
            comboBoxPrinterSticker.Text = defualtPrinterSticker;
            adressRusBox.Text = adressMasterRus;
            adressEngBox.Text = adressMasterEng;


            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                comboBoxPrinterWord.Items.Add(printer);
                comboBoxPrinterSticker.Items.Add(printer);
            }
        }

        private void FileSavePath_Click(object sender, EventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем форму настроек
        }

        // Метод для сохранения пути к файлу
        private void SaveJsonSettings()
        {
            SettingsJS settingsJS = new SettingsJS()
            {
                FilePath = filePath,
                FormatingSticker = formatingSticker,
                DefualtPrinterWord = defualtPrinterWord,
                DefualtPrinterSticker = defualtPrinterSticker,
                AdressMasterRus = adressRusBox.Text,
                AdressMasterEng = adressEngBox.Text,
            };

            var options = new JsonSerializerOptions();

            // Для визуального красивого расположения
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(settingsJS);

            File.WriteAllText(pathSettingsFile, jsonString);
        }

        // Тут идёт обработка если настройки были приняты в ручную через textBox
        private void textBoxFileSavePath_TextChanged(object sender, EventArgs e)
        {
            filePath = textBoxFileSavePath.Text;
            SaveJsonSettings();
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
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void checkBoxFormateSticker_CheckedChanged(object sender, EventArgs e)
        {
            formatingSticker = checkBoxFormateSticker.Checked;
            SaveJsonSettings();
        }

        private void comboBoxPrinterWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            defualtPrinterWord = comboBoxPrinterWord.Text;
            SaveJsonSettings();
        }

        private void comboBoxPrinterSticker_SelectedIndexChanged(object sender, EventArgs e)
        {
            defualtPrinterSticker = comboBoxPrinterSticker.Text;
            SaveJsonSettings();
        }

        private void linkLabelOpenPanelInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InfoSettings infoSettings = new InfoSettings();

            infoSettings.ShowDialog();
        }

        private void adressRusBox_TextChanged(object sender, EventArgs e)
        {
            SaveJsonSettings();
        }
    }
}
