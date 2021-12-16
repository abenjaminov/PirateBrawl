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
        public UnityAction<Ship> ShipClickedEvent;
        
        [SerializeField] private ShipsChannel ShipsChannel;
        [HideInInspector] public string Id;
        [SerializeField] private ShipMeta ShipMeta;

        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            Id = Guid.NewGuid().ToString();

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            
            if(_spriteRenderer == null)
                Debug.LogError("Ship must have a sprite renderer in children");
        }

        private void Start()
        {
            _spriteRenderer.sprite = ShipMeta.ImageFullHealth;
            transform.localScale = new Vector3(ShipMeta.Scale, ShipMeta.Scale, 0);
            
            ShipsChannel.OnShipAdded(this);
        }
        
        public void HandleClick(ClickEventInfo eventInfo)
        {
            Debug.Log(ShipMeta.Name + " Clicked");
            
            ShipClickedEvent?.Invoke(this);
        }
    }
}