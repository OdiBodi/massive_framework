namespace MassiveCore.Framework
{
    public interface IOriginator<out T>
    {
        IMemento Memento();
        T RestoreFromMemento(IMemento memento);
    }
}
