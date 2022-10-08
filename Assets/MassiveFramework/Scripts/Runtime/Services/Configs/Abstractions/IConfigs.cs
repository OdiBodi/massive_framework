namespace MassiveCore.Framework
{
    public interface IConfigs
    {
        public T Config<T>() where T : Config;
    }
}
