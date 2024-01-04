using System.Collections.Generic;
using System;
using UnityEngine;
using Exiled.API.Features;

namespace AntiCheatFree.API
{
    public static class Extensions
    {
        private static LayerMask _layerMasks;

        static Extensions() => _layerMasks = ~LayerMask.GetMask("Player", "Pickup", "Hitbox", "DestroyedDoor", "BreakableGlass", "SCP018", "Light", "Grenade", "Ragdoll");

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict == null)
            {
                throw new ArgumentNullException(nameof(dict));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }

        public static bool TryGetRaycast(this Player player, out RaycastHit hit)
            => Physics.Raycast(player.CameraTransform.position + player.CameraTransform.forward * 0.2f, player.CameraTransform.forward, out hit, 100, _layerMasks);

        public static bool TryGetFromParent(this GameObject obj, Func<GameObject, bool> func)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            while (obj != null)
            {
                if (func(obj))
                {
                    return true;
                }

                obj = obj.transform?.parent?.gameObject;
            }

            return false;
        }
    }
}
