using UnityEngine;

namespace Moving
{
    public class PlayerMovementDirectionController : CharacterMovementDirectionController
    {
        private float radius;
        protected override void Awake()
        {
            base.Awake();
            radius = GetComponent<SphereCollider>().radius * transform.lossyScale.x;
        }
        protected override void Move()
        {
            base.Move();
            
            if (_rb.linearVelocity.magnitude > 0.01f)
            {
                float dist = _rb.linearVelocity.magnitude * Time.deltaTime;
                float angle = dist / radius * Mathf.Rad2Deg;
                Vector3 axis = Vector3.Cross(Vector3.up, _rb.linearVelocity.normalized);
                transform.Rotate(axis, angle, Space.World);
            }
        }
    }
}