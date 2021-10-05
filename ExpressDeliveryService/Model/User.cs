using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ExpressDeliveryService.Model
{
    public sealed class User
    {
        ///<summary> Уникальный идентификатор.</summary>

        [BsonId]
        public Guid Id { get; set; }

        #region Технические данные

        ///<summary> Логин пользователя.</summary>

        public string Login
        {
            get => _login;
            set
            {
                if (value != String.Empty)
                {
                    _login = value;
                }
                else
                {
                    _login = String.Empty;
                }
            }
        }

        private string _login;

        ///<summary> Пароль пользователя.</summary>

        public string Password
        {
            get => _password;
            set
            {
                if (value != String.Empty)
                {
                    _password = value;
                }
                else
                {
                    _password = String.Empty;
                }
            }
        }

        private string _password;

        #endregion

        #region Личные данные

        ///<summary> Имя.</summary>

        public string Name
        {
            get => _name;
            set
            {
                if (value != String.Empty)
                {
                    _name = value;
                }
                else
                {
                    _name = String.Empty;
                }
            }
        }

        private string _name;

        ///<summary> Фамилия.</summary>

        public string Surname
        {
            get => _surname;
            set
            {
                if (value != String.Empty)
                {
                    _surname = value;
                }
                else
                {
                    _surname = String.Empty;
                }
            }
        }

        private string _surname;

        ///<summary> Отчество.</summary>

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                if (value != String.Empty)
                {
                    _patronymic = value;
                }
                else
                {
                    _patronymic = String.Empty;
                }
            }
        }

        private string _patronymic;

        ///<summary> Электронная почта.</summary>

        public string Mail
        {
            get => _mail;
            set
            {
                if (value != String.Empty)
                {
                    _mail = value;
                }
                else
                {
                    _mail = String.Empty;
                }
            }
        }

        private string _mail;

        ///<summary> Список заказов этого пользователя.</summary>

        public List<Order> Orders
        {
            get => _orders;
            set => _orders = value;
        }

        private List<Order> _orders;

        #endregion

    }
}
