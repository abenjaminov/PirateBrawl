using System.Collections.Generic;
using Game.Ships;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Configuration;
using ScriptableObjects.Interactions;
using ScriptableObjects.Interactions.Models;
using ScriptableObjects.Interactions.Receivers;
using ScriptableObjects.Models;
using UnityEngine;

namespace Game
{
    public class FleetBase : MonoBehaviour
    {
        private Dictionary<string, Ship> _ships = new Dictionary<string, Ship>();
        [SerializeField] private ShipsChannel ShipsChannel;
        [SerializeField] private List<Transform> SpawnPoints;
        [SerializeField] private Ship ShipPrefab;
        [SerializeField] private ShipMetas ShipMetas;
        [SerializeField] private InteractionsAgent InteractionsAgent;
        [SerializeField] private GameInteractionsReceiver GameInteractionsReceiver;
        [SerializeField] private Teams Teams;
        
        private Team Team;

        private void Awake()
        {
            ShipsChannel.OnPlaceNewShipEvent += OnPlaceNewShipEvent;
            GameInteractionsReceiver.OnMoveShipReceivedEvent += OnMoveShipEvent;
            GameInteractionsReceiver.OnSpawnShipReceivedEvent += OnSpawnShipReceivedEvent;
            Team = Teams.GetUnoccupiedTeam();
            Teams.OccupyTeam(Team);
        }

        private void OnSpawnShipReceivedEvent(SpawnShipInfo info)
        {
            if (info.Team.Id != Team.Id) return; 
            var shipMeta = ShipMetas.GetMetaById(info.ShipMetaId);
            
            SpawnShipInternal(info.ShipId, shipMeta);
        }

        private void OnMoveShipEvent(MoveShipInfo arg0)
        {
            
        }

        private void OnDestroy()
        {
            ShipsChannel.OnPlaceNewShipEvent -= OnPlaceNewShipEvent;
            GameInteractionsReceiver.OnMoveShipReceivedEvent -= OnMoveShipEvent;
            GameInteractionsReceiver.OnSpawnShipReceivedEvent -= OnSpawnShipReceivedEvent;
        }

        private void OnPlaceNewShipEvent(string id, ShipMeta shipMeta)
        {
            SpawnShipInternal(id, shipMeta);
        }

        private void SpawnShipInternal(string id, ShipMeta shipMeta)
        {
            var index = Random.Range(0, SpawnPoints.Count);
            var spawnPoint = SpawnPoints[index];

            var ship = InteractionsAgent.SpawnShip(new SpawnShipInfo()
            {
                ShipId = id,
                ShipPrefab = ShipPrefab,
                ShipMeta = shipMeta,
                Position = spawnPoint.position,
                Rotation = spawnPoint.transform.localRotation,
                Team = Team
            });

            _ships.Add(id, ship);
        }
    }
}