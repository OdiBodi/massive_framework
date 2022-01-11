using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class FloatExtensions
    {
        public static Vector2 ToVector2(this float value)
        {
            return new Vector2(value, value);
        }

        public static bool EqualsTo(this float a, float b, float error)
        {
            return Math.Abs(a - b) < error;
        }
    }
}
