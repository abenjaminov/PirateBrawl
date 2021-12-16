using GameInput.Models;

namespace GameInput.Interfaces
{
    public interface IDoubleClickHandler
    {
        void HandleDoubleClick(ClickEventInfo eventInfo);
    }
}