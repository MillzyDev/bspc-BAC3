using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace BAC3.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
    }
}