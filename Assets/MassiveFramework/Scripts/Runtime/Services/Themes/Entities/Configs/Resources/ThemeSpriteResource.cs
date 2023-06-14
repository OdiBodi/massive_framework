using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [Serializable]
    public struct ThemeSpriteResource
    {
        public string Id;
        [PreviewField(ObjectFieldAlignment.Center, Height = 100)]
        public Sprite Sprite;
    }
}
