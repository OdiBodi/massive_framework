using MassiveCore.Framework.Runtime.Patterns;
using UnityEngine;

namespace MassiveCore.Framework.Runtime.Ui.ItemsPanel
{
    public class ItemViewFactory : IAbstractFactory<ItemView>
    {
        public ItemView Product(IAbstractFactoryArguments arguments)
        {
            var args = arguments as ItemViewFactoryArguments;
            var prefab = args.Prefab;
            var content = args.Content;
            var itemView = GameObject.Instantiate(prefab, content);
            itemView.name = "item";
            return itemView;
        }
    }
}
