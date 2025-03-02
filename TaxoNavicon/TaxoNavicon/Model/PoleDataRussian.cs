namespace TaxoNaviconRussian
{
    public class PoleDataRussian
    {
        #region order
        public int orderNumber; // номер заказа
        public string master; // мастер
        public string dataJob; // дата выполнение работ
        public string newDataJob; // дата выполнение новых работ
        #endregion

        #region customer
        public string nameCustomer; // имя заказчика
        public string adresCustomer; // адрес заказчика
        #endregion

        #region vehicle
        public string markaVehicle; // марка
        public string modelVehicle; // модель транспорта
        public string vinVehicle; // вин номер транспорта
        public string registrationNumberVehicle; // рег. номер
        public string tireMarkingsVehicle; // маркировка шин
        public string odometerKmVehicle; // одометр км
        #endregion

        #region Tachograph
        public string manufacturerTahograph; // производитель
        public string serialNumberTahograph; // срийный номр
        public string modelTachograph; // модель
        public string producedTachograph; // произведен .год
        #endregion

        public string l;
        public string w;
        public string k;


        public string locationInstallationTable; // Расположение установочной таблицы
        public string inspectionResult; // Результат инспекции
        public string signsManipulation; // Признаки манипуляции
        public string specialMarks; // Особые отметки

    }
}
