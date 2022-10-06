using Zenject;

namespace MassiveCore.Framework
{
    public class VisualEffects : IVisualEffects
    {
        [Inject]
        private readonly IPool _pool;

        public IVisualEffect VisualEffect(string id)
        {
            return _pool.Request<VisualEffect>(id);
        }
    }
}
