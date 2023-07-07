using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime.Patterns
{
    public class AsyncCommandsInvoker : ICommandInvoker<UniTask>, IEnumerable<ICommand<UniTask>>
    {
        private readonly List<ICommand<UniTask>> _commands;

        public AsyncCommandsInvoker(IEnumerable<ICommand<UniTask>> commands)
        {
            _commands = new List<ICommand<UniTask>>(commands);
        }

        public async UniTask Execute()
        {
            foreach (var command in _commands)
            {
                await command.Execute();
            }
        }

        public IEnumerator<ICommand<UniTask>> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
