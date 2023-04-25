namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface IOriginator<out T>
    {
        IMemento Memento();
        T RestoreFromMemento(IMemento memento);
    }
}
