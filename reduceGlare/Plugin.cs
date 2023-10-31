using HMUI;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Settings;
using reduceGlare.Configuration;
using reduceGlare;

namespace reduceGlare
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, IPA.Config.Config conf)
        {
            Instance = this;
            Log = logger;
            Log.Info("reduceGlare initialized.");

            BS_Utils.Utilities.BSEvents.gameSceneLoaded += onGameSceneLoaded;
            settings.Instance = conf.Generated<settings>();
            BSMLSettings.instance.AddSettingsMenu("reduceGlare", "reduceGlare.Configuration.settings.bsml", settingsHandler.instance);

        }

        private async void onGameSceneLoaded()
        {
            if (!settings.Instance.modEnable) return;


            await Task.Delay(1000);
            var envObjs = GameObject.FindObjectsOfType<GameObject>();
            var environment = GameObject.Find("Environment");

            //Plugin.Log.Info("count:" + envObjs.Count().ToString());

            if (!settings.Instance.reduceGlare) environment.layer = 3;
            foreach (var envObj in envObjs)
            {
                if (envObj.GetComponent<TubeBloomPrePassLight>() != null)
                {
                    var blooms = envObj.GetComponents<TubeBloomPrePassLight>();
                    foreach (var bloom in blooms) bloom.bloomFogIntensityMultiplier = 0;

                    if (!settings.Instance.reduceGlare) envObj.layer = 3;

                }
                if (envObj.GetComponent<BloomPrePassBackgroundColorsGradient>() != null)
                {
                    envObj.GetComponent<BloomPrePassBackgroundColorsGradient>().enabled = false;
                    if (!settings.Instance.reduceGlare) envObj.layer = 3;

                }
                if (envObj.GetComponent<BloomPrePassBackgroundNonLightRenderer>() != null)
                {

                    envObj.GetComponent<BloomPrePassBackgroundNonLightRenderer>().enabled = false;
                    if (!settings.Instance.reduceGlare) envObj.layer = 3;

                }
                if (envObj.layer != 14) continue;


                if (envObj.GetComponent<TrackLaneRing>() != null)
                {
                    envObj.GetComponent<TrackLaneRing>().enabled = false;
                    if (!settings.Instance.reduceGlare) envObj.layer = 3;
                    if (!settings.Instance.reduceGlare) updateAllLayer(envObj.transform, 3);

                }
            }

            if (!settings.Instance.reduceGlare) updateAllLayer(environment.transform, 3);
        }

        private void updateAllLayer(Transform root, int layer)
        {
            if (root.name.IndexOf("GameHUD") >= 0) return;

            // 子オブジェクトの数を取得
            int childCount = root.childCount;

            // 子オブジェクトをループで処理
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = root.GetChild(i);

                if (layer != -1)
                {
                    childTransform.gameObject.layer = layer;
                }
                else
                {
                    childTransform.gameObject.SetActive(false);
                }
                //Plugin.Log.Info(childTransform.name);

                // 子オブジェクトの中にさらに子オブジェクトがある場合、再帰的に処理を続行
                if (root.name != "PlayersPlace" || !settings.Instance.playersPlace) updateAllLayer(childTransform, layer);
            }
        }
        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            new GameObject("PlatformDisablerController").AddComponent<reduceGlareController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
