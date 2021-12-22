using ScriptableObjects;
using ScriptableObjects.Models;
using UnityEngine;

namespace Game.Ships
{
    public class ShipStats : MonoBehaviour
    {
        public ShipMeta ShipMeta { get; set; }
        public Team Team { get; set; }

        public float GetSpeedChangeRate()
        {
            return ShipMeta.SpeedChangeRate;
        }

        public float GetSpeed()
        {
            return ShipMeta.Speed;
        }

        public float GetScale()
        {
            return ShipMeta.Scale;
        }

        public Sprite GetImage()
        {
            return ShipMeta.ImageFullHealth;
        }
    }
}