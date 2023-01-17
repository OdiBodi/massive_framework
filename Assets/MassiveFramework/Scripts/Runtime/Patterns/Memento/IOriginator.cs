namespace MassiveCore.Framework.Runtime
{
    public interface IOriginator<out T>
    {
        IMemento Memento();
        T RestoreFromMemento(IMemento memento);
    }
}
