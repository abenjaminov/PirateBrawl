using ScriptableObjects.Interactions.Models;
using UnityEngine;

namespace ScriptableObjects.Interactions.Receivers
{
    [CreateAssetMenu(menuName = "Interactions/Receivers/Self Debug", fileName = "Self debug interactions receiver")]
    public class SelfDebugReceiver : ScriptableObject,IGameInteractionsReceiver
    {
        public void MoveShip(MoveShipInfo info)
        {
            
        }

        public void SpawnShip(SpawnShipInfo info)
        {
            
        }
    }
}