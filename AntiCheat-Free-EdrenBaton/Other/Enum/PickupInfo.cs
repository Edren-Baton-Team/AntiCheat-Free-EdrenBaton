using Exiled.API.Features;
using UnityEngine;

namespace AntiCheat_Free_EdrenBaton.Other.Enum
{
    public class PickupInfo
    {
        public PickupInfo(Player owner, Vector3 startPos, bool startRaycast)
        {
            this.Owner = owner;
            this.startPos = startPos;
            this.startRaycast = startRaycast;
        }
        public Player Owner { get; set; }
        public Vector3 startPos { get; set; }
        public bool startRaycast { get; set; } = false;
    }
}
