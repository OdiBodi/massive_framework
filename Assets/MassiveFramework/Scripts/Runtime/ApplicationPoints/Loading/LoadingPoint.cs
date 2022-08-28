using System;
using System.Linq;
using UniRx;
using Unity.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MassiveCore.Framework
{
    public class LoadingPoint : ApplicationPoint
    {
        [SerializeField]
        private string scene;

        public override async void Init()
        {
            await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive).AsAsyncOperationObservable();
            SubscribeOnLoadedScene(UnloadCurrentScene);
            Complete();
        }

        private void SubscribeOnLoadedScene(Action onLoaded)
        {
            var mainScene = SceneManager.GetSceneByName(scene);
            var entryPoint = mainScene.GetRootGameObjects().OfComponent<EntryPoint>().First();
            if (entryPoint.Loaded)
            {
                onLoaded?.Invoke();
            }
            else
            {
                entryPoint.OnLoaded += () => onLoaded?.Invoke();
            }
        }

        private void UnloadCurrentScene()
        {
            SceneManager.UnloadSceneAsync(CacheGameObject.scene);
        }
    }
}
