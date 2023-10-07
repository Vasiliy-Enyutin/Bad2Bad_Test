using Pathfinding;
using PlayerLogic;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class Enemy : MonoBehaviour
    {
        private AIDestinationSetter _destinationSetter = null!;
        private float _hp = 100;
        
        private void Awake()
        {
            _destinationSetter = GetComponent<AIDestinationSetter>();
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