using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class Screen : BaseMonoBehaviour
    {
        public class Factory : PlaceholderFactory<Type, Screen>
        {
        }

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
    }
}
