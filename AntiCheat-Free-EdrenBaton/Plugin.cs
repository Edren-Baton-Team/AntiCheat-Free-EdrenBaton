using Exiled.API.Features;
using HarmonyLib;
using System;

namespace AntiCheat_Free_EdrenBaton
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "AntiCheat";
        public override string Name => "AntiCheat-Free-EdrenBaton";
        public override string Author => "Rysik5318";
        public override Version Version => new Version(1, 0, 1);
        public static Plugin plugin;

        private Handlers.Player.EventHandlers EventHandlers_Player;
        public static Harmony harmony;
        public override void OnEnabled()
        {
            plugin = new Plugin();
            EventHandlers_Player = new();

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            plugin = null;
            EventHandlers_Player = null;

            base.OnDisabled();
        }
    }
}
