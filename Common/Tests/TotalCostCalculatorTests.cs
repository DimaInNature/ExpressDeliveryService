using Models;
using System;
using Xunit;

namespace Common.Tests
{
    public class TotalCostCalculatorTests
    {
        [Fact]
        public void Calculate_BoxData_Test()
        {
            BoxModel boxData = BoxModel.CreateBuilder()
                .SetWidth(10)
                .SetHeight(10)
                .SetLength(10);

            var res = Math.Round(
                value: boxData.Height * boxData.Length * boxData.Width / 30,
                mode: MidpointRounding.ToEven,
                digits: 1);

            Assert.Equal(33.3, res);
        }

        [Fact]
        public void Calculate_ProductCost_Test()
        {
            ProductModel productData = ProductModel.CreateBuilder()
                .SetCost(200);

            var res = Math.Round(
                value: productData.Cost / 5,
                mode: MidpointRounding.ToEven,
                digits: 1);

            Assert.Equal(40, res);
        }

        [Fact]
        public void Calculate_ProductWeight_Test()
        {
            ProductModel productData = ProductModel.CreateBuilder()
                .SetWeight(1000);

            var res = Math.Round(
                value: productData.Weight / 10,
                mode: MidpointRounding.ToEven,
                digits: 1);

            Assert.Equal(100, res);
        }

        [Fact]
        public void Calculate_Distance_Test()
        {
            OrderModel orderData = OrderModel.CreateBuilder()
                .SetFromPlace(fromPlace: "Test")
                .SetToPlace(toPlace: "Data");

            var res = FakeGetDistanceBetweenTwoKeywords(
                fromKey: orderData.FromPlace,
                toKey: orderData.ToPlace) / 1000 / 100 * 20;

            Assert.Equal(2000, res);
        }

        [Fact]
        public void Calculate_Test()
        {
            BoxModel boxData = BoxModel.CreateBuilder()
                .SetWidth(10)
                .SetHeight(10)
                .SetLength(10);

            ProductModel productData = ProductModel.CreateBuilder()
                .SetCost(200)
                .SetWeight(1000);

            OrderModel orderData = OrderModel.CreateBuilder()
                .SetFromPlace("Foo")
                .SetToPlace("Bar")
                .SetAvailabilityOfInsurancePurchased(true)
                .SetComplianceTemperatureRegimePurchased(true)
                .SetPackagingPurchased(true);

            Calculate(boxData, productData, orderData, out double res);

            Assert.Equal(2412, res);
        }

        private double FakeGetDistanceBetweenTwoKeywords
            (string fromKey, string toKey) => 10_000_000; //or 10 000 km

        private void Calculate(BoxModel boxData,
            ProductModel productData, OrderModel orderData, out double result)
        {
            result = 0;

            result += boxData.Height * boxData.Length * boxData.Width / 30;

            result += productData.Cost / 5;

            result += productData.Weight / 10;

            result += orderData.AvailabilityOfInsurancePurchased == "True"
                ? productData.Cost / 10
                : 0;

            result += orderData.PackagingPurchased == "True"
                ? 150
                : 0;

            result += orderData.ComplianceTemperatureRegimePurchased == "True"
                ? result / 100 * 20
                : 0;

            var distance = FakeGetDistanceBetweenTwoKeywords(
                fromKey: orderData.FromPlace,
                toKey: orderData.ToPlace);

            result += distance / 1000 / 100 * 20;

            result = Math.Round(
                value: result,
                mode: MidpointRounding.ToEven,
                digits: 1);
        }
    }
}
