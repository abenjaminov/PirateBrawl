using GameInput.Models;

namespace GameInput.Interfaces
{
    public interface IClickHandler
    {
        void HandleClick(ClickEventInfo eventInfo);
    }
}