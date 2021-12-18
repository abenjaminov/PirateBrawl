using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = Unity.Mathematics.Random;

namespace Game
{
    public enum TileType
    {
        Water,
        Land
    }
    
    public class ArenaGenerator : MonoBehaviour
    {
        //public Sprite
        [SerializeField] private Arena Arena;
        [SerializeField] private Tilemap LandTileMap;
        [SerializeField] private Tilemap WaterTileMap;
        public TileBase LandTile;
        public TileBase WaterTile;

        public TileType[,] Grid;

        [Range(0,100)]
        public int RandomFillPercent;
        public int SmoothAmount;
        
        private Vector2 ArenaSize;
        private System.Random GeneratorRandom;
        
        private void Awake()
        {
            var collider = Arena.GetComponent<BoxCollider2D>();
            ArenaSize = collider.size;

            Grid = new TileType[(int)ArenaSize.x, (int)ArenaSize.y];

            RandomFillMap();
            
            for (int i = 0; i < SmoothAmount; i++)
            {
                SmoothStep();
            }

            InitializeWater();
            UpdateArena();
        }

        private void RandomFillMap()
        {
            GeneratorRandom = new System.Random(DateTime.Now.Millisecond);

            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    if (x == 0 || x == (int)ArenaSize.x - 1 || y == 0 || y == (int)ArenaSize.y - 1)
                    {
                        Grid[x, y] = TileType.Water;
                    }
                    else
                    {
                        Grid[x, y] = GeneratorRandom.Next(0, 100) < RandomFillPercent ? TileType.Land : TileType.Water;
                    }
                }
            }
        }

        [ContextMenu("SmoothStep")]
        private void SmoothStep()
        {
            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    var sandCount = GetSurroundingWallCount(x, y);

                    if (sandCount > 4)
                    {
                        Grid[x, y] = TileType.Water;
                    }
                    else if (sandCount < 4)
                    {
                        Grid[x, y] = TileType.Land;
                    }
                }
            }
        }

        private int GetSurroundingWallCount(int x, int y)
        {
            var count = 0;

            for (int neighborX = x-1; neighborX <= x + 1; neighborX++)
            {
                for (int neightborY = y-1; neightborY <= y + 1; neightborY++)
                {
                    if (neighborX >= 0 && neighborX < ArenaSize.x && neightborY >= 0 && neightborY < ArenaSize.y)
                    {
                        if (neighborX != x || neightborY != y)
                        {
                            count += Grid[neighborX, neightborY] == TileType.Water ? 1 : 0;
                        }    
                    }
                    else
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void InitializeWater()
        {
            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    WaterTileMap.SetTile(new Vector3Int(x, y, 0), WaterTile);    
                }
            }
        }
        
        [ContextMenu("UpdateArena")]
        private void UpdateArena()
        {
            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    if (Grid[x, y] == TileType.Land)
                    {
                        LandTileMap.SetTile(new Vector3Int(x, y, 0), LandTile);    
                    }
                    
                }
            }
        }
    }
}