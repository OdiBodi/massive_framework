using System.Linq;
using Unity.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ServicesInstaller : BaseMonoBehaviour
    {
        [SerializeField]
        private SceneContext _sceneContext;

        private void Awake()
        {
            _sceneContext.Installers = CacheGameObject.Descendants().OfInterfaceComponent<IServiceInstaller>()
                .Select(installer => installer.Installer()).Where(installer => installer.Activity());
        }
    }
}
