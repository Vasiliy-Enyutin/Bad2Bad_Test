using System;
using Pathfinding;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class Enemy : MonoBehaviour
    {
        private AIDestinationSetter _destinationSetter = null!;
        
        public float CurrentHp { get; private set; }
        
        public event Action OnDamageReceived;

        private void Awake()
        {
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        public void Init(Transform playerTransform, float hp)
        {
            _destinationSetter.target = playerTransform;
            CurrentHp = hp;
        }

        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;
            OnDamageReceived?.Invoke();
            
            if (CurrentHp <= 0)
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