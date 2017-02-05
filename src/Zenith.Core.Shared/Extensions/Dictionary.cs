using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static string Dump<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var lines = dictionary.Select(kvPair => string.Concat(kvPair.Key, ":", kvPair.Value));
            return string.Join(",", lines);
        }
    }
}
