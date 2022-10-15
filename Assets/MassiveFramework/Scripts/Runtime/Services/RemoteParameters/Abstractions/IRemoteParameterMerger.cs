namespace MassiveCore.Framework
{
    public interface IRemoteParameterMerger
    {
        (string localValue, string remoteValue) Merge(IRemoteParameter thisParameter, IRemoteParameter otherParameter);
    }
}
