namespace MassiveCore.Framework.Runtime
{
    public class ScreenOrientation
    {
        private readonly Resolution _resolution;

        public ScreenOrientation(Resolution resolution)
        {
            _resolution = resolution;
        }

        public Orientation Orientation
        {
            get
            {
                if (_resolution.width == _resolution.height)
                {
                    return Orientation.Square;
                }
                if (_resolution.width < _resolution.height)
                {
                    return Orientation.Portrait;
                }
                return Orientation.Landscape;
            }            
        }
    }
}
