using Game.Models;
using Game.Ships;
using UnityEngine;

namespace ScriptableObjects.Interactions.Models
{
    public class SpawnShipInfo
    {
        public string Id { get; set; }
        public Ship ShipPrefab { get; set; }
        public ShipMeta ShipMeta { get; set; }
        public Team Team { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public override string ToString()
        {
            return $"Ship id : {Id}, Team: {Team?.Name}";
        }
    }
}