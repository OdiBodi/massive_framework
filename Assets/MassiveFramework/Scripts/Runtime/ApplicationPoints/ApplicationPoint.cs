using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework
{
    public class ApplicationPoint : BaseMonoBehaviour
    {
        private AsyncSubject<bool> completeSubject = new AsyncSubject<bool>();

        public virtual void Init()
        {
        }

        public async UniTask<bool> WaitForComplete()
        {
            return await completeSubject.ToTask();
        }

        public void Complete()
        {
            completeSubject.OnNext(true);
            completeSubject.OnCompleted();
        }
    }
}
