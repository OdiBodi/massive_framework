using System;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ItemView : BaseMonoBehaviour
    {
        [SerializeField]
        private Button button;

        protected ItemModel Model { get; private set; }

        public event Action<ItemModel> OnClicked;

        protected void Awake()
        {
            SubscribeOnButton();
        }

        private void SubscribeOnButton()
        {
            button.onClick.AddListener(() => OnClicked?.Invoke(Model));
        }

        public virtual void Init(ItemModel model)
        {
            Model = model;
        }
    }
}
