using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public enum TileType
    {
        Water,
        Sand
    }
    
    public class ArenaGenerator : MonoBehaviour
    {
        //public Sprite
        [SerializeField] private Arena Arena;
        [SerializeField] private Tilemap TileMap;
        public TileBase SandTile;
        public TileBase WaterTile;

        public TileType[,] Grid;

        [Range(0,100)]
        public int RandomFillPercent;
        public int SmoothAmount;
        
        private Vector2 ArenaSize;
        
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
            UpdateArena();
        }

        private void RandomFillMap()
        {
            var random = new System.Random(DateTime.Now.Millisecond);

            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    if (x == 0 || x == (int)ArenaSize.x - 1 || y == 0 || y == (int)ArenaSize.y - 1)
                    {
                        Grid[x, y] = TileType.Sand;
                    }
                    else
                    {
                        Grid[x, y] = random.Next(0, 100) < RandomFillPercent ? TileType.Sand : TileType.Water;
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
                        Grid[x, y] = TileType.Sand;
                    }
                    else if (sandCount < 4)
                    {
                        Grid[x, y] = TileType.Water;
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
                            count += Grid[neighborX, neightborY] == TileType.Sand ? 1 : 0;
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

        [ContextMenu("UpdateArena")]
        private void UpdateArena()
        {
            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    TileMap.SetTile(new Vector3Int(x, y, 0), Grid[x, y] == TileType.Sand ? SandTile : WaterTile);
                }
            }
        }
    }
}