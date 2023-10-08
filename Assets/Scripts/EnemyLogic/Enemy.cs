using Pathfinding;
using PlayerLogic;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class Enemy : MonoBehaviour
    {
        private AIDestinationSetter _destinationSetter = null!;
        
        private Transform _playerTransform;
        private float _hp;

        private void Awake()
        {
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        public void Init(Transform playerTransform, float hp)
        {
            _playerTransform = playerTransform;
            _hp = hp;
        }

        private void Start()
        {
            _destinationSetter.target = FindObjectOfType<PlayerMovement>().transform;
        }

        public void TakeDamage(float damage)
        {
            _hp -= damage;
            
            if (_hp <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}