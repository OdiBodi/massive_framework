using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ItemsPanel : BaseMonoBehaviour
    {
        [SerializeField]
        private RectTransform content;

        [SerializeField]
        private ItemView itemViewPrefab;

        private ItemModel[] model;

        public event Action<ItemView, ItemModel> OnItemClicked;
        
        public ItemModel[] Model
        {
            get => model;
            set
            {
                model = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            foreach (var itemModel in model)
            {
                var itemView = Instantiate(itemViewPrefab, content);
                itemView.Init(itemModel);
                itemView.OnClicked += model => OnItemClicked?.Invoke(itemView, itemModel);
            }
        }
    }
}
