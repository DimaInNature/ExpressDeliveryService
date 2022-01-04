using System.Collections.Generic;

namespace Models.FluentBuilders
{
    public sealed class UserBuilder
    {
        public UserBuilder()
        {
            _user = new UserModel();
        }

        private readonly UserModel _user;

        public UserBuilder SetId(int id)
        {
            _user.Id = id;
            return this;
        }

        public UserBuilder SetLogin(string login)
        {
            _user.Login = login;
            return this;
        }

        public UserBuilder SetPassword(string password)
        {
            _user.Password = password;
            return this;
        }

        public UserBuilder SetName(string name)
        {
            _user.Name = name;
            return this;
        }

        public UserBuilder SetSurname(string surname)
        {
            _user.Surname = surname;
            return this;
        }

        public UserBuilder SetPatronymic(string patronymic)
        {
            _user.Patronymic = patronymic;
            return this;
        }

        public UserBuilder SetMail(string mail)
        {
            _user.Mail = mail;
            return this;
        }

        public UserBuilder SetOrders(List<OrderModel> orders)
        {
            _user.Orders = orders;
            return this;
        }

        public static implicit operator UserModel(UserBuilder builder) => builder._user;
    }
}
