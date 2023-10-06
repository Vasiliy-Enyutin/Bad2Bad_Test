using Descriptors;
using EnemyLogic;
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
        private Enemy _targetEnemy;
        
        public bool FacingRight { get; private set; } = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _moveDirection = new Vector3(_playerInputService.MoveDirection.x, _playerInputService.MoveDirection.y, 0);
            UpdateTargetEnemy();
            Move(_playerDescriptor.MoveSpeed);
            RotatePlayer();
        }
        
        private void UpdateTargetEnemy()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _playerDescriptor.ShootTargetRadius, LayerMask.GetMask("Enemy"));

            if (colliders.Length > 0)
            {
                float closestDistance = float.MaxValue;
                Enemy closestEnemy = null;

                foreach (Collider2D collider in colliders)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy)
                    {
                        float distance = Vector2.Distance(transform.position, enemy.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestEnemy = enemy;
                        }
                    }
                }

                _targetEnemy = closestEnemy;
            }
            else
            {
                _targetEnemy = null;
            }
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
            if (_targetEnemy != null)
            {
                Vector2 enemyDirection = _targetEnemy.transform.position - _hand.position;
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

                // Отражение модели персонажа в зависимости от положения цели
                UpdateGraphicsFlip(enemyDirection);
                // if (enemyDirection.x < 0 && FacingRight)
                // {
                //     FacingRight = false;
                //     _playerGfxTransform.localScale = new Vector3(-1, 1, 1);
                // }
                // else if (enemyDirection.x > 0 && !FacingRight)
                // {
                //     FacingRight = true;
                //     _playerGfxTransform.localScale = new Vector3(1, 1, 1);
                // }
            }
            else if (_moveDirection.magnitude > 0)
            {
                UpdateGraphicsFlip(_moveDirection);
                // if (_moveDirection.x < 0 && FacingRight)
                // {
                //     FacingRight = false;
                //     _playerGfxTransform.localScale = new Vector3(-1, 1, 1);
                // }
                // else if (_moveDirection.x > 0 && !FacingRight)
                // {
                //     FacingRight = true;
                //     _playerGfxTransform.localScale = new Vector3(1, 1, 1);
                // }
                
                MoveHandInMoveDirection();
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
