using ScriptableObjects.Models;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Game Channel", menuName = "Channels/Game")]
    public class GameChannel : ScriptableObject
    {
        public UnityAction<ArenaClickedEventInfo> OnArenaClickedEvent;

        public void OnArenaClicked(ArenaClickedEventInfo info)
        {
            OnArenaClickedEvent?.Invoke(info);   
        }
    }
}