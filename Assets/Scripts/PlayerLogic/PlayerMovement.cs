using Descriptors;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Transform _playerGfxTransform;
        
        [Inject]
        private PlayerInputService _playerInputService = null!;
        [Inject]
        private PlayerDescriptor _playerDescriptor = null!;

        private Rigidbody2D _rigidbody = null!;
        
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_playerInputService == null || _playerDescriptor == null)
            {
                return;
            }

            Move(_playerInputService.MoveDirection, _playerDescriptor.MoveSpeed);
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            if (_moveDirection.magnitude > 0)
            {
                _playerGfxTransform.up = _moveDirection;
            }
        }

        private void Move(Vector3 moveDirection, float moveSpeed)
        {
            _moveDirection = transform.right * moveDirection.x +
                             transform.up * moveDirection.y;

            if (_moveDirection.magnitude > 1)
            {
                _moveDirection.Normalize();
            }

            _rigidbody.MovePosition(transform.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
