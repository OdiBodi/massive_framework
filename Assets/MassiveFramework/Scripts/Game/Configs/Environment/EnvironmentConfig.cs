using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "environment_config", menuName = "Massive Framework/Configs/Environment Config")]
    public class EnvironmentConfig : ScriptableObject
    {
        [Serializable]
        public struct DirectionalLight
        {
            public Quaternion rotation;
            public Color color;
            [Space]
            public LightShadows shadowType;
            [Range(0f, 1f), ShowIf("@shadowType != LightShadows.None")]
            public float shadowStrength;

            public void ApplyTo(Light light)
            {
                light.transform.rotation = rotation;
                light.color = color;
                light.shadows = shadowType; 
                light.shadowStrength = shadowStrength;
            }
        }

        [Serializable]
        public struct AmbientLighting
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

        [SerializeField]
        private DirectionalLight directionalLight = new DirectionalLight
        {
            rotation = Quaternion.identity,
            color = Color.white,
            shadowType = LightShadows.None,
            shadowStrength = 1f
        };

        [SerializeField]
        private AmbientLighting ambientLighting = new AmbientLighting
        {
            shadowColor = new Color(0.42f, 0.48f, 0.63f),
            type = AmbientLighting.Type.Flat,
            color = new Color(0.21f, 0.23f, 0.26f),
            skyColor = new Color(0.21f, 0.23f, 0.26f),
            equatorColor = new Color(0.11f, 0.13f, 0.13f),
            groundColor = new Color(0.05f, 0.04f, 0.04f)
        };

        public DirectionalLight DirectionalLightConfig => directionalLight;
        public AmbientLighting AmbientLightingConfig => ambientLighting;
    }
}
