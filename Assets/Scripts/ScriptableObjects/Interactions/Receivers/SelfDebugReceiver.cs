using ScriptableObjects.Interactions.Models;
using UnityEngine;

namespace ScriptableObjects.Interactions.Receivers
{
    [CreateAssetMenu(menuName = "Interactions/Receivers/Self Debug", fileName = "Self debug interactions receiver")]
    public class SelfDebugReceiver : GameInteractionsReceiver
    {
        public override void MoveShip(MoveShipInfo info)
        {
            base.OnMoveShipReceivedEvent(info);
        }

        public override void SpawnShip(SpawnShipInfo info)
        {
            base.OnSpawnShipReceivedEvent(info);
        }
    }
}