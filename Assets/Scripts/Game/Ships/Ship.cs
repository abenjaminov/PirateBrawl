using System;
using GameInput.Interfaces;
using GameInput.Models;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Ships
{
    public class Ship : MonoBehaviour, IClickHandler
    {
        [HideInInspector] public string Id;
        public UnityAction<Ship> ShipClickedEvent;
        
        [SerializeField] private ShipsChannel ShipsChannel;
        [SerializeField] private ShipStats ShipStats;
        [SerializeField] private GameObject ShipVisuals;
        
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            Id = Guid.NewGuid().ToString();
        }

        private void Start()
        {
            transform.localScale = new Vector3(ShipStats.GetScale(), ShipStats.GetScale(), 0);
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
            
            _spriteRenderer.sprite = ShipStats.GetImage();
            //transform.SetPositionAndRotation(position, direction);
            transform.position = position;
            transform.localRotation = direction;
            
            ShipsChannel.OnShipAdded(this);
        }
    }
}