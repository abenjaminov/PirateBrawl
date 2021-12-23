using System;
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

        [Range(0, 1)] public float TeamColorBlendRange;

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }
    }
}
