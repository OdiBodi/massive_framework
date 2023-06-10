using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [RequireComponent(typeof(Canvas))]
    public class Screen : BaseMonoBehaviour
    {
        private readonly AsyncSubject<bool> _openingSubject = new();
        private readonly AsyncSubject<ScreenClosingResult> _closingSubject = new();

        public int Order
        {
            get => CacheCanvas.sortingOrder;
            set => CacheCanvas.sortingOrder = value;
        }

        protected virtual void Start()
        {
            TriggerOpen();
        }

        protected virtual void OnDestroy()
        {
            TriggerCloseResult(ScreenClosingResult.Close);
        }

        public async UniTask<bool> WaitForOpening()
        {
            return await _openingSubject.ToTask();
        }

        public async UniTask<ScreenClosingResult> WaitForClosing()
        {
            return await _closingSubject.ToTask();
        }

        protected void TriggerOpen()
        {
            _openingSubject.OnNext(true);
            _openingSubject.OnCompleted();
        }

        protected void TriggerCloseResult(ScreenClosingResult result)
        {
            _closingSubject.OnNext(result);
            _closingSubject.OnCompleted();
        }

        public virtual void Close(ScreenClosingResult result)
        {
            TriggerCloseResult(result);
            Destroy(CacheGameObject);
        }

        public IEnumerable<T> Controls<T>()
            where T : Component
        {
            return CacheGameObject.Descendants().OfComponent<T>();
        }

        public IEnumerable<T> Controls<T>(string[] names)
            where T : Component
        {
            return Controls<T>().Where(x => names.Contains(x.name));
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
            return Controls<T>().First();
        }
        
        public T Control<T>(string name)
            where T : Component
        {
            return Controls<T>(name).First();
        }
    }
}
