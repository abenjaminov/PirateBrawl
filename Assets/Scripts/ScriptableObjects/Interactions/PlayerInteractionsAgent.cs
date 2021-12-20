using Game.Ships;
using ScriptableObjects.Interactions.Models;
using ScriptableObjects.Interactions.Transmitters;
using UnityEngine;

namespace ScriptableObjects.Interactions
{
    [CreateAssetMenu(menuName = "Interactions/Agents/Player", fileName = "Player interaction agent")]
    public class PlayerInteractionsAgent : InteractionsAgent
    {
        [SerializeField] private GameInteractionsTransmitter InteractionsTransmitter;
        
        public override void MoveShip(MoveShipInfo info)
        {
            base.MoveShip(info);
            
            InteractionsTransmitter?.MoveShip(info);
        }

        public override Ship SpawnShip(SpawnShipInfo info)
        {
            var ship = base.SpawnShip(info);
            
            InteractionsTransmitter?.SpawnShip(info);

            return ship;
        }
    }
}