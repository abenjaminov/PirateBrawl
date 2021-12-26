﻿using System;
using Game.Ships;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Configuration;
using ScriptableObjects.Interactions.Models;
using ScriptableObjects.Interactions.Receivers;
using UnityEngine;

namespace GameDebug
{
    public class DebugHelper : MonoBehaviour
    {
        public Teams TeamsList;
        public GameInteractionsReceiver EnemyReceiver;
        public ShipsChannel FriendlyShipChannel;
        public Ship ShipPrefab;
        
        [Header("Ship metas")]
        public ShipMeta SpoolMeta;
        public ShipMeta SnoocherMeta;
        public ShipMeta BrinagyteMeta;
        public ShipMeta GeallonMeta;
        public ShipMeta FriggMeta;
        public ShipMeta ManuvarMeta;

        private static DebugHelper HelperInstance;

        private void Awake()
        {
            HelperInstance = this;
        }

        [UnityEditor.MenuItem("Spawn Ship/Friendly/Manuvar")]
        public static void SpawnFriendlyManuvar()
        {
            SpawnShip(HelperInstance.ManuvarMeta, HelperInstance.FriendlyShipChannel);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Frigg")]
        public static void SpawnFriendlyFrigg()
        {
            SpawnShip(HelperInstance.FriggMeta, HelperInstance.FriendlyShipChannel);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Geallon")]
        public static void SpawnFriendlyGeallon()
        {
            SpawnShip(HelperInstance.GeallonMeta, HelperInstance.FriendlyShipChannel);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Spool")]
        public static void SpawnFriendlySpool()
        {
            SpawnShip(HelperInstance.SpoolMeta, HelperInstance.FriendlyShipChannel);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Snoocher")]
        public static void SpawnFriendlySnoocher()
        {
            SpawnShip(HelperInstance.SnoocherMeta, HelperInstance.FriendlyShipChannel);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Brinagyte")]
        public static void SpawnFriendlyBrinagyte()
        {
            SpawnShip(HelperInstance.BrinagyteMeta, HelperInstance.FriendlyShipChannel);
        }

        [UnityEditor.MenuItem("Spawn Ship/Enemy/Manuvar")]
        public static void SpawnEnemyManuvar()
        {
            HelperInstance.EnemyReceiver.SpawnShip(new SpawnShipInfo()
            {
                ShipId = Guid.NewGuid().ToString(),
                Team = HelperInstance.TeamsList.GetFirstEnemyTeam(),
                ShipMetaId = HelperInstance.ManuvarMeta.Id
            });
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Enemy/Frigg")]
        public static void SpawnEnemyFrigg()
        {
            HelperInstance.EnemyReceiver.SpawnShip(new SpawnShipInfo()
            {
                ShipId = Guid.NewGuid().ToString(),
                Team = HelperInstance.TeamsList.GetFirstEnemyTeam(),
                ShipMetaId = HelperInstance.FriggMeta.Id
            });
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Enemy/Geallon")]
        public static void SpawnEnemyGeallon()
        {
            HelperInstance.EnemyReceiver.SpawnShip(new SpawnShipInfo()
            {
                ShipId = Guid.NewGuid().ToString(),
                Team = HelperInstance.TeamsList.GetFirstEnemyTeam(),
                ShipMetaId = HelperInstance.GeallonMeta.Id
            });
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Enemy/Spool")]
        public static void SpawnEnemySpool()
        {
            HelperInstance.EnemyReceiver.SpawnShip(new SpawnShipInfo()
            {
                ShipId = Guid.NewGuid().ToString(),
                Team = HelperInstance.TeamsList.GetFirstEnemyTeam(),
                ShipMetaId = HelperInstance.SpoolMeta.Id
            });
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Enemy/Snoocher")]
        public static void SpawnEnemySnoocher()
        {
            HelperInstance.EnemyReceiver.SpawnShip(new SpawnShipInfo()
            {
                ShipId = Guid.NewGuid().ToString(),
                Team = HelperInstance.TeamsList.GetFirstEnemyTeam(),
                ShipMetaId = HelperInstance.SnoocherMeta.Id
            });
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Enemy/Brinagyte")]
        public static void SpawnEnemyBrinagyte()
        {
            HelperInstance.EnemyReceiver.SpawnShip(new SpawnShipInfo()
            {
                ShipId = Guid.NewGuid().ToString(),
                Team = HelperInstance.TeamsList.GetFirstEnemyTeam(),
                ShipMetaId = HelperInstance.BrinagyteMeta.Id
            });
        }
        
        private static void SpawnShip(ShipMeta shipMeta, ShipsChannel shipChannel)
        {
            var id = Guid.NewGuid().ToString();
            shipChannel.OnPlaceNewShip(id, shipMeta);
        }
    }
}