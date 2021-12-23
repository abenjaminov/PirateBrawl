using ScriptableObjects.Interactions.Models;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Interactions.Receivers
{
    public abstract class GameInteractionsReceiver : ScriptableObject
    {
        public UnityAction<MoveShipInfo> OnMoveShipReceivedEvent;
        public UnityAction<SpawnShipInfo> OnSpawnShipReceivedEvent;

        public virtual void MoveShip(MoveShipInfo info)
        {
            OnMoveShipReceivedEvent?.Invoke(info);
        }

        public virtual void SpawnShip(SpawnShipInfo info)
        {
            OnSpawnShipReceivedEvent?.Invoke(info);
        }
    }
}