using System;
using Descriptors;
using UnityEngine;

namespace PlayerLogic
{
    public class Player : MonoBehaviour
    {
        public float CurrentHp { get; private set; }
        
        public PlayerDescriptor PlayerDescriptor { get; private set; }

        public ItemDescriptor AmmoItemDescriptor { get; private set; }
        
        public event Action OnDestroy;

        public event Action OnDamageReceived; 

        public void Init(PlayerDescriptor playerDescriptor, ItemDescriptor ammoItemDescriptor)
        {
            CurrentHp = playerDescriptor.Hp;
            PlayerDescriptor = playerDescriptor;
            AmmoItemDescriptor = ammoItemDescriptor;
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
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}