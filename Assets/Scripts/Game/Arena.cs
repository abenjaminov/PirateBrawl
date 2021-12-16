using ScriptableObjects.Channels;
using ScriptableObjects.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class Arena : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameChannel GameChannel;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            // TODO : Check the type of terrain that was clicked

            GameChannel.OnArenaClicked(new ArenaClickedEventInfo()
            {
                WorldPosition = eventData.pointerCurrentRaycast.worldPosition
            });
        }
    }
}