using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using ScriptableObjects.Models;
using UnityEngine;

namespace Game.Ships
{
    public class ShipMovementConductor : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private ShipsChannel _shipsChannel;

        private Dictionary<string, Ship> _ships = new Dictionary<string, Ship>();
        private Ship _selectedShip;
        private ShipMovement _selectedShipMovement;
        
        private void Awake()
        {
            _shipsChannel.OnShipAddedEvent += OnShipAddedEvent;
            _gameChannel.OnArenaClickedEvent += OnArenaClickedEvent;
        }

        private void OnDestroy()
        {
            _shipsChannel.OnShipAddedEvent -= OnShipAddedEvent;
            _gameChannel.OnArenaClickedEvent -= OnArenaClickedEvent;
        }
        
        private void OnArenaClickedEvent(ArenaClickedEventInfo info)
        {
            if (_selectedShip == null) return;
            
            _selectedShipMovement.SetTarget(info.WorldPosition);
        }

        private void OnShipAddedEvent(Ship ship)
        {
            ship.ShipClickedEvent += ShipClickedEvent;
            _ships.Add(ship.Id, ship);
        }

        private void ShipClickedEvent(Ship ship)
        {
            _selectedShip = ship;
            _selectedShipMovement = ship.GetComponent<ShipMovement>();
        }
    }
}