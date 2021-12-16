using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ship Meta")]
    public class ShipMeta : ScriptableObject
    {
        public string Name;
        public float Scale;
        public float Speed;
        public float SlowRate;
        public Sprite ImageFullHealth;
    }
}
