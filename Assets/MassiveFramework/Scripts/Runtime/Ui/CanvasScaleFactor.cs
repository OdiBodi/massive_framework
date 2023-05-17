using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class CanvasScaleFactor
    {
        private readonly Resolution _referenceResolution;
        private readonly Resolution _screenResolution;

        public CanvasScaleFactor(Resolution referenceResolution, Resolution screenResolution)
        {
            _referenceResolution = referenceResolution;
            _screenResolution = screenResolution;
        }

        public float MatchFactor(float matchWidthOrHeight)
        {
            var logWidth = Mathf.Log(_screenResolution.width / (float) _referenceResolution.width, 2f);
            var logHeight = Mathf.Log(_screenResolution.height / (float) _referenceResolution.height, 2f);
            var logWeightedAverage = Mathf.Lerp(logWidth, logHeight, matchWidthOrHeight);
            var scaleFactor = Mathf.Pow(2f, logWeightedAverage);
            return scaleFactor;
        }

        public float ExpandFactor()
        {
            return Mathf.Min(_screenResolution.width / _referenceResolution.width, _screenResolution.height / _referenceResolution.height);
        }

        public float ShrinkFactor()
        {
            return Mathf.Max(_screenResolution.width / _referenceResolution.width, _screenResolution.height / _referenceResolution.height);
        }
    }
}
