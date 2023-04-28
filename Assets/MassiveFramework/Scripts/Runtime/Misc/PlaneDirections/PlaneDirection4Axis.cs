using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class PlaneDirection4Axis
    {
        public enum Type
        {
            None,
            Up,
            Right,
            Down,
            Left
        }

        private readonly Vector2 _direction;

        public PlaneDirection4Axis(Vector2 direction)
        {
            _direction = direction;
        }

        public Type Direction()
        {
            var direction = Type.Left;
            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            if (angle is > 45f and < 135f)
            {
                direction = Type.Up;
            }
            else if (angle is > -45f and < 45f)
            {
                direction = Type.Right;
            }
            else if (angle is > -135f and < -45)
            {
                direction = Type.Down;
            }
            // Left: angle is > 135f or < -135f
            return direction;
        }
    }
}
