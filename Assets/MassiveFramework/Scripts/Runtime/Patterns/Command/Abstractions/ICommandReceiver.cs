namespace MassiveCore.Framework.Runtime
{
    public interface ICommandReceiver<out T>
    {
        T Execute();
    }
}
