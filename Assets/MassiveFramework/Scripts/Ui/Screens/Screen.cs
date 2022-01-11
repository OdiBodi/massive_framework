using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public enum ClosingResult
    {
        Close
    }

    public class Screen : BaseMonoBehaviour
    {
        public class Factory : PlaceholderFactory<Type, Screen>
        {
        }

        [SerializeField]
        private Canvas canvas;

        protected AsyncSubject<ClosingResult> closeSubject = new AsyncSubject<ClosingResult>();

        public int Order
        {
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }

        protected virtual void OnDestroy()
        {
            TriggerCloseResult(ClosingResult.Close);
        }

        public async Task<ClosingResult> WaitForClose()
        {
            return await closeSubject.ToTask();
        }

        private void TriggerCloseResult(ClosingResult result)
        {
            closeSubject.OnNext(result);
            closeSubject.OnCompleted();
        }

        public void Close(ClosingResult result)
        {
            TriggerCloseResult(result);
            Destroy(CacheGameObject);
        }
    }
}
