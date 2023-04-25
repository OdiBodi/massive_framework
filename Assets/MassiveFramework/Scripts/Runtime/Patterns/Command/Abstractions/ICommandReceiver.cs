namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface ICommandReceiver<out T>
    {
        T Execute();
    }
}
