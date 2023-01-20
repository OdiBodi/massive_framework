using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [Serializable]
    public struct PlayerLoopPhase
    {
        public bool enabled;

        [HideInInspector]
        public string typeAssemblyQualifiedName;

        [ListDrawerSettings(IsReadOnly = true, Expanded = true)]
        public PlayerLoopSystem[] systems;

        public Type Type => Type.GetType(typeAssemblyQualifiedName);
        public bool Valid => systems is { Length: > 0 };
    }
}
