using Descriptors;
using Services;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TargetSearcher))]
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Transform _playerGfxTransform;
        [SerializeField]
        private Transform _hand;
        
        [Inject]
        private PlayerInputService _playerInputService = null!;
        
        private Rigidbody2D _rigidbody = null!;
        private TargetSearcher _targetSearcher = null!;
        private PlayerDescriptor _playerDescriptor = null!;

        private Vector3 _moveDirection;
        
        public bool FacingRight { get; private set; } = true;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _targetSearcher = GetComponent<TargetSearcher>();
            _playerDescriptor = GetComponent<Player>().PlayerDescriptor;
        }

        private void FixedUpdate()
        {
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
            if (_targetSearcher.TargetEnemy == null)
            {
                if (_moveDirection.magnitude > 0)
                {
                    UpdateGraphicsFlip(_moveDirection);
                    MoveHandInMoveDirection();
                }
            }
            else
            {
                Vector2 enemyDirection = _targetSearcher.TargetEnemy.transform.position - _hand.position;
                float angle = Mathf.Atan2(enemyDirection.y, enemyDirection.x) * Mathf.Rad2Deg;

                if (FacingRight)
                {
                    _hand.rotation = Quaternion.Euler(0, 0, angle);
                }
                else
                {
                    // Если персонаж отзеркален, отражаем угол поворота
                    angle += 180;
                    _hand.rotation = Quaternion.Euler(0, 0, angle);
                }

                UpdateGraphicsFlip(enemyDirection);
            }
        }

        private void UpdateGraphicsFlip(Vector3 direction)
        {
            if (direction.x < 0 && FacingRight)
            {
                FacingRight = false;
                _playerGfxTransform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x > 0 && !FacingRight)
            {
                FacingRight = true;
                _playerGfxTransform.localScale = new Vector3(1, 1, 1);
            }
        }
        
        private void MoveHandInMoveDirection()
        {
            float angle = Mathf.Atan2(_moveDirection.y, Mathf.Abs(_moveDirection.x)) * Mathf.Rad2Deg;
            _hand.rotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(_moveDirection.x));
        }
    }
}
