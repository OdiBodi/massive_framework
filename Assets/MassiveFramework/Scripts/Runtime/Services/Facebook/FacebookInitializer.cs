using Cysharp.Threading.Tasks;
using Facebook.Unity;

namespace MassiveCore.Framework
{
    public class FacebookInitializer : ServiceInitializer
    {
        public override UniTask<bool> Initialize()
        {
            InitializeFacebook();
            return base.Initialize();
        }

        private void InitializeFacebook()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(OnFacebookInitialized);
            }
            else
            {
                OnFacebookInitialized();
            }
        }

        private void OnFacebookInitialized()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                CompleteInitialize();
                logger.Print("Facebook SDK initialize success!");
            }
            else
            {
                logger.Print("Facebook SDK initialize failed!");
            }
        }
    }
}
