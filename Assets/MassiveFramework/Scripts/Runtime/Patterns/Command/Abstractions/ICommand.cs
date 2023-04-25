namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface ICommand<out T>
    {
        T Execute();
    }
}
