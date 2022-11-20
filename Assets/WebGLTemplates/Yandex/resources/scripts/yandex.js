YaGames.init().then(ysdk => {
  console.log('Yandex SDK initialized!');
  window.ysdk = ysdk;
});

function showBannerAdv() {
  window.ysdk.adv.showBannerAdv()
}

function hideBannerAdv() {
  window.ysdk.adv.hideBannerAdv()
}

function showFullscreenAdv() {
  window.ysdk.adv.showFullscreenAdv({
    callbacks: {
      onOpen: function() {
        window.unityInstance.SendMessage('yandex', 'OnInterstitialAdsOpened');
      },
      onClose: function(wasShown) {
        window.unityInstance.SendMessage('yandex', 'OnInterstitialAdsClosed', wasShown ? 1 : 0);
      },
      onError: function(error) {
        window.unityInstance.SendMessage('yandex', 'OnInterstitialAdsError', error.message);
      }
    }
  })
}

function showRewardedVideo() {
  window.ysdk.adv.showRewardedVideo({
    callbacks: {
      onOpen: function() {
        window.unityInstance.SendMessage('yandex', 'OnRewardedAdsOpened');
      },
      onRewarded: function() {
        window.unityInstance.SendMessage('yandex', 'OnRewardedAdsRewarded');
      },
      onClose: function() {
        window.unityInstance.SendMessage('yandex', 'OnRewardedAdsClosed');
      },
      onError: function(error) {
        window.unityInstance.SendMessage('yandex', 'OnRewardedAdsError', error.message);
      }
    }
  })
}
