namespace Models.FluentBuilders
{
    public sealed class ProductBuilder
    {
        public ProductBuilder()
        {
            _product = new ProductModel();
        }

        private readonly ProductModel _product;

        public ProductBuilder SetId(int id)
        {
            _product.Id = id;
            return this;
        }

        public ProductBuilder SetName(string name)
        {
            _product.Name = name;
            return this;
        }

        public ProductBuilder SetCost(double cost)
        {
            _product.Cost = cost;
            return this;
        }

        public ProductBuilder SetWeight(int weight)
        {
            _product.Weight = weight;
            return this;
        }

        public static implicit operator ProductModel(ProductBuilder builder) => builder._product;
    }
}
