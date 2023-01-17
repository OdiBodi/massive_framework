namespace MassiveCore.Framework.Runtime
{
    public interface IResources
    {
        void BindResource<T>(T resource) where T : class, IResource;
        T Resource<T>() where T : class, IResource;
    }
}
