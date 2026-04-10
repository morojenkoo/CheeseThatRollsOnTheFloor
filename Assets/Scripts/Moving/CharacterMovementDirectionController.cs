using Characters;
using UnityEngine;

namespace Moving
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class CharacterMovementDirectionController : MonoBehaviour
    {
        [SerializeField] protected float  _movementSpeed = 3f;
        [SerializeField] protected float _stoppingDistance = 0.1f;
        protected Rigidbody _rb;
        protected float _speedAcceleration;
        public Vector3 MovementDirection { get; set; }
        
        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            //_speedAcceleration = GetComponent<BaseCharacter>().speedAcceleration;
        }
        protected void FixedUpdate()
        {
            if (MovementDirection != Vector3.zero)
            {
                Move();
            }
        }

        protected virtual void Move()
        {
            Vector3 direction = (MovementDirection - transform.position).normalized;
            float distance = (transform.position - MovementDirection).sqrMagnitude;
            _rb.linearVelocity = _movementSpeed * GetComponent<BaseCharacter>().speedAcceleration * direction;
            if (distance < _stoppingDistance)
            {
                _rb.linearVelocity = Vector3.zero;
                MovementDirection = Vector3.zero;
            }
        }

    }
}