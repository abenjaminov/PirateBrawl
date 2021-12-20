using System;
using Game.Ships;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace GameDebug
{
    public class DebugHelper : MonoBehaviour
    {
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
        public static void SpawnManuvar()
        {
            SpawnShip(HelperInstance.ManuvarMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Frigg")]
        public static void SpawnFrigg()
        {
            SpawnShip(HelperInstance.FriggMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Geallon")]
        public static void SpawnGeallon()
        {
            SpawnShip(HelperInstance.GeallonMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Spool")]
        public static void SpawnSpool()
        {
            SpawnShip(HelperInstance.SpoolMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Snoocher")]
        public static void SpawnSnoocher()
        {
            SpawnShip(HelperInstance.SnoocherMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Friendly/Brinagyte")]
        public static void SpawnBrinagyte()
        {
            SpawnShip(HelperInstance.BrinagyteMeta);
        }

        private static void SpawnShip(ShipMeta shipMeta)
        {
            var ship = Instantiate(HelperInstance.ShipPrefab);
            ship.GetComponent<ShipStats>().SetMeta(shipMeta);
            HelperInstance.FriendlyShipChannel.OnPlaceNewShip(ship);
        }
    }
}