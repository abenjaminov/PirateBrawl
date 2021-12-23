using System.Collections.Generic;
using ScriptableObjects.Interactions.Models;
using ScriptableObjects.Models;

namespace Game.Ships.ShipInteractors
{
    public class PlayerShipsInteractor : ShipInteractor
    {
        
        private Ship _selectedShip;

        private void Awake()
        {
            _gameChannel.OnArenaClickedEvent += OnArenaClickedEvent;
        }

        private void OnDestroy()
        {
            _gameChannel.OnArenaClickedEvent -= OnArenaClickedEvent;

            foreach (var ship in Ships.Values)
            {
                ship.ShipClickedEvent -= ShipClickedEvent;
            }
        }
        
        private void OnArenaClickedEvent(ArenaClickedEventInfo info)
        {
            if (_selectedShip == null) return;
            
            _interactionsAgent.MoveShip(new MoveShipInfo()
            {
                Ship = _selectedShip,
                WorldPosition = info.WorldPosition
            });
        }

        protected override void OnShipAddedEvent(Ship ship)
        {
            ship.ShipClickedEvent += ShipClickedEvent;
            
            base.OnShipAddedEvent(ship);
        }

        private void ShipClickedEvent(Ship ship)
        {
            if (_selectedShip != null)
            {
                _selectedShip.Visuals.HideOutlines();    
            }
            
            _selectedShip = ship;
            _selectedShip.Visuals.ShowOutlines();
            _shipsChannel.OnShipSelected(ship);
        }
    }
}