using System;

namespace ExpressDeliveryService.Model
{
    ///<summary> Класс - модель, представляющий коробку,
    /// в которую будет упаковываться товар.</summary>
    
    public class Box
    {
        ///<summary> Ширина коробки.</summary>

        public double Width
        {
            get => width;
            set
            {
                if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                {
                    width = Convert.ToDouble(value);
                }
                else
                {
                    width = 0;
                }
            }
        }

        private double width;

        ///<summary> Высота коробки.</summary>

        public double Height
        {
            get => height;
            set
            {
                if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                {
                    height = value;
                }
                else
                {
                    height = 0;
                }
            }
        }

        private double height;

        ///<summary> Длина коробки.</summary>

        public double Lenght
        {
            get => lenght;
            set
            {
                if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                {
                    lenght = Convert.ToDouble(value);
                }
                else
                {
                    lenght = 0;
                }
            }
        }

        private double lenght;
    }
}
