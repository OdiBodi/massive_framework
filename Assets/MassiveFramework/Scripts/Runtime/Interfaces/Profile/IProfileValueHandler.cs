namespace MassiveCore.Framework
{
    public interface IProfileValueHandler
    {
        void Load(string id, object value);
        void Save(string id, object value);
    }
}
