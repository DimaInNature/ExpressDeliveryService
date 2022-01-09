using GMap.NET.WindowsPresentation;
using System.Device.Location;

namespace Common
{
    public static class GoogleMapHelper
    {
        public static double GetLongitudeByKeywords(string street) =>
            new GMapControl().GetPositionByKeywords(street).Lng;

        public static double GetLatitudeByKeywords(string street) =>
            new GMapControl().GetPositionByKeywords(street).Lat;

        public static double GetDistanceBetweenTwoKeywords(string fromKey, string toKey)
        {
            var fromLatitude = GetLatitudeByKeywords(fromKey);
            var fromLongitude = GetLongitudeByKeywords(fromKey);
            var fromGeo = new GeoCoordinate(latitude: fromLatitude, longitude: fromLongitude);

            var toLatitude = GetLatitudeByKeywords(toKey);
            var toLongitude = GetLongitudeByKeywords(toKey);
            var toGeo = new GeoCoordinate(latitude: toLatitude, longitude: toLongitude);

            return fromGeo.GetDistanceTo(toGeo);
        }

        public static double GetDistanceBetweenThreeKeywords(string firstKey,
            string secondKey, string thirdKey)
        {
            var d1 = GetDistanceBetweenTwoKeywords(fromKey: firstKey, toKey: secondKey);

            var d2 = GetDistanceBetweenTwoKeywords(fromKey: firstKey, toKey: thirdKey);

            var d3 = GetDistanceBetweenTwoKeywords(fromKey: secondKey, toKey: thirdKey);

            var distance = (d1 + d2 + d3) / 3;

            return distance;
        }
    }
}
