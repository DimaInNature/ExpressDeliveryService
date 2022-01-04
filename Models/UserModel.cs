using Models.Abstract;
using Models.FluentBuilders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Users")]
    public class UserModel : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Mail { get; set; }

        public string IsAdmin { get; set; } = "False";

        public virtual List<OrderModel> Orders { get; set; }

        public UserModel() => CreatedDate = DateTime.UtcNow;

        public static UserBuilder CreateBuilder() => new UserBuilder();
    }
}
