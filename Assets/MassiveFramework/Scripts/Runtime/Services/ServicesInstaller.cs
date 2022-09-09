using System.Linq;
using Unity.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class ServicesInstaller : BaseMonoBehaviour
    {
        [SerializeField]
        private SceneContext sceneContext;

        private void Awake()
        {
            sceneContext.Installers = CacheGameObject.Descendants().OfType<IServiceInstaller>()
                .Select(installer => installer.Installer());
        }
    }
}
