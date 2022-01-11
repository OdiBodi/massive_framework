using UnityEngine;

namespace MassiveCore.Framework
{
    public static class IntExtensions
    {
        public static int RandomRangeExcept(this int except, int min, int max)
        {
            if (max-min <= 1)
            {
                return min;
            }
            int number;
            do
            {
                number = Random.Range(min, max);
            }
            while (number == except);
            return number;
        }
    }
}
