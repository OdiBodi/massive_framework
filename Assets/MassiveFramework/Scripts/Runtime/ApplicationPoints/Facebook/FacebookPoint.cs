using Facebook.Unity;

namespace MassiveCore.Framework
{
    public class FacebookPoint : ApplicationPoint
    {
        public override void Init()
        {
            InitFB();
        }

        private void InitFB()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(OnFBInitialized);
            }
            else
            {
                OnFBInitialized();
            }
        }

        private void OnFBInitialized()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                Complete();
                logger.Print("Facebook SDK initialize success!");
            }
            else
            {
                logger.Print("Facebook SDK initialize failed!");
            }
        }
    }
}
