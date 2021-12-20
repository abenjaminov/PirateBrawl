using GameInput.Interfaces;
using GameInput.Models;
using ScriptableObjects.Channels;
using ScriptableObjects.Models;
using UnityEngine;

namespace Game
{
    public class Arena : MonoBehaviour, IClickHandler
    {
        [SerializeField] private GameChannel GameChannel;
        
        public void HandleClick(ClickEventInfo eventInfo)
        {
            GameChannel.OnArenaClicked(new ArenaClickedEventInfo()
            {
                WorldPosition = eventInfo.WorldPosition
            });
        }
    }
}