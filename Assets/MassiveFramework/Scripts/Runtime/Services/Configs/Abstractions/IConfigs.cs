namespace MassiveCore.Framework.Runtime
{
    public interface IConfigs
    {
        T Config<T>() where T : Config;
    }
}
