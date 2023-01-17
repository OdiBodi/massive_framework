using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void DestroyImmediateChildren(this Transform transform)
        {
            while (transform.childCount != 0)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}
