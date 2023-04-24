using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class PlaneDirection2Axis
    {
        public enum Type
        {
            None,
            Horizontal,
            Vertical
        }

        private readonly Vector2 _direction;

        public PlaneDirection2Axis(Vector2 direction)
        {
            _direction = direction;
        }

        public Type Direction()
        {
            var direction = new PlaneDirection4Axis(_direction).Direction();
            if (direction is PlaneDirection4Axis.Type.Left or PlaneDirection4Axis.Type.Right)
            {
                return Type.Horizontal;
            }
            return Type.Vertical;
        }
    }
}
