using System;
using UnityEngine;

namespace Game.Fog
{
    public class RevealedBox : MonoBehaviour
    {
        private BoxCollider2D _collider;
        [SerializeField] private string _id;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        public string GetId()
        {
            return _id;
        }
        
        public float GetWidth()
        {
            return _collider.size.x;
        }
        
        public float GetHeight()
        {
            return _collider.size.y;
        }
        
        public Vector2 GetCenter()
        {
            return _collider.bounds.center;
        }
    }
}