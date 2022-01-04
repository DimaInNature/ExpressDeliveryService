namespace Models.FluentBuilders
{
    public sealed class BoxBuilder
    {
        public BoxBuilder() => _box = new BoxModel();

        public static implicit operator BoxModel(BoxBuilder builder) => builder._box;

        private readonly BoxModel _box;

        public BoxBuilder SetId(int id)
        {
            _box.Id = id;
            return this;
        }

        public BoxBuilder SetWidth(double width)
        {
            _box.Width = width;
            return this;
        }

        public BoxBuilder SetHeight(double height)
        {
            _box.Height = height;
            return this;
        }

        public BoxBuilder SetLength(double length)
        {
            _box.Length = length;
            return this;
        }
    }
}
