using UnityEngine;
using UnityEngine.InputSystem;

namespace Lesson7
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;
        [SerializeField] private PlayerInputActions _playerInputActions;

        private Transform _transform;
        private Vector2 _moveInput;

        private InputAction _moveAction;

        private void Awake()
        {
            _moveAction = _playerInputActions.Player.Move;
            _moveAction.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _moveAction.canceled += ctx => _moveInput = Vector2.zero;
        }

        private void OnEnable()
        {
            _moveAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
        }

        private void Start()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            Vector3 forward = _transform.forward;
            Vector3 right = _transform.right;
            Vector3 movement = forward * _moveInput.y + right * _moveInput.x;
            _characterController.SimpleMove(movement * _speed);
        }
    }
}
