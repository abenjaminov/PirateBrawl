﻿using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Game.Ships
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField] private ShipMeta ShipMeta;

        private const float MaxAngleForMovement = 20;
        private Vector3 _target;
        private float currentSpeed = 0;

        public void SetTarget(Vector3 target)
        {
            _target = target;

            StopCoroutine(nameof(MoveToTarget));
            StartCoroutine(nameof(MoveToTarget));
        }

        IEnumerator MoveToTarget()
        {
            var maxSpeed = currentSpeed;

            // Gain speed while rotating to the target
            while (Vector2.Distance(transform.position, _target) > .3f)
            {
                var directionToTarget = (_target - transform.position).normalized;
                var angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

                var rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
                var currentRotation = transform.rotation;

                var angleToDestinationRotation = Quaternion.Angle(rotation, transform.rotation);
                
                if (angleToDestinationRotation > MaxAngleForMovement)
                {
                    transform.rotation = Quaternion.Slerp(currentRotation, rotation, Time.deltaTime);
                    
                    currentSpeed = Mathf.Max(0,currentSpeed - ((maxSpeed / ShipMeta.SpeedChangeRate) * Time.deltaTime));
                    transform.position += transform.right * Time.deltaTime * currentSpeed;
                    
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(currentRotation, rotation, Time.deltaTime);

                    currentSpeed = Mathf.Min(ShipMeta.Speed, currentSpeed + (ShipMeta.Speed / ShipMeta.SpeedChangeRate) * Time.deltaTime);
                    transform.position += transform.right * Time.deltaTime * currentSpeed;
                
                    yield return new WaitForEndOfFrame();
                }
            }

            maxSpeed = currentSpeed;

            while (currentSpeed > 0)
            {
                currentSpeed = Mathf.Max(0,currentSpeed - ((maxSpeed / ShipMeta.SpeedChangeRate) * Time.deltaTime));
                transform.position += transform.right * Time.deltaTime * currentSpeed;
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