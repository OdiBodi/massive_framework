namespace MassiveCore.Framework
{
    public class RemoteParameterMerger : IRemoteParameterMerger
    {
        public (string localValue, string remoteValue) Merge(IRemoteParameter thisParameter, IRemoteParameter otherParameter)
        {
            if (thisParameter.RemoteValue == otherParameter.RemoteValue)
            {
                return (thisParameter.LocalValue, thisParameter.RemoteValue);
            }
            return (otherParameter.RemoteValue, otherParameter.RemoteValue);
        }
    }
}
