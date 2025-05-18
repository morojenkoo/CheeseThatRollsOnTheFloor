using UnityEngine;

namespace Moving
{
    public class PlayerMovementController : CharacterMovementController
    {
        [SerializeField]
        private LayerMask _clickableLayers;
        [SerializeField]
        private ParticleSystem _clickEffect;
        [SerializeField]
        private float _lookRotationSpeed = 10f;
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon; 
        private Vector3 _screenPosition;
        private Vector3 _worldPosition;
        private InputActions _inputActions;
        private UnityEngine.AI.NavMeshAgent _navMeshAgent;
        private Animator _animator;
        public Vector3 LookDirection { get; set; }
        private void Awake()
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _inputActions = new InputActions();
            _animator = GetComponent<Animator>();
            AssignInputs();
        }
        private void Update()
        {
            if (LookDirection != Vector3.zero)
            {
                FaceTarget();
                SetAnimations();
            }
            
            if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            {
                LookDirection = (_navMeshAgent.steeringTarget - transform.position).normalized;
            }
        }
        private void AssignInputs()
        {
            _inputActions.Main.Move.performed += ctx => ClickToMove();
        }

        private void ClickToMove()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayers))
            {
                _navMeshAgent.destination = hit.point;
                LookDirection = (hit.point - transform.position).normalized;
                if (_clickEffect != null)
                {
                    Instantiate(_clickEffect, hit.point += new Vector3(0, 0.1f, 0), _clickEffect.transform.rotation);
                }
            }
        }

        private void FaceTarget()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;
            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookDirection, Vector3.up), 
                    _lookRotationSpeed * Time.deltaTime);
                transform.rotation = newRotation;
            }
        }

        private void SetAnimations()
        {
            _animator.SetBool("IsMoving", _navMeshAgent.velocity != Vector3.zero);
        }
        private void OnEnable()
        {
            _inputActions.Enable();
        }
        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}