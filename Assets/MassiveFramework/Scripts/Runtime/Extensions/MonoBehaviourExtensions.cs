using UnityEngine;

namespace MassiveCore.Framework
{
    public static class MonoBehaviourExtensions
    {
        public static void ChangeActivity(this MonoBehaviour monoBehaviour, bool value)
        {
            monoBehaviour.gameObject.SetActive(value);
        }

        public static bool Activity(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.gameObject.activeInHierarchy;
        }
    }
}
