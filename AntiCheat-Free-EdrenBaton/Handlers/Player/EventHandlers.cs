using AntiCheat_Free_EdrenBaton.Other.Enum;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using UnityEngine;

namespace AntiCheat_Free_EdrenBaton.Handlers.Player
{
    public class EventHandlers
    {
        public static Dictionary<Pickup, PickupInfo> PickupInfo = new();
        private static LayerMask layerMask =
        ~(LayerMask.GetMask("Player") | LayerMask.GetMask("Pickup")
        | LayerMask.GetMask("Hitbox") | LayerMask.GetMask("Hitbox")
        | LayerMask.GetMask("DestroyedDoor") | LayerMask.GetMask("BreakableGlass")
        | LayerMask.GetMask("SCP018") | LayerMask.GetMask("Light")
        | LayerMask.GetMask("Grenade") | LayerMask.GetMask("Ragdoll"));
        public EventHandlers()
        {
            Exiled.Events.Handlers.Player.SearchingPickup += OnSearchingPickup;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
        }
        ~EventHandlers()
        {
            Exiled.Events.Handlers.Player.SearchingPickup -= OnSearchingPickup;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
        }
        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (!PickupInfo.ContainsKey(ev.Pickup)) return;

            Physics.Raycast(new Ray(ev.Player.CameraTransform.position + ev.Player.CameraTransform.forward * 0.2f, ev.Player.CameraTransform.forward), out RaycastHit hit, 9999, layerMask);
            var pickupInfo = PickupInfo[ev.Pickup];
            if ((hit.rigidbody is null || Pickup.Get(hit.rigidbody.gameObject) is null) && !pickupInfo.startRaycast && ev.Pickup.Position == pickupInfo.startPos)
                ev.IsAllowed = false;
            PickupInfo.Remove(ev.Pickup);
        }
        public void OnSearchingPickup(SearchingPickupEventArgs ev)
        {
            if (!ev.Pickup.Rigidbody.useGravity || !ev.Pickup.Rigidbody.detectCollisions || ev.Pickup.Rigidbody.isKinematic) return;
            Physics.Raycast(new Ray(ev.Player.CameraTransform.position + ev.Player.CameraTransform.forward * 0.2f, ev.Player.CameraTransform.forward), out RaycastHit hit, 9999, layerMask);
            bool raycast = false;
            if (hit.collider is not null && hit.rigidbody is not null && Pickup.Get(hit.rigidbody.gameObject) is not null && Pickup.Get(hit.rigidbody.gameObject) == ev.Pickup)
                raycast = true;
            if (!PickupInfo.ContainsKey(ev.Pickup))
                PickupInfo.Add(ev.Pickup, new(ev.Player, ev.Pickup.Position, raycast));
            else
                PickupInfo[ev.Pickup] = new(ev.Player, ev.Pickup.Position, raycast);
        }
    }
}