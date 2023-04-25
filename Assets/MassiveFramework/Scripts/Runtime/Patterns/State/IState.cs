namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface IState<out T>
    {
        T Enter(IStateArguments arguments);
        T Exit();
    }
}
