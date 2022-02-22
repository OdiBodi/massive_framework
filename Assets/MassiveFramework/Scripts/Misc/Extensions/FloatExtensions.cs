using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class FloatExtensions
    {
        public static bool EqualsTo(this float a, float b, float error = 0.01f)
        {
            return Math.Abs(a - b) < error;
        }

        public static Vector2 ToVector2(this float value)
        {
            return new Vector2(value, value);
        }
    }
}
