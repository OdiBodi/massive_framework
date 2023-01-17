using Cysharp.Threading.Tasks;
using Facebook.Unity;

namespace MassiveCore.Framework.Runtime
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
            var result = FB.IsInitialized;
            if (result)
            {
                FB.ActivateApp();
                _logger.Print("Facebook SDK initialize success!");
            }
            else
            {
                _logger.Print("Facebook SDK initialize failed!");
            }
            CompleteInitialize(result);
        }
    }
}
