using System;
using System.Collections.Generic;
using System.Linq;
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
        private string[] _setterIds;

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
                RevealRadius = ship.Stats.GetRevealRadius(),
                BeaconId = ship.Id
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
            _setterIds = new string[_colors.Length];

            for (int i = 0; i < _colors.Length; i++)
            {
                _colors[i] = Color.white;
                _setterIds[i] = "";
            }

            var revealedBoxes = FindObjectsOfType<RevealedBox>();
            foreach (var revealedBox in revealedBoxes)
            {
                RevealBox(revealedBox);
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

                RevealBeacon(beacon);
                beacon.CurrentPosition = beacon.TargetRevealTransform.position;
            }
            
            UpdateColor();
        }

        private void RevealBox(RevealedBox revealedBox)
        {
            var position = revealedBox.GetCenter();
            var center = new Vector2(position.x, position.y);
            var centerOnTexture = ((center - (Vector2)FogOfWarObject.transform.position)) * _resolution;
            var realWidth = (revealedBox.GetWidth() / 2) * _resolution;
            var realHeight = (revealedBox.GetHeight() / 2) * _resolution;

            for (int x = (int)(centerOnTexture.x - realWidth); x <= centerOnTexture.x; x++)
            {
                for (int y = (int)(centerOnTexture.y - realHeight); y <= centerOnTexture.y; y++)
                {
                    int xSym = (int)(centerOnTexture.x - (x - centerOnTexture.x));
                    int ySym = (int)(centerOnTexture.y - (y - centerOnTexture.y));

                    var point1 = new Vector2Int(x, y);
                    var point2 = new Vector2Int(x, ySym);
                    var point3 = new Vector2Int(xSym, y);
                    var point4 = new Vector2Int(xSym, ySym);

                    SetPixelTransparent(point1, revealedBox.GetId());
                    SetPixelTransparent(point2, revealedBox.GetId());
                    SetPixelTransparent(point3, revealedBox.GetId());
                    SetPixelTransparent(point4, revealedBox.GetId());
                }
            }
        }

        private void SetPixelTransparent(Vector2Int position, string setterId)
        {
            var index = position.y * _texture.width + position.x;

            if (index < 0 || index >= _colors.Length) return;
            
            if (_colors[index].a == 0 && setterId != _setterIds[index]) return;
            
            _colors[index].a = 0;
            _setterIds[index] = setterId;
        }

        private void RevealBeacon(RevealBeacon beacon)
        {
            var position = beacon.CurrentPosition;
            var center = new Vector2(position.x, position.y);
            var centerOnTexture = ((center - (Vector2)FogOfWarObject.transform.position)) * _resolution;
            var realRadius = beacon.RevealRadius * _resolution;
            var realRadiusAndBorder = realRadius + 10;

            for (int x = (int)(centerOnTexture.x - realRadiusAndBorder); x <= centerOnTexture.x; x++)
            {
                for (int y = (int)(centerOnTexture.y - realRadiusAndBorder); y <= centerOnTexture.y; y++)
                {
                    var distanceFromCenterSquared = (x - centerOnTexture.x) * (x - centerOnTexture.x) +
                                                        (y - centerOnTexture.y) * (y - centerOnTexture.y);

                    int xSym = (int)(centerOnTexture.x - (x - centerOnTexture.x));
                    int ySym = (int)(centerOnTexture.y - (y - centerOnTexture.y));

                    var point1 = new Vector2Int(x, y);
                    var point2 = new Vector2Int(x, ySym);
                    var point3 = new Vector2Int(xSym, y);
                    var point4 = new Vector2Int(xSym, ySym);

                    // we don't have to take the square root, it's slow
                    if (distanceFromCenterSquared <= realRadius * realRadius)
                    {
                        SetPixelTransparent(point1, beacon.BeaconId);
                        SetPixelTransparent(point2, beacon.BeaconId);
                        SetPixelTransparent(point3, beacon.BeaconId);
                        SetPixelTransparent(point4, beacon.BeaconId);
                    }
                    else if (distanceFromCenterSquared <= realRadiusAndBorder * realRadiusAndBorder)
                    {
                        SetAlphaInEdge(point1, beacon.BeaconId);
                        SetAlphaInEdge(point2, beacon.BeaconId);
                        SetAlphaInEdge(point3, beacon.BeaconId);
                        SetAlphaInEdge(point4, beacon.BeaconId);
                    }
                }
            }
        }

        private void SetAlphaInEdge(Vector2Int position, string setterId)
        {
            var index = position.y * _texture.width + position.x;

            if (index < 0 || index >= _colors.Length) return;
            
            var alpha = _colors[index].a;
            var newValue =  Mathf.Min(Mathf.Max(alpha, 0.5f), 1);

            if (_colors[index].a == 0 && setterId != _setterIds[index]) return;
            
            _colors[index].a = newValue;
        }
    }
}