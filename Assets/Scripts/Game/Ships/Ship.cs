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
        [SerializeField] private GameObject ShipVisuals;

        public string Id { get; set; }
        public ShipMovement Movement { get; set; }
        public ShipStats Stats { get; set; }
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            Movement = GetComponent<ShipMovement>();
            Stats = GetComponent<ShipStats>();
        }

        private void Start()
        {
            transform.localScale = new Vector3(Stats.GetScale(), Stats.GetScale(), 0);
        }
        
        public void HandleClick(ClickEventInfo eventInfo)
        {
            ShipClickedEvent?.Invoke(this);
        }

        public void SpawnShip(Vector2 position, Quaternion direction)
        {
            ShipVisuals.SetActive(true);
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            
            if(_spriteRenderer == null)
                Debug.LogError("Ship must have a sprite renderer in children");
            
            _spriteRenderer.sprite = Stats.GetImage();
            transform.SetPositionAndRotation(position, direction);

            ShipsChannel.OnShipAdded(this);
        }
    }
}