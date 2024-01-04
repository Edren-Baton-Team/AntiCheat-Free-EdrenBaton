using AntiCheatFree.API;
using AntiCheatFree.API.Features;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using UnityEngine;

namespace AntiCheatFree.Handlers
{
    internal sealed class PlayerHandlers
    {
        private Dictionary<Pickup, PickupInfo> _pickupInfo;
        private HashSet<string> _doorNames;

        internal PlayerHandlers()
        {
            _pickupInfo = [];
            _doorNames = ["elevatorbutton1", "collider", "elevatorpanel (1)", "collider_glass", "hczbutton#1", "collider_door", "hczbutton#2", "door_left", "touchscreenpanel", "plainside", "door_left_col", "door_right_right", "door 104-second", "door 104", "door_left_001"];
        }

        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Pickup == null || !ev.IsAllowed || !_pickupInfo.ContainsKey(ev.Pickup))
            {
                return;
            }

            var pickupInfo = _pickupInfo[ev.Pickup];

            if (!ev.Player.TryGetRaycast(out var hit) || (hit.rigidbody == null || Pickup.Get(hit.rigidbody.gameObject) == null) && !pickupInfo.StartRaycast)
            {
                ev.IsAllowed = false;
            }

            _pickupInfo.Remove(ev.Pickup);
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Door == null || !ev.IsAllowed)
            {
                return;
            }

            if (ev.Player.TryGetRaycast(out var hit))
            {
                var name = hit.collider.gameObject.name.ToLower().Trim();

                Log.Info(name);

                ev.IsAllowed = hit.collider.gameObject.TryGetFromParent(obj => Door.Get(obj) == ev.Door) || _doorNames.Contains(name);
            }
            else
            {
                ev.IsAllowed = false;
            }
        }

        public void OnSearchingPickup(SearchingPickupEventArgs ev)
        {
            if (ev.Pickup == null || !ev.IsAllowed || !ev.Pickup.Rigidbody.useGravity || !ev.Pickup.Rigidbody.detectCollisions || ev.Pickup.Rigidbody.isKinematic)
            {
                return;
            }

            bool raycast = ev.Player.TryGetRaycast(out var hit) && hit.rigidbody != null && Pickup.Get(hit.rigidbody.gameObject) == ev.Pickup;

            _pickupInfo.AddOrUpdate(ev.Pickup, new PickupInfo(ev.Player, ev.Pickup.Position, raycast));
        }
    }
}