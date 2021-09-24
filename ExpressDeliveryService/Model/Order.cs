using System;

namespace ExpressDeliveryService.Model
{
    ///<summary> Класс - модель,
    /// представляющий заказ.</summary>
    
    public class Order
    {
        ///<summary> Уникальный идентификатор заказа.</summary>
        
        public int IdOrder { get; set; }

        ///<summary> Место получения товара.</summary>
        
        public string FromPlace
        {
            get => fromPlace;
            set
            {
                if (value != String.Empty)
                {
                    fromPlace = value;
                }
                else
                {
                    fromPlace = "NoData";
                }
            }
        }

        private string fromPlace;

        ///<summary> Дата получения товара (dd/mm/yy).</summary>
        
        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                if (value != DateTime.MinValue)
                {
                    fromDate = value;
                }
            }
        }

        private DateTime fromDate;

        ///<summary> Время получения товара.</summary>
        
        public string FromTime
        {
            get => fromTime;
            set
            {
                if ((value != String.Empty) && (value.Length == 5))
                {
                    fromTime = value;
                }
                else
                {
                    fromTime = "00:00";
                }
            }
        }

        private string fromTime;

        ///<summary> Место доставки товара.</summary>
        
        public string ToPlace
        {
            get => toPlace;
            set
            {
                if (value != String.Empty)
                {
                    toPlace = value;
                }
                else
                {
                    toPlace = "NoData";
                }
            }
        }

        private string toPlace;

        ///<summary> Дата доставки товара (dd/mm/yy).</summary>
        
        public DateTime ToDate
        {
            get => toDate;
            set
            {
                if (value != DateTime.MinValue)
                {
                    toDate = value;
                }
            }
        }

        private DateTime toDate;

        ///<summary> Время доставки товара.</summary>
        
        public string ToTime
        {
            get => toTime;
            set
            {
                if ((value != String.Empty) && (value.Length == 5))
                {
                    toTime = value;
                }
                else
                {
                    toTime = "00:00";
                }
            }
        }

        private string toTime;

        ///<summary> Объект, представляющий коробку,
        /// в которую будет упаковываться товар.</summary>

        public Box Box { get; set; }

        ///<summary> Объект, представляющий товар,
        /// который будет перевозиться.</summary>
        
        public Product Product { get; set; }

        public bool AvailabilityOfInsurancePurchased { get; set; }
        
        public bool ComplianceTemperatureRegimePurchased { get; set; } 

        public bool PackagingPurchased { get; set; }

        ///<summary> Итоговая стоимость доставки. </summary>
        
        public double TotalCost
        {
            get => totalCost;
            set
            {
                if (Convert.ToDouble(value) > 0)
                {
                    totalCost = Convert.ToDouble(value);
                }
                else
                {
                    totalCost = 0;
                }
            }
        }

        private double totalCost;
    }
}
