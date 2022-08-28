using UniRx;

namespace MassiveCore.Framework
{
    public class ProfileReactiveCollectionHandler<T> : IProfileValueHandler
    {
        public virtual void Load(string id, object value)
        {
            var collection = (ReactiveCollection<T>) value;
            Load(id, collection);
        }

        public void Save(string id, object value)
        {
            var collection = (ReactiveCollection<T>) value;
            Save(id, collection);
        }

        protected virtual void Load(string id, ReactiveCollection<T> collection)
        {
        }

        protected virtual void Save(string id, ReactiveCollection<T> collection)
        {
        }
    }
}
