using System;
using UnityEngine;

namespace ScriptableObjects.Models
{
    [Serializable]
    public class Team
    {
        public string Id;
        public bool IsOccupied;
        public bool IsPlayerTeam;
        public Color Color;
    }
}