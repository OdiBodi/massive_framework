mergeInto(LibraryManager.library, {

  SdkGameLoadingStart: function() {
    window.CrazyGames.SDK.game.sdkGameLoadingStart();
  },

  SdkGameLoadingStop: function() {
    window.CrazyGames.SDK.game.sdkGameLoadingStop();
  },

  SdkGameplayStart: function() {
    window.CrazyGames.SDK.game.gameplayStart();
  },

  SdkGameplayStop: function() {
    window.CrazyGames.SDK.game.gameplayStop();
  },

  SdkHappyTime: function() {
    window.CrazyGames.SDK.game.happytime();
  },

  SdkRequestBannerAds: function(containerId, width, height) {
    var elementId = UTF8ToString(containerId);
    var element = document.getElementById(elementId);
    element.style.display = "block";
    const callback = (error, result) => {
      if (error) {
        var json = JSON.stringify([elementId, error]);
        window.unityInstance.SendMessage("crazy_games", "OnAdsBannerError", json);
      }
      else {
        window.unityInstance.SendMessage("crazy_games", "OnAdsBannerShown", elementId);
      }
    };
    window.CrazyGames.SDK.banner.requestBanner(
    {
      id: elementId,
      width: width,
      height: height,
    },
    callback);
  },

  SdkRequestResponsiveBannerAds: function(containerId) {
    var elementId = UTF8ToString(containerId);
    var element = document.getElementById(elementId);
    element.style.display = "block";
    const callback = (error, result) => {
      if (error) {
        var json = JSON.stringify([elementId, error]);
        window.unityInstance.SendMessage("crazy_games", "OnAdsBannerError", json);
      }
      else {
        window.unityInstance.SendMessage("crazy_games", "OnAdsBannerShown", elementId);
      }
    };
    window.CrazyGames.SDK.banner.requestResponsiveBanner(elementId, callback);
  },

  SdkHideBannerAds: function(containerId) {
    var elementId = UTF8ToString(containerId);
    var element = document.getElementById(elementId);
    element.style.display = "none";
    window.unityInstance.SendMessage("crazy_games", "OnAdsBannerHid", elementId);
  },

  SdkRequestInterstitialAds: function() {
    const callbacks = {
      adStarted: () => window.unityInstance.SendMessage("crazy_games", "OnAdsInterstitialStarted"),
      adError: (error) => window.unityInstance.SendMessage("crazy_games", "OnAdsInterstitialError", error),
      adFinished: () => window.unityInstance.SendMessage("crazy_games", "OnAdsInterstitialFinished")
    };
    window.CrazyGames.SDK.ad.requestAd("midgame", callbacks);
  },

  SdkRequestRewardedAds: function() {
    const callbacks = {
      adStarted: () => window.unityInstance.SendMessage("crazy_games", "OnAdsRewardedStarted"),
      adError: (error) => window.unityInstance.SendMessage("crazy_games", "OnAdsRewardedError", error),
      adFinished: () => window.unityInstance.SendMessage("crazy_games", "OnAdsRewardedFinished")
    };
    window.CrazyGames.SDK.ad.requestAd("rewarded", callbacks);
  },

});
