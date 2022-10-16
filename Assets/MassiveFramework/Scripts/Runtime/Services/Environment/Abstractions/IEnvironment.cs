namespace MassiveCore.Framework
{
    public interface IEnvironment
    {
        EnvironmentConfig ConfigBy(string name);
        void ApplyConfig(string name);
    }
}
