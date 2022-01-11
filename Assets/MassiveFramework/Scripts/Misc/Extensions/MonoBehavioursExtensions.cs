using UnityEngine;

namespace MassiveCore.Framework
{
    public static class MonoBehavioursExtensions
    {
        public static void SetActive(this MonoBehaviour monoBehaviour, bool active)
        {
            monoBehaviour.gameObject.SetActive(active);
        }
    }
}
