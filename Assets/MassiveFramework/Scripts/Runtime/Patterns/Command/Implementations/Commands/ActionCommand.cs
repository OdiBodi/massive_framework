using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime.Patterns
{
    public class ActionCommand : ICommand<UniTask>
    {
        private readonly Func<UniTask> _action;

        public ActionCommand(Func<UniTask> action)
        {
            _action = action;
        }

        public UniTask Execute()
        {
            return _action();
        }
    }
}
