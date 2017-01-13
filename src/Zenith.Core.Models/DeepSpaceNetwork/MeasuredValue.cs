namespace Zenith.Core.Models.DeepSpaceNetwork
{
    public class MeasuredValue<T> where T : struct
    {
        T Value { get; set; }
        string Units { get; set; }
    }
}
