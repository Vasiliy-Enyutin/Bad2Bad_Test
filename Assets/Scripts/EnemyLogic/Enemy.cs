using System;
using Descriptors;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        public float CurrentHp { get; private set; }
        
        public EnemyDescriptor EnemyDescriptor { get; private set; }
        
        public event Action OnDamageReceived;

        public void Init(EnemyDescriptor enemyDescriptor)
        {
            CurrentHp = enemyDescriptor.Hp;
            EnemyDescriptor = enemyDescriptor;
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