using System;

namespace Common.Extensions
{
    public static class Extensions
    {
        public static void Also(this object obj, Action action)
        {
            action.Invoke();
        }

        public static TR Let<T, TR>(this T receiver, Func<T, TR> block)
        {
            return block(receiver);
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
    }
}