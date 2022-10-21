namespace MassiveCore.Framework
{
    public interface IOriginator
    {
        IMemento Memento();
        void RestoreFromMemento(IMemento memento);
    }
}
