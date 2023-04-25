using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime.Ui.ItemsPanel
{
    public class ItemView : BaseMonoBehaviour, IAbstractProduct
    {
        public virtual void Initialize(IItemModel model)
        {
            Model = model;
        }

        public IItemModel Model { get; private set; }
    }
}
