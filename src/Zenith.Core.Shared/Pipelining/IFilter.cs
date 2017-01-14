namespace Zenith.Core.Shared.Pipelining
{
    public interface IFilter<T>
    {
        T Execute(T input);
        void Register(IFilter<T> nextFilter);
    }
}
