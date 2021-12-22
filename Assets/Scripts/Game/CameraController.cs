using System;
using Game.Ships;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        private BoxCollider2D _boundsCollider;
        [SerializeField] private float _followSpeed;
        [SerializeField] private bool ClipToSelectedShip;
        [SerializeField] private ShipsChannel ShipsChannel;
        [SerializeField] private Transform ShipToFollowTransform;
        private Camera _camera;
        
        private float _rightBound;
        private float _leftBound;
        private float _topBound;
        private float _bottomBound;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            ShipsChannel.OnShipSelectedEvent += OnShipSelectedEvent;
            Initialize();
        }

        private void OnDestroy()
        {
            ShipsChannel.OnShipSelectedEvent -= OnShipSelectedEvent;
        }

        private void OnShipSelectedEvent(Ship ship)
        {
            ShipToFollowTransform = ship.transform;
        }

        private void Initialize()
        {
            var Arena = GameObject.FindObjectOfType<Arena>();

            if (Arena == null)
            {
                Debug.LogError("There is no ArenaObject");
                return;
            }
            
            _boundsCollider = Arena.GetComponent<BoxCollider2D>();
            
            UpdateBounds();
            
            var destination = GetClampedDestinationPosition(transform.position);
            transform.position = destination;
        }

        private void UpdateBounds()
        {
            var vertExtent = _camera.orthographicSize;
            var horizExtent = vertExtent * Screen.width / Screen.height;

            var bounds = _boundsCollider.bounds;

            _leftBound = bounds.min.x + horizExtent;
            _rightBound = bounds.max.x - horizExtent;
            _bottomBound = bounds.min.y + vertExtent;
            _topBound = bounds.max.y - vertExtent;
        }

        void FixedUpdate () 
        {
            if (ClipToSelectedShip)
            {
                var currentPosition = transform.position;
            
                var destination = GetClampedDestinationPosition(currentPosition);
                currentPosition = Vector3.Lerp(currentPosition, destination, _followSpeed * Time.deltaTime);
                transform.position = currentPosition;
            }
        }

        private Vector3 GetClampedDestinationPosition(Vector3 currentPosition)
        {
            var playerPosition = ShipToFollowTransform.position;
            var destination = new Vector3(playerPosition.x, playerPosition.y, currentPosition.z);
            destination.x = Mathf.Clamp(destination.x, _leftBound, _rightBound);
            destination.y = Mathf.Clamp(destination.y, _bottomBound, _topBound);
            return destination;
        }
    }
}