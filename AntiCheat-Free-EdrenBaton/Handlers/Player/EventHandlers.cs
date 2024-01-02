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
            if (PickupInfo.ContainsKey(ev.Pickup))
            {
                Physics.Raycast(new Ray(ev.Player.CameraTransform.position + ev.Player.CameraTransform.forward * 0.2f, ev.Player.CameraTransform.forward), out RaycastHit hit);
                var pickupInfo = PickupInfo[ev.Pickup];
                if ((hit.rigidbody == null || Pickup.Get(hit.rigidbody.gameObject) is null) && !pickupInfo.startRaycast && ev.Pickup.Position == pickupInfo.startPos)
                    ev.IsAllowed = false;
                PickupInfo.Remove(ev.Pickup);
            }
        }
        public void OnSearchingPickup(SearchingPickupEventArgs ev)
        {
            Physics.Raycast(new Ray(ev.Player.CameraTransform.position + ev.Player.CameraTransform.forward * 0.2f, ev.Player.CameraTransform.forward), out RaycastHit hit);
            if (!ev.Pickup.Rigidbody.useGravity || !ev.Pickup.Rigidbody.detectCollisions || ev.Pickup.Rigidbody.isKinematic) return;
            bool raycast = false;
            if (hit.collider != null && hit.rigidbody != null && Pickup.Get(hit.rigidbody.gameObject) is not null && Pickup.Get(hit.rigidbody.gameObject) == ev.Pickup)
                raycast = true;
            if (!PickupInfo.ContainsKey(ev.Pickup))
                PickupInfo.Add(ev.Pickup, new(ev.Player, ev.Pickup.Position, raycast));
            else
                PickupInfo[ev.Pickup] = new(ev.Player, ev.Pickup.Position, raycast);
        }
    }
}