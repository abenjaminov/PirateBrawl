using System.Collections.Generic;
using Game.Models;
using Game.Ships;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Interactions;
using ScriptableObjects.Interactions.Models;
using UnityEngine;

namespace Game
{
    public class FleetBase : MonoBehaviour
    {
        [SerializeField] private ShipsChannel ShipsChannel;
        [SerializeField] private List<Transform> SpawnPoints;
        [SerializeField] private Ship ShipPrefab;

        [SerializeField] private InteractionsAgent InteractionsAgent;
        
        private Team Team;

        private void Awake()
        {
            ShipsChannel.OnPlaceNewShipEvent += OnPlaceNewShipEvent;
        }

        private void OnDestroy()
        {
            ShipsChannel.OnPlaceNewShipEvent += OnPlaceNewShipEvent;
        }

        private void OnPlaceNewShipEvent(string id, ShipMeta shipMeta)
        {
            var index = Random.Range(0, SpawnPoints.Count);
            var spawnPoint = SpawnPoints[index];

            InteractionsAgent.SpawnShip(new SpawnShipInfo()
            {
                Id = id,
                ShipPrefab = ShipPrefab,
                ShipMeta = shipMeta,
                Position = spawnPoint.position,
                Rotation = spawnPoint.transform.localRotation,
                Team = Team
            });
        }
    }
}