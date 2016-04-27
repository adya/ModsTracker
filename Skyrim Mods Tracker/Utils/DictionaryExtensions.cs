using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Utils
{
    static class DictionaryExtensions
    {
        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (key == null || value == null) return;
            dic.Remove(key);
            dic.Add(key, value);
        }

        public static bool TryCopy<TKey, TValue>(this Dictionary<TKey, TValue> src, TKey key, Dictionary<TKey, TValue> dst)
        {
            bool success = src.ContainsKey(key);
            if (success)
                dst.Set(key, src[key]);
            return success;
        }
    }
}
