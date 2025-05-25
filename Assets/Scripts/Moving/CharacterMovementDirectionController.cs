using UnityEngine;

namespace Moving
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public abstract class CharacterMovementDirectionController : MonoBehaviour
    {
        [SerializeField] private float  _movementSpeed = 3f;
        [SerializeField] private float _stoppingDistance = 0.1f;
        private Rigidbody _rb;
        private Animator _animator;
        public Vector3 MovementDirection { get; set; }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if (MovementDirection != Vector3.zero)
            {
                Move();
                SetAnimations();
            }
        }

        private void Move()
        {
            Vector3 direction = (MovementDirection - transform.position).normalized;
            float distance = (transform.position - MovementDirection).sqrMagnitude;
            _rb.linearVelocity = direction * _movementSpeed;
            if (distance < _stoppingDistance)
            {
                _rb.linearVelocity = Vector3.zero;
                MovementDirection = Vector3.zero;
            }
        }
        private void SetAnimations()
        {
            _animator.SetBool("IsMoving", _rb.linearVelocity != Vector3.zero);
        }
    }
}