namespace Zenith.Core.Shared.Pipelining
{
    public interface IFilterChain<T>
    {
        void Execute(T input);
        IFilterChain<T> Register(IFilter<T> nextFilter);
    }
}
