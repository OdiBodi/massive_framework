using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class Screen : BaseMonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        protected readonly AsyncSubject<ScreenClosingResult> _closingSubject = new();

        public Canvas Canvas => _canvas;
        public int Order
        {
            get => _canvas.sortingOrder;
            set => _canvas.sortingOrder = value;
        }

        protected virtual void OnDestroy()
        {
            TriggerCloseResult(ScreenClosingResult.Close);
        }

        public async UniTask<ScreenClosingResult> WaitForClose()
        {
            return await _closingSubject.ToTask();
        }

        private void TriggerCloseResult(ScreenClosingResult result)
        {
            _closingSubject.OnNext(result);
            _closingSubject.OnCompleted();
        }

        public void Close(ScreenClosingResult result)
        {
            TriggerCloseResult(result);
            Destroy(CacheGameObject);
        }

        public IEnumerable<T> Controls<T>()
            where T : Component
        {
            var components = CacheGameObject.Descendants().OfComponent<T>();
            return components;
        }

        public IEnumerable<T> Controls<T>(string[] names)
            where T : Component
        {
            var components = Controls<T>().Where(x => names.Contains(x.name));
            return components;
        }

        public IEnumerable<T> Controls<T>(string name)
            where T : Component
        {
            var names = new[] { name };
            var components = Controls<T>().Where(x => names.Contains(x.name));
            return components;
        }

        public T Control<T>()
            where T : Component
        {
            var component = Controls<T>().First();
            return component;
        }
        
        public T Control<T>(string name)
            where T : Component
        {
            var component = Controls<T>(name).First();
            return component;
        }
    }
}
