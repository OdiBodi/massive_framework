namespace MassiveCore.Framework
{
    public interface IStateContext<T>
    {
        IState<T> CurrentState { get; }
        T ChangeState<S>(IStateArguments arguments = null) where S : class, IState<T>;
    }
}
