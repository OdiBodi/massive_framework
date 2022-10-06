using System;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ItemView : BaseMonoBehaviour
    {
        [SerializeField]
        private Button _button;

        protected ItemModel Model { get; private set; }

        public event Action<ItemModel> OnClicked;

        protected void Awake()
        {
            SubscribeOnButton();
        }

        private void SubscribeOnButton()
        {
            _button.onClick.AddListener(() => OnClicked?.Invoke(Model));
        }

        public virtual void Initialize(ItemModel model)
        {
            Model = model;
        }
    }
}
