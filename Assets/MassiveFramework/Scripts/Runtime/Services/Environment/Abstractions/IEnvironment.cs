namespace MassiveCore.Framework
{
    public interface IEnvironment
    {
        public EnvironmentConfig ConfigBy(string name);
        public void ApplyConfig(string name);
    }
}
