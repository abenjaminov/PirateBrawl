using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ship Meta")]
    public class ShipMeta : ScriptableObject
    {
        public string Id;
        public string Name;
        public float Scale;
        public float Speed;
        public float SpeedChangeRate;
        public Sprite ImageFullHealth;
    }
}
