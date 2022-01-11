using System.Linq;
using Unity.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class AutoSceneContext : BaseMonoBehaviour
    {
        [SerializeField]
        private SceneContext sceneContext;

        private void Awake()
        {
            sceneContext.Installers = CacheGameObject.AfterSelf().OfComponent<MonoInstaller>()
                .Where(x => x.gameObject.activeInHierarchy);
        }
    }
}
