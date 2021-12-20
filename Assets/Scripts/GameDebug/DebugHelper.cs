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
        public ShipsChannel ShipChannel;
        public Ship ShipPrefab;
        
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

        [UnityEditor.MenuItem("Spawn Ship/Manuvar")]
        public static void SpawnManuvar()
        {
            SpawnShip(HelperInstance.ManuvarMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Frigg")]
        public static void SpawnFrigg()
        {
            SpawnShip(HelperInstance.FriggMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Geallon")]
        public static void SpawnGeallon()
        {
            SpawnShip(HelperInstance.GeallonMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Spool")]
        public static void SpawnSpool()
        {
            SpawnShip(HelperInstance.SpoolMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Snoocher")]
        public static void SpawnSnoocher()
        {
            SpawnShip(HelperInstance.SnoocherMeta);
        }
        
        [UnityEditor.MenuItem("Spawn Ship/Brinagyte")]
        public static void SpawnBrinagyte()
        {
            SpawnShip(HelperInstance.BrinagyteMeta);
        }

        private static void SpawnShip(ShipMeta shipMeta)
        {
            var ship = Instantiate(HelperInstance.ShipPrefab);
            ship.GetComponent<ShipStats>().SetMeta(shipMeta);
            HelperInstance.ShipChannel.OnPlaceNewShip(ship);
        }
    }
}