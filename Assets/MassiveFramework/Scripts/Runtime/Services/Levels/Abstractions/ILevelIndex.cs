namespace MassiveCore.Framework.Runtime
{
    public interface ILevelIndex
    {
        int Current();
        void UpdateToNext();
    }
}
