using Xunit;

namespace Common.Tests
{
    public class GoogleMapHelperTests
    {
        [Fact]
        public void GetDistanceBetweenThreeKeywords_Test()
        {
            var res = GetDistanceBetweenThreeKeywords();

            Assert.Equal(600, res);
        }

        private double GetDistanceBetweenThreeKeywords()
        {
            var d1 = 1000;

            var d2 = 500;

            var d3 = 300;

            var distance = (d1 + d2 + d3) / 3;

            return distance;
        }
    }
}
