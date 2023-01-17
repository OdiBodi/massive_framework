using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MassiveCore.Framework.Runtime
{
    public class SceneLoaderInitializer : ServiceInitializer
    {
        [SerializeField]
        private string _sceneName;

        public override async UniTask<bool> Initialize()
        {
            await LoadScene();
            UnloadCurrentScene();
            return await base.Initialize();
        }

        private async UniTask LoadScene()
        {
            await SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive).AsAsyncOperationObservable();

            var mainScene = SceneManager.GetSceneByName(_sceneName);

            var servicesInitializer = mainScene.GetRootGameObjects().OfComponent<ServicesInitializer>().First();
            var servicesInitialized = servicesInitializer.Initialized;
            if (servicesInitialized.Value)
            {
                return;
            }
            await servicesInitialized.Where(result => result).ToReadOnlyReactiveProperty();
        }

        private void UnloadCurrentScene()
        {
            SceneManager.UnloadSceneAsync(CacheGameObject.scene);
        }
    }
}
