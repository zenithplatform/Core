namespace Zenith.Core.Models.DeepSpaceNetwork
{
    public class Coordinates
    {
        public GeoPoint Latitude;
        public GeoPoint Longitude;
    }

    public class GeoPoint
    {
        public MeasuredValue<double> Degrees;
        public MeasuredValue<double> Minutes;
        public MeasuredValue<double> Seconds;
    }
}
