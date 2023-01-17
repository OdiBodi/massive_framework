using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [Serializable]
    public struct DirectionalLightParams
    {
        public Vector2 rotation;
        public Color color;
        [Space]
        public LightShadows shadowType;
        [Range(0f, 1f), ShowIf("@shadowType != LightShadows.None")]
        public float shadowStrength;

        public void ApplyTo(Light light)
        {
            light.transform.eulerAngles = rotation;
            light.color = color;
            light.shadows = shadowType; 
            light.shadowStrength = shadowStrength;
        }
    }
}
