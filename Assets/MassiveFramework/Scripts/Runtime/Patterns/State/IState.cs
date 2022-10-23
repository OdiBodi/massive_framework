namespace MassiveCore.Framework
{
    public interface IState<out T>
    {
        T Enter(IStateArguments arguments);
        T Exit();
    }
}
