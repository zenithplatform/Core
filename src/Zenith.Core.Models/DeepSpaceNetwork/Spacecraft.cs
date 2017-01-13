namespace Zenith.Core.Models.DeepSpaceNetwork
{
    public class Spacecraft
    {
        public string Identifier;
        public string ExplorerName;
        public string Name;
        public MeasuredValue<double> UplegRange;
        public MeasuredValue<double> DownlegRange;
        public MeasuredValue<double> RoundtripLightTime;
    }
}
