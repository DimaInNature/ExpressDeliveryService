using Models.Abstract;
using Models.FluentBuilders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Products")]
    public class ProductModel : BaseModel
    {
        public string Name { get; set; }

        public double Cost { get; set; }

        public int Weight { get; set; }

        public ProductModel() => CreatedDate = DateTime.UtcNow;

        public static ProductBuilder CreateBuilder() => new ProductBuilder();
    }
}
