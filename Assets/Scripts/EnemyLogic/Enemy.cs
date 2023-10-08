using Pathfinding;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class Enemy : MonoBehaviour
    {
        private AIDestinationSetter _destinationSetter = null!;
        
        private float _hp;

        private void Awake()
        {
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        public void Init(Transform playerTransform, float hp)
        {
            _destinationSetter.target = playerTransform;
            _hp = hp;
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