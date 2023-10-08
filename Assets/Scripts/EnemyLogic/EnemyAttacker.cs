using System.Collections;
using Descriptors;
using PlayerLogic;
using Services;
using UnityEngine;
using Zenject;

namespace EnemyLogic
{
    [RequireComponent(typeof(EnemyCollisionDetector))]
    [RequireComponent(typeof(EnemyAnimationController))]
    [RequireComponent(typeof(Enemy))]
    public class EnemyAttacker : MonoBehaviour
    {
        [Inject]
        private GameFactoryService _gameFactoryService = null!;
        
        private EnemyCollisionDetector _enemyCollisionDetector;
        private EnemyAnimationController _enemyAnimationController;
        private EnemyDescriptor _enemyDescriptor = null!;

        private bool _isNextToPlayer = false;
        private bool _isAttacking = false;

        private void Awake()
        {
            _enemyCollisionDetector = GetComponent<EnemyCollisionDetector>();
            _enemyAnimationController = GetComponent<EnemyAnimationController>();
            
            _enemyCollisionDetector.OnPlayerCollisionEnter += () => _isNextToPlayer = true;
            _enemyCollisionDetector.OnPlayerCollisionExit += () => _isNextToPlayer = false;
        }

        private void Start()
        {
            _enemyDescriptor = GetComponent<Enemy>().EnemyDescriptor;
        }

        private void Update()
        {
            if (_isNextToPlayer && !_isAttacking)
            {
                StartCoroutine(Attack());
                StartCoroutine(DamagePlayer());
            }
        }

        private IEnumerator Attack()
        {
            _enemyAnimationController.PlayAttackAnimation();
            _isAttacking = true;
            yield return new WaitForSeconds(_enemyDescriptor.TimeBetweenAttacks);
            _isAttacking = false;
            _enemyAnimationController.PlayIdleAnimation();
        }

        private IEnumerator DamagePlayer()
        {
            yield return new WaitForSeconds(_enemyDescriptor.TimeBeforeDamage);
            
            if (_gameFactoryService.Player.TryGetComponent(out Player player))
            {
                player.TakeDamage(_enemyDescriptor.Damage);
            }
        }
    }
}
