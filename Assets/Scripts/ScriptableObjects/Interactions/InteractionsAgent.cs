﻿using Game.Ships;
using ScriptableObjects.Interactions.Models;
using UnityEngine;

namespace ScriptableObjects.Interactions
{
    [CreateAssetMenu(menuName = "Interactions/Agents/Base", fileName = "Base interaction agent")]
    public class InteractionsAgent : ScriptableObject
    {
        public virtual void MoveShip(MoveShipInfo info)
        {
            info.Ship.Movement.SetTarget(info.WorldPosition);
        }

        public virtual void SpawnShip(SpawnShipInfo info)
        {
            var ship = Object.Instantiate(info.ShipPrefab);
            ship.Id = info.Id;
            ship.Stats.SetMeta(info.ShipMeta);
            ship.SpawnShip(info.Position, info.Rotation);
        }
    }
}