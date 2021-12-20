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
        Land,
        Empty
    }
    
    public class ArenaGenerator : MonoBehaviour
    {
        //public Sprite
        [SerializeField] private Arena Arena;
        [SerializeField] private Tilemap LandTileMap;
        [SerializeField] private Tilemap WaterTileMap;
        [SerializeField] private Tilemap LandDecorationTileMap;
        [SerializeField] private List<TileBase> LandCenterDecorationTiles;
        public TileBase LandTile;
        public TileBase WaterTile;

        public TileType[,] Grid;

        [Range(0,100)]
        public int RandomFillPercent;
        [Range(0,100)]
        public int RandomIslandPercentage;

        [Range(0,100)]
        public int RandomLandDecorationsPercentage;
        
        public int BaseIslandSize;
        public int SmoothAmount;
        
        private Vector2 ArenaSize;
        private System.Random GeneratorRandom;
        
        private void Awake()
        {
            // var collider = Arena.GetComponent<BoxCollider2D>();
            // ArenaSize = collider.size;
            //
            // Grid = new TileType[(int)ArenaSize.x, (int)ArenaSize.y];
            // GeneratorRandom = new System.Random(DateTime.Now.Millisecond);
            //
            // InitGrid();
            // RandomFillMap();
            //
            // for (int i = 0; i < SmoothAmount; i++)
            // {
            //     SmoothStep();
            // }
            //
            // InitializeWater();
            // UpdateArena();
        }

        private void InitGrid()
        {
            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    Grid[x, y] = TileType.Empty;
                }
            }
        }
        
        private void RandomFillMap()
        {
            for (var x = 0; x < ArenaSize.x; x++)
            {
                for (var y = 0; y < ArenaSize.y; y++)
                {
                    if (x >= ArenaSize.x) continue;
                    if (Grid[x, y] != TileType.Empty) continue;
                    
                    var islandRandom = GeneratorRandom.Next(0, 101);

                    if (islandRandom < RandomIslandPercentage)
                    {
                        for (int x_minor = 0; x_minor < BaseIslandSize; x_minor++)
                        {
                            for (int y_minor = 0; y_minor < BaseIslandSize; y_minor++)
                            {
                                if (x + x_minor < ArenaSize.x && y + y_minor < ArenaSize.y)
                                {
                                    Grid[x + x_minor, y + y_minor] = TileType.Land;    
                                }
                            }   
                        }
                            
                        y+=BaseIslandSize-1;
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
                    var sandCount = GetSurroundingWaterCount(x, y);

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

        private int GetSurroundingWaterCount(int x, int y)
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

            DecorateLand();
        }

        private void DecorateLand()
        {
            for (int x = 0; x < ArenaSize.x; x++)
            {
                for (int y = 0; y < ArenaSize.y; y++)
                {
                    if (Grid[x, y] != TileType.Land) continue;
                    
                    var waterCount = GetSurroundingWaterCount(x, y);

                    if (waterCount == 0)
                    {
                        var shouldDecorate = GeneratorRandom.Next(0,101) < RandomLandDecorationsPercentage;

                        if (shouldDecorate)
                        {
                            var index = GeneratorRandom.Next(0, LandCenterDecorationTiles.Count);
                            LandDecorationTileMap.SetTile(new Vector3Int(x, y, 0),LandCenterDecorationTiles[index]);
                        }
                    }
                }
            }
        }
    }
}