using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "environment_config", menuName = "Massive Framework/Configs/Environment Config")]
    public class EnvironmentConfig : ScriptableObject
    {
        [SerializeField]
        private DirectionalLightParams directionalLightParams = new()
        {
            rotation = Vector2.zero,
            color = Color.white,
            shadowType = LightShadows.None,
            shadowStrength = 1f
        };

        [SerializeField]
        private AmbientLightingParams ambientLightingParams = new()
        {
            shadowColor = new Color(0.42f, 0.48f, 0.63f),
            type = AmbientLightingParams.Type.Flat,
            color = new Color(0.21f, 0.23f, 0.26f),
            skyColor = new Color(0.21f, 0.23f, 0.26f),
            equatorColor = new Color(0.11f, 0.13f, 0.13f),
            groundColor = new Color(0.05f, 0.04f, 0.04f)
        };

        public DirectionalLightParams DirectionalLightParams => directionalLightParams;
        public AmbientLightingParams AmbientLightingParams => ambientLightingParams;
    }
}
