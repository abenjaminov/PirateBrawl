using System;
using System.Collections.Generic;
using Game.Ships;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Game.Fog
{
    public class FogOfWar : MonoBehaviour
    {
        [SerializeField] private ShipsChannel ShipsChannel;
        [SerializeField] private GameObject FogOfWarObject;
        [SerializeField] private int _resolution;
        
        [SerializeField] private float RevealSpeed;
        
        private Texture2D _texture;
        private Vector2[] _vertices;
        private Color[] _colors;

        private List<RevealBeacon> Beacons;
        
        private void OnDestroy()
        {
            ShipsChannel.OnShipAddedEvent -= OnShipAddedEvent;
        }

        private void Awake()
        {
            Beacons = new List<RevealBeacon>();
            ShipsChannel.OnShipAddedEvent += OnShipAddedEvent;
        }

        private void OnShipAddedEvent(Ship ship)
        {
            var shipTransform = ship.transform;
            
            Beacons.Add(new RevealBeacon()
            {
                TargetRevealTransform = shipTransform,
                CurrentPosition = shipTransform.position,
                RevealRadius = ship.Stats.GetRevealRadius()
            });
        }

        private void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            _resolution = 10;
            var baseWidth = 256;
            var baseHeight = 144;
            
            _texture = new Texture2D(baseWidth * _resolution, baseHeight * _resolution);
            var sprite = Sprite.Create(_texture, new Rect(0,0, _texture.width, _texture.height), Vector2.zero, _resolution);
            
            FogOfWarObject.transform.position = new Vector3(-baseWidth / 2f, -baseHeight / 2f, FogOfWarObject.transform.position.z);
            FogOfWarObject.GetComponent<SpriteRenderer>().sprite = sprite;
            
            _colors = new Color[_texture.GetPixels().Length];

            for (int i = 0; i < _colors.Length; i++)
            {
                _colors[i] = Color.black;
            }

            UpdateColor();
        }

        void UpdateColor()
        {
            _texture.SetPixels(_colors);
            _texture.Apply();
        }

        private void Update()
        {
            foreach (var beacon in Beacons)
            {
                var distance = Vector2.Distance(beacon.CurrentPosition, beacon.TargetRevealTransform.position);
                if (Mathf.Approximately(distance, 0)) continue;
                
                
                Reveal(beacon);
                beacon.CurrentPosition = beacon.TargetRevealTransform.position;
            }
            
            
            UpdateColor();
        }

        private void Reveal(RevealBeacon beacon)
        {
            var position = beacon.CurrentPosition;
            var centerInt = new Vector2(position.x, position.y);
            var centerOnTexture = ((centerInt - (Vector2)FogOfWarObject.transform.position)) * _resolution;
            var realRadius = beacon.RevealRadius * _resolution;
            var realRadiusAndBorder = realRadius + 10;

            for (int x = (int)(centerOnTexture.x - realRadiusAndBorder); x <= centerOnTexture.x; x++)
            {
                for (int y = (int)(centerOnTexture.y - realRadiusAndBorder); y <= centerOnTexture.y; y++)
                {
                    var distanceFromCenterSquared = (x - centerOnTexture.x) * (x - centerOnTexture.x) +
                                                    (y - centerOnTexture.y) * (y - centerOnTexture.y);

                    // we don't have to take the square root, it's slow
                    if (distanceFromCenterSquared <= realRadius * realRadius)
                    {
                        int xSym = (int)(centerOnTexture.x - (x - centerOnTexture.x));
                        int ySym = (int)(centerOnTexture.y - (y - centerOnTexture.y));

                        var point1 = new Vector2Int(x, y);
                        var point2 = new Vector2Int(x, ySym);
                        var point3 = new Vector2Int(xSym, y);
                        var point4 = new Vector2Int(xSym, ySym);

                        _colors[point1.y * _texture.width + point1.x].a = 0;
                        _colors[point2.y * _texture.width + point2.x].a = 0;
                        _colors[point3.y * _texture.width + point3.x].a = 0;
                        _colors[point4.y * _texture.width + point4.x].a = 0;
                    }
                    else if (distanceFromCenterSquared <= realRadiusAndBorder * realRadiusAndBorder)
                    {
                        int xSym = (int)(centerOnTexture.x - (x - centerOnTexture.x));
                        int ySym = (int)(centerOnTexture.y - (y - centerOnTexture.y));

                        var point1 = new Vector2Int(x, y);
                        var point2 = new Vector2Int(x, ySym);
                        var point3 = new Vector2Int(xSym, y);
                        var point4 = new Vector2Int(xSym, ySym);

                        SetAlphaInEdge(point1);
                        SetAlphaInEdge(point2);
                        SetAlphaInEdge(point3);
                        SetAlphaInEdge(point4);
                    }
                }
            }
        }

        private void SetAlphaInEdge(Vector2Int point1)
        {
            var alpha1 = _colors[point1.y * _texture.width + point1.x].a;
            _colors[point1.y * _texture.width + point1.x].a = Mathf.Min(Mathf.Max(alpha1, 0.5f), 1);
        }
    }
}