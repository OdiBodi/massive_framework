using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework
{
    [Serializable]
    public struct AmbientLightingParams
    {
        public enum Type
        {
            Flat,
            Trilight
        }

        public Color shadowColor;
        [Space]
        public Type type;
        [ColorUsage(false, true), ShowIf("@type == Type.Flat")]
        public Color color;
        [ColorUsage(false, true),ShowIf("@type == Type.Trilight")]
        public Color skyColor;
        [ColorUsage(false, true),ShowIf("@type == Type.Trilight")]
        public Color equatorColor;
        [ColorUsage(false, true),ShowIf("@type == Type.Trilight")]
        public Color groundColor;

        public void Apply()
        {
            RenderSettings.subtractiveShadowColor = shadowColor;
            switch (type)
            {
                case Type.Flat:
                    RenderSettings.ambientLight = color;
                    break;
                case Type.Trilight:
                    RenderSettings.ambientSkyColor = skyColor;
                    RenderSettings.ambientEquatorColor = equatorColor;
                    RenderSettings.ambientGroundColor = groundColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
