using Game.Ships;
using ScriptableObjects.Models;
using UnityEngine;

namespace ScriptableObjects.Interactions.Models
{
    public class SpawnShipInfo
    {
        public string ShipId { get; set; }
        public Ship ShipPrefab { get; set; }
        public string ShipMetaId { get; set; }
        public ShipMeta ShipMeta { get; set; }
        public Team Team { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public override string ToString()
        {
            return $"Ship id : {ShipId}, Team Id: {Team?.Id}";
        }
    }
}