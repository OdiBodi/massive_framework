namespace MassiveCore.Framework
{
    public interface IConfigs
    {
        T Config<T>() where T : Config;
    }
}
