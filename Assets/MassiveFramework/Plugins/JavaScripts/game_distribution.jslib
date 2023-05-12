mergeInto(LibraryManager.library, {

  SdkInitialize: function(gameId, debug) {
    window["GD_OPTIONS"] = {
      debug: debug == 1,
      gameId: UTF8ToString(gameId),
      onEvent: function(event) {
        switch (event.name) {
          case "SDK_READY":
            window.unityInstance.SendMessage("game_distribution", "OnInitialized", 1);
            break;
          case "SDK_ERROR":
            window.unityInstance.SendMessage("game_distribution", "OnInitialized", 0);
            break;
          case "SDK_GAME_START":
            window.unityInstance.SendMessage("game_distribution", "OnResumed");
            break;
          case "SDK_GAME_PAUSE":
            window.unityInstance.SendMessage("game_distribution", "OnPaused");
            break;
          case "LOADED":
            window.unityInstance.SendMessage("game_distribution", "OnAdsVideoLoaded");
            break;
          case "IMPRESSION":
            window.unityInstance.SendMessage("game_distribution", "OnAdsVideoOpened");
            break;
          case "SKIPPED":
            window.unityInstance.SendMessage("game_distribution", "OnAdsVideoSkipped");
            break;
          case "COMPLETE":
            window.unityInstance.SendMessage("game_distribution", "OnAdsVideoComplete");
            break;
        }
      }
    };
    (function(d, s, id) {
      var js, fjs = d.getElementsByTagName(s)[0];
      if (d.getElementById(id)){
       return;
      }
      js = d.createElement(s);
      js.id = id;
      js.src = "https://html5.api.gamedistribution.com/main.min.js";
      fjs.parentNode.insertBefore(js, fjs);
    })(document, "script", "gamedistribution-jssdk");
  },

  SdkShowConsole: function() {
    window.gdsdk.openConsole();
  },

  SdkPreloadInterstitialAds: function() {
    window.gdsdk.preloadAd(window.gdsdk.AdType.Interstitial)
      .then(function(response) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsInterstitialPreloaded", 1);
      })
      .catch(function(error) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsInterstitialPreloaded", 0);
      });
  },

  SdkPreloadRewardedAds: function() {
    window.gdsdk.preloadAd(window.gdsdk.AdType.Rewarded)
      .then(function(response) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsRewardedPreloaded", 1);
      })
      .catch(function(error) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsRewardedPreloaded", 0);
      });
  },

  SdkShowBannerAds: function(containerId) {
    var elementId = UTF8ToString(containerId);
    var element = document.getElementById(elementId);
    element.style.display = "block";
    window.gdsdk.showAd(window.gdsdk.AdType.Display, { containerId: elementId })
      .then(function(response) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsBannerShown", elementId);
      })
      .catch(function(error) {
        var json = JSON.stringify([elementId, error.message]);
        window.unityInstance.SendMessage("game_distribution", "OnAdsBannerError", json);
      });
  },

  SdkHideBannerAds: function(containerId) {
    var elementId = UTF8ToString(containerId);
    var element = document.getElementById(elementId);
    element.style.display = "none";
    window.unityInstance.SendMessage("game_distribution", "OnAdsBannerHid", elementId);
  },

  SdkShowInterstitialAds: function() {
    window.gdsdk.showAd(window.gdsdk.AdType.Interstitial)
      .then(function(response) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsInterstitialFinished");
      })
      .catch(function(error) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsInterstitialFinished");
      });
  },

  SdkShowRewardedAds: function() {
    window.gdsdk.showAd(window.gdsdk.AdType.Rewarded)
      .then(function(response) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsRewardedFinished");
      })
      .catch(function(error) {
        window.unityInstance.SendMessage("game_distribution", "OnAdsRewardedFinished");
      });
  },

});
