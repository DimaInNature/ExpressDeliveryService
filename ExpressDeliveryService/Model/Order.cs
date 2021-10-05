using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpressDeliveryService.Model
{
    ///<summary> Класс - модель,
    /// представляющий заказ.</summary>
    
    public sealed class Order
    {
        ///<summary> Уникальный идентификатор.</summary>

        [BsonId]
        public Guid Id { get; set; }

        ///<summary> Место получения товара.</summary>
        
        public string FromPlace
        {
            get => _fromPlace;
            set
            {
                if (value != String.Empty)
                {
                    _fromPlace = value;
                }
                else
                {
                    _fromPlace = "NoData";
                }
            }
        }

        private string _fromPlace;

        ///<summary> Дата получения товара (dd/mm/yy).</summary>
        
        public DateTime? FromDate
        {
            get => _fromDate;
            set
            {
                if (value != DateTime.MinValue)
                {
                    _fromDate = value;
                }
            }
        }

        private DateTime? _fromDate;

        ///<summary> Время получения товара.</summary>
        
        public string FromTime
        {
            get => _fromTime;
            set
            {
                _fromTime = value;
            }
        }

        private string _fromTime;

        ///<summary> Место доставки товара.</summary>
        
        public string ToPlace
        {
            get => _toPlace;
            set
            {
                if (value != String.Empty)
                {
                    _toPlace = value;
                }
                else
                {
                    _toPlace = "NoData";
                }
            }
        }

        private string _toPlace;

        ///<summary> Дата доставки товара (dd/mm/yy).</summary>
        
        public DateTime? ToDate
        {
            get => _toDate;
            set
            {
                if (value != DateTime.MinValue)
                {
                    _toDate = value;
                }
            }
        }

        private DateTime? _toDate;

        ///<summary> Время доставки товара.</summary>
        
        public string ToTime
        {
            get => _toTime;
            set
            {
                _toTime = value;
            }
        }

        private string _toTime;

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
            get => _totalCost;
            set
            {
                if (Convert.ToDouble(value) > 0)
                {
                    _totalCost = Convert.ToDouble(value);
                }
                else
                {
                    _totalCost = 0;
                }
            }
        }

        private double _totalCost;

        ///<summary> Пользователь, который сделал заказ. </summary>

    }
}
