using System.Collections.Generic;
using System.Linq;
using GameInput.Interfaces;
using GameInput.Models;
using UnityEngine;

namespace GameInput
{
    public class ClickSystem : MonoBehaviour
    {
        private float _timeBetweenClicks = 0;
        private int _numberOfClicks;
        [SerializeField] private float _doubleClickTime;
        private float previousClickTime;

        [SerializeField] private LayerMask _layerMask;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _numberOfClicks++;
                previousClickTime = Time.time;

                if ((Time.time - previousClickTime) < _doubleClickTime && _numberOfClicks == 2)
                {
                    HandleDoubleClick();
                    _numberOfClicks = 0;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                HandleRightClick();
            }
            
            if (_numberOfClicks == 1 && (Time.time - previousClickTime) > _doubleClickTime)
            {
                HandleSingleClick();
                _numberOfClicks = 0;
            }
        }

        private void HandleSingleClick()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var clickableObjects = GetClickableObjects<IClickHandler>(worldPosition);

            if (!clickableObjects.Any()) return;
            
            foreach (var clickableObject in clickableObjects)
            {
                clickableObject.HandleClick(new ClickEventInfo()
                {
                    WorldPosition = worldPosition
                });
            }
        }

        private IEnumerable<T> GetClickableObjects<T>(Vector3 worldPosition)
        {
            var hit = Physics2D.RaycastAll(worldPosition, Vector3.forward,1000, _layerMask).ToList();
            hit.AddRange(Physics2D.RaycastAll(Input.mousePosition, Vector3.forward,1000, _layerMask));
            
            var clickableObjects = hit.Select(x => x.collider.GetComponents<T>()).Where(x => x != null).ToList();

            return clickableObjects.SelectMany(x => x).Take(1);
        }
        
        private void HandleDoubleClick()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var clickableObjects = GetClickableObjects<IDoubleClickHandler>(worldPosition);

            if (!clickableObjects.Any()) return;
            
            foreach (var clickableObject in clickableObjects)
            {
                clickableObject.HandleDoubleClick(new ClickEventInfo()
                {
                    WorldPosition = worldPosition
                });
            }
        }

        private void HandleRightClick()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var clickableObjects =
                GetClickableObjects<IRightClickHandler>(worldPosition);
            
            foreach (var clickableObject in clickableObjects)
            {
                clickableObject.HandleRightClick(new ClickEventInfo()
                {
                    WorldPosition = worldPosition
                });
            }
        }
    }
}