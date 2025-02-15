using Microsoft.Office.Interop.Word;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using TaxoNaviconRussian;
using Word = Microsoft.Office.Interop.Word;

namespace TaxoNavicon
{

    public partial class RussianTypeForm : Form
    {
        /*
        --order
        <orderNumber>
        <master>
        <dataJob> - дата выполнение работ
        <newDataJob> - дата выполнение новых работ

         --customer
        <nameCustomer>
        <adresCustomer>

        --vehicle
        <markaVehicle>
        <modelVehicle>
        <vinVehicle>
        <registrationNumberVehicle>
        <tireMarkingsVehicle>
        <odometrKmVehicle>

        --Tachograph
        <manufacturerTahograph>
        <serialNumberTachograph>
        <modelTahograph>
        <dataTahograph>
        <productionTahograph>

        <L>
        <W>
        <k>
        */
        PoleDataRussian poleDataRussian;
        private PrintDocument printDocument;

        private Word.Application wordApp;
        private Word.Document wordDoc;
        private string filePath;
        public RussianTypeForm()
        {
            InitializeComponent();
            poleDataRussian = new PoleDataRussian();
            string relativePath = @"test.doc"; // Относительный путь к файлу
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            wordApp = new Word.Application();
            dateTimePickerJob.CustomFormat = "dd/MM/yyyy"; // Устанавливаем только дату
        }


        // Открытие окна загрузок
        private void toolStripMenuItemLoadRussianDocument_Click(object sender, System.EventArgs e)
        {
            LoadRussianDocument loadRussianDocument = new LoadRussianDocument();

            loadRussianDocument.Show();
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
            poleDataRussian.markaVehicle = textBoxManufacturerTachograph.Text; // марка машины
            poleDataRussian.modelVehicle = textBoxModelVehicle.Text; // модель машины
            poleDataRussian.vinVehicle = textBoxVinNumberVehicle.Text; // вин номер машины
            poleDataRussian.registrationNumberVehicle = textBoxRegistrationNumberVehicle.Text; // рег. номер машины
            poleDataRussian.odometerKmVehicle = textBoxOdometerKmVehicle.Text; // одометр км

            //Tahograf - тахограф
            poleDataRussian.manufacturerTahograph = textBoxManufacturerTachograph.Text; // производитель
            poleDataRussian.modelTachograph = textBoxModelTachograph.Text; // модель тахографа

            poleDataRussian.l = textBoxL.Text;
            poleDataRussian.w = textBoxW.Text;
            poleDataRussian.k = textBoxK.Text;

            poleDataRussian.dataJob = dateTimePickerJob.Value.ToShortDateString();//  время выполнения работ
            
            // Тут обработаем новую дату выполнения работ
            DateTimePicker dateTimePicker = dateTimePickerJob;
            dateTimePicker.Value.AddYears(3);
            poleDataRussian.newDataJob = dateTimePicker.Value.ToShortDateString();//  время выполнения работ
        }
    }
}
