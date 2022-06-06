using IPA;
using IPA.Config;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace BAC3
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Logger { get; private set; }

        [Init]
        public void Init(Config config, IPALogger logger)
        {
            Instance = this;
            Logger = logger;
            Logger.Info("BAC3 initialized.");
        }
    }
}
