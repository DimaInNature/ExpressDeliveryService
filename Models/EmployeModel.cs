using Models.Abstract;
using Models.FluentBuilders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table(name: "Employees")]
    public class EmployeModel : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Mail { get; set; }

        public string FavouriteLocation { get; set; } = "Astrakhan Lenin Square";

        public EmployeModel() => CreatedDate = DateTime.UtcNow;

        public static EmployeBuilder CreateBuilder() => new EmployeBuilder();
    }
}
