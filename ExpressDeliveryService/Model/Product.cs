using System;

namespace ExpressDeliveryService.Model
{
    ///<summary> Класс - модель, представляющий товар,
    /// который будет перевозиться.</summary>
    
    public class Product
    {
        ///<summary> Название товара.</summary>
        public string Name
        {
            get => name;
            set
            {
                if (value != string.Empty)
                {
                    name = value;
                }
                else
                {
                    name = "NoData";
                }
            }
        }

        private string name;

        ///<summary> Ценность товара в рублях.</summary>
        public double Cost
        {
            get => cost;
            set
            {
                if (value > 0)
                {
                    cost = Convert.ToDouble(value);
                }
                else
                {
                    cost = 0;
                }
            }
        }

        private double cost;

        ///<summary> Вес товара в граммах.</summary>
        public int Weight
        {
            get => weight;
            set
            {
                if (value > 0 && Convert.ToInt32(value) < 10000)
                {
                    weight = Convert.ToInt32(value);
                }
                else
                {
                    weight = 0;
                }
            }
        }

        private int weight;
    }
}
