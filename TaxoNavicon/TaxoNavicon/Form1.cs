using System;
using System.Windows.Forms;

namespace TaxoNavicon
{
    public partial class Form1 : Form
    {
        PoleData poleData;
        public Form1()
        {
            InitializeComponent();
            poleData = new PoleData();
        }

        private void ToolStripMenuItemCertificate_Click(object sender, EventArgs e)
        {
            SetData();
            Certificate Certificate = new Certificate(poleData);
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
            poleData.orderNumber = (int)numericUpDowntextBoxOrderNumber.Value;// номер заказа
            poleData.master = comboBoxMaster.Text; // мастер


            //Customer
            poleData.nameCustomer = textBoxNameCustomer.Text; // имя
            poleData.nameCustomerEng = textBoxNameCustomerEng.Text; // имя на английском
            poleData.adresCustomer = textBoxAdresCustomer.Text;// адрес заказчика

            //Vehicle
            poleData.markaVehicle = textBoxMarkaVehicle.Text; // марка машины
            poleData.modelVehicle = textBoxModelVehicle.Text; // модель машины
            poleData.vinVehicle = textBoxVinNumberVehicle.Text; // вин номер машины
            poleData.registrationNumberVehicle = textBoxRegistrationNumberVehicle.Text; // рег. номер машины
            poleData.odometerKmVehicle = textBoxOdometerKmVehicle.Text; // одометр км
            poleData.yearOfIssueVehiccle = textBoxYearOfIssueVehiccle.Text; // год выпуска

            //Tahograf
            poleData.manufacturerTahograph = textBoxManufacturerTachograph.Text; // производитель
            poleData.modelTachograph = textBoxModelTachograph.Text; // модель тахографа
            poleData.serialNumberTachograph = textBoxSerialNumberTachograph.Text; // модель тахографа

            poleData.l = textBoxL.Text;
            poleData.w = textBoxW.Text;
            poleData.k = textBoxK.Text;

            poleData.noteOrder = textBoxNoteOrder.Text; // примечания
            poleData.dataJob = dateTimePickerJob.ToString(); //  время выполнения работ
        }
    }
}
