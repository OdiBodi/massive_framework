namespace MassiveCore.Framework.Runtime
{
    public interface IRemoteParameter
    {
        string Name { get; }
        string LocalValue { get; set; }
        string RemoteValue { get; }
        bool Updatable { get; }
        void Merge(IRemoteParameter other, IRemoteParameterMerger merger);
    }
}
