namespace MassiveCore.Framework.Runtime
{
    public interface ICaretaker<out T>
    {
        T Save();
        T Restore();
    }
}
