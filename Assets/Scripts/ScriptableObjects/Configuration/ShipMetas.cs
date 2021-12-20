using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

namespace ScriptableObjects.Configuration
{
    [CreateAssetMenu(menuName = "Configuration/Ship Metas")]
    public class ShipMetas : ScriptableObject
    {
        public List<ShipMeta> AllShipMetas;
        [HideInInspector] public Dictionary<string, ShipMeta> ShipMetaMap;
        private void OnEnable()
        {
#if UNITY_EDITOR
            var shipMetas = AssetsHelper.GetAllAssets<ShipMeta>();

            AllShipMetas ??= new List<ShipMeta>();
            AllShipMetas.Clear();

            UnityEditor.AssetDatabase.Refresh();
            
            foreach (var shipMeta in shipMetas)
            {
                if (string.IsNullOrEmpty(shipMeta.Id))
                {
                    shipMeta.Id = Guid.NewGuid().ToString();
                    UnityEditor.EditorUtility.SetDirty(shipMeta);
                }
                AllShipMetas.Add(shipMeta);
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
#endif
            ShipMetaMap = AllShipMetas.ToDictionary(x => x.Id, x => x);
        }

        public ShipMeta GetMetaById(string id)
        {
            if (!ShipMetaMap.ContainsKey(id)) return null;

            return ShipMetaMap[id];
        }
    }
}