using ScriptableObjects.Interactions.Models;
using ScriptableObjects.Interactions.Receivers;
using UnityEngine;

namespace ScriptableObjects.Interactions.Transmitters
{
    [CreateAssetMenu(menuName = "Interactions/Transmitters/Self Debug", fileName = "Self debug interactions transmitter")]
    public class SelfDebugTransmitter : GameInteractionsTransmitter
    {
        public override void MoveShip(MoveShipInfo info)
        {
            Debug.Log($"Move ship transmission {info}");
        }

        public override void SpawnShip(SpawnShipInfo info)
        {
            Debug.Log($"Spawn ship transmission {info}");
        }
    }
}