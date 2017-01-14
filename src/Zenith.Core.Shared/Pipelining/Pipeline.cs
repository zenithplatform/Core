namespace Zenith.Core.Shared.Pipelining
{
    public class Pipeline<T> : IFilterChain<T>
    {
        IFilter<T> _root;

        public void Execute(T input)
        {
            _root.Execute(input);
        }

        public IFilterChain<T> Register(IFilter<T> nextFilter)
        {
            if (_root == null)
                _root = nextFilter;
            else
                _root.Register(nextFilter);

            return this;
        }
    }
}

/*

    //Usage :
    var input = "Test string";
    Console.WriteLine("Original : {0}", input);

    Pipeline<string> pipeline = new Pipeline<string>();
    pipeline.Register(new ToLowercase())
            .Register(new ToUpper())
            .Register(new ToLowercase())
            .Register(new Reverse())
            .Execute(input);

    Console.ReadLine();

*/
