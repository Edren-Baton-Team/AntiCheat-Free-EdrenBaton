using Exiled.API.Features;
using UnityEngine;

namespace AntiCheatFree.API.Features
{
    public class PickupInfo(Player owner, Vector3 startPos, bool startRaycast)
    {
        public Player Owner { get; set; } = owner;

        public Vector3 StartPos { get; set; } = startPos;

        public bool StartRaycast { get; set; } = startRaycast;
    }
}
