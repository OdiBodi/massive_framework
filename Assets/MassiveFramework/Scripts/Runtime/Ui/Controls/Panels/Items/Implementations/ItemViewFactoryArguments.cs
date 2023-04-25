using MassiveCore.Framework.Runtime.Patterns;
using UnityEngine;

namespace MassiveCore.Framework.Runtime.Ui.ItemsPanel
{
    public class ItemViewFactoryArguments : IAbstractFactoryArguments
    {
        public ItemViewFactoryArguments(ItemView prefab, RectTransform content)
        {
            Prefab = prefab;
            Content = content;
        }

        public ItemView Prefab { get; private set; }
        public RectTransform Content { get; private set; }
    }
}
