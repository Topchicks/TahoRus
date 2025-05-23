namespace TaxoNavicon
{
    public class PoleDataEuropean
    {
        #region order
        public int orderNumber; // номер заказа
        public string master; // мастер
        public string dataJob; // дата выполнение работ
        #endregion

        #region customer
        public string nameCustomer; // имя заказчика
        public string nameCustomerEng; // имя заказчика на английском
        public string adresCustomer; // адрес заказчика
        public string adresCustomerEng; // адрес заказчика
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
        public string serialNumberTahograph; // серийный номр
        public string modelTachograph; // модель
        #endregion

        public string temperature;
        public string protectore;

        public string russAdresMaster;
        public string engAdresMaster;

        public string l;
        public string w;
        public string k;
    }
}
