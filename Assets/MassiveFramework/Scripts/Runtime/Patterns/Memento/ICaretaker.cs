namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface ICaretaker<out T>
    {
        T Save();
        T Restore();
    }
}
