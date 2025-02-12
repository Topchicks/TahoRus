using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxoNavicon
{
    public class PoleDataEuropean
    {
        #region order
        public int orderNumber; // номер заказа
        public string master; // мастер
        public string noteOrder; // примечание
        public string dataJob; // дата выполнение работ
        #endregion

        #region customer
        public string nameCustomer; // имя заказчика
        public string nameCustomerEng; // имя заказчика на английском
        public string adresCustomer; // адрес заказчика
        #endregion

        #region vehicle
        public string manufacturerVehicle; // производитель транспорта
        public string modelVehicle; // модель транспорта
        public string vinVehicle; // вин номер транспорта
        public string registrationNumberVehicle; // рег. номер
        public string tireMarkingsVehicle; // маркировка шин
        public string odometerKmVehicle; // одометр км
        public string yearOfIssueVehiccle; // год выпуска
        #endregion

        #region Tachograph
        public string manufacturerTahograph; // производитель
        public string serialNumberTachograph; // срийный номр
        public string modelTachograph; // модель
        public string producedTachograph; // произведен .год
        #endregion

        public string l;
        public string w;
        public string k;
        
    }
}
