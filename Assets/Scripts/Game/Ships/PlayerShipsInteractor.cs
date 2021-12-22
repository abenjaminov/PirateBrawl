using System.Collections.Generic;
using ScriptableObjects.Channels;
using ScriptableObjects.Interactions;
using ScriptableObjects.Interactions.Models;
using ScriptableObjects.Models;
using UnityEngine;

namespace Game.Ships
{
    public class PlayerShipsInteractor : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private ShipsChannel _shipsChannel;
        [SerializeField] private PlayerInteractionsAgent PlayerInteractionsAgent;

        private Dictionary<string, Ship> _ships = new Dictionary<string, Ship>();
        private Ship _selectedShip;

        private void Awake()
        {
            _shipsChannel.OnShipAddedEvent += OnShipAddedEvent;
            _gameChannel.OnArenaClickedEvent += OnArenaClickedEvent;
        }

        private void OnDestroy()
        {
            _shipsChannel.OnShipAddedEvent -= OnShipAddedEvent;
            _gameChannel.OnArenaClickedEvent -= OnArenaClickedEvent;

            foreach (var ship in _ships.Values)
            {
                ship.ShipClickedEvent -= ShipClickedEvent;
            }
        }
        
        private void OnArenaClickedEvent(ArenaClickedEventInfo info)
        {
            if (_selectedShip == null) return;
            
            PlayerInteractionsAgent.MoveShip(new MoveShipInfo()
            {
                Ship = _selectedShip,
                WorldPosition = info.WorldPosition
            });
        }

        private void OnShipAddedEvent(Ship ship)
        {
            ship.ShipClickedEvent += ShipClickedEvent;
            _ships.Add(ship.Id, ship);
            
            var shipTransform = ship.transform;
            var worldPosition = shipTransform.position + (shipTransform.right * 2);
            
            PlayerInteractionsAgent.MoveShip(new MoveShipInfo()
            {
                Ship = ship,
                WorldPosition = worldPosition
            });
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