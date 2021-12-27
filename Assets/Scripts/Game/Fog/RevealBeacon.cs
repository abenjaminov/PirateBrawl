using UnityEngine;

namespace Game.Fog
{
    public class RevealBeacon
    {
        public string BeaconId;
        public Transform TargetRevealTransform;
        public Vector2 CurrentPosition;
        public int RevealRadius;
    }
}