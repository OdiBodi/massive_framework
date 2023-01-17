namespace MassiveCore.Framework.Runtime
{
    public interface ICommand<out T>
    {
        T Execute();
    }
}
