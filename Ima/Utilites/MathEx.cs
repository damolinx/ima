using System;

namespace Ima.Utilites
{
    internal static class MathEx
    {
        public static T Clamp<T>(T value, T min, T max)
            where T : IComparable
        {
            var result = value;
            if (value.CompareTo(min) < 0)
            {
                result = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = max;
            }
            return result;
        }
    }
}
