using System;

namespace ExpressDeliveryService.Model
{
    public class Order
    {
        public Order(string ProductName)
        {
            this.ProductName = ProductName;
        }

        public Order(){}
        ///<summary> Уникальный идентификатор заказа.</summary>
        public int IdOrder { get; private set; }

        ///<summary> Место получения товара.</summary>
        public string FromPlace { get; set; }
        ///<summary> Дата получения товара (dd/mm/yy).</summary>
        public DateTime FromDate { get; set; }
        ///<summary> Время получения товара.</summary>
        public string FromTime { get; set; }

        ///<summary> Место доставки товара.</summary>
        public string ToPlace { get; set; }
        ///<summary> Дата доставки товара (dd/mm/yy).</summary>
        public DateTime ToDate { get; set; }
        ///<summary> Время доставки товара.</summary>
        public string ToTime { get; set; }

        ///<summary> Ширина коробки.</summary>
        public double BoxWidth { get; set; }
        ///<summary> Высота коробки.</summary>
        public double BoxHeight { get; set; }
        ///<summary> Длина коробки.</summary>
        public double BoxLenght { get; set; }

        ///<summary> Ценность товара в рублях.</summary>
        public double ProductValue { get; set; }
        ///<summary> Название товара.</summary>
        public string ProductName { get; set; }
        ///<summary> Вес товара в граммах.</summary>
        public double ProductWeight { get; set; }

        ///<summary> Услуга страхования. True - Подключено. </summary>
        public bool AvailabilityOfInsuranceService { get; set; }
        ///<summary> Услуга соблюдения температурного режима. True - Подключено. </summary>
        public bool ComplianceTemperatureRegimeService { get; set; }
        ///<summary> Услуга упаковки товара. True - Подключено. </summary>
        public bool PackagingService { get; set; }

        ///<summary> Итоговая стоимость доставки. </summary>
        public double TotalCostOfDelivery { get; set; }

        

    }
}
