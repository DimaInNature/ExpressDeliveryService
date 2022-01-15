using Models;
using System;

namespace Common
{
    public static class TotalCostCalculator
    {
        public static void Calculate(BoxModel boxData,
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

            var distance = GoogleMapHelper.GetDistanceBetweenTwoKeywords(
                fromKey: orderData.FromPlace,
                toKey: orderData.ToPlace);

            result += distance / 1000 / 100 * 20;

            result = Math.Round(
                value: result,
                mode: MidpointRounding.ToEven,
                digits: 2);
        }
    }
}
