using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [Serializable]
    public struct PlayerLoopSystem
    {
        [ToggleGroup("enabled", "$typeName")]
        public bool enabled;

        [HideInInspector]
        public string typeName;

        [HideInInspector]
        public string typeAssemblyQualifiedName;

        public bool Valid => !string.IsNullOrEmpty(typeName);
        public Type Type => Type.GetType(typeAssemblyQualifiedName);
    }
}
