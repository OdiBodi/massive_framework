using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime.Patterns
{
    public class DelayCommand : ICommand<UniTask>
    {
        private readonly TimeSpan _timeSpan;

        public DelayCommand(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public UniTask Execute()
        {
            return UniTask.Delay(_timeSpan);
        }
    }
}
