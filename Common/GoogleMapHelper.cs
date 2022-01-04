using GMap.NET.WindowsPresentation;

namespace Common
{
    public static class GoogleMapHelper
    {
        public static double GetLongitudeByKeywords(string street) =>
            new GMapControl().GetPositionByKeywords(street).Lng;

        public static double GetLatitudeByKeywords(string street) =>
            new GMapControl().GetPositionByKeywords(street).Lat;
    }
}
