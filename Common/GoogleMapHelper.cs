using GMap.NET.WindowsPresentation;
using System.Device.Location;

namespace Common
{
    public static class GoogleMapHelper
    {
        public static (double Latitude, double Longitude) GetLatitudeAndLongitudeByKeywords(string street)
        {
            var data = new GMapControl().GetPositionByKeywords(street);
            return (Latitude: data.Lat, Longitude: data.Lng);
        }

        public static double GetDistanceBetweenTwoKeywords(string fromKey, string toKey)
        {
            (var fromLatitude, var fromLongitude) = GetLatitudeAndLongitudeByKeywords(fromKey);

            var fromGeo = new GeoCoordinate(latitude: fromLatitude, longitude: fromLongitude);

            (var toLatitude, var toLongitude) = GetLatitudeAndLongitudeByKeywords(street: toKey);

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
