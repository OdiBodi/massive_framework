using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime.Patterns
{
    public class AsyncAbortedCommandsInvoker<T> : ICommandInvoker<UniTask<(bool Abort, T Result)>>,
        IEnumerable<ICommand<UniTask<(bool Abort, T Result)>>>
    {
        private readonly List<ICommand<UniTask<(bool Abort, T Result)>>> _commands;

        public AsyncAbortedCommandsInvoker(IEnumerable<ICommand<UniTask<(bool Abort, T Result)>>> commands)
        {
            _commands = new List<ICommand<UniTask<(bool Abort, T Result)>>>(commands);
        }

        public async UniTask<(bool Abort, T Result)> Execute()
        {
            foreach (var command in _commands)
            {
                var (abort, result) = await command.Execute();
                if (abort)
                {
                    return (true, result);
                }
            }
            return (false, default);
        }

        public IEnumerator<ICommand<UniTask<(bool, T)>>> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
