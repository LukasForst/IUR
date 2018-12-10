using System;

namespace Common.Extensions
{
    public static class Extensions
    {
        public static void Also(this object obj, Action action)
        {
            action.Invoke();
        }
    }
}