namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface ICommandInvoker<out T>
    {
        T Execute();
    }
}
