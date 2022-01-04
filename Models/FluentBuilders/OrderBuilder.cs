using System;

namespace Models.FluentBuilders
{
    public sealed class OrderBuilder
    {
        public OrderBuilder()
        {
            _order = new OrderModel();
        }

        private readonly OrderModel _order;

        public OrderBuilder SetId(int id)
        {
            _order.Id = id;
            return this;
        }

        public OrderBuilder SetBox(int boxId)
        {
            _order.BoxId = boxId;
            return this;
        }

        public OrderBuilder SetBox(BoxModel box)
        {
            _order.BoxId = box.Id;
            return this;
        }

        public OrderBuilder SetProduct(int productId)
        {
            _order.ProductId = productId;
            return this;
        }

        public OrderBuilder SetProduct(ProductModel product)
        {
            _order.ProductId = product.Id;
            return this;
        }

        public OrderBuilder SetUser(int userId)
        {
            _order.UserId = userId;
            return this;
        }

        public OrderBuilder SetUser(UserModel user)
        {
            _order.UserId = user.Id;
            return this;
        }

        public OrderBuilder SetFromPlace(string fromPlace)
        {
            _order.FromPlace = fromPlace;
            return this;
        }

        public OrderBuilder SetFromDate(DateTime fromDate)
        {
            _order.FromDate = fromDate;
            return this;
        }

        public OrderBuilder SetFromTime(string fromTime)
        {
            _order.FromTime = fromTime;
            return this;
        }

        public OrderBuilder SetToPlace(string toPlace)
        {
            _order.ToPlace = toPlace;
            return this;
        }

        public OrderBuilder SetToDate(DateTime toDate)
        {
            _order.ToDate = toDate;
            return this;
        }

        public OrderBuilder SetToTime(string toTime)
        {
            _order.ToTime = toTime;
            return this;
        }

        public OrderBuilder SetAvailabilityOfInsurancePurchased(bool availabilityOfInsurancePurchased)
        {
            _order.AvailabilityOfInsurancePurchased = availabilityOfInsurancePurchased
                ? "True"
                : "False";
            return this;
        }

        public OrderBuilder SetComplianceTemperatureRegimePurchased(bool complianceTemperatureRegimePurchased)
        {
            _order.ComplianceTemperatureRegimePurchased = complianceTemperatureRegimePurchased
                ? "True"
                : "False";
            return this;
        }

        public OrderBuilder SetPackagingPurchased(bool packagingPurchased)
        {
            _order.PackagingPurchased = packagingPurchased
                ? "True"
                : "False";
            return this;
        }

        public OrderBuilder SetTotalCost(double totalCost)
        {
            _order.TotalCost = totalCost;
            return this;
        }

        public static implicit operator OrderModel(OrderBuilder builder) => builder._order;
    }
}
