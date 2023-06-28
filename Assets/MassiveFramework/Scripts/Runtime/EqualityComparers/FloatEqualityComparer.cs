using System.Collections.Generic;

namespace MassiveCore.Framework.Runtime
{
    public class FloatEqualityComparer : IEqualityComparer<float>
    {
        public static readonly FloatEqualityComparer Default = new(0.01f);

        private readonly float _error;

        public FloatEqualityComparer(float error)
        {
            _error = error;
        }

        public bool Equals(float a, float b)
        {
            return a.EqualsTo(b, _error);
        }

        public int GetHashCode(float value)
        {
            return 0;
        }
    }
}
