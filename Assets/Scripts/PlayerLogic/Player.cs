using System;
using UnityEngine;

namespace PlayerLogic
{
    public class Player : MonoBehaviour
    {
        private float _hp;
        
        public event Action OnDestroy;

        public void Init(float hp)
        {
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
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}