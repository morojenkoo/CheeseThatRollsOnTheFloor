using UnityEngine;

namespace Moving
{
    public class PlayerMovementController : CharacterMovementController
    {
        [SerializeField]
        LayerMask clickableLayers;
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon; 
        private Vector3 _screenPosition;
        private Vector3 _worldPosition;
        private InputActions _inputActions;
        private UnityEngine.AI.NavMeshAgent _navMeshAgent;
        public Vector3 LookDirection { get; set; }
        private void Awake()
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _inputActions = new InputActions();
            AssignInputs();
        }
        private void Update()
        {
            if (LookDirection != Vector3.zero)
                FaceTarget();
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
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
            {
                _navMeshAgent.destination = hit.point;
                LookDirection = (hit.point - transform.position).normalized;
            }
            
        }

        private void FaceTarget()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;
            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookDirection, Vector3.up), 
                    10f * Time.deltaTime);
                transform.rotation = newRotation;
            }
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