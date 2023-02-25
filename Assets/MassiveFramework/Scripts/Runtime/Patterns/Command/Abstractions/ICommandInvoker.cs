namespace MassiveCore.Framework.Runtime
{
    public interface ICommandInvoker<out T>
    {
        T Execute();
    }
}
