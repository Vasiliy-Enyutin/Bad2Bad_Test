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
        [SerializeField]
        private Transform _hand;
        
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

            _moveDirection = new Vector3(_playerInputService.MoveDirection.x, _playerInputService.MoveDirection.y, 0);
            
            Move(_playerDescriptor.MoveSpeed);
            RotatePlayer();
        }

        private void Move(float moveSpeed)
        {
            if (_moveDirection.magnitude > 1)
            {
                _moveDirection.Normalize();
            }

            _rigidbody.MovePosition(transform.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        private void RotatePlayer()
        {
            if (_moveDirection.magnitude > 0)
            {
                if (_moveDirection.x < 0)
                {
                    _playerGfxTransform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    _playerGfxTransform.localScale = new Vector3(1, 1, 1);
                }

                MoveHandInMoveDirection();
            }
        }

        private void MoveHandInMoveDirection()
        {
            float angle = Mathf.Atan2(_moveDirection.y, Mathf.Abs(_moveDirection.x)) * Mathf.Rad2Deg;
            _hand.rotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(_moveDirection.x));
        }
    }
}
