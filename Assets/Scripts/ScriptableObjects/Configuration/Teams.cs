using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Models;
using UnityEngine;

namespace ScriptableObjects.Configuration
{
    [CreateAssetMenu(menuName = "Configuration/Teams")]
    public class Teams : ScriptableObject
    {
        [SerializeField] private List<Team> AllTeams;
        private void OnEnable()
        {
            foreach (var team in AllTeams)
            {
                team.IsOccupied = false;
                if (string.IsNullOrEmpty(team.Id))
                    team.Id = Guid.NewGuid().ToString();
            }
        }

        public Team GetFirstEnemyTeam()
        {
            return AllTeams.FirstOrDefault(x => x.IsOccupied && !x.IsPlayerTeam);
        }

        public Team GetPlayerTeam()
        {
            return AllTeams.FirstOrDefault(x => x.IsPlayerTeam);
        }
        
        public Team GetUnoccupiedTeam()
        {
            return AllTeams.FirstOrDefault(x => !x.IsOccupied);
        }

        public void OccupyTeam(Team team)
        {
            team.IsOccupied = true;
        }
    }
}