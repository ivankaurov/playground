namespace Playground.IDP.Application
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class DictionaryExtensions
    {
        public static IEnumerable<TValue> Find<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            IEnumerable<TKey> keys)
        {
            return keys
                .Select(key =>
                {
                    var found = dictionary.TryGetValue(key, out var res);
                    return (Found: found, Result: res);
                })
                .Where(c => c.Found)
                .Select(c => c.Result);
        }
    }
}