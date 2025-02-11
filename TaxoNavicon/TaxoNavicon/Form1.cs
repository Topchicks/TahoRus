using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TaxoNavicon
{
    public partial class EuropeanTypeForm : Form
    {

        private static readonly HttpClient client = new HttpClient();
        PoleData poleData;

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
            poleData.nameCustomerEng = textBoxNameCustomerEng.Text; // имя
            
            poleData.adresCustomer = textBoxAdresCustomer.Text;// адрес заказчика

            //Vehicle
            poleData.manufacturerVehicle = textBoxManufacturerTachograph.Text; // марка машины
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
            poleData.dataJob = dateTimePickerJob.Value.Date.ToString(); //  время выполнения работ
        }
    }
}
