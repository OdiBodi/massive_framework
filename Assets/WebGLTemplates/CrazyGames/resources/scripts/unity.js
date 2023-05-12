const buildUrl = "Build";
const loaderUrl = buildUrl + "/{{{ LOADER_FILENAME }}}";
const config = {
  dataUrl: buildUrl + "/{{{ DATA_FILENAME }}}",
  frameworkUrl: buildUrl + "/{{{ FRAMEWORK_FILENAME }}}",
  codeUrl: buildUrl + "/{{{ CODE_FILENAME }}}",
#if MEMORY_FILENAME
  memoryUrl: buildUrl + "/{{{ MEMORY_FILENAME }}}",
#endif
#if SYMBOLS_FILENAME
  symbolsUrl: buildUrl + "/{{{ SYMBOLS_FILENAME }}}",
#endif
  streamingAssetsUrl: "StreamingAssets",
  companyName: "{{{ COMPANY_NAME }}}",
  productName: "{{{ PRODUCT_NAME }}}",
  productVersion: "{{{ PRODUCT_VERSION }}}",
};

const container = document.querySelector("#unity-container");
const canvas = document.querySelector("#unity-canvas");
const loadingCover = document.querySelector("#loading-cover");
const progressBarFull = document.querySelector("#unity-progress-bar-full");

if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
  container.className = "unity-mobile";
}

loadingCover.style.display = "";

window.CrazyGames.SDK.game.sdkGameLoadingStart();

const script = document.createElement("script");
script.src = loaderUrl;
script.onload = () => {
  createUnityInstance(canvas, config, progress => {
    progressBarFull.style.width = `${100 * progress}%`;
  })
  .then(instance => {
    window.CrazyGames.SDK.game.sdkGameLoadingStop();

    console.log("Unity initialized!");

    loadingCover.style.display = "none";

    window.unityInstance = instance;
    window.unityInstance.SendMessage("engine", "OnInitialized");
  })
  .catch(message => {
    alert(message);
  });
};
document.body.appendChild(script);

window.addEventListener("wheel", (event) => event.preventDefault(), {
  passive: false,
});

window.addEventListener("keydown", (event) => {
  if (["ArrowUp", "ArrowDown", " "].includes(event.key)) {
    event.preventDefault();
  }
});
