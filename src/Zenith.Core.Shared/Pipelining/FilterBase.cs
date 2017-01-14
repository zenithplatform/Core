namespace Zenith.Core.Shared.Pipelining
{
    public abstract class FilterBase<T> : IFilter<T>
    {
        IFilter<T> _next;
        protected abstract T Process(T input);

        public T Execute(T input)
        {
            T result = Process(input);
            if (_next != null) result = _next.Execute(result);

            return result;
        }

        public void Register(IFilter<T> nextFilter)
        {
            if (_next == null)
                _next = nextFilter;
            else
                _next.Register(nextFilter);
        }
    }
}
