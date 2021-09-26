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
            get => _width;
            set
            {
                if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                {
                    _width = Convert.ToDouble(value);
                }
                else
                {
                    _width = 0;
                }
            }
        }

        private double _width;

        ///<summary> Высота коробки.</summary>

        public double Height
        {
            get => _height;
            set
            {
                if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                {
                    _height = value;
                }
                else
                {
                    _height = 0;
                }
            }
        }

        private double _height;

        ///<summary> Длина коробки.</summary>

        public double Lenght
        {
            get => _lenght;
            set
            {
                if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                {
                    _lenght = Convert.ToDouble(value);
                }
                else
                {
                    _lenght = 0;
                }
            }
        }

        private double _lenght;
    }
}
