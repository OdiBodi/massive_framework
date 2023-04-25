namespace MassiveCore.Framework.Runtime
{
    public class InertiaInitialVelocity
    {
        private readonly float _distance;
        private readonly float _dumping;

        public InertiaInitialVelocity(float distance, float dumping)
        {
            _distance = distance;
            _dumping = dumping;
        }

        public float Velocity()
        {
            return (2f * _distance) / (_dumping * _dumping);
        }
    }
}
