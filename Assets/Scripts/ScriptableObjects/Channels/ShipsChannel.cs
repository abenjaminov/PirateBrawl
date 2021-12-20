using Game.Ships;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Ships Channel", menuName = "Channels/Ships")]
    public class ShipsChannel : ScriptableObject
    {
        public UnityAction<Ship> OnShipAddedEvent;
        public UnityAction<string, ShipMeta> OnPlaceNewShipEvent;

        public void OnPlaceNewShip(string id, ShipMeta meta)
        {
            OnPlaceNewShipEvent?.Invoke(id, meta);
        }
        
        public void OnShipAdded(Ship ship)
        {
            OnShipAddedEvent?.Invoke(ship);
        }
    }
}