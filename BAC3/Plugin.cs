using BAC3.Configuration;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System.Reflection;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace BAC3
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Logger { get; private set; }
        internal static Harmony HarmonyInstance { get; private set; }

        [Init]
        public void Init(Zenjector zenjector, Config config, IPALogger logger)
        {
            Instance = this;
            Logger = logger;

            zenjector.UseLogger(Logger);
            zenjector.UseMetadataBinder<Plugin>();

            PluginConfig.Instance = config.Generated<PluginConfig>();

            HarmonyInstance = new Harmony(Assembly.GetExecutingAssembly().FullName);
            HarmonyInstance.PatchAll();

            Logger.Info("BAC3 initialized.");
        }
    }
}
