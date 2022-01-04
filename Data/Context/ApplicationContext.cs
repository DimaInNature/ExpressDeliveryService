using Models;
using System.Data.Entity;

namespace Data.Context
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<BoxModel> Boxes { get; set; }
    }
}
