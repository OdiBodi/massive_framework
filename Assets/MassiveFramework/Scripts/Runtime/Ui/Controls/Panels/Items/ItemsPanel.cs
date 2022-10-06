using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ItemsPanel : BaseMonoBehaviour
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private ItemView _itemViewPrefab;

        private ItemModel[] _model;

        public event Action<ItemView, ItemModel> OnItemClicked;

        public ItemModel[] Model
        {
            get => _model;
            set
            {
                _model = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            foreach (var itemModel in _model)
            {
                var itemView = Instantiate(_itemViewPrefab, _content);
                itemView.Initialize(itemModel);
                itemView.OnClicked += model => OnItemClicked?.Invoke(itemView, itemModel);
            }
        }
    }
}
