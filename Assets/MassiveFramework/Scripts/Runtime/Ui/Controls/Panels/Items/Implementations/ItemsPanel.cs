using System.Collections;
using System.Collections.Generic;
using MassiveCore.Framework.Runtime.Patterns;
using UnityEngine;

namespace MassiveCore.Framework.Runtime.Ui.ItemsPanel
{
    public class ItemsPanel : BaseMonoBehaviour, IEnumerable<(IItemModel, ItemView)>
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private ItemView _itemViewPrefab;

        private IAbstractFactoryArguments _itemViewFactoryArguments;
        private IItemModel[] _itemModels;

        public IAbstractFactory<ItemView> ItemViewFactory { get; set; } = new ItemViewFactory();
        public ItemView[] ItemViews { get; set; }
        public IItemModel[] ItemModels
        {
            get => _itemModels;
            set
            {
                _itemModels = value;
                ClearView();
                UpdateView();
            }
        }

        private void Awake()
        {
            InitializeItemViewFactoryArguments();
        }

        private void InitializeItemViewFactoryArguments()
        {
            _itemViewFactoryArguments = new ItemViewFactoryArguments(_itemViewPrefab, _content);
        }

        private void ClearView()
        {
            _content.DestroyChildren();
        }

        private void UpdateView()
        {
            var count = ItemModels.Length; 
            ItemViews = new ItemView[count];
            for (var i = 0; i < count; ++i)
            {
                var itemModel = ItemModels[i];
                var itemView = ItemViewFactory.Product(_itemViewFactoryArguments);
                itemView.Initialize(itemModel);
                ItemViews[i] = itemView;
            }
        }

        public IEnumerator<(IItemModel, ItemView)> GetEnumerator()
        {
            for (var i = 0; i < ItemModels.Length; ++i)
            {
                var model = ItemModels[i];
                var view = ItemViews[i];
                yield return (model, view);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
