using ScriptableObjects.Interactions.Models;
using UnityEngine;

namespace ScriptableObjects.Interactions.Transmitters
{
    public abstract class GameInteractionsTransmitter : ScriptableObject
    {
        public abstract void MoveShip(MoveShipInfo info);
        public abstract void SpawnShip(SpawnShipInfo info);
    }
}