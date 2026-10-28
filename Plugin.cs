using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace JoinFix
{
    // Joining a lobby: the joining game asks the host to accept it once a second and gives up after
    // 5 s ("No connection to the host could be made, please check firewall"), then allows another 5 s
    // for the host's game state. Setting up the Steam peer-to-peer connection (especially through
    // Steam's relays) often takes longer than that. Pausing "fixed" it only because these timers
    // count game time, which stops while paused. Wait longer instead.
    // Same fix as CompetitiveMod's JoinTimeout; running both is harmless.
    [BepInPlugin(Guid, Name, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "joinfix.stickfight";
        public const string Name = "JoinFix";
        public const string Version = "1.0.0";

        internal static ConfigEntry<float> Timeout;

        private void Awake()
        {
            Timeout = Config.Bind("General", "JoinTimeout", 30f,
                "Seconds to wait for the host to accept you, and then for the host's game state, when joining a lobby (vanilla: 5).");
            new Harmony(Guid).PatchAll(typeof(Plugin).Assembly);
            Logger.LogInfo($"{Name} {Version} loaded (JoinTimeout={Timeout.Value}s)");
        }
    }

    [HarmonyPatch(typeof(MultiplayerManager))]
    internal static class JoinTimeoutPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("TimeOutAfterSecondsIfNotAccepted")]
        private static void AcceptedPrefix(ref float v)
        {
            if (v < Plugin.Timeout.Value)
            {
                v = Plugin.Timeout.Value;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("TimeOutAfterSecondsIfNotInitialized")]
        private static void InitializedPrefix(ref float v)
        {
            if (v < Plugin.Timeout.Value)
            {
                v = Plugin.Timeout.Value;
            }
        }
    }
}
