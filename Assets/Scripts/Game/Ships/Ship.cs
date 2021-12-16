using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Ships
{
    public class Ship : MonoBehaviour, IPointerClickHandler
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
            
            ShipsChannel.OnShipAdded(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ShipClickedEvent?.Invoke(this);
        }
    }
}