using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class FrameratePoint : ApplicationPoint
    {
        public enum Rate
        {
            [Number(30)]
            _30,
            [Number(60)]
            _60
        }

        [SerializeField]
        private Rate rate = Rate._30;

        public override void Init()
        {
            Application.targetFrameRate = rate.Number();
            Complete();
        }
    }
}
