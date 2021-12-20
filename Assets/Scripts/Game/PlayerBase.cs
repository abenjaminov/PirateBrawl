using System.Collections.Generic;
using Game.Ships;
using ScriptableObjects.Channels;
using ScriptableObjects.Models;
using UnityEngine;

namespace Game
{
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField] private ShipsChannel ShipsChannel;
        [SerializeField] private List<Transform> SpawnPoints;
        private Team Team;

        private void Awake()
        {
            ShipsChannel.OnPlaceNewShipEvent += OnPlaceNewShipEvent;
        }

        private void OnDestroy()
        {
            ShipsChannel.OnPlaceNewShipEvent += OnPlaceNewShipEvent;
        }

        private void OnPlaceNewShipEvent(Ship shipToSpawn)
        {
            var index = Random.Range(0, SpawnPoints.Count);
            var spawnPoint = SpawnPoints[index];
            
            shipToSpawn.SpawnShip(Team, spawnPoint.position, spawnPoint.transform.localRotation);
        }
    }
}