using UnityEngine;
using UnityEngine.InputSystem;

namespace Lesson7
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;
        [SerializeField] private GrenadeThrower _grenadeThrower; // Додаємо поле для кидка гранати

        private PlayerInput _playerInput;
        private Transform _transform;
        private Vector2 _moveInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _playerInput.actions.Enable();
        }

        private void OnDisable()
        {
            _playerInput.actions.Disable();
        }

        private void Start()
        {
            _transform = transform;
        }

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnPrimary(InputValue value)
        {
            if (value.isPressed && _grenadeThrower != null)
            {
                _grenadeThrower.ThrowGrenade();
            }
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