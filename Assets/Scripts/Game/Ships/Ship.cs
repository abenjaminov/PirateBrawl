using System;
using GameInput.Interfaces;
using GameInput.Models;
using ScriptableObjects.Channels;
using ScriptableObjects.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Ships
{
    public class Ship : MonoBehaviour, IClickHandler
    {
        public UnityAction<Ship> ShipClickedEvent;
        
        [SerializeField] private ShipsChannel ShipsChannel;

        public string Id { get; set; }
        public ShipMovement Movement { get; set; }
        public ShipStats Stats { get; set; }
        public ShipVisuals Visuals { get; set; }
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            Movement = GetComponent<ShipMovement>();
            Stats = GetComponent<ShipStats>();
            Visuals = GetComponent<ShipVisuals>();
        }

        public void HandleClick(ClickEventInfo eventInfo)
        {
            ShipClickedEvent?.Invoke(this);
        }

        public void SpawnShip(Vector2 position, Quaternion direction)
        {
            Visuals.ShowVisuals();
            Visuals.SetImage(Stats.GetImage());
            Visuals.SetOutlineColor(Stats.Team.Color);
            Visuals.HideOutlines();

            transform.localScale = new Vector3(Stats.GetScale(), Stats.GetScale(), 0);
            
            // So it will be infront of the arena for clicking
            var position3 = new Vector3(position.x, position.y, -1);
            transform.SetPositionAndRotation(position3, direction);

            ShipsChannel.OnShipAdded(this);
        }
    }
}