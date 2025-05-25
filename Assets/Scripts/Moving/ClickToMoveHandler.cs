using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moving
{
    [RequireComponent(typeof(PlayerLookDirectionController), typeof(PlayerMovementDirectionController))]
    public class ClickToMoveHandler : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _clickableLayers;
        [SerializeField]
        private ParticleSystem _clickEffect;
        private PlayerLookDirectionController _playerLookDirectionController;
        private PlayerMovementDirectionController _playerMovementDirectionController;
        private InputActions _inputActions;
        private Action<InputAction.CallbackContext> _moveActionHandler;
        private void Awake()
        {
            _playerLookDirectionController = GetComponent<PlayerLookDirectionController>();
            _playerMovementDirectionController = GetComponent<PlayerMovementDirectionController>();
            _inputActions = new InputActions();
            AssignInputs();
        }
        private void AssignInputs()
        {
            _moveActionHandler = _ => ClickToMove();
            _inputActions.Main.Move.performed += _moveActionHandler;
        }
        private void ClickToMove()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayers))
            {
                _playerLookDirectionController.LookDirection = hit.point - transform.position;
                _playerMovementDirectionController.MovementDirection = hit.point;
                if (_clickEffect != null)
                {
                    Instantiate(_clickEffect, hit.point += new Vector3(0, 0.1f, 0), _clickEffect.transform.rotation);
                }
            }
        }
        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            if (_moveActionHandler != null)
            {
                _inputActions.Main.Move.performed -= _moveActionHandler;
            }
            _inputActions.Disable();
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
            {
                _inputActions.Dispose();
            }
        }
    }
}