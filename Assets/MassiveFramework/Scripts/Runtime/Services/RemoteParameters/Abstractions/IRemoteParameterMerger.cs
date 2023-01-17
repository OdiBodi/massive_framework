namespace MassiveCore.Framework.Runtime
{
    public interface IRemoteParameterMerger
    {
        (string localValue, string remoteValue) Merge(IRemoteParameter thisParameter, IRemoteParameter otherParameter);
    }
}
