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
        private Canvas canvas;

        protected AsyncSubject<ScreenClosingResult> closeSubject = new AsyncSubject<ScreenClosingResult>();

        public Canvas Canvas => canvas;
        public int Order
        {
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }

        protected virtual void OnDestroy()
        {
            TriggerCloseResult(ScreenClosingResult.Close);
        }

        public async UniTask<ScreenClosingResult> WaitForClose()
        {
            return await closeSubject.ToTask();
        }

        private void TriggerCloseResult(ScreenClosingResult result)
        {
            closeSubject.OnNext(result);
            closeSubject.OnCompleted();
        }

        public void Close(ScreenClosingResult result)
        {
            TriggerCloseResult(result);
            Destroy(CacheGameObject);
        }
    }
}
