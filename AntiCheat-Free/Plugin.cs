using AntiCheatFree.Handlers;
using Exiled.Events.Handlers;
using System;

namespace AntiCheatFree
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        private PlayerHandlers _handlers;

        public override string Prefix => "AntiCheat";

        public override string Name => "AntiCheat-Free";

        public override string Author => "Edren Baton Team & NotAloneAgain";

        public override Version Version { get; } = new Version(1, 0, 2);

        public override void OnEnabled()
        {
            _handlers = new();

            Player.InteractingDoor += _handlers.OnInteractingDoor;
            Player.SearchingPickup += _handlers.OnSearchingPickup;
            Player.PickingUpItem += _handlers.OnPickingUpItem;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.PickingUpItem -= _handlers.OnPickingUpItem;
            Player.SearchingPickup -= _handlers.OnSearchingPickup;
            Player.InteractingDoor -= _handlers.OnInteractingDoor;

            _handlers = null;

            base.OnDisabled();
        }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }
    }
}
