using System;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class Extensions
    {
        public static void Also<T>(this T obj, Action action)
        {
            action.Invoke();
        }

        public static T Also<T>(this T obj, Action<T> action)
        {
            action.Invoke(obj);
            return obj;
        }

        public static TR Let<T, TR>(this T receiver, Func<T, TR> block)
        {
            return block(receiver);
        }

        public static void ForEach<T>(this IEnumerable<T> col, Action<T> execute)
        {
            foreach (var x1 in col) execute(x1);
        }

        public static int? ParseIntOrNull(this string str)
        {
            if (int.TryParse(str, out var result)) return result;
            return null;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}