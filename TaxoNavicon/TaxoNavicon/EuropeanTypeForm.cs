using System;
using System.Net.Http;
using System.Windows.Forms;

namespace TaxoNavicon
{
    public partial class EuropeanTypeForm : Form
    {
        PoleDataEuropean poleDataEuropean;

        public class TranslateResponse
        {
            public Translation[] Translations { get; set; }
        }

        public class Translation
        {
            public string Text { get; set; }
        }

        public EuropeanTypeForm()
        {
            InitializeComponent();
            poleDataEuropean = new PoleDataEuropean();
        }

        private void ToolStripMenuItemCertificate_Click(object sender, EventArgs e)
        {
            SetData();
            Certificate Certificate = new Certificate(poleDataEuropean);
            Certificate.Show();
        }

        private void ToolStripMenuItemSticker_Click(object sender, EventArgs e)
        {
            Sticker sticker = new Sticker();

            sticker.Show();
        }
        
        private void SetData()
        {
            //Order
            poleDataEuropean.orderNumber = (int)numericUpDowntextBoxOrderNumber.Value;// номер заказа
            poleDataEuropean.master = comboBoxMaster.Text; // мастер


            //Customer
            poleDataEuropean.nameCustomer = textBoxNameCustomer.Text; // имя
            poleDataEuropean.nameCustomerEng = textBoxNameCustomerEng.Text; // имя
            
            poleDataEuropean.adresCustomer = textBoxAdresCustomer.Text;// адрес заказчика

            //Vehicle
            poleDataEuropean.manufacturerVehicle = textBoxManufacturerTachograph.Text; // марка машины
            poleDataEuropean.modelVehicle = textBoxModelVehicle.Text; // модель машины
            poleDataEuropean.vinVehicle = textBoxVinNumberVehicle.Text; // вин номер машины
            poleDataEuropean.registrationNumberVehicle = textBoxRegistrationNumberVehicle.Text; // рег. номер машины
            poleDataEuropean.odometerKmVehicle = textBoxOdometerKmVehicle.Text; // одометр км
            poleDataEuropean.yearOfIssueVehiccle = textBoxYearOfIssueVehiccle.Text; // год выпуска

            //Tahograf
            poleDataEuropean.manufacturerTahograph = textBoxManufacturerTachograph.Text; // производитель
            poleDataEuropean.modelTachograph = textBoxModelTachograph.Text; // модель тахографа
            poleDataEuropean.serialNumberTachograph = textBoxSerialNumberTachograph.Text; // модель тахографа

            poleDataEuropean.l = textBoxL.Text;
            poleDataEuropean.w = textBoxW.Text;
            poleDataEuropean.k = textBoxK.Text;

            poleDataEuropean.noteOrder = textBoxNoteOrder.Text; // примечания
            poleDataEuropean.dataJob = dateTimePickerJob.Value.Date.ToString(); //  время выполнения работ
        }
    }
}
