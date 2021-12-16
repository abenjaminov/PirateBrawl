using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Game.Ships
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField] private ShipMeta ShipMeta;
        
        private Vector3 _target;

        public void SetTarget(Vector3 target)
        {
            _target = target;

            StopCoroutine(nameof(MoveToTarget));
            StartCoroutine(nameof(MoveToTarget));
        }

        IEnumerator MoveToTarget()
        {
            var speed = ShipMeta.Speed;
            
            while (Vector3.Distance(transform.position, _target) > .3f)
            {
                var directionToTarget = (_target - transform.position).normalized;
                var angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                var rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
                var current = transform.rotation;
                
                transform.rotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
                
                 transform.position += transform.right * Time.deltaTime * speed;
                
                yield return new WaitForEndOfFrame();
            }

            var maxSpeed = speed;

            while (speed > 0)
            {
                speed -= (maxSpeed / ShipMeta.SlowRate) * Time.deltaTime;
                transform.position += transform.right * Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
            
            yield return null;
        }

        void OnDrawGizmos()
        {
            DrawHelperAtCenter(this.transform.right, Color.red, 2f);
        }
        
        private void DrawHelperAtCenter(
            Vector3 direction, Color color, float scale)
        {
            Gizmos.color = color;
            Vector3 destination = transform.position + direction * scale;
            Gizmos.DrawLine(transform.position, destination);
        }
    }
}