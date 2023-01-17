namespace MassiveCore.Framework.Runtime
{
    public interface IState<out T>
    {
        T Enter(IStateArguments arguments);
        T Exit();
    }
}
