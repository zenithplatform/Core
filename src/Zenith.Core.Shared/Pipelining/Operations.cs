using System;

namespace Zenith.Core.Shared.Pipelining
{
    //public class ToLowercase : FilterBase<string>
    //{
    //    protected override string Process(string input)
    //    {
    //        string result = input.ToLowerInvariant();
    //        Console.WriteLine(result);

//        return result;
//    }
//}

//public class ToUpper : FilterBase<string>
//{
//    protected override string Process(string input)
//    {
//        string result = input.ToUpperInvariant();
//        Console.WriteLine(result);

//        return result;
//    }
//}

//public class Reverse : FilterBase<string>
//{
//    protected override string Process(string input)
//    {
//        string result = ReverseString(input);
//        Console.WriteLine(result);

//        return result;
//    }

//    string ReverseString(string s)
//    {
//        char[] charArray = s.ToCharArray();
//        Array.Reverse(charArray);
//        return new string(charArray);
//    }
//}

/* Usage :

    static void Main(string[] args)
    {
        var input = "Test string";
        Console.WriteLine("Original : {0}", input);

        Pipeline<string> pipeline = new Pipeline<string>();
        pipeline.Register(new ToLowercase())
                .Register(new ToUpper())
                .Register(new ToLowercase())
                .Register(new Reverse())
                .Execute(input);

        Console.ReadLine();
    }

*/
}
