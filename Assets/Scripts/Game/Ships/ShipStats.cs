using ScriptableObjects;
using UnityEngine;

namespace Game.Ships
{
    public class ShipStats : MonoBehaviour
    {
        [SerializeField] private ShipMeta ShipMeta;

        public void SetMeta(ShipMeta shipMeta)
        {
            ShipMeta = shipMeta;
        }
        
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