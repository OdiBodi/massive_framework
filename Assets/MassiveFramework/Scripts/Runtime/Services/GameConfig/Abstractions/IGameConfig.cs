namespace MassiveCore.Framework
{
    public interface IGameConfig
    {
        public T Config<T>() where T : Config;
    }
}
