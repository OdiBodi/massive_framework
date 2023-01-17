using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public class ProfileReactiveValueHandler<T> : IProfileValueHandler
    {
        public virtual void Load(string id, object value)
        {
            var property = (ReactiveProperty<T>) value;
            Load(id, property);
        }

        public void Save(string id, object value)
        {
            var property = (ReactiveProperty<T>) value;
            Save(id, property);
        }

        protected virtual void Load(string id, ReactiveProperty<T> property)
        {
        }

        protected virtual void Save(string id, ReactiveProperty<T> property)
        {
        }
    }
}
