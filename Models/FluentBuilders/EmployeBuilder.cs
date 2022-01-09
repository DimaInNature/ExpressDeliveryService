namespace Models.FluentBuilders
{
    public class EmployeBuilder
    {
        public EmployeBuilder() => _employe = new EmployeModel();

        public static implicit operator EmployeModel(EmployeBuilder builder) => builder._employe;

        private readonly EmployeModel _employe;

        public EmployeBuilder SetId(int id)
        {
            _employe.Id = id;
            return this;
        }

        public EmployeBuilder SetLogin(string login)
        {
            _employe.Login = login;
            return this;
        }

        public EmployeBuilder SetPassword(string password)
        {
            _employe.Password = password;
            return this;
        }

        public EmployeBuilder SetName(string name)
        {
            _employe.Name = name;
            return this;
        }

        public EmployeBuilder SetSurname(string surname)
        {
            _employe.Surname = surname;
            return this;
        }

        public EmployeBuilder SetPatronymic(string patronymic)
        {
            _employe.Patronymic = patronymic;
            return this;
        }

        public EmployeBuilder SetMail(string mail)
        {
            _employe.Mail = mail;
            return this;
        }

        public EmployeBuilder SetFavouriteLocation(string location)
        {
            _employe.FavouriteLocation = location;
            return this;
        }
    }
}
