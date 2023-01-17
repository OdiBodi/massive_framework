namespace MassiveCore.Framework.Runtime
{
    public interface IEnvironment
    {
        EnvironmentConfig ConfigBy(string name);
        void ApplyConfig(string name);
    }
}
