using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        private float _hp = 100;
        
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