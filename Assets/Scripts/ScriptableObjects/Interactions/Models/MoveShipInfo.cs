using Game.Ships;
using UnityEngine;

namespace ScriptableObjects.Interactions.Models
{
    public class MoveShipInfo
    {
        public string ShipId;
        public Ship Ship;
        public Vector3 WorldPosition;

        public override string ToString()
        {
            return $"Ship name : {Ship.name}, Position: {WorldPosition.ToString()}";
        }
    }
}