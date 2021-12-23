using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using ScriptableObjects.Interactions;
using ScriptableObjects.Interactions.Models;
using UnityEngine;

namespace Game.Ships.ShipInteractors
{
    public abstract class ShipInteractor : MonoBehaviour
    {
        [SerializeField] protected InteractionsAgent _interactionsAgent;
        [SerializeField] protected GameChannel _gameChannel;
        [SerializeField] protected ShipsChannel _shipsChannel;  
        protected Dictionary<string, Ship> Ships = new Dictionary<string, Ship>();

        private void OnDestroy()
        {
            _shipsChannel.OnShipAddedEvent -= OnShipAddedEvent;
        }

        private void Awake()
        {
            _shipsChannel.OnShipAddedEvent += OnShipAddedEvent;
        }
        
        protected virtual void OnShipAddedEvent(Ship ship)
        {
            Ships.Add(ship.Id, ship);
            
            var shipTransform = ship.transform;
            var worldPosition = shipTransform.position + (shipTransform.right * 5);
            
            _interactionsAgent.MoveShip(new MoveShipInfo()
            {
                Ship = ship,
                WorldPosition = worldPosition
            });
        }
    }
}