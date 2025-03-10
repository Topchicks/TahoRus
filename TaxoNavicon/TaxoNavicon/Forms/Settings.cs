using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace TaxoNavicon
{
    public partial class Settings : Form
    {
        private string pathSettingsFile; // Путь к файлу json сохранения
        public string filePath; // Путь к файлу с таблицей Certificate
        public bool formatingSticker;
        public Settings()
        {
            InitializeComponent();
            // Тут получим относительный путь к файлу JSon настроек
            pathSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonSetting.json");
            LoadSaveJson();
            checkBoxFormateSticker.Checked = formatingSticker;
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
    }
}
