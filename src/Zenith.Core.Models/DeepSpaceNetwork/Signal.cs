namespace Zenith.Core.Models.DeepSpaceNetwork
{
    public class Signal
    {
        public Direction Direction;
        public SignalType Type;
        public string Debug;
        public MeasuredValue<double> DataRate;
        public MeasuredValue<double> Frequency;
        public MeasuredValue<double> Power;
        public Spacecraft Spacecraft;
    }

    public enum Direction
    {
        Up,
        Down
    }

    public enum SignalType
    {
        None,
        Data,
        Carrier
    }
}
