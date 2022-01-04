using Models.Abstract;
using Models.FluentBuilders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Boxes")]
    public class BoxModel : BaseModel
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public BoxModel() => CreatedDate = DateTime.UtcNow;

        public static BoxBuilder CreateBuilder() => new BoxBuilder();
    }
}
