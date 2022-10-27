namespace MassiveCore.Framework
{
    public interface ICaretaker<out T>
    {
        T Save();
        T Restore();
    }
}
