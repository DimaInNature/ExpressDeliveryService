using System;

namespace ExpressDeliveryService.Model
{
    ///<summary> Класс - модель, представляющий товар,
    /// который будет перевозиться.</summary>

    public sealed class Product
    {
        ///<summary> Название товара.</summary>
        
        public string Name
        {
            get => _name;
            set
            {
                if (value != string.Empty)
                {
                    _name = value;
                }
                else
                {
                    _name = "NoData";
                }
            }
        }

        private string _name;

        ///<summary> Ценность товара в рублях.</summary>
        
        public double Cost
        {
            get => _cost;
            set
            {
                if (value > 0)
                {
                    _cost = Convert.ToDouble(value);
                }
                else
                {
                    _cost = 0;
                }
            }
        }

        private double _cost;

        ///<summary> Вес товара в граммах.</summary>
        
        public int Weight
        {
            get => _weight;
            set
            {
                if (value > 0 && Convert.ToInt32(value) < 10000)
                {
                    _weight = Convert.ToInt32(value);
                }
                else
                {
                    _weight = 0;
                }
            }
        }

        private int _weight;
    }
}
