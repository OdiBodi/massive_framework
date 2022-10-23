namespace MassiveCore.Framework
{
    public interface ICommand<out T>
    {
        T Execute();
    }
}
